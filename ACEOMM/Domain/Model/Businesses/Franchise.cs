using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ACEOMM.Domain.Model.Businesses
{
    public class Franchise : Business
    {
        public Franchise()
        {
            Products = new ObservableCollection<Product>();
            FranchiseType = FranchiseType.Unknown;
            Type = BusinessType.Franchise;
        }

        public ObservableCollection<Product> Products { get; set; }

        private FranchiseType _franchiseType;
        public FranchiseType FranchiseType
        {
            get { return _franchiseType; }
            set { SetProperty(ref _franchiseType, value); }
        }
    }
}
