using EnginCan.Entity.Models.Systems;
using EnginCan.Entity.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EnginCan.Dal.EfCore.Seed.Systems
{
    public static class PagePermissionCreator
    {
        public static void Create(ModelBuilder modelBuilder)
        {
            // Admin tüm sayfalara yetkili
            modelBuilder.Entity<PagePermission>().HasData(PageCreator.AllPages.Select(page => new PagePermission
            {
                Id = page.Id,
                RoleId = 1,
                PageId = page.Id,
                Forbidden = false,
                DataStatus = DataStatus.Activated,
                CreatedUserId = 1,
                CreatedAt = new DateTime(2020, 03, 09)
            }));
        }
    }
}
