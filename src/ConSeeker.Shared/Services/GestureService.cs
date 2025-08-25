using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConSeeker.Shared.Services
{
    public interface IGestureService
    {
        event Action DoubleShakeDetected;
        void StartGestureDetection();
    }
}
