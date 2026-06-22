using bluepen.powershell.domain.entities;
using bluepen.powershell.services.exceptions;

namespace bluepen.powershell.services.emethods
{
    /// <summary>
    /// Represents class with extension methods
    /// </summary>
    public static class QuickApplicantExtensions
    {
        /// <summary>
        /// To keep Domain-Driven Design or Clean Architecture, we often want our domain models or entities to be "pure" data containers without heavy dependency on UI, Web, or external
        /// infrastructure frameworks
        /// </summary>
        /// <param name="quickApplicant">represents unique applicant account</param>
        /// <returns>content read and returned from either command prompt parameter or from input file based ona File switch</returns>
        public static string GetContent(this QuickApplicant quickApplicant) {

            string fileContents = string.Empty;

            //sort out content...
            if (quickApplicant.IsFile)
            {
                FileInfo fileInfo = new FileInfo(quickApplicant.ContentPath);

                if (fileInfo.Exists)
                {
                    //we have a file...
                    fileContents = File.ReadAllText(quickApplicant.ContentPath);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(quickApplicant.Content))
                {
                    fileContents = quickApplicant.Content;
                }
            }
            //sort out content...
            return fileContents;

        }

        /// <summary>
        /// To keep Domain-Driven Design or Clean Architecture, we often want our domain models or entities to be "pure" data containers without heavy dependency on UI, Web ir external 
        /// infrastructure framework
        /// </summary>
        /// <param name="quickApplicant">represents unique applicant account</param>
        /// <returns>collection of receipts read from command prompt parameter or from input recipients file</returns>
        /// <exception cref="ContentProvidedException">Thrown when recipients list is zero or null</exception>
        public static IList<string> GetRecipients(this QuickApplicant quickApplicant) {

            IList<string>? recipients = null;

            //sort out recipients...
            if (quickApplicant.IsFile)
            {
                FileInfo fileInfo = new FileInfo(quickApplicant.RecipientPath);

                if (fileInfo.Exists)
                {
                    //we have a file...
                    recipients = File.ReadAllText(fileInfo.FullName).Split("\r\n").ToList();
                }
            }
            else
            {
                if (quickApplicant.Recipients.Any())
                {
                    recipients = quickApplicant.Recipients;
                }
            }            
            return recipients;
        }
    }
}
