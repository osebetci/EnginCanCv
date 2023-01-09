using EnginCan.Dal.EfCore.Seed.Systems;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnginCan.Dal.EfCore.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region Users
            RoleCreator.Create(modelBuilder);
            UserRoleCreator.Create(modelBuilder);
            UserCreator.Create(modelBuilder);
            #endregion Users

            #region Systems
            LookupTypeCreator.Create(modelBuilder);
            PageCreator.Create(modelBuilder);
            PagePermissionCreator.Create(modelBuilder);
            #endregion Systems

        }
    }
}
