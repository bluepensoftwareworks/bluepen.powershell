using bluepen.powershell.domain.services;
using bluepen.powershell.domain.services.abstracts;
using bluepen.powershell.domain.services.interfaces;
using bluepen.powershell.services.validators;

namespace bluepen.powershell.services.factories
{
    /// <summary>
    /// Represents service creator factory responsible for creating Yahoo Notification Service
    /// </summary>
    public class YahooServiceCreator : NotificationServiceCreator
    {
        private readonly IMemoryLog memoryLog;

        /// <summary>
        /// YahooServiceCreator
        /// </summary>
        /// <param name="memoryLog"></param>
        public YahooServiceCreator(IMemoryLog memoryLog) {
            this.memoryLog = memoryLog;
        }

        /// <summary>
        /// Gets instance of Yahoo Notification service that extends NotificationService class that implements INotificationService interface
        /// </summary>
        /// <returns>YahooNotificationService</returns>
        public override INotificationService GetNotificationService()
        {
            return new YahooNotificationService(new QuickApplicantValidator(), memoryLog);
        }
    }
}
