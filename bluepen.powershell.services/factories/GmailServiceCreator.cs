using bluepen.powershell.domain.entities;
using bluepen.powershell.domain.services.abstracts;
using bluepen.powershell.domain.services.interfaces;

namespace bluepen.powershell.services.factories
{
    public class GmailServiceCreator : NotificationServiceCreator
    {
        public GmailServiceCreator(QuickApplicant quickApplicant) : base(quickApplicant) { }

        public override INotificationService GetNotificationService()
        {
            return new GmailNotificationService(applicant);
        }
    }
}
