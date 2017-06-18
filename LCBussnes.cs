using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace KeyGenAxTools
{
    internal sealed class Xu000EXu2005Xu2008
    {
        // Token: 0x04000839 RID: 2105
        public string Xu0002;

        // Token: 0x0400083A RID: 2106
        public string Xu0003;

        // Token: 0x0400083B RID: 2107
        public int Xu0005;

        // Token: 0x0400083C RID: 2108
        public DateTime Xu0008;

        // Token: 0x0400083D RID: 2109
        public bool Xu0006;

        // Token: 0x0400083E RID: 2110
        public bool Xu000E;
    }
    internal sealed class Xu000FXu2005Xu2008
    {
        // Token: 0x06000F0C RID: 3852 RVA: 0x000796AC File Offset: 0x000778AC
        public Xu000EXu2005Xu2008 Xu0002(string Xu0002)
        {
            Xu0002 = Xu0002.Trim();
            Encoding uTF = Encoding.UTF8;
            char c = 'P';
            bool Xu0006;
            bool Xu000E;
            byte[] bytes;
            byte[] bytes2;
            if (Xu0002.StartsWith("6kDE7f22"))
            {
                if (c == 'E' || c == 'U')
                {
                    return null;
                }
                Xu0006 = false;
                Xu000E = false;
                Xu0002 = Xu0002.Substring(8);
                bytes = uTF.GetBytes("b34BWthpykRn8Fb3");
                bytes2 = uTF.GetBytes("eWyNaikNyP7BrW9kM7koj3YetNEpmUuM");
            }
            else if (Xu0002.StartsWith("6pAF3t43"))
            {
                Xu0006 = true;
                Xu000E = false;
                Xu0002 = Xu0002.Substring(8);
                bytes = uTF.GetBytes("WP84fnsE8Zotg9fB");
                bytes2 = uTF.GetBytes("K3tK6h9nvXXZbNuYQMWXnrbzKn9ePWjG");
            }
            else
            {
                if (!Xu0002.StartsWith("tt4e2HN4X3g"))
                {
                    return null;
                }
                Xu0006 = false;
                Xu000E = true;
                Xu0002 = Xu0002.Substring(11);
                bytes = uTF.GetBytes("CmCmrkEvePfScpXc");
                bytes2 = uTF.GetBytes("ys7QwdDhZCW8wBVtUj9fnFEBoURZ4sJA");
            }
            byte[] array = null;
            try
            {
                array = Convert.FromBase64String(Xu0002);
            }
            catch
            {
                Xu000EXu2005Xu2008 result = null;
                return result;
            }
            RijndaelManaged rijndaelManaged = new RijndaelManaged
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
            catch (SystemException Xu0003)
            {
                //Xu0006Xu2007Xu2007.Xu0002(636049834433470479L, Xu0003);
            }
            byte[] array2 = null;
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
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
            string text = string.Empty;
            if (array2 != null)
            {
                text = new string(Encoding.UTF8.GetChars(array2));
            }
            if (text.Length < 83)
            {
                return null;
            }
            Xu000EXu2005Xu2008 Xu000EXu2005Xu2008 = new Xu000EXu2005Xu2008
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
                Xu000EXu2005Xu2008.Xu0005 = int.Parse(text.Substring(73, 4));
            }
            catch
            {
                Xu000EXu2005Xu2008 result = null;
                return result;
            }
            try
            {
                Xu000EXu2005Xu2008.Xu0008 = new DateTime(int.Parse("20" + text.Substring(77, 2)), int.Parse(text.Substring(79, 2)), int.Parse(text.Substring(81, 2)));
            }
            catch
            {
                Xu000EXu2005Xu2008 result = null;
                return result;
            }
            return Xu000EXu2005Xu2008;
        }
    }
}