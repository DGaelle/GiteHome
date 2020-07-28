using GiteHouse.PersistenceInDBSqlServer.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GiteHouse.PersistenceInDBSqlServer
{
    public class GiteHouseContext:DbContext
    {
        public GiteHouseContext()
            : base("name=GiteHouseEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Departement> Departements { get; set; }
        public virtual DbSet<Ville> Villes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RegionConfiguration());
        }
    }
}