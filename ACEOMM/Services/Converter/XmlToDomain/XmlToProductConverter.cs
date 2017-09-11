using ACEOMM.Domain.Model.Businesses;
using System.Xml;

namespace ACEOMM.Services.Converter.XmlToDomain
{
    public class XmlToProductConverter : XmlToEntityConverter<Product>
    {
        protected override void Fill(Product entity, XmlElement node)
        {
            base.Fill(entity, node);
            entity.Color = ConvertColor(node, "Color");
            entity.Logo = ConvertLogo(node);
            entity.Price = ConvertInt(node, "Price");
            entity.Type = ConvertFranchiseType(node, "Type");
        }
    }
}
