using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Threading;
using System.Diagnostics;
using Client;
using GMusicBox;

namespace Client
{
    public partial class frmAbout : PerPixelAlphaBlendForm
    {

        private Bitmap b = null;
        public frmAbout()
        {
            this.InitializeComponent();

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.b = new Bitmap(((System.Drawing.Image) resources.GetObject("BackgroundImage")));
           // this.b = new Bitmap(this.BackgroundImage);
            Font font = new Font("微软雅黑", 9f, FontStyle.Regular);
            Graphics graphics = Graphics.FromImage(this.b);
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            //if (this.devmode)
            //{
            //    graphics.DrawString(res.Version + " dev", font, Brushes.Black, new PointF(240f, 67f));
            //}
            //else
            //{
                graphics.DrawString("3.0.1.49", font, Brushes.Black, new PointF(240f, 67f));
            //}
            graphics.DrawString("http://music.google.org.cn", font, Brushes.RoyalBlue, new PointF(107f, 108f));
            graphics.Dispose();
            font.Dispose();
        }
        private void frmAbout_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void frmAbout_Deactivate(object sender, EventArgs e)
        {
            base.Close();
        }

        private void frmAbout_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                for (double i = 1.0; i >= 0.0; i -= 0.1)
                {
                    this.SetBitmap(this.b, (byte)(255.0 * i));
                    Thread.Sleep(0x19);
                }
            }
            catch
            {
            }
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            base.Show();
            for (double i = 0.0; i <= 1.0; i += 0.1)
            {
                this.SetBitmap(this.b, (byte)(255.0 * i));
                this.Refresh();
                Thread.Sleep(0x19);
            }
        }
        private void lnkWebSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://music.google.org.cn/");
        }
    }
}
