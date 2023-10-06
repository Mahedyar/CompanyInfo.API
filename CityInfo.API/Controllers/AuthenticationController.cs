using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CompanyInfo.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public class AuthenticationRequestBody
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }   
        }
        public class CompanyUserInfo
        {
            public int UserID { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Company { get; set; }

            public CompanyUserInfo(int userID , string userName , string firstName , string lastName , string company)
            {
                UserID = userID;
                UserName = userName;
                FirstName = firstName;
                LastName = lastName;
                Company = company;
            }
        }
        IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            var user = ValidateUserCredentials(authenticationRequestBody.UserName, authenticationRequestBody.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"])
                );
            var signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("userID", user.UserID.ToString()));
            claimsForToken.Add(new Claim("Username", user.FirstName.ToString()));
            claimsForToken.Add(new Claim(ClaimTypes.Email,user.Company.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken, DateTime.UtcNow, DateTime.UtcNow.AddHours(1), signingCredentials
                );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);


            return Ok(tokenToReturn);
        }

        private CompanyUserInfo ValidateUserCredentials(string? userName , string? password)
        {
            return new CompanyUserInfo(1, userName ?? "", "Mahed", "Yarmohammadi", "Benz");
        }
    }
}
