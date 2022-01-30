using PressAllYourButtonWebApp.Models;
using PressAllYourButtonWebApp.DTOs;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace PressAllYourButtonWebApp.Services
{
    
    public class SignUpService
    {
        readonly IConfiguration configuration;
        PressAYBDbContext dbcontext;
        public SignUpService(PressAYBDbContext db, IConfiguration config)
        {
            dbcontext = db;
            configuration = config;
        }

        public string SignUp(SignUpInfoDTO value)
        {
            var user = dbcontext.UserInfos.Where(u => u.Email == value.Email).SingleOrDefault();
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
            UserInfo userinfo = new UserInfo() {
                UserName = value.UserName,
                Email = value.Email,
                Password = encryptedPassword,
                Iv = ivForUser,
                Verified = false
                
            };

            dbcontext.UserInfos.Add(userinfo);
            dbcontext.SaveChanges();


            return "UserAdded";
        }

        

    }
}
