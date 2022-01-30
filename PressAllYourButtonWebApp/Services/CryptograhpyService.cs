using System.Security.Cryptography;
namespace PressAllYourButtonWebApp.Services
{
    public static class CryptograhpyService
    {
        public static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
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
        public static string DecryptStringFromBytes_Aes(byte[] db_pass, byte[] db_Key, byte[] db_IV)
        {
            string plaintext = "";

            Aes aesObj = Aes.Create();

            // Set AES Key and initial vector from caller
            aesObj.Key = db_Key;
            aesObj.IV = db_IV;

            ICryptoTransform decryptor = aesObj.CreateDecryptor(aesObj.Key, aesObj.IV);

            // use using statment to specify stream object's lifeime
            // but for readability I manually dispose AES object at the end of this function
            using (MemoryStream msDecrypt = new MemoryStream(db_pass))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

            aesObj.Dispose();

            return plaintext;
        }
    }
}
