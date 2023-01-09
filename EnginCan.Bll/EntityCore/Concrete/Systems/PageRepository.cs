using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnginCan.Bll.EntityCore.Abstract.Systems;
using EnginCan.Bll.Helpers;
using EnginCan.Core.Middleware;
using EnginCan.Core.Utilities.Results.Abstract;
using EnginCan.Core.Utilities.Results.Concrete;
using EnginCan.Dal.EfCore;
using EnginCan.Dal.EfCore.Abstract;
using EnginCan.Dal.EfCore.Concrete;
using EnginCan.Entity.Models.Systems;
using EnginCan.Entity.Shared;
using Microsoft.EntityFrameworkCore;

namespace EnginCan.Bll.EntityCore.Concrete.Systems
{
    public class PageRepository : EntityBaseRepository<Page>, IPageRepository
    {
        private readonly IPagePermissionRepository _pagePermissionRepository;
        private readonly ICustomHttpContextAccessor _customHttpContextAccessor;
        public PageRepository(EnginCanContext context, IPagePermissionRepository pagePermissionRepository,
                              ICustomHttpContextAccessor customHttpContextAccessor) : base(context)
        {
            _pagePermissionRepository = pagePermissionRepository;
            _customHttpContextAccessor = customHttpContextAccessor;
        }

        /// <summary>
        /// Yeni sayfa oluşturur
        /// </summary>
        public IResult AddPage(Page page)
        {
            try
            {
                Add(page);
                Commit();
                return new SuccessResult(SystemConstants.AddedMessage);
            }
            catch (Exception)
            {
                return new ErrorResult(SystemConstants.AddedErrorMessage);
            }
        }

        
        /// <summary>
        /// Sayfa siler
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IResult DeletePage(int id)
        {
            var result = FindBy(a => a.Id == id).FirstOrDefault();
            if (result == null)
                return new ErrorResult(SystemConstants.NoData);

            else
            {
                try
                {
                    Delete(result);
                    Commit();
                    return new SuccessResult(SystemConstants.DeletedMessage);
                }
                catch (Exception)
                {
                    return new ErrorResult(SystemConstants.DeletedErrorMessage);
                }
            }
        }

        /// <summary>
        ///  Tüm Page verilerini getirir.
        /// </summary>
        /// <returns></returns>
        public IDataResult<IQueryable<Page>> GetAllPagePermissions()
        {
            var result = FindBy(x => x.MenuShow == true)
                                .OrderBy(a => a.ParentId)
                                .ThenBy(a => a.Order)
                                .Distinct()
                                .AsQueryable();
            return new SuccessDataResult<IQueryable<Page>>(result);
        }

        /// <summary>
        /// Tekil bilgisine göre sayfa getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IDataResult<Page> GetById(int id)
        {
            var result = FindBy(m =>  m.Id == id)
                       .FirstOrDefault();

            if (result != null)
                return new SuccessDataResult<Page>(result);
            else
                return new ErrorDataResult<Page>(null, SystemConstants.NoData);
        }

        /// <summary>
        /// Yetkili sayfaları döndürür
        /// </summary>
        /// <returns></returns>
        public IDataResult<List<Page>> GetPermissionPage()
        {
            if (_customHttpContextAccessor.GetUserRoleId().Contains(1))
            {
                List<Page> page = FindBy(x => x.MenuShow)
                                        .OrderBy(a => a.ParentId)
                                        .ThenBy(a => a.Order)
                                        .Distinct()
                                        .ToList();

                return new SuccessDataResult<List<Page>>(page);
            }
            else
            {
                List<Page> pages = _pagePermissionRepository.GetPagePermissionListCache(false).Where(w =>
                                                                       (w.UserId == _customHttpContextAccessor.GetUserId() || (w.RoleId.HasValue && _customHttpContextAccessor.GetUserRoleId().Contains(w.RoleId.Value))) &&
                                                                       !w.Forbidden &&
                                                                       w.DataStatus == Entity.Shared.DataStatus.Activated &&
                                                                       w.Page.MenuShow &&
                                                                       !_pagePermissionRepository
                                                                            .GetPagePermissionListCache(false).Where(w2 => (
                                                                            w2.UserId == _customHttpContextAccessor.GetUserId() ||
                                                                            (w2.RoleId.HasValue && _customHttpContextAccessor.GetUserRoleId().Contains(w2.RoleId.Value))) &&
                                                                            w2.Forbidden && w2.DataStatus == Entity.Shared.DataStatus.Activated)
                                                                .Select(a => a.PageId).Contains(w.PageId))
                                                                .Select(a => a.Page)
                                                                .OrderBy(a => a.ParentId)
                                                                .ThenBy(a => a.Order)
                                                                .Distinct()
                                                                .ToList();

                return new SuccessDataResult<List<Page>>(pages);
            }
        }

        /// <summary>
        /// Sayfa günceller
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IResult UpdatePage(Page page)
        {
            var hasData = FindBy(m =>  m.Id == page.Id)
                         .AsNoTracking()
                         .FirstOrDefault();
            if (hasData == null)
                return new ErrorDataResult<Page>(null, SystemConstants.NoData);

            try
            {
                Update(page);
                Commit();

                return new SuccessResult(SystemConstants.UpdatedMessage);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<Page>(null, SystemConstants.UpdatedErrorMessage);
            }

        }
    }
}
