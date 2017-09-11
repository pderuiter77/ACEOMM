using ACEOMM.Domain.Model.Businesses;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ACEOMM.Services.Converter.DomainToJson.Model
{
    public class JsonProduct
    {
        [JsonProperty(Order = 1)]
        public string productType { get; set;  }
        [JsonProperty(Order = 2)]
        public int pricePerUnit { get; set; }
        [JsonProperty(Order = 3)]
        public string imagePath { get; set; }
        [JsonProperty(Order = 4)]
        public List<JsonColor> availableColors { get; set; }

        [JsonIgnore]
        public string Id { get; set; }

        [JsonIgnore]
        public bool IsDefault { get; set; }

        [JsonIgnore]
        public FranchiseType Type { get; set; }

        public static JsonProduct Convert(Product product)
        {
            return new JsonProduct
            {
                productType = product.Name,
                pricePerUnit = product.Price,
                imagePath = !string.IsNullOrWhiteSpace(product.Logo.LocalFilename) ? product.Logo.LocalFilename : "NoImage.png",
                availableColors = new List<JsonColor> { JsonColor.Convert(product.Color) }
            };
        }
    }
}
