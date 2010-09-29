using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Luke.Net.Models;

namespace Luke.Net.Views.LuceneIndex
{
    /// <summary>
    /// Interaction logic for LoadIndexPage.xaml
    /// </summary>
    public partial class LoadIndexPage : Page
    {
        public LoadIndexPage()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lv = (ListView)sender;
            TermMenu.IsEnabled = lv.SelectedItems.Count > 0;
        }

        private void InspectTerms_Click(object sender, RoutedEventArgs e)
        {
            var selectedTerms = fieldsListView.SelectedItems.Cast<FieldInfo>();

            ((LoadIndexModel)DataContext).InspectFields.Execute(selectedTerms);
        }
    }
}
