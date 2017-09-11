using ACEOMM.Domain.Model;
using System.Xml;

namespace ACEOMM.Services.Converter.DomainToXml
{
    public class BusinessToXmlConverter<T> : EntityToXmlConverter<T>
        where T : Business
    {
        public override void Convert(XmlElement node, XmlDocument document, T entity)
        {
            base.Convert(node, document, entity);
            node.SetAttribute("CEO", entity.CEO);
            node.SetAttribute("Country", entity.Country.Code);
            node.SetAttribute("Class", entity.Class.ToString());
            node.AppendChild(ConvertLogo(document, entity.Logo));
        }
    }
}
