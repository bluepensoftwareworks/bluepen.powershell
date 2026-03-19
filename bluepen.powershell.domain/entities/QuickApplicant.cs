namespace bluepen.powershell.domain.entities
{
    public class QuickApplicant
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string[] Recipients { get; set; }
        public string Subject { get; set; }
        public string Topic { get; set; }
        public string Content { get; set; }
        public string Attachment { get; set; }
        public string Signature { get; set; }
        public bool IsFile { get; set; }
    }
}
