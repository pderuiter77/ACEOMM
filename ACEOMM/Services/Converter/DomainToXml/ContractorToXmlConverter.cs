using ACEOMM.Domain.Model.Businesses;
using System.Xml;

namespace ACEOMM.Services.Converter.DomainToXml
{
    public class ContractorToXmlConverter : BusinessToXmlConverter<Contractor>
    {
        public override void Convert(XmlElement node, XmlDocument document, Contractor entity)
        {
            base.Convert(node, document, entity);
        }
    }
}
