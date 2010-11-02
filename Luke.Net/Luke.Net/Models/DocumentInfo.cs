using System.Collections.Generic;

namespace Luke.Net.Models
{
    public class DocumentInfo
    {
        private readonly List<FieldInfo> _fields;

        public DocumentInfo(IEnumerable<FieldInfo> fields)
        {
            _fields = new List<FieldInfo>(fields);
        }

        public void AddField(FieldInfo field)
        {
            _fields.Add(field);
        }

        public IEnumerable<FieldInfo> Fields
        {
            get
            {
                return _fields;
            }
        }
    }
}
