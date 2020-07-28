using GiteHouse.Core;
using GiteHouse.Core.Repositories;
using GiteHouse.PersistenceInDBSqlServer.Repositories;

namespace GiteHouse.PersistenceInDBSqlServer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GiteHouseEntities _context;

        public UnitOfWork(GiteHouseEntities context)
        {
            _context = context;
            Regions = new RegionRepository(_context);
            //Departements = new DepartementRepository(_context);
        }

        public IRegionRepository Regions { get; private set; }
       // public IDepartementRepository Departement { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}