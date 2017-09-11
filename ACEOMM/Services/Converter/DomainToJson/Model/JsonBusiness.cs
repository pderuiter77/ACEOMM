using ACEOMM.Domain.Model;
using Newtonsoft.Json;

namespace ACEOMM.Services.Converter.DomainToJson.Model
{
    public class JsonBusiness
    {
        [JsonProperty(Order = 1)]
        public string name { get; set; }
        [JsonProperty(Order = 2)]
        public string description { get; set; }
        [JsonProperty(Order = 3)]
        public string logoPath { get; set; }
        [JsonProperty(Order = 4)]
        public int businessClass { get; set; }
        [JsonProperty(Order = 5)]
        public int businessType { get; set; }
        [JsonProperty(Order = 6)]
        public string CEOName { get; set; }

        [JsonIgnore]
        public bool IsDefault { get; set; }

        [JsonIgnore]
        public string Id { get; set; }

        protected static void Fill(JsonBusiness jsonBusiness, Business business)
        {
            jsonBusiness.name = business.Name;
            jsonBusiness.description = business.Description;
            jsonBusiness.logoPath = !string.IsNullOrWhiteSpace(business.Logo.LocalFilename) ? business.Logo.LocalFilename : "NoImage.png";
            jsonBusiness.businessClass = (int)business.Class;
            jsonBusiness.businessType = (int)business.Type;
            jsonBusiness.CEOName = business.CEO;
        }

        public static JsonBusiness Convert(Business business)
        {
            var jsonBusiness = new JsonBusiness();
            Fill(jsonBusiness, business);
            return jsonBusiness;
        }
    }
}
