using PressAllYourButtonWebApp.DTOs;
using PressAllYourButtonWebApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
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
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
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

        public string SignUp(SignUpInfoDTO value)
        {
            var user = dbContext.UserInfos.Where(u => u.Email == value.Email).SingleOrDefault();
            if (user != null)
                return "UserExists";

            // get appsetting.json
            var builder = WebApplication.CreateBuilder();

            // need to add validation in the future
            // add here
            // -------------------------------

            // Password Encryption

            // create a new aes obj to generat Iv for this particular user
            Aes aesForUser = Aes.Create();
            var ivForUser = aesForUser.IV;

            // decoder for key
            UTF8Encoding utf8 = new UTF8Encoding();

            // Call aes algorithm to encrypt password
            var encryptedPassword = CryptograhpyService.EncryptStringToBytes_Aes(value.Password, utf8.GetBytes(configuration["AES_Key"]), ivForUser);

            // create new object for dbo.UserInfos
            UserInfo userinfo = new UserInfo()
            {
                UserName = value.UserName,
                Email = value.Email,
                Password = encryptedPassword,
                Iv = ivForUser,
                Verified = false

            };

            dbContext.UserInfos.Add(userinfo);
            dbContext.SaveChanges();


            return "UserAdded";
        }


    }
}
