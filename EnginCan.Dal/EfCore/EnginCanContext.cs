using EnginCan.Dal.EfCore.Extensions;
using EnginCan.Entity.Models.Abouts;
using EnginCan.Entity.Models.Systems;
using EnginCan.Entity.Models.Users;
using EnginCan.Entity.Shared;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EnginCan.Dal.EfCore
{
    public class EnginCanContext : DbContext
    {
        public EnginCanContext(DbContextOptions<EnginCanContext> options) : base(options)
        {
        }


        #region Users
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        #endregion Users

        #region Systems
        public DbSet<Lookup> Lookup { get; set; }
        public DbSet<LookupType> LookupType { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PagePermission> PagePermissions { get; set; }
        #endregion

        #region Abouts

        public DbSet<About> About { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /// <summary>
            /// https://docs.microsoft.com/en-us/ef/core/modeling/indexes#indexes
            /// </summary>
            /// 

            /// IndexAttribute ile oluşturulan indexleri create eder.
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
                foreach (var prop in entity.GetProperties())
                    try
                    {
                        var attr = prop.PropertyInfo.GetCustomAttribute<IndexAttribute>();
                        if (attr != null)
                        {
                            var index = entity.AddIndex(prop);
                            index.IsUnique = attr.IsUnique;
                        }
                    }
                    catch
                    {
                    }

            // Seed
            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
