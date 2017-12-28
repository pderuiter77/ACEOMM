using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace ACEOMM.Domain.Model.Businesses
{
    public class Airline : Business
    {
        public const string UnknownIATA = "ZZ";

        public Airline()
        {
            Liveries = new ObservableCollection<Livery>();
            Type = BusinessType.Airline;
        }

        private string _iata;
        public string IATA
        {
            get { return _iata; }
            set { SetProperty(ref _iata, value); }
        }

        private string _icao;
        public string ICAO
        {
            get { return _icao; }
            set { SetProperty(ref _icao, value); }
        }

        public ObservableCollection<Livery> Liveries { get; set; }
    }
}
