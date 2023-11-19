using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace CafeMobile_api.Models.Entities
{
    public class Authentication
    {


        public readonly IConfiguration config;

        public Authentication(IConfiguration config)
        {
            this.config = config;
        }   
        public void CreatePasswordHash(string password, out string password_hash)
        {
            // BCrypt maintains internal salt
            password_hash = BC.HashPassword(password);
        }

        //Verifying password
        public bool verifyPasswordHash(string password, string hash)
        {
            return BC.Verify(password, hash);
        }

        //Creating Token
        public string createToken(string id, string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, id),
                new Claim(ClaimTypes.Role, role),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JwtSettings:secretKey").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),//Expires 24 hours after creation time
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }


}