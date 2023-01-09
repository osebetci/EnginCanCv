using EnginCan.Bll.EntityCore.Abstract.Systems;
using EnginCan.Dal.EfCore;
using EnginCan.Dal.EfCore.Concrete;
using EnginCan.Dto.Shared;
using EnginCan.Entity.Models.Systems;
using EnginCan.Entity.Shared;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnginCan.Bll.EntityCore.Concrete.Systems
{
    public class PagePermissionRepository : EntityBaseRepository<PagePermission>, IPagePermissionRepository
    {
        private readonly IMemoryCache _memoryCache;

        public PagePermissionRepository(EnginCanContext context, IMemoryCache memoryCache) : base(context)
        {
            _memoryCache = memoryCache;
        }

        private async Task AddNewPagePermissionList(IEnumerable<int> addPageIds, CustomPostPagePermission val)
        {
            List<PagePermission> newPagePermissions = new List<PagePermission>();
            foreach (var pageId in addPageIds)
            {
                newPagePermissions.Add(new PagePermission
                {
                    Id = 0,
                    UserId = val.UserId,
                    RoleId = val.RoleId,
                    Forbidden = val.Forbidden,
                    PageId = pageId,
                });
            }

            await BulkInsertAsync(newPagePermissions);
        }

        public async Task<Response> CustomPost(CustomPostPagePermission val)
        {
            try
            {
                var oldPagePermissions = FindBy(x => x.RoleId == val.RoleId &&
                                                     x.UserId == val.UserId &&
                                                     x.DataStatus == DataStatus.Activated);

                if (oldPagePermissions.Count() == 0)
                {
                    await AddNewPagePermissionList(val.PageIds, val);
                }
                else
                {
                    var removePagePermissions = oldPagePermissions.Where(x => !val.PageIds.Contains(x.PageId));
                    await BulkDeleteAsync(removePagePermissions.ToList());

                    var addPageIds = val.PageIds.Where(x => !oldPagePermissions.Select(y => y.PageId).Contains(x));
                    await AddNewPagePermissionList(addPageIds, val);
                }

                GetPagePermissionListCache(true);
                return new Response(true, "Kayıt başarılı.");
            }
            catch
            {
                return new Response(false, "Kayıt esnasında bir hata ile karşılaşıldı.");
            }
        }

        public string GetFisrtFireLink(int[] allUserRoles) =>
            GetPagePermissionListCache(false).Where(a => a.RoleId.HasValue && allUserRoles.Contains(a.RoleId.Value))
                .Select(a => a.Page)
                .Where(a => a.ParentId == null)
                .OrderBy(a => a.Order)
                .FirstOrDefault()?
                .AllRouterLink;

        public List<MenuPage> GetMenuPageListCache(bool isRefresh)
        {
            if (isRefresh || !_memoryCache.TryGetValue("GetMenuPageListCache", out List<MenuPage> data))
            {
                data = AllIncluding(s => s.Page).Select(a => new MenuPage
                {
                    Id = a.PageId,
                    ParentId = a.Page.ParentId,
                    Icon = a.Page.Icon,
                    AllRouterLink = a.Page.AllRouterLink,
                    Name = a.Page.Name,
                    Order = a.Page.Order,
                    UserId = a.UserId,
                    Forbidden = a.Forbidden,
                    RoleId = a.RoleId,
                    MenuShow = a.Page.MenuShow
                }).ToList();
                _memoryCache.Set("GetMenuPageListCache", data, new MemoryCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddMinutes(30) });
            }

            return data;
        }

        public List<PagePermission> GetPagePermissionListCache(bool isRefresh)
        {
            if (isRefresh || !_memoryCache.TryGetValue("GetPagePermissionListCache", out List<PagePermission> data))
            {
                data = FindBy(a => a.DataStatus == DataStatus.Activated).Select(a => new PagePermission
                {
                    Id = a.Id,
                    RoleId = a.RoleId,
                    UserId = a.UserId,
                    Forbidden = a.Forbidden,
                    PageId = a.PageId,
                    Page = a.Page,
                    DataStatus = a.DataStatus
                }).ToList();
                _memoryCache.Set("GetPagePermissionListCache", data, new MemoryCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddMinutes(30) });
            }

            return data;
        }

    }
}
