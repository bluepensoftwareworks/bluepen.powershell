using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace bluepen.powershell.services.exceptions
{
    public  class ContentProvidedException: Exception
    {
        public ContentProvidedException() { }

        public ContentProvidedException(string message):base(message) { }

        public ContentProvidedException(string message, Exception inner): base(message, inner) { }
    }
}
