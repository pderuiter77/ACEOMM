using ACEOMM.Domain.Model;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Collections.ObjectModel;

namespace ACEOMM.Services.Converter.XmlToDomain
{
    public class XmlToModConverter : XmlToEntityConverter<Mod>
    {
        private IEnumerable<Business> _businesses;

        public XmlToModConverter(IEnumerable<Business> businesses)
        {
            _businesses = businesses;
        }

        protected Business LookupBusiness(XmlElement node, string attributeName)
        {
            var businessId = ConvertGuid(node, attributeName);
            var business = _businesses.SingleOrDefault(x => x.Id == businessId);
            return business ?? Business.UnknownBusiness;
        }

        private List<Business> ConvertBusinesses(XmlElement node)
        {
            var mainNode = node.SelectSingleNode("Businesses");
            var result = new List<Business>();
            foreach (XmlElement businessNode in mainNode.ChildNodes)
            {
                var business = LookupBusiness(businessNode, "Id");
                result.Add(business);
            }
            return result;
        }

        protected override void Fill(Mod entity, XmlElement node)
        {
            base.Fill(entity, node);
            entity.Businesses = new ObservableCollection<Business>(ConvertBusinesses(node).OrderBy(x => x.Name));
        }
    }
}
