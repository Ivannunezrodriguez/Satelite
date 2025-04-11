using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Satelite
{
    class TripleDESUtil
    {
        public byte[] IV { get; set; }
        public byte[] Key { get; set; }

        public string DesEncriptar(byte[] message)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            IV = encoding.GetBytes(Variables.pki1);
            Key = encoding.GetBytes(Variables.pki2);

            TripleDES cryptoProvider = new TripleDESCryptoServiceProvider();
            ICryptoTransform cryptoTransform =cryptoProvider.CreateDecryptor(Key, IV);
            MemoryStream memoryStream = new MemoryStream(message);
            CryptoStream cryptoStream =new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cryptoStream, true);

            string textoLimpio = sr.ReadToEnd();
            //Console.WriteLine("Texto Limpio:{0}", textoLimpio);
            return textoLimpio;
        }

        public byte[] Encriptar(string cadenaOrigen)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] message = encoding.GetBytes(cadenaOrigen);
            TripleDESCryptoServiceProvider criptoProvider = new TripleDESCryptoServiceProvider();

            IV = encoding.GetBytes(Variables.pki1);
            Key = encoding.GetBytes(Variables.pki2);

            //IV = criptoProvider.IV;
            //Key = criptoProvider.Key;

            ICryptoTransform criptoTransform = criptoProvider.CreateEncryptor(Key, IV);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, criptoTransform, CryptoStreamMode.Write);

            cryptoStream.Write(message, 0, message.Length);
            // cryptoStream.Flush();
            cryptoStream.FlushFinalBlock();

            byte[] encriptado = memoryStream.ToArray();
            string cadenaEncriptada = encoding.GetString(encriptado);
            //Console.WriteLine("Texto encriptado:{0}", cadenaEncriptada);
            //Console.WriteLine();
            return encriptado;
        }

        public string DesEncriptarInst(byte[] message)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            IV = encoding.GetBytes(Variables.pki1);
            Key = encoding.GetBytes(Variables.pki2);

            TripleDES cryptoProvider = new TripleDESCryptoServiceProvider();
            ICryptoTransform cryptoTransform = cryptoProvider.CreateDecryptor(Key, IV);
            MemoryStream memoryStream = new MemoryStream(message);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cryptoStream, true);

            string textoLimpio = sr.ReadToEnd();
            //Console.WriteLine("Texto Limpio:{0}", textoLimpio);
            return textoLimpio;
        }

        public byte[] EncriptarIns(string cadenaOrigen)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] message = encoding.GetBytes(cadenaOrigen);
            TripleDESCryptoServiceProvider criptoProvider = new TripleDESCryptoServiceProvider();

            IV = encoding.GetBytes(Variables.pki1);
            Key = encoding.GetBytes(Variables.pki2);

            //IV = criptoProvider.IV;
            //Key = criptoProvider.Key;

            ICryptoTransform criptoTransform = criptoProvider.CreateEncryptor(Key, IV);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, criptoTransform, CryptoStreamMode.Write);

            cryptoStream.Write(message, 0, message.Length);
            // cryptoStream.Flush();
            cryptoStream.FlushFinalBlock();

            byte[] encriptado = memoryStream.ToArray();
            string cadenaEncriptada = encoding.GetString(encriptado);
            //Console.WriteLine("Texto encriptado:{0}", cadenaEncriptada);
            //Console.WriteLine();
            return encriptado;
        }
    }
}
