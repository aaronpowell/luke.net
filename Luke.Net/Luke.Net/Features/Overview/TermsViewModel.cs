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
        private readonly List<TermInfo> _terms = new List<TermInfo>();

        public TermsViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<SelectedFieldChangedEvent>().Subscribe(FilterTermsExecuted);
            eventAggregator.GetEvent<IndexLoadedEvent>().Subscribe(IndexChanged);
            FilterTerms = new RelayCommand<IEnumerable<FieldInfo>>(FilterTermsExecuted);
        }

        private void IndexChanged(LuceneIndex index)
        {
            _terms.Clear();
            _terms.AddRange(index.Terms);
            RaisePropertyChanged(() => Terms);
            RaisePropertyChanged(() => TermCount);
        }

        public ICommand FilterTerms { get; set; }

        void FilterTermsExecuted(IEnumerable<FieldInfo> fields)
        {
            // ToDo: Termporary solution. Should think of a better way
            if (fields != null && fields.Any())
                _termFilter = t => fields.Any(f => f.Field == t.Field);
            else
                _termFilter = _defaultTermFilter;

            // notify that terms view has changed. 
            // ToDo: should find a better way. 
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

        private readonly Func<TermInfo, bool> _defaultTermFilter = t => true;
        private Func<TermInfo, bool> _termFilter = t => true;

        public IEnumerable<TermInfo> Terms
        {
            get
            {
                if (_terms == null)
                    return new TermInfo[] { }; // ToDo: just a quick hack. should be fixed

                return _terms.Where(_termFilter).Take(NumberOfTopTerms);
            }
        }

        public int TermCount
        {
            get
            {
                return _terms.Count();
            }
        }
    }
}