﻿using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using System.Collections.Generic;

namespace ACEOMM.Services
{
    public interface IDataService
    {
        string Version { get; }

        IEnumerable<Mod> GetMods();
        IEnumerable<Business> GetBusinesses();
        IEnumerable<Product> GetProducts();
        IEnumerable<Livery> GetLiveries();
        IEnumerable<Country> GetCountries();

        void Load(string dataPath);
        void Save(string dataPath, IEnumerable<Product> products, IEnumerable<Business> businesses, IEnumerable<Mod> mods);
    }
}
