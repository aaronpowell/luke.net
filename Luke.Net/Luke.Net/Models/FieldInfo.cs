using System.Collections.Generic;

namespace Luke.Net.Models
{
    public class FieldInfo
    {
        public FieldInfo()
        {
            _terms = new List<TermInfo>();
        }

        public string Field { get; set; }
        public string Value { get; set; }
        private readonly List<TermInfo> _terms;

        public IEnumerable<TermInfo> Terms
        {
            get
            {
                return _terms;
            }
        }

        public void AddTerm(TermInfo term)
        {
            _terms.Add(term);
        }
    }
}