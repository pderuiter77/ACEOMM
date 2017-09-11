using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using System;
using System.Collections.Generic;
using System.Xml;

namespace ACEOMM.Services.Converter.XmlToDomain
{
    public class XmlToBankConverter : XmlToBusinessConverter<Bank>
    {
        public XmlToBankConverter(IEnumerable<Country> countries)
            : base(countries)
        { }

        private BankType ConvertType(XmlElement node, string attributeName)
        { 
            var enumText = GetAttributeValue(node, attributeName);
            BankType parsedType;

            if (Enum.TryParse(enumText, out parsedType))
                return parsedType;

            return BankType.Unknown;
        }

        protected override void Fill(Bank entity, XmlElement node)
        {
            base.Fill(entity, node);
            entity.BankType = ConvertType(node, "Type");
        }
    }
}
