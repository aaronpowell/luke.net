using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Luke.Net.Features.Overview.Services;
using Luke.Net.Infrastructure;
using Luke.Net.Models;
using Luke.Net.Models.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace Luke.Net.Features.Overview
{
    public class TermsViewModel : NotificationObject
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IIndexOverviewService _indexOverviewService;
        private List<TermInfo> _terms;
        private readonly ObservableCollection<TermInfo> _termsCollection;
        private IEnumerable<FieldInfo> _fieldsFilter;

        public TermsViewModel(IEventAggregator eventAggregator, IIndexOverviewService indexOverviewService)
        {
            _eventAggregator = eventAggregator;
            _indexOverviewService = indexOverviewService;
            _eventAggregator.GetEvent<SelectedFieldChangedEvent>().Subscribe(FilterTermsExecuted);

            _terms = new List<TermInfo>();
            FilterTerms = new RelayCommand<IEnumerable<FieldInfo>>(FilterTermsExecuted);
            _termsCollection = new ObservableCollection<TermInfo>();
            Terms = new ListCollectionView(_termsCollection);

            InspectDocuments = new RelayCommand(ExecuteInspectDocuments, CanInspectDocuments);

            LoadModel();
        }

        private void ExecuteInspectDocuments()
        {
            var selectedTerm = (TermInfo)Terms.CurrentItem;
            var termToInspect = new TermToInspect{FieldName = selectedTerm.Field, TermName = selectedTerm.Term};
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
            var pipeline = new BlockingCollection<TermInfo>();

            Task.Factory.StartNew(
                () =>
                    {
                        foreach (var term in _indexOverviewService.GetTerms())
                            pipeline.Add(term);

                        pipeline.CompleteAdding();
                    });

            Task.Factory
                .StartNew(
                    () =>
                        {
                            foreach (var term in pipeline.GetConsumingEnumerable())
                                _terms.Add(term);
                        })
                .ContinueWith(
                    t =>
                        {
                            UpdateTermsView();
                            RaisePropertyChanged(() => TermCount);
                            IsLoading = false;
                            // let others know that terms have been loaded
                            _eventAggregator.GetEvent<TermsLoadedEvent>().Publish(_terms);
                        },
                    CancellationToken.None,
                    TaskContinuationOptions.OnlyOnRanToCompletion,
                    ui);
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

        void FilterTermsExecuted(IEnumerable<FieldInfo> fields)
        {
            if (fields != null && fields.Any())
                _fieldsFilter = fields;
            else
                _fieldsFilter = null;

            NumberOfTopTerms = Math.Min(NumberOfTopTerms, TermCount);

            UpdateTermsView();
            RaisePropertyChanged(() => TermCount);
        }

        void UpdateTermsView()
        {
            lock (_termsCollection)
            {
                _termsCollection.Clear();

                var terms = from term in _terms
                            where _fieldsFilter == null || _fieldsFilter.Any(f => f.Field == term.Field)
                            orderby term.Frequency descending
                            select term;

                foreach (var term in terms.Take(NumberOfTopTerms))
                    _termsCollection.Add(term);
            }
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
                return _terms.Count();
            }
        }

        public ICommand InspectDocuments { get; private set; }
    }
}