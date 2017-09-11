using ACEOMM.Domain.Model;
using System.Xml;

namespace ACEOMM.Services.Converter.DomainToXml
{
    public class ModToXmlConverter : EntityToXmlConverter<Mod>
    {
        public override void Convert(XmlElement node, XmlDocument document, Mod entity)
        {
            base.Convert(node, document, entity);
            var mainNode = document.CreateElement("Businesses");
            foreach (var business in entity.Businesses)
            {
                var childNode = document.CreateElement("Business");
                childNode.SetAttribute("Id", business.Id.ToString());
                mainNode.AppendChild(childNode);
            }
            node.AppendChild(mainNode);
        }
    }
}
