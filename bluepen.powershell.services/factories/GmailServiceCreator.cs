using bluepen.powershell.domain.services.abstracts;
using bluepen.powershell.domain.services.interfaces;
using bluepen.powershell.services.validators;

namespace bluepen.powershell.services.factories
{
    /// <summary>
    /// Represents service creator factory responsible for creating Gmail Notification Service
    /// </summary>
    public class GmailServiceCreator : NotificationServiceCreator
    {
        /// <summary>
        /// Gets instance of Gmail Notification service that extends NotificationService base class that implements INotificationService interface
        /// </summary>
        /// <returns>GmailNotificationService</returns>
        public override INotificationService GetNotificationService()
        {
            return new GmailNotificationService(new QuickApplicantValidator());
        }
    }
}
