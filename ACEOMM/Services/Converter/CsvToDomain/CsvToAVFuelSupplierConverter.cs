using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using System.Collections.Generic;

namespace ACEOMM.Services.Converter.CsvToDomain
{
    public class CsvToAVFuelSupplierConverter : CsvToBusinessConverter<AviationFuelSupplier>
    {
        public CsvToAVFuelSupplierConverter(IEnumerable<Country> countries)
            : base(countries)
        { }

        protected override void Fill(AviationFuelSupplier entity, string[] fields)
        {
            base.Fill(entity, fields);
        }
    }
}
