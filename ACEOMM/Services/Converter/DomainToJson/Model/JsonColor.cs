using Newtonsoft.Json;
using System.Windows.Media;

namespace ACEOMM.Services.Converter.DomainToJson.Model
{
    public class JsonColor
    {
        [JsonProperty(Order = 1)]
        public int r { get; set; }
        [JsonProperty(Order = 2)]
        public int g { get; set; }
        [JsonProperty(Order = 3)]
        public int b { get; set; }
        [JsonProperty(Order = 4)]
        public int a { get; set; }

        public static JsonColor Convert(Color color)
        {
            return new JsonColor
            {
                r = color.R,
                g = color.G,
                b = color.B,
                a = color.A
            };
        }
    }
}
