using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Collections.ObjectModel;
using System.IO;
using System;

namespace ACEOMM.Services.Converter.XmlToDomain
{
    public class XmlToAirlineConverter : XmlToBusinessConverter<Airline>
    {
        public XmlToAirlineConverter(IEnumerable<Country> countries, IEnumerable<Livery> liveries)
            : base(countries)
        {
            _liveries = liveries;
        }

        private IEnumerable<Livery> _liveries;

        protected Livery LookupLivery(XmlElement node, string attributeName)
        {
            var liveryPath = Path.GetFullPath(GetAttributeValue(node, attributeName)).Replace(".", "");
            var livery = _liveries.SingleOrDefault(x => x.Path == liveryPath);
            return livery ?? Livery.UnknownLivery;
        }

        private List<Livery> ConvertLiveries(XmlElement node)
        {
            var mainNode = node.SelectSingleNode("Liveries");
            var result = new List<Livery>();
            if (mainNode == null)
                return result;
            foreach (XmlElement liveryNode in mainNode.ChildNodes)
            {
                var livery = LookupLivery(liveryNode, "Path");
                if (livery != Livery.UnknownLivery)
                    result.Add(livery);
            }
            return result;
        }

        protected override void Fill(Airline entity, XmlElement node)
        {
            base.Fill(entity, node);
            entity.IATA = GetAttributeValue(node, "IATA");
            entity.ICAO = GetAttributeValue(node, "ICAO");
            entity.Liveries = new ObservableCollection<Livery>(ConvertLiveries(node).OrderBy(x => x.Airline));

        }
    }
}
