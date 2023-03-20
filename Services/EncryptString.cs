using BlazorRepoEstoque.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BlazorRepoEstoque.Services
{
    public class EncryptString : IEncryptString
    {
        private readonly IConfiguration _config;

        public EncryptString(IConfiguration config)
        {
            _config = config;
        }      

        public string CriptografarTextoAES(string texto)
        {
            byte[] textoBytes = Encoding.UTF8.GetBytes(texto);
            byte[] chaveBytes = Encoding.UTF8.GetBytes(_config["secretKey"]);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = chaveBytes;
                aesAlg.GenerateIV();

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                byte[] textoCriptografadoBytes;
                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(textoBytes, 0, textoBytes.Length);
                    }
                    textoCriptografadoBytes = msEncrypt.ToArray();
                }

                byte[] textoCriptografadoIVBytes = new byte[aesAlg.IV.Length + textoCriptografadoBytes.Length];
                Buffer.BlockCopy(aesAlg.IV, 0, textoCriptografadoIVBytes, 0, aesAlg.IV.Length);
                Buffer.BlockCopy(textoCriptografadoBytes, 0, textoCriptografadoIVBytes, aesAlg.IV.Length, textoCriptografadoBytes.Length);

                string textoCriptografado = Convert.ToBase64String(textoCriptografadoIVBytes);

                return textoCriptografado;
            }
        }
    }
}
