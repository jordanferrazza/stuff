using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuffProject
{
    public class ExceptionSandbox<T> where T : Exception
    {
        public Action<T> Catch { get; set; }

        public Action EntryWatermark { get; set; } = () => { };
        public Action ExitWatermark { get; set; } = () => { };

        public void Run(Action action)
        {
            try
            {
                EntryWatermark();
                action();
                ExitWatermark();
            }
            catch (T ex)
            {
                Catch(ex);
            }
        }

        public ExceptionSandbox(Action<T> onError)
        {
            Catch = onError;
        }
        public ExceptionSandbox(Action onEnter, Action<T> onError, Action onExit)
        {
            EntryWatermark = onEnter;
            Catch = onError;
            ExitWatermark = onExit;
        }


    }
}
