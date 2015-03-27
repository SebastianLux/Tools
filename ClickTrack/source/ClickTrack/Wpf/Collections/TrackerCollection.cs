using System.Windows;

namespace ClickTrack.Wpf.Collections
{
    /// <summary>
    /// Represents a collection of <see cref="Tracker"/>.
    /// </summary>
    public class TrackerCollection : FreezableCollection<Tracker>
    {
        /// <summary>
        /// The <see cref="DependencyProperty"/> of the <see cref="TrackedObject"/> property.
        /// </summary>
        public static readonly DependencyProperty TrackedObjectProperty =
            DependencyProperty.Register("TrackedObject", typeof(FrameworkElement), typeof(TrackerCollection), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the <see cref="DependencyObject"/> that is tracked.
        /// </summary>
        public FrameworkElement TrackedObject
        {
            get { return (FrameworkElement)GetValue(TrackedObjectProperty); }
            set { SetValue(TrackedObjectProperty, value); }
        }
    }
}
