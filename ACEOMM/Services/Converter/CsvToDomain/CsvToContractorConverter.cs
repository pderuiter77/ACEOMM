using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using System.Collections.Generic;

namespace ACEOMM.Services.Converter.CsvToDomain
{
    public class CsvToContractorConverter : CsvToBusinessConverter<Contractor>
    {
        public CsvToContractorConverter(IEnumerable<Country> countries)
            : base(countries)
        { }

        protected override void Fill(Contractor entity, string[] fields)
        {
            base.Fill(entity, fields);
        }
    }
}
