using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Luke.Net.Features.OpenIndex;
using Luke.Net.Infrastructure;
using Luke.Net.Models;
using Luke.Net.Models.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using FieldInfo = Luke.Net.Models.FieldInfo;

namespace Luke.Net.Features.Overview
{
    public class FieldsViewModel : NotificationObject
    {
        private readonly IEventAggregator _eventAggregator;
        private LuceneIndex _model;
        private readonly List<FieldInfo> _fields = new List<FieldInfo>();

        public FieldsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            InspectFields = new RelayCommand<IEnumerable<FieldInfo>>(InspectFieldsExecuted);
            eventAggregator.GetEvent<IndexLoadedEvent>().Subscribe(IndexChanged);
            Fields = new ListCollectionView(_fields);
        }

        private void IndexChanged(LuceneIndex index)
        {
            _model = index;

            _fields.Clear();
            _fields.AddRange(_model.Fields);
            Fields.Refresh();
        }

        public ICommand InspectFields { get; set; }

        void InspectFieldsExecuted(IEnumerable<FieldInfo> fields)
        {
            var searcher = new IndexSearcher(_model.OpenIndex.Directory, true);

            var q = new BooleanQuery();
            foreach (var field in fields)
            {
                q.Add(new TermQuery(new Term(field.Field)), BooleanClause.Occur.SHOULD);
            }
        }

        public ICollectionView Fields { get; private set; }
    }
}
