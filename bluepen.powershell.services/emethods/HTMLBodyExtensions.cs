namespace bluepen.powershell.services.emethods
{
    /// <summary>
    /// Represents extension class with method to format content read from input file or command prompt parameter into HTML representation
    /// </summary>
    public static class HTMLBodyExtensions
    {
        /// <summary>
        /// Gets content formatted in HTML
        /// </summary>
        /// <param name="content">body of the message context</param>
        /// <param name="topic">subject in focus</param>
        /// <param name="signature">named token defined in footer of notification context</param>
        /// <returns>formatted content</returns>
        public static string GetHTMLBody(this string content, string topic, string signature)
        {            
            return content.Replace("{topic}", topic).Replace("{signature}", signature).Replace("\r\n", "<BR />").Replace("\n", "<BR />").Replace("\r", "<BR />");
        }
    }
}
