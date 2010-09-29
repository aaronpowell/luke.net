using Lucene.Net.Store;

namespace Luke.Net.Models
{
    public class ActiveIndexModel
    {
        public string Path { get; set; }
        public bool ReadOnly { get; set; }
        public Directory Directory { get; set; }
    }
}
