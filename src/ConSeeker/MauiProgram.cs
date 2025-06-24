using ConSeeker.Services;
using ConSeeker.Shared.Model;
using ConSeeker.Shared.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;

namespace ConSeeker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the ConSeeker.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            
#if DEBUG
            var env = "Development";
#else
            var env = "Production";
#endif
            builder.Configuration["Environment:Name"] = env;

#if WINDOWS
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ConSeeker", "data", "db.sqlite");
#else
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "data", "db.sqlite");
#endif
            var csBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = dbPath,
                ForeignKeys = true,
                Pooling = true
            };
            var cs = csBuilder.ConnectionString;

            builder.Services.AddDbContext<ConSeekerDbContext>(options =>
            {
                options.UseSqlite(cs);
            });

            var app = builder.Build();

            

            return app;
        }
    }
}
