using System;
using Luke.Net.Infrastructure.Utilities;

namespace Luke.Net.Features.Overview
{
    public class IndexInfo
    {
        public FormatDetails IndexDetails { get; set; }

        public int FieldCount { get; set; }

        public long TermCount { get; set; }

        public DateTime LastModified { get; set; }

        public string Version { get; set; }

        public int DocumentCount { get; set; }

        public bool HasDeletions { get; set; }

        public int DeletionCount { get; set; }

        public bool Optimized { get; set; }

        public string IndexPath { get; set; }
    }
}
