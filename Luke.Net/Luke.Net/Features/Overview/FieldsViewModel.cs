using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Luke.Net.Features.Overview.Services;
using Luke.Net.Infrastructure;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using System.Linq;

namespace Luke.Net.Features.Overview
{
    public class FieldsViewModel : NotificationObject
    {
        private readonly IIndexOverviewService _indexOverviewService;
        private readonly List<FieldInfo> _fields = new List<FieldInfo>();

        public FieldsViewModel(IEventAggregator eventAggregator, IIndexOverviewService indexOverviewService)
        {
            _indexOverviewService = indexOverviewService;
            eventAggregator.GetEvent<TermsLoadedEvent>().Subscribe(TermsLoaded);
            InspectFields = new RelayCommand<IEnumerable<FieldInfo>>(InspectFieldsExecuted);
            Fields = new ListCollectionView(_fields);

            LoadModel();
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

        private void LoadModel()
        {
            _fields.Clear();

            IsLoading = true;
            ProgressText = "Loading fields from index. Please wait ...";
            var ui = TaskScheduler.FromCurrentSynchronizationContext();

            Task.Factory
                .StartNew(() => _indexOverviewService.GetFields().ToList())
                .ContinueWith(
                    t =>
                        {
                            _fields.AddRange(t.Result);
                            Fields.Refresh();
                            ProgressText = "Waiting for terms to load. Please wait ...";
                        },
                    ui);
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
}
