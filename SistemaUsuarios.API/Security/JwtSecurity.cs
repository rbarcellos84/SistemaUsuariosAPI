using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace SistemaUsuarios.API.Security
{
    public static class JwtSecurity
    {
        //atributo para o token
        //public static int ExpirationInMinutes = 20;
        public static int ExpirationInHours = 6;
        public static string SecretKey = "899335d4-3515-488c-bbd6-fe9ef591d0d5";

        //geração de token
        public static string GenerateToken(string enail)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            //token
            var tokenDescription = new SecurityTokenDescriptor
            {
                //identificação do usuario para AspNet
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //email do usuario
                    new Claim(ClaimTypes.Name, enail)
                }),

                //tempo de expiração do token
                Expires = DateTime.Now.AddHours(ExpirationInHours),

                //criptografia do token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}
