using System.Windows.Controls;

namespace Luke.Net.Features.Overview
{
    /// <summary>
    /// Interaction logic for LoadIndexView.xaml
    /// </summary>
    public partial class LoadIndexView : UserControl
    {
        public LoadIndexView(LoadIndexModel model)
        {
            DataContext = model;
            InitializeComponent();
        }
    }
}
