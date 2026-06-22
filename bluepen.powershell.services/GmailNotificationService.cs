using bluepen.powershell.domain.entities;
using bluepen.powershell.domain.services.interfaces;
using bluepen.powershell.services.emethods;
using bluepen.powershell.services.exceptions;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.ComponentModel.DataAnnotations;

namespace bluepen.powershell.services
{
    /// <summary>
    /// Represents Notification Service that uses Gmail mail service to send out mail notifications using MailKit and MimeKit packages
    /// </summary>
    /// <remarks>
    /// This class provides methods for disposing Gmail Notification Service and notifying receiver with a message containing subject, topic, attachment and body text
    /// </remarks>
    public class GmailNotificationService: INotificationService
    {
        protected IValidator validator;

        private bool _disposed = false;

        /// <summary>
        /// Instantiates new instance of Gmail Notification service
        /// </summary>
        /// <param name="quickApplicant">the unique applicant account</param>
        public GmailNotificationService(IValidator validator) {
            this.validator = validator;
        }

        /// <summary>
        /// disposes of Gmail notification service instance
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
                    
                }

                // Clean up unmanaged resources here, regardless of the 'disposing' value
                // e.g., CloseHandle(_unmanagedResource);

                _disposed = true; // Mark as disposed
            }
        }

        /// <summary>
        /// Notifies a specific set of recipients with notification message, subject, topic, optional attachment, and defined signature with utilization of Gmail Mail service
        /// </summary>
        /// <returns></returns>
        public async Task NotifyAsync(QuickApplicant quickApplicant)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    var validationResult = validator.Validate(quickApplicant);
                    if (!validationResult.IsValid) {
                        throw new ContentProvidedException(validationResult.Errors);
                    }

                    await client.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                    //Authenticate using your full Yahoo email address and the application password
                    await client.AuthenticateAsync(quickApplicant.Username, quickApplicant.Password);

                    string fileContents = quickApplicant.GetContent();

                    IList<string> recipients = quickApplicant.GetRecipients();


                    var bodyBuilder = new BodyBuilder
                    {

                        HtmlBody = fileContents.GetHTMLBody(quickApplicant.Topic, quickApplicant.Signature),
                        TextBody = fileContents.Replace("{topic}", quickApplicant.Topic).Replace("{signature}", quickApplicant.Signature)
                    };
                    //should be utilized when IsFile switch is present at the command prompt.
                    if (!string.IsNullOrEmpty(quickApplicant.AttachmentPath)) {
                        FileInfo fileInfo = new FileInfo(quickApplicant.AttachmentPath);
                        if (fileInfo.Exists)
                        {
                            bodyBuilder.Attachments.Add(Path.GetFileName(quickApplicant.AttachmentPath), File.ReadAllBytes(quickApplicant.AttachmentPath));
                        }
                    }

                    foreach (string recipient in recipients)
                    {
                        try
                        {
                            var message = new MimeMessage();
                            message.From.Add(new MailboxAddress(quickApplicant.Signature, quickApplicant.Username));
                            message.To.Add(new MailboxAddress(null, recipient));
                            message.Subject = quickApplicant.Subject;

                            message.Body = bodyBuilder.ToMessageBody();

                            //Send the message
                            await client.SendAsync(message);
                            string result = $"Username: {quickApplicant.Username}, Subject: {quickApplicant.Subject}, Topic: {quickApplicant.Topic}, Signature: {quickApplicant.Signature}";
                            MemoryLog.Log(result);
                        }
                        catch (Exception ex)
                        {
                            MemoryLog.Log($"Failed to send email. Error: {ex.Message}");
                        }
                    }
                }
                catch (Exception e)
                {
                    MemoryLog.Log($"{e.Message}");
                }
                finally
                {
                    //Disconnect from the server
                    await client.DisconnectAsync(true);
                }
            }
        }
    }
}
