using Common;
using Common.Commons;
using Example_Project.Services.Implement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.EF;
using Repository.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Example_Project.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService usertService)
        {
            _userService = usertService;
        }

        [HttpPost]
        //[Authorized]
        [Route("get-all")]
        ////[PermissionAttributeFilter("User Management", "access")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<List<BCC01_User>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<List<BCC01_User>>))]
        public async Task<IActionResult> GetAll()
        {
            ResponseService<List<BCC01_User>> response = await _userService.GetAll();
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.exception);
            }
        }

    }
}
