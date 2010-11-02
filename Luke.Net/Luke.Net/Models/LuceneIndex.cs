using System;
using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Documents;
using Luke.Net.Features.OpenIndex;
using Lucene.Net.Index;
using Luke.Net.Infrastructure.Utilities;

namespace Luke.Net.Models
{
    public class LuceneIndex
    {
        public LuceneIndex()
        {
            _documents = new List<DocumentInfo>();
        }

        public OpenIndexModel OpenIndex { get; private set; }

        public FormatDetails IndexDetails { get; private set; }

        public int FieldCount { get; private set; }

        public long TermCount { get; private set; }

        public DateTime LastModified { get; private set; }

        public string Version { get; private set; }

        public int DocumentCount { get; private set; }

        private readonly List<DocumentInfo> _documents;
        public IEnumerable<DocumentInfo> Documents
        {
            get
            {
                return _documents;
            }
        }

        public bool HasDeletions { get; private set; }

        public int DeletionCount { get; private set; }

        public bool Optimized { get; private set; }

        public void LoadIndex(OpenIndexModel indexInfo)
        {
            IndexReader indexReader = null;

            try
            {
                var directory = indexInfo.Directory;
                indexReader = IndexReader.Open(directory, indexInfo.ReadOnly);
                var fieldCounter = new List<FieldInfo>();

                for (var i = 0; i < indexReader.NumDocs(); i++)
                {
                    var doc = indexReader.Document(i);
                    var fields = new List<FieldInfo>();
                    foreach (var field in doc.GetFields().Cast<Field>())
                    {
                        var fieldInfo = new FieldInfo {Field = field.Name(), Value = doc.Get(field.Name())};
                        fields.Add(fieldInfo);
                        var terms = indexReader.Terms(new Term(field.Name(), string.Empty));
                        while (terms.Next())
                        {
                            var term = terms.Term();
                            fieldInfo.AddTerm(new TermInfo {Field = fieldInfo, Term = term.Text()});
                        }
                    }

                    _documents.Add(new DocumentInfo(fields));
                }

                var termCount = 0;
                //var terms = indexReader.Terms();

                //var termCounter = new List<TermInfo>();

                //while (terms.Next())
                //{
                //    var term = terms.Term();
                //    var field = fieldCounter.SingleOrDefault(x => x.Field == term.Field());

                //    if(field != null)
                //    {
                //        field.Count++;
                //    }
                //    else
                //    {
                //        field = new FieldInfo {Count = 1, Field = term.Field()};
                //        fieldCounter.Add(field);
                //    }

                //    var termInfo = new TermInfo {Term = term.Text(), Field = field, Frequency = terms.DocFreq()};
                //    termCounter.Add(termInfo);
                //    field.AddTerm(termInfo);

                //    termCount++;
                //}

                IndexDetails = IndexGate.GetFormatDetails(IndexGate.GetIndexFormat(directory));
                FieldCount = indexReader.GetFieldNames(IndexReader.FieldOption.ALL).Count;
                TermCount = termCount;
                //LastModified = dir.LastWriteTime, // IndexReader.LastModified(directory);
                Version = indexReader.GetVersion().ToString("x");
                DocumentCount = indexReader.NumDocs();
                //Fields = fieldCounter.Select(x =>
                //                                 {
                //                                     x.Frequency = ((double) x.Count*100)/termCount;
                //                                     return x;
                //                                 });
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
