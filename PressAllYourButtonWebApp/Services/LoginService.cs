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


        public async Task<string> Login(LoginInfoDTO value)
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
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);
                
                return "OK";
            }
        }

    }
}
