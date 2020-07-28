using System.Data.Entity.ModelConfiguration;
using GiteHouse.Core.Domain;

namespace GiteHouse.PersistenceInDBSqlServer.EntityConfigurations
{
    public class RegionConfiguration : EntityTypeConfiguration<GiteHouse.Core.Domain.Region>
    {
        public RegionConfiguration()
        {
            Property(r => r.Nom)
                .IsRequired()
                .HasMaxLength(32);
        }
    }
}