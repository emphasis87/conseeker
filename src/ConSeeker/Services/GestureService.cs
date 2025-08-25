using ConSeeker.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConSeeker.Services
{
    public class GestureService : IGestureService, IDisposable
    {
        public GestureService()
        {
            
        }

        public event Action DoubleShakeDetected = () => { };

        public void StartGestureDetection()
        {
            if (Accelerometer.IsSupported)
            {
                Accelerometer.ReadingChanged += OnReadingChanged;
                Accelerometer.Start(SensorSpeed.Game);
            }
        }

        private enum ShakeDirection { Left, Right };

        private int _shakeCount;
        private double _lastShake;
        private long _lastShakeTime;

        private void OnReadingChanged(object? sender, AccelerometerChangedEventArgs e)
        {
            var x = e.Reading.Acceleration.X;

            var prev = _lastShakeTime;
            var current = _lastShakeTime = System.Diagnostics.Stopwatch.GetTimestamp();
            var elapsed = System.Diagnostics.Stopwatch.GetElapsedTime(prev, current);
            if (elapsed > TimeSpan.FromMilliseconds(1500))
            {
                _shakeCount = 0;
            }

            if (Math.Abs(x) > 0.6)
            {
                if (_shakeCount == 0)
                {
                    _shakeCount = 1;
                    _lastShake = x;
                    _lastShakeTime = current;
                }
                else if (Math.Sign(_lastShake) != Math.Sign(x))
                {
                    _shakeCount++;
                    _lastShake = x;
                    _lastShakeTime = current;
                }
            }

            if (_shakeCount == 3)
            {
                _shakeCount = 0;
                DoubleShakeDetected();
            }
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);

            if (Accelerometer.IsSupported)
            {
                Accelerometer.ReadingChanged -= OnReadingChanged;
                Accelerometer.Stop();
            }
        }
    }
}
