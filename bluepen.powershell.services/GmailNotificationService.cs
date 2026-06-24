using bluepen.powershell.domain.entities;
using bluepen.powershell.domain.services;
using bluepen.powershell.domain.services.interfaces;

namespace bluepen.powershell.services
{
    /// <summary>
    /// Represents Notification Service that uses Gmail mail service to send out mail notifications using MailKit and MimeKit packages
    /// </summary>
    /// <remarks>
    /// This class provides methods for disposing Gmail Notification Service and notifying receiver with a message containing subject, topic, attachment and body text
    /// </remarks>
    public class GmailNotificationService: NotificationService
    {
        /// <summary>
        /// Instantiates new instance of Gmail Notification service
        /// </summary>
        /// <param name="quickApplicant">the unique applicant account</param>
        public GmailNotificationService(IValidator validator, IMemoryLog memoryLog):base("smtp.gmail.com", validator, memoryLog) {}

        /// <summary>
        /// Notifies a specific set of recipients with notification message, subject, topic, optional attachment, and defined signature with utilization of Gmail Mail service
        /// </summary>
        /// <returns></returns>
        public async Task NotifyAsync(QuickApplicant quickApplicant, CancellationToken token)
        {
            await base.NotifyAsync(quickApplicant, token);
        }
    }
}
