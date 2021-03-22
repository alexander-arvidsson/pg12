﻿using System;
using System.Text;
using System.Security.Cryptography;
namespace Lösenordshanterare_PG12
{
    //This class should be good. Only lacks a decryption method
    public class AesEncryptor
    {
        private AesCryptoServiceProvider aesCrypto = new AesCryptoServiceProvider();
        private Encryption.KeyDerivation derivateKey = new Encryption.KeyDerivation();
        string masterPassword;

        public String EncryptVault(string vault, string IV, string clientPath)
        {
            Console.WriteLine("Enter your master password");
            masterPassword = Console.ReadLine();
            aesCrypto.IV = Convert.FromBase64String(IV);
            aesCrypto.Key = derivateKey.GetVaultKey(masterPassword, clientPath);

            ICryptoTransform transform = aesCrypto.CreateEncryptor(aesCrypto.Key, aesCrypto.IV);

            byte[] encrypt = transform.TransformFinalBlock(Encoding.ASCII.GetBytes(vault), 0, vault.Length);

            string encryptedVault = Convert.ToBase64String(encrypt);

            return encryptedVault;

        }

        public String DecryptVault(byte[] vault, string IV, string clientPath)
        {
            Console.WriteLine("Enter your master password");
            masterPassword = Console.ReadLine();
            aesCrypto.IV = Convert.FromBase64String(IV);
            aesCrypto.Key = derivateKey.GetVaultKey(masterPassword, clientPath);

            ICryptoTransform transform = aesCrypto.CreateDecryptor(aesCrypto.Key, aesCrypto.IV);

            byte[] decryptedVault = transform.TransformFinalBlock(vault, 0, vault.Length);
            string decryptedString = ASCIIEncoding.ASCII.GetString(decryptedVault);
            Console.WriteLine(decryptedString);

            return decryptedString;
        }

        public String DecryptVault(byte[] vault, string IV, string clientPath, string secretKey)
        {
            Console.WriteLine("Enter your master password");
            masterPassword = Console.ReadLine();
            aesCrypto.IV = Convert.FromBase64String(IV);
            aesCrypto.Key = derivateKey.GetVaultKey(masterPassword, clientPath, secretKey);

            ICryptoTransform transform = aesCrypto.CreateDecryptor(aesCrypto.Key, aesCrypto.IV);

            byte[] decryptedVault = transform.TransformFinalBlock(vault, 0, vault.Length);
            string decryptedString = ASCIIEncoding.ASCII.GetString(decryptedVault);
            Console.WriteLine(decryptedString);

            return decryptedString;
        }


        public String GenerateIV()
        {
            aesCrypto.GenerateIV();
            return Convert.ToBase64String(aesCrypto.IV);
        }
    }
}
