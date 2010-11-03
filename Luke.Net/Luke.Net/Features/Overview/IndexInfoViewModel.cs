using System;
using Luke.Net.Features.Overview.Services;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace Luke.Net.Features.Overview
{
    public class IndexInfoViewModel : NotificationObject
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IIndexOverviewService _indexOverviewService;

        public IndexInfoViewModel(IEventAggregator eventAggregator, IIndexOverviewService indexOverviewService)
        {
            _eventAggregator = eventAggregator;
            _indexOverviewService = indexOverviewService;

            LoadModel();
        }

        private void LoadModel()
        {
            var indexInfo = _indexOverviewService.GetIndexInfo();

            FieldCount = indexInfo.FieldCount;
            DocumentCount = indexInfo.DocumentCount;
            TermCount = indexInfo.TermCount;
            HasDeletions = indexInfo.HasDeletions;
            DeletionCount = indexInfo.DeletionCount;
            LastModified = indexInfo.LastModified;
            Version = indexInfo.Version;
            Optimized = indexInfo.Optimized;
            IndexPath = indexInfo.IndexPath;
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
