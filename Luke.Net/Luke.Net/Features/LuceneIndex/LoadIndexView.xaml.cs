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

        public LoadIndexModel IndexModel
        {
            get
            {
                return (LoadIndexModel)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }
    }
}
