using System.Windows;
using ClickTrack.Wpf.Collections;

namespace ClickTrack.Wpf
{
    /// <summary>
    /// Is responsible for attaching a <see cref="TrackerCollection"/> to a <see cref="FrameworkElement"/>.
    /// </summary>
    public class Tracking
    {
        #region Public Static Methods

        /// <summary>
        /// Gets the <see cref="TrackerCollection"/>.
        /// </summary>
        /// <param name="dependencyObject">The <see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="TrackerCollection"/>.</returns>
        public static TrackerCollection GetTrackerCollection(DependencyObject dependencyObject)
        {
            var trackerCollection = dependencyObject.GetValue(TrackerCollectionProperty) as TrackerCollection;
            if (trackerCollection == null)
            {
                trackerCollection = new TrackerCollection();
                SetTrackerCollection(dependencyObject, trackerCollection);
            }

            return trackerCollection;
        }

        /// <summary>
        /// Sets the <see cref="TrackerCollection"/>.
        /// </summary>
        /// <param name="dependencyObject">The <see cref="DependencyObject"/>.</param>
        /// <param name="value">The <see cref="TrackerCollection"/>.</param>
        public static void SetTrackerCollection(DependencyObject dependencyObject, TrackerCollection value)
        {
            dependencyObject.SetValue(TrackerCollectionProperty, value);
        }

        #endregion

        #region Private Static Methods

        private static void TrackerCollectionPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (dependencyObject != null)
            {
                var trackerCollection = dependencyObject.GetValue(TrackerCollectionProperty) as TrackerCollection;

                if (trackerCollection != null)
                {
                    trackerCollection.SetValue(TrackerCollection.TrackedObjectProperty, dependencyObject);
                }
            }
        }

        #endregion

        /// <summary>
        /// The <see cref="DependencyProperty"/> of the <see cref="TrackerCollection"/> property.
        /// </summary>
        public static readonly DependencyProperty TrackerCollectionProperty =
            DependencyProperty.RegisterAttached("TrackerCollectionInternal",
                typeof(TrackerCollection),
                typeof(Tracking),
                new UIPropertyMetadata(null, TrackerCollectionPropertyChangedCallback));
    }
}