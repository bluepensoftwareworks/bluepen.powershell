using bluepen.powershell.domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bluepen.powershel.services
{
    public static class MemoryLog
    {
        private static readonly List<string> _logs = new List<string>();
        private static readonly object _lockObject = new object();

        public static void Log(string quickApplicant) {
            lock (_lockObject) {
                _logs.Add(quickApplicant);
            }
        }

        public static List<string> GetLogs()
        {
            lock (_lockObject)
            {
                return new List<string>(_logs);
            }
        }
        public static void ResetLogs() {
            lock (_lockObject)
            {
                _logs.Clear();
            }
        }
    }
}
