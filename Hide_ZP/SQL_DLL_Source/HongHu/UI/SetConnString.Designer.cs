namespace HongHu.UI
{
    partial class SetConnString
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.SQLServer_ComBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DBUser_textBox = new System.Windows.Forms.TextBox();
            this.DBUPwdtextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Test_button = new System.Windows.Forms.Button();
            this.Clos_button = new System.Windows.Forms.Button();
            this.TianChong_button = new System.Windows.Forms.Button();
            this.Enter_button = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 154);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(292, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(29, 17);
            this.toolStripStatusLabel1.Text = "就绪";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Visible = false;
            // 
            // SQLServer_ComBox
            // 
            this.SQLServer_ComBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SQLServer_ComBox.FormattingEnabled = true;
            this.SQLServer_ComBox.Location = new System.Drawing.Point(104, 7);
            this.SQLServer_ComBox.Name = "SQLServer_ComBox";
            this.SQLServer_ComBox.Size = new System.Drawing.Size(118, 20);
            this.SQLServer_ComBox.TabIndex = 1;
            this.SQLServer_ComBox.TextChanged += new System.EventHandler(this.XBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "SQL服务器:";
            // 
            // DBUser_textBox
            // 
            this.DBUser_textBox.Location = new System.Drawing.Point(104, 45);
            this.DBUser_textBox.Name = "DBUser_textBox";
            this.DBUser_textBox.Size = new System.Drawing.Size(166, 21);
            this.DBUser_textBox.TabIndex = 3;
            this.DBUser_textBox.TextChanged += new System.EventHandler(this.XBox_TextChanged);
            // 
            // DBUPwdtextBox
            // 
            this.DBUPwdtextBox.Location = new System.Drawing.Point(104, 84);
            this.DBUPwdtextBox.Name = "DBUPwdtextBox";
            this.DBUPwdtextBox.Size = new System.Drawing.Size(166, 21);
            this.DBUPwdtextBox.TabIndex = 4;
            this.DBUPwdtextBox.TextChanged += new System.EventHandler(this.XBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "用 户 名:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "密     码:";
            // 
            // Test_button
            // 
            this.Test_button.Location = new System.Drawing.Point(24, 121);
            this.Test_button.Name = "Test_button";
            this.Test_button.Size = new System.Drawing.Size(58, 23);
            this.Test_button.TabIndex = 7;
            this.Test_button.Text = "测 试";
            this.Test_button.UseVisualStyleBackColor = true;
            this.Test_button.Click += new System.EventHandler(this.TestConn);
            // 
            // Clos_button
            // 
            this.Clos_button.Location = new System.Drawing.Point(212, 121);
            this.Clos_button.Name = "Clos_button";
            this.Clos_button.Size = new System.Drawing.Size(58, 23);
            this.Clos_button.TabIndex = 8;
            this.Clos_button.Text = "取 消";
            this.Clos_button.UseVisualStyleBackColor = true;
            this.Clos_button.Click += new System.EventHandler(this.Close);
            // 
            // TianChong_button
            // 
            this.TianChong_button.Location = new System.Drawing.Point(228, 4);
            this.TianChong_button.Name = "TianChong_button";
            this.TianChong_button.Size = new System.Drawing.Size(42, 24);
            this.TianChong_button.TabIndex = 9;
            this.TianChong_button.Text = "填充";
            this.TianChong_button.UseVisualStyleBackColor = true;
            this.TianChong_button.Click += new System.EventHandler(this.EnumeratorSQL);
            // 
            // Enter_button
            // 
            this.Enter_button.Enabled = false;
            this.Enter_button.Location = new System.Drawing.Point(117, 121);
            this.Enter_button.Name = "Enter_button";
            this.Enter_button.Size = new System.Drawing.Size(58, 23);
            this.Enter_button.TabIndex = 10;
            this.Enter_button.Text = "确 定";
            this.Enter_button.UseVisualStyleBackColor = true;
            this.Enter_button.Click += new System.EventHandler(this.Enter_button_Click);
            // 
            // SetConnString
            // 
            this.ClientSize = new System.Drawing.Size(292, 176);
            this.Controls.Add(this.Enter_button);
            this.Controls.Add(this.TianChong_button);
            this.Controls.Add(this.Clos_button);
            this.Controls.Add(this.Test_button);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DBUPwdtextBox);
            this.Controls.Add(this.DBUser_textBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SQLServer_ComBox);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "SetConnString";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置数据库连接";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetConnString_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ComboBox SQLServer_ComBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DBUser_textBox;
        private System.Windows.Forms.TextBox DBUPwdtextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Test_button;
        private System.Windows.Forms.Button Clos_button;
        private System.Windows.Forms.Button TianChong_button;
        private System.Windows.Forms.Button Enter_button;
    }
}