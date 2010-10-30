using System;
using Luke.Net.Features.OpenIndex;
using Luke.Net.Models;
using Luke.Net.Models.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace Luke.Net.Features.Overview
{
    public class IndexInfoViewModel : NotificationObject
    {
        private readonly IEventAggregator _eventAggregator;

        public IndexInfoViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<IndexLoadedEvent>().Subscribe(IndexLoaded);
            _eventAggregator.GetEvent<IndexChangedEvent>().Subscribe(IndexChanged);
        }

        private void IndexChanged(OpenIndexModel index)
        {
            IndexPath = index.Path;
        }

        private void IndexLoaded(LuceneIndex index)
        {
            FieldCount = index.FieldCount;
            DocumentCount = index.DocumentCount;
            TermCount = index.TermCount;
            HasDeletions = index.HasDeletions;
            DeletionCount = index.DeletionCount;
            LastModified = index.LastModified;
            Version = index.Version;
            Optimized = index.Optimized;
        }

        private string _indexPath;
        public string IndexPath
        {
            get { return _indexPath; }
            set
            {
                _indexPath = value;
                RaisePropertyChanged(() => IndexPath);
            }
        }

        private int _fieldCount;
        public int FieldCount
        {
            get { return _fieldCount; }
            private set
            {
                _fieldCount = value;
                RaisePropertyChanged(()=>FieldCount);
            }
        }

        private long _termCount;
        public long TermCount
        {
            get { return _termCount; }
            private set
            {
                _termCount = value;
                RaisePropertyChanged(() => TermCount);
            }
        }

        private DateTime _lastModified;
        public DateTime LastModified
        {
            get { return _lastModified; }
            private set
            {
                _lastModified = value;
                RaisePropertyChanged(() => LastModified);
            }
        }

        private string _version;
        public string Version
        {
            get { return _version; }
            private set
            {
                _version = value;
                RaisePropertyChanged(() => Version);
            }
        }

        private int _documentCount;
        public int DocumentCount
        {
            get { return _documentCount; }
            private set
            {
                _documentCount = value;
                RaisePropertyChanged(() => DocumentCount);
            }
        }

        private bool _hasDeletions;
        public bool HasDeletions
        {
            get { return _hasDeletions; }
            private set
            {
                _hasDeletions = value;
                RaisePropertyChanged(() => HasDeletions);
            }
        }

        private int _deletionCount;
        public int DeletionCount
        {
            get { return _deletionCount; }
            private set
            {
                _deletionCount = value;
                RaisePropertyChanged(() => DeletionCount);
            }
        }

        private bool _optimized;
        public bool Optimized
        {
            get { return _optimized; }
            private set
            {
                _optimized = value;
                RaisePropertyChanged(() => Optimized);
            }
        }
    }
}
