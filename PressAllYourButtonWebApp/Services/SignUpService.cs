using PressAllYourButtonWebApp.Models;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
namespace PressAllYourButtonWebApp.Services
{
    
    public class SignUpService
    {
        readonly IConfiguration Configuration;
        PressAYBDbContext context;
        public SignUpService(PressAYBDbContext db, IConfiguration configuration)
        {
            context = db;
            Configuration = configuration;
        }

        public string SignUpAsync(string name, string email, string inputpw)
        {
            var user = context.UserInfos.Where(u => u.Email == email).SingleOrDefault();
            if (user != null)
                return "UserExists";

            // get appsetting.json
            var builder = WebApplication.CreateBuilder();

            // need to add validation in the future

            // -------------------------------

            // Password Encryption

            // create a new aes obj to generat Iv for this particular user
            Aes aesForUser = Aes.Create();
            var ivForUser = aesForUser.IV;

            // decoder for key
            UTF8Encoding utf8 = new UTF8Encoding();

            // Call aes algorithm to encrypt password
            var encryptedPassword = EncryptStringToBytes_Aes(inputpw, utf8.GetBytes(Configuration["AES_Key"]), ivForUser);

            // create new object for dbo.UserInfos
            UserInfo userinfo = new UserInfo() {
                UserName = name,
                Email = email,
                Password = encryptedPassword
            };
            




            return null;
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
