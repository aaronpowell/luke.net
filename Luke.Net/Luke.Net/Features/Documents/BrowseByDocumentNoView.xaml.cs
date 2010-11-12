using System.Windows.Controls;

namespace Luke.Net.Features.Documents
{
    /// <summary>
    /// Interaction logic for BrowseByDocumentNoView.xaml
    /// </summary>
    public partial class BrowseByDocumentNoView : UserControl
    {
        public BrowseByDocumentNoView(BrowseByDocumentNoViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ((BrowseByDocumentNoViewModel)DataContext).BrowseDocument.Execute((int)DocumentNoSlider.Value);
        }
    }
}
