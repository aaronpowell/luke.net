using System.Collections.Generic;

namespace Luke.Net.Features.Documents
{
    public class DocumentInfo
    {
        private readonly List<FieldByDocumentInfo> _fields;

        public DocumentInfo(int documentNo, IEnumerable<FieldByDocumentInfo> fields)
        {
            _fields = new List<FieldByDocumentInfo>(fields);
            DocumentNumber = documentNo;
        }

        public void AddField(FieldByDocumentInfo fieldByTerm)
        {
            _fields.Add(fieldByTerm);
        }

        public IEnumerable<FieldByDocumentInfo> Fields
        {
            get
            {
                return _fields;
            }
        }

        public int DocumentNumber { get; private set; }
    }
}
