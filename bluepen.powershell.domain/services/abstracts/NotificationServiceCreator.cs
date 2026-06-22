using bluepen.powershell.domain.entities;
using bluepen.powershell.domain.services.interfaces;


namespace bluepen.powershell.domain.services.abstracts
{
    /// <summary>
    /// Represents abstract to design and create Abstract Factory via concrete classes for Notification Creators and their related product services
    /// </summary>
    public abstract class NotificationServiceCreator
    {       
        /// <summary>
        /// Gets notification service
        /// </summary>
        /// <returns>The notification service is created</returns>
        public abstract INotificationService GetNotificationService();
    }
}
