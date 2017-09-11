using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Collections.ObjectModel;

namespace ACEOMM.Services.Converter.XmlToDomain
{
    public class XmlToFranchiseConverter : XmlToBusinessConverter<Franchise>
    {
        private IEnumerable<Product> _products;

        public XmlToFranchiseConverter(IEnumerable<Country> countries, IEnumerable<Product> products)
            : base(countries)
        { 
            _products = products;
        }

        protected Product LookupProduct(XmlElement node, string attributeName)
        {
            var productId = ConvertGuid(node, attributeName);
            var product = _products.SingleOrDefault(x => x.Id == productId);
            return product ?? Product.UnknownProduct;
        }


        private List<Product> ConvertProducts(XmlElement node)
        {
            var mainNode = node.SelectSingleNode("Products");
            var result = new List<Product>();
            foreach (XmlElement productNode in mainNode.ChildNodes)
            {
                var product = LookupProduct(productNode, "Id");
                result.Add(product);
            }
            return result;
        }

        protected override void Fill(Franchise entity, XmlElement node)
        {
            base.Fill(entity, node);
            entity.FranchiseType = ConvertFranchiseType(node, "FranchiseType");
            entity.Products = new ObservableCollection<Product>(ConvertProducts(node).OrderBy(x => x.Name));
        }
    }
}
