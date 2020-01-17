using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SwaggerApp.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SwaggerApp.Services
{
    public class AuthenticateService: IAuthenticateService
    {
        private List<Account> people = new List<Account>
        {
            new Account {Login="admin@gmail.com", Password="QwERt1324232_d", Role = "admin" },
            new Account { Login="qwerty@gmail.com", Password="55555", Role = "user" }
        };
        private UserManager<Account> _userManager;
        public AuthenticateService(UserManager<Account> userManager)
        {
           _userManager=userManager;
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

            return  new ResponseModel
            {
                Access_token = encodedJwt,
                UserName = identity.Name
            };

        }
        public ClaimsIdentity GetIdentity(Account model)
        {
            Account person = _userManager.Users.FirstOrDefault(x => x.Email == model.Email );
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

        public List<Account> GetAccounts()
        {
            return _userManager.Users.ToList();
        }

        public Account GetAccount(string userName)
        {
            return _userManager.Users.FirstOrDefault(x => x.UserName == userName) ?? null;
        }

        public async Task<Account> AddAccount(Account account)
        {

                var user = new Account
                {
                    Password = account.Password,
                    Role = account.Role,
                    UserName = account.UserName,
                    Email = account.Email
                };
                var result = await _userManager.CreateAsync(user, account.Password);
                return result.Succeeded ? user : null;
    
        }

        public void UpdateAccount(string userName, Account account)
        {
            var item = _userManager.Users.FirstOrDefault(x => x.UserName == userName); 
            if (item != null)
            {
               _userManager.UpdateAsync(account);
            }
        }

        public void DeleteAccount(string userName)
        {
            var account = _userManager.Users.FirstOrDefault(x => x.UserName == userName);
            if (account != null)
            {
               _userManager.DeleteAsync(account);
            }
         
        }
    }
}
