using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnginCan.Bll.EntityCore.Abstract.Systems;
using EnginCan.Entity.Models.Systems;
using EnginCan.Core.Middleware;

namespace EnginCan.Api.Controllers.Systems
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private readonly IPageRepository _service;
        private readonly IPagePermissionRepository _pagePermissionRepository;
        private readonly ICustomHttpContextAccessor _customHttpContextAccessor;

        /// <summary>
        /// Yapıcı method.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="pagePermissionRepository"></param>
        /// <param name="customHttpContextAccessor"></param>
        public PageController(IPageRepository service,
                              IPagePermissionRepository pagePermissionRepository,
                              ICustomHttpContextAccessor customHttpContextAccessor)
        {
            _service = service;
            _pagePermissionRepository = pagePermissionRepository;
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
            var result = _service.GetAllPagePermissions();
            if (result.Success)
                return Ok(result);
            else
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
            var result = _service.GetById(key);
            if (result.Success)
                return Ok(result);
            else
                return Ok(result);
        }

        /// <summary>
        /// Yeni page oluşturur
        /// </summary>
        [HttpPost, Route("PostPage")]
        [Authorize]
        [Produces("application/json")]
        public IActionResult PostPage([FromBody] Page page)
        {
            var result = _service.AddPage(page);
            if (result.Success)
                return Ok(result);
            else
                return Ok(result);
        }

        /// <summary>
        /// Page Günceller
        /// </summary>
        [HttpPost, Route("UpdatePage")]
        [Authorize]
        [Produces("application/json")]
        public IActionResult Update([FromBody] Page page)
        {
            var result = _service.UpdatePage(page);
            if (result.Success)
                return Ok(result);
            else
                return Ok(result);
        }

        /// <summary>
        /// Page Siler
        /// </summary>
        [HttpGet, Route("DeletePage/{key:int}")]
        [Authorize]
        [Produces("application/json")]
        public IActionResult DeletePage([FromRoute] int id)
        {
            var result = _service.DeletePage(id);
            if (result.Success)
                return Ok(result);
            else
                return Ok(result);
        }

        /// <summary>
        /// Tüm Page verilerini getirir.
        /// </summary>
        [HttpGet, Route("GetPermissionPage")]
 
        [Produces("application/json")]
        public IActionResult GetPermissionPage()
        {
            var result = _service.GetPermissionPage();
            if (result.Success)
                return Ok(result);
            else
                return Ok(result);
        }




    }
}
