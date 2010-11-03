using System;
using System.Collections.Generic;
using System.Linq;
using Luke.Net.Features.OpenIndex;
using Lucene.Net.Index;
using Luke.Net.Features.Overview;
using Luke.Net.Infrastructure.Utilities;

namespace Luke.Net.Models
{
    public class LuceneIndex
    {
        public OpenIndexModel OpenIndex { get; private set; }

        public FormatDetails IndexDetails { get; private set; }

        public int FieldCount { get; private set; }

        public long TermCount { get; private set; }

        public DateTime LastModified { get; private set; }

        public string Version { get; private set; }

        public int DocumentCount { get; private set; }

        public IEnumerable<FieldByTermInfo> Fields { get; private set; }

        public bool HasDeletions { get; private set; }

        public int DeletionCount { get; private set; }

        public bool Optimized { get; private set; }

        public void LoadIndex(OpenIndexModel indexInfo)
        {
            //DirectoryInfo dir = new DirectoryInfo(indexInfo.IndexPath);
            var directory = indexInfo.Directory;
            IndexReader indexReader = null;

            try
            {
                indexReader = IndexReader.Open(directory, indexInfo.ReadOnly);
                var v = IndexGate.GetIndexFormat(directory);

                IndexDetails = IndexGate.GetFormatDetails(v);
                FieldCount = indexReader.GetFieldNames(IndexReader.FieldOption.ALL).Count;
                //LastModified = dir.LastWriteTime, // IndexReader.LastModified(directory);
                Version = indexReader.GetVersion().ToString("x");
                DocumentCount = indexReader.NumDocs();
                HasDeletions = indexReader.HasDeletions();
                DeletionCount = indexReader.NumDeletedDocs();
                Optimized = indexReader.IsOptimized();
                OpenIndex = indexInfo;
            }
            finally
            {
                if (indexReader != null) 
                    indexReader.Close();
            }
        }
    }
}
