using ConSeeker.Services;
using ConSeeker.Shared.Model;
using ConSeeker.Shared.Services;
using FluentMigrator.Runner;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConSeeker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var ex = e.ExceptionObject as Exception;
                System.Diagnostics.Debug.WriteLine($"[UnhandledException] {ex}");
            };
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine($"[UnobservedTaskException] {e.Exception}");
                e.SetObserved(); // prevent app crash
            };
#if WINDOWS
            Microsoft.UI.Xaml.Application.Current.UnhandledException += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine($"[WinUI UnhandledException] {e.Exception}");
                e.Handled = true;
            };
#elif ANDROID
            Android.Runtime.AndroidEnvironment.UnhandledExceptionRaiser += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine($"[Android UnhandledException] {e.Exception}");
                e.Handled = true;
            };
#endif

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the ConSeeker.Shared project
            builder.Services.AddSingleton<IFatalErrorService, FatalErrorService>();
            builder.Services.AddSingleton<IFormFactor, FormFactor>();
            builder.Services.AddSingleton<IExitService, ExitService>();
            builder.Services.AddSingleton<IGeolocationService, GeolocationService>();
            builder.Services.AddSingleton<IContactService, ContactService>();
            builder.Services.AddSingleton<IGestureService, GestureService>();
#if ANDROID
            builder.Services.AddSingleton<IHapticService, AndroidHapticService>();
#else
            builder.Services.AddSingleton<IHapticService, HapticService>();
#endif

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            // CONFIGURATION
#if DEBUG
            var env = "Development";
#else
            var env = "Production";
#endif
            builder.Configuration["Environment:Name"] = env;

            // DATABASE
            ConfigureDatabase(builder);

            // API CLIENT
            builder.Services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7205");
            });

            var app = builder.Build();

            RunMigrations(app);

            var haptics = app.Services.GetRequiredService<IHapticService>();
            var gestures = app.Services.GetRequiredService<IGestureService>();
            var geolocationService = app.Services.GetRequiredService<IGeolocationService>();
            var locationService = app.Services.GetRequiredService<IContactService>();

            Task.Run(() =>
            {
                gestures.DoubleShakeDetected += () =>
                {
                    Task.Run(async () =>
                    {
                        haptics.ConfirmShakeDetected();

                        await locationService.AnnounceLocationAsync();
                    });
                };
                gestures.StartGestureDetection();
            });

            return app;
        }

        private static void RunMigrations(MauiApp app)
        {
            // ENABLE FOREIGN KEYS
            var connectionString = app.Configuration["ConnectionStrings:ConSeeker"];
            var connection = new SqliteConnection(connectionString);
            connection.Open();
            try
            {
                // Enable foreign keys
                using var cmd = connection.CreateCommand();
                cmd.CommandText = "PRAGMA foreign_keys=ON;";
                cmd.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }

            // RUN MIGRATIONS
            using var scope = app.Services.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

        private static void ConfigureDatabase(MauiAppBuilder builder)
        {
#if DEBUG
            var dbname = "conseeker-dev.sqlite";
#else
            var dbname = "conseeker.sqlite";
#endif

            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

#if WINDOWS
            var dbpath = Path.Combine(appdata, "ConSeeker", dbname);
#else
            var dbpath = Path.Combine(FileSystem.AppDataDirectory, dbname);
#endif

#if DEBUG
            if (File.Exists(dbpath))
            {
                File.Delete(dbpath);
            }
#endif

            var connectionStringBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = dbpath,
                ForeignKeys = true,
                Pooling = true
            };

            var connectionString = connectionStringBuilder.ConnectionString;

            builder.Configuration["ConnectionStrings:ConSeeker"] = connectionString;

            // ENTITY FRAMEWORK
            builder.Services.AddDbContext<ConSeekerDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            // MIGRATIONS
            builder.Services.AddFluentMigratorCore()
                .ConfigureRunner(rb =>
                {
                    rb.AddCustomSQLite(compatibilityMode: CompatibilityMode.LOOSE);
                    rb.WithGlobalConnectionString(connectionString)
                        .ScanIn(typeof(ConSeeker.Shared.Module).Assembly)
                        .ScanIn(typeof(ConSeeker.Module).Assembly)
                        .For.Migrations();
                });
        }
    }
}
