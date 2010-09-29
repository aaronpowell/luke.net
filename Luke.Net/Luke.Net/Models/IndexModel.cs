
using System.ComponentModel;
namespace Luke.Net.Models
{
    public class IndexModel
    {
        [DisplayName("Path")]
        public string IndexPath { get; set; }

        [DisplayName("Read-only Index")]
        public bool ReadOnly { get; set; }
    }
}
