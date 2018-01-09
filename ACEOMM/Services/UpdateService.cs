using System;
using System.IO;
using System.Net;
using System.Reflection;

namespace ACEOMM.Services
{
    public static class UpdateService
    {
        public static string Version { get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }
        public static string ReleaseNotes { get; private set; }
        public static bool CheckForUpdates()
        {
            var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
            using (var client = new WebClient())
            {
                client.DownloadFile("https://drive.google.com/uc?export=download&id=1zGvyVFJ-gcKjhpx_pj7nU-x-BKgwjdbg", @".\Data\RemoteVersion.txt");
                var remoteText = File.ReadAllLines(@".\Data\RemoteVersion.txt");
                ReleaseNotes = string.Join("\r\n", remoteText);
                var remoteVersion = new Version(remoteText[0]);
                
                return remoteVersion.Major > currentVersion.Major || remoteVersion.Minor > currentVersion.Minor || remoteVersion.Build > currentVersion.Build;
            }
        }

        public static bool DownloadDataSheet()
        {
            const string sheetUrl = "https://spreadsheets.google.com/feeds/download/spreadsheets/Export?key=1tMljvFW3_C3WhZhd94ygFXW3BJGdALayji7CTOwSsfY&gid={0}&exportFormat=tsv";
            using (var client = new WebClient())
            {
                client.DownloadFile(string.Format(sheetUrl, "583703764"), @".\Data\Import\Airlines.txt");
                client.DownloadFile(string.Format(sheetUrl, "230860337"), @".\Data\Import\Banks.txt");
                client.DownloadFile(string.Format(sheetUrl, "0"), @".\Data\Import\Contractors.txt");
                client.DownloadFile(string.Format(sheetUrl, "1357197754"), @".\Data\Import\Restaurants.txt");
                client.DownloadFile(string.Format(sheetUrl, "879697771"), @".\Data\Import\Shops.txt");
                client.DownloadFile(string.Format(sheetUrl, "1173787397"), @".\Data\Import\AviationFuelSuppliers.txt");
                client.DownloadFile(string.Format(sheetUrl, "71216677"), @".\Data\Import\ShopProducts.txt");
                client.DownloadFile(string.Format(sheetUrl, "1174509655"), @".\Data\Import\RestaurantProducts.txt");
                return true;
            }
        }
    }
}
