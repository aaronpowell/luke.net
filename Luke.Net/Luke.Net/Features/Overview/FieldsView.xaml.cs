using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Events;

namespace Luke.Net.Features.Overview
{
    /// <summary>
    /// Interaction logic for FieldsView.xaml
    /// </summary>
    public partial class FieldsView : UserControl
    {
        private readonly IEventAggregator _eventAggregator;

        public FieldsView(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
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
            // ToDo: This whole thing should be moved into the view model
            var selectedFields = fieldsView.SelectedItems.Cast<FieldInfo>();
            _eventAggregator.GetEvent<SelectedFieldChangedEvent>().Publish(selectedFields);
        }
    }
}
