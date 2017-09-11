using ACEOMM.Domain.Model.Businesses;
using System.Xml;

namespace ACEOMM.Services.Converter.DomainToXml
{
    public class ProductToXmlConverter : EntityToXmlConverter<Product>
    {
        public override void Convert(XmlElement node, XmlDocument document, Product entity)
        {
            base.Convert(node, document, entity);
            node.SetAttribute("Color", entity.Color.ToString());
            node.SetAttribute("Price", entity.Price.ToString());
            node.SetAttribute("Type", entity.Type.ToString());
            node.AppendChild(ConvertLogo(document, entity.Logo));
        }
    }
}
