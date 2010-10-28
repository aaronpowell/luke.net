using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace Luke.Net.Features.Overview
{
    class OverviewModule : IModule
    {
        private readonly IUnityContainer _unityContainer;
        private readonly IRegionManager _regionManager;

        public OverviewModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            _unityContainer = unityContainer;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(
                Regions.Overview, 
                () => _unityContainer.Resolve<IndexOverviewShell>());
            _regionManager.RegisterViewWithRegion(
                Regions.OverviewFields,
                () => _unityContainer.Resolve<FieldsView>());
            _regionManager.RegisterViewWithRegion(
                Regions.OverviewTerms,
                () => _unityContainer.Resolve<TermsView>());
        }
    }
}
