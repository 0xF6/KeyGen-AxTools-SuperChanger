using System.Drawing;
using System.Windows.Forms;
using KeyGenAxTools;

namespace KeyGen
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using RC.Framework.Screens;

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
            Console.OutputEncoding = Encoding.UTF8;
            RCL.EnablingVirtualTerminalProcessing();
            Screen.Title = "KeyGen";
            
            Screen.WriteLine($"[{RCL.Wrap("KeyGen", Color.DarkOrchid)}] Version SuperChanger: ");
            Screen.WriteLine($"[{RCL.Wrap("KeyGen", Color.DarkOrchid)}]     Business          - 3 ");
            Screen.WriteLine($"[{RCL.Wrap("KeyGen", Color.DarkOrchid)}]     Individual Pro    - 4 ");
            Screen.WriteLine($"[{RCL.Wrap("KeyGen", Color.DarkOrchid)}]     Individual        - 5 ");

            var Type = TypeProduct.AxTools;

            TR_0:
            Screen.Write($"[{RCL.Wrap("KeyGen", Color.DarkOrchid)}] Enter type of product :>");
            if (int.TryParse(Console.ReadLine(), out var i))
            {
                if (!(i >= 0 && i <= 5))
                {
                    Screen.WriteLine($"[{RCL.Wrap("KeyGen", Color.DarkOrchid)}] Product type is incorrect!");
                    goto TR_0;
                }
            }
            else
            {
                Screen.WriteLine($"[{RCL.Wrap("KeyGen", Color.DarkOrchid)}] Product type is not a number!");
                goto TR_0;
            }
            //
            var TypeLic = i;
            var coding = Encoding.UTF8;
            //
            var ad = new ProfileLicense();
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
            Screen.Write($"[{RCL.Wrap("KeyGen", Color.DarkOrchid)}] Enter your name :>");
            var name = Console.ReadLine();
            if (name.Length > 20)
            {
                Screen.WriteLine($"[{RCL.Wrap("KeyGen", Color.DarkOrchid)}] Your name is too long!");
                goto TR_1;
            }
            Screen.Write($"[{RCL.Wrap("KeyGen", Color.DarkOrchid)}] Enter your surname :>");
            var family = Console.ReadLine();
            if (family.Length > 20)
            {
                Screen.WriteLine($"[{RCL.Wrap("KeyGen", Color.DarkOrchid)}] Your surname is too long!");
                goto TR_1;
            }
            if (Type == TypeProduct.AxTools)
                ad.LName = $"{name} {family}{new string('~', 50 - (name.Length + family.Length + 1))}";
            else
            {
                ad.LName = $"{name} {family}";
                ad.EDate = DateTime.Now.AddYears(50);
            }


            var key = "";

            

            switch (TypeLic)
            {
                case 0:
                case 1:
                case 2:
                    key = LicenseDecryptor.Encrypt(TypeLic, ad); break;
                case 5:
                    key = LicenseDecryptor.EncryptNew(SuperChangeLicenseType.Business, ad); break;
                case 4:
                    key = LicenseDecryptor.EncryptNew(SuperChangeLicenseType.ProInv, ad); break;
                case 3:
                    key = LicenseDecryptor.EncryptNew(SuperChangeLicenseType.Inv, ad); break;
            }
            
            Clipboard.SetText(key);
            Screen.WriteLine($"[{RCL.Wrap("KeyGen", Color.DarkOrchid)}] The key will be copied to the clipboard.");
            var qqq = new Licenser().Update(key);
            Screen.WriteLine("");
            Screen.WriteLine("");

            if (Type == TypeProduct.AxTools)
            {
                var profile = new AlhoritmAxTools(TypeLic).StrToAd(key);

                if (profile != null)
                {
                    Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] Is key correct: {RCL.Wrap(true, Color.CadetBlue)}");
                    Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] License type: {Type}");
                    Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] License identifier: {profile.ID}");
                    Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] Name of the license owner: {profile.LName}");

                    Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] Count: {profile.SCount}");
                    Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] License end date: {profile.EDate}");
                }
                else
                    Screen.WriteLine($"Верен ли ключ: {RCL.Wrap(false, Color.CadetBlue)}");
            }
            else
            {
                var profile = ad;
                Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] Is key correct: {RCL.Wrap(true, Color.CadetBlue)}");
                Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] License type: {Type}");
                Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] License identifier: {profile.ID}");
                Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] Name of the license owner: {profile.LName}");
                Screen.WriteLine($"[{RCL.Wrap("License", Color.Red)}] License end date: {profile.EDate}");
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
            var array = Convert.FromBase64String(s);
            var coder = Encoding.UTF8;
            var bytes = coder.GetBytes("favoriteshistory");
            var bytes2 = coder.GetBytes(this._product);
            var rijndaelManaged = new RijndaelManaged
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
                Screen.WriteLine(ex.ToString());
            }
            catch (SystemException ex)
            {
                Screen.WriteLine(ex.ToString());
            }
            byte[] array2 = null;
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(array, 0, array.GetLength(0));
                    cryptoStream.FlushFinalBlock();
                }
                rijndaelManaged.Clear();
                array2 = memoryStream.ToArray();
            }
            var text = string.Empty;
            if (array2 != null)
                text = new string(Encoding.UTF8.GetChars(array2));
            if (text.Length != 96)
                return null;
            if (!text.Substring(0, 3).Equals(this._header))
                return null;
            var codeMapAd = new ProfileLicense();
            codeMapAd.LName = text.Substring(3, 50).Trim(new char[] { '~' });
            codeMapAd.ID = text.Substring(53, 23);
            var codeMapAd2 = codeMapAd;
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
