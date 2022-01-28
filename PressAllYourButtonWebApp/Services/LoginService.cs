using PressAllYourButtonWebApp.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace PressAllYourButtonWebApp.Services
{
    public class LoginService
    {
        PressAYBDbContext dbContext;
        IHttpContextAccessor httpContextAccessor; // To access HttpContext.SignInAsync
        public LoginService(PressAYBDbContext p, IHttpContextAccessor h)
        {
            dbContext = p;
            httpContextAccessor = h;
        }


        public async Task<string> LoginAsync(LoginInfoDTO value)
        {
            
            var user = dbContext.UserInfos.
                Where(u => u.Email == value.Email).
                Where(u => u.Password == value.Password).SingleOrDefault();

            if (user == null)
            {
                return "Wrong user info";
            }
            else
            {
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
        }

        public async Task<string> LogoutAsync()
        {
            await httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            //string result = 

            return "LoggedOut";
        }

    }
}
