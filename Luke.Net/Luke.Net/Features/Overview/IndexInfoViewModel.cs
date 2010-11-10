using System.Collections.Generic;
using Luke.Net.Features.Overview.Services;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using System.Linq;

namespace Luke.Net.Features.Overview
{
    public class IndexInfoViewModel : NotificationObject
    {
        private readonly IIndexOverviewService _indexOverviewService;

        public IndexInfoViewModel(IEventAggregator eventAggregator, IIndexOverviewService indexOverviewService)
        {
            _indexOverviewService = indexOverviewService;
            eventAggregator.GetEvent<TermsLoadedEvent>().Subscribe(TermsLoaded);

            IndexInfo = _indexOverviewService.GetIndexInfo();
        }

        private void TermsLoaded(IEnumerable<TermInfo> terms)
        {
            IndexInfo.TermCount = terms.Count();
            RaisePropertyChanged(() => IndexInfo);
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
