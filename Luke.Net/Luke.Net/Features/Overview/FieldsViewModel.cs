using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Luke.Net.Features.OpenIndex;
using Luke.Net.Infrastructure;
using Luke.Net.Models.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using System.Linq;

namespace Luke.Net.Features.Overview
{
    public class FieldsViewModel : NotificationObject
    {
        private readonly List<FieldInfo> _fields = new List<FieldInfo>();

        public FieldsViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<TermsLoadedEvent>().Subscribe(TermsLoaded);
            eventAggregator.GetEvent<FieldsLoadedEvent>().Subscribe(FieldsLoaded);
            eventAggregator.GetEvent<FieldsLoadingEvent>().Subscribe(FieldsLoading);
            InspectFields = new RelayCommand<IEnumerable<FieldInfo>>(InspectFieldsExecuted);
            Fields = new ListCollectionView(_fields);
        }

        private void FieldsLoading(IndexInfo indexInfo)
        {
            _fields.Clear();
            IsLoading = true;
            ProgressText = string.Format("Loading {0} fields from index. Please wait ...", indexInfo.FieldCount);
        }

        private void FieldsLoaded(IEnumerable<FieldInfo> fields)
        {
            ProgressText = "Fields Loaded. Please wait ...";
            _fields.AddRange(fields);
            Fields.Refresh();
        }

        private void TermsLoaded(IEnumerable<TermInfo> terms)
        {
            // calculating fields properties after terms are loaded
            ProgressText = "Calculating term count and percentage. Please wait ...";
            var termsCount = terms.Count();

            foreach (var field in _fields)
            {
                FieldInfo field1 = field;
                field.Count = terms.Count(t => t.Field == field1.Field);
                field.Frequency = ((double)field.Count*100)/termsCount;
            }

            Fields.Refresh();
            IsLoading = false;
        }

        public ICommand InspectFields { get; set; }

        void InspectFieldsExecuted(IEnumerable<FieldInfo> fields)
        {
        }

        public ICollectionView Fields { get; private set; }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            private set
            {
                _isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }

        private string _progressText;
        public string ProgressText
        {
            get { return _progressText; }
            private set
            {
                _progressText = value;
                RaisePropertyChanged(()=>ProgressText);
            }
        }
    }

    public class FieldsLoadingEvent : CompositePresentationEvent<IndexInfo>
    {
    }
}
