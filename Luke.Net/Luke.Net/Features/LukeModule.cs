using Luke.Net.Features.Documents;
using Luke.Net.Features.Overview;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace Luke.Net.Features
{
    class LukeModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public LukeModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            _container = unityContainer;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.Resolve<OverviewModule>().Initialize();
            _container.Resolve<DocumentsModule>().Initialize();

            _regionManager.RegisterViewWithRegion(
                Regions.LuceneShellRegion,
                () => _container.Resolve<LuceneShell>());
        }
    }
}
