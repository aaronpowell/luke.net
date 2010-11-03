using System.Windows.Controls;

namespace Luke.Net.Features.Documents
{
    /// <summary>
    /// Interaction logic for DocumentInfoView.xaml
    /// </summary>
    public partial class DocumentInfoView : UserControl
    {
        public DocumentInfoView(DocumentInfoViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
