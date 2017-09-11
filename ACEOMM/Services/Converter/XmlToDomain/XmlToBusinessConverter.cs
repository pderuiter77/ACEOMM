using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace ACEOMM.Services.Converter.XmlToDomain
{
    public class XmlToBusinessConverter<T> : XmlToEntityConverter<T>
        where T: Business, new()
    {
        private IEnumerable<Country> _countries;

        public XmlToBusinessConverter(IEnumerable<Country> countries)
        {
            _countries = countries;
        }

        protected Country LookupCountry(XmlElement node, string attributeName)
        {
            var countryCode = GetAttributeValue(node, attributeName);
            var country = _countries.SingleOrDefault(x => x.Code == countryCode);
            return country ?? Country.UnknownCountry;
        }

        private BusinessClass ConvertClass(XmlElement node, string attributeName)
        {
            var enumText = GetAttributeValue(node, attributeName);
            BusinessClass parsedType;

            if (Enum.TryParse(enumText, out parsedType))
                return parsedType;

            return BusinessClass.Unknown;
        }

        protected override void Fill(T entity, XmlElement node)
        {
            base.Fill(entity, node);
            entity.CEO = GetAttributeValue(node, "CEO");
            entity.Country = LookupCountry(node, "Country");
            entity.Class = ConvertClass(node, "Class");
            entity.Logo = ConvertLogo(node);
        }
    }
}
