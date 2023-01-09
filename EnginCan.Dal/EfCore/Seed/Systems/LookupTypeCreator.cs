using Microsoft.EntityFrameworkCore;
using EnginCan.Entity.Models.Systems;

namespace EnginCan.Dal.EfCore.Seed.Systems
{
    public static class LookupTypeCreator
    {
        public static void Create(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LookupType>().HasData(new LookupType[] {
                new LookupType
                {
                    Id = LookupTypes.Gender,
                    Name = "Cinsiyet"
                },
                 new LookupType
                {
                    Id = LookupTypes.Country,
                    Name = "Ülke"
                },
                 new LookupType
                {
                    Id = LookupTypes.Province,
                    Name = "İl"
                },
                  new LookupType
                {
                    Id = LookupTypes.County,
                    Name = "İlçe"
                },
                  new LookupType
                {
                    Id = LookupTypes.Currency,
                    Name = "Döviz"
                }
            });
        }
    }
}
