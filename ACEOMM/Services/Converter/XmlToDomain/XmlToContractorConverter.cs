using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using System.Collections.Generic;
using System.Xml;

namespace ACEOMM.Services.Converter.XmlToDomain
{
    public class XmlToContractorConverter : XmlToBusinessConverter<Contractor>
    {
        public XmlToContractorConverter(IEnumerable<Country> countries)
            : base(countries)
        { }

        protected override void Fill(Contractor entity, XmlElement node)
        {
            base.Fill(entity, node);
            // Nothing to do yet
        }
    }
}
