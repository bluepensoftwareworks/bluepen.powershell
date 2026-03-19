using bluepen.powershell.services.exceptions;
using bluepen.powershell.domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bluepen.powershell.services.emethods
{
    public static class QuickApplicantExtensions
    {
        public static string GetContent(this QuickApplicant quickApplicant) {

            string fileContents = string.Empty;

            //sort out content...
            if (quickApplicant.IsFile)
            {
                if (quickApplicant.Content.IndexOfAny(new char[] { '\\', '/', ':' }) != -1)
                {
                    //we have a file...
                    fileContents = File.ReadAllText(quickApplicant.Content);
                }
            }
            else
            {
                if (quickApplicant.Content.IndexOfAny(new char[] { '\\', '/', ':' }) == -1)
                {
                    fileContents = quickApplicant.Content;
                }
            }
            //sort out content...
            return fileContents;

        }

        public static IList<string> GetRecipients(this QuickApplicant quickApplicant) {

            IList<string> recipients = null;

            //sort out recipients...
            if (quickApplicant.IsFile)
            {
                if (quickApplicant.Recipients[0].IndexOfAny(new char[] { '\\', '/', ':' }) != -1)
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
            //sort out recipients...
            return recipients;
        }
    }
}
