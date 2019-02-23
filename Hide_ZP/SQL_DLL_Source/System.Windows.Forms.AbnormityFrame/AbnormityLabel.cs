namespace System.Windows.Forms.AbnormityFrame
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AbnormityLabel : Label
    {
        private bool _AllowDrag = false;
        private bool isMouseDown = false;
        private Point mouseOffset;
        private Form ParentForm = null;
        private long timeOffset = DateTime.Now.ToBinary();

        private void _MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mouseOffset = e.Location;
                this.isMouseDown = true;
                Control t = this;
                do
                {
                    t = t.Parent;
                }
                while (!(t is Form));
                this.ParentForm = (Form) t;
            }
        }

        private void _MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isMouseDown && (((DateTime.Now.ToBinary() - this.timeOffset) / 0x493e0L) > 0L))
            {
                this.timeOffset = DateTime.Now.ToBinary();
                this.ParentForm.Left += e.X - this.mouseOffset.X;
                this.ParentForm.Top += e.Y - this.mouseOffset.Y;
            }
        }

        private void _MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.isMouseDown = false;
            }
        }

        [DefaultValue(true)]
        public bool AllowDrag
        {
            get
            {
                return this._AllowDrag;
            }
            set
            {
                if (value)
                {
                    if (!this._AllowDrag)
                    {
                        base.MouseMove += new MouseEventHandler(this._MouseMove);
                        base.MouseUp += new MouseEventHandler(this._MouseUp);
                        base.MouseDown += new MouseEventHandler(this._MouseDown);
                    }
                }
                else if (this._AllowDrag)
                {
                    base.MouseMove -= new MouseEventHandler(this._MouseMove);
                    base.MouseUp -= new MouseEventHandler(this._MouseUp);
                    base.MouseDown -= new MouseEventHandler(this._MouseDown);
                }
                this._AllowDrag = value;
            }
        }

        public Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
            }
        }
    }
}

