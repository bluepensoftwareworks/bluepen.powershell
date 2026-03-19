using bluepen.powershell.domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bluepen.powershell.services.emethods
{
    public static class HTMLBodyExtensions
    {
        public static string GetHTMLBody(this string content, string topic, string signature)
        {            
            return content.Replace("{topic}", topic).Replace("{signature}", signature).Replace("\r\n", "<BR />").Replace("\n", "<BR />").Replace("\r", "<BR />");
        }
    }
}
