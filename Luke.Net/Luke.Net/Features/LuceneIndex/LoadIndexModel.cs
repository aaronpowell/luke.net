using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Luke.Net.Features.Popup;
using Luke.Net.Utilities;
using Lucene.Net.Search;
using Lucene.Net.Index;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace Luke.Net.Features.LuceneIndex
{
    public class LoadIndexModel : NotificationObject
    {
        // ToDo: this should go ASAP
        public LoadIndexModel() : this(App.EventAggregator)
        {
        }

        public LoadIndexModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<IndexChangedEvent>().Subscribe(LoadIndex);
            InspectFields = new DelegateCommand<IEnumerable<FieldInfo>>(InspectFieldsExecuted);
        }

        private ActiveIndexModel _activeIndex;
        public ActiveIndexModel ActiveIndex
        {
            get
            {
                return _activeIndex;
            }
            set
            {
                _activeIndex = value;
                RaisePropertyChanged(() => ActiveIndex);
            }
        }

        private FormatDetails _indexDetails;
        public FormatDetails IndexDetails
        {
            get
            {
                return _indexDetails;
            }
            set
            {
                _indexDetails = value;
                RaisePropertyChanged(() => IndexDetails);
            }
        }

        private int _fieldCount;
        public int FieldCount
        {
            get
            {
                return _fieldCount;
            }
            set
            {
                _fieldCount = value;
                RaisePropertyChanged(() => FieldCount);
            }
        }

        private long _termCount;
        public long TermCount
        {
            get
            {
                return _termCount;
            }
            set
            {
                _termCount = value;
                RaisePropertyChanged(() => TermCount);
            }
        }

        private DateTime _lastModified;
        public DateTime LastModified
        {
            get
            {
                return _lastModified;
            }
            set
            {
                _lastModified = value;
                RaisePropertyChanged(() => LastModified);
            }
        }

        private string _version;
        public string Version
        {
            get
            {
                return _version;
            }
            set
            {
                _version = value;
                RaisePropertyChanged(() => Version);
            }
        }

        private int _documentCount;
        public int DocumentCount
        {
            get
            {
                return _documentCount;
            }
            set
            {
                _documentCount = value;
                RaisePropertyChanged(() => DocumentCount);
            }
        }

        private IEnumerable<FieldInfo> _fields;
        public IEnumerable<FieldInfo> Fields
        {
            get
            {
                return _fields;
            }
            set
            {
                _fields = value;
                RaisePropertyChanged(() => Terms);
            }
        }

        private bool _hasDeletions;
        public bool HasDeletions
        {
            get
            {
                return _hasDeletions;
            }
            set
            {
                _hasDeletions = value;
                RaisePropertyChanged(() => HasDeletions);
            }
        }

        private int _deletionCount;
        public int DeletionCount
        {
            get
            {
                return _deletionCount;
            }
            set
            {
                _deletionCount = value;
                RaisePropertyChanged(() => DeletionCount);
            }
        }

        private bool _optimized;
        public bool Optimized
        {
            get
            {
                return _optimized;
            }
            set
            {
                _optimized = value;
                RaisePropertyChanged(() => Optimized);
            }
        }

        private IEnumerable<TermInfo> _terms;
        public IEnumerable<TermInfo> Terms
        {
            get
            {
                if (_terms == null)
                    return new TermInfo[] { }; // ToDo: just a quick hack. should be fixed

                return _terms.Take(50);
            }
            set
            {
                _terms = value;
                RaisePropertyChanged(() => Terms);
            }
        }

        public ICommand InspectFields { get; set; }

        void InspectFieldsExecuted(IEnumerable<FieldInfo> terms)
        {
            var searcher = new IndexSearcher(ActiveIndex.Directory, true);
            
            var q = new BooleanQuery();
            foreach (var term in terms)
            {
                q.Add(new TermQuery(new Term(term.Field)), BooleanClause.Occur.SHOULD);
            }
        }

        public void LoadIndex(ActiveIndexModel indexInfo)
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
            Terms = termCounter.OrderByDescending(x => x.Frequency).Select((x, i) =>
                                                                               {
                                                                                   x.Rank = i + 1;
                                                                                   return x;
                                                                               });
            ActiveIndex = indexInfo;
        }


    }

    public class FieldInfo
    {
        public string Field { get; set; }
        public int Count { get; set; }
        public double Frequency { get; set; }
    }

    public class TermInfo
    {
        public int Rank { get; set; }
        public string Field { get; set; }
        public string Term { get; set; }
        public int Frequency { get; set; }
    }
}
