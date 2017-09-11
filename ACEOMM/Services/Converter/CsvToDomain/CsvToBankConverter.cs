using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using System;
using System.Collections.Generic;

namespace ACEOMM.Services.Converter.CsvToDomain
{
    public class CsvToBankConverter : CsvToBusinessConverter<Bank>
    {
        public CsvToBankConverter(IEnumerable<Country> countries)
            : base(countries)
        { }

        private BankType ConvertType(string[] fields, int field)
        {
            var enumText = FieldValue(fields, field);
            BankType parsedType;

            if (Enum.TryParse(enumText, out parsedType))
                return parsedType;

            return BankType.Unknown;
        }

        protected override void Fill(Bank entity, string[] fields)
        {
            base.Fill(entity, fields);
            entity.BankType = ConvertType(fields, TypeField);
        }

        public static int TypeField = 9;
    }
}
