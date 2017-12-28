using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ACEOMM.Services.Converter.CsvToDomain
{
    public class CsvToAirlineConverter : CsvToBusinessConverter<Airline>
    {
        public CsvToAirlineConverter(IEnumerable<Country> countries)
            : base(countries)
        { }

        protected override void Fill(Airline entity, string[] fields)
        {
            base.Fill(entity, fields);
            entity.IATA = FieldValue(fields, IATAField);
            entity.ICAO = FieldValue(fields, ICAOField);
        }

        public static int IATAField = 9;
        public static int ICAOField = 10;

        public static int FleetStartField = 12;
    }
}
