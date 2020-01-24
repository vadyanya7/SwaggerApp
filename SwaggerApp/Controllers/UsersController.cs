using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swagger.Models;
using SwaggerApp.Services;
using SwaggerApp.Models;
using SwaggerApp;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SwaggerApp.Models.ViewModels;

namespace SwaggerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UsersController(IUserService service , IMapper mapper)
        {
            _userService = service;
            _mapper = mapper;
        }


        [HttpPost("/token")]
        public async Task<IActionResult> Token(string userName,string password)
        {
            var identity = await _userService.GetIdentity(userName,password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }
            var response = _userService.Token(identity);

            return Json(response);
        }
        [HttpGet]
        public IEnumerable<UserModel> Get()
        {
            var listModel = new List<UserModel>();
            var users = _userService.GetUsers();
            foreach (var l in users)
            {
                listModel.Add(_mapper.Map<User, UserModel>(l));
            }

            return listModel;
        }

        [HttpGet("{userName}")]
        public async Task<UserModel> Get(string userName)
        {
            var user = await  _userService.GetUser(userName);
            var df = _mapper.Map<User, UserModel>(user);
            return df;
        }

        [HttpPost]
        public UserModel Post([FromBody] UserModel userModel,string password)
        {
            var user = _mapper.Map<UserModel, User>(userModel);
            var sd = _userService.AddUser(user, password);
            return userModel;
        }

        [HttpPut("{userName}")]
        public UpdateUserModel Put(string userName, [FromBody] UpdateUserModel userModel)
        {
            var user = _mapper.Map<UpdateUserModel, User>(userModel);
            var sr = _userService.UpdateUser(userName, user);
            return userModel;
        }
        [HttpDelete("{userName}")]
        public Task<IdentityResult> Delete(string userName)
        {
           return _userService.DeleteUser(userName);
        }
    }
}
