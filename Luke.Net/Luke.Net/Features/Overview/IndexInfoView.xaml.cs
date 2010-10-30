using System.Windows.Controls;

namespace Luke.Net.Features.Overview
{
    /// <summary>
    /// Interaction logic for IndexInfoView.xaml
    /// </summary>
    public partial class IndexInfoView : UserControl
    {
        public IndexInfoView(IndexInfoViewModel model)
        {
            DataContext = model;
            InitializeComponent();
        }
    }
}
