using ACEOMM.Domain.Model.Businesses;
using System;

namespace ACEOMM.Domain.Model
{
    public class Business : Entity
    {
        public Business()
        {
            Class = BusinessClass.Unknown;
            Country = new Country();
            Logo = new Logo();
        }

        private Country _country;
        public Country Country 
        {
            get { return _country; }
            set { SetProperty(ref _country, value); }
        }

        private string _ceo;
        public string CEO
        {
            get { return _ceo; }
            set { SetProperty(ref _ceo, value); }
        }

        private BusinessType _type;
        public BusinessType Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }

        private BusinessClass _class;
        public BusinessClass Class
        {
            get { return _class; }
            set { SetProperty(ref _class, value); }
        }

        private Logo _logo;
        public Logo Logo
        {
            get { return _logo; }
            set { SetProperty(ref _logo, value); }
        }

        private static Business _unknownBusiness = new Business { Id = Guid.Empty, Name = "Unknown" };
        public static Business UnknownBusiness { get { return _unknownBusiness; } }
    }
}
