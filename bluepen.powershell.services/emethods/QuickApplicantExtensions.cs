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
                if (!string.IsNullOrEmpty(quickApplicant.Content) && quickApplicant.Content.IndexOfAny(new char[] { '\\', '/', ':' }) != -1)
                {
                    //we have a file...
                    fileContents = File.ReadAllText(quickApplicant.Content);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(quickApplicant.Content) && quickApplicant.Content.IndexOfAny(new char[] { '\\', '/', ':' }) == -1)
                {
                    fileContents = quickApplicant.Content;
                }
            }

            if (string.IsNullOrEmpty(fileContents))
            {
                throw new ContentProvidedException("Content provided is something else...There is an issue...");
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

            IList<string> recipients = null;

            //sort out recipients...
            if (quickApplicant.IsFile)
            {
                if (quickApplicant.Recipients.Any() && quickApplicant.Recipients[0].IndexOfAny(new char[] { '\\', '/', ':' }) != -1)
                {
                    //we have a file...
                    recipients = File.ReadAllText(quickApplicant.Recipients[0]).Split("\r\n").ToList();
                }
            }
            else
            {
                if (quickApplicant.Recipients.Any())
                {
                    recipients = quickApplicant.Recipients;
                }
            }
            
            if (recipients == null)
            {
                throw new ContentProvidedException("Content provided is something else...There is an issue...");
            }

            //sort out recipients...
            return recipients;
        }
    }
}
