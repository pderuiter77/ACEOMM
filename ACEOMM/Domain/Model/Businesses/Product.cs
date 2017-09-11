using System;
using System.Windows.Media;

namespace ACEOMM.Domain.Model.Businesses
{
    public class Product : Entity
    {
        public Product()
        {
            Color = Colors.White;
            Price = 0;
            Type = FranchiseType.Unknown;
            Logo = new Logo();
        }

        private FranchiseType _type;
        public FranchiseType Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }

        private Color _color;
        public Color Color
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }

        private int _price;
        public int Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }

        private Logo _logo;
        public Logo Logo
        {
            get { return _logo; }
            set { SetProperty(ref _logo, value); }
        }

        private static Product _unknownProduct = new Product { Id = Guid.Empty, Name = "Unknown" };

        public static Product UnknownProduct { get { return _unknownProduct; } }
    }
}
