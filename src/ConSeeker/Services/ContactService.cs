using ConSeeker.Api.Data.Models;
using Sentry.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ConSeeker.Services
{
    public interface IContactService
    {
        Task AnnounceLocationAsync();
    }

    public class ContactService : IContactService
    {
        private readonly IGeolocationService _geolocationService;
        private readonly HttpClient _client;

        public ContactService(IGeolocationService geolocationService, IHttpClientFactory httpClientFactory)
        {
            _geolocationService = geolocationService;
            _client = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task AnnounceLocationAsync()
        {
            var location = await _geolocationService.GetCurrentLocationAsync();
            if (location is not null)
            {
                var request = new AnnouncementContactRequest
                {
                    Location = location
                };

                try
                {
                    var response = await _client.PostAsJsonAsync("/contact/announce", request);
                    if (response.IsSuccessStatusCode)
                    {
                        System.Diagnostics.Debug.WriteLine("Location sent successfully");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Failed to send location: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to send location: {ex.Message}");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Failed to obtain location");
            }
        }
    }
}
