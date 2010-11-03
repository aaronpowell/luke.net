using System;
using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Luke.Net.Features.OpenIndex;

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
    }
}