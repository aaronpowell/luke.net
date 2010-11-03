using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace Luke.Net.Features.Documents
{
    class DocumentsModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public DocumentsModule(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            _regionManager = regionManager;
            _container = unityContainer;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(
                Regions.DocumentRegion,
                () => _container.Resolve<DocumentShell>());
            _regionManager.RegisterViewWithRegion(
                Regions.DocumentListRegion,
                () => _container.Resolve<DocumentListView>());
            _regionManager.RegisterViewWithRegion(
                Regions.BrowseDocByDocNoRegion,
                () => _container.Resolve<BrowseByDocumentNoView>());
            _regionManager.RegisterViewWithRegion(
                Regions.BrowseDocByTermsRegion,
                () => _container.Resolve<BrowseByTermView>());
        }
    }
}
