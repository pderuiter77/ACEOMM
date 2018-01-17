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
                if (aircraft.StartsWith("A320", System.StringComparison.InvariantCultureIgnoreCase))
                    result.Add("A320");
                if (aircraft.StartsWith("ATR 72", System.StringComparison.InvariantCultureIgnoreCase) ||
                    aircraft.StartsWith("ATR72", System.StringComparison.InvariantCultureIgnoreCase))
                    result.Add("ATR72");
                if (aircraft.StartsWith("ATR 42", System.StringComparison.InvariantCultureIgnoreCase) ||
                    aircraft.StartsWith("ATR42", System.StringComparison.InvariantCultureIgnoreCase))
                    result.Add("ATR42");
                if (aircraft.StartsWith("737-600", System.StringComparison.InvariantCultureIgnoreCase) ||
                    aircraft.StartsWith("B737600", System.StringComparison.InvariantCultureIgnoreCase))
                    result.Add("B737600");
                if (aircraft.StartsWith("737-800", System.StringComparison.InvariantCultureIgnoreCase) ||
                    aircraft.StartsWith("B737800", System.StringComparison.InvariantCultureIgnoreCase))
                    result.Add("B737800");
                if (aircraft.StartsWith("CRJ700", System.StringComparison.InvariantCultureIgnoreCase))
                    result.Add("CRJ700");
                if (aircraft.StartsWith("Bae 146", System.StringComparison.InvariantCultureIgnoreCase) ||
                    aircraft.StartsWith("Bae146", System.StringComparison.InvariantCultureIgnoreCase))
                    result.Add("BAE146");

                if (aircraft.StartsWith("Cessna 182", System.StringComparison.InvariantCultureIgnoreCase) ||
                    aircraft.StartsWith("Cessna182", System.StringComparison.InvariantCultureIgnoreCase))
                    result.Add("CESSNA182");
                if (aircraft.StartsWith("Cessna 208", System.StringComparison.InvariantCultureIgnoreCase) ||
                    aircraft.StartsWith("Cessna208", System.StringComparison.InvariantCultureIgnoreCase))
                    result.Add("CESSNA208");
                if (aircraft.StartsWith("DHC-6", System.StringComparison.InvariantCultureIgnoreCase) ||
                    aircraft.StartsWith("DHC6", System.StringComparison.InvariantCultureIgnoreCase))
                    result.Add("DHC6");
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
            if (airline.IATA == "GA")
            {
                // Empty fields for GA
                jsonAirline.CEOName = string.Empty;
                jsonAirline.description = string.Empty;
                jsonAirline.logoPath = string.Empty;
                jsonAirline.region = "Global";
            }
                return jsonAirline;
        }
    }
}
