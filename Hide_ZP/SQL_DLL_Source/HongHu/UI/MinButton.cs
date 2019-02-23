namespace HongHu.UI
{
    using HongHu.Properties;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    public class MinButton : PictureBox
    {
        private Graphics g = null;
        private ImageAttributes imageAttr = new ImageAttributes();
        private Bitmap minBmp = Resource.btn_mini_normal;

        public MinButton()
        {
            this.BackColor = Color.Transparent;
            base.Location = new Point(0x1bb, 1);
            base.Name = "ButtonMin";
            base.Size = new Size(0x19, 0x12);
            base.SizeMode = PictureBoxSizeMode.AutoSize;
            base.TabIndex = 20;
            base.TabStop = false;
            base.MouseLeave += new EventHandler(this.ButtonMin_MouseLeave);
            base.MouseClick += new MouseEventHandler(this.ButtonMin_MouseClick);
            base.MouseDown += new MouseEventHandler(this.ButtonMin_MouseDown);
            base.Paint += new PaintEventHandler(this.ButtonMin_Paint);
            base.MouseUp += new MouseEventHandler(this.ButtonMin_MouseUp);
            base.MouseEnter += new EventHandler(this.ButtonMin_MouseEnter);
        }

        private void ButtonMin_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.GetParentForm(((MinButton) sender).Parent).Hide();
            }
        }

        private void ButtonMin_MouseDown(object sender, MouseEventArgs e)
        {
            this.minBmp = Resource.btn_mini_down;
            base.Invalidate();
        }

        private void ButtonMin_MouseEnter(object sender, EventArgs e)
        {
            this.minBmp = Resource.btn_mini_highlight;
            base.Invalidate();
        }

        private void ButtonMin_MouseLeave(object sender, EventArgs e)
        {
            this.minBmp = Resource.btn_mini_normal;
            base.Invalidate();
        }

        private void ButtonMin_MouseUp(object sender, MouseEventArgs e)
        {
            this.minBmp = Resource.btn_mini_normal;
            base.Invalidate();
        }

        private void ButtonMin_Paint(object sender, PaintEventArgs e)
        {
            this.g = e.Graphics;
            this.g.DrawImage(this.minBmp, new Rectangle(0, 0, this.minBmp.Width, this.minBmp.Height), 0, 0, this.minBmp.Width, this.minBmp.Height, GraphicsUnit.Pixel, this.imageAttr);
        }

        private Form GetParentForm(Control parent)
        {
            Form form = parent as Form;
            if (form != null)
            {
                return form;
            }
            if (parent != null)
            {
                return this.GetParentForm(parent.Parent);
            }
            return null;
        }
    }
}

