using ACEOMM.Domain.Model.Businesses;
using System.Xml;

namespace ACEOMM.Services.Converter.DomainToXml
{
    public class AVFuelSupplierToXmlConverter : BusinessToXmlConverter<AviationFuelSupplier>
    {
        public override void Convert(XmlElement node, XmlDocument document, AviationFuelSupplier entity)
        {
            base.Convert(node, document, entity);
        }
    }
}
