using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Luke.Net.Infrastructure;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace Luke.Net.Features.Overview
{
    public class FieldsViewModel : NotificationObject
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ITermService _termService;
        private readonly List<FieldByTermInfo> _fields = new List<FieldByTermInfo>();

        public FieldsViewModel(IEventAggregator eventAggregator, ITermService termService)
        {
            _eventAggregator = eventAggregator;
            _termService = termService;
            InspectFields = new RelayCommand<IEnumerable<FieldByTermInfo>>(InspectFieldsExecuted);
            Fields = new ListCollectionView(_fields);

            LoadModel();
        }

        private void LoadModel()
        {
            _fields.Clear();
            _fields.AddRange(_termService.GetFieldsAndTerms());
            Fields.Refresh();
        }

        public ICommand InspectFields { get; set; }

        void InspectFieldsExecuted(IEnumerable<FieldByTermInfo> fields)
        {
        }

        public ICollectionView Fields { get; private set; }
    }
}
