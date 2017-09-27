using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using ACEOMM.Services.Converter.XmlToDomain;
using ACEOMM.Services.Converter.DomainToXml;
using NLog;

namespace ACEOMM.Services
{
    public class XmlDataService : IDataService
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        private const string DataFileName = "Datastore.xml";

        private List<Mod> _mods = new List<Mod>();
        private List<Business> _businesses = new List<Business>();
        private List<Product> _products = new List<Product>();
        private List<Country> _countries = new List<Country>();

        private void AssertIsDataLoaded()
        { 

        }

        public IEnumerable<Mod> GetMods()
        {
            AssertIsDataLoaded();
            return _mods;
        }

        public IEnumerable<Business> GetBusinesses()
        {
            AssertIsDataLoaded();
            return _businesses;
        }

        public IEnumerable<Product> GetProducts()
        {
            AssertIsDataLoaded();
            return _products;
        }

        public IEnumerable<Country> GetCountries()
        {
            AssertIsDataLoaded();
            return _countries;
        }

        public string Version { get; private set; }

        private void LoadCountries()
        {
            logger.Info("Loading countries");
            _countries.Add(new Country { Code = "AD", Name = "Andorra" });
            _countries.Add(new Country { Code = "AE", Name = "United Arab Emirates" });
            _countries.Add(new Country { Code = "AF", Name = "Afghanistan" });
            _countries.Add(new Country { Code = "AG", Name = "Antigua & Barbuda" });
            _countries.Add(new Country { Code = "AI", Name = "Anguilla" });
            _countries.Add(new Country { Code = "AL", Name = "Albania" });
            _countries.Add(new Country { Code = "AM", Name = "Armenia" });
            _countries.Add(new Country { Code = "AO", Name = "Angola" });
            _countries.Add(new Country { Code = "AQ", Name = "Antarctica" });
            _countries.Add(new Country { Code = "AR", Name = "Argentina" });
            _countries.Add(new Country { Code = "AS", Name = "American Samoa" });
            _countries.Add(new Country { Code = "AT", Name = "Austria" });
            _countries.Add(new Country { Code = "AU", Name = "Australia" });
            _countries.Add(new Country { Code = "AW", Name = "Aruba" });
            _countries.Add(new Country { Code = "AX", Name = "Åland Islands" });
            _countries.Add(new Country { Code = "AZ", Name = "Azerbaijan" });
            _countries.Add(new Country { Code = "BA", Name = "Bosnia and Herzegovina" });
            _countries.Add(new Country { Code = "BB", Name = "Barbados" });
            _countries.Add(new Country { Code = "BD", Name = "Bangladesh" });
            _countries.Add(new Country { Code = "BE", Name = "Belgium" });
            _countries.Add(new Country { Code = "BF", Name = "Burkina Faso" });
            _countries.Add(new Country { Code = "BG", Name = "Bulgaria" });
            _countries.Add(new Country { Code = "BH", Name = "Bahrain" });
            _countries.Add(new Country { Code = "BI", Name = "Burundi" });
            _countries.Add(new Country { Code = "BJ", Name = "Benin" });
            _countries.Add(new Country { Code = "BL", Name = "Saint Barthélemy" });
            _countries.Add(new Country { Code = "BM", Name = "Bermuda" });
            _countries.Add(new Country { Code = "BN", Name = "Brunei Darussalam" });
            _countries.Add(new Country { Code = "BO", Name = "Bolivia" });
            _countries.Add(new Country { Code = "BQ", Name = "Bonaire, Sint Eustatius and Saba" });
            _countries.Add(new Country { Code = "BR", Name = "Brazil" });
            _countries.Add(new Country { Code = "BS", Name = "Bahamas" });
            _countries.Add(new Country { Code = "BT", Name = "Bhutan" });
            _countries.Add(new Country { Code = "BV", Name = "Bouvet Island" });
            _countries.Add(new Country { Code = "BW", Name = "Botswana" });
            _countries.Add(new Country { Code = "BY", Name = "Belarus" });
            _countries.Add(new Country { Code = "BZ", Name = "Belize" });
            _countries.Add(new Country { Code = "CA", Name = "Canada" });
            _countries.Add(new Country { Code = "CC", Name = "Cocos (Keeling) Islands" });
            _countries.Add(new Country { Code = "CD", Name = "Congo" });
            _countries.Add(new Country { Code = "CF", Name = "Central African Republic" });
            _countries.Add(new Country { Code = "CG", Name = "Congo" });
            _countries.Add(new Country { Code = "CH", Name = "Switzerland" });
            _countries.Add(new Country { Code = "CI", Name = "Côte d'Ivoire" });
            _countries.Add(new Country { Code = "CK", Name = "Cook Islands" });
            _countries.Add(new Country { Code = "CL", Name = "Chile" });
            _countries.Add(new Country { Code = "CM", Name = "Cameroon" });
            _countries.Add(new Country { Code = "CN", Name = "China" });
            _countries.Add(new Country { Code = "CO", Name = "Colombia" });
            _countries.Add(new Country { Code = "CR", Name = "Costa Rica" });
            _countries.Add(new Country { Code = "CU", Name = "Cuba" });
            _countries.Add(new Country { Code = "CV", Name = "Cabo Verde" });
            _countries.Add(new Country { Code = "CW", Name = "Curaçao" });
            _countries.Add(new Country { Code = "CX", Name = "Christmas Island" });
            _countries.Add(new Country { Code = "CY", Name = "Cyprus" });
            _countries.Add(new Country { Code = "CZ", Name = "Czechia" });
            _countries.Add(new Country { Code = "DE", Name = "Germany" });
            _countries.Add(new Country { Code = "DJ", Name = "Djibouti" });
            _countries.Add(new Country { Code = "DK", Name = "Denmark" });
            _countries.Add(new Country { Code = "DM", Name = "Dominica" });
            _countries.Add(new Country { Code = "DO", Name = "Dominican Republic" });
            _countries.Add(new Country { Code = "DZ", Name = "Algeria" });
            _countries.Add(new Country { Code = "EC", Name = "Ecuador" });
            _countries.Add(new Country { Code = "EE", Name = "Estonia" });
            _countries.Add(new Country { Code = "EG", Name = "Egypt" });
            _countries.Add(new Country { Code = "EH", Name = "Western Sahara" });
            _countries.Add(new Country { Code = "ER", Name = "Eritrea" });
            _countries.Add(new Country { Code = "ES", Name = "Spain" });
            _countries.Add(new Country { Code = "ET", Name = "Ethiopia" });
            _countries.Add(new Country { Code = "FI", Name = "Finland" });
            _countries.Add(new Country { Code = "FJ", Name = "Fiji" });
            _countries.Add(new Country { Code = "FK", Name = "Falkland Islands" });
            _countries.Add(new Country { Code = "FM", Name = "Micronesia" });
            _countries.Add(new Country { Code = "FO", Name = "Faroe Islands" });
            _countries.Add(new Country { Code = "FR", Name = "France" });
            _countries.Add(new Country { Code = "GA", Name = "Gabon" });
            _countries.Add(new Country { Code = "GB", Name = "United Kingdom of Great Britain and Northern Ireland" });
            _countries.Add(new Country { Code = "GD", Name = "Grenada" });
            _countries.Add(new Country { Code = "GE", Name = "Georgia" });
            _countries.Add(new Country { Code = "GF", Name = "French Guiana" });
            _countries.Add(new Country { Code = "GG", Name = "Guernsey" });
            _countries.Add(new Country { Code = "GH", Name = "Ghana" });
            _countries.Add(new Country { Code = "GI", Name = "Gibraltar" });
            _countries.Add(new Country { Code = "GL", Name = "Greenland" });
            _countries.Add(new Country { Code = "GM", Name = "Gambia" });
            _countries.Add(new Country { Code = "GN", Name = "Guinea" });
            _countries.Add(new Country { Code = "GP", Name = "Guadeloupe" });
            _countries.Add(new Country { Code = "GQ", Name = "Equatorial Guinea" });
            _countries.Add(new Country { Code = "GR", Name = "Greece" });
            _countries.Add(new Country { Code = "GS", Name = "South Georgia and the South Sandwich Islands" });
            _countries.Add(new Country { Code = "GT", Name = "Guatemala" });
            _countries.Add(new Country { Code = "GU", Name = "Guam" });
            _countries.Add(new Country { Code = "GW", Name = "Guinea -Bissau" });
            _countries.Add(new Country { Code = "GY", Name = "Guyana" });
            _countries.Add(new Country { Code = "HK", Name = "Hong Kong" });
            _countries.Add(new Country { Code = "HM", Name = "Heard Island and McDonald Islands" });
            _countries.Add(new Country { Code = "HN", Name = "Honduras" });
            _countries.Add(new Country { Code = "HR", Name = "Croatia" });
            _countries.Add(new Country { Code = "HT", Name = "Haiti" });
            _countries.Add(new Country { Code = "HU", Name = "Hungary" });
            _countries.Add(new Country { Code = "ID", Name = "Indonesia" });
            _countries.Add(new Country { Code = "IE", Name = "Ireland" });
            _countries.Add(new Country { Code = "IL", Name = "Israel" });
            _countries.Add(new Country { Code = "IM", Name = "Isle of Man" });
            _countries.Add(new Country { Code = "IN", Name = "India" });
            _countries.Add(new Country { Code = "IO", Name = "British Indian Ocean Territory" });
            _countries.Add(new Country { Code = "IQ", Name = "Iraq" });
            _countries.Add(new Country { Code = "IR", Name = "Iran" });
            _countries.Add(new Country { Code = "IS", Name = "Iceland" });
            _countries.Add(new Country { Code = "IT", Name = "Italy" });
            _countries.Add(new Country { Code = "JE", Name = "Jersey" });
            _countries.Add(new Country { Code = "JM", Name = "Jamaica" });
            _countries.Add(new Country { Code = "JO", Name = "Jordan" });
            _countries.Add(new Country { Code = "JP", Name = "Japan" });
            _countries.Add(new Country { Code = "KE", Name = "Kenya" });
            _countries.Add(new Country { Code = "KG", Name = "Kyrgyzstan" });
            _countries.Add(new Country { Code = "KH", Name = "Cambodia" });
            _countries.Add(new Country { Code = "KI", Name = "Kiribati" });
            _countries.Add(new Country { Code = "KM", Name = "Comoros" });
            _countries.Add(new Country { Code = "KN", Name = "Saint Kitts and Nevis" });
            _countries.Add(new Country { Code = "KP", Name = "North Korea" });
            _countries.Add(new Country { Code = "KR", Name = "South Korea" });
            _countries.Add(new Country { Code = "KW", Name = "Kuwait" });
            _countries.Add(new Country { Code = "KY", Name = "Cayman Islands" });
            _countries.Add(new Country { Code = "KZ", Name = "Kazakhstan" });
            _countries.Add(new Country { Code = "LA", Name = "Laos" });
            _countries.Add(new Country { Code = "LB", Name = "Lebanon" });
            _countries.Add(new Country { Code = "LC", Name = "Saint Lucia" });
            _countries.Add(new Country { Code = "LI", Name = "Liechtenstein" });
            _countries.Add(new Country { Code = "LK", Name = "Sri Lanka" });
            _countries.Add(new Country { Code = "LR", Name = "Liberia" });
            _countries.Add(new Country { Code = "LS", Name = "Lesotho" });
            _countries.Add(new Country { Code = "LT", Name = "Lithuania" });
            _countries.Add(new Country { Code = "LU", Name = "Luxembourg" });
            _countries.Add(new Country { Code = "LV", Name = "Latvia" });
            _countries.Add(new Country { Code = "LY", Name = "Libya" });
            _countries.Add(new Country { Code = "MA", Name = "Morocco" });
            _countries.Add(new Country { Code = "MC", Name = "Monaco" });
            _countries.Add(new Country { Code = "MD", Name = "Moldova" });
            _countries.Add(new Country { Code = "ME", Name = "Montenegro" });
            _countries.Add(new Country { Code = "MF", Name = "Saint Martin (French part)" });
            _countries.Add(new Country { Code = "MG", Name = "Madagascar" });
            _countries.Add(new Country { Code = "MH", Name = "Marshall Islands" });
            _countries.Add(new Country { Code = "ML", Name = "Mali" });
            _countries.Add(new Country { Code = "MM", Name = "Myanmar" });
            _countries.Add(new Country { Code = "MN", Name = "Mongolia" });
            _countries.Add(new Country { Code = "MO", Name = "Macao" });
            _countries.Add(new Country { Code = "MP", Name = "Northern Mariana Islands" });
            _countries.Add(new Country { Code = "MQ", Name = "Martinique" });
            _countries.Add(new Country { Code = "MR", Name = "Mauritania" });
            _countries.Add(new Country { Code = "MS", Name = "Montserrat" });
            _countries.Add(new Country { Code = "MT", Name = "Malta" });
            _countries.Add(new Country { Code = "MU", Name = "Mauritius" });
            _countries.Add(new Country { Code = "MV", Name = "Maldives" });
            _countries.Add(new Country { Code = "MW", Name = "Malawi" });
            _countries.Add(new Country { Code = "MX", Name = "Mexico" });
            _countries.Add(new Country { Code = "MY", Name = "Malaysia" });
            _countries.Add(new Country { Code = "MZ", Name = "Mozambique" });
            _countries.Add(new Country { Code = "NA", Name = "Namibia" });
            _countries.Add(new Country { Code = "NC", Name = "New Caledonia" });
            _countries.Add(new Country { Code = "NE", Name = "Niger" });
            _countries.Add(new Country { Code = "NF", Name = "Norfolk Island" });
            _countries.Add(new Country { Code = "NG", Name = "Nigeria" });
            _countries.Add(new Country { Code = "NI", Name = "Nicaragua" });
            _countries.Add(new Country { Code = "NL", Name = "Netherlands" });
            _countries.Add(new Country { Code = "NO", Name = "Norway" });
            _countries.Add(new Country { Code = "NP", Name = "Nepal" });
            _countries.Add(new Country { Code = "NR", Name = "Nauru" });
            _countries.Add(new Country { Code = "NU", Name = "Niue" });
            _countries.Add(new Country { Code = "NZ", Name = "New Zealand" });
            _countries.Add(new Country { Code = "OM", Name = "Oman" });
            _countries.Add(new Country { Code = "PA", Name = "Panama" });
            _countries.Add(new Country { Code = "PE", Name = "Peru" });
            _countries.Add(new Country { Code = "PF", Name = "French Polynesia" });
            _countries.Add(new Country { Code = "PG", Name = "Papua New Guinea" });
            _countries.Add(new Country { Code = "PH", Name = "Philippines" });
            _countries.Add(new Country { Code = "PK", Name = "Pakistan" });
            _countries.Add(new Country { Code = "PL", Name = "Poland" });
            _countries.Add(new Country { Code = "PM", Name = "Saint Pierre and Miquelon" });
            _countries.Add(new Country { Code = "PN", Name = "Pitcairn" });
            _countries.Add(new Country { Code = "PR", Name = "Puerto Rico" });
            _countries.Add(new Country { Code = "PS", Name = "Palestine" });
            _countries.Add(new Country { Code = "PT", Name = "Portugal" });
            _countries.Add(new Country { Code = "PW", Name = "Palau" });
            _countries.Add(new Country { Code = "PY", Name = "Paraguay" });
            _countries.Add(new Country { Code = "QA", Name = "Qatar" });
            _countries.Add(new Country { Code = "RE", Name = "Réunion" });
            _countries.Add(new Country { Code = "RO", Name = "Romania" });
            _countries.Add(new Country { Code = "RS", Name = "Serbia" });
            _countries.Add(new Country { Code = "RU", Name = "Russian Federation" });
            _countries.Add(new Country { Code = "RW", Name = "Rwanda" });
            _countries.Add(new Country { Code = "SA", Name = "Saudi Arabia" });
            _countries.Add(new Country { Code = "SB", Name = "Solomon Islands" });
            _countries.Add(new Country { Code = "SC", Name = "Seychelles" });
            _countries.Add(new Country { Code = "SD", Name = "Sudan" });
            _countries.Add(new Country { Code = "SE", Name = "Sweden" });
            _countries.Add(new Country { Code = "SG", Name = "Singapore" });
            _countries.Add(new Country { Code = "SH", Name = "Saint Helena, Ascension and Tristan da Cunha" });
            _countries.Add(new Country { Code = "SI", Name = "Slovenia" });
            _countries.Add(new Country { Code = "SJ", Name = "Svalbard and Jan Mayen" });
            _countries.Add(new Country { Code = "SK", Name = "Slovakia" });
            _countries.Add(new Country { Code = "SL", Name = "Sierra Leone" });
            _countries.Add(new Country { Code = "SM", Name = "San Marino" });
            _countries.Add(new Country { Code = "SN", Name = "Senegal" });
            _countries.Add(new Country { Code = "SO", Name = "Somalia" });
            _countries.Add(new Country { Code = "SR", Name = "Suriname" });
            _countries.Add(new Country { Code = "SS", Name = "South Sudan" });
            _countries.Add(new Country { Code = "ST", Name = "Sao Tome and Principe" });
            _countries.Add(new Country { Code = "SV", Name = "El Salvador" });
            _countries.Add(new Country { Code = "SX", Name = "Sint Maarten (Dutch part)" });
            _countries.Add(new Country { Code = "SY", Name = "Syrian Arab Republic" });
            _countries.Add(new Country { Code = "SZ", Name = "Swaziland" });
            _countries.Add(new Country { Code = "TC", Name = "Turks and Caicos Islands" });
            _countries.Add(new Country { Code = "TD", Name = "Chad" });
            _countries.Add(new Country { Code = "TF", Name = "French Southern Territories" });
            _countries.Add(new Country { Code = "TG", Name = "Togo" });
            _countries.Add(new Country { Code = "TH", Name = "Thailand" });
            _countries.Add(new Country { Code = "TJ", Name = "Tajikistan" });
            _countries.Add(new Country { Code = "TK", Name = "Tokelau" });
            _countries.Add(new Country { Code = "TL", Name = "Timor-Leste" });
            _countries.Add(new Country { Code = "TM", Name = "Turkmenistan" });
            _countries.Add(new Country { Code = "TN", Name = "Tunisia" });
            _countries.Add(new Country { Code = "TO", Name = "Tonga" });
            _countries.Add(new Country { Code = "TR", Name = "Turkey" });
            _countries.Add(new Country { Code = "TT", Name = "Trinidad and Tobago" });
            _countries.Add(new Country { Code = "TV", Name = "Tuvalu" });
            _countries.Add(new Country { Code = "TW", Name = "Taiwan" });
            _countries.Add(new Country { Code = "TZ", Name = "Tanzania" });
            _countries.Add(new Country { Code = "UA", Name = "Ukraine" });
            _countries.Add(new Country { Code = "UG", Name = "Uganda" });
            _countries.Add(new Country { Code = "UM", Name = "United States Minor Outlying Islands" });
            _countries.Add(new Country { Code = "US", Name = "United States of America" });
            _countries.Add(new Country { Code = "UY", Name = "Uruguay" });
            _countries.Add(new Country { Code = "UZ", Name = "Uzbekistan" });
            _countries.Add(new Country { Code = "VA", Name = "Holy See" });
            _countries.Add(new Country { Code = "VC", Name = "Saint Vincent and the Grenadines" });
            _countries.Add(new Country { Code = "VE", Name = "Venezuela" });
            _countries.Add(new Country { Code = "VG", Name = "Virgin Islands, British" });
            _countries.Add(new Country { Code = "VI", Name = "Virgin Islands, U.S." });
            _countries.Add(new Country { Code = "VN", Name = "Viet Nam" });
            _countries.Add(new Country { Code = "VU", Name = "Vanuatu" });
            _countries.Add(new Country { Code = "WF", Name = "Wallis and Futuna" });
            _countries.Add(new Country { Code = "WS", Name = "Samoa" });
            _countries.Add(new Country { Code = "YE", Name = "Yemen" });
            _countries.Add(new Country { Code = "YT", Name = "Mayotte" });
            _countries.Add(new Country { Code = "ZA", Name = "South Africa" });
            _countries.Add(new Country { Code = "ZM", Name = "Zambia" });
            _countries.Add(new Country { Code = "ZW", Name = "Zimbabwe" });

            _countries.Add(Country.UnknownCountry);
            logger.Info("{0} Countries loaded", _countries.Count);
        }

        private void LoadBusinessesOfType(XmlDocument document, string nodeName, Func<XmlElement, Business> converterCallback)
        {
            logger.Info("Loading {0}", nodeName);
            var mainNode = document.SelectSingleNode(string.Format("/Data/Businesses/{0}", nodeName));
            var counter = 0;
            foreach (XmlElement node in mainNode.ChildNodes)
            {
                var entity = converterCallback(node);
                _businesses.Add(entity);
                counter++;
            }
            logger.Info("{0} {1} loaded", counter, nodeName);
        }

        private void LoadBusinesses(XmlDocument document, IEnumerable<Country> countries, IEnumerable<Product> products)
        {
            logger.Info("Loading businesses");
            LoadBusinessesOfType(document, "Airlines", new XmlToAirlineConverter(countries).Convert);
            LoadBusinessesOfType(document, "AviationFuelSuppliers", new XmlToAVFuelSupplierConverter(countries).Convert);
            LoadBusinessesOfType(document, "Banks", new XmlToBankConverter(countries).Convert);
            LoadBusinessesOfType(document, "Contractors", new XmlToContractorConverter(countries).Convert);
            LoadBusinessesOfType(document, "Franchises", new XmlToFranchiseConverter(countries, products).Convert);
            _businesses = _businesses.OrderBy(x => x.Name).ToList();
            logger.Info("{0} Businesses loaded", _businesses.Count);
        }

        private void LoadProducts(XmlDocument document)
        {
            logger.Info("Loading products");
            var mainNode = document.SelectSingleNode("/Data/Products");
            var converter = new XmlToProductConverter();
            foreach (XmlElement node in mainNode.ChildNodes)
            {
                var entity = converter.Convert(node);
                _products.Add(entity);
            }

            _products = _products.OrderBy(x => x.Name).ToList();
            logger.Info("{0} products loaded", _products.Count);
        }

        private void LoadMods(XmlDocument document, IEnumerable<Business> businesses)
        {
            logger.Info("Loading mods");
            var mainNode = document.SelectSingleNode("/Data/Mods");
            var converter = new XmlToModConverter(businesses);
            _mods.Add(Mod.UnknownMod);
            foreach (XmlElement node in mainNode.ChildNodes)
            {
                var entity = converter.Convert(node);
                _mods.Add(entity);
            }
            logger.Info("{0} mods loaded", _mods.Count);
        }

        public void Load(string dataPath)
        {
            logger.Info("Loading from {0}", dataPath);
            if (!Directory.Exists(dataPath))
                throw new Exception(string.Format("Path '{0}' not found", dataPath));

            var fullFilename = Path.GetFullPath(Path.Combine(dataPath, DataFileName));

            if (!File.Exists(fullFilename))
                throw new Exception(string.Format("File '{0}' not found", fullFilename));

            var document = new XmlDocument();
            document.Load(fullFilename);

            var declaration = document.ChildNodes.OfType<XmlDeclaration>().FirstOrDefault();
            if (declaration != null)
                Version = declaration.Version;
            else
                Version = string.Empty;

            LoadCountries();
            LoadProducts(document);
            LoadBusinesses(document, _countries, _products);
            LoadMods(document, _businesses);
            logger.Info("Loading done");
        }

        private void MakeBackup(string dataPath)
        {            
            var backupPath = Path.Combine(dataPath, "Backup");
            logger.Info("Creating backup in {0}", backupPath);
            if (!Directory.Exists(backupPath))
            {
                logger.Info("Directory did not exist, creating");
                Directory.CreateDirectory(backupPath);
            }

            var fullFilename = Path.Combine(dataPath, DataFileName);
            var backupPrefix = DateTime.Now.ToString("yyyyMMdd_HHmmss_");
            var fullBackupFilename = Path.Combine(backupPath, backupPrefix + DataFileName);
            logger.Info("Creating backup {0}", fullBackupFilename);
            File.Copy(fullFilename, fullBackupFilename, true);
            logger.Info("Backup created");
        }

        private void SaveProducts(XmlDocument document, XmlNode root, IEnumerable<Product> products)
        {
            logger.Info("Saving products");
            var mainNode = document.CreateElement("Products");
            var converter = new ProductToXmlConverter();
            foreach (var product in products)
            {
                if (product == Product.UnknownProduct)
                    continue;
                var childNode = document.CreateElement("Product");
                converter.Convert(childNode, document, product);
                mainNode.AppendChild(childNode);
                product.Status = EntityStatus.Unchanged;
            }

            root.AppendChild(mainNode);
            logger.Info("{0} Products saved", products.Count());
        }

        private void SaveBusinessesOfType<T>(XmlElement node, XmlDocument document, IEnumerable<T> businesses, Action<XmlElement, XmlDocument, T> converterCallback)
            where T: Business
        {
            var nodeName = typeof(T).Name;
            var mainNodeName = string.Format("{0}s", nodeName);
            logger.Info("Saving {0}", mainNodeName);
            var mainNode = document.CreateElement(mainNodeName);
            foreach (var business in businesses)
            {
                if (business == Business.UnknownBusiness)
                    continue;
                var childNode = document.CreateElement(nodeName);
                converterCallback(childNode, document, business);
                mainNode.AppendChild(childNode);
                business.Status = EntityStatus.Unchanged;
            }
            logger.Info("{0} {1} saved", businesses.Count(), mainNodeName);
            node.AppendChild(mainNode);
        }

        private void SaveBusinesses(XmlDocument document, XmlNode root, IEnumerable<Business> businesses)
        {
            logger.Info("Saving businesses");
            var node = document.CreateElement("Businesses");
            SaveBusinessesOfType(node, document, businesses.Where(x => x is Airline).Cast<Airline>(), new AirlineToXmlConverter().Convert);
            SaveBusinessesOfType(node, document, businesses.Where(x => x is AviationFuelSupplier).Cast<AviationFuelSupplier>(), new AVFuelSupplierToXmlConverter().Convert);
            SaveBusinessesOfType(node, document, businesses.Where(x => x is Bank).Cast<Bank>(), new BankToXmlConverter().Convert);
            SaveBusinessesOfType(node, document, businesses.Where(x => x is Contractor).Cast<Contractor>(), new ContractorToXmlConverter().Convert);
            SaveBusinessesOfType(node, document, businesses.Where(x => x is Franchise).Cast<Franchise>(), new FranchiseToXmlConverter().Convert);
            logger.Info("{0} businesses saved", businesses.Count());
            root.AppendChild(node);
        }

