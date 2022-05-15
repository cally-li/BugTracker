using BugTracker.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BugTracker.Services
{
    //create a token and pass to controller
    public class TokenService : ITokenService
    {
        //symmetric encryption: same key used to encrypt(sign) and decrypt(verify) data
        private readonly SymmetricSecurityKey _key;
        //constructor
        //Inject IConfig to access the Token Key from appsettings.Development.json
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(User user)
        {
            //add claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            //create credentials. specify type of security algo being used
            var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512);
            
            //create token description
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            //create token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //return token as string
            return tokenHandler.WriteToken(token);  

        }
    }
}
