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

            IndexInfo = _indexOverviewService.GetIndexInfo();
        }

        private IndexInfo _indexInfo;
        public IndexInfo IndexInfo
        {
            get { return _indexInfo; }
            set
            {
                _indexInfo = value;
                RaisePropertyChanged(()=>IndexInfo);
            }
        }
    }
}
