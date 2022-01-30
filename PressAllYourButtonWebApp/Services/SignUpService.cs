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
            var encryptedPassword = EncryptStringToBytes_Aes(value.Password, utf8.GetBytes(configuration["AES_Key"]), ivForUser);

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

        private static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;

            Aes aesObj = Aes.Create();

            // Set AES Key and initial vector from caller
            aesObj.Key = Key;
            aesObj.IV = IV;

            // 待補
            ICryptoTransform encryptor = aesObj.CreateEncryptor(aesObj.Key, aesObj.IV);


            // use using statment to specify stream object's lifeime
            // but for readability I manually dispose AES object at the end of this function
            using (MemoryStream msEncrypt = new MemoryStream()) 
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                       
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }


            aesObj.Dispose();

            return encrypted;


        }

    }
}
