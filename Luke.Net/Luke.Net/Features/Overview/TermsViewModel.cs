using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Luke.Net.Infrastructure;
using Luke.Net.Models;
using Luke.Net.Models.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace Luke.Net.Features.Overview
{
    public class TermsViewModel : NotificationObject
    {
        private LuceneIndex _index;
        private IEnumerable<FieldInfo> _fields;

        public TermsViewModel(IEventAggregator eventAggregator)
        {
            // just an empty array till an index is provided
            _fields = new FieldInfo[] {};

            eventAggregator.GetEvent<SelectedFieldChangedEvent>().Subscribe(FilterTermsExecuted);
            eventAggregator.GetEvent<IndexLoadedEvent>().Subscribe(IndexChanged);
            FilterTerms = new RelayCommand<IEnumerable<FieldInfo>>(FilterTermsExecuted);
        }

        private void IndexChanged(LuceneIndex index)
        {
            _index = index;
            _fields = _index.Fields;
            RaisePropertyChanged(() => Terms);
            RaisePropertyChanged(() => TermCount);
        }

        public ICommand FilterTerms { get; set; }

        void FilterTermsExecuted(IEnumerable<FieldInfo> fields)
        {
            if (fields != null && fields.Any())
                _fields = fields;
            else
                _fields = _index.Fields;

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