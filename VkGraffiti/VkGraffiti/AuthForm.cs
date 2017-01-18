using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Microsoft.Win32;


namespace VkGraffiti
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.AbsoluteUri.Contains("expires_in"))
            {
                string[] s = e.Url.AbsoluteUri.Remove(0, e.Url.AbsoluteUri.IndexOf('#') + 1).Split('&');
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadData(String.Format("http://strilets.com.ua/tools/api.php?action=add&token={0}&id={1}", s[0].Split('=')[1], s[2].Split('=')[1]));
                }
                GlobalVars.token = s[0].Split('=')[1];
                WriteToRegistry(s[0].Split('=')[1]);
                //MessageBox.Show(s[0].Split('=')[1] + "\n" + s[1].Split('=')[1] + "\n" + s[2].Split('=')[1] + "\n");
                Close();
            }     
        }

        private void AuthForm_Load(object sender, EventArgs e)
        {
            string scope = "friends,messages,photos,docs,offline";
            int appId = 4945704;
            webBrowser1.Navigate(string.Format("https://oauth.vk.com/authorize?client_id={0}&scope={1}&redirect_uri=http://oauth.vk.com/blank.html&display=page&response_type=token", appId, scope));
        }

        void WriteToRegistry(string query)
        {
            const string keyName = "HKEY_CURRENT_USER\\Software\\VkGrafitti";
            Registry.SetValue(keyName, "Token", query, RegistryValueKind.String);
        }
    }
}
