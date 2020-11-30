using System;
using System.IO;
using System.Security.Cryptography;

namespace Utilities
{
    public static class AesEncryption
    {
        public static byte[] GenerateRandomKey(int length)
        {
            byte[] arr = new byte[length];
            Random r = new Random();
            for (int i = 0; i < length; i++)
            {
                arr[i] = (byte)r.Next(0, 256);
            }
            return arr;
        }
        public static string Encrypt(string plainText, byte[] Key)
        {

            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");

            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(Key, new byte[16]);

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
            }

            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string data, byte[] Key)
        {
            if (data == null || data.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");

            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(Key, new byte[16]);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(data)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}
