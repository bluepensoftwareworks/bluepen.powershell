namespace bluepen.powershell.domain.services
{
    /// <summary>
    /// Represents memory log with basic caching capabilities withuot utilization of any extra libraries.
    /// </summary>
    public static class MemoryLog
    {
        private static readonly List<string> _logs = new List<string>();
        private static readonly object _lockObject = new object();

        /// <summary>
        /// Adds/Logs quick applicant information that was utilized to send notification to a group of recipients individually -OR- error(s) that were raised upon unsuccessful sent
        /// </summary>
        /// <param name="quickApplicant">quick application information -OR- error occured while notification was sent</param>
        public static void Log(string quickApplicant) {
            //thread safety synchronization
            lock (_lockObject) {
                _logs.Add(quickApplicant);
            }
        }

        /// <summary>
        /// Gets list of log records produced after all notifications were sent whether successfully or not
        /// </summary>
        /// <returns>list of log records</returns>
        public static List<string> GetLogs(){
            //thread safety synchronization
            lock (_lockObject)
            {
                return new List<string>(_logs);
            }
        }

        /// <summary>
        /// Resets memory log up on each new commandlet execution / invocation cycle
        /// </summary>
        public static void ResetLogs() {
            //thread safety synchronization
            lock (_lockObject)
            {
                _logs.Clear();
            }
        }
    }
}
