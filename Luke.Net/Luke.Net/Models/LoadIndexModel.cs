using System;
using System.Collections.Generic;
using System.Windows.Input;
using Magellan.Framework;
using Luke.Net.Utilities;
using Lucene.Net.Search;
using Lucene.Net.Index;
using Magellan;

namespace Luke.Net.Models
{
    public class LoadIndexModel : ViewModel
    {
        public LoadIndexModel()
        {
            this.InspectFields = new RelayCommand<IEnumerable<FieldInfo>>(InspectFieldsExecuted);
        }

        private ActiveIndexModel activeIndex;
        public ActiveIndexModel ActiveIndex
        {
            get
            {
                return activeIndex;
            }
            set
            {
                activeIndex = value;
                this.NotifyChanged("ActiveIndex");
            }
        }

        private FormatDetails _IndexDetails;
        public FormatDetails IndexDetails
        {
            get
            {
                return _IndexDetails;
            }
            set
            {
                _IndexDetails = value;
                this.NotifyChanged("IndexDetails");
            }
        }

        private int _FieldCount;
        public int FieldCount
        {
            get
            {
                return _FieldCount;
            }
            set
            {
                _FieldCount = value;
                this.NotifyChanged("FieldCount");
            }
        }

        private long _TermCount;
        public long TermCount
        {
            get
            {
                return _TermCount;
            }
            set
            {
                _TermCount = value;
                this.NotifyChanged("TermCount");
            }
        }

        private DateTime _LastModified;
        public DateTime LastModified
        {
            get
            {
                return _LastModified;
            }
            set
            {
                _LastModified = value;
                this.NotifyChanged("LastModified");
            }
        }

        private string _Version;
        public string Version
        {
            get
            {
                return _Version;
            }
            set
            {
                _Version = value;
                this.NotifyChanged("Version");
            }
        }

        private int _DocumentCount;
        public int DocumentCount
        {
            get
            {
                return _DocumentCount;
            }
            set
            {
                _DocumentCount = value;
                this.NotifyChanged("DocumentCount");
            }
        }

        private IEnumerable<FieldInfo> fields;
        public IEnumerable<FieldInfo> Fields
        {
            get
            {
                return fields;
            }
            set
            {
                fields = value;
                this.NotifyChanged("Terms");
            }
        }

        private bool _HasDeletions;
        public bool HasDeletions
        {
            get
            {
                return _HasDeletions;
            }
            set
            {
                _HasDeletions = value;
                this.NotifyChanged("HasDeletions");
            }
        }

        private int _DeletionCount;
        public int DeletionCount
        {
            get
            {
                return _DeletionCount;
            }
            set
            {
                _DeletionCount = value;
                this.NotifyChanged("DeletionCount");
            }
        }

        private bool optimized;
        public bool Optimized
        {
            get
            {
                return optimized;
            }
            set
            {
                optimized = value;
                this.NotifyChanged("Optimized");
            }
        }

        private IEnumerable<TermInfo> terms;
        public IEnumerable<TermInfo> Terms
        {
            get
            {
                return terms;
            }
            set
            {
                terms = value;
                this.NotifyChanged("Terms");
            }
        }

        public ICommand InspectFields { get; set; }

        void InspectFieldsExecuted(IEnumerable<FieldInfo> terms)
        {
            var searcher = new IndexSearcher(ActiveIndex.Directory, true);
            
            BooleanQuery q = new BooleanQuery();
            foreach (var term in terms)
            {
                q.Add(new TermQuery(new Term(term.Field)), BooleanClause.Occur.SHOULD);
            }

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
