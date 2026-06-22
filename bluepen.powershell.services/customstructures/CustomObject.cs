using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bluepen.powershell.services.customstructures
{
    /// <summary>
    /// CustomObject
    /// </summary>
    public class CustomObject
    {
        /// <summary>
        /// Provider
        /// </summary>
        public string   Provider { get; set; }
        /// <summary>
        /// Recipients
        /// </summary>
        public string   Recipients { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public string   Status { get; set; }
        /// <summary>
        /// TimeStamp
        /// </summary>
        public DateTime TimeStamp { get; set; }
    }
}
