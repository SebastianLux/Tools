using System.Diagnostics;
using System.Threading.Tasks;

namespace ClickTrack.Examples.TrackedEventHandler
{
    public class DebugTrackedEventHandler : ITrackedEventHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trackedEvent"></param>
        public Task HandleTrackedEventAsync(TrackedEvent trackedEvent)
        {
            return Task.Run(() => Debug.WriteLine(trackedEvent.Id));
        }
    }
}