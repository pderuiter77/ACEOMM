using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using System.Collections.Generic;
using System.Xml;

namespace ACEOMM.Services.Converter.XmlToDomain
{
    public class XmlToAVFuelSupplierConverter : XmlToBusinessConverter<AviationFuelSupplier>
    {
        public XmlToAVFuelSupplierConverter(IEnumerable<Country> countries)
            : base(countries)
        { }

        protected override void Fill(AviationFuelSupplier entity, XmlElement node)
        {
            base.Fill(entity, node);
            // Nothing to do yet
        }
    }
}
