using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lucene.Net.Index;
using Luke.Net.Features.OpenIndex;
using Luke.Net.Infrastructure.Utilities;
using Luke.Net.Models;

namespace Luke.Net.Features.Overview.Services
{
    class IndexOverviewService : IIndexOverviewService
    {
        private readonly OpenIndexModel _openIndexModel;
        private List<FieldByTermInfo> _fields;

        public IndexOverviewService()
        {
            // ToDo: i should change this soon
            _openIndexModel = App.OpenIndexModel;
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
                               TermCount = GetFieldsAndTerms().SelectMany(f =>f.Terms).Count()
                           };
            }
            finally
            {
                if (indexReader != null) 
                    indexReader.Close();
            }
        }        

        public IEnumerable<FieldByTermInfo> GetFieldsAndTerms()
        {
            if (_fields != null)
                return _fields;

            _fields = new List<FieldByTermInfo>();

            var directory = _openIndexModel.Directory;
            IndexReader indexReader = null;

            try
            {
                indexReader = IndexReader.Open(directory, true); // ToDo should i open this only once

                var terms = indexReader.Terms();
                var termCount = 0;

                _fields.Clear();

                while (terms.Next())
                {
                    var term = terms.Term();
                    var field = _fields.SingleOrDefault(x => x.Field.FieldName == term.Field());

                    if (field != null)
                    {
                        field.Count++;
                    }
                    else
                    {
                        field = new FieldByTermInfo { Count = 1, Field = new Field(term.Field()) };
                        _fields.Add(field);
                    }

                    var termInfo = new TermInfo { Term = term.Text(), Field = field, Frequency = terms.DocFreq() };
                    field.AddTerm(termInfo);

                    termCount++;
                }

                _fields.Select(x =>
                                   {
                                       x.Frequency = ((double)x.Count * 100) / termCount;
                                       return x;
                                   });
            }
            finally
            {
                if (indexReader != null)
                    indexReader.Close();
            }

            return _fields;
        }
    }
}