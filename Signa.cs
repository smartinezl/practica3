using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Signature
{
    class Signa
    {
        // String per conservar la ubicació de les claus.
        public static string keysPath;

        // String per conservar el nom del fitxer de la clau publica.
        private static string publicKey = "/public.xml";

        // String per conservar el nom del fitxer de la clau privada.
        private  static string privateKey = "/private.xml";

        /// <summary>
        ///     Genera la clau que conté el parell de clau privada i pública utilitzant 1024 bytes. Guarda el conjunt de claus en els fitxers private.xml i public.xml.
        /// </summary>
        public static void GenerateKeys()
        {
            // Variables d'aces a fitxers.
            FileStream fs = null;
            StreamWriter sw = null;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            try
            {
                // Guarda la clau privada
                fs = new FileStream(keysPath + privateKey, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);
                sw.Write(rsa.ToXmlString(true));
                sw.Flush();
            }
            finally
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
            }

            try
            {
                // Guarda la clau publica
                fs = new FileStream(keysPath + publicKey, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);
                sw.Write(rsa.ToXmlString(false));
                sw.Flush();
            }
            finally
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
            }
            rsa.Clear();
        }

        /// <summary>
        ///     Comprova si les claus publica i privada han estat creades.
        /// </summary>
        /// <returns>Boolea indicant si han estat creades.</returns>
        public static bool KeysArePresent()
        {
            return File.Exists(keysPath + publicKey) & File.Exists(keysPath + privateKey);
        }

        /// <summary>
        ///     Signa el fitxer utilitzant la clau privada.
        /// </summary>
        /// <param name="fs">Fitxer a signar.</param>
        public static void Sign(FileStream fs, String path)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024))
            {
                rsa.FromXmlString(File.ReadAllText(keysPath + privateKey));
                string finalName = path + "/" + Path.GetFileNameWithoutExtension(fs.Name) + ".rsa";
                byte[] data = Encoding.UTF8.GetBytes(File.ReadAllText(fs.Name));
                byte[] signature = rsa.SignData(data, new SHA1CryptoServiceProvider());
                using (StreamWriter sw = new StreamWriter(new FileStream(finalName, FileMode.Create, FileAccess.Write)))
                {
                    sw.Write(Convert.ToBase64String(signature));
                }
            }
        }

        /// <summary>
        ///     Comprova si la signatura es valida per al fitxer.
        /// </summary>
        /// <param name="fitxer">Fitxer a comprovar.</param>
        /// <param name="signatura">Signatura a comprovar.</param>
        /// <returns>Cert si la signatura és vàlida i fals si no ho és.</returns>
        public static bool ValidateSignature(FileStream fitxer, FileStream signatura)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024))
            {
                rsa.FromXmlString(File.ReadAllText(keysPath + publicKey));
                byte[] data = Encoding.UTF8.GetBytes(File.ReadAllText(fitxer.Name));
                byte[] sign = Convert.FromBase64String(File.ReadAllText(signatura.Name));
                rsa.VerifyData(data, CryptoConfig.MapNameToOID("SHA1"), sign);
                SHA1Managed hash = new SHA1Managed();
                byte[] hashedData = hash.ComputeHash(data);
                return rsa.VerifyHash(hashedData, CryptoConfig.MapNameToOID("SHA1"), sign);
            }
        }
    }
}
