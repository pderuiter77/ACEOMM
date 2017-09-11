using System;

namespace ACEOMM.Domain.Model
{
    public class Country : IComparable<Country>, IComparable
    {
        public Country()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        private static Country _unknownCountry = new Country { Code = "ZZ", Name = "Unknown" };

        public static Country UnknownCountry 
        {
            get { return _unknownCountry; }
        }

        #region IComparable<>
        public int CompareTo(Country other)
        {
            if (other == null)
                return 0;
            if (Name == null)
                return 0;
            if (other.Name == null)
                return 0;
            return Name.CompareTo(other.Name);
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as Country);
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }
    }
}
