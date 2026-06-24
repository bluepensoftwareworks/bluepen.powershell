using bluepen.powershell.domain.entities;

namespace bluepen.powershell.domain.services.interfaces
{
    /// <summary>
    /// Represents a notification service interface that declares what needs to be implemented by concrete different notification service classes
    /// </summary>
    /// <remarks>
    /// This interface is a simple example for defining what different notification service classes need to implement
    /// </remarks>
    public interface INotificationService : IDisposable
    {
        /// <summary>
        /// Notifies recipients individually with subject, topic, content, (optional attachment), signature
        /// </summary>        /// 
        /// <returns>A <see cref="Task"/> that represents the asynchronous notify operation.</returns>
        public Task NotifyAsync(QuickApplicant quickApplicant);
    }
}
