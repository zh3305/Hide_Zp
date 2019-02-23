namespace HongHu.UI
{
    using HongHu;
    using System;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    public class HotKeyTextBox : TextBox
    {
        public int HAlt = 0;
        public int HCtrl = 0;
        public Keys Hkey = Keys.Escape;
        public int HShift = 0;
        private StringBuilder tbt;

        public HotKeyTextBox()
        {
            this.BackColor = Color.White;
            base.ReadOnly = true;
            this.Text = "无";
            base.KeyDown += new KeyEventHandler(this.HotKeyTextBox_KeyDown);
            base.KeyUp += new KeyEventHandler(this.HotKeyTextBox_KeyUp);
            base.Leave += new EventHandler(this.HotKeyTextBox_Leave);
        }

        private void HotKeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            this.tbt = new StringBuilder();
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Escape))
            {
                this.tbt.Append("无");
            }
            else
            {
                if (e.Control)
                {
                    this.tbt.Append("Ctrl + ");
                    this.HCtrl = 2;
                }
                else
                {
                    this.HCtrl = 0;
                }
                if (e.Shift)
                {
                    this.tbt.Append("Shift + ");
                    this.HShift = 4;
                }
                else
                {
                    this.HShift = 0;
                }
                if (e.Alt)
                {
                    this.tbt.Append("Alt + ");
                    this.HAlt = 1;
                }
                else
                {
                    this.HAlt = 0;
                }
                if ((e.KeyValue > 30) && (e.KeyValue != 0x5b))
                {
                    if (this.KeyModifiers == 0)
                    {
                        this.tbt.Append("Ctrl + Alt + ");
                        this.HAlt = 1;
                        this.HCtrl = 2;
                    }
                    this.Hkey = e.KeyCode;
                    this.tbt.Append(e.KeyCode.ToString());
                }
                else
                {
                    this.Hkey = Keys.Escape;
                }
            }
            this.Text = this.tbt.ToString();
            this.tbt = new StringBuilder();
        }

        private void HotKeyTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.Hkey == Keys.Escape)
            {
                this.tbt = new StringBuilder();
                if ((e.KeyCode != Keys.Back) && (e.KeyCode != Keys.Escape))
                {
                    this.HotKeyTextBox_KeyDown(sender, e);
                }
            }
        }

        private void HotKeyTextBox_Leave(object sender, EventArgs e)
        {
            if (this.Hkey == Keys.Escape)
            {
                this.Text = "无";
            }
        }

        public void ShowHotKey()
        {
            this.tbt = new StringBuilder();
            if (this.Hkey == Keys.Escape)
            {
                this.tbt.Append("无");
            }
            else
            {
                if (this.HCtrl == 2)
                {
                    this.tbt.Append("Ctrl + ");
                }
                if (this.HShift == 4)
                {
                    this.tbt.Append("Shift + ");
                }
                if (this.HAlt == 1)
                {
                    this.tbt.Append("Alt + ");
                }
                this.tbt.Append(this.Hkey.ToString());
            }
            this.Text = this.tbt.ToString();
            this.tbt = new StringBuilder();
        }

        public int KeyModifiers
        {
            get
            {
                return ((this.HCtrl + this.HAlt) + this.HShift);
            }
            set
            {
                switch (value)
                {
                    case 0:
                        this.HAlt = 0;
                        this.HShift = 0;
                        this.HCtrl = 0;
                        break;

                    case 1:
                        this.HAlt = 1;
                        this.HShift = 0;
                        this.HCtrl = 0;
                        break;

                    case 2:
                        this.HCtrl = 2;
                        this.HAlt = 0;
                        this.HShift = 0;
                        break;

                    case 3:
                        this.HCtrl = 2;
                        this.HAlt = 1;
                        this.HShift = 0;
                        break;

                    case 4:
                        this.HShift = 4;
                        this.HAlt = 0;
                        this.HCtrl = 0;
                        break;

                    case 5:
                        this.HShift = 4;
                        this.HAlt = 1;
                        this.HCtrl = 0;
                        break;

                    case 6:
                        this.HShift = 4;
                        this.HCtrl = 2;
                        this.HAlt = 0;
                        break;

                    case 7:
                        this.HShift = 4;
                        this.HCtrl = 2;
                        this.HAlt = 1;
                        break;

                    default:
                        SysDataLog.log.Error("给KeyModifiers 赋了一个不正确的值");
                        throw new Exception("给KeyModifiers 赋了一个不正确的值");
                }
            }
        }
    }
}

