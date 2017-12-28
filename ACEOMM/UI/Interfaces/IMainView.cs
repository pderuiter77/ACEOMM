using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using System.Collections.Generic;

namespace ACEOMM.UI.Interfaces
{
    public interface IMainView : IView
    {
        bool EditBusiness(Business entity, List<Product> products, List<Country> countries, List<Livery> liveries);

        bool EditMod(Mod entity, List<Business> businesses);

        bool EditProduct(Product product, List<Franchise> franchises);
    }
}
