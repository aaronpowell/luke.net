using Luke.Net.Models;

namespace Luke.Net.Features.Documents
{
    public class FieldByDocumentInfo
    {
        public Field Field { get; set; }
        public string Value { get; set; }
        public bool PositionTermVector { get; set; }
        public bool OffsetTermVector { get; set; }
        public bool Binary { get; set; }
        public bool Lazy { get; set; }
        public bool OmitTermFrequency { get; set; }
        public bool OmitNorms { get; set; }
        public bool Stored { get; set; }
        public bool Tokenized { get; set; }
        public bool Indexed { get; set; }
    }
}