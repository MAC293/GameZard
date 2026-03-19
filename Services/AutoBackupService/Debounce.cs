using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace GameZard.Services.AutoBackupService
{
    public class Debounce : IDisposable
    {
        private Dictionary<String, Timer> _Timers;

        public Debounce()
        {
            Timers = new Dictionary<String, Timer>();
        }

        public Dictionary<String, Timer> Timers
        {
            get { return _Timers; }
            set { _Timers = value; }
        }

        public void Execute(String key, int delayMilliseconds, Action action)
        {
            if (Timers.ContainsKey(key))
            {
                Timers[key].Stop();
                Timers[key].Dispose();
                Timers.Remove(key);
            }

            var timer = new Timer(delayMilliseconds);
            timer.AutoReset = false;
            timer.Elapsed += (s, e) =>
            {
                action();
                Timers.Remove(key);
                timer.Dispose();
            };

            Timers[key] = timer;
            timer.Start();
        }

        public void Dispose()
        {
            foreach (var timer in Timers.Values)
                timer.Dispose();

            Timers.Clear();
        }
    }
}
