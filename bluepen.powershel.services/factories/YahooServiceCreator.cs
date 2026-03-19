using bluepen.powershell.domain.entities;
using bluepen.powershell.domain.services.abstracts;
using bluepen.powershell.domain.services.interfaces;

namespace bluepen.powershel.services.factories
{
    public class YahooServiceCreator : NotificationServiceCreator
    {
        public YahooServiceCreator(QuickApplicant quickApplicant) : base(quickApplicant) { }

        public override INotificationService GetNotificationService()
        {
            return new YahooNotificationService(applicant);
        }
    }
}
