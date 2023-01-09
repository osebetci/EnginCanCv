using EnginCan.Bll.EntityCore.Abstract.Users;
using EnginCan.Dal.EfCore;
using EnginCan.Dal.EfCore.Concrete;
using EnginCan.Entity.Models.Users;

namespace EnginCan.Bll.EntityCore.Concrete.Users
{
    public class RoleRepository : EntityBaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(EnginCanContext context) : base(context)
        {
        }
    }
}