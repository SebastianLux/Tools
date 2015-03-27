using ClickTrack.Wpf;

namespace ClickTrack.Configuration
{
    /// <summary>
    /// Provides basic configuration functionality.
    /// </summary>
    public static class TrackingConfiguration
    {
        /// <summary>
        /// Registers the <paramref name="trackedEventHandler"/>.
        /// </summary>
        /// <param name="trackedEventHandler">An implementation of the <see cref="ITrackedEventHandler"/>.</param>
        public static void RegisterTrackedEventHandler(ITrackedEventHandler trackedEventHandler)
        {
            Tracker.TrackedEventHandlers.Add(trackedEventHandler);
        }
    }
}