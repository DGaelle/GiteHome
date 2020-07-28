using GiteHouse.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using GiteHouse.PersistenceInDBSqlServer;

namespace GiteHouse.PersistenceInDBSqlServer.Repositories
{
    public class RegionRepository : Repository<Region>, IRegionRepository
    {
        public RegionRepository(GiteHouseEntities context)
           : base(context)
        {
        }

        public IEnumerable<Region> GetAllRegions()
        {
            return GetAll();
        }

        //public IEnumerable<Departement> GetAllDepartementsRegion(int regionId)
        //{
        //    throw new NotImplementedException();
        //}

        public GiteHouseContext GiteHouseContext
        {
            get { return Context as GiteHouseContext; }
        }
    }
}
