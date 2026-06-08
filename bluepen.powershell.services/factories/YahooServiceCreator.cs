using bluepen.powershell.domain.entities;
using bluepen.powershell.domain.services.abstracts;
using bluepen.powershell.domain.services.interfaces;

namespace bluepen.powershell.services.factories
{
    /// <summary>
    /// Represents service creator factory responsible for creating Yahoo Notification Service
    /// </summary>
    public class YahooServiceCreator : NotificationServiceCreator
    {
        /// <summary>
        /// Initializes a new instance of Yahoo Service Creator Factory
        /// </summary>
        /// <param name="quickApplicant">The unique application account</param>
        public YahooServiceCreator(QuickApplicant quickApplicant) : base(quickApplicant) { }

        /// <summary>
        /// Gets instance of Yahoo Notification service that implements INotificationService interface
        /// </summary>
        /// <returns>YahooNotificationService</returns>
        public override INotificationService GetNotificationService()
        {
            return new YahooNotificationService(applicant);
        }
    }
}
