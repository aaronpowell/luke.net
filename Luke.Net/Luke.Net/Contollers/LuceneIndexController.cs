using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lucene.Net.Store;
using Luke.Net.Models;
using Luke.Net.Utilities;
using Magellan.Framework;

namespace Luke.Net.Contollers
{
    public class LuceneIndexController : ExtendedController
    {
        public ActionResult LoadIndex(ActiveIndexModel indexInfo)
        {
            //DirectoryInfo dir = new DirectoryInfo(indexInfo.IndexPath);
            var directory = indexInfo.Directory;
            var indexReader = Lucene.Net.Index.IndexReader.Open(directory, indexInfo.ReadOnly);

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

            var model = new LoadIndexModel
            {
                IndexDetails = IndexGate.GetFormatDetails(v),
                FieldCount = indexReader.GetFieldNames(Lucene.Net.Index.IndexReader.FieldOption.ALL).Count,
                TermCount = termCount,
                //LastModified = dir.LastWriteTime, // IndexReader.LastModified(directory),
                Version = indexReader.GetVersion().ToString("x"),
                DocumentCount = indexReader.NumDocs(),
                Fields = fieldCounter.Select(x =>
                {
                    x.Frequency = ((double)x.Count * 100) / (double)termCount;
                    return x;
                }),
                HasDeletions = indexReader.HasDeletions(),
                DeletionCount = indexReader.NumDeletedDocs(),
                Optimized = indexReader.IsOptimized(),
                Terms = termCounter.OrderByDescending(x => x.Frequency).Select((x, i) =>
                {
                    x.Rank = i+1;
                    return x;
                }),
                ActiveIndex = indexInfo
            };

            return Page(model);
        }

        public ActionResult TermsForFields(IEnumerable<FieldInfo> fields)
        {
            return Object(null);
        }
    }
}
