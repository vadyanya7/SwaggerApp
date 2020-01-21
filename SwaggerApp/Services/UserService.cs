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
        public ClaimsIdentity GetIdentity(User model)
        {
            User person = _userManager.Users.FirstOrDefault(x=>x.UserName==model.UserName);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Name),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Password)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
        public async Task<User> AddUser(User user)
        {
   
           
            var result2 = await  _userManager.CreateAsync(user, user.Password);
            //var result = await _userManager.CreateAsync(user);
            //var result23 =  _userManager.CreateAsync(user);
            //var result223 = _userManager.CreateAsync(user, user.PasswordHash);
            // var sdf =  result2.Succeeded ? user : null;

            //_users.Add(user);
            //_users.SaveChanges();
            return user;
        }

        public void DeleteUser(string userName)
        {
            var account = _userManager.Users.FirstOrDefault(x => x.UserName == userName);
            if (account != null)
            {
                _userManager.DeleteAsync(account);
            }

            //_users.Delete(id);
            //_users.SaveChanges();
        }

        public User GetUser(string userName)
        {
            return _userManager.Users.FirstOrDefault(x => x.UserName == userName) ?? null;
          //  return _users.Get(id);
        }

        public List<User> GetUsers()
        {
            var list = _userManager.Users.ToList();
            return list;
        }

        public async Task<User> UpdateUser(string userName, User user)
        {
            var sa= await  _userManager.UpdateAsync(user);
                         
            return user;
            //_users.Update(userName, user);
            //_users.SaveChanges();
        }
    }
}
