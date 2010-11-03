using Luke.Net.Features.Overview.Services;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace Luke.Net.Features.Overview
{
    class OverviewModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public OverviewModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            _container = unityContainer;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterType<IIndexOverviewService, IndexOverviewService>();

            _regionManager.RegisterViewWithRegion(
                Regions.Overview, 
                () => _container.Resolve<IndexOverviewShell>());
            _regionManager.RegisterViewWithRegion(
                Regions.OverviewFields,
                () => _container.Resolve<FieldsView>());
            _regionManager.RegisterViewWithRegion(
                Regions.OverviewTerms,
                () => _container.Resolve<TermsView>());
            _regionManager.RegisterViewWithRegion(
                Regions.IndexInfo,
                () => _container.Resolve<IndexInfoView>());
        }
    }
}
