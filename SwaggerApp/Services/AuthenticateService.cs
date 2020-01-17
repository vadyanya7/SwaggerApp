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
            new Account {Login="admin@gmail.com", Password="12345", Role = "admin" },
            new Account { Login="qwerty@gmail.com", Password="55555", Role = "user" }
        };

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
        public ClaimsIdentity GetIdentity(string username, string password)
        {
            Account person = people.FirstOrDefault(x => x.Login == username && x.Password == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
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
    }
}
