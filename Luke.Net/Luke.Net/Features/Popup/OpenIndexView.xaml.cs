using System.Windows;
using System.Windows.Controls;

namespace Luke.Net.Features.Popup
{
    /// <summary>
    /// Interaction logic for OpenIndexView.xaml
    /// </summary>
    public partial class OpenIndexView : Window
    {
        public OpenIndexView()
        {
            InitializeComponent();
        }

        public ActiveIndexModel IndexToOpen
        {
            get
            {
                return (ActiveIndexModel) DataContext;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
