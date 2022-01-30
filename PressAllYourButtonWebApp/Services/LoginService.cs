using PressAllYourButtonWebApp.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using System.Text;
namespace PressAllYourButtonWebApp.Services
{
    public class LoginService
    {
        PressAYBDbContext dbContext;
        IHttpContextAccessor httpContextAccessor; // To access HttpContext.SignInAsync
        readonly IConfiguration configuration;

        public LoginService(PressAYBDbContext p, IHttpContextAccessor h, IConfiguration config)
        {
            dbContext = p;
            httpContextAccessor = h;
            configuration = config;
        }


        public async Task<string> LoginAsync(LoginInfoDTO value)
        {
            // Check Account Existence
            var user = dbContext.UserInfos.
                Where(u => u.Email == value.Email)
                .SingleOrDefault();
            if (user == null)
                return "Wrong user info";
            
            // Check Password
            var key = UTF8Encoding.UTF8.GetBytes(configuration["AES_Key"]);
            var cryptPw = user.Password;
            var cryptIv = user.Iv;


            var pwText = CryptograhpyService.DecryptStringFromBytes_Aes(cryptPw, key, cryptIv);
            if (value.Password != pwText)
                return "Wrong user password";




            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                   // new Claim(ClaimTypes.Role, "Administrator")
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {

            };

            await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            return "OK";

        }

        public async Task<string> LogoutAsync()
        {
            await httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //string result = 

            return "LoggedOut";
        }

        

    }
}
