using bluepen.powershell.domain.entities;
using bluepen.powershell.domain.services;
using bluepen.powershell.domain.services.interfaces;


namespace bluepen.powershell.services
{
    /// <summary>
    /// Represents Notification Service that uses Yahoo mail service to send out mail notifications using MailKit and MimeKit packages
    /// </summary>
    /// <remarks>
    /// This class provides methods for disposing Yahoo Notification Service and notifying receiver with a message containing subject, topic, attachment and body text
    /// </remarks>
    public  class YahooNotificationService: NotificationService
    {
        /// <summary>
        /// YahooNotificationService
        /// </summary>
        /// <param name="validator"></param>
        public YahooNotificationService(IValidator validator): base("smtp.mail.yahoo.com", validator) {}        

        /// <summary>
        /// Notifies 
        /// </summary>
        /// <returns></returns>
        public async Task NotifyAsync(QuickApplicant quickApplicant) {
            await base.NotifyAsync(quickApplicant);
        }
    }
}
