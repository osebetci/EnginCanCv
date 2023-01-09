using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EnginCan.Dal.EfCore.Abstract;
using EnginCan.Dto.Shared;
using EnginCan.Entity.Models.Systems;

namespace EnginCan.Bll.EntityCore.Abstract.Systems
{
    public interface IPagePermissionRepository : IEntityBaseRepository<PagePermission>
    {
        Task<Response> CustomPost(CustomPostPagePermission val);

        string GetFisrtFireLink(int[] allUserRoles);

        List<PagePermission> GetPagePermissionListCache(bool isRefresh);

        List<MenuPage> GetMenuPageListCache(bool isRefresh);
    }
}
