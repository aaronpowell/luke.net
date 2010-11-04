using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Luke.Net.Features.Overview.Services;
using Luke.Net.Infrastructure;
using Luke.Net.Models;
using Luke.Net.Models.Events;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace Luke.Net.Features.Overview
{
    public class TermsViewModel : NotificationObject
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IIndexOverviewService _indexOverviewService;
        private IEnumerable<FieldByTermInfo> _fields;
        private readonly ObservableCollection<TermInfo> _terms;

        public TermsViewModel(IEventAggregator eventAggregator, IIndexOverviewService indexOverviewService)
        {
            _eventAggregator = eventAggregator;
            _indexOverviewService = indexOverviewService;

            // just an empty array till an index is provided
            _fields = new FieldByTermInfo[] {};

            _eventAggregator.GetEvent<SelectedFieldChangedEvent>().Subscribe(FilterTermsExecuted);
            FilterTerms = new RelayCommand<IEnumerable<FieldByTermInfo>>(FilterTermsExecuted);

            _terms = new ObservableCollection<TermInfo>();
            Terms = new ListCollectionView(_terms);

            InspectDocuments = new RelayCommand(ExecuteInspectDocuments, CanInspectDocuments);

            LoadModel();
        }

        private void ExecuteInspectDocuments()
        {
            var selectedTerm = (TermInfo)Terms.CurrentItem;
            _eventAggregator.GetEvent<InspectDocumentsForTermEvent>().Publish(selectedTerm);
        }

        private bool CanInspectDocuments()
        {
            return Terms.CurrentItem != null;
        }

        private void LoadModel()
        {
            _fields = _indexOverviewService.GetFieldsAndTerms();

            UpdateTermsView();

            RaisePropertyChanged(() => TermCount);
        }

        public ICommand FilterTerms { get; set; }

        void FilterTermsExecuted(IEnumerable<FieldByTermInfo> fields)
        {
            if (fields != null && fields.Any())
                _fields = fields;
            else
                _fields = _indexOverviewService.GetFieldsAndTerms();

            UpdateTermsView();
            RaisePropertyChanged(() => TermCount);
        }

        void UpdateTermsView()
        {
            _terms.Clear();

            _fields
                .SelectMany(f => f.Terms)
                .OrderByDescending(t => t.Frequency)
                .Take(NumberOfTopTerms)
                .ForEach(t => _terms.Add(t));

            // notify that terms view has changed. 
            RaisePropertyChanged(() => Terms);
        }

        private int _numberOfTopTerms = 50; // the default number of items to show
        public int NumberOfTopTerms
        {
            get { return _numberOfTopTerms; }
            set
            {
                _numberOfTopTerms = value;
                RaisePropertyChanged(() => NumberOfTopTerms);
                RaisePropertyChanged(() => Terms);
            }
        }

        public ListCollectionView Terms { get; private set;}

        public int TermCount
        {
            get
            {
                return _fields.SelectMany(f => f.Terms).Count();
            }
        }

        public ICommand InspectDocuments { get; private set; }
    }
}