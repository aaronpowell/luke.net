using System.Collections.Generic;
using Luke.Net.Models;

namespace Luke.Net.Features.Documents.Services
{
    public interface IDocumentService
    {
        IEnumerable<DocumentInfo> GetDocumentsInfo();
        DocumentInfo GetDocumentInfo(int documentNo);
        int GetNumberOfDocuments();
        IEnumerable<string> GetFields();
    }
}