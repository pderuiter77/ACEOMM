using ACEOMM.Services.Converter.CsvToDomain;
using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NLog;

namespace ACEOMM.Services
{
    public class ImportService
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        private string _progressText = string.Empty;
        private IEnumerable<T> ParseFile<T>(string filename, Func<string[], T> predicate)
            where T : Entity
        {
            logger.Info("Importing file {0}", filename);
            var lines = File.ReadAllLines(Path.GetFullPath(Path.Combine(@".\Data\Import\", filename)));
            var max = lines.Count();
            logger.Info("{0} lines read", max);
            var current = 0;

            var result = new List<T>();
            foreach (var line in lines)
            {
                current++;
                _progressCallback(max, current, _progressText);
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var fields = line.Split('\t');
                result.Add(predicate(fields));
            }
            logger.Info("Done importing file {0}", filename);
            return result;
        }

        private void ParseFileForModRelations(string filename, IEnumerable<Business> businesses)
        {
            logger.Info("Importing relations of file {0}", filename);
            var lines = File.ReadAllLines(Path.GetFullPath(Path.Combine(@".\Data\Import\", filename)));
            var relationConverter = new CsvToBusinessModRelationConverter(businesses, _mods);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var fields = line.Split('\t');
                relationConverter.Convert(fields);
            }
            logger.Info("Done importing relations of file {0}", filename);
        }

        private List<Business> _businesses;
        private List<Product> _products;
        private List<Livery> _liveries;
        private List<Mod> _mods;
        private IEnumerable<Country> _countries;
        private Action<int, int, string> _progressCallback;

        public ImportService(List<Business> businesses, List<Product> products, List<Mod> mods, IEnumerable<Country> countries, List<Livery> liveries, Action<int, int, string> progressCallback)
        {
            _businesses = businesses;
            _products = products;
            _mods = mods;
            _countries = countries;
            _liveries = liveries;
            _progressCallback = progressCallback;
        }

        private bool UpdateEntity(Entity existing, Entity imported)
        {
            if (existing == null)
                return false;

            if (!existing.IsEditAllowed)
                return false;

            existing.Author = imported.Author;
            existing.Description = imported.Description;
            existing.Name = imported.Name;
            existing.Notes = imported.Notes;
            existing.Version = imported.Version;

            return true;
        }

        private void UpdateLogo(Logo existing, Logo imported)
        {
            existing.RemoteUrl = imported.RemoteUrl;
        }

        private bool UpdateBusiness(Business existing, Business imported)
        {
            if (existing == null)
                _businesses.Add(imported);

            
            var result = UpdateEntity(existing, imported);
            if (!result)
                return false;

            UpdateLogo(existing.Logo, imported.Logo);
            existing.Country = imported.Country;
            existing.Class = imported.Class;
            existing.CEO = imported.CEO;
            existing.Region = imported.Region;

            return true;
        }

        private bool UpdateAirline(Airline existing, Airline imported)
        {
            var result = UpdateBusiness(existing, imported);
            if (!result)
                return false;

            existing.IATA = imported.IATA;
            existing.ICAO = imported.ICAO;

            return true;
        }

        private bool UpdateAVFuelSupplier(AviationFuelSupplier existing, AviationFuelSupplier imported)
        {
            var result = UpdateBusiness(existing, imported);
            if (!result)
                return false;

            return true;
        }

        private bool UpdateBank(Bank existing, Bank imported)
        {
            var result = UpdateBusiness(existing, imported);
            if (!result)
                return false;

            existing.BankType = imported.BankType;
            return true;
        }

        private bool UpdateContractor(Contractor existing, Contractor imported)
        {
            var result = UpdateBusiness(existing, imported);
            if (!result)
                return false;

            return true;
        }

        private bool UpdateFranchise(Franchise existing, Franchise imported)
        {
            var result = UpdateBusiness(existing, imported);
            if (!result)
                return false;

            existing.FranchiseType = imported.FranchiseType;
            if (imported.Products.Any())
                existing.Products.Clear();
            foreach (var product in imported.Products)
                existing.Products.Add(product);

            return true;
        }

        private bool UpdateProduct(Product existing, Product imported)
        {
            if (existing == null)
                _products.Add(imported);

            var result = UpdateEntity(existing, imported);
            if (!result)
                return false;

            existing.Color = imported.Color;
            existing.Price = imported.Price;
            UpdateLogo(existing.Logo, imported.Logo);

            return true;
        }

        private T FindBusiness<T>(BusinessType type, T entity, bool useName)
            where T : Business
        {
            Business result;
            if (useName)
                result = _businesses.SingleOrDefault(x => x.Type == type && x.Name == entity.Name);
            else
                result =_businesses.SingleOrDefault(x => x.Type == type && x.Id == entity.Id);

            return (T)result;
        }

        private Product FindProduct(Product entity, bool useName)
        {
            if (useName)
                return _products.SingleOrDefault(x => x.Name == entity.Name);
            else
                return _products.SingleOrDefault(x => x.Id == entity.Id);
        }

        private async Task ImportBusiness<T>(string filename, BusinessType businessType, CsvToBusinessConverter<T> converter, Func<T, T, bool> updateCallback, bool useName)
            where T : Business, new()
        {
            logger.Info("Importing {0}", businessType.ToString());
            await Task.Delay(1);
            try
            {
                _progressText = "Reading " + businessType.ToString();
                var list = ParseFile(filename, converter.Convert);
                _progressText = "Parsing " + businessType.ToString();
                var max = list.Count();
                var current = 0;
                foreach (var imported in list)
                {
                    current++;
                    _progressCallback(max, current, _progressText);
                    var existing = FindBusiness(businessType, imported, useName);
                    updateCallback(existing, imported);
                }
                ParseFileForModRelations(filename, _businesses.Where(x => x.Type == businessType).ToList());
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
            logger.Info("Done importing {0}", businessType.ToString());
        }

        public async Task ImportAirlines(string filename)
        {
            await ImportBusiness(filename, BusinessType.Airline, new CsvToAirlineConverter(_countries), UpdateAirline, CsvToAirlineConverter.IdField == -1);
        }

        public async Task ImportAVFuelSuppliers(string filename)
        {
            await ImportBusiness(filename, BusinessType.AviationFuelSupplier, new CsvToAVFuelSupplierConverter(_countries), UpdateAVFuelSupplier, CsvToAVFuelSupplierConverter.IdField == -1);
        }

        public async Task ImportBanks(string filename)
        {
            await ImportBusiness(filename, BusinessType.Bank, new CsvToBankConverter(_countries), UpdateBank, CsvToBankConverter.IdField == -1);
        }

        public async Task ImportContractors(string filename)
        {
            await ImportBusiness(filename, BusinessType.Contractor, new CsvToContractorConverter(_countries), UpdateContractor, CsvToContractorConverter.IdField == -1);
        }

        public async Task ImportFranchises(string filename)
        {
            await ImportBusiness(filename, BusinessType.Franchise, new CsvToFranchiseConverter(_countries, _products), UpdateFranchise, CsvToFranchiseConverter.IdField == -1);
        }

        public async Task ImportProducts(string filename)
        {
            logger.Info("Importing products");
            await Task.Delay(1);
            try
            {
                _progressText = "Reading Products";
                var list = ParseFile(filename, new CsvToProductConverter().Convert);
                _progressText = "Parsing Products";
                var max = list.Count();
                var current = 0;
                foreach (var imported in list)
                {
                    current++;
                    _progressCallback(max, current, _progressText);
                    var existing = FindProduct(imported, CsvToProductConverter.IdField == -1);
                    UpdateProduct(existing, imported);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
            logger.Info("Done importing products");
        }
    }
}
