using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.Win32;

namespace VkGraffiti
{
    public partial class MainForm : Form
    {
        # region interop
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion

        List<FaceBox> LFb = new List<FaceBox>();
        List<FaceBox> LFbSort = new List<FaceBox>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LFbSort.Clear();
            if (tb_Search.Text != "")
            {
                foreach (FaceBox fb in LFb)
                {
                    if (fb.LabelName.ToLower().Contains(tb_Search.Text.ToLower()))
                        LFbSort.Add(fb);
                }
            }
            else
            {
                LFbSort.AddRange(LFb);
            }

            Draw();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            if (GlobalVars.token == null)
                new AuthForm().ShowDialog();

            using (WebClient wc = new WebClient())
            {
                Me id = JsonConvert.DeserializeObject<Me>(Encoding.UTF8.GetString(wc.DownloadData("https://api.vk.com/method/docs.get?count=1&v=5.62&access_token=" + GlobalVars.token)));
                Own me = JsonConvert.DeserializeObject<Own>(Encoding.UTF8.GetString(wc.DownloadData(String.Format("https://api.vk.com/method/users.get?user_id={0}&fields=online,photo_50&token={1}&v=5.62", id.response.items[0].owner_id, GlobalVars.token))));
                FaceBox fbm = new FaceBox(me.response[0].id, me.response[0].first_name + " " + me.response[0].last_name, me.response[0].online, 0, me.response[0].photo_50, new Mouse_Click(fb_DoubleClick));
                LFb.Add(fbm);

                string s = Encoding.UTF8.GetString(wc.DownloadData("https://api.vk.com/method/friends.get?order=hints&fields=online,photo_50&v=5.62&access_token=" + GlobalVars.token));
                RootObject rt = JsonConvert.DeserializeObject<RootObject>(s);
             
                foreach (Item resp in rt.response.items)
                {
                    try
                    {
                        FaceBox fb = new FaceBox(resp.id, resp.first_name + " " + resp.last_name, resp.online, resp.online_mobile, resp.photo_50, new Mouse_Click(fb_DoubleClick));
                        LFb.Add(fb);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }

                LFbSort.AddRange(LFb);
                Draw();
            }
        }


        private void Draw()
        {
            int y = 0;
            panel_Faces.Controls.Clear();
            foreach (FaceBox fb in LFbSort)
            {
                fb.Location = new Point(1, y);
                y += 57;
                panel_Faces.Controls.Add(fb);
            }
        }

        private void fb_DoubleClick(FaceBox fb)
        {
            SendForm sf = new SendForm(fb.UserID, fb.Photo, fb.LabelName);
            sf.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            tb_Search.Focus();
        }

    }
}