using System.Collections.Generic;
using System.IO;
using Lucene.Net.Index;
using Luke.Net.Features.OpenIndex;
using Luke.Net.Infrastructure.Utilities;

namespace Luke.Net.Features.Overview.Services
{
    class IndexOverviewService : IIndexOverviewService
    {
        private readonly OpenIndexModel _openIndexModel;

        public IndexOverviewService(OpenIndexModel openIndexModel)
        {
            _openIndexModel = openIndexModel;
        }

        public IEnumerable<FieldInfo> GetFields()
        {
            var directory = _openIndexModel.Directory;
            IndexReader indexReader = null;

            try
            {
                indexReader = IndexReader.Open(directory, true); // ToDo should i open this only once
                foreach (var fieldName in indexReader.GetFieldNames(IndexReader.FieldOption.ALL))
                {
                    System.Threading.Thread.Sleep(400);
                    yield return new FieldInfo { Field = fieldName };
                }
            }
            finally
            {
                if (indexReader != null)
                    indexReader.Close();
            }

            yield break;
        }

        public IndexInfo GetIndexInfo()
        {
            var directory = _openIndexModel.Directory;
            IndexReader indexReader = null;

            try
            {
                indexReader = IndexReader.Open(directory, _openIndexModel.ReadOnly);
                var v = IndexGate.GetIndexFormat(directory);

                return new IndexInfo
                           {
                               IndexDetails = IndexGate.GetFormatDetails(v),
                               FieldCount = indexReader.GetFieldNames(IndexReader.FieldOption.ALL).Count,
                               //LastModified = dir.LastWriteTime, // IndexReader.LastModified(directory),
                               Version = indexReader.GetVersion().ToString("x"),
                               DocumentCount = indexReader.NumDocs(),
                               HasDeletions = indexReader.HasDeletions(),
                               DeletionCount = indexReader.NumDeletedDocs(),
                               Optimized = indexReader.IsOptimized(),
                               IndexPath = Path.GetFullPath(_openIndexModel.Path),
                               TermCount = 0 
                           };
            }
            finally
            {
                if (indexReader != null) 
                    indexReader.Close();
            }
        }        

        public IEnumerable<TermInfo> GetTerms()
        {
            var directory = _openIndexModel.Directory;
            IndexReader indexReader = null;
            TermEnum terms = null;

            try
            {
                indexReader = IndexReader.Open(directory, true); // ToDo should i open this only once
                terms = indexReader.Terms();

                while (terms.Next())
                {
                    System.Threading.Thread.Sleep(2);
                    var term = terms.Term();
                    yield return new TermInfo { Term = term.Text(), Field = term.Field(), Frequency = terms.DocFreq() };
                }
            }
            finally
            {
                if (indexReader != null)
                    indexReader.Close();

                if (terms != null)
                    terms.Close();
            }

            yield break;
        }
    }
}