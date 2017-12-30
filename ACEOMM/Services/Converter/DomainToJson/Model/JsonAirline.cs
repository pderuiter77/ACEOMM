using ACEOMM.Domain.Model.Businesses;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ACEOMM.Services.Converter.DomainToJson.Model
{
    public class JsonAirline : JsonBusiness
    {
        [JsonProperty(Order = 7)]
        public string flightPrefix { get; set; }
        [JsonProperty(Order = 8)]
        public List<string> fleet { get; set; }
        [JsonProperty(Order = 9)]
        public bool isCustom { get; set; }

        public static JsonAirline Convert(Airline airline)
        {
            var jsonAirline = new JsonAirline();
            Fill(jsonAirline, airline);
            jsonAirline.isCustom = true;
            jsonAirline.flightPrefix = airline.IATA;
            jsonAirline.fleet = airline.Liveries.Select(x => x.Name).ToList();
            return jsonAirline;
        }
    }
}
