using System.IO;
using Lucene.Net.Store;
using Directory = Lucene.Net.Store.Directory;

namespace Luke.Net.Features.Popup
{
    public class ActiveIndexModel
    {
        public ActiveIndexModel()
        {
            Path = @"..\..\..\..\LuceneIndex";
        }

        public string Path { get; set; }
        public bool ReadOnly { get; set; }
        public Directory Directory
        {
            get
            {
                if (string.IsNullOrEmpty(Path))
                    return null;

                return FSDirectory.Open(new DirectoryInfo(Path));
            }
        }
    }
}
