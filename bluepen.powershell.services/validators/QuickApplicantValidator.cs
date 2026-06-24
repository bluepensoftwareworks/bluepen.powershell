using bluepen.powershell.domain.entities;
using bluepen.powershell.domain.services;
using bluepen.powershell.domain.services.interfaces;
using MimeKit;
using System.Net.Mail;

namespace bluepen.powershell.services.validators
{
    /// <summary>
    /// QuickApplicantValidator
    /// </summary>
    public class QuickApplicantValidator : IValidator
    {
        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="quickApplicant"></param>
        /// <returns>ValidationResult</returns>
        public ValidationResult Validate(QuickApplicant quickApplicant)
        {
            var results = new ValidationResult();

            if (string.IsNullOrEmpty(quickApplicant.Username)) {
                results.Errors.Add("Username is required.");                
            }
            if (string.IsNullOrEmpty(quickApplicant.Password)){
                results.Errors.Add("Application Password is required.");
            }
            if (string.IsNullOrEmpty(quickApplicant.Subject)) {
                results.Errors.Add("Subject is required.");
            }
            if (string.IsNullOrEmpty(quickApplicant.Topic))
            {
                results.Errors.Add("Topic is required.");
            }
            if (string.IsNullOrEmpty(quickApplicant.Signature))
            {
                results.Errors.Add("Signature is required.");
            }            
            if (quickApplicant.IsFile)
            {
                if (string.IsNullOrEmpty(quickApplicant.RecipientPath))
                {
                    results.Errors.Add("RecipientPath is required.");
                }
                else 
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(quickApplicant.RecipientPath);
                        if (fileInfo.Exists)
                        {
                            MailboxAddress? mailboxAddress = null;
                            var recipients = File.ReadAllText(fileInfo.FullName).Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();

                            if (recipients.Any(g => !MailboxAddress.TryParse(g, out mailboxAddress)))
                            {
                                results.Errors.Add("One of the recipients email addresses is not properly formatted at least");
                            }
                            //divide bytes by 1024 to force a double result
                            double sizeInKB = fileInfo.Length / 1024;
                            if (sizeInKB > 50)
                            {
                                results.Errors.Add("Recipient File size is greater than 50KB");
                            }
                        }
                        else
                        {
                            results.Errors.Add("File with recipients does not exist.");
                        }
                    }
                    catch (Exception e) {
                        results.Errors.Add(e.Message);
                    }
                }
                if (string.IsNullOrEmpty(quickApplicant.ContentPath))
                {
                    results.Errors.Add("ContentPath is required.");
                }
                else 
                {
                    try {
                        FileInfo fileInfo = new FileInfo(quickApplicant.ContentPath);
                        if (fileInfo.Exists)
                        {
                            var content = File.ReadAllText(fileInfo.FullName);
                            if (string.IsNullOrEmpty(content))
                            {
                                results.Errors.Add("File Content is empty. We need some content in the file.");
                            }
                            else {
                                //divide bytes by 1024 to force a double result
                                double sizeInKB = fileInfo.Length / 1024;
                                if (sizeInKB > 100)
                                {
                                    results.Errors.Add("Content File size is greater than 100KB");
                                }
                            }
                        }
                        else
                        {
                            results.Errors.Add("File with content does not exist.");
                        }
                    }
                    catch (Exception e) {
                        results.Errors.Add(e.Message);
                    }
                }
            }
            else {
                if (quickApplicant.Recipients == null || !quickApplicant.Recipients.Any()){
                    results.Errors.Add("Recipients list needs at least one recipient");
                }else {
                    MailboxAddress? mailboxAddress = null;
                    if (quickApplicant.Recipients.Any(g => !MailboxAddress.TryParse(g, out mailboxAddress))){
                        results.Errors.Add("One of the recipients email addresses is not properly formatted at least");
                    }
                }
                if (string.IsNullOrEmpty(quickApplicant.Content)) {
                    results.Errors.Add("Content is required.");
                }
            }
            if (!string.IsNullOrEmpty(quickApplicant.AttachmentPath)) {
                try {
                    FileInfo fileInfo = new FileInfo(quickApplicant.AttachmentPath);
                    if (fileInfo.Exists)
                    {
                        //divide bytes by 1024 to force a double result
                        double sizeInKB = fileInfo.Length / 1024;
                        if (sizeInKB > 300)
                        {
                            results.Errors.Add("Attachment File size is greater than 300KB");
                        }
                    }
                    else {
                        results.Errors.Add("Attachment File does not exist.");
                    }
                }catch (Exception e) {
                    results.Errors.Add(e.Message);
                }
            }
            return results;
        }
    }
}
