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

        private static List<string> GetAircraft(Airline airline)
        {
            var result = new List<string>();
            foreach (var aircraft in airline.Liveries.Select(x => x.Aircraft).Distinct().ToList())
            {
                if (aircraft.StartsWith("A320"))
                    result.Add("A320");
                if (aircraft.StartsWith("ATR 72"))
                    result.Add("ATR72");
                if (aircraft.StartsWith("ATR 42"))
                    result.Add("ATR42");
                if (aircraft.StartsWith("737-600"))
                    result.Add("B737600");
                if (aircraft.StartsWith("737-800"))
                    result.Add("B737800");
                if (aircraft.StartsWith("CRJ700"))
                    result.Add("CRJ700");
                if (aircraft.StartsWith("Bae 146"))
                    result.Add("BAE146");

                if (aircraft.StartsWith("Cessna 182"))
                    result.Add("Cessna182");
                if (aircraft.StartsWith("Cessna 206"))
                    result.Add("Cessna206");
            }

            return result;
        }

        public static JsonAirline Convert(Airline airline)
        {
            var jsonAirline = new JsonAirline();
            Fill(jsonAirline, airline);
            jsonAirline.isCustom = true;
            jsonAirline.flightPrefix = airline.IATA;
            jsonAirline.fleet = GetAircraft(airline);
            return jsonAirline;
        }
    }
}
