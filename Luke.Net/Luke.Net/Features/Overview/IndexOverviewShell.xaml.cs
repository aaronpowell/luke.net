using System.Windows.Controls;

namespace Luke.Net.Features.Overview
{
    /// <summary>
    /// Interaction logic for IndexOverviewShell.xaml
    /// </summary>
    public partial class IndexOverviewShell : UserControl
    {
        public IndexOverviewShell(LoadIndexModel model)
        {
            DataContext = model;
            InitializeComponent();
        }
    }
}
