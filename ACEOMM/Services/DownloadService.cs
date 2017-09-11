using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using NLog;
using System;
using System.IO;
using System.Net;

namespace ACEOMM.Services
{
    public class DownloadService
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        private static string Download(Logo logo, string path, string filename)
        {
            logger.Info("Downloading logo at url {0}", logo.RemoteUrl);
            if (string.IsNullOrWhiteSpace(logo.RemoteUrl))
                return string.Empty;

            var localFilename = string.Format("{0}.png", filename);
            var localPath = Path.GetFullPath(string.Format(@"{0}\{1}", path, localFilename));
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(logo.RemoteUrl, localPath);
                    logo.LocalFilename = localFilename;
                    logger.Info("Logo downloaded as {0}", localPath);
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Failed downloading logo : " + ex.Message);
                throw;
            }
        }

        public static string DownloadProductLogo(Product product, string path)
        {
            product.Status = EntityStatus.Modified;
            return Download(product.Logo, Path.Combine(path, "/Products/"), product.Name);
        }

        public static string DownloadBusinessLogo(Business business, string path)
        {
            business.Status = EntityStatus.Modified;
            return Download(business.Logo, Path.Combine(path, "Businesses/", business.GetType().Name), business.Name);
        }
    }
}
