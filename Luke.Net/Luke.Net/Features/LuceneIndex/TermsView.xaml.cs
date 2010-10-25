using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Luke.Net.Features.LuceneIndex
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
