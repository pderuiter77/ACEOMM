using ACEOMM.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACEOMM.Services.Converter.CsvToDomain
{
    public class CsvToBusinessModRelationConverter : CsvToEntityConverter<Mod>
    {
        private List<Mod> _mods;
        private IEnumerable<Business> _businesses;
        public CsvToBusinessModRelationConverter(IEnumerable<Business> businesses, List<Mod> mods)
        {
            _mods = mods;
            _businesses = businesses;
        }

        private Mod FindMod(string[] fields, bool useName)
        {
            var text = FieldValue(fields, ModField);
            Mod mod = null;
            if (string.IsNullOrWhiteSpace(text))
                mod = Mod.UnknownMod;
            else
            {
                if (useName)
                    mod = _mods.SingleOrDefault(x => x.Name == text);
                else
                    mod = _mods.SingleOrDefault(x => x.Id == Guid.Parse(text));
            }
            if (mod == null)
            {
                if (useName)
                    mod = new Mod { Name = text };
                else
                    mod = new Mod { Id = Guid.Parse(text) };
                _mods.Add(mod);
            }

            return mod;
        }

        private Business FindBusiness(string[] fields, bool useName)
        {
            var text = useName ? FieldValue(fields, NameField) : FieldValue(fields, IdField);

            if (useName)
                return _businesses.First(x => x.Name == text);
            else
                return _businesses.First(x => x.Id == Guid.Parse(text));
        }

        public new void Convert(string[] fields)
        {
            var useName = IdField == -1;
            
            var mod = FindMod(fields, useName);
            var business = FindBusiness(fields, useName);

            if (!mod.Businesses.Contains(business))
                mod.Businesses.Add(business);
        }

        public static int ModField = 6;
    }
}
