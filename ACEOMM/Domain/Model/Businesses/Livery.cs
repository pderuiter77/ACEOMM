﻿using System.Linq;

namespace ACEOMM.Domain.Model.Businesses
{
    public class Livery
    {
        public string Aircraft { get; set; }
        public string Path { get; set; }
        public string Airline { get; set; }
        public string Author { get; set; }

        private static Livery _unknownLivery = new Livery { Aircraft = "Unknown", Airline = "Unknown" };

        public static Livery UnknownLivery { get { return _unknownLivery; } }

        public string Name 
        {
            get { return string.Format("{0}_{1}_{2}", Aircraft, Airline, Author); }
        }

        public string LinkPath
        {
            get
            {
                return Path.Split('\\').Last();
            }
        }
    }
}
