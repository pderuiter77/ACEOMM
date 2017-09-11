using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACEOMM.Services.Converter.CsvToDomain
{
    public class CsvToBusinessConverter<T> : CsvToEntityConverter<T>
        where T : Business, new()
    {
        private IEnumerable<Country> _countries;

        public CsvToBusinessConverter(IEnumerable<Country> countries)
        {
            _countries = countries;
        }

        protected Country LookupCountry(string[] fields, int field)
        {
            var countryName = FieldValue(fields, field);
            var country = _countries.SingleOrDefault(x => x.Name == countryName);
            return country ?? Country.UnknownCountry;
        }

        private BusinessClass ConvertClass(string[] fields, int field)
        {
            var enumText = FieldValue(fields, field);
            BusinessClass parsedType;

            if (Enum.TryParse(enumText, out parsedType))
                return parsedType;

            return BusinessClass.Unknown;
        }
        
        protected override void Fill(T entity, string[] fields)
        {
            base.Fill(entity, fields);
            entity.CEO = FieldValue(fields, CEOField);
            entity.Class = ConvertClass(fields, ClassField);
            entity.Country = LookupCountry(fields, CountryField);
            entity.Logo = ConvertLogo(fields, LogoField);
        }

        public static int CEOField = 3;
        public static int ClassField = 4;
        public static int CountryField = 1;
        public static int LogoField = 8;
    }
}
