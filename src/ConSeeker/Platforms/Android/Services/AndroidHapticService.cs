using Android.Content;
using Android.OS;
using ConSeeker.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConSeeker.Services
{
    public class AndroidHapticService : IHapticService
    {
        public void ConfirmShakeDetected()
        {
            Vibrate(300, 1);
        }

        public static void Vibrate(int milliseconds, int strength = -1)
        {
            if (Platform.CurrentActivity == null)
                return;

            Vibrator? vibrator = null;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.S) // API 31+
            {
                // New API → use VibratorManager
                var vm = (VibratorManager?)Platform.CurrentActivity
                    .GetSystemService(Context.VibratorManagerService);
                vibrator = vm?.DefaultVibrator;
            }
            else
            {
                // Older API → fall back to old Vibrator service
                vibrator = (Vibrator?)Platform.CurrentActivity
                    .GetSystemService(Context.VibratorService);
            }

            if (vibrator == null || !vibrator.HasVibrator)
                return;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var effect = VibrationEffect.CreateOneShot(
                    milliseconds,
                    strength == -1 ? VibrationEffect.DefaultAmplitude : strength
                );
                vibrator.Vibrate(effect);
            }
            else
            {
                vibrator.Vibrate(milliseconds);
            }
        }
    }
}
