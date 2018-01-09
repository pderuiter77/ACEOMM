using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACEOMM.Services.Converter.JsonToDomain
{
    public class LiveryIdentification
    {
        [JsonProperty(Order = 1)]
        public string airline { get; set; }

        [JsonProperty(Order = 2)]
        public string aircraft { get; set; }

        [JsonProperty(Order = 3)]
        public string author { get; set; }
    }
}
