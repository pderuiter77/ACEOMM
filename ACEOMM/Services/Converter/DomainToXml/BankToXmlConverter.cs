using ACEOMM.Domain.Model.Businesses;
using System.Xml;

namespace ACEOMM.Services.Converter.DomainToXml
{
    public class BankToXmlConverter : BusinessToXmlConverter<Bank>
    {
        public override void Convert(XmlElement node, XmlDocument document, Bank entity)
        {
            base.Convert(node, document, entity);
            node.SetAttribute("Type", entity.BankType.ToString());
        }
    }
}
