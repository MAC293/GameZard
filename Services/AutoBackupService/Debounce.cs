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
        private readonly Dictionary<String, Timer> _timers = new();

        public void Execute(String key, int delayMilliseconds, Action action)
        {
            if (_timers.ContainsKey(key))
            {
                _timers[key].Stop();
                _timers[key].Dispose();
                _timers.Remove(key);
            }

            var timer = new Timer(delayMilliseconds);
            timer.AutoReset = false; 
            timer.Elapsed += (s, e) =>
            {
                action();
                _timers.Remove(key);
                timer.Dispose();
            };

            _timers[key] = timer;
            timer.Start();
        }

        public void Dispose()
        {
            foreach (var timer in _timers.Values)
                timer.Dispose();

            _timers.Clear();
        }
    }
}
