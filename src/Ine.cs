using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace KeyGen
{
    internal static class u000Eu2006u2008
    {
        // Token: 0x06000BEB RID: 3051 RVA: 0x00064128 File Offset: 0x00062328
        private static void u00023()
        {
            try
            {
                u0003 = new RijndaelManaged
                {
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.Zeros,
                    BlockSize = 128,
                    KeySize = 256
                };
                var bytes = u0002.GetBytes(u0006);
                var bytes2 = u0002.GetBytes(u000E);
                u0008 = u0003.CreateDecryptor(bytes2, bytes);
                u0005 = true;
            }
            catch (SystemException ex)
            {
            }
        }

        // Token: 0x06000BEC RID: 3052 RVA: 0x000641C0 File Offset: 0x000623C0
        public static string u00023(string u0002)
        {
            if (!u0005)
            {
                u00023();
            }
            var result = string.Empty;
            try
            {
                var array = Convert.FromBase64String(u0002);
                byte[] array2 = null;
                var memoryStream = new MemoryStream();
                try
                {
                    var cryptoStream = new CryptoStream(memoryStream, u0008, CryptoStreamMode.Write);
                    try
                    {
                        cryptoStream.Write(array, 0, array.GetLength(0));
                        cryptoStream.FlushFinalBlock();
                    }
                    finally
                    {
                        ((IDisposable)cryptoStream).Dispose();
                    }
                    u0003.Clear();
                    array2 = memoryStream.ToArray();
                }
                finally
                {
                    ((IDisposable)memoryStream).Dispose();
                }
                if (array2 != null)
                {
                    result = new string(Encoding.UTF8.GetChars(array2)).TrimEnd(new char[1]);
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        // Token: 0x040007AB RID: 1963
        private static Encoding u0002 = Encoding.UTF8;

        // Token: 0x040007AC RID: 1964
        private static RijndaelManaged u0003;

        // Token: 0x040007AD RID: 1965
        private static bool u0005 = false;

        // Token: 0x040007AE RID: 1966
        private static ICryptoTransform u0008;

        // Token: 0x040007AF RID: 1967
        private static string u0006 = "bXng7T29ECPXu8zn";

        // Token: 0x040007B0 RID: 1968
        private static string u000E = "NQHsfAFgBcoHn9C9xjgf8UTJXsyyjFQ7";
    }
}