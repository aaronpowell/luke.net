using System.Collections.Generic;

namespace Luke.Net.Features.Documents.Services
{
    public interface IDocumentService
    {
        IEnumerable<DocumentInfo> GetDocumentsInfo();
        DocumentInfo GetDocumentInfo(int documentNo);
        int GetNumberOfDocuments();
    }
}