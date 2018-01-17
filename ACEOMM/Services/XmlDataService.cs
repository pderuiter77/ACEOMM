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
using Newtonsoft.Json;
using ACEOMM.Services.Converter.JsonToDomain;

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
        private List<Livery> _liveries = new List<Livery>();

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

        public IEnumerable<Livery> GetLiveries()
        {
            AssertIsDataLoaded();
            return _liveries;
        }

        public string Version { get; private set; }

        private void LoadCountries()
        {
            logger.Info("Loading countries");
            _countries.Add(new Country { Code = "AD", Name = "Andorra", Region = "Europe" });
            _countries.Add(new Country { Code = "AE", Name = "United Arab Emirates", Region = "Asia" });
            _countries.Add(new Country { Code = "AF", Name = "Afghanistan", Region = "Asia" });
            _countries.Add(new Country { Code = "AG", Name = "Antigua & Barbuda", Region = "North america" });
            _countries.Add(new Country { Code = "AI", Name = "Anguilla", Region = "North america" });
            _countries.Add(new Country { Code = "AL", Name = "Albania", Region = "Europe" });
            _countries.Add(new Country { Code = "AM", Name = "Armenia", Region = "Asia" });
            _countries.Add(new Country { Code = "AO", Name = "Angola", Region = "Africa" });
            _countries.Add(new Country { Code = "AQ", Name = "Antarctica", Region = "Oceania" });
            _countries.Add(new Country { Code = "AR", Name = "Argentina", Region = "South america" });
            _countries.Add(new Country { Code = "AS", Name = "American Samoa", Region = "Oceania" });
            _countries.Add(new Country { Code = "AT", Name = "Austria", Region = "Europe" });
            _countries.Add(new Country { Code = "AU", Name = "Australia", Region = "Oceania" });
            _countries.Add(new Country { Code = "AW", Name = "Aruba", Region = "North america" });
            _countries.Add(new Country { Code = "AX", Name = "Åland Islands", Region = "Europe" });
            _countries.Add(new Country { Code = "AZ", Name = "Azerbaijan", Region = "Asia" });
            _countries.Add(new Country { Code = "BA", Name = "Bosnia and Herzegovina", Region = "Europe" });
            _countries.Add(new Country { Code = "BB", Name = "Barbados", Region = "North america" });
            _countries.Add(new Country { Code = "BD", Name = "Bangladesh", Region = "Asia" });
            _countries.Add(new Country { Code = "BE", Name = "Belgium", Region = "Europe" });
            _countries.Add(new Country { Code = "BF", Name = "Burkina Faso", Region = "Africa" });
            _countries.Add(new Country { Code = "BG", Name = "Bulgaria", Region = "Europe" });
            _countries.Add(new Country { Code = "BH", Name = "Bahrain", Region = "Asia" });
            _countries.Add(new Country { Code = "BI", Name = "Burundi", Region = "Africa" });
            _countries.Add(new Country { Code = "BJ", Name = "Benin", Region = "Africa" });
            _countries.Add(new Country { Code = "BL", Name = "Saint Barthélemy", Region = "North america" });
            _countries.Add(new Country { Code = "BM", Name = "Bermuda", Region = "North america" });
            _countries.Add(new Country { Code = "BN", Name = "Brunei Darussalam", Region = "Asia" });
            _countries.Add(new Country { Code = "BO", Name = "Bolivia", Region = "South america" });
            _countries.Add(new Country { Code = "BQ", Name = "Bonaire, Sint Eustatius and Saba", Region = "North america" });
            _countries.Add(new Country { Code = "BR", Name = "Brazil", Region = "South america" });
            _countries.Add(new Country { Code = "BS", Name = "Bahamas", Region = "North america" });
            _countries.Add(new Country { Code = "BT", Name = "Bhutan", Region = "Asia" });
            _countries.Add(new Country { Code = "BV", Name = "Bouvet Island", Region = "Oceania" });
            _countries.Add(new Country { Code = "BW", Name = "Botswana", Region = "Africa" });
            _countries.Add(new Country { Code = "BY", Name = "Belarus", Region = "Europe" });
            _countries.Add(new Country { Code = "BZ", Name = "Belize", Region = "North america" });
            _countries.Add(new Country { Code = "CA", Name = "Canada", Region = "North america" });
            _countries.Add(new Country { Code = "CC", Name = "Cocos (Keeling) Islands", Region = "Asia" });
            _countries.Add(new Country { Code = "CD", Name = "Congo", Region = "Africa" });
            _countries.Add(new Country { Code = "CF", Name = "Central African Republic", Region = "Africa" });
            _countries.Add(new Country { Code = "CG", Name = "Congo", Region = "Africa" });
            _countries.Add(new Country { Code = "CH", Name = "Switzerland", Region = "Europe" });
            _countries.Add(new Country { Code = "CI", Name = "Côte d'Ivoire", Region = "Africa" });
            _countries.Add(new Country { Code = "CK", Name = "Cook Islands", Region = "Oceania" });
            _countries.Add(new Country { Code = "CL", Name = "Chile", Region = "South america" });
            _countries.Add(new Country { Code = "CM", Name = "Cameroon", Region = "Africa" });
            _countries.Add(new Country { Code = "CN", Name = "China", Region = "Asia" });
            _countries.Add(new Country { Code = "CO", Name = "Colombia", Region = "South america" });
            _countries.Add(new Country { Code = "CR", Name = "Costa Rica", Region = "North america" });
            _countries.Add(new Country { Code = "CU", Name = "Cuba", Region = "North america" });
            _countries.Add(new Country { Code = "CV", Name = "Cabo Verde", Region = "Africa" });
            _countries.Add(new Country { Code = "CW", Name = "Curaçao", Region = "North america" });
            _countries.Add(new Country { Code = "CX", Name = "Christmas Island", Region = "Asia" });
            _countries.Add(new Country { Code = "CY", Name = "Cyprus", Region = "Asia" });
            _countries.Add(new Country { Code = "CZ", Name = "Czechia", Region = "Europe" });
            _countries.Add(new Country { Code = "DE", Name = "Germany", Region = "Europe" });
            _countries.Add(new Country { Code = "DJ", Name = "Djibouti", Region = "Africa" });
            _countries.Add(new Country { Code = "DK", Name = "Denmark", Region = "Europe" });
            _countries.Add(new Country { Code = "DM", Name = "Dominica", Region = "North america" });
            _countries.Add(new Country { Code = "DO", Name = "Dominican Republic", Region = "North america" });
            _countries.Add(new Country { Code = "DZ", Name = "Algeria", Region = "Africa" });
            _countries.Add(new Country { Code = "EC", Name = "Ecuador", Region = "South america" });
            _countries.Add(new Country { Code = "EE", Name = "Estonia", Region = "Europe" });
            _countries.Add(new Country { Code = "EG", Name = "Egypt", Region = "Africa" });
            _countries.Add(new Country { Code = "EH", Name = "Western Sahara", Region = "Africa" });
            _countries.Add(new Country { Code = "ER", Name = "Eritrea", Region = "Africa" });
            _countries.Add(new Country { Code = "ES", Name = "Spain", Region = "Europe" });
            _countries.Add(new Country { Code = "ET", Name = "Ethiopia", Region = "Africa" });
            _countries.Add(new Country { Code = "FI", Name = "Finland", Region = "Europe" });
            _countries.Add(new Country { Code = "FJ", Name = "Fiji", Region = "Oceania" });
            _countries.Add(new Country { Code = "FK", Name = "Falkland Islands", Region = "South america" });
            _countries.Add(new Country { Code = "FM", Name = "Micronesia", Region = "Oceania" });
            _countries.Add(new Country { Code = "FO", Name = "Faroe Islands", Region = "Europe" });
            _countries.Add(new Country { Code = "FR", Name = "France", Region = "Europe" });
            _countries.Add(new Country { Code = "GA", Name = "Gabon", Region = "Africa" });
            _countries.Add(new Country { Code = "GB", Name = "United Kingdom of Great Britain and Northern Ireland", Region = "Europe" });
            _countries.Add(new Country { Code = "GD", Name = "Grenada", Region = "North america" });
            _countries.Add(new Country { Code = "GE", Name = "Georgia", Region = "Asia" });
            _countries.Add(new Country { Code = "GF", Name = "French Guiana", Region = "South america" });
            _countries.Add(new Country { Code = "GG", Name = "Guernsey", Region = "Europe" });
            _countries.Add(new Country { Code = "GH", Name = "Ghana", Region = "Africa" });
            _countries.Add(new Country { Code = "GI", Name = "Gibraltar", Region = "Europe" });
            _countries.Add(new Country { Code = "GL", Name = "Greenland", Region = "North america" });
            _countries.Add(new Country { Code = "GM", Name = "Gambia", Region = "Africa" });
            _countries.Add(new Country { Code = "GN", Name = "Guinea", Region = "Africa" });
            _countries.Add(new Country { Code = "GP", Name = "Guadeloupe", Region = "North america" });
            _countries.Add(new Country { Code = "GQ", Name = "Equatorial Guinea", Region = "Africa" });
            _countries.Add(new Country { Code = "GR", Name = "Greece", Region = "Europe" });
            _countries.Add(new Country { Code = "GS", Name = "South Georgia and the South Sandwich Islands", Region = "South america" });
            _countries.Add(new Country { Code = "GT", Name = "Guatemala", Region = "North america" });
            _countries.Add(new Country { Code = "GU", Name = "Guam", Region = "Oceania" });
            _countries.Add(new Country { Code = "GW", Name = "Guinea-Bissau", Region = "Africa" });
            _countries.Add(new Country { Code = "GY", Name = "Guyana", Region = "South america" });
            _countries.Add(new Country { Code = "HK", Name = "Hong Kong", Region = "Asia" });
            _countries.Add(new Country { Code = "HM", Name = "Heard Island and McDonald Islands" });
            _countries.Add(new Country { Code = "HN", Name = "Honduras", Region = "North america" });
            _countries.Add(new Country { Code = "HR", Name = "Croatia", Region = "Europe" });
            _countries.Add(new Country { Code = "HT", Name = "Haiti", Region = "North america" });
            _countries.Add(new Country { Code = "HU", Name = "Hungary", Region = "Europe" });
            _countries.Add(new Country { Code = "ID", Name = "Indonesia", Region = "Asia" });
            _countries.Add(new Country { Code = "IE", Name = "Ireland", Region = "Europe" });
            _countries.Add(new Country { Code = "IL", Name = "Israel", Region = "Asia" });
            _countries.Add(new Country { Code = "IM", Name = "Isle of Man", Region = "Europe" });
            _countries.Add(new Country { Code = "IN", Name = "India", Region = "Asia" });
            _countries.Add(new Country { Code = "IO", Name = "British Indian Ocean Territory", Region = "Asia" });
            _countries.Add(new Country { Code = "IQ", Name = "Iraq", Region = "Asia" });
            _countries.Add(new Country { Code = "IR", Name = "Iran", Region = "Asia" });
            _countries.Add(new Country { Code = "IS", Name = "Iceland", Region = "Europe" });
            _countries.Add(new Country { Code = "IT", Name = "Italy", Region = "Europe" });
            _countries.Add(new Country { Code = "JE", Name = "Jersey", Region = "Europe" });
            _countries.Add(new Country { Code = "JM", Name = "Jamaica", Region = "North america" });
            _countries.Add(new Country { Code = "JO", Name = "Jordan", Region = "Asia" });
            _countries.Add(new Country { Code = "JP", Name = "Japan", Region = "Asia" });
            _countries.Add(new Country { Code = "KE", Name = "Kenya", Region = "Africa" });
            _countries.Add(new Country { Code = "KG", Name = "Kyrgyzstan", Region = "Asia" });
            _countries.Add(new Country { Code = "KH", Name = "Cambodia", Region = "Asia" });
            _countries.Add(new Country { Code = "KI", Name = "Kiribati", Region = "Oceania" });
            _countries.Add(new Country { Code = "KM", Name = "Comoros", Region = "Africa" });
            _countries.Add(new Country { Code = "KN", Name = "Saint Kitts and Nevis", Region = "North america" });
            _countries.Add(new Country { Code = "KP", Name = "North Korea", Region = "Asia" });
            _countries.Add(new Country { Code = "KR", Name = "South Korea", Region = "Asia" });
            _countries.Add(new Country { Code = "KW", Name = "Kuwait", Region = "Asia" });
            _countries.Add(new Country { Code = "KY", Name = "Cayman Islands", Region = "North america" });
            _countries.Add(new Country { Code = "KZ", Name = "Kazakhstan", Region = "Asia" });
            _countries.Add(new Country { Code = "LA", Name = "Laos", Region = "Asia" });
            _countries.Add(new Country { Code = "LB", Name = "Lebanon", Region = "Asia" });
            _countries.Add(new Country { Code = "LC", Name = "Saint Lucia", Region = "North america" });
            _countries.Add(new Country { Code = "LI", Name = "Liechtenstein", Region = "Europe" });
            _countries.Add(new Country { Code = "LK", Name = "Sri Lanka", Region = "Asia" });
            _countries.Add(new Country { Code = "LR", Name = "Liberia", Region = "Africa" });
            _countries.Add(new Country { Code = "LS", Name = "Lesotho", Region = "Africa" });
            _countries.Add(new Country { Code = "LT", Name = "Lithuania", Region = "Europe" });
            _countries.Add(new Country { Code = "LU", Name = "Luxembourg", Region = "Europe" });
            _countries.Add(new Country { Code = "LV", Name = "Latvia", Region = "Europe" });
            _countries.Add(new Country { Code = "LY", Name = "Libya", Region = "Africa" });
            _countries.Add(new Country { Code = "MA", Name = "Morocco", Region = "Africa" });
            _countries.Add(new Country { Code = "MC", Name = "Monaco", Region = "Europe" });
            _countries.Add(new Country { Code = "MD", Name = "Moldova", Region = "Europe" });
            _countries.Add(new Country { Code = "ME", Name = "Montenegro", Region = "Europe" });
            _countries.Add(new Country { Code = "MF", Name = "Saint Martin (French part)", Region = "North america" });
            _countries.Add(new Country { Code = "MG", Name = "Madagascar", Region = "Africa" });
            _countries.Add(new Country { Code = "MH", Name = "Marshall Islands", Region = "Oceania" });
            _countries.Add(new Country { Code = "ML", Name = "Mali", Region = "Africa" });
            _countries.Add(new Country { Code = "MM", Name = "Myanmar", Region = "Asia" });
            _countries.Add(new Country { Code = "MN", Name = "Mongolia", Region = "Asia" });
            _countries.Add(new Country { Code = "MO", Name = "Macao", Region = "Asia" });
            _countries.Add(new Country { Code = "MP", Name = "Northern Mariana Islands" });
            _countries.Add(new Country { Code = "MQ", Name = "Martinique", Region = "North america" });
            _countries.Add(new Country { Code = "MR", Name = "Mauritania", Region = "Africa" });
            _countries.Add(new Country { Code = "MS", Name = "Montserrat", Region = "North america" });
            _countries.Add(new Country { Code = "MT", Name = "Malta", Region = "Europe" });
            _countries.Add(new Country { Code = "MU", Name = "Mauritius", Region = "Africa" });
            _countries.Add(new Country { Code = "MV", Name = "Maldives", Region = "Asia" });
            _countries.Add(new Country { Code = "MW", Name = "Malawi", Region = "Africa" });
            _countries.Add(new Country { Code = "MX", Name = "Mexico", Region = "North america" });
            _countries.Add(new Country { Code = "MY", Name = "Malaysia", Region = "Asia" });
            _countries.Add(new Country { Code = "MZ", Name = "Mozambique", Region = "Africa" });
            _countries.Add(new Country { Code = "NA", Name = "Namibia", Region = "Africa" });
            _countries.Add(new Country { Code = "NC", Name = "New Caledonia", Region = "Oceania" });
            _countries.Add(new Country { Code = "NE", Name = "Niger", Region = "Africa" });
            _countries.Add(new Country { Code = "NF", Name = "Norfolk Island", Region = "Oceania" });
            _countries.Add(new Country { Code = "NG", Name = "Nigeria", Region = "Africa" });
            _countries.Add(new Country { Code = "NI", Name = "Nicaragua", Region = "North america" });
            _countries.Add(new Country { Code = "NL", Name = "Netherlands", Region = "Europe" });
            _countries.Add(new Country { Code = "NO", Name = "Norway", Region = "Europe" });
            _countries.Add(new Country { Code = "NP", Name = "Nepal", Region = "Asia" });
            _countries.Add(new Country { Code = "NR", Name = "Nauru", Region = "Oceania" });
            _countries.Add(new Country { Code = "NU", Name = "Niue", Region = "Oceania" });
            _countries.Add(new Country { Code = "NZ", Name = "New Zealand", Region = "Oceania" });
            _countries.Add(new Country { Code = "OM", Name = "Oman", Region = "Asia" });
            _countries.Add(new Country { Code = "PA", Name = "Panama", Region = "North america" });
            _countries.Add(new Country { Code = "PE", Name = "Peru", Region = "South america" });
            _countries.Add(new Country { Code = "PF", Name = "French Polynesia", Region = "Oceania" });
            _countries.Add(new Country { Code = "PG", Name = "Papua New Guinea", Region = "Oceania" });
            _countries.Add(new Country { Code = "PH", Name = "Philippines", Region = "Asia" });
            _countries.Add(new Country { Code = "PK", Name = "Pakistan", Region = "Asia" });
            _countries.Add(new Country { Code = "PL", Name = "Poland", Region = "Europe" });
            _countries.Add(new Country { Code = "PM", Name = "Saint Pierre and Miquelon", Region = "North america" });
            _countries.Add(new Country { Code = "PN", Name = "Pitcairn", Region = "Oceania" });
            _countries.Add(new Country { Code = "PR", Name = "Puerto Rico", Region = "North america" });
            _countries.Add(new Country { Code = "PS", Name = "Palestine", Region = "Asia" });
            _countries.Add(new Country { Code = "PT", Name = "Portugal", Region = "Europe" });
            _countries.Add(new Country { Code = "PW", Name = "Palau", Region = "Oceania" });
            _countries.Add(new Country { Code = "PY", Name = "Paraguay", Region = "South america" });
            _countries.Add(new Country { Code = "QA", Name = "Qatar", Region = "Asia" });
            _countries.Add(new Country { Code = "RE", Name = "Réunion", Region = "Africa" });
            _countries.Add(new Country { Code = "RO", Name = "Romania", Region = "Europe" });
            _countries.Add(new Country { Code = "RS", Name = "Serbia", Region = "Europe" });
            _countries.Add(new Country { Code = "RU", Name = "Russian Federation", Region = "Europe" });
            _countries.Add(new Country { Code = "RW", Name = "Rwanda", Region = "Africa" });
            _countries.Add(new Country { Code = "SA", Name = "Saudi Arabia", Region = "Asia" });
            _countries.Add(new Country { Code = "SB", Name = "Solomon Islands", Region = "Oceania" });
            _countries.Add(new Country { Code = "SC", Name = "Seychelles", Region = "Africa" });
            _countries.Add(new Country { Code = "SD", Name = "Sudan", Region = "Africa" });
            _countries.Add(new Country { Code = "SE", Name = "Sweden", Region = "Europe" });
            _countries.Add(new Country { Code = "SG", Name = "Singapore", Region = "Asia" });
            _countries.Add(new Country { Code = "SH", Name = "Saint Helena, Ascension and Tristan da Cunha", Region = "Africa" });
            _countries.Add(new Country { Code = "SI", Name = "Slovenia", Region = "Europe" });
            _countries.Add(new Country { Code = "SJ", Name = "Svalbard and Jan Mayen", Region = "Europe" });
            _countries.Add(new Country { Code = "SK", Name = "Slovakia", Region = "Europe" });
            _countries.Add(new Country { Code = "SL", Name = "Sierra Leone", Region = "Africa" });
            _countries.Add(new Country { Code = "SM", Name = "San Marino", Region = "Europe" });
            _countries.Add(new Country { Code = "SN", Name = "Senegal", Region = "Africa" });
            _countries.Add(new Country { Code = "SO", Name = "Somalia", Region = "Africa" });
            _countries.Add(new Country { Code = "SR", Name = "Suriname", Region = "South america" });
            _countries.Add(new Country { Code = "SS", Name = "South Sudan", Region = "Africa" });
            _countries.Add(new Country { Code = "ST", Name = "Sao Tome and Principe", Region = "Africa" });
            _countries.Add(new Country { Code = "SV", Name = "El Salvador", Region = "North america" });
            _countries.Add(new Country { Code = "SX", Name = "Sint Maarten (Dutch part)" });
            _countries.Add(new Country { Code = "SY", Name = "Syrian Arab Republic", Region = "Africa" });
            _countries.Add(new Country { Code = "SZ", Name = "Swaziland", Region = "Africa" });
            _countries.Add(new Country { Code = "TC", Name = "Turks and Caicos Islands", Region = "North america" });
            _countries.Add(new Country { Code = "TD", Name = "Chad", Region = "Africa" });
            _countries.Add(new Country { Code = "TF", Name = "French Southern Territories" });
            _countries.Add(new Country { Code = "TG", Name = "Togo", Region = "Africa" });
            _countries.Add(new Country { Code = "TH", Name = "Thailand", Region = "Asia" });
            _countries.Add(new Country { Code = "TJ", Name = "Tajikistan", Region = "Asia" });
            _countries.Add(new Country { Code = "TK", Name = "Tokelau", Region = "Oceania" });
            _countries.Add(new Country { Code = "TL", Name = "Timor-Leste", Region = "Asia" });
            _countries.Add(new Country { Code = "TM", Name = "Turkmenistan", Region = "Asia" });
            _countries.Add(new Country { Code = "TN", Name = "Tunisia", Region = "Africa" });
            _countries.Add(new Country { Code = "TO", Name = "Tonga", Region = "Oceania" });
            _countries.Add(new Country { Code = "TR", Name = "Turkey", Region = "Asia" });
            _countries.Add(new Country { Code = "TT", Name = "Trinidad and Tobago", Region = "North america" });
            _countries.Add(new Country { Code = "TV", Name = "Tuvalu", Region = "Oceania" });
            _countries.Add(new Country { Code = "TW", Name = "Taiwan", Region = "Asia" });
            _countries.Add(new Country { Code = "TZ", Name = "Tanzania", Region = "Africa" });
            _countries.Add(new Country { Code = "UA", Name = "Ukraine", Region = "Europe" });
            _countries.Add(new Country { Code = "UG", Name = "Uganda", Region = "Africa" });
            _countries.Add(new Country { Code = "UM", Name = "United States Minor Outlying Islands", Region = "North america" });
            _countries.Add(new Country { Code = "US", Name = "United States of America", Region = "North america" });
            _countries.Add(new Country { Code = "UY", Name = "Uruguay", Region = "South america" });
            _countries.Add(new Country { Code = "UZ", Name = "Uzbekistan", Region = "Asia" });
            _countries.Add(new Country { Code = "VA", Name = "Holy See", Region = "Europe" });
            _countries.Add(new Country { Code = "VC", Name = "Saint Vincent and the Grenadines", Region = "North america" });
            _countries.Add(new Country { Code = "VE", Name = "Venezuela", Region = "South america" });
            _countries.Add(new Country { Code = "VG", Name = "Virgin Islands, British", Region = "North america" });
            _countries.Add(new Country { Code = "VI", Name = "Virgin Islands, U.S.", Region = "North america" });
            _countries.Add(new Country { Code = "VN", Name = "Viet Nam", Region = "Asia" });
            _countries.Add(new Country { Code = "VU", Name = "Vanuatu", Region = "Oceania" });
            _countries.Add(new Country { Code = "WF", Name = "Wallis and Futuna", Region = "Oceania" });
            _countries.Add(new Country { Code = "WS", Name = "Samoa", Region = "Oceania" });
            _countries.Add(new Country { Code = "YE", Name = "Yemen", Region = "Asia" });
            _countries.Add(new Country { Code = "YT", Name = "Mayotte", Region = "Africa" });
            _countries.Add(new Country { Code = "ZA", Name = "South Africa", Region = "Africa" });
            _countries.Add(new Country { Code = "ZM", Name = "Zambia", Region = "Africa" });
            _countries.Add(new Country { Code = "ZW", Name = "Zimbabwe", Region = "Africa" });

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

        private void FindLiveries(string path, List<string> liveryFiles)
        {
            if (!Directory.Exists(path))
            {
                logger.Error("Cannot find livery path '{0}'", path);
                return;
            }
            foreach (var f in Directory.GetFiles(path, "liveryData.json"))
            {
                liveryFiles.Add(f);
            }
            foreach (var s in Directory.GetDirectories(path))
            {
                FindLiveries(s, liveryFiles);
            }
        }

        private void LoadLiveries(string dataPath)
        {
            logger.Info("Loading liveries");

            var liveryPaths = new List<string>();
            FindLiveries(Path.GetFullPath(Path.Combine(dataPath, "Liveries")), liveryPaths);
            foreach (var liveryPath in liveryPaths)
            {
                // Airline = Folder where liveryData.json resides
                // Aircraft = Folder where airline resides
                var path = Path.GetDirectoryName(liveryPath);
                // if identification.json is available, use that
                var pathParts = path.TrimEnd(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar).ToList();
                
                var airline = pathParts[pathParts.Count - 1];
                var aircraft = pathParts[pathParts.Count - 2];
                var author = string.Empty;
                var identFileName = string.Format(@"{0}\Identification.json", path);
                if (File.Exists(identFileName))
                {
                    var identification = JsonConvert.DeserializeObject<LiveryIdentification>(File.ReadAllText(identFileName));
                    airline = identification.airline;
                    aircraft = identification.aircraft;
                    author = identification.author;
                }

                _liveries.Add(new Livery { Aircraft = aircraft, Airline = airline, Author = author, Path = path });
            }
            _liveries = _liveries.OrderBy(x => x.Airline).ToList();
            logger.Info("{0} liveries loaded", _liveries.Count);
        }

        private void LoadBusinesses(XmlDocument document, IEnumerable<Country> countries, IEnumerable<Product> products, IEnumerable<Livery> liveries)
        {
            logger.Info("Loading businesses");
            LoadBusinessesOfType(document, "Airlines", new XmlToAirlineConverter(countries, liveries).Convert);
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
            LoadLiveries(dataPath);
            LoadProducts(document);
            LoadBusinesses(document, _countries, _products, _liveries);
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
