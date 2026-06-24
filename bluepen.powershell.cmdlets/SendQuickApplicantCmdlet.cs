using bluepen.powershell.domain.entities;
using bluepen.powershell.domain.services;
using bluepen.powershell.domain.services.abstracts;
using bluepen.powershell.services;
using bluepen.powershell.services.customstructures;
using bluepen.powershell.services.factories;
using System.Management.Automation;

namespace bluepen.powershell.cmdlets
{
    /// <summary>
    /// Represents SendQuickApplicant CommandLet..
    /// Prompt: Provide step by step instructions how to package and distribute Binary Powershell Module that has dependency on two other class library assemblies and System.Management.Automation, MailKit and MimeKit packages in Visual Studio 2022 for Powershell 7
    /// </summary>
    [Cmdlet(VerbsCommunications.Send, "QuickApplicant", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Low, DefaultParameterSetName = "SwitchIsOff")]
    [OutputType(typeof(CustomObject))]
    public class SendQuickApplicantCmdlet() : Cmdlet
    {
        /*
        [Alias("u")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, HelpMessage = "The username of the account accesses mail service.")]
        public required string Username { get; set; }
        
        [Alias("p")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, HelpMessage = "The password of the account accesses mail service.")]
        public required string Password { get; set; }
        */

        [Alias("cr")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, HelpMessage = "Credentials (username and app password) are required parameters for this Cmdlet to execute")]
        public PSCredential Credential { get; set; }

        [Alias("m", "ms")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, HelpMessage = "The name of the mail service to utilize. For example, Y - Yahoo or G - Gmail")]
        public required string Service { get; set; }

        [Alias("r")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ParameterSetName = "SwitchIsOff", HelpMessage = "The list of recipients separated by comma")]
        public required string[] Recipients { get; set; }

        [Alias("rp")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ParameterSetName = "SwitchIsOn", HelpMessage = "The list of full file path to recipients list")]
        public required string RecipientPath { get; set; }

        [Alias("s")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, HelpMessage = "This is a subject of the email notification")]
        public required string Subject { get; set; }
        
        [Alias("t")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, HelpMessage = "This is a topic within a text of the email notification" )]
        public required string Topic { get; set; }
        
        [Alias("c")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ParameterSetName = "SwitchIsOff", HelpMessage = "This is a content of the email notification")]
        public required string Content { get; set; }

        [Alias("cp")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ParameterSetName = "SwitchIsOn", HelpMessage = "This is a full file path to content used in email notification")]
        public required string ContentPath { get; set; }


        [Alias("a")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = false, HelpMessage = "This is optional attachment document to the email notification")]
        public string AttachmentPath { get; set; }
        
        [Alias("sg")]
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, HelpMessage = "This is signature to be used for email notification" )]
        public required string Signature { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "SwitchIsOn", HelpMessage = "This is a switch that provides opportunity of choice to decide if we want to process Recipients as an array of contacts or picked up file of contacts.")]
        [Parameter(Mandatory = false, ParameterSetName = "SwitchIsOff", HelpMessage = "This is a switch that provides opportunity of choice to decide if we want to process Recipients as an array of contacts or picked up file of contacts.")]
        public SwitchParameter File { get; set; }

        private NotificationServiceCreator? serviceCreator;

        // 1. Create a CancellationTokenSource
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly MemoryLog memoryLog = new MemoryLog();

        /// <summary>
        /// Begins Processing of the commandLet, do initialization
        /// </summary>        
        protected override void BeginProcessing()
        {
            //Setup
            base.BeginProcessing();
            WriteVerbose("Begin Processing...");
            try{

                switch (this.Service.ToUpper()) {
                    case "Y":
                        serviceCreator = new YahooServiceCreator(memoryLog);
                        break;
                    case "G":
                        serviceCreator = new GmailServiceCreator(memoryLog);
                        break;
                    default:
                        throw new Exception("Service Creator is not available...");
                }
            }
            catch (Exception ex) {
                WriteError(new ErrorRecord(ex, Guid.NewGuid().ToString(), ErrorCategory.InvalidOperation, null));
            }
        }

        // 3. Monitor the token in your main processing record method
        /// <summary>
        /// Processes each record in the pipeline
        /// </summary>        
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            WriteVerbose("Processing a Record...");
            CancellationToken token = cancellationTokenSource.Token;

            if (ShouldProcess(Content, "Send Notifications") || ShouldProcess(ContentPath, "Send Notifications"))
            {
                try
                {

                    using (var notificationService = serviceCreator?.GetNotificationService())
                    {
                        // Code inside here ONLY runs if the user explicitly confirms or did not use -WhatIf
                        notificationService?.NotifyAsync(
                            new QuickApplicant()
                            {
                                Username = Credential.UserName,
                                Password = new System.Net.NetworkCredential(string.Empty, Credential.Password).Password,
                                Subject = Subject,
                                Topic = Topic,
                                Content = Content,
                                ContentPath = ContentPath,
                                Recipients = Recipients,
                                RecipientPath = RecipientPath,
                                AttachmentPath = AttachmentPath,
                                Signature = Signature,
                                IsFile = File
                            }, token).GetAwaiter().GetResult();
                    }

                    foreach (var item in memoryLog.GetLogs())
                    {
                        WriteVerbose(item);
                    }
                    memoryLog.ResetLogs();


                    WriteObject(new CustomObject()
                    {
                        Provider = this.Service.ToUpper() == "G" ? "Gmail" : "Yahoo",
                        Recipients = File.IsPresent ? RecipientPath : string.Join(", ", Recipients),
                        Status = "Sent",
                        TimeStamp = DateTime.Now,
                    });
                }
                catch (Exception ex)
                {
                    WriteError(new ErrorRecord(ex, Guid.NewGuid().ToString(), ErrorCategory.InvalidOperation, ""));
                }
                finally {
                    cancellationTokenSource.Dispose();
                }
            }
        }

        
        /// <summary>
        /// Do finalization
        /// </summary>
        protected override void EndProcessing()
        {
            base.EndProcessing();
            WriteVerbose("End Processing...");
            try{
                if (serviceCreator != null) {
                    serviceCreator = null;
                }

            }catch (Exception ex){
                WriteError(new ErrorRecord(ex, Guid.NewGuid().ToString(), ErrorCategory.InvalidOperation, ""));
            }
        }

        /// <summary>
        /// Handle abnormal termination
        /// </summary>
        // 2. Override StopProcessing to signal cancellation
        protected override void StopProcessing()
        {
            base.StopProcessing();
            try
            {
                WriteVerbose("Stop Processing...");
                // This is called by PowerShell from a separate thread when Ctrl+C is pressed
                cancellationTokenSource.Cancel();

            }
            catch (Exception ex){
                WriteError(new ErrorRecord(ex, Guid.NewGuid().ToString(), ErrorCategory.InvalidOperation, ""));
            }
        }
    }
}
