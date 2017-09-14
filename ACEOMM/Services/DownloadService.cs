using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using NLog;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

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
                logger.Error(ex.Message);
                return ex.Message;
            }
        }

        public static string DownloadProductLogo(Product product, string path)
        {
            if (string.IsNullOrWhiteSpace(product.Logo.RemoteUrl))
                return string.Empty;
            product.Status = EntityStatus.Modified;
            var result = Download(product.Logo, Path.Combine(path, @"Products\"), product.Name);
            if (!string.IsNullOrWhiteSpace(result))
            {
                result = string.Format("Failed downloading logo for product '{0}' : {1}", product.Name, result);
                logger.Error(result);
                return result;
            }
            return string.Empty;
        }

        public static string DownloadBusinessLogo(Business business, string path)
        {
            if (string.IsNullOrWhiteSpace(business.Logo.RemoteUrl))
                return string.Empty;
            business.Status = EntityStatus.Modified;
            var result = Download(business.Logo, Path.Combine(path, @"Businesses\", business.GetType().Name), business.Name);
            if (!string.IsNullOrWhiteSpace(result))
            {
                result = string.Format("Failed downloading logo for business '{0}' : {1}", business.Name, result);
                logger.Error(result);
                return result;
            }
            return string.Empty;
        }
    }
}
