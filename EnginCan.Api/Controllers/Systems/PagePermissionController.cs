using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnginCan.Bll.EntityCore.Abstract.Systems;
using EnginCan.Dto.Shared;
using EnginCan.Entity.Models.Systems;
using EnginCan.Entity.Shared;
using EnginCan.Core.Middleware;

namespace EnginCan.Api.Controllers.Systems
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagePermissionController : ControllerBase
    {
        private readonly IPagePermissionRepository _service;
        private readonly ICustomHttpContextAccessor _customHttpContextAccessor;

        /// <summary>
        /// Yapıcı method.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="customHttpContextAccessor"></param>
        public PagePermissionController(IPagePermissionRepository service, ICustomHttpContextAccessor customHttpContextAccessor)
        {
            _service = service;
            _customHttpContextAccessor = customHttpContextAccessor;
        }

        /// <summary>
        /// Tüm Page verilerini getirir.
        /// </summary>
        /// <param name="sicilId">Bordro tekil bilgisidir.</param>
        /// <returns>Istenen bordro detay bilgisini döndürür.</returns>
        [HttpGet, Route("GetAllPagePermissions")]
        [Authorize]
        [Produces("application/json")]
        public IActionResult GetAllPagePermissions()
        {
            var result = _service.FindBy(m => m.DataStatus == Entity.Shared.DataStatus.Activated);
            return Ok(result);
        }

        /// <summary>
        /// Tekil bilgisine göre page döndürür
        /// </summary>
        [HttpGet, Route("GetById/{key:int}")]
        [Authorize]
        [Produces("application/json")]
        public IActionResult GetById([FromRoute] int key)
        {
            var result = _service.FindBy(a => a.Id == key).FirstOrDefault();
            if (result != null)
                return Ok(result);
            else
                return BadRequest("İstenilen sayfa yetkisi bulunamadı!");
        }

        /// <summary>
        /// Yeni page oluşturur
        /// </summary>
        [HttpPost, Route("PostPagePermission")]
        [Authorize]
        [Produces("application/json")]
        public IActionResult PostPagePermission([FromBody] CustomPostPagePermission pagePermission)
        {
            try
            {
                var response = _service.CustomPost(pagePermission);
                return Ok(response);
                _service.Commit();
                return Ok("İşlem Başarılı!");
            }
            catch (Exception e)
            {
                return BadRequest("Sayfa yetkisi eklenirken bir hata oluştu!");
            }
        }

        /// <summary>
        /// Tekil bilgisine göre page döndürür
        /// </summary>
        [HttpGet, Route("CanActivate/{key:int}")]
        [Authorize]
        [Produces("application/json")]
        public IActionResult CanActivate([FromRoute] int key)
        {
            var page = _service.FindBy(a =>
            (a.UserId == _customHttpContextAccessor.GetUserId() || (a.RoleId.HasValue && _customHttpContextAccessor.GetUserRoleId().Contains(a.RoleId.Value))) && a.PageId == key && a.DataStatus == DataStatus.Activated)
                                                .OrderByDescending(a => a.Forbidden)
                                                .Select(a => new { a.Page.Name, BreadCrumb = a.Page.AllName, IsAuthority = true, a.Forbidden })
                                                .FirstOrDefault();

            if (page != null)
            {
                if (page.Forbidden)
                {
                    return Ok(new
                    {
                        Name = "Bu sayfayı görüntülemeye yetkiniz yok. Lütfen sistem yöneticinizden yetki talep edin.",
                        IsAuthority = false
                    });
                }
                return Ok(page);
            }

            return Ok(new
            {
                Name = "Bu sayfayı görüntülemeye yetkiniz yok. Lütfen sistem yöneticinizden yetki talep edin.",
                IsAuthority = false
            });
        }

        /// <summary>
        /// Page Günceller
        /// </summary>
        [HttpPost, Route("UpdatePagePermission")]
        [Authorize]
        [Produces("application/json")]
        public IActionResult UpdatePagePermission([FromBody] PagePermission pagePermission)
        {
            try
            {
                var hasData = _service.FindBy(m => m.Id == pagePermission.Id).FirstOrDefault();
                if (hasData == null)
                    return BadRequest("Güncellenmek istenilen sayfa yetkisi bulunamadı!");
                else
                {
                    _service.Update(pagePermission);
                    _service.Commit();
                    return Ok("İşlem Başarılı!");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Sayfa yetkisi eklenirken bir hata oluştu!");
            }
        }

        /// <summary>
        /// Page Siler
        /// </summary>
        [HttpGet, Route("DeletePagePermission/{key:int}")]
        [Authorize]
        [Produces("application/json")]
        public IActionResult DeletePagePermission([FromRoute] int id)
        {
            try
            {
                var hasData = _service.FindBy(m => m.Id == id).FirstOrDefault();
                if (hasData == null)
                    return BadRequest("Silinmek istenilen sayfa bulunamadı!");
                else
                {
                    _service.Delete(hasData);
                    _service.Commit();
                    return Ok("İşlem Başarılı!");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Sayfa eklenirken bir hata oluştu!");
            }
        }

    }
}
