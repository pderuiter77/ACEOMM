using ACEOMM.Domain.Model.Businesses;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ACEOMM.Services.Converter.DomainToJson.Model
{
    public class JsonFranchise : JsonBusiness
    {
        [JsonProperty(Order = 7)]
        public string franchiseType { get; set; }
        [JsonProperty(Order = 8)]
        public List<string> Products { get; set; }

        public static JsonFranchise Convert(Franchise franchise)
        {
            var jsonFranchise = new JsonFranchise();
            Fill(jsonFranchise, franchise);
            jsonFranchise.franchiseType = franchise.FranchiseType.ToString();
            jsonFranchise.Products = franchise.Products.Select(x => x.Name).ToList();
            return jsonFranchise;
        }
    }
}
