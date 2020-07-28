using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiteHouse.Core.Domain
{
    public class Region
    {
        public Region()
        {
            //this.Departements = new HashSet<Departement>();
        }

        public int IdRegion { get; set; }
        public string Nom { get; set; }

        //public virtual ICollection<Departement> Departements { get; set; }
    }
}