using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace Luke.Net.Features.Documents
{
    class DocumentsModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unityContainer;

        public DocumentsModule(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            _regionManager = regionManager;
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(
                Regions.DocumentRegion,
                () => _unityContainer.Resolve<DocumentShell>());
            _regionManager.RegisterViewWithRegion(
                Regions.DocumentListRegion,
                () => _unityContainer.Resolve<DocumentListView>());
            _regionManager.RegisterViewWithRegion(
                Regions.BrowseDocByDocNoRegion,
                () => _unityContainer.Resolve<BrowseByDocumentNoView>());
            _regionManager.RegisterViewWithRegion(
                Regions.BrowseDocByTermsRegion,
                () => _unityContainer.Resolve<BrowseByTermView>());
        }
    }
}
