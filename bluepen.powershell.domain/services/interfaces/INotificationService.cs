using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bluepen.powershell.domain.services.interfaces
{
    public interface INotificationService: IDisposable
    {
        public Task NotifyAsync();
    }
}
