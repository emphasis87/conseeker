using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConSeeker.Shared.Services
{
    public interface IFatalErrorService
    {
        public event Action<string>? OnFatalError;

        public void Show(string message);
    }

    public class FatalErrorService : IFatalErrorService
    {
        public event Action<string>? OnFatalError;

        public void Show(string message)
        {
            OnFatalError?.Invoke(message);
        }
    }
}
