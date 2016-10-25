namespace KeyGen
{
    using RC.Framework;
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Windows.Forms;


    public class Program
    {
        [STAThread]
        static void Main(string[] args) { new Program().tMain(args); }
        public void tMain(string[] args)
        {
            Terminal.Title = "KeyGen - AxTools 10";
            //# +-------------------------+-+
            //# | Версии продуктов - VS10  \|\
            //# +---------------------------+-+-+
            //# | xCode Map                 | 0 |
            //# +---------------------------+---+
            //# | xEditor Viem              | 1 |
            //# +---------------------------+---+
            //# | xComment                  | 2 |
            //# +---------------------------+---+
            //@ vs10xcodemapexts, vs10xeditorviewe, vs10xcommentsext
            Terminal.WriteLine($"+g2\0+-------------------------+-++g0\0", "+g3\0Info+g0\0");
            Terminal.WriteLine($"+g2\0|+g0\0 Версия - +g3\0AxTools+g0\010{new string(' ', 7)}+g2\0\\|\\+g2\0", "+g3\0Info+g0\0");
            Terminal.WriteLine($"+g2\0+---------------------------+-+-++g0\0", "+g3\0Info+g0\0");
            Terminal.WriteLine($"+g2\0|+g0\0 +g4\0x+g3\0Code Map+g0\0{new string(' ', 17)}+g2\0|+g0\0 0 +g2\0|+g0\0", "+g3\0Info+g0\0");
            Terminal.WriteLine($"+g2\0+---------------------------+---++g0\0", "+g3\0Info+g0\0");
            Terminal.WriteLine($"+g2\0|+g0\0 +g4\0x+g3\0Editor Viem+g0\0{new string(' ', 14)}+g2\0|+g0\0 1 +g2\0|+g0\0", "+g3\0Info+g0\0");
            Terminal.WriteLine($"+g2\0+---------------------------+---++g0\0", "+g3\0Info+g0\0");
            Terminal.WriteLine($"+g2\0|+g0\0 +g4\0x+g3\0Comment+g0\0{new string(' ', 18)}+g2\0|+g0\0 2 +g2\0|+g0\0", "+g3\0Info+g0\0");
            Terminal.WriteLine($"+g2\0+---------------------------+---++g0\0", "+g3\0Info+g0\0");

            TR_0:
            Terminal.Write("Введите тип продукта :>", "+g5\0KeyGen+g0\0");
            int i;
            if (Int32.TryParse(Console.ReadLine(), out i))
            {
                if (!(i >= 0 && i <= 2))
                {
                    Terminal.WriteLine("Ошибка! Тип продукта не верен!", "+g5\0KeyGen+g0\0");
                    goto TR_0;
                }
            }
            else
            {
                Terminal.WriteLine("Ошибка! Тип продукта не является числом!", "+g5\0KeyGen+g0\0");
                goto TR_0;
            }
            //
            int TypeLic = i;
            Encoding coding = Encoding.UTF8;
            //
            ProfileLicense ad = new ProfileLicense();
            ad.ID = $"AxTools License ID Code";

            TR_1:
            Terminal.Write("Введите своё имя :>", "+g5\0KeyGen+g0\0");
            string name = Console.ReadLine();
            if (name.Length > 20)
            {
                Terminal.WriteLine("Имя слишком длинное!", "+g5\0KeyGen+g0\0");
                goto TR_1;
            }
            Terminal.Write("Введите свою фамилию :>", "+g5\0KeyGen+g0\0");
            string family = Console.ReadLine();
            if (family.Length > 20)
            {
                Terminal.WriteLine("Фамилия слишком длинная!", "+g5\0KeyGen+g0\0");
                goto TR_1;
            }
            ad.LName = $"{name} {family}{new string('~', 50 - (name.Length + family.Length + 1))}";
            int iss = ad.LName.Length;
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
            RijndaelManaged rijManaged = new RijndaelManaged();
            rijManaged.Mode = CipherMode.CBC;
            rijManaged.Padding = PaddingMode.Zeros;
            rijManaged.BlockSize = 128;
            rijManaged.KeySize = 128;
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

            Clipboard.SetText(key);
            Terminal.WriteLine($"Ключ скопирован в буфер обмена.", "+g4\0Key+g0\0");

            ProfileLicense profile = new AlhoritmAxTools(TypeLic).StrToAd(key);

            if (profile != null)
            {
                Terminal.WriteLine($"Верен ли ключ: {true}", "+g4\0License+g0\0");
                Terminal.WriteLine($"Индификатор лицензии: {profile.ID}", "+g4\0License+g0\0");
                Terminal.WriteLine($"Имя владельца лицензии: {profile.LName}", "+g4\0License+g0\0");
                Terminal.WriteLine($"Кол-во: {profile.SCount}", "+g4\0License+g0\0");
                Terminal.WriteLine($"Дата окончания лицензии: {profile.EDate}", "+g4\0License+g0\0");
            }
            else
                Terminal.WriteLine($"Верен ли ключ: {false}", "+g4\0License+g0\0");
            Terminal.Pause();
        }
    }

    /// <summary>
    /// Данные о лицензии
    /// </summary>
    public class ProfileLicense
    {
        public string LName;
        public string ID;
        public int SCount;
        public DateTime EDate;
    }
    /// <summary>
    /// Копипаст алгоритма из библиотеки AxTools
    /// Данный алгоритм проверяет ключ и разбирает его на составляющие
    /// </summary>
    public class AlhoritmAxTools
    {
        private readonly string _header = string.Empty;
        private readonly string _product = string.Empty;
        public AlhoritmAxTools(int prod)
        {
            switch (prod)
            {
                case 0:
                    this._header = "#$a";
                    this._product = "vs10xcodemapexts";
                    return;
                case 1:
                    this._header = "#$b";
                    this._product = "vs10xeditorviewe";
                    return;
                case 2:
                    this._header = "#$c";
                    this._product = "vs10xcommentsext";
                    return;
                default:
                    return;
            }
        }
        public ProfileLicense StrToAd(string s)
        {
            s = s.Trim();
            if (!s.StartsWith(this._header))
                return null;
            s = s.Substring(this._header.Length);
            byte[] array = Convert.FromBase64String(s);
            Encoding coder = Encoding.UTF8;
            byte[] bytes = coder.GetBytes("favoriteshistory");
            byte[] bytes2 = coder.GetBytes(this._product);
            RijndaelManaged rijndaelManaged = new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.Zeros,
                BlockSize = 128,
                KeySize = 128
            };
            ICryptoTransform transform = null;
            try
            {
                transform = rijndaelManaged.CreateDecryptor(bytes2, bytes);
            }
            catch (CryptographicException ex)
            {
                Terminal.WriteLine(ex.ToString());
            }
            catch (SystemException ex)
            {
                Terminal.WriteLine(ex.ToString());
            }
            byte[] array2 = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(array, 0, array.GetLength(0));
                    cryptoStream.FlushFinalBlock();
                }
                rijndaelManaged.Clear();
                array2 = memoryStream.ToArray();
            }
            string text = string.Empty;
            if (array2 != null)
                text = new string(Encoding.UTF8.GetChars(array2));
            if (text.Length != 96)
                return null;
            if (!text.Substring(0, 3).Equals(this._header))
                return null;
            ProfileLicense codeMapAd = new ProfileLicense();
            codeMapAd.LName = text.Substring(3, 50).Trim(new char[] { '~' });
            codeMapAd.ID = text.Substring(53, 23);
            ProfileLicense codeMapAd2 = codeMapAd;
            try
            {
                codeMapAd2.SCount = int.Parse(text.Substring(76, 4));
            }
            catch
            {
                codeMapAd = null;
                return codeMapAd;
            }
            try
            {
                codeMapAd2.EDate = new DateTime(int.Parse("20" + text.Substring(80, 2)), int.Parse(text.Substring(82, 2)), int.Parse(text.Substring(84, 2)));
            }
            catch
            {
                codeMapAd = null;
                return codeMapAd;
            }
            return codeMapAd2;
        }
    }
}
