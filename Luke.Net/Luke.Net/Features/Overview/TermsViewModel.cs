using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Luke.Net.Features.Overview.Services;
using Luke.Net.Infrastructure;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace Luke.Net.Features.Overview
{
    public class TermsViewModel : NotificationObject
    {
        private readonly IIndexOverviewService _indexOverviewService;
        private IEnumerable<FieldByTermInfo> _fields;

        public TermsViewModel(IEventAggregator eventAggregator, IIndexOverviewService indexOverviewService)
        {
            _indexOverviewService = indexOverviewService;

            // just an empty array till an index is provided
            _fields = new FieldByTermInfo[] {};

            eventAggregator.GetEvent<SelectedFieldChangedEvent>().Subscribe(FilterTermsExecuted);
            FilterTerms = new RelayCommand<IEnumerable<FieldByTermInfo>>(FilterTermsExecuted);

            LoadModel();
        }

        private void LoadModel()
        {
            _fields = _indexOverviewService.GetFieldsAndTerms();
            RaisePropertyChanged(() => Terms);
            RaisePropertyChanged(() => TermCount);
        }

        public ICommand FilterTerms { get; set; }

        void FilterTermsExecuted(IEnumerable<FieldByTermInfo> fields)
        {
            if (fields != null && fields.Any())
                _fields = fields;
            else
                _fields = _indexOverviewService.GetFieldsAndTerms();

            // notify that terms view has changed. 
            RaisePropertyChanged(() => Terms);
            RaisePropertyChanged(() => TermCount);
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

        public IEnumerable<TermInfo> Terms
        {
            get
            {
                return _fields.SelectMany(f => f.Terms).OrderByDescending(t => t.Frequency).Take(NumberOfTopTerms);
            }
        }

        public int TermCount
        {
            get
            {
                return _fields.SelectMany(f => f.Terms).Count();
            }
        }
    }
}