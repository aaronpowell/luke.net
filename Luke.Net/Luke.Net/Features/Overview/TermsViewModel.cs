using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
            var termToInspect = new TermToInspect{FieldName = selectedTerm.Field.Field, TermName = selectedTerm.Term};
            _eventAggregator.GetEvent<InspectDocumentsForTermEvent>().Publish(termToInspect);
        }

        private bool CanInspectDocuments()
        {
            return Terms.CurrentItem != null;
        }

        private void LoadModel()
        {
            IsLoading = true;

            var ui = TaskScheduler.FromCurrentSynchronizationContext();

            Task.Factory
                .StartNew(() => _indexOverviewService.GetFieldsAndTerms())
                .ContinueWith(t =>
                                  {
                                      _fields = t.Result;
                                      UpdateTermsView();
                                      RaisePropertyChanged(() => TermCount);
                                      IsLoading = false;
                                  }, ui);
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                RaisePropertyChanged(()=>IsLoading);
            }
        }

        public ICommand FilterTerms { get; set; }

        void FilterTermsExecuted(IEnumerable<FieldByTermInfo> fields)
        {
            if (fields != null && fields.Any())
                _fields = fields;
            else
                _fields = _indexOverviewService.GetFieldsAndTerms();

            NumberOfTopTerms = Math.Min(NumberOfTopTerms, TermCount);

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

                UpdateTermsView();
                RaisePropertyChanged(() => NumberOfTopTerms);
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