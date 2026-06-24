using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bluepen.powershell.domain.services.interfaces
{
    /// <summary>
    /// IMemoryLog
    /// </summary>
    public interface IMemoryLog : IDisposable
    {
        /// <summary>
        /// Log method
        /// </summary>
        /// <param name="quickApplicant"></param>
        void Log(string quickApplicant);
        /// <summary>
        /// GetLogs() method
        /// </summary>
        /// <returns></returns>
        List<string> GetLogs();
        /// <summary>
        /// ResetLogs() method
        /// </summary>
        void ResetLogs();
    }
}
