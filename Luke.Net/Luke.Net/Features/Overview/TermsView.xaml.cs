using System.Windows.Controls;
using Luke.Net.Models;

namespace Luke.Net.Features.Overview
{
    /// <summary>
    /// Interaction logic for TermsView.xaml
    /// </summary>
    public partial class TermsView : UserControl
    {
        public TermsView(TermsViewModel model)
        {
            DataContext = model;
            InitializeComponent();
        }

        private void TermsViewLoadingRow(object sender, DataGridRowEventArgs e)
        {
            var rowData = (TermInfo)e.Row.DataContext;
            // Row rank should be set on the fly as grid could show any subset of terminfo
            rowData.Rank = e.Row.GetIndex() + 1;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TopTermSlider.GetBindingExpression(Slider.ValueProperty).UpdateSource();
        }

        private void termsView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(InspectDocumentsButton.IsEnabled)
            {
                InspectDocumentsButton.Command.Execute(null);
            }
        }
    }
}
