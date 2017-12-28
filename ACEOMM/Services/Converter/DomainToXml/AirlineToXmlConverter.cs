using ACEOMM.Domain.Model.Businesses;
using System;
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
            var mainNode = document.CreateElement("Liveries");
            foreach (var livery in entity.Liveries)
            {
                var childNode = document.CreateElement("Livery");
                var path = livery.Path;
                path = path.Replace(AppDomain.CurrentDomain.BaseDirectory, ".");
                childNode.SetAttribute("Path", path);
                mainNode.AppendChild(childNode);
            }
            node.AppendChild(mainNode);
        }
    }
}
