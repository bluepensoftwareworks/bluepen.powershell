namespace bluepen.powershell.domain.services
{
    public class ValidationResult
    {
        public bool IsValid => !Errors.Any();
        public List<string> Errors { get; } = new();
    }
}
