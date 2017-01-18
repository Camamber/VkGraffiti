using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VkGraffiti
{
    public delegate void Mouse_Click(FaceBox fb);
    static class Program
    {
       
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (ReadRegistry() != "")
                GlobalVars.token = ReadRegistry();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static string ReadRegistry()
        {
            const string keyName = "HKEY_CURRENT_USER\\Software\\VkGrafitti";
            if (Registry.CurrentUser.OpenSubKey("Software\\VkGrafitti") != null)
                return (string)Registry.GetValue(keyName, "Token", "");
            else
                Registry.CurrentUser.CreateSubKey("Software\\VkGrafitti"); return "";

        }
    }
}
