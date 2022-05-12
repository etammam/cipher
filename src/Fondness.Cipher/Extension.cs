using System.Security.Cryptography;
using System.Text;

namespace Fondness.Cipher
{
    public static class Extension
    {
        private const string EncryptionKey = "b14ca5898a4e4133bbce2ea2315a1916";

        /// <summary>
        /// uses this extension to encrypt a string
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encryptionKey">optional params, don't use it if you don't need to provider a custom encryption key</param>
        /// <returns></returns>
        public static string ToEncrypt(this string input, string encryptionKey = EncryptionKey)
        {
            var clearBytes = Encoding.Unicode.GetBytes(input);
            using var standard = Aes.Create();
            var pdb = new Rfc2898DeriveBytes(encryptionKey,
                new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            standard.Key = pdb.GetBytes(32);
            standard.IV = pdb.GetBytes(16);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, standard.CreateEncryptor(),
                       CryptoStreamMode.Write))
            {
                cs.Write(clearBytes, 0, clearBytes.Length);
                cs.Close();
            }

            input = Convert.ToBase64String(ms.ToArray());

            return input;
        }

        /// <summary>
        /// uses this extension to decrypt a string
        /// </summary>
        /// <param name="input">the encrypted string</param>
        /// <param name="encryptionKey">optional params, don't use it if you don't need to provider a custom encryption key</param>
        /// <returns></returns>
        public static string ToDecrypt(this string input, string encryptionKey = EncryptionKey)
        {
            input = input.Replace(" ", "+");
            var cipherBytes = Convert.FromBase64String(input);
            using var standard = Aes.Create();
            var pdb = new Rfc2898DeriveBytes(encryptionKey,
                new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            standard.Key = pdb.GetBytes(32);
            standard.IV = pdb.GetBytes(16);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, standard.CreateDecryptor(),
                       CryptoStreamMode.Write))
            {
                cs.Write(cipherBytes, 0, cipherBytes.Length);
                cs.Close();
            }

            input = Encoding.Unicode.GetString(ms.ToArray());

            return input;
        }

    }
}