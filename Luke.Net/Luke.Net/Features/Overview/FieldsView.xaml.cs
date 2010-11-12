using System.Linq;
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

        public FieldsView(IEventAggregator eventAggregator, FieldsViewModel viewModel)
        {
            DataContext = viewModel;
            _eventAggregator = eventAggregator;
            InitializeComponent();
        }

        private void fieldsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ToDo: This whole thing should be moved into the view model
            var selectedFields = fieldsView.SelectedItems.Cast<FieldInfo>();
            _eventAggregator.GetEvent<SelectedFieldChangedEvent>().Publish(selectedFields);
        }
    }
}
