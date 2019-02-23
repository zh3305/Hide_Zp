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

        #region 圆角
        //public void SetWindowRegion()
        //{
        //    System.Drawing.Drawing2D.GraphicsPath FormPath;
        //    FormPath = new System.Drawing.Drawing2D.GraphicsPath();
        //    Rectangle rect = new Rectangle(0, 5, this.Width, this.Height - 5);//this.Left-10,this.Top-10,this.Width-10,this.Height-10);                 
        //    FormPath = GetRoundedRectPath(rect, 3);
        //    this.Region = new Region(FormPath);
        //}
        //private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        //{
        //    int diameter = radius;
        //    Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
        //    GraphicsPath path = new GraphicsPath();
        //    //   左上角   
        //    path.AddArc(arcRect, 180, 90);
        //    //   右上角   
        //    //arcRect.X = rect.Right;
        //    arcRect.X = rect.Right - diameter;
        //    path.AddArc(arcRect, 270, 90);
        //    //   右下角   
        //    //arcRect.Y = rect.Bottom;
        //    arcRect.Y = rect.Bottom - diameter;
        //    path.AddArc(arcRect, 0, 90);
        //    //   左下角   
        //    arcRect.X = rect.Left;
        //    path.AddArc(arcRect, 90, 90);
        //    path.CloseFigure();
        //    return path;
        //}

        //protected override void OnResize(System.EventArgs e)
        //{
        //    this.Region = null;
        //    //SetWindowRegion();
        //    Type(this, 30, 0.1);  
        //}
        //private void Type(Control sender, int p_1, double p_2)
        //{
        //    GraphicsPath oPath = new GraphicsPath();
        //    oPath.AddClosedCurve(
        //        new Point[] {
        //    new Point(0, sender.Height / p_1),
        //    new Point(sender.Width / p_1, 0), 
        //    new Point(sender.Width - sender.Width / p_1, 0), 
        //    new Point(sender.Width, sender.Height / p_1),
        //    new Point(sender.Width, sender.Height - sender.Height / p_1), 
        //    new Point(sender.Width - sender.Width / p_1, sender.Height), 
        //    new Point(sender.Width / p_1, sender.Height),
        //    new Point(0, sender.Height - sender.Height / p_1) },

        //        (float)p_2);

        //    sender.Region = new Region(oPath);
        //}
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


        #endregion 圆角

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
 
    /// <summary>
    /// 支持拖动的LABEL
    /// </summary>
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