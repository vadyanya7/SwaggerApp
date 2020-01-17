using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SwaggerApp.Models;
using SwaggerApp;
using SwaggerApp.Services;
// класс Person

namespace TokenApp.Controllers
{
    public class AccountController : Controller
    {
  
        private IAuthenticateService _service;
        
        public AccountController(IAuthenticateService service)
        {
            _service = service;
        }

        [HttpPost("/token")]
        public IActionResult Token(string username, string password)
        {
            var identity = _service.GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }
            var response = _service.Token(identity);

            return Json(response);
        }       
    }
}