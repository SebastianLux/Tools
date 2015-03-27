using System.Threading.Tasks;

namespace ClickTrack
{
    /// <summary>
    /// Defines the handling of a <see cref="TrackedEvent"/>.
    /// </summary>
    public interface ITrackedEventHandler
    {
        /// <summary>
        /// Handles a <see cref="TrackedEvent"/> async.
        /// </summary>
        /// <param name="trackedEvent">The <see cref="TrackedEvent"/>.</param>
        Task HandleTrackedEventAsync(TrackedEvent trackedEvent);
    }
}