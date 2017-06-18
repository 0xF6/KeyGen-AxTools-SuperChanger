namespace KeyGenAxTools
{
    using System.Text;
    using System.IO;
    using System.Security.Cryptography;
    using System;
    using KeyGen;

    public enum SuperChangeLicenseType
    {
        Business = 0,
        ProInv = 1,
        Inv = 2
    }

    public class LicenseDecryptor
    {
        public static readonly Encoding coding = Encoding.UTF8;


        public static string EncryptNew(SuperChangeLicenseType TypeLic, ProfileLicense ad)
        {
            byte[] MainBox = null;
            byte[] rgbKey = null;
            byte[] EncryptArray = null;
            byte[] rbgIV = null;
            string header = "";
            string header2 = "";

            string value = "6k";
            string value2 = "6p";
            string value3 = "tt";
            string lics2e =
               $"{ad.LName}{new string('~', 50 - ad.LName.Length)}SuperChanger{new string('~', 23 - "SuperChanger".Length)}" +
               $"{ad.SCount:0000}" +
               $"{ad.EDate.Year.ToString().Remove(0, 2)}" +
               $"{ad.EDate.Month:00}" +
               $"{ad.EDate.Day:00}";


            switch (TypeLic)
            {
                case SuperChangeLicenseType.Business:
                    header = "6k";
                    header2 = "DE7f22";
                    rbgIV = coding.GetBytes("b34BWthpykRn8Fb3");
                    rgbKey = coding.GetBytes("eWyNaikNyP7BrW9kM7koj3YetNEpmUuM");
                    break;
                case SuperChangeLicenseType.ProInv:
                    header = "6p";
                    header2 = "AF3t43";
                    rbgIV = coding.GetBytes("WP84fnsE8Zotg9fB");
                    rgbKey = coding.GetBytes("K3tK6h9nvXXZbNuYQMWXnrbzKn9ePWjG");
                    break;
                case SuperChangeLicenseType.Inv:
                    header = "tt";
                    header2 = "4e2HN4X3g";
                    rbgIV = coding.GetBytes("CmCmrkEvePfScpXc");
                    rgbKey = coding.GetBytes("ys7QwdDhZCW8wBVtUj9fnFEBoURZ4sJA");
                    break;
            }
            MainBox = coding.GetBytes(lics2e);


            RijndaelManaged rijManaged = new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.Zeros,
                BlockSize = 128,
                KeySize = 256
            };
            ICryptoTransform CryTransform = rijManaged.CreateEncryptor(rgbKey, rbgIV);
            using (MemoryStream mStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(mStream, CryTransform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(MainBox, 0, MainBox.GetLength(0));
                    cryptoStream.FlushFinalBlock();
                }
                rijManaged.Clear();
                EncryptArray = mStream.ToArray();
            }

            string key = $"{header}{header2}{Convert.ToBase64String(EncryptArray)}";

            return key;
        }

        public static string Encrypt(int TypeLic, ProfileLicense ad)
        {
            byte[] MainBox = null;
            byte[] rgbKey = null;
            byte[] EncryptArray = null;
            byte[] rbgIV = coding.GetBytes("favoriteshistory");
            string header = "";
            switch (TypeLic)
            {
                case 0:
                    header = "#$a";
                    MainBox = coding.GetBytes($"{header}{ad.LName}{ad.ID}{"0001"}{"201220"}");
                    rgbKey = coding.GetBytes("vs10xcodemapexts");
                    break;
                case 1:
                    header = "#$b";
                    MainBox = coding.GetBytes($"{header}{ad.LName}{ad.ID}{"0001"}{"201220"}");
                    rgbKey = coding.GetBytes("vs10xeditorviewe");
                    break;
                case 2:
                    header = "#$c";
                    MainBox = coding.GetBytes($"{header}{ad.LName}{ad.ID}{"0001"}{"201220"}");
                    rgbKey = coding.GetBytes("vs10xcommentsext");
                    break;
            }

            RijndaelManaged rijManaged = new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.Zeros,
                BlockSize = 128,
                KeySize = 128
            };
            ICryptoTransform CryTransform = rijManaged.CreateEncryptor(rgbKey, rbgIV);
            using (MemoryStream mStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(mStream, CryTransform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(MainBox, 0, MainBox.GetLength(0));
                    cryptoStream.FlushFinalBlock();
                }
                rijManaged.Clear();
                EncryptArray = mStream.ToArray();
            }

            string key = $"{header}{Convert.ToBase64String(EncryptArray)}";

            return key;
        }
    }
}