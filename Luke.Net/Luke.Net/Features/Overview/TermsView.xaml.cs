using System.Windows.Controls;

namespace Luke.Net.Features.Overview
{
    /// <summary>
    /// Interaction logic for TermsView.xaml
    /// </summary>
    public partial class TermsView : UserControl
    {
        public TermsView()
        {
            InitializeComponent();
        }

        private void TermsViewLoadingRow(object sender, DataGridRowEventArgs e)
        {
            var rowData = (TermInfo)e.Row.DataContext;
            // Row rank should be set on the fly as grid could show any subset of terminfo
            rowData.Rank = e.Row.GetIndex() + 1;
        }
    }
}
