using bluepen.powershell.services.emethods;
using bluepen.powershell.services.exceptions;
using bluepen.powershell.domain.entities;
using bluepen.powershell.domain.services.interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace bluepen.powershell.services
{
    public class GmailNotificationService: INotificationService
    {
        protected QuickApplicant quickApplicant;

        private bool _disposed = false;

        public GmailNotificationService(QuickApplicant quickApplicant) {
            this.quickApplicant = quickApplicant;
        }

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

        public async Task NotifyAsync()
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                    //client.Connect ("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    //Authenticate using your full Yahoo email address and the app password
                    await client.AuthenticateAsync(quickApplicant.Username, quickApplicant.Password);

                    string fileContents = quickApplicant.GetContent();

                    if (string.IsNullOrEmpty(fileContents)) {
                        throw new ContentProvidedException("Content provided is something else...There is an issue...");
                    }

                    IList<string> recipients = quickApplicant.GetRecipients();

                    if (recipients == null)
                    {
                        throw new ContentProvidedException("Content provided is something else...There is an issue...");
                    }


                    foreach (string recipient in recipients)
                    {
                        try
                        {
                            var message = new MimeMessage();
                            message.From.Add(new MailboxAddress(quickApplicant.Signature, quickApplicant.Username));
                            message.To.Add(new MailboxAddress(null, recipient));
                            message.Subject = quickApplicant.Subject;

                            var bodyBuilder = new BodyBuilder
                            {
                                
                                HtmlBody = fileContents.GetHTMLBody(quickApplicant.Topic, quickApplicant.Signature ),
                                TextBody = fileContents.Replace("{topic}", quickApplicant.Topic).Replace("{signature}", quickApplicant.Signature)
                            };

                            if (!string.IsNullOrEmpty(quickApplicant.Attachment))
                            {
                                if (quickApplicant.Attachment.IndexOfAny(new char[] { '\\', '/', ':' }) != -1)
                                {
                                    bodyBuilder.Attachments.Add(Path.GetFileName(quickApplicant.Attachment), File.ReadAllBytes(quickApplicant.Attachment));
                                }
                            }

                            message.Body = bodyBuilder.ToMessageBody();

                            //Send the message
                            await client.SendAsync(message);
                            string result = $"Username: {quickApplicant.Username}, Password: {quickApplicant.Password}, Subject: {quickApplicant.Subject}, Topic: {quickApplicant.Topic}, Recipient: {recipient}, " +
                                $"Content: {quickApplicant.Content}, Attachment: {quickApplicant.Attachment}, Signature: {quickApplicant.Signature}";
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
