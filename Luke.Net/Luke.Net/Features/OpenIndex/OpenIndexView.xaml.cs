using System.Windows;
using System.Windows.Controls;

namespace Luke.Net.Features.OpenIndex
{
    /// <summary>
    /// Interaction logic for OpenIndexView.xaml
    /// </summary>
    public partial class OpenIndexView : UserControl
    {
        public OpenIndexView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loadIndexExpander.IsExpanded = false;
        }
    }
}
