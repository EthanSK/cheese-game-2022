using System;
using System.Security.Cryptography;
using System.Text;

namespace ETGgames.Utils
{
    public static class RandomStringGenerator
    {
        internal static readonly char[] chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public static string RandomString(int size) //cryptographically sound https://stackoverflow.com/a/1344255/6820042
        {
            return RandomString(size, chars);
        }

        public static string RandomString(int size, char[] chars) //cryptographically sound https://stackoverflow.com/a/1344255/6820042
        {
            byte[] data = new byte[4 * size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();
        }
    }
}