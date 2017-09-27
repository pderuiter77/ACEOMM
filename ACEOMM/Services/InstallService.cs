using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using ACEOMM.Services.Converter.DomainToJson;
using Microsoft.Win32;
using NLog;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using ACEOMM.Services.Converter.DomainToJson.Model;

namespace ACEOMM.Services
{
    public class InstallService
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        private const string DefaultInstallPath = @".\Install";

        private static string DetermineInstallPath()
        {
            //return @"C:\Program Files (x86)\Steam\steamapps\common\Airport CEO";
            // Step 1: Find Steam path
            var steamPath = ReadSteamInstallPathFromRegistry();
            if (string.IsNullOrWhiteSpace(steamPath))
                return DefaultInstallPath;
            // Step 2: Find game at default location
            var defaultGameInstallPath = Path.Combine(steamPath, "steamapps/common/Airport CEO/");
            if (Directory.Exists(defaultGameInstallPath))
                return defaultGameInstallPath;
            // Step 3: Find game through vdf file
            var movedGameInstallPath = ReadLibraryFoldersFromFile(Path.Combine(steamPath, "steamapps/libraryfolders.vdf"));
            if (Directory.Exists(movedGameInstallPath))
                return movedGameInstallPath;

            // Step 4: Alas, we'll return our local path
            return DefaultInstallPath;

        }

        private static string ReadLibraryFoldersFromFile(string filename)
        {
            if (!File.Exists(filename))
                return string.Empty;

            var lines = File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                var fields = line.Split('\t');

                if (fields.Length == 4)
                {
                    int dummy;
                    var name = fields[1].Replace("\"", "");
                    var value = fields[3].Replace("\"", "");
                    if (int.TryParse(name, out dummy))
                    {
                        var aceoPath = Path.Combine(value, "steamapps/common/Airport CEO/");
                        if (Directory.Exists(aceoPath))
                            return aceoPath;
                    }
                }
            }
            return string.Empty;
        }

        private static string ReadSteamInstallPathFromRegistry()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Valve\\Steam"))
            {
                if (key != null)
                {
                    var o = key.GetValue("SteamPath");
                    if (o != null)
                        return o as string;
                }
            }
            return string.Empty;
        }

        private static string _installPath;
        public static string InstallPath 
        { 
            get 
            {
                if (string.IsNullOrWhiteSpace(_installPath))
                    _installPath = DetermineInstallPath();

                return _installPath;
            } 
        }

        public static bool GameFound
        {
            get { return InstallPath != DefaultInstallPath; }
        }

        public static object JsonBusinuess { get; private set; }

        private static string CompaniesPath = "/Airport CEO_Data/DataFiles/Companies/";
        private static string ProductsPath = "/Airport CEO_Data/DataFiles/Products/";

        private static string BusinessInstallPath(Business business, string modPath)
        {
            var basePath = modPath + @"\{0}s\";
            if (business.Type == BusinessType.Franchise)
            {
                if (((Franchise)business).FranchiseType == FranchiseType.Shop)
                    return string.Format(basePath, "ShopFranchise");

                return string.Format(basePath, "FoodFranchise");
            }
            else
                return string.Format(basePath, business.Type.ToString());

        }

        private static string ConvertBusinessToJson(Business business)
        {
            if (business is Franchise)
                return JsonConvert.SerializeObject(JsonFranchise.Convert(business as Franchise), Formatting.Indented);

            return JsonConvert.SerializeObject(JsonBusiness.Convert(business), Formatting.Indented);
        }

        private static void InstallLogo(Logo logo, string imagePath, string installPath)
        {
            if (string.IsNullOrWhiteSpace(logo.LocalFilename))
                CopyFile("NoImage.png", @".\Data\Images\", installPath);
            else
                CopyFile(logo.LocalFilename, imagePath, installPath);
        }

        private static void InstallBusiness(Business business, string modPath) 
        {
            var installPath = BusinessInstallPath(business, modPath) + business.Name;
            logger.Info("Installing business to {0}", installPath);
            if (!Directory.Exists(installPath))
                Directory.CreateDirectory(installPath);

            var json = ConvertBusinessToJson(business);

            var fileName = Path.Combine(installPath, business.Name + ".json");
            File.WriteAllText(fileName, json, Encoding.UTF8);

            var imagePath = Path.Combine(@".\Data\Images\Businesses\", business.Type.ToString());
            InstallLogo(business.Logo, imagePath, installPath);

            File.WriteAllText(Path.Combine(installPath, "ACEOMM.Mod"), business.Id.ToString());
        }

        private static void CopyFile(string fileName, string sourcePath, string targetPath)
        {
            var sourceFile = Path.Combine(sourcePath, fileName);
            var targetFile = Path.Combine(targetPath, fileName);//.Replace("/", @"\").Replace(@"\\", @"\");
            if (!File.Exists(sourceFile))
                return;

            File.Copy(sourceFile, targetFile, true);
        }

        private static void InstallMiscFiles(string modPath)
        {
            CopyFile("Credits.txt", @".\Data\", modPath);
            CopyFile("Disclaimer.txt", @".\Data\", modPath);
        }

        private static void InstallBusinesses(Mod mod, Dictionary<JsonBusiness, string> installedBusinesses, bool useDefaults)
        {
            // Step 1 : Find or create mod folder
            var companiesInstallPath = InstallPath + CompaniesPath + mod.Name;
            logger.Info("Trying to install to {0}", companiesInstallPath);
            if (!Directory.Exists(companiesInstallPath))
            {
                logger.Info("Path not found, creating");
                Directory.CreateDirectory(companiesInstallPath);
            }
            // Step 2 : Install businesses
            foreach (var business in mod.Businesses)
            {
                var installedBusiness = installedBusinesses.FirstOrDefault(x => (useDefaults || !x.Key.IsDefault) && x.Key.name == business.Name && x.Key.Id != business.Id.ToString());
                if (installedBusiness.Key != null)
                {
                    var msg = string.Format("Business '{0}' already installed ({1}), cannot install", business.Name, installedBusiness.Value);
                    logger.Error(msg);
                    throw new System.Exception(msg);
                }
                InstallBusiness(business, companiesInstallPath);
            }

            // Step 3 : Install misc. files
            InstallMiscFiles(companiesInstallPath);
        }

        private static void AddProductsToList(IEnumerable<Product> productsToAdd, List<Product> list)
        {
            foreach (var product in productsToAdd)
            {
                if (!list.Contains(product))
                    list.Add(product);
            }
        }

        private static void InstallProducts(List<Product> products, FranchiseType type, string modPath)
        {
            var installPath = Path.Combine(modPath, type == FranchiseType.Shop ? "Shop" : "Food");
            if (!Directory.Exists(installPath))
                Directory.CreateDirectory(installPath);

            var jsonProducts = new JsonProducts
            {
                array = products.Select(x => JsonProduct.Convert(x)).ToList()
            };

            var json = JsonConvert.SerializeObject(jsonProducts, Formatting.Indented);

            var fileName = Path.Combine(installPath, string.Format("{0}Products.json", type.ToString()));
            File.WriteAllText(fileName, json);
            File.WriteAllLines(Path.Combine(installPath, string.Format("ACEOMM {0}Products.mod", type.ToString())), products.Select(x => x.Id.ToString()).ToList());
        }

        private static void InstallProducts(Mod mod, Dictionary<JsonProduct, string> installedProducts, bool useDefaults)
        {
            // Step 1: Find or create mod folder
            var productsInstallPath = InstallPath + ProductsPath + mod.Name;
            if (!Directory.Exists(productsInstallPath))
                Directory.CreateDirectory(productsInstallPath);

            var franchises = mod.Businesses.Where(x => x.Type == BusinessType.Franchise).Cast<Franchise>().ToList();
            var totalproductList = new Dictionary<Product, Business>();
            foreach (var franchise in franchises)
            {
                foreach (var product in franchise.Products)
                {
                    if (product.Type == FranchiseType.Unknown)
                    {
                        var msg = string.Format("Product {0} has unknown type, cannot install", product.Name);
                        logger.Error(msg);
                        throw new System.Exception(msg);
                    }
                    var toInstallProduct = totalproductList.FirstOrDefault(x => x.Key.Name == product.Name && x.Key.Type != product.Type);
                    if (toInstallProduct.Key != null)
                    {
                        var msg = string.Format("Product {0} as {1} in {2} will be installed double as {3}, cannot install", product.Name, product.Type, franchise.Name, toInstallProduct.Key.Type);
                        logger.Error(msg);
                        throw new System.Exception(msg);
                    }
                    if (!totalproductList.ContainsKey(product))
                        totalproductList.Add(product, franchise);
                }
            }

            // Step 2 : Group by franchise type
            foreach (var group in franchises.GroupBy(x => x.FranchiseType))
            {
                var list = new List<Product>();
                foreach (var franchise in group)
                {
                    foreach (var product in franchise.Products)
                    {
                        var installedProduct = installedProducts.FirstOrDefault(x => (useDefaults || !x.Key.IsDefault) && x.Key.productType == product.Name && x.Key.Id != product.Id.ToString());
                        if (installedProduct.Key != null)
                        {
                            var msg = string.Format("Product '{0}' already installed ({1}), cannot install", product.Name, installedProduct.Value);
                            logger.Error(msg);
                            throw new System.Exception(msg);
                        }
                        if (product.Type != franchise.FranchiseType)
                        {
                            var msg = string.Format("Product '{0}' has type {1}, but franchise has type {2}, cannot install", product.Name, product.Type, franchise.Type);
                            logger.Error(msg);
                            throw new System.Exception(msg);
                        }
                    }
                    AddProductsToList(franchise.Products, list);
                }

                // Step 3 : Install products 
                InstallProducts(list, group.Key, productsInstallPath);
                // Step 4 : Install misc. files
                InstallMiscFiles(productsInstallPath);
            }
        }

        private static void Readproducts(Dictionary<JsonProduct, string> list, string path, FranchiseType type, bool isDefault)
        {
            var typePath = Path.Combine(path, type == FranchiseType.Shop ? "Shop" : "Food");
            var jsonFile = Path.Combine(typePath, string.Format("{0}Products.json", type.ToString()));
            if (!File.Exists(jsonFile))
                return;

            var idFile = Path.Combine(typePath, string.Format("ACEOMM {0}Products.mod", type.ToString()));
            var products = JsonConvert.DeserializeObject<JsonProducts>(File.ReadAllText(jsonFile));
            foreach (var product in products.array)
            {
                product.IsDefault = isDefault;
                product.Type = type;
                list.Add(product, jsonFile);
            }

            if (!File.Exists(idFile))
                return;

            var ids = File.ReadAllLines(idFile);
            if ( products.array.Count != ids.Length)
                return;

            for (var i = 0; i < ids.Length; i++)
                products.array[i].Id = ids[i];
        }

        private static void ReadBusinesses(Dictionary<JsonBusiness, string> list, string path, BusinessType type, FranchiseType franchiseType, bool isDefault)
        {
            var typePath = type == BusinessType.Franchise 
                ? franchiseType == FranchiseType.Shop 
                    ? Path.Combine(path, "ShopFranchises")
                    : Path.Combine(path, "FoodFranchises")
                : Path.Combine(path, string.Format("{0}s", type.ToString()));

            if (!Directory.Exists(typePath))
                return;

            var businessPaths = Directory.EnumerateDirectories(typePath);
            foreach (var businessPath in businessPaths)
            {
                var jsonFile = Directory.EnumerateFiles(businessPath, "*.json").First();
                
                var idFile = Path.Combine(businessPath, string.Format("ACEOMM.mod"));
                string id = string.Empty;

                if (File.Exists(idFile))
                    id = File.ReadAllText(idFile);

                JsonBusiness business;
                if (type == BusinessType.Franchise)
                    business = JsonConvert.DeserializeObject<JsonFranchise>(File.ReadAllText(jsonFile));
                else
                    business = JsonConvert.DeserializeObject<JsonBusiness>(File.ReadAllText(jsonFile));

                business.IsDefault = isDefault;
                business.Id = id;
                list.Add(business, jsonFile);
            }
        }

        private static Dictionary<JsonBusiness, string> GetInstalledBusinesses()
        {
            var result = new Dictionary<JsonBusiness, string>();
            var companiesInstallPath = InstallPath + CompaniesPath;
            var modDirectories = Directory.EnumerateDirectories(companiesInstallPath);
            foreach (var modDirectory in modDirectories)
            {
                var isDefault = Path.GetFileName(modDirectory.TrimEnd(Path.DirectorySeparatorChar)) == "default";
                ReadBusinesses(result, modDirectory, BusinessType.Airline, FranchiseType.Unknown, isDefault);
                ReadBusinesses(result, modDirectory, BusinessType.AviationFuelSupplier, FranchiseType.Unknown, isDefault);
                ReadBusinesses(result, modDirectory, BusinessType.Bank, FranchiseType.Unknown, isDefault);
                ReadBusinesses(result, modDirectory, BusinessType.Contractor, FranchiseType.Unknown, isDefault);
                ReadBusinesses(result, modDirectory, BusinessType.Franchise, FranchiseType.Unknown, isDefault);
                ReadBusinesses(result, modDirectory, BusinessType.Franchise, FranchiseType.Shop, isDefault);
            }
            return result;
        }

        private static Dictionary<JsonProduct, string> GetInstalledProducts()
        {
            var result = new Dictionary<JsonProduct, string>();
            var productsInstallPath = InstallPath + ProductsPath;
            var modDirectories = Directory.EnumerateDirectories(productsInstallPath);
            foreach (var modDirectory in modDirectories)
            {
                var isDefault = Path.GetFileName(modDirectory.TrimEnd(Path.DirectorySeparatorChar)) == "default";
                Readproducts(result, modDirectory, FranchiseType.Shop, isDefault);
                Readproducts(result, modDirectory, FranchiseType.Restaurant, isDefault);
                Readproducts(result, modDirectory, FranchiseType.Bakery, isDefault);
                Readproducts(result, modDirectory, FranchiseType.Bar, isDefault);
            }
            return result;
        }

        private static bool DefaultsFileValue(string filename)
        {
            return File.ReadAllText(filename) == "TRUE";
        }

        public static void Install(Mod mod)
        {
            if (mod == null)
                return;

            logger.Info("Installing {0}", mod.Name);

            logger.Info("Getting already installed products");
            var installedProducts = GetInstalledProducts();
            logger.Info("Found {0} installed products", installedProducts.Count);
            logger.Info("Getting already installed businesses");
            var installedBusinesses = GetInstalledBusinesses();
            logger.Info("Found {0} installed businesses", installedBusinesses.Count);

            var businessesUseDefaults = DefaultsFileValue(InstallPath + CompaniesPath + "use_default.txt");
            var productsUseDefaults = DefaultsFileValue(InstallPath + ProductsPath + "use_default.txt");

            InstallBusinesses(mod, installedBusinesses, businessesUseDefaults);
            InstallProducts(mod, installedProducts, productsUseDefaults);
            logger.Info("Installed {0} businesss", mod.Businesses.Count);
        }

        public static void Uninstall(Mod mod)
        {
            if (mod == null)
                return;

            logger.Info("Uninstalling {0}", mod.Name);

            var installedProducts = GetInstalledProducts();
            var installedBusinesses = GetInstalledBusinesses();

            // TODO: Uninstall

            logger.Info("Uninstalled {0} businesss", mod.Businesses.Count);
        }
    }
}
