using bluepen.powershel.services;
using bluepen.powershel.services.factories;
using bluepen.powershell.domain.entities;
using bluepen.powershell.domain.services.abstracts;
using System.Management.Automation;

namespace bluepen.powershell.cmdlets
{
    [Cmdlet(VerbsCommunications.Send, "QuickApplicant")]
    [OutputType(typeof(string))]
    public class SendQuickApplicantCmdlet() : Cmdlet
    {
        
        [Alias("m", "ms")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, HelpMessage = "The name of the mail service to utilize. For example, Y - Yahoo or G - Gmail")]
        public string Service { get; set; }
        
        [Alias("u")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, HelpMessage = "The username of the account accesses mail service.")]
        public string Username { get; set; }
        
        [Alias("p")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, HelpMessage = "The password of the account accesses mail service.")]
        public string Password { get; set; }
        
        [Alias("r")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, HelpMessage = "The list of recipients separated by comma")]
        public string[] Recipients { get; set; } 
        
        [Alias("s")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, HelpMessage = "This is a subject of the email notification")]
        public string Subject { get; set; }
        
        [Alias("t")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, HelpMessage = "This is a topic within a text of the email notification" )]
        public string Topic { get; set; }
        
        [Alias("c")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, HelpMessage = "This is a content of the email notification")]
        public string Content { get; set; } 
        
        [Alias("a")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = false, HelpMessage = "This is optional attachment document to the email notification")]
        public string Attachment { get; set; }
        
        [Alias("sg")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, HelpMessage = "This is signature to be used for email notification" )]
        public string Signature { get; set; }

        [Parameter(HelpMessage = "This is a switch that provides opportunity of choice to decide if we want to process Recipients as an array of contacts or picked up file of contacts.")]
        public SwitchParameter File { get; set; }

        private NotificationServiceCreator? serviceCreator;

        //do initialization
        protected override void BeginProcessing()
        {
            //Setup
            base.BeginProcessing();
            WriteObject("Begin Processing...");
            try{

                var quickApplicant = new QuickApplicant() {
                     Username = Username,
                     Password = Password,
                     Recipients = Recipients,
                     Subject = Subject,
                     Topic = Topic,
                     Content = Content,
                     Attachment = Attachment,
                     Signature = Signature,
                     IsFile = File
                };

                switch (this.Service.ToUpper()) {
                    case "Y":
                        serviceCreator = new YahooServiceCreator(quickApplicant);
                        break;
                    case "G":
                        serviceCreator = new GmailServiceCreator(quickApplicant);
                        break;
                    default:
                        throw new Exception("Service Creator is not available...");
                }
            }
            catch (Exception ex) {
                WriteError(new ErrorRecord(ex, Guid.NewGuid().ToString(), ErrorCategory.InvalidOperation, null));
            }
        }

        //to process each item in the pipeline
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            WriteObject("Processing a Record...");

            try
            {
                using (var notificationService = serviceCreator?.GetNotificationService()) {
                    notificationService?.NotifyAsync().GetAwaiter().GetResult();
                }
                foreach (var item in MemoryLog.GetLogs()) {
                    WriteObject(item);
                }
                MemoryLog.ResetLogs();
            }
            catch (Exception ex){
                WriteError(new ErrorRecord(ex, Guid.NewGuid().ToString(), ErrorCategory.InvalidOperation, ""));
            }
        }

        //to do finalization
        protected override void EndProcessing()
        {
            base.EndProcessing();            
            WriteObject("End Processing...");
            try{
                if (serviceCreator != null) {
                    serviceCreator = null;
                }

            }catch (Exception ex){
                WriteError(new ErrorRecord(ex, Guid.NewGuid().ToString(), ErrorCategory.InvalidOperation, ""));
            }
        }

        //to handle abnormal termination
        protected override void StopProcessing()
        {
            base.StopProcessing();            
            WriteObject("Stop Processing...");
            try{

            }catch (Exception ex){
                WriteError(new ErrorRecord(ex, Guid.NewGuid().ToString(), ErrorCategory.InvalidOperation, ""));
            }
        }
    }
}
