namespace Luke.Net.Models
{
    public class TermInfo
    {
        public int Rank { get; set; }
        public FieldByTermInfo Field { get; set; }
        public string Term { get; set; }
        public int Frequency { get; set; }
    }
}