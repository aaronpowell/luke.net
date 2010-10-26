using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Luke.Net.Infrastructure.ValueConverters;

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

        private void fieldsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedFields = fieldsView.SelectedItems.Cast<FieldInfo>();
            App.EventAggregator.GetEvent<SelectedFieldChangedEvent>().Publish(selectedFields);
        }
    }
}
