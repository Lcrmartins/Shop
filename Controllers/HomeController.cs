using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shop.Models;
using shop.Repositories;
using shop.Services;

namespace shop.Controllers
{
    [Route("v1/account")]
    public class HomeController : ControllerBase
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromBody] User model)
            {
            var user = UserRepository.GetUser(model.Username, model.Password);
            if (user==null)
                return NotFound("User and/or passord invalid.");

            var token = TokenService.GenerateToken(user);
            user.Password = "";

            return new { user, token };
        }


        [HttpGet("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anonymous";


        [HttpGet("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Authenticated - {0}", User.Identity.Name);


        [HttpGet("employee")]
        [Authorize(Roles = "employee, manager")]
        public string Employee() => "Employee";


        [HttpGet("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Manager";
    }
}