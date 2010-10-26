using Lucene.Net.Index;
using Lucene.Net.Store;

namespace Luke.Net.Infrastructure.Utilities
{
    static class IndexGate
    {
        public static int GetIndexFormat(Directory dir)
        {
            var fsf = new SegmentsFile(dir);
            return (int)fsf.Run();
        }

        public static FormatDetails GetFormatDetails(int format)
        {
            FormatDetails res = new FormatDetails();
            switch (format)
            {
                case SegmentInfos.FORMAT:
                    res.Capabilities = "old plain";
                    res.GenericName = "Lucene Pre-2.1";
                    break;
                case SegmentInfos.FORMAT_LOCKLESS:
                    res.Capabilities = "lock-less";
                    res.GenericName = "Lucene 2.1";
                    break;
                case SegmentInfos.FORMAT_SINGLE_NORM_FILE:
                    res.Capabilities = "lock-less, single norms file";
                    res.GenericName = "Lucene 2.2";
                    break;
                case SegmentInfos.FORMAT_SHARED_DOC_STORE:
                    res.Capabilities = "lock-less, single norms file, shared doc store";
                    res.GenericName = "Lucene 2.3";
                    break;
                case SegmentInfos.FORMAT_CHECKSUM:
                    res.Capabilities = "lock-less, single norms, shared doc store, checksum";
                    res.GenericName = "Lucene 2.4";
                    break;
                case SegmentInfos.FORMAT_DEL_COUNT:
                    res.Capabilities = "lock-less, single norms, shared doc store, checksum, del count";
                    res.GenericName = "Lucene 2.4";
                    break;
                case SegmentInfos.FORMAT_HAS_PROX:
                    res.Capabilities = "lock-less, single norms, shared doc store, checksum, del count, omitTf";
                    res.GenericName = "Lucene 2.4";
                    break;
                case SegmentInfos.FORMAT_USER_DATA:
                    res.Capabilities = "lock-less, single norms, shared doc store, checksum, del count, omitTf, user data";
                    res.GenericName = "Lucene 2.9-dev";
                    break;
                case SegmentInfos.FORMAT_DIAGNOSTICS:
                    res.Capabilities = "lock-less, single norms, shared doc store, checksum, del count, omitTf, user data, diagnostics";
                    res.GenericName = "Lucene 2.9";
                    break;
                default:
                    res.Capabilities = "unknown";
                    res.GenericName = "Lucene 1.3 or prior";
                    break;
            }
           
            return res;
        }

        class SegmentsFile : SegmentInfos.FindSegmentsFile
        {
            private Directory dir;
            public SegmentsFile(Directory dir)
                : base(dir)
            {
                this.dir = dir;
            }

            public override object DoBody(string segmentFileName)
            {
                IndexInput i = dir.OpenInput(segmentFileName);
                var indexFormat = i.ReadInt();
                i.Close();
                return indexFormat;
            }
        }
    }

    public class FormatDetails
    {
        public string Capabilities { get; set; }
        public string GenericName { get; set; }
    }
}
