using bluepen.powershell.domain.entities;
using bluepen.powershell.domain.services.abstracts;
using bluepen.powershell.domain.services.interfaces;

namespace bluepen.powershell.services.factories
{
    /// <summary>
    /// Represents service creator factory responsible for creating Gmail Notification Service
    /// </summary>
    public class GmailServiceCreator : NotificationServiceCreator
    {
        /// <summary>
        /// Initializes a new instance of Gmail Service Creator Factory
        /// </summary>
        /// <param name="quickApplicant">The unique application account</param>
        public GmailServiceCreator(QuickApplicant quickApplicant) : base(quickApplicant) { }

        /// <summary>
        /// Gets instance of Gmail Notification service that implements INotificationService interface
        /// </summary>
        /// <returns>GmailNotificationService</returns>
        public override INotificationService GetNotificationService()
        {
            return new GmailNotificationService(applicant);
        }
    }
}
