using System.Windows;
using Luke.Net.Features.LuceneIndex;
using Luke.Net.Features.Popup;

namespace Luke.Net
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var popup = new OpenIndexView();
            if(popup.ShowDialog() ?? false)
            {
                var path = popup.IndexToOpen;
                indexView.DataContext = LoadIndexModel.LoadIndex(path);
            }
        }
    }
}
