using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Luke.Net.Features.LuceneIndex
{
    /// <summary>
    /// Interaction logic for FieldsView.xaml
    /// </summary>
    public partial class FieldsView : UserControl
    {
        public FieldsView()
        {
            InitializeComponent();
        }

        private void InspectTerms_Click(object sender, RoutedEventArgs e)
        {
            var selectedTerms = fieldsView.SelectedItems.Cast<FieldInfo>();

            IndexModel.InspectFields.Execute(selectedTerms);
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
