using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Index;
using Luke.Net.Features.OpenIndex;

namespace Luke.Net.Features.Overview
{
    public interface ITermService
    {
        IEnumerable<FieldByTermInfo> GetFieldsAndTerms();
    }

    class TermService : ITermService
    {
        private readonly OpenIndexModel _openIndexModel;
        private List<FieldByTermInfo> _fields;

        public TermService()
        {
            // ToDo: i should change this soon
            _openIndexModel = App.OpenIndexModel;
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
                    var field = _fields.SingleOrDefault(x => x.Field == term.Field());

                    if (field != null)
                    {
                        field.Count++;
                    }
                    else
                    {
                        field = new FieldByTermInfo { Count = 1, Field = term.Field() };
                        _fields.Add(field);
                    }

                    var termInfo = new TermInfo { Term = term.Text(), FieldByTerm = field, Frequency = terms.DocFreq() };
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