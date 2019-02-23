using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.AbnormityFrame;

namespace HongHu.UI
{
    public partial class RunU_Form_Demo : AbnormityForm
    {
        public RunU_Form_Demo()
        {
            InitializeComponent();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new HongHu.UI.AboutBox1().ShowDialog();
        }
    }
}
