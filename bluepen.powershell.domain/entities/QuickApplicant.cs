namespace bluepen.powershell.domain.entities
{
    /// <summary>
    /// Represents an applicant account with basic configuration and intentions settings
    /// </summary>
    /// <remarks>
    /// This class is a simple example for tracking general configuration settings such as username, application password, recipients list, subject,
    /// topic, notification content, optional attachment file, dynamic signature and [File] switch to know where to read information from either command prompt or from a file.
    /// </remarks>
    public class QuickApplicant
    {
        /// <summary>
        /// Gets, Sets the unique username.
        /// </summary>
        /// <value>A string representing the username</value>
        public string Username { get; set; }
        /// <summary>
        /// Gets, Sets the application password (created either in Yahoo Mail Service, Google Mail Service or something else)
        /// </summary>
        /// <value>A string representing the application password</value>
        public string Password { get; set; }
        /// <summary>
        /// Gets, Sets comma separated list of recipients. the list is read from command prompt as a comma separated list or input file.(!)
        /// </summary>
        /// <value>A string array representing a list of recipients</value>
        public string[] Recipients { get; set; }
        /// <summary>
        /// Gets, Sets title of the notification
        /// </summary>
        /// <value>A string representing the title of the notification to be sent and received</value>
        public string Subject { get; set; }
        /// <summary>
        /// Gets, Sets subject matter in question like work position / job title
        /// </summary>
        /// <value>A string representing a topic, subject matter in focus</value>
        public string Topic { get; set; }
        /// <summary>
        /// Gets, Sets a content of the notification message / announcement. The content is built / created by command line prompting or in-line filepath reference to file that contains notification
        /// context that is to be embodied into notification message.(!)
        /// </summary>
        /// <value>A string representing context of notification message either as text or filepath to a file with text</value>
        public string Content { get; set; }
        /// <summary>
        /// Gets, Sets optional attachment that can be included with sent notification. The attachment is a file that is required to be referenced when IsFile parameter is TRUE or IsFile command prompt parameter is present.
        /// </summary>
        /// <value>A string representing filepath to a file to be attached to notification message</value>
        public string Attachment { get; set; }
        /// <summary>
        /// Gets, Sets a signature context for the notification message
        /// </summary>
        /// <value>A string representing signature with which notification message is to be signed by</value>
        public string Signature { get; set; }
        /// <summary>
        /// Gets, Sets a switch parameter to instruct the commandlet in binary module how to read in-line input from different above mentioned parameters
        /// </summary>
        public bool IsFile { get; set; }
    }
}
