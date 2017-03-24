using System;
using Xamarin.Forms;

namespace InfoFrame.Controls
{
    public partial class RatingControl : ContentView
    {
        /// <summary>
        /// Initializes a new instance of the RatingControl.
        /// </summary>
        public RatingControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the current value of the control.
        /// </summary>
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Identifies the Value bindable property.
        /// </summary>
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create("Value", typeof(int), typeof(RatingControl), default(int),
                BindingMode.Default, null, ValueChanged);

        /// <summary>
        /// Value bindable property changed handler.
        /// </summary>
        /// <param name="bindable">The bindable object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void ValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((RatingControl)bindable).ValueChanged((int)newValue);
        }

        /// <summary>
        /// Set the visibility of items.
        /// </summary>
        /// <param name="newValue">The new value.</param>
        private void ValueChanged(int newValue)
        {
            for (int i = 1; i <= LayoutRoot.Children.Count; i++)
            {
                if (i % 2 == 0)
                {
                    bool isVisible = (i / 2) <= newValue;
                    LayoutRoot.Children[i - 1].IsVisible = isVisible;
                    LayoutRoot.Children[i - 2].IsVisible = !isVisible;
                }
                else
                {
                    LayoutRoot.Children[i - 1].IsVisible = true;
                }
            }
        }

        /// <summary>
        /// Called when the item tapped event occurs.
        /// Calculates the new Value of this control.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event args.</param>
        private void ItemTapped(object sender, EventArgs e)
        {
            int tappedItemIndex = LayoutRoot.Children.IndexOf((View)sender);
            decimal value = Math.Round(((decimal)tappedItemIndex + 1) / 2, MidpointRounding.AwayFromZero);
            Value = Convert.ToInt32(value);
        }
    }
}
