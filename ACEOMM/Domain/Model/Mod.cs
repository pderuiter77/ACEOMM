using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ACEOMM.Domain.Model
{
    public class Mod : Entity
    {
        public Mod()
        {
            Businesses = new ObservableCollection<Business>();
        }

        public ObservableCollection<Business> Businesses { get; set; }

        private static Mod _unknownMod = new Mod { Id = Guid.Empty, Name = "Unknown", Status = EntityStatus.Unchanged };
        public static Mod UnknownMod { get { return _unknownMod; } }
    }
}
