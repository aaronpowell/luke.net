using System;
using System.Collections.Generic;
using System.Linq;
using Luke.Net.Features.OpenIndex;
using Lucene.Net.Index;
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

        public IEnumerable<FieldInfo> Fields { get; private set; }

        public bool HasDeletions { get; private set; }

        public int DeletionCount { get; private set; }

        public bool Optimized { get; private set; }

        public IEnumerable<TermInfo> Terms { get; private set; }

        public void LoadIndex(OpenIndexModel indexInfo)
        {
            //DirectoryInfo dir = new DirectoryInfo(indexInfo.IndexPath);
            var directory = indexInfo.Directory;
            var indexReader = IndexReader.Open(directory, indexInfo.ReadOnly);

            var v = IndexGate.GetIndexFormat(directory);

            var termCount = 0;
            var terms = indexReader.Terms();

            var fieldCounter = new List<FieldInfo>();

            var termCounter = new List<TermInfo>();

            while (terms.Next())
            {
                var term = terms.Term();
                string field = term.Field();

                if (fieldCounter.Any(x => x.Field == field))
                {
                    fieldCounter.Single(x => x.Field == field).Count++;
                }
                else
                {
                    fieldCounter.Add(new FieldInfo
                    {
                        Field = field,
                        Count = 0,
                    });
                }

                termCounter.Add(new TermInfo { Term = term.Text(), Field = field, Frequency = terms.DocFreq() });

                termCount++;
            }

            IndexDetails = IndexGate.GetFormatDetails(v);
            FieldCount = indexReader.GetFieldNames(IndexReader.FieldOption.ALL).Count;
            TermCount = termCount;
            //LastModified = dir.LastWriteTime, // IndexReader.LastModified(directory);
            Version = indexReader.GetVersion().ToString("x");
            DocumentCount = indexReader.NumDocs();
            Fields = fieldCounter.Select(x =>
                                             {
                                                 x.Frequency = ((double) x.Count*100)/termCount;
                                                 return x;
                                             });
            HasDeletions = indexReader.HasDeletions();
            DeletionCount = indexReader.NumDeletedDocs();
            Optimized = indexReader.IsOptimized();
            Terms = termCounter; 
            OpenIndex = indexInfo;
        }
    }
}
