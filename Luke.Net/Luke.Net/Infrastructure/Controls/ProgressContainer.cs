using System.Windows;
using System.Windows.Controls;

namespace Luke.Net.Infrastructure.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Luke.Net.Infrastructure.Controls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Luke.Net.Infrastructure.Controls;assembly=Luke.Net.Infrastructure.Controls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:ProgressContainer/>
    ///
    /// </summary>
    public class ProgressContainer : ContentControl
    {
        static ProgressContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressContainer), new FrameworkPropertyMetadata(typeof(ProgressContainer)));
        }

        public double BusyFrameOpacity
        {
            get { return (double)GetValue(BusyFrameOpacityProperty); }
            set { SetValue(BusyFrameOpacityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BusyFrameOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BusyFrameOpacityProperty =
            DependencyProperty.Register("BusyFrameOpacity", typeof(double), typeof(ProgressContainer), new UIPropertyMetadata(.8));

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsBusy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(ProgressContainer), new UIPropertyMetadata(true));

        public string ProgressText
        {
            get { return (string)GetValue(ProgressTextProperty); }
            set { SetValue(ProgressTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProgressText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressTextProperty =
            DependencyProperty.Register("ProgressText", typeof(string), typeof(ProgressContainer), new UIPropertyMetadata(string.Empty));


        public bool IsIndeterminate
        {
            get { return (bool)GetValue(IsIndeterminateProperty); }
            set { SetValue(IsIndeterminateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsIndeterminate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsIndeterminateProperty =
            DependencyProperty.Register("IsIndeterminate", typeof(bool), typeof(ProgressContainer), new UIPropertyMetadata(true));

        public int ProgressValue
        {
            get { return (int)GetValue(ProgressValueProperty); }
            set { SetValue(ProgressValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProgressValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressValueProperty =
            DependencyProperty.Register("ProgressValue", typeof(int), typeof(ProgressContainer), new UIPropertyMetadata(0));
    }
}
