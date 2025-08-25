using ConSeeker.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

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
