using ConSeeker.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureHttpsDefaults(o => { });
});

var app = builder.Build();

// Log actual local IPs
var hostName = System.Net.Dns.GetHostName();
var addresses = System.Net.Dns.GetHostAddresses(hostName)
    .Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

foreach (var addr in addresses)
{
    app.Logger.LogInformation("{IPAddress}", addr);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
else
{
    app.UseHttpsRedirection();
}

app.MapGet("/info", () =>
{
    return "SUCCESS";
});

app.MapPost("/contact/announce", (AnnouncementContactRequest? request) =>
{
    System.Diagnostics.Debug.WriteLine("Received contact announcement request.");
})
.WithName("AnnounceContact");

app.Run();
