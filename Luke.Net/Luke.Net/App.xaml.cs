using System.Windows;
using Microsoft.Practices.Prism.Events;

namespace Luke.Net
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region very scary code
        // ToDo: Just a quick hack. This should be fixed ASAP
        private static IEventAggregator _eventAggregator;

        static App()
        {
            _eventAggregator = new EventAggregator();
        }

        public static IEventAggregator EventAggregator
        {
            get
            {
                return _eventAggregator;
            }
        }
        #endregion
    }
}
