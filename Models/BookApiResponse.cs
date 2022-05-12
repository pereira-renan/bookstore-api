using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class BookDataResponse
    {
        [JsonPropertyName("operationType")]
        public string operationType { get; set; }

        [JsonPropertyName("booksAdded")]
        public int totalResult { get; set; }

        [JsonPropertyName("items")]
        public List<BookData> Results { get; set; }
    }
}