        private void SaveMods(XmlDocument document, XmlNode root, IEnumerable<Mod> mods)
        {
            logger.Info("Saving mods");
            var mainNode = document.CreateElement("Mods");
            var converter = new ModToXmlConverter();
            foreach (var mod in mods)
            {
                if (mod == Mod.UnknownMod)
                    continue;
                var childNode = document.CreateElement("Mod");
                converter.Convert(childNode, document, mod);
                mainNode.AppendChild(childNode);
                mod.Status = EntityStatus.Unchanged;
            }
            logger.Info("{0} mods saved", mods.Count());
            root.AppendChild(mainNode);
        }

        public void Save(string dataPath, IEnumerable<Product> products, IEnumerable<Business> businesses, IEnumerable<Mod> mods)
        {
            logger.Info("Saving data to {0}", dataPath);
            if (!Directory.Exists(dataPath))
                throw new Exception(string.Format("Path '{0}' not found", dataPath));

            MakeBackup(dataPath);

            var document = new XmlDocument();
            var declaration = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            document.InsertBefore(declaration, document.DocumentElement);
            var rootNode = document.CreateElement("Data");
            document.AppendChild(rootNode);
            SaveProducts(document, rootNode, products);
            SaveBusinesses(document, rootNode, businesses);
            SaveMods(document, rootNode, mods);

            var fullFilename = Path.Combine(dataPath, DataFileName);
            document.Save(fullFilename);
            logger.Info("Saving done");
        }
    }
}
