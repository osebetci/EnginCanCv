using EnginCan.Bll.EntityCore.Abstract.Users;
using EnginCan.Dal.EfCore;
using EnginCan.Dal.EfCore.Concrete;
using EnginCan.Entity.Models.Users;

namespace EnginCan.Bll.EntityCore.Concrete.Users
{
    public class UserRoleRepository : EntityBaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(EnginCanContext context) : base(context)
        {
        }
    }
}