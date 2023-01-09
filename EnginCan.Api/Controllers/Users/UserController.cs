using EnginCan.Bll.EntityCore.Abstract.Systems;
using EnginCan.Bll.EntityCore.Abstract.Users;
using EnginCan.Core.Middleware;
using EnginCan.Dto.Systems;
using EnginCan.Entity.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnginCan.Api.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _service;
        private readonly IPagePermissionRepository _pagePermissionRepository;
        private readonly ICustomHttpContextAccessor _customHttpContextAccessor;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly ILookupRepository _lookupRepository;
        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// Yapıcı method.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="pagePermissionRepository"></param>
        /// <param name="userRoleRepository"></param>
        /// <param name="lookupRepository"></param>
        /// <param name="roleRepository"></param>
        /// <param name="customHttpContextAccessor"></param>
        public UserController(IUserRepository service,
            IPagePermissionRepository pagePermissionRepository,
            IUserRoleRepository userRoleRepository,
            ILookupRepository lookupRepository,
            IRoleRepository roleRepository,
            ICustomHttpContextAccessor customHttpContextAccessor)
        {
            _service = service;
            _pagePermissionRepository = pagePermissionRepository;
            _customHttpContextAccessor = customHttpContextAccessor;
            _userRoleRepository = userRoleRepository;
            _lookupRepository = lookupRepository;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// Tüm User verilerini getirir.
        /// </summary>
        [HttpGet, Route("GetAllUser")]
        [Authorize]
        [Produces("application/json")]
        public IActionResult GetAllUser()
        {
            var result = _service.GetAllUser();
            if (result.Success)
                return Ok(result);
            else
                return Ok(result);
        }

        /// <summary>
        /// Tekil bilgisine göre user döndürür
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
        /// Giriş yapan kullanıcı bilgilerini döndürür.
        /// </summary>
        [HttpGet, Route("LoginUser")]
        [Authorize]
        [Produces("application/json")]
        public IActionResult LoginUser()
        {
            var result = _service.LoginUser();
            if (result.Success)
                return Ok(result);
            else
                return Ok(result);
        }

        /// <summary>
        /// Yeni kullanıcı kaydı
        /// </summary>
        [HttpPost, Route("PostUser")]
        [Authorize]
        [Produces("application/json")]
        public IActionResult PostUser([FromBody] User val)
        {
            var result = _service.AddUser(val);
            if (result.Success)
                return Ok(result);
            else
                return Ok(result);
        }

        /// <summary>
        /// Kullanıcı kaydını değişen alanlara göre günceller.
        /// </summary>
        [HttpPost, Route("UpdateUser")]
        [Authorize]
        [Produces("application/json")]
        public IActionResult UpdateUser([FromBody] User val)
        {
            var result = _service.UpdateUser(val);
            if (result.Success)
                return Ok(result);
            else
                return Ok(result);
        }

        [HttpGet, Route("DeleteUser/{key:int}")]
        [Authorize]
        [Produces("application/json")]
        public IActionResult DeleteUser([FromRoute] int key)
        {
            var result = _service.DeleteUser(key);
            if (result.Success)
                return Ok(result);
            else
                return Ok(result);
        }

        /// <summary>
        /// Yeni giriş kaydı
        /// </summary>
        [HttpPost, Route("Authenticate")]
        [Produces("application/json")]
        public IActionResult Authenticate([FromBody] Login login)
        {
            var result = _service.Authenticate(login);
            if (result.Success)
                return Ok(result);
            else
                return Ok(result);
        }

        /// <summary>
        /// Select boxlar için kullanıcı listesi döndürür
        /// </summary>
        [HttpGet, Route("GetAllUsersForSelection")]
        [Authorize]
        [Produces("application/json")]
        public IActionResult GetAllUsersForSelection()
        {
            var result = _service.GetAllUsersForSelection();
            if (result.Success)
                return Ok(result);
            else
                return Ok(result);
        }



    }
}
