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
    }
}
