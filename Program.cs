using System.Drawing;
using System.Windows.Forms;
using KeyGenAxTools;
using RC.Framework;
using RCL = Rc.Framework.Screens.RCL;
using Screen = Rc.Framework.Screens.Screen;

namespace KeyGen
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class Program
    {
        [STAThread]
        static void Main(string[] args) { new Program().tMain(args); }
        public void tMain(string[] args)
        {
            string licse =
                $"Yuuki{new string('~', 50 - ("Yuuki").Length)}Wesp{new string(' ', 23 - "Wesp".Length)}{2020}{20}{12}{12}000";
            string value = "6k";
            string value2 = "6p";
            string value3 = "tt";
            var ls = LicenseDecryptor.EncryptNew(SuperChangeLicenseType.Bisnes, 
                new ProfileLicense() {EDate = DateTime.Now.AddYears(55), ID = "1", LName = "Yuuki Wesp", SCount = 3333});
            // text.Length > 100 && (text.StartsWith(value) || text.StartsWith(value2) || text.StartsWith(value3))
            // tt || 6k || 6p

            string lic = $"tt{new string('0', 100)}";

            string a = u000Eu2006u2008.u00023("d/59m2LflM3hLZkNi8N2LNvkqGcGLxjkNdLwAgpAD7aO6RnYf6Y1XGIM0Gdx5HT5V+me0vWec+y/34uE9ZRePw==");


            Screen.Title = "KeyGen - AxTools 10";
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
            Screen.WriteLine($"{RCL.Wrap("+-------------------------+-++", Color.Bisque)}");
            Screen.WriteLine($"| Версия - AxTools10{new string(' ', 7)}\\|\\");
            Screen.WriteLine($"+---------------------------+-+-+");
            Screen.WriteLine($"| xCode Map{new string(' ', 17)}| 0 |");
            Screen.WriteLine($"+---------------------------+---+");
            Screen.WriteLine($"| xEditor Viem{new string(' ', 14)}| 1 |");
            Screen.WriteLine($"+---------------------------+---+");
            Screen.WriteLine($"| xComment{new string(' ', 18)}| 2 |");
            Screen.WriteLine($"{RCL.Wrap("+-------------------------+-++", Color.Bisque)}");


            Screen.WriteLine($"{RCL.Wrap("+-------------------------+---+", Color.Bisque)}");

            StringBuilder builder = new StringBuilder();

            builder.AppendLine("+-------------------------+---+");
            builder.AppendLine("| Версия - AxTools10      |---|");
            builder.AppendLine("+-------------------------+---+");
            builder.AppendLine("|-------------------------| 0 |");
            builder.AppendLine("|-------------------------| 1 |");
            builder.AppendLine("|-------------------------| 2 |");
            builder.AppendLine("+-------------------------+---+");
            builder.AppendLine("| Версия - SuperChanger   +---+");
            builder.AppendLine("+-------------------------+---+");
            builder.AppendLine("|-------------------------| 3 |");
            builder.AppendLine("|-------------------------| 4 |");
            builder.AppendLine("|-------------------------| 5 |");
            builder.AppendLine("+-------------------------+---+");


            string tr = builder.ToString();

            tr = tr
                .Replace("0", RCL.Wrap("0", Color.Red))
                .Replace("1", RCL.Wrap("1", Color.Red))
                .Replace("2", RCL.Wrap("2", Color.Red))
                .Replace("3", RCL.Wrap("3", Color.Red))
                .Replace("4", RCL.Wrap("4", Color.Red))
                .Replace("5", RCL.Wrap("5", Color.Red))

                .Replace("SuperChanger", RCL.Wrap("SuperChanger", Color.Chartreuse))
                .Replace("AxTools10", RCL.Wrap("AxTools10", Color.Chartreuse))

                ;

            Screen.WriteLine(tr);


            TR_0:
            Screen.Write("Введите тип продукта :>");
            int i;
            if (Int32.TryParse(Console.ReadLine(), out i))
            {
                if (!(i >= 0 && i <= 2))
                {
                    Screen.WriteLine("Ошибка! Тип продукта не верен!");
                    goto TR_0;
                }
            }
            else
            {
                Screen.WriteLine("Ошибка! Тип продукта не является числом!");
                goto TR_0;
            }
            //
            int TypeLic = i;
            Encoding coding = Encoding.UTF8;
            //
            ProfileLicense ad = new ProfileLicense();
            ad.ID = $"AxTools License ID Code";

            TR_1:
            Screen.Write("Введите своё имя :>");
            string name = Console.ReadLine();
            if (name.Length > 20)
            {
                Screen.WriteLine("Имя слишком длинное!");
                goto TR_1;
            }
            Screen.Write("Введите свою фамилию :>");
            string family = Console.ReadLine();
            if (family.Length > 20)
            {
                Screen.WriteLine("Фамилия слишком длинная!");
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
            Screen.WriteLine($"Ключ скопирован в буфер обмена.");

            ProfileLicense profile = new AlhoritmAxTools(TypeLic).StrToAd(key);

            if (profile != null)
            {
                Screen.WriteLine($"Верен ли ключ: {true}");
                Screen.WriteLine($"Индификатор лицензии: {profile.ID}");
                Screen.WriteLine($"Имя владельца лицензии: {profile.LName}");
                Screen.WriteLine($"Кол-во: {profile.SCount}");
                Screen.WriteLine($"Дата окончания лицензии: {profile.EDate}");
            }
            else
                Screen.WriteLine($"Верен ли ключ: {false}");
            Console.ReadKey();
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
