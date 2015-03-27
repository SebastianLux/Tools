using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using ClickTrack.Wpf.Collections;

namespace ClickTrack.Wpf
{
    /// <summary>
    /// Tracks the invokation of an event with the <see cref="EventName"/>.
    /// </summary>
    /// <remarks>
    /// Must be added to a <see cref="TrackerCollection"/>.
    /// </remarks>
    public class Tracker : Freezable
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance of the <see cref="Tracker"/> class.
        /// </summary>
        public Tracker()
        {
            BindingOperations.SetBinding(this, TrackedObjectProperty, new Binding("TrackedObject")
                                                                      {
                                                                          RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof (TrackerCollection), 1),
                                                                          Mode = BindingMode.OneWay,
                                                                      });
        }

        #endregion

        #region Overrides of Base

        /// <summary>
        /// When implemented in a derived class, creates a new instance of the <see cref="T:System.Windows.Freezable"/> derived class. 
        /// </summary>
        /// <returns>
        /// The new instance.
        /// </returns>
        protected override Freezable CreateInstanceCore()
        {
            return new Tracker();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the event that is tracked.
        /// </summary>
        public String EventName
        {
            get { return (String) GetValue(EventNameProperty); }
            set { SetValue(EventNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets the unique identifier of the <see cref="Tracker"/>.
        /// </summary>
        public String Id
        {
            get { return (String) GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Gets or sets the <see cref="FrameworkElement"/> that is tracked.
        /// </summary>
        private FrameworkElement TrackedObject
        {
            get { return (FrameworkElement) GetValue(TrackedObjectProperty); }
            set { SetValue(TrackedObjectProperty, value); }
        }

        #endregion

        #region Private Static Methods

        private static void TrackedObjectPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            // Here is the entry point for attaching.

            var tracker = dependencyObject as Tracker;

            if (tracker != null)
            {
                if (tracker.Id == null)
                {
                    throw new NullReferenceException(String.Format("'{0}' is null", "Tracker.Id"));
                }

                if (tracker.EventName == null)
                {
                    throw new NullReferenceException(String.Format("'{0}' is null", "Tracker.EventName"));
                }

                var eventInfo = tracker.GetEventInfo(tracker.EventName);
                var eventHandler = tracker.CreateEventHandler(eventInfo);

                if (tracker.TrackedObject.IsLoaded)
                {
                    tracker.AddEventHandler(eventInfo, eventHandler);
                }

                tracker.TrackedObject.Loaded += (sender, args) => tracker.AddEventHandler(eventInfo, eventHandler);
                tracker.TrackedObject.Unloaded += (sender, args) => tracker.RemoveEventHandler(eventInfo, eventHandler);
            }
        }

        #endregion

        #region Private Methods

        private void AddEventHandler(EventInfo eventInfo, Delegate eventHandler)
        {
            eventInfo.AddEventHandler(TrackedObject, eventHandler);
        }

        private Delegate CreateEventHandler(EventInfo eventInfo)
        {
            if (eventInfo == null)
            {
                throw new ArgumentNullException("eventInfo");
            }

            if (eventInfo.EventHandlerType == null)
            {
                throw new ArgumentException("eventInfo.EventHandlerType is null");
            }

            return Delegate.CreateDelegate(eventInfo.EventHandlerType,
                this,
                GetType().GetMethod("OnEventRaised", BindingFlags.NonPublic | BindingFlags.Instance));
        }

        private EventInfo GetEventInfo(string eventName)
        {
            if (String.IsNullOrEmpty(eventName))
            {
                throw new ArgumentNullException("eventName");
            }

            if (TrackedObject == null)
            {
                throw new NullReferenceException("TrackedObject is null");
            }

            var eventInfo = TrackedObject.GetType().GetEvent(eventName, BindingFlags.Public | BindingFlags.Instance);

            if (eventInfo == null)
            {
                throw new NullReferenceException(String.Format("Event with the name '{0}' was not found.", eventName));
            }

            return eventInfo;
        }

        private async void OnEventRaised(object sender, EventArgs args)
        {
            var trackedClick = new TrackedEvent(Id);
            {
                foreach (var trackedHandler in TrackedEventHandlers)
                {
                    await trackedHandler.HandleTrackedEventAsync(trackedClick);
                }
            }
        }

        private void RemoveEventHandler(EventInfo eventInfo, Delegate eventHandler)
        {
            eventInfo.RemoveEventHandler(TrackedObject, eventHandler);
        }

        #endregion

        /// <summary>
        /// The <see cref="DependencyProperty"/> of the <see cref="Id"/> property.
        /// </summary>
        public static readonly DependencyProperty IdProperty = DependencyProperty.Register(
            "Id", typeof (String), typeof (Tracker), new PropertyMetadata(default(String)));

        /// <summary>
        /// The <see cref="DependencyProperty"/> of the <see cref="EventName"/> property.
        /// </summary>
        public static readonly DependencyProperty EventNameProperty = DependencyProperty.Register(
            "EventName", typeof (String), typeof (Tracker), new PropertyMetadata(default(String)));

        /// <summary>
        /// The <see cref="DependencyProperty"/> of the <see cref="TrackedObject"/> property.
        /// </summary>
        private static readonly DependencyProperty TrackedObjectProperty =
            DependencyProperty.Register("TrackedObject",
                typeof (FrameworkElement), typeof (Tracker),
                new PropertyMetadata(default(FrameworkElement), TrackedObjectPropertyChangedCallback));

        internal static readonly ConcurrentBag<ITrackedEventHandler> TrackedEventHandlers = new ConcurrentBag<ITrackedEventHandler>();
    }
}