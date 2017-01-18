using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace VkGraffiti
{
    public partial class FaceBox : UserControl
    {
        private int uid = 0;
        private Mouse_Click mouse_click;

        public FaceBox(int id, string Name, int online, int mobile, string photoUrl, Mouse_Click dg)
        {
            InitializeComponent();
                                        
            Handed();
            mouse_click = dg;
            uid = id;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox2.BackColor = Color.Transparent;
            roundPictureBox1.LoadAsync(photoUrl);
            label1.Text = Name;
            if (online == 1)
                if (mobile == 1)
                    pictureBox1.Visible = true;
                else
                    pictureBox2.Visible = true;
        }

        public void Handed()
        {
            GotFocus += FaceBox_GotFocus;
            LostFocus += FaceBox_LostFocus;         
        }

        void FaceBox_LostFocus(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.Control;
        }

        void FaceBox_GotFocus(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.ActiveCaption;             
        }


        public string LabelName
        {
            get { return label1.Text; }
        }

        public int UserID
        {
            get { return uid; }
        }

        public Image Photo
        {
            get { return roundPictureBox1.Image; }
        }

        private void roundPictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            mouse_click(this);
        }

        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            mouse_click(this);
        }

        private void FaceBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            mouse_click(this);
        }

        private void FaceBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
                mouse_click(this);
        }

        private void control_Click(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}
