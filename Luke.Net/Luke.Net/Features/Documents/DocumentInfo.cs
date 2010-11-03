using System.Collections.Generic;

namespace Luke.Net.Features.Documents
{
    public class DocumentInfo
    {
        private readonly List<FieldByDocumentInfo> _fields;

        public DocumentInfo(IEnumerable<FieldByDocumentInfo> fields)
        {
            _fields = new List<FieldByDocumentInfo>(fields);
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
    }
}
