using ACEOMM.Domain.Model.Businesses;
using System.Xml;

namespace ACEOMM.Services.Converter.DomainToXml
{
    public class FranchiseToXmlConverter : BusinessToXmlConverter<Franchise>
    {
        public override void Convert(XmlElement node, XmlDocument document, Franchise entity)
        {
            base.Convert(node, document, entity);
            node.SetAttribute("FranchiseType", entity.FranchiseType.ToString());
            var mainNode = document.CreateElement("Products");
            foreach (var product in entity.Products)
            {
                var childNode = document.CreateElement("Product");
                childNode.SetAttribute("Id", product.Id.ToString());
                mainNode.AppendChild(childNode);
            }
            node.AppendChild(mainNode);
        }
    }
}
