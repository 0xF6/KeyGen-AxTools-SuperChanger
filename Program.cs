using System.Drawing;
using System.Windows.Forms;
using KeyGenAxTools;
using Rc.Framework.Screens;
using RC.Framework;
using RCL = Rc.Framework.Screens.RCL;
using Screen = Rc.Framework.Screens.Screen;

namespace KeyGen
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public enum TypeProduct
    {
        AxTools,
        SuperChanger
    }

    public class Program
    {
        [STAThread]
        static void Main(string[] args) { new Program().tMain(args); }
        public void tMain(string[] args)
        {
            Screen.Title = "KeyGen";
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


            StringBuilder builder = new StringBuilder();

            builder.AppendLine("+-------------------------++");
            builder.AppendLine("| Версия - AxTools10      | \\");
            builder.AppendLine("+-------------------------+---+");
            builder.AppendLine("|  xCode Map              | 0 |");
            builder.AppendLine("+-------------------------+---+");
            builder.AppendLine("|  xEditor Viem           | 1 |");
            builder.AppendLine("+-------------------------+---+");
            builder.AppendLine("|  xComment               | 2 |");
            builder.AppendLine("+-------------------------+---+");
            builder.AppendLine("| Версия - SuperChanger   |   |");
            builder.AppendLine("+-------------------------+---+");
            builder.AppendLine("|  Business               | 3 |");
            builder.AppendLine("+-------------------------+---+");
            builder.AppendLine("|  Individual Pro         | 4 |");
            builder.AppendLine("+-------------------------+---+");
            builder.AppendLine("|  Individual             | 5 |");
            builder.AppendLine("+-------------------------+---+");


            string tr = builder.ToString();

            tr = tr
                    .Map(new[] {"0", "1", "2", "3", "4", "5"}, Color.Red)
                    .Map(new[] { "SuperChanger", "AxTools" }, Color.Chartreuse)
                    .Map(new[] { "Map", "xEditor", "xCode", "xComment", "Viem" }, Color.DeepPink)
                    .Map(new[] { "Business", "Individual", "Pro" }, Color.LightCoral)
                    .Map(new[] { "+", "-", "|", "\\" }, Color.Gray)
                ;

            Screen.WriteLine(tr);
            TypeProduct Type = TypeProduct.AxTools;

            TR_0:
            Screen.Write($"[{RCL.Wrap("KeyGen", Color.DarkOrchid)}] Введите тип продукта :>");
            int i;
            if (Int32.TryParse(Console.ReadLine(), out i))
            {
                if (!(i >= 0 && i <= 5))
                {
                    Screen.WriteLine($"[{RCL.Wrap("KeyGen", Color.DarkOrchid)}] Тип продукта не верен!");
                    goto TR_0;
                }
            }
            else
            {
                Screen.WriteLine($"[{RCL.Wrap("KeyGen", Color.DarkOrchid)}] Тип продукта не является числом!");
                goto TR_0;
            }
            //
            int TypeLic = i;
            Encoding coding = Encoding.UTF8;
            //
            ProfileLicense ad = new ProfileLicense();
            ad.ID = $"AxTools License ID Code";


            switch (TypeLic)
            {
                case 0:
                case 1:
                case 2: Type = TypeProduct.AxTools; break;
                case 3:
                case 4:
                case 5: Type = TypeProduct.SuperChanger; break;
            }

            Screen.Title = "KeyGen " + Type;


            TR_1:
            Screen.Write($"[{RCL.Wrap("KeyGen", Color.DarkOrchid)}] Введите своё имя :>");
            string name = Console.ReadLine();
            if (name.Length > 20)
            {
                Screen.WriteLine($"[{RCL.Wrap("KeyGen", Color.DarkOrchid)}] Имя слишком длинное!");
                goto TR_1;
            }
            Screen.Write($"[{RCL.Wrap("KeyGen", Color.DarkOrchid)}] Введите свою фамилию :>");
            string family = Console.ReadLine();
            if (family.Length > 20)
            {
                Screen.WriteLine($"[{RCL.Wrap("KeyGen", Color.DarkOrchid)}] Фамилия слишком длинная!");
                goto TR_1;
            }
            if (Type == TypeProduct.AxTools)
                ad.LName = $"{name} {family}{new string('~', 50 - (name.Length + family.Length + 1))}";
            else
            {
                ad.LName = $"{name} {family}";
                ad.EDate = DateTime.Now.AddYears(50);
            }


            string key = "";

            

            switch (TypeLic)
            {
                case 0:
                case 1:
                case 2:
                    key = LicenseDecryptor.Encrypt(TypeLic, ad); break;
                case 3:
                    key = LicenseDecryptor.EncryptNew(SuperChangeLicenseType.Business, ad); break;
                case 4:
                    key = LicenseDecryptor.EncryptNew(SuperChangeLicenseType.ProInv, ad); break;
                case 5:
                    key = LicenseDecryptor.EncryptNew(SuperChangeLicenseType.Inv, ad); break;
            }
            
            Clipboard.SetText(key);
            Screen.WriteLine($"[{RCL.Wrap("KeyGen", Color.DarkOrchid)}] Ключ скопирован в буфер обмена.");

            Screen.WriteLine("");
            Screen.WriteLine("");

            if (Type == TypeProduct.AxTools)
            {
                ProfileLicense profile = new AlhoritmAxTools(TypeLic).StrToAd(key);

                if (profile != null)
                {
                    Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] Верен ли ключ: {RCL.Wrap(true, Color.CadetBlue)}");
                    Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] Тип лицензии: {Type}");
                    Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] Индификатор лицензии: {profile.ID}");
                    Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] Имя владельца лицензии: {profile.LName}");

                    Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] Кол-во: {profile.SCount}");
                    Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] Дата окончания лицензии: {profile.EDate}");
                }
                else
                    Screen.WriteLine($"Верен ли ключ: {RCL.Wrap(false, Color.CadetBlue)}");
            }
            else
            {
                ProfileLicense profile = ad;
                Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] Верен ли ключ: {RCL.Wrap(true, Color.CadetBlue)}");
                Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] Тип лицензии: {Type}");
                Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] Индификатор лицензии: {profile.ID}");
                Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] Имя владельца лицензии: {profile.LName}");
                Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] Дата окончания лицензии: {profile.EDate}");
            }
                
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
