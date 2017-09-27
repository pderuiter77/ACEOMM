using System;
using System.IO;
using System.Net;
using System.Reflection;

namespace ACEOMM.Services
{
    public static class UpdateService
    {
        public static bool CheckForUpdates()
        {
            var currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            using (var client = new WebClient())
            {
                client.DownloadFile("https://drive.google.com/uc?export=download&id=0B_AYSTz1eo1ANU9na19uRFY2YmM", @".\Data\RemoteVersion.txt");
                var remoteVersion = File.ReadAllText(@".\Data\RemoteVersion.txt");
                return remoteVersion != currentVersion;
            }
        }
    }
}
