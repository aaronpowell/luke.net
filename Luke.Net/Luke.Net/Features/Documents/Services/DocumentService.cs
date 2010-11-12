using System;
using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Luke.Net.Features.OpenIndex;
using Luke.Net.Models;

namespace Luke.Net.Features.Documents.Services
{
    class DocumentService : IDocumentService
    {
        private readonly OpenIndexModel _openIndexModel;

        public DocumentService()
        {
            _openIndexModel = App.OpenIndexModel;
        }

        public IEnumerable<DocumentInfo> GetDocumentsInfo()
        {
            for (var i = 0; i < GetNumberOfDocuments(); i++)
            {
                yield return GetDocumentInfo(i);
            }

            yield break;
        }

        public DocumentInfo GetDocumentInfo(int documentNo)
        {
            IndexReader indexReader = null;

            try
            {
                var directory = _openIndexModel.Directory;
                indexReader = IndexReader.Open(directory, _openIndexModel.ReadOnly);

                var doc = indexReader.Document(documentNo);
                var fields = doc
                    .GetFields()
                    .Cast<Field>()
                    .Select(field =>
                            new FieldByDocumentInfo
                                {
                                    Indexed = field.IsIndexed(),
                                    Tokenized = field.IsTokenized(),
                                    Stored = field.IsStored(),
                                    OmitNorms = field.GetOmitNorms(),
                                    OmitTermFrequency = field.GetOmitTermFreqAndPositions(),
                                    Lazy = field.IsLazy(),
                                    Binary = field.IsBinary(),
                                    OffsetTermVector = field.IsStoreOffsetWithTermVector(),
                                    PositionTermVector = field.IsStorePositionWithTermVector(),
                                    Field = field.Name(),
                                    Value = doc.Get(field.Name())
                                });

                return new DocumentInfo(documentNo, fields);
            }
            finally
            {
                if (indexReader != null)
                    indexReader.Close();
            }
        }

        public int GetNumberOfDocuments()
        {
            IndexReader indexReader = null;

            try
            {
                var directory = _openIndexModel.Directory;
                indexReader = IndexReader.Open(directory, _openIndexModel.ReadOnly);

                return indexReader.NumDocs(); 
            }
            finally
            {
                if (indexReader != null)
                    indexReader.Close();
            }
        }

        public IEnumerable<string> GetFields()
        {
            IndexReader indexReader = null;

            try
            {
                var directory = _openIndexModel.Directory;
                indexReader = IndexReader.Open(directory, _openIndexModel.ReadOnly);

                return indexReader.GetFieldNames(IndexReader.FieldOption.ALL);
            }
            finally
            {
                if (indexReader != null)
                    indexReader.Close();
            }
        }

        public IEnumerable<DocumentInfo> SearchDocumentsFor(TermToInspect termToInspect)
        {
            // ToDo: a bit clean up is required here.
            // It has been implementely very quickly and with a lot of deprecated methods
            IndexSearcher searcher = null;
            try
            {
                searcher = new IndexSearcher(_openIndexModel.Directory, _openIndexModel.ReadOnly);
                var queryParser = new QueryParser(termToInspect.FieldName, new StandardAnalyzer());
                var query = queryParser.Parse(termToInspect.TermName);
                var hits = searcher.Search(query);
                for (var i = 0; i < hits.Length(); i++)
                {
                    yield return GetDocumentInfo(hits.Id(i));
                }
            }
            finally
            {
                if(searcher!= null)
                    searcher.Close();
            }

            yield break;
        }
    }
}