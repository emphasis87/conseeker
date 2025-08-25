using ConSeeker.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConSeeker.Services
{
    public class ExitService : IExitService
    {
        public void Exit()
        {
#if ANDROID
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
#elif WINDOWS
            Microsoft.Maui.Controls.Application.Current?.Quit();
#elif IOS || MACCATALYST
            // iOS/macOS: just block navigation, can't force exit
#endif
        }
    }
}
