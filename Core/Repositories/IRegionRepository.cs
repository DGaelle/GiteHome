using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GiteHouse.Core.Domain;

namespace GiteHouse.Core.Repositories
{
    public interface IRegionRepository : IRepository<Region>
    {
        IEnumerable<Region> GetAllRegions();
        //IEnumerable<Departement> GetAllDepartementsRegion(int regionId);
    }
}