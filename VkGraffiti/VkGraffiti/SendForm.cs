using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;

namespace VkGraffiti
{
    public partial class SendForm : Form
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        string path = "", name = "";



        public SendForm(int user_id, Image photo, string user_name)
        {
            InitializeComponent();
            GlobalVars.user_id = user_id.ToString();
            pictureBox1.Image = photo;
            label1.Text = user_name;
        }



        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (path!="")
            {
                btn_Send.Enabled = false;
                using (WebClient wc = new WebClient())
                {
                    wc.UploadProgressChanged += wc_UploadProgressChanged;
                    wc.UploadFileCompleted += wc_UploadFileCompleted;
                    JSON_GetServer up = JsonConvert.DeserializeObject<JSON_GetServer>(wc.DownloadString("https://api.vk.com/method/docs.getUploadServer?lang=ru&type=graffiti&v=5.62&access_token="+GlobalVars.token));
                    DocObject file = JsonConvert.DeserializeObject<DocObject>(Encoding.UTF8.GetString(wc.UploadFile(up.response.upload_url, path)));
                    DocInformation saved = JsonConvert.DeserializeObject<DocInformation>(wc.DownloadString(String.Format("https://api.vk.com/method/docs.save?file={0}&title={1}&lang=ru&access_token={2}&v=5.62", file.file, name, GlobalVars.token)));
                    MsgSend(richTextBox1.Text, "doc" + saved.response[0].owner_id + "_" + saved.response[0].id);                                
                }
            }
            else
            {
                MessageBox.Show("Сначала следует выбрать граффити");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                name = openFileDialog1.SafeFileName;
                pictureBox2.Image = Image.FromFile(path);

            }
        } 

        private void MsgSend(string msg, string attachment)
        {
            using (WebClient wc = new WebClient())
            {
                wc.DownloadString(string.Format("https://api.vk.com/method/messages.send?user_id={3}&attachment={0}&message={1}&v=5.62&access_token={2}", attachment, msg, GlobalVars.token, GlobalVars.user_id));           
            }
            btn_Send.Enabled = true;
            DialogResult dialogResult = MessageBox.Show("Успешно отправленно!!!\n Хотите отправить что-то ещё?", "Отправлено", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                path = "";
            }
            else
                this.Close();   
        }

        private void SendForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        void wc_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            progressBar1.Value = 0;
        }

        void wc_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
         
    }
}