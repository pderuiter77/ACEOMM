using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using System.Collections.Generic;
using System.Xml;

namespace ACEOMM.Services.Converter.XmlToDomain
{
    public class XmlToAirlineConverter : XmlToBusinessConverter<Airline>
    {
        public XmlToAirlineConverter(IEnumerable<Country> countries)
            : base(countries)
        { }

        protected override void Fill(Airline entity, XmlElement node)
        {
            base.Fill(entity, node);
            entity.IATA = GetAttributeValue(node, "IATA");
            entity.ICAO = GetAttributeValue(node, "ICAO");
        }
    }
}
