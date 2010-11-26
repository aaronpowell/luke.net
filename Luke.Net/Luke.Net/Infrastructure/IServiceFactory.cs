using Luke.Net.Features.Documents.Services;
using Luke.Net.Features.OpenIndex;
using Luke.Net.Features.Overview.Services;

namespace Luke.Net.Infrastructure
{
    public interface IServiceFactory
    {
        IDocumentService CreateDocumentService(OpenIndexModel index);
        IIndexOverviewService CreateIndexOverviewService(OpenIndexModel index);
    }

    class ServiceFactory : IServiceFactory
    {
        public IDocumentService CreateDocumentService(OpenIndexModel index)
        {
            return new DocumentService(index);
        }

        public IIndexOverviewService CreateIndexOverviewService(OpenIndexModel index)
        {
            return new IndexOverviewService(index);
        }
    }
}
