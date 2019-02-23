namespace HongHu.UI
{
    partial class RunU_Form_Demo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunU_Form_Demo));
            this.closButton1 = new HongHu.UI.ClosButton();
            this.minButton1 = new HongHu.UI.MinButton();
            this.XiaoTuBiao = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip_XiaoTuBiao = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sQL连接设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.closButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minButton1)).BeginInit();
            this.contextMenuStrip_XiaoTuBiao.SuspendLayout();
            this.SuspendLayout();
            // 
            // closButton1
            // 
            this.closButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closButton1.BackColor = System.Drawing.Color.Transparent;
            this.closButton1.Location = new System.Drawing.Point(642, 0);
            this.closButton1.Name = "closButton1";
            this.closButton1.Size = new System.Drawing.Size(38, 18);
            this.closButton1.TabIndex = 21;
            this.closButton1.TabStop = false;
            // 
            // minButton1
            // 
            this.minButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minButton1.BackColor = System.Drawing.Color.Transparent;
            this.minButton1.Location = new System.Drawing.Point(615, 0);
            this.minButton1.Name = "minButton1";
            this.minButton1.Size = new System.Drawing.Size(25, 18);
            this.minButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.minButton1.TabIndex = 20;
            this.minButton1.TabStop = false;
            // 
            // XiaoTuBiao
            // 
            this.XiaoTuBiao.ContextMenuStrip = this.contextMenuStrip_XiaoTuBiao;
            this.XiaoTuBiao.Icon = ((System.Drawing.Icon)(resources.GetObject("XiaoTuBiao.Icon")));
            this.XiaoTuBiao.Text = "XiaoTuBiao";
            this.XiaoTuBiao.Visible = true;
            // 
            // contextMenuStrip_XiaoTuBiao
            // 
            this.contextMenuStrip_XiaoTuBiao.BackgroundImage = global::HongHu.Properties.Resources.MoveForm_backimage2;
            this.contextMenuStrip_XiaoTuBiao.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sQL连接设置ToolStripMenuItem,
            this.设置ToolStripMenuItem,
            this.关于ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.contextMenuStrip_XiaoTuBiao.Name = "contextMenuStrip1";
            this.contextMenuStrip_XiaoTuBiao.Size = new System.Drawing.Size(153, 114);
            // 
            // sQL连接设置ToolStripMenuItem
            // 
            this.sQL连接设置ToolStripMenuItem.Name = "sQL连接设置ToolStripMenuItem";
            this.sQL连接设置ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sQL连接设置ToolStripMenuItem.Text = "SQL连接设置";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // RunU_Form_Demo
            // 
            this.AllowDrag = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::HongHu.Properties.Resources.MoveForm_backimage2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(680, 417);
            this.Controls.Add(this.minButton1);
            this.Controls.Add(this.closButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RunU_Form_Demo";
            this.Text = "帐套携带工具";
            ((System.ComponentModel.ISupportInitialize)(this.closButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minButton1)).EndInit();
            this.contextMenuStrip_XiaoTuBiao.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private global::HongHu.UI.ClosButton closButton1;
        //private global::HongHu.UI.MinButton minButton1;
        private ClosButton closButton1;
        private MinButton minButton1;
        protected System.Windows.Forms.NotifyIcon XiaoTuBiao;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_XiaoTuBiao;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sQL连接设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
    }
}