using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokenkey"]));
        }
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            { 
                new(JwtRegisteredClaimNames.NameId,user.UserName)
            };
            var cred = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor =  new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                 SigningCredentials = cred
            };

            var TokenHandler = new JwtSecurityTokenHandler();

                var token = TokenHandler.CreateToken(tokenDescriptor);

                return TokenHandler.WriteToken(token);
        }
    }
}