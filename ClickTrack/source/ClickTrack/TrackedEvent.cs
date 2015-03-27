using System;

namespace ClickTrack
{
    /// <summary>
    /// Contains all relevant data of a tracked event.
    /// </summary>
    public class TrackedEvent
    {
        /// <summary>
        /// Creates a new instance of the <see cref="TrackedEvent"/> class.
        /// </summary>
        /// <param name="id"></param>
        public TrackedEvent(String id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets the identifier of the <see cref="TrackedEvent"/>.
        /// </summary>
        public String Id { get; private set; } 
    }
}