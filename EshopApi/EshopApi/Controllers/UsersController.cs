using Core.Contracts;
using Core.DTO.User;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EshopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers([FromQuery]GetUsersByFilter filter)
        {
            var users = _userService.GetAllUsers(filter);

            var result = new ObjectResult(users);

            return result;
        }

        [HttpGet]
        [Route("GetUser/{userId:Int}")]
        public IActionResult GetUser(int userId)
        {
            var user = _userService.GetUserById(userId);

            if (user != null) return Ok(user);

            return NotFound();
        }

        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                if (!_userService.IsExistUserByUserName(user.Name))
                {
                    _userService.InsertUser(user);
                    return CreatedAtAction("GetUser", new { userId = user.UserId }, user);
                }

                ModelState.AddModelError("Name", "نام کاربری قبلا ثبت شده است");
                return ValidationProblem(ModelState);
            }

            return ValidationProblem(ModelState);
        }
    }
}