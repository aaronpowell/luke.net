using System;
using System.Windows;
using Magellan;
using Magellan.Events;
using Magellan.Progress;

namespace Luke.Net
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INavigationProgressListener
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public INavigator Navigator { get; set; }

        #region INavigationProgressListener Members

        public void UpdateProgress(NavigationEvent navigationEvent)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
