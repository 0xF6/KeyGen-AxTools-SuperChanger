using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace KeyGenAxTools
{
    internal sealed class LicenseData
    {
        public string Xu0002;
        public string Xu0003;
        public int Xu0005;
        public DateTime Xu0008;
        public bool Xu0006;
        public bool Xu000E;
    }
    internal sealed class Licenser
    {
        public LicenseData Update(string lic_code)
        {
            lic_code = lic_code.Trim();
            var uTF = Encoding.UTF8;
            var c = 'P';
            bool Xu0006;
            bool Xu000E;
            byte[] bytes;
            byte[] bytes2;
            if (lic_code.StartsWith("6kDE7f22"))
            {
                if (c == 'E' || c == 'U')
                    return null;
                Xu0006 = false;
                Xu000E = false;
                lic_code = lic_code.Substring(8);
                bytes = uTF.GetBytes("b34BWthpykRn8Fb3");
                bytes2 = uTF.GetBytes("eWyNaikNyP7BrW9kM7koj3YetNEpmUuM");
            }
            else if (lic_code.StartsWith("6pAF3t43"))
            {
                Xu0006 = true;
                Xu000E = false;
                lic_code = lic_code.Substring(8);
                bytes = uTF.GetBytes("WP84fnsE8Zotg9fB");
                bytes2 = uTF.GetBytes("K3tK6h9nvXXZbNuYQMWXnrbzKn9ePWjG");
            }
            else
            {
                if (!lic_code.StartsWith("tt4e2HN4X3g"))
                    return null;
                Xu0006 = false;
                Xu000E = true;
                lic_code = lic_code.Substring(11);
                bytes = uTF.GetBytes("CmCmrkEvePfScpXc");
                bytes2 = uTF.GetBytes("ys7QwdDhZCW8wBVtUj9fnFEBoURZ4sJA");
            }
            byte[] array = null;
            try
            {
                array = Convert.FromBase64String(lic_code);
            }
            catch
            {
                return default;
            }
            var rijndaelManaged = new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.Zeros,
                BlockSize = 128,
                KeySize = 256
            };
            ICryptoTransform transform = null;
            try
            {
                transform = rijndaelManaged.CreateDecryptor(bytes2, bytes);
            }
            catch (SystemException)
            {
            }
            byte[] array2 = null;
            var memoryStream = new MemoryStream();
            try
            {
                var cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
                try
                {
                    cryptoStream.Write(array, 0, array.GetLength(0));
                    cryptoStream.FlushFinalBlock();
                }
                finally
                {
                    ((IDisposable)cryptoStream).Dispose();
                }
                rijndaelManaged.Clear();
                array2 = memoryStream.ToArray();
            }
            finally
            {
                ((IDisposable)memoryStream).Dispose();
            }
            var text = new string(Encoding.UTF8.GetChars(array2));
            if (text.Length < 83)
            {
                return null;
            }
            var licenseData = new LicenseData
            {
                Xu0002 = text.Substring(0, 50).Trim(new char[]
                {
                '~'
                }),
                Xu0003 = text.Substring(50, 23),
                Xu0006 = Xu0006,
                Xu000E = Xu000E
            };
            try
            {
                licenseData.Xu0005 = int.Parse(text.Substring(73, 4));
            }
            catch
            {
                return default;
            }
            try
            {
                licenseData.Xu0008 = new DateTime(int.Parse("20" + text.Substring(77, 2)), int.Parse(text.Substring(79, 2)), int.Parse(text.Substring(81, 2)));
            }
            catch
            {
                return default;
            }
            return licenseData;
        }
    }
}