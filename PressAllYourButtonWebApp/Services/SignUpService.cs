using PressAllYourButtonWebApp.Models;
using PressAllYourButtonWebApp.DTOs;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace PressAllYourButtonWebApp.Services
{
    
    public class SignUpService
    {
        readonly IConfiguration Configuration;
        PressAYBDbContext dbcontext;
        public SignUpService(PressAYBDbContext db, IConfiguration configuration)
        {
            dbcontext = db;
            Configuration = configuration;
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
            var encryptedPassword = EncryptStringToBytes_Aes(value.Password, utf8.GetBytes(Configuration["AES_Key"]), ivForUser);

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

        public byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;

            Aes aesObj = Aes.Create();

            // Set AES Key(128) and initial vector
            aesObj.Key = Key;
            aesObj.IV = IV;

            // 待補
            ICryptoTransform encryptor = aesObj.CreateEncryptor(aesObj.Key, aesObj.IV);

            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            StreamWriter swEncrypt = new StreamWriter(csEncrypt);

            swEncrypt.Write(plainText);
            swEncrypt.Dispose();

            encrypted = msEncrypt.ToArray();
            csEncrypt.Dispose();



            msEncrypt.Dispose();
            return encrypted;


        }

    }
}
