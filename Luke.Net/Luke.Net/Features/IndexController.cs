using Luke.Net.Features.OpenIndex;
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
        private LuceneIndex _model;

        public IndexController(
            IEventAggregator eventAggregator,
            IRegionManager regionManager,
            IUnityContainer container)
        {
            // ToDo: just a rough cut; still not quite sure who should create the model
            _model = new LuceneIndex();
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _container = container;
            eventAggregator.GetEvent<IndexChangedEvent>().Subscribe(UpdateIndex);
        }

        private void UpdateIndex(OpenIndexModel newIndex)
        {
            App.OpenIndexModel = newIndex;

            _model.LoadIndex(newIndex);
            _container.Resolve<LukeModule>().Initialize();
        }
    }

    public interface IIndexController
    {
    }
}
