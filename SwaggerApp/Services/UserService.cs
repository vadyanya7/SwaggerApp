using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Swagger.Models;
using SwaggerApp.Models;
using SwaggerApp.Repositories;

namespace SwaggerApp.Services
{
    public class UserService : IUserService
    {
        private readonly IRepoUser _users;
        private readonly UserManager<User> _userManager;
        public UserService(IRepoUser userRepository, UserManager<User> userManager)
        {
            _users = userRepository;
            _userManager = userManager; 
        }
        public async Task<ClaimsIdentity> GetIdentity(string userName, string password)
        {
            var person = await _userManager.FindByNameAsync(userName);
                          
            if (await _userManager.CheckPasswordAsync(person, password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.UserName),
                };
                var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            // если пользователя не найдено
            return null;
        }
        public ResponseModel Token(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new ResponseModel
            {
                Access_token = encodedJwt,
                UserName = identity.Name
            };

        }
        public async Task<IdentityResult> AddUser(User user, string password)
        {
            var result2 = await  _userManager.CreateAsync(user, password);
            return result2;
        }

        public async Task<IdentityResult> DeleteUser(string userName)
        {
           var account = await _userManager.FindByNameAsync(userName);
            if (account != null)
            {
                return await _userManager.DeleteAsync(account);
            }
            return null;
        }

        public async Task<User> GetUser(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public List<User> GetUsers()
        {
            var list = _users.GetAll().ToList();
            return list;
        }

        public async Task<IdentityResult> UpdateUser(string userName, User user)
        {
            var item = await _userManager.FindByNameAsync(userName);
            if (item != null)
            {
               return await _userManager.UpdateAsync(user);
            }         
            return null;
        }
    }
}
