using bluepen.powershell.services.emethods;
using bluepen.powershell.domain.entities;
using bluepen.powershell.domain.services.interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


namespace bluepen.powershell.services
{
    /// <summary>
    /// Represents Notification Service that uses Yahoo mail service to send out mail notifications using MailKit and MimeKit packages
    /// </summary>
    /// <remarks>
    /// This class provides methods for disposing Yahoo Notification Service and notifying receiver with a message containing subject, topic, attachment and body text
    /// </remarks>
    public  class YahooNotificationService: INotificationService
    {
        /// <summary>
        /// Gets, Sets QuickApplicant
        /// </summary>
        protected QuickApplicant quickApplicant;

        private bool _disposed = false;

        public YahooNotificationService(QuickApplicant quickApplicant) {
            this.quickApplicant = quickApplicant;
        }

        /// <summary>
        /// Deposits existing Yahoo Notification Service
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
        /// Notifies 
        /// </summary>
        /// <returns></returns>
        public async Task NotifyAsync() {
            using (var client = new SmtpClient()) {
                try
                {
                    await client.ConnectAsync("smtp.mail.yahoo.com", 465, SecureSocketOptions.SslOnConnect);
                    //Authenticate using your full Yahoo email address and the application password
                    await client.AuthenticateAsync(quickApplicant.Username, quickApplicant.Password);

                    string fileContents = quickApplicant.GetContent();
                    IList<string> recipients = quickApplicant.GetRecipients();

                    foreach (string recipient in recipients)
                    {
                        try
                        {
                            var message = new MimeMessage();
                            message.From.Add(new MailboxAddress(quickApplicant.Signature, quickApplicant.Username));
                            message.To.Add(new MailboxAddress(null, recipient));
                            message.Subject = quickApplicant.Subject;
                                                        
                            var bodyBuilder = new BodyBuilder { HtmlBody = fileContents.GetHTMLBody(quickApplicant.Topic, quickApplicant.Signature),
                                                                TextBody = fileContents.Replace("{topic}", quickApplicant.Topic).Replace("{signature}", quickApplicant.Signature)};
                            //should be utilized when IsFile switch is present at the command prompt.
                            if (!string.IsNullOrEmpty(quickApplicant.Attachment)) {
                                if (quickApplicant.Attachment.IndexOfAny(new char[] { '\\', '/', ':' }) != -1) {                                    
                                    bodyBuilder.Attachments.Add(Path.GetFileName(quickApplicant.Attachment), File.ReadAllBytes(quickApplicant.Attachment));
                                }
                            }

                            message.Body = bodyBuilder.ToMessageBody();

                            //Send the message
                            await client.SendAsync(message);
                            string result = $"Username: {quickApplicant.Username}, Password: {quickApplicant.Password}, Subject: {quickApplicant.Subject}, Topic: {quickApplicant.Topic}, Recipient: {recipient}, " +
                                $"Content: {quickApplicant.Content}, Attachment: {quickApplicant.Attachment}, Signature: {quickApplicant.Signature}";
                            MemoryLog.Log(result);
                            Thread.Sleep(TimeSpan.FromSeconds(5));
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
                finally {
                    //Disconnect from the server
                    await client.DisconnectAsync(true);
                }
            }
        }
    }
}
