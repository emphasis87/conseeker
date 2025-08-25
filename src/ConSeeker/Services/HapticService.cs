using ConSeeker.Shared.Services;

namespace ConSeeker.Services
{
    public class HapticService : IHapticService
    {
        public void ConfirmShakeDetected()
        {
            try
            {
                Vibration.Vibrate(TimeSpan.FromMilliseconds(300));
            }
            catch (FeatureNotSupportedException)
            {
                // Vibration not supported on device
            }
            catch (Exception ex)
            {
                // Handle unexpected errors (permissions, hardware issues, etc.)
                System.Diagnostics.Debug.WriteLine($"Vibration failed: {ex.Message}");
            }
        }
    }
}
