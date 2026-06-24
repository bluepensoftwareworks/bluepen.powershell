using bluepen.powershell.domain.services.interfaces;

namespace bluepen.powershell.domain.services
{
    /// <summary>
    /// Represents memory log with basic caching capabilities withuot utilization of any extra libraries.
    /// </summary>
    public class MemoryLog : IMemoryLog
    {
        private bool _disposed = false;
        private readonly List<string> _logs = new List<string>();        

        /// <summary>
        /// Adds/Logs quick applicant information that was utilized to send notification to a group of recipients individually -OR- error(s) that were raised upon unsuccessful sent
        /// </summary>
        /// <param name="quickApplicant">quick application information -OR- error occured while notification was sent</param>
        public void Log(string quickApplicant) {            
            _logs.Add(quickApplicant);            
        }

        /// <summary>
        /// Gets list of log records produced after all notifications were sent whether successfully or not
        /// </summary>
        /// <returns>list of log records</returns>
        public List<string> GetLogs(){            
            return new List<string>(_logs);            
        }

        /// <summary>
        /// Resets memory log up on each new commandlet execution / invocation cycle
        /// </summary>
        public void ResetLogs() {                        
            _logs.Clear();            
        }

        /// <summary>
        /// disposes of MemoryLog notification service instance
        /// </summary>
        public void Dispose()
        {
            // Call our private Dispose method. Pass 'true' to indicate deterministic disposal.
            Dispose(true);

            // Suppress finalization. This takes the object off the finalization queue
            // and prevents the finalizer from running a second time.
            GC.SuppressFinalize(this);
        }

        // Protected virtual method for derived classes to override
        // The 'disposing' parameter indicates the source of the call.
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose of managed resources (other objects that implement IDisposable)
                    if (_logs != null) {
                        _logs.Clear();                        
                    }
                }

                // Clean up unmanaged resources here, regardless of the 'disposing' value
                // e.g., CloseHandle(_unmanagedResource);

                _disposed = true; // Mark as disposed
            }
        }
    }
}
