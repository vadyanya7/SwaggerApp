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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
// класс Person

namespace TokenApp.Controllers
{
    public class AccountController : Controller
    {
  
        private IAuthenticateService _service;
        private UserManager<Account> _userManager;
        public AccountController(IAuthenticateService service, UserManager<Account> userManager)
        {
            _service = service;
            _userManager = userManager;

        }

        [HttpPost("/token")]
        public IActionResult Token([FromBody] Account model)
        {
            var identity = _service.GetIdentity( model);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }
            var response = _service.Token(identity);

            return Json(response);
        }

        [HttpPost("/add")]
        public Task<Account> Add([FromBody] Account model)
        {
            var acc = _service.AddAccount(model);
            return acc;
        }

        [HttpPut("/update")]
        public void Update(string userName,[FromBody] Account account)
        {

            _service.UpdateAccount(userName, account);

        }

        [HttpDelete("/delete")]
        public void Delete(string userName)
        {
            _service.DeleteAccount(userName);

        }

        [HttpGet("/get")]
        public IEnumerable<Account> Get()
        {
            var accounts = _service.GetAccounts();

            return accounts;
        }

        [HttpGet("{id}")]
        public Account Get(string userName)
        {
            var office = _service.GetAccount(userName);
            return office;
        }
    }
}