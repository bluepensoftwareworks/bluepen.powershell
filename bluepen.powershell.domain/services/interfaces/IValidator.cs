using bluepen.powershell.domain.entities;

namespace bluepen.powershell.domain.services.interfaces
{
    /// <summary>
    /// IValidator
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Used to validate quick applicant object
        /// </summary>
        /// <param name="quickApplicant"></param>
        /// <returns></returns>
        public ValidationResult Validate(QuickApplicant quickApplicant);
    }
}
