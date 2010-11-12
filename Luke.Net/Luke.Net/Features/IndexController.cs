using System;
using System.Linq;
using Luke.Net.Features.OpenIndex;
using Luke.Net.Features.Overview;
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
        private readonly IUnityContainer _container;

        public IndexController(
            IEventAggregator eventAggregator,
            IRegionManager regionManager,
            IUnityContainer container)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _container = container;
            eventAggregator.GetEvent<IndexChangedEvent>().Subscribe(UpdateIndex);
            eventAggregator.GetEvent<InspectDocumentsForTermEvent>().Subscribe(ActivateDocumentsView);
        }

        private void ActivateDocumentsView(TermToInspect obj)
        {
            // ToDo: This is not very nice, I know. I will have to find a better way to activate the tab
            var documentsRegion = _regionManager.Regions[Regions.LuceneShellRegion];
            var view = documentsRegion.Views.OfType<LuceneShell>().Single();
            view.DocumentsTab.IsSelected = true;
        }

        private void UpdateIndex(OpenIndexModel newIndex)
        {
            App.OpenIndexModel = newIndex;

            _container.Resolve<LukeModule>().Initialize();
        }
    }

    public interface IIndexController
    {
    }
}
