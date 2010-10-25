 using System;
 using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Luke.Net.Features.LuceneIndex
{
    /// <summary>
    /// Interaction logic for LoadIndexView.xaml
    /// </summary>
    public partial class LoadIndexView : UserControl
    {
        public LoadIndexView()
        {
            InitializeComponent();
        }

        private void VisibleTermCount_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(Char.IsNumber);
            base.OnPreviewTextInput(e);
        }

        private void ShowTopTerms_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
