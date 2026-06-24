using bluepen.powershell.domain.emethods;
using bluepen.powershell.domain.entities;
using bluepen.powershell.domain.exceptions;
using bluepen.powershell.domain.services.interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace bluepen.powershell.services
{
    /// <summary>
    /// Represents a notification service interface that declares what needs to be implemented by concrete different notification service classes
    /// </summary>
    /// <remarks>
    /// This interface is a simple example for defining what different notification service classes need to implement
    /// </remarks>
    public class NotificationService: INotificationService
    {
        private bool _disposed = false;

        private readonly IMemoryLog memoryLog;
        private string multiProviderSMTP;
        protected IValidator validator;

        /// <summary>
        /// NotificationService
        /// </summary>
        public NotificationService(string multiProviderSMTP, IValidator validator, IMemoryLog memoryLog) {
            this.multiProviderSMTP = multiProviderSMTP;
            this.validator = validator;
            this.memoryLog = memoryLog;
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
        /// Notifies recipients individually with subject, topic, content, (optional attachment), signature
        /// </summary>  
        /// <returns>A <see cref="Task"/> that represents the asynchronous notify operation.</returns>
        public virtual async Task NotifyAsync(QuickApplicant quickApplicant, CancellationToken token)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    var validationResult = validator.Validate(quickApplicant);
                    if (!validationResult.IsValid)
                    {
                        throw new ContentProvidedException(validationResult.Errors);
                    }

                    await client.ConnectAsync(multiProviderSMTP, 465, SecureSocketOptions.SslOnConnect, token);
                    //Authenticate using your full Yahoo email address and the application password
                    await client.AuthenticateAsync(quickApplicant.Username, quickApplicant.Password, token);

                    string fileContents = quickApplicant.GetContent();

                    IList<string> recipients = quickApplicant.GetRecipients();


                    var bodyBuilder = new BodyBuilder
                    {

                        HtmlBody = fileContents.GetHTMLBody(quickApplicant.Topic, quickApplicant.Signature),
                        TextBody = fileContents.Replace("{topic}", quickApplicant.Topic).Replace("{signature}", quickApplicant.Signature)
                    };
                    //should be utilized when IsFile switch is present at the command prompt.
                    if (!string.IsNullOrEmpty(quickApplicant.AttachmentPath))
                    {
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

                            if (token.IsCancellationRequested) {
                                break;
                            }
                            //Send the message
                            await client.SendAsync(message, token);

                            string result = $"Username: {quickApplicant.Username}, Subject: {quickApplicant.Subject}, Topic: {quickApplicant.Topic}, Signature: {quickApplicant.Signature}";
                            memoryLog.Log(result);
                            await Task.Delay(TimeSpan.FromSeconds(5), token);
                        }
                        catch (Exception ex)
                        {
                            memoryLog.Log($"Failed to send email. Error: {ex.Message}");
                        }
                    }
                }
                catch (Exception e)
                {
                    memoryLog.Log($"{e.Message}");
                }
                finally
                {
                    //Disconnect from the server
                    await client.DisconnectAsync(true, token);
                }
            }
        }
    }
}
