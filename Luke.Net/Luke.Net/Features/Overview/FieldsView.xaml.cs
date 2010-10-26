using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Luke.Net.Features.Overview
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

        private void InspectTerms_Click(object sender, RoutedEventArgs e)
        {
            var selectedTerms = fieldsView.SelectedItems.Cast<FieldInfo>();
            IndexModel.InspectFields.Execute(selectedTerms);
        }

        private void fieldsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedFields = fieldsView.SelectedItems.Cast<FieldInfo>();
            App.EventAggregator.GetEvent<SelectedFieldChangedEvent>().Publish(selectedFields);
        }
    }
}
