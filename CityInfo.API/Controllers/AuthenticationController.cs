using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            var user = ValidateUserCredentials(authenticationRequestBody.UserName, authenticationRequestBody.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            return "";
        }

        private CompanyUserInfo ValidateUserCredentials(string? userName , string? password)
        {
            return new CompanyUserInfo(1, userName ?? "", "Mahed", "Yarmohammadi", "Benz");
        }
    }
}
