using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.AbnormityFrame
{
    internal static class AbnormityControl
    {
        public static GraphicsPath CalculateControlGraphicsPath(Image image)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            Bitmap bitmap = (Bitmap)image;
            Color colorTransparent = bitmap.GetPixel(0, 0);
            int colOpaquePixel = 0;
            for (int row = 0; row < bitmap.Height; row++)
            {
                colOpaquePixel = 0;
                for (int col = 0; col < bitmap.Width; col++)
                {
                    if (bitmap.GetPixel(col, row) != colorTransparent)
                    {
                        colOpaquePixel = col;
                        int colNext = col;
                        for (colNext = colOpaquePixel; colNext < bitmap.Width; colNext++)
                            if (bitmap.GetPixel(colNext, row) == colorTransparent)
                                break;
                        graphicsPath.AddRectangle(new Rectangle(colOpaquePixel, row, colNext - colOpaquePixel, 1));
                        col = colNext;
                    }
                }
            }
            return graphicsPath;
        }
    }

    public class AbnormityForm : Form
    {
        //public override Image BackgroundImage
        //{
        //    get
        //    {
        //        return base.BackgroundImage;
        //    }
        //    set
        //    {
        //        this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        //        this.FormBorderStyle = FormBorderStyle.None;
        //        this.Width = value.Width;
        //        this.Height = value.Height;
        //        base.BackgroundImage = value;
        //        this.Region = new Region(AbnormityControl.CalculateControlGraphicsPath(value));
        //    }
        //}

        #region 支持窗体拖动的代码

        private bool isMouseDown = false;
        private Point mouseOffset;
        private long timeOffset = DateTime.Now.ToBinary();
        private bool _AllowDrag = false;

        [DefaultValue(true)]
        public bool AllowDrag
        {
            get
            {
                return _AllowDrag;
            }
            set
            {
                if (value)
                {
                    if (!_AllowDrag)
                    {
                        this.MouseMove += new MouseEventHandler(_MouseMove);
                        this.MouseUp += new MouseEventHandler(_MouseUp);
                        this.MouseDown += new MouseEventHandler(_MouseDown);
                    }
                }
                else
                {
                    if (_AllowDrag)
                    {
                        this.MouseMove -= new MouseEventHandler(_MouseMove);
                        this.MouseUp -= new MouseEventHandler(_MouseUp);
                        this.MouseDown -= new MouseEventHandler(_MouseDown);
                    }
                }
                _AllowDrag = value;
            }
        }

        private void _MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOffset = e.Location;
                isMouseDown = true;
                // this.Opacity = 0.85;
            }
        }

        private void _MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
                //this.Opacity = 1;
            }
        }

        private void _MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown && ((DateTime.Now.ToBinary() - timeOffset) / 300000 > 0))
            {
                timeOffset = DateTime.Now.ToBinary();
                this.Left += e.X - mouseOffset.X;
                this.Top += e.Y - mouseOffset.Y;
            }
        }

        #endregion
    }

    public class AbnormityLabel : Label
    {
        public new Image BackgroundImage
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

        #region 支持窗体拖动的代码

        private bool isMouseDown = false;
        private Point mouseOffset;
        private long timeOffset = DateTime.Now.ToBinary();
        private bool _AllowDrag = false;
        private Form ParentForm = null;

        [DefaultValue(true)]
        public bool AllowDrag
        {
            get
            {
                return _AllowDrag;
            }
            set
            {
                if (value)
                {
                    if (!_AllowDrag)
                    {
                        this.MouseMove += new MouseEventHandler(_MouseMove);
                        this.MouseUp += new MouseEventHandler(_MouseUp);
                        this.MouseDown += new MouseEventHandler(_MouseDown);
                    }
                }
                else
                {
                    if (_AllowDrag)
                    {
                        this.MouseMove -= new MouseEventHandler(_MouseMove);
                        this.MouseUp -= new MouseEventHandler(_MouseUp);
                        this.MouseDown -= new MouseEventHandler(_MouseDown);
                    }
                }
                _AllowDrag = value;
            }
        }

        private void _MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOffset = e.Location;
                isMouseDown = true;
                Control t = this;
                do
                {
                    t = t.Parent;
                } while (!(t is Form));
                ParentForm = (Form)t;
                //              //  ParentForm.Opacity = 0.85;
            }
        }

        private void _MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
                // ParentForm.Opacity = 1;
            }
        }

        private void _MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown && ((DateTime.Now.ToBinary() - timeOffset) / 300000 > 0))
            {
                timeOffset = DateTime.Now.ToBinary();
                ParentForm.Left += e.X - mouseOffset.X;
                ParentForm.Top += e.Y - mouseOffset.Y;
            }
        }

        #endregion
    }

}