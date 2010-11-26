using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Luke.Net.Features.OpenIndex;
using Luke.Net.Features.Overview;
using Luke.Net.Features.Overview.Services;
using Luke.Net.Infrastructure;
using Luke.Net.Models;
using Luke.Net.Models.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace Luke.Net.Features
{
    class IndexController : IIndexController
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly IServiceFactory _serviceFactory;
        private IIndexOverviewService _indexOverviewService;

        public IndexController(
            IEventAggregator eventAggregator,
            IRegionManager regionManager,
            IServiceFactory serviceFactory)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _serviceFactory = serviceFactory;
            eventAggregator.GetEvent<InspectDocumentsForTermEvent>().Subscribe(ActivateDocumentsView);
            eventAggregator.GetEvent<IndexChangedEvent>().Subscribe(IndexChanged);
        }

        // this method is used to synchronise between dependent threads
        private void IndexChanged(OpenIndexModel indexModel)
        {
            _indexOverviewService = _serviceFactory.CreateIndexOverviewService(indexModel);
            var ui = TaskScheduler.FromCurrentSynchronizationContext();

            var indexInfo = LoadInfo();
            _eventAggregator.GetEvent<IndexInfoLoadedEvent>().Publish(indexInfo);
            
            _eventAggregator.GetEvent<FieldsLoadingEvent>().Publish(indexInfo);

            Task<IEnumerable<FieldInfo>>.Factory
                .StartNew(() => LoadFields().ToList())
                .ContinueWith(
                    t=> 
                    {
                        _eventAggregator.GetEvent<FieldsLoadedEvent>().Publish(t.Result);
                        _eventAggregator.GetEvent<TermsLoadingEvent>().Publish(indexInfo);
                    },
                    CancellationToken.None,
                    TaskContinuationOptions.None,
                    ui)
                .ContinueWith(t => LoadTerms().ToList())
                .ContinueWith(
                    t => _eventAggregator.GetEvent<TermsLoadedEvent>().Publish(t.Result),
                    CancellationToken.None,
                    TaskContinuationOptions.None,
                    ui);
        }

        private IEnumerable<TermInfo> LoadTerms()
        {
            return _indexOverviewService.GetTerms();
        }

        private IndexInfo LoadInfo()
        {
            return _indexOverviewService.GetIndexInfo();
        }

        private IEnumerable<FieldInfo> LoadFields()
        {
            return _indexOverviewService.GetFields();
        }

        private void ActivateDocumentsView(TermToInspect termToInspect)
        {
            // ToDo: This is not very nice, I know. I will have to find a better way to activate the tab
            var documentsRegion = _regionManager.Regions[Regions.LuceneShellRegion];
            var view = documentsRegion.Views.OfType<LuceneShell>().Single();
            view.DocumentsTab.IsSelected = true;
        }
    }

    internal class TermsLoadingEvent : CompositePresentationEvent<IndexInfo>
    {
    }

    internal class FieldsLoadedEvent : CompositePresentationEvent<IEnumerable<FieldInfo>>
    {
    }

    public interface IIndexController
    {
    }
}
