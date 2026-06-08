using System;
namespace bluepen.powershell.services.exceptions
{
    /// <summary>
    /// Represents an exception that describes invalid provided content
    /// </summary>
    public  class ContentProvidedException: Exception
    {
        /// <summary>
        /// Initializes a new instance of content provided exception
        /// </summary>
        public ContentProvidedException() { }

        /// <summary>
        /// Initializes a new instance of content provided exception
        /// </summary>
        /// <param name="message">describes error / exception that has occured</param>
        public ContentProvidedException(string message):base(message) { }

        /// <summary>
        /// Initializes a new instance of content provided exception
        /// </summary>
        /// <param name="message">describes error / exception that has occured</param>
        /// <param name="inner">inner stack trace information reported</param>
        public ContentProvidedException(string message, Exception inner): base(message, inner) { }
    }
}
