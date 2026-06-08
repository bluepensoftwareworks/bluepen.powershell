using bluepen.powershell.domain.entities;
using bluepen.powershell.domain.services.interfaces;


namespace bluepen.powershell.domain.services.abstracts
{
    /// <summary>
    /// Represents abstract to design and create Abstract Factory via concrete classes for Notification Creators and their related product services
    /// </summary>
    public abstract class NotificationServiceCreator
    {
        protected QuickApplicant applicant;
        /// <summary>
        /// Initializes a new factory instance of a derived concrete class
        /// </summary>
        /// <param name="applicant">The unique applicant who is sending notification</param>
        public NotificationServiceCreator(QuickApplicant applicant)
        {
            this.applicant = applicant;
        }
        /// <summary>
        /// Gets notification service
        /// </summary>
        /// <returns>The notification service is created</returns>
        public abstract INotificationService GetNotificationService();
    }
}
