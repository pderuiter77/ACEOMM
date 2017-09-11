using ACEOMM.Domain.Model.Businesses;
using System.Xml;

namespace ACEOMM.Services.Converter.DomainToXml
{
    public class AirlineToXmlConverter : BusinessToXmlConverter<Airline>
    {
        public override void Convert(XmlElement node, XmlDocument document, Airline entity)
        {
            base.Convert(node, document, entity);
            node.SetAttribute("IATA", entity.IATA);
            node.SetAttribute("ICAO", entity.ICAO);
        }
    }
}
