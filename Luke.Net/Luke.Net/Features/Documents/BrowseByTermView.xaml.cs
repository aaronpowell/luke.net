using System.Windows.Controls;

namespace Luke.Net.Features.Documents
{
    /// <summary>
    /// Interaction logic for BrowseByTermView.xaml
    /// </summary>
    public partial class BrowseByTermView : UserControl
    {
        public BrowseByTermView(BrowseByTermViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
