using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConSeeker.Services
{
    public interface IGeolocationService
    {
        Task<ConSeeker.Api.Data.Models.Location?> GetCurrentLocationAsync();
    }

    public class GeolocationService : IGeolocationService
    {
        private PermissionStatus? _locationPermission;

        public GeolocationService()
        {
            
        }

        public async Task<ConSeeker.Api.Data.Models.Location?> GetCurrentLocationAsync()
        {
            try
            {
                if (_locationPermission is null)
                {
                    if (MainThread.IsMainThread)
                    {
                        _locationPermission = await Permissions.RequestAsync<Permissions.LocationAlways>();
                    }
                    else
                    {
                        var tcs = new TaskCompletionSource();
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            _locationPermission = await Permissions.RequestAsync<Permissions.LocationAlways>();
                            tcs.SetResult();
                        });
                        await tcs.Task;
                    }
                }

                if (_locationPermission == PermissionStatus.Granted)
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
                    var location = await Geolocation.Default.GetLocationAsync(request);
                    if (location is not null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Lat: {location.Latitude}, Lon: {location.Longitude}");
                        return new ConSeeker.Api.Data.Models.Location
                        {
                            Latitude = location.Latitude,
                            Longitude = location.Longitude,
                            Accuracy = location.Accuracy,
                            Altitude = location.Altitude,
                            AltitudeAccuracy = location.VerticalAccuracy,
                            Course = location.Course,
                            Speed = location.Speed,
                            Timestamp = location.Timestamp
                        };
                    }
                }
            }
            catch (FeatureNotSupportedException ex)
            {
                System.Diagnostics.Debug.WriteLine($"GPS not supported on this device. {ex.Message}");
            }
            catch (FeatureNotEnabledException ex)
            {
                System.Diagnostics.Debug.WriteLine($"GPS is disabled. {ex.Message}");
            }
            catch (PermissionException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Location permission denied. {ex.Message}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading location: {ex.Message}");
            }

            return null;
        }
    }
}
