using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Contracts;
using Core.DTO.User;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EshopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody]RegisterUserDTO register)
        {
            if (ModelState.IsValid)
            {
                if (!_userService.IsExistUserByEmail(register.Email))
                {
                    var user = new User
                    {
                        Name = register.Name,
                        Family = register.Family,
                        Address = register.Address,
                        Password = register.Password,
                        Email = register.Email
                    };

                    _userService.AddUser(user);
                    return Ok(register);
                }

                ModelState.AddModelError("Email", "ایمیل وارد شده تکراری است");
            }

            return BadRequest(register);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = _userService.GetUserByEmail(login.Email);

            if (user == null) return Ok(new { result = "NotFound" });

            if (user.Password != login.Password) return Ok(new { result = "NotFound" });

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("OurVerifyEshopWebApi"));

            var singinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOption = new JwtSecurityToken(
                issuer: "http://localhost:13172",
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserId.ToString())
                },
                expires: DateTime.Now.AddDays(3),
                signingCredentials: singinCredentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);

            return Ok(new { result = "Done", token = tokenString, expireTime = 3, User = user });
        }

        [HttpGet]
        [Route("SignOut", Name = "SignOut")]
        public IActionResult LogOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(new { status = "success" });
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("IsExistUserByEmail")]
        public IActionResult IsExistUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return Ok(new { IsExist = false });

            return Ok(new { IsExist = _userService.IsExistUserByEmail(email) });
        }

        [Route("CheckAuthentication")]
        public IActionResult CheckAuthentication()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = Convert.ToInt32(User.Identity.Name);

                var user = _userService.GetUserById(userId);

                return Ok(new { status = true, User = user });
            }

            return BadRequest(new { status = false });
        }
    }
}