using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ACEOMM.Services.Converter.CsvToDomain
{
    public class CsvToFranchiseConverter : CsvToBusinessConverter<Franchise>
    {
        private IEnumerable<Product> _products;

        public CsvToFranchiseConverter(IEnumerable<Country> countries, IEnumerable<Product> products)
            : base(countries)
        {
            _products = products;
        }

        protected Product LookupProduct(string name)
        {
            var product = _products.SingleOrDefault(x => x.Name == name);
            return product ?? Product.UnknownProduct;
        }

        private List<Product> ConvertProducts(string[] fields, int startField)
        {
            var result = new List<Product>();
            for (var i = startField; i < fields.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(fields[i]))
                    continue;
                var useCategory = fields[i].Contains(" - ");
                var product = LookupProduct(useCategory ? fields[i].Substring(fields[i].IndexOf(" - ") + 3) : fields[i]);
                result.Add(product);
            }
            return result;
        }

        protected override void Fill(Franchise entity, string[] fields)
        {
            base.Fill(entity, fields);
            entity.FranchiseType = ConvertFranchiseType(fields, TypeField);
            entity.Products = new ObservableCollection<Product>(ConvertProducts(fields, ProductStartField));
        }

        public static int TypeField = 9;
        public static int ProductStartField = 10;
    }
}
