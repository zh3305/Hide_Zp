namespace HongHu.UI
{
    using HongHu.Properties;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    public class ClosButton : PictureBox
    {
        private Bitmap closeBmp = Resource.btn_close_normal;
        private Graphics g = null;
        private ImageAttributes imageAttr = new ImageAttributes();

        public ClosButton()
        {
            this.BackColor = Color.Transparent;
            base.Size = new Size(0x26, 0x12);
            base.TabIndex = 0x15;
            base.TabStop = false;
            base.MouseLeave += new EventHandler(this.ButtonClose_MouseLeave);
            base.MouseDown += new MouseEventHandler(this.ButtonClose_MouseDown);
            base.MouseClick += new MouseEventHandler(this.ButtonClose_MouseClick);
            base.Paint += new PaintEventHandler(this.ButtonClose_Paint);
            base.MouseUp += new MouseEventHandler(this.ButtonClose_MouseUp);
            base.MouseEnter += new EventHandler(this.ButtonClose_MouseEnter);
        }

        private void ButtonClose_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.GetParentForm(((PictureBox) sender).Parent).Close();
            }
        }

        private void ButtonClose_MouseDown(object sender, MouseEventArgs e)
        {
            this.closeBmp = Resource.btn_close_down;
            base.Invalidate();
        }

        private void ButtonClose_MouseEnter(object sender, EventArgs e)
        {
            this.closeBmp = Resource.btn_close_highlight;
            base.Invalidate();
        }

        private void ButtonClose_MouseLeave(object sender, EventArgs e)
        {
            this.closeBmp = Resource.btn_close_normal;
            base.Invalidate();
        }

        private void ButtonClose_MouseUp(object sender, MouseEventArgs e)
        {
            if (!base.IsDisposed)
            {
                this.closeBmp = Resource.btn_close_normal;
                base.Invalidate();
            }
        }

        private void ButtonClose_Paint(object sender, PaintEventArgs e)
        {
            this.g = e.Graphics;
            this.g.DrawImage(this.closeBmp, new Rectangle(0, 0, this.closeBmp.Width, this.closeBmp.Height), 0, 0, this.closeBmp.Width, this.closeBmp.Height, GraphicsUnit.Pixel, this.imageAttr);
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

