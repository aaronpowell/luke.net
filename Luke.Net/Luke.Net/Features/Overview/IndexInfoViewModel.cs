using System.Collections.Generic;
using Luke.Net.Features.OpenIndex;
using Luke.Net.Infrastructure;
using Luke.Net.Models.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using System.Linq;

namespace Luke.Net.Features.Overview
{
    public class IndexInfoViewModel : NotificationObject
    {
        private readonly IServiceFactory _serviceFactory;

        public IndexInfoViewModel(IEventAggregator eventAggregator, IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            
            eventAggregator.GetEvent<TermsLoadedEvent>().Subscribe(TermsLoaded);
            eventAggregator.GetEvent<IndexInfoLoadedEvent>().Subscribe(IndexInfoLoaded);
        }

        private void IndexInfoLoaded(IndexInfo indexInfo)
        {
            IndexInfo = indexInfo;
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

    public class IndexInfoLoadedEvent : CompositePresentationEvent<IndexInfo>
    {
    }
}
