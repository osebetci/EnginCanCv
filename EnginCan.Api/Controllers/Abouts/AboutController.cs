using EnginCan.Bll.EntityCore.Abstract.Abouts;
using EnginCan.Bll.EntityCore.Abstract.Systems;
using EnginCan.Core.Middleware;
using EnginCan.Entity.Models.Abouts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnginCan.Api.Controllers.Abouts
{

    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IAboutRepository _service;

        /// <summary>
        /// Yapıcı method.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="AboutPermissionRepository"></param>
        /// <param name="customHttpContextAccessor"></param>
        public AboutController(IAboutRepository service)
        {
            _service = service;
        }

        /// <summary>
        /// Tüm About verilerini getirir.
        /// </summary>
        /// <param name="sicilId">Bordro tekil bilgisidir.</param>
        /// <returns>Istenen bordro detay bilgisini döndürür.</returns>
        [HttpGet, Route("GetAllAbouts")]
        [Authorize]
        [Produces("application/json")]
        public IActionResult GetAllAbouts()
        {
            var result = _service.GetAllAbouts();
            if (result.Success)
                return Ok(result);
            else
                return Ok(result);
        }

        /// <summary>
        /// Tekil bilgisine göre About döndürür
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
        /// Yeni About oluşturur
        /// </summary>
        [HttpPost, Route("PostAbout")]
        [Authorize]
        [Produces("application/json")]
        public IActionResult PostAbout([FromBody] About About)
        {
            var result = _service.AddAbout(About);
            if (result.Success)
                return Ok(result);
            else
                return Ok(result);
        }

        /// <summary>
        /// About Günceller
        /// </summary>
        [HttpPost, Route("UpdateAbout")]
        [Authorize]
        [Produces("application/json")]
        public IActionResult Update([FromBody] About About)
        {
            var result = _service.UpdateAbout(About);
            if (result.Success)
                return Ok(result);
            else
                return Ok(result);
        }

        /// <summary>
        /// About Siler
        /// </summary>
        [HttpGet, Route("DeleteAbout/{key:int}")]
        [Authorize]
        [Produces("application/json")]
        public IActionResult DeleteAbout([FromRoute] int id)
        {
            var result = _service.DeleteAbout(id);
            if (result.Success)
                return Ok(result);
            else
                return Ok(result);
        }
    }
}
