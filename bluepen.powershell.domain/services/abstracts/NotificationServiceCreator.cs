using bluepen.powershell.domain.entities;
using bluepen.powershell.domain.services.interfaces;


namespace bluepen.powershell.domain.services.abstracts
{
    public abstract class NotificationServiceCreator
    {
        protected QuickApplicant applicant;

        public NotificationServiceCreator(QuickApplicant applicant)
        {
            this.applicant = applicant;
        }

        public abstract INotificationService GetNotificationService();
    }
}
