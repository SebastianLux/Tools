using System;
using System.Windows;
using ClickTrack.Examples.TrackedEventHandler;

namespace ClickTrack.Examples
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs"/> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Configuration.TrackingConfiguration.RegisterTrackedEventHandler(new DebugTrackedEventHandler());
        }
    }
}
