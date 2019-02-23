using System.Windows.Forms.AbnormityFrame;
using System.Windows.Forms;
using Glass;
using HongHu.UI;
using HongHu.DLL.Config;
using System.Drawing;
using System;
namespace HongHu
{
    partial class FormMoveRun : AbnormityForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMoveRun));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.Txtb = new System.Windows.Forms.NotifyIcon(this.components);
            this.IcoMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示主界面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置SQLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ONE__Panel = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Three_panel = new System.Windows.Forms.Panel();
            this.P3_sx_bt = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cAcc_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cAcc_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cAcc_Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HideId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cAcc_live = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CAcc_hide = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Options_Panel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.P2_SQLSET_button = new System.Windows.Forms.Button();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.Po_hotkeys_gb = new System.Windows.Forms.GroupBox();
            this.Po_xs_tb = new HongHu.UI.HotKeyTextBox();
            this.Po_Yc_tb = new HongHu.UI.HotKeyTextBox();
            this.Po_Zjm_tb = new HongHu.UI.HotKeyTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Po_Enter_bt = new System.Windows.Forms.Button();
            this.Po_App_bt = new System.Windows.Forms.Button();
            this.Po_Cancel_bt = new System.Windows.Forms.Button();
            this.Po_Xtb_cb = new System.Windows.Forms.CheckBox();
            this.Po_jd_cb = new System.Windows.Forms.CheckBox();
            this.Po_zrun_cb = new System.Windows.Forms.CheckBox();
            this.Po_Ycms_gb = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Bows_Path = new System.Windows.Forms.Button();
            this.Po_Movetarget_tb = new System.Windows.Forms.TextBox();
            this.Po_r_Move = new System.Windows.Forms.RadioButton();
            this.Po_r_Detach = new System.Windows.Forms.RadioButton();
            this.Po_r_System = new System.Windows.Forms.RadioButton();
            this.Po_r_Login = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.minButton1 = new HongHu.UI.MinButton();
            this.closButton1 = new HongHu.UI.ClosButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tp_welcome = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.IcoMenuStrip.SuspendLayout();
            this.ONE__Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.Three_panel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.Options_Panel.SuspendLayout();
            this.Po_hotkeys_gb.SuspendLayout();
            this.Po_Ycms_gb.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closButton1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tp_welcome.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Tan;
            this.label1.Location = new System.Drawing.Point(7, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "My Toos";
            // 
            // Txtb
            // 
            this.Txtb.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.Txtb.BalloonTipText = "HongHu";
            this.Txtb.BalloonTipTitle = "HongHu";
            this.Txtb.ContextMenuStrip = this.IcoMenuStrip;
            this.Txtb.Icon = ((System.Drawing.Icon)(resources.GetObject("Txtb.Icon")));
            this.Txtb.Text = "HongHu";
            this.Txtb.Visible = true;
            this.Txtb.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Txtb_MouseDoubleClick);
            // 
            // IcoMenuStrip
            // 
            this.IcoMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示主界面ToolStripMenuItem,
            this.关于ToolStripMenuItem,
            this.设置SQLToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.IcoMenuStrip.Name = "IcoMenuStrip";
            this.IcoMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.IcoMenuStrip.Size = new System.Drawing.Size(137, 92);
            // 
            // 显示主界面ToolStripMenuItem
            // 
            this.显示主界面ToolStripMenuItem.Name = "显示主界面ToolStripMenuItem";
            this.显示主界面ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.显示主界面ToolStripMenuItem.Text = "显示主界面";
            this.显示主界面ToolStripMenuItem.Click += new System.EventHandler(this.显示主界面ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // 设置SQLToolStripMenuItem
            // 
            this.设置SQLToolStripMenuItem.Name = "设置SQLToolStripMenuItem";
            this.设置SQLToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.设置SQLToolStripMenuItem.Text = "设置SQL";
            this.设置SQLToolStripMenuItem.Click += new System.EventHandler(this.设置SQLToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ONE__Panel
            // 
            this.ONE__Panel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ONE__Panel.Controls.Add(this.pictureBox2);
            this.ONE__Panel.Controls.Add(this.pictureBox1);
            this.ONE__Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ONE__Panel.Location = new System.Drawing.Point(0, 0);
            this.ONE__Panel.Name = "ONE__Panel";
            this.ONE__Panel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ONE__Panel.Size = new System.Drawing.Size(552, 297);
            this.ONE__Panel.TabIndex = 1000;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::HongHu.Properties.Resources.欢迎使用账套保护工具;
            this.pictureBox2.Location = new System.Drawing.Point(6, 67);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(469, 50);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::HongHu.Properties.Resources.weelcome;
            this.pictureBox1.Location = new System.Drawing.Point(6, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(378, 70);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Three_panel
            // 
            this.Three_panel.BackColor = System.Drawing.Color.Transparent;
            this.Three_panel.Controls.Add(this.P3_sx_bt);
            this.Three_panel.Controls.Add(this.groupBox1);
            this.Three_panel.Location = new System.Drawing.Point(3, 0);
            this.Three_panel.Name = "Three_panel";
            this.Three_panel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Three_panel.Size = new System.Drawing.Size(545, 302);
            this.Three_panel.TabIndex = 1001;
            // 
            // P3_sx_bt
            // 
            this.P3_sx_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.P3_sx_bt.Location = new System.Drawing.Point(3, 269);
            this.P3_sx_bt.Name = "P3_sx_bt";
            this.P3_sx_bt.Size = new System.Drawing.Size(71, 23);
            this.P3_sx_bt.TabIndex = 5;
            this.P3_sx_bt.Text = "刷新";
            this.P3_sx_bt.UseVisualStyleBackColor = true;
            this.P3_sx_bt.Click += new System.EventHandler(this.P3_sx_bt_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Lime;
            this.groupBox1.Location = new System.Drawing.Point(5, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(538, 264);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择帐套";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PaleGreen;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Green;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Thistle;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cAcc_Id,
            this.cAcc_Name,
            this.cAcc_Year,
            this.HideId,
            this.cAcc_live,
            this.CAcc_hide});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Lime;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.OrangeRed;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(532, 244);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // cAcc_Id
            // 
            this.cAcc_Id.DataPropertyName = "cAcc_Id";
            this.cAcc_Id.FillWeight = 65F;
            this.cAcc_Id.HeaderText = "帐套号";
            this.cAcc_Id.MinimumWidth = 2;
            this.cAcc_Id.Name = "cAcc_Id";
            this.cAcc_Id.ReadOnly = true;
            this.cAcc_Id.Width = 65;
            // 
            // cAcc_Name
            // 
            this.cAcc_Name.DataPropertyName = "cAcc_Name";
            this.cAcc_Name.FillWeight = 180F;
            this.cAcc_Name.HeaderText = "帐套";
            this.cAcc_Name.Name = "cAcc_Name";
            this.cAcc_Name.ReadOnly = true;
            this.cAcc_Name.Width = 180;
            // 
            // cAcc_Year
            // 
            this.cAcc_Year.HeaderText = "年度";
            this.cAcc_Year.Name = "cAcc_Year";
            this.cAcc_Year.ReadOnly = true;
            this.cAcc_Year.Visible = false;
            // 
            // HideId
            // 
            this.HideId.DataPropertyName = "HideId";
            this.HideId.HeaderText = "HideId";
            this.HideId.Name = "HideId";
            this.HideId.ReadOnly = true;
            this.HideId.Visible = false;
            // 
            // cAcc_live
            // 
            this.cAcc_live.DataPropertyName = "CAcc_live_p";
            this.cAcc_live.HeaderText = "隐藏方式";
            this.cAcc_live.Name = "cAcc_live";
            this.cAcc_live.ReadOnly = true;
            // 
            // CAcc_hide
            // 
            this.CAcc_hide.DataPropertyName = "CAcc_hide_p";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Red;
            this.CAcc_hide.DefaultCellStyle = dataGridViewCellStyle2;
            this.CAcc_hide.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CAcc_hide.HeaderText = "";
            this.CAcc_hide.Name = "CAcc_hide";
            this.CAcc_hide.ReadOnly = true;
            this.CAcc_hide.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CAcc_hide.Text = "显示";
            // 
            // Options_Panel
            // 
            this.Options_Panel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Options_Panel.Controls.Add(this.button1);
            this.Options_Panel.Controls.Add(this.P2_SQLSET_button);
            this.Options_Panel.Controls.Add(this.checkBox3);
            this.Options_Panel.Controls.Add(this.Po_hotkeys_gb);
            this.Options_Panel.Controls.Add(this.Po_Enter_bt);
            this.Options_Panel.Controls.Add(this.Po_App_bt);
            this.Options_Panel.Controls.Add(this.Po_Cancel_bt);
            this.Options_Panel.Controls.Add(this.Po_Xtb_cb);
            this.Options_Panel.Controls.Add(this.Po_jd_cb);
            this.Options_Panel.Controls.Add(this.Po_zrun_cb);
            this.Options_Panel.Controls.Add(this.Po_Ycms_gb);
            this.Options_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Options_Panel.Location = new System.Drawing.Point(3, 3);
            this.Options_Panel.Name = "Options_Panel";
            this.Options_Panel.Size = new System.Drawing.Size(546, 291);
            this.Options_Panel.TabIndex = 1000;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Enabled = false;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(189, 120);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(151, 20);
            this.button1.TabIndex = 13;
            this.button1.Text = "清除用友用户操作日志";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // P2_SQLSET_button
            // 
            this.P2_SQLSET_button.BackColor = System.Drawing.Color.White;
            this.P2_SQLSET_button.FlatAppearance.BorderSize = 0;
            this.P2_SQLSET_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.P2_SQLSET_button.ForeColor = System.Drawing.Color.Black;
            this.P2_SQLSET_button.Location = new System.Drawing.Point(14, 120);
            this.P2_SQLSET_button.Margin = new System.Windows.Forms.Padding(0);
            this.P2_SQLSET_button.Name = "P2_SQLSET_button";
            this.P2_SQLSET_button.Size = new System.Drawing.Size(151, 20);
            this.P2_SQLSET_button.TabIndex = 13;
            this.P2_SQLSET_button.Text = "设置SQL链接";
            this.P2_SQLSET_button.UseVisualStyleBackColor = false;
            this.P2_SQLSET_button.Click += new System.EventHandler(this.SetSqlConn);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Enabled = false;
            this.checkBox3.ForeColor = System.Drawing.Color.Black;
            this.checkBox3.Location = new System.Drawing.Point(280, 86);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(60, 16);
            this.checkBox3.TabIndex = 12;
            this.checkBox3.Text = "安全型";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // Po_hotkeys_gb
            // 
            this.Po_hotkeys_gb.Controls.Add(this.Po_xs_tb);
            this.Po_hotkeys_gb.Controls.Add(this.Po_Yc_tb);
            this.Po_hotkeys_gb.Controls.Add(this.Po_Zjm_tb);
            this.Po_hotkeys_gb.Controls.Add(this.label4);
            this.Po_hotkeys_gb.Controls.Add(this.label3);
            this.Po_hotkeys_gb.Controls.Add(this.label2);
            this.Po_hotkeys_gb.ForeColor = System.Drawing.Color.Black;
            this.Po_hotkeys_gb.Location = new System.Drawing.Point(14, 11);
            this.Po_hotkeys_gb.Name = "Po_hotkeys_gb";
            this.Po_hotkeys_gb.Size = new System.Drawing.Size(260, 101);
            this.Po_hotkeys_gb.TabIndex = 11;
            this.Po_hotkeys_gb.TabStop = false;
            this.Po_hotkeys_gb.Text = "热键设置";
            this.toolTip1.SetToolTip(this.Po_hotkeys_gb, "对软件相关热键的设置");
            // 
            // Po_xs_tb
            // 
            this.Po_xs_tb.BackColor = System.Drawing.Color.White;
            this.Po_xs_tb.Enabled = false;
            this.Po_xs_tb.KeyModifiers = 0;
            this.Po_xs_tb.Location = new System.Drawing.Point(73, 72);
            this.Po_xs_tb.Name = "Po_xs_tb";
            this.Po_xs_tb.ReadOnly = true;
            this.Po_xs_tb.Size = new System.Drawing.Size(151, 21);
            this.Po_xs_tb.TabIndex = 1;
            this.Po_xs_tb.Text = "无";
            this.toolTip1.SetToolTip(this.Po_xs_tb, "设置 显示帐套的热键");
            // 
            // Po_Yc_tb
            // 
            this.Po_Yc_tb.BackColor = System.Drawing.Color.White;
            this.Po_Yc_tb.Enabled = false;
            this.Po_Yc_tb.KeyModifiers = 0;
            this.Po_Yc_tb.Location = new System.Drawing.Point(73, 45);
            this.Po_Yc_tb.Name = "Po_Yc_tb";
            this.Po_Yc_tb.ReadOnly = true;
            this.Po_Yc_tb.Size = new System.Drawing.Size(151, 21);
            this.Po_Yc_tb.TabIndex = 1;
            this.Po_Yc_tb.Text = "无";
            this.toolTip1.SetToolTip(this.Po_Yc_tb, "设置 隐藏帐套的热键");
            // 
            // Po_Zjm_tb
            // 
            this.Po_Zjm_tb.BackColor = System.Drawing.Color.White;
            this.Po_Zjm_tb.KeyModifiers = 0;
            this.Po_Zjm_tb.Location = new System.Drawing.Point(73, 19);
            this.Po_Zjm_tb.Name = "Po_Zjm_tb";
            this.Po_Zjm_tb.ReadOnly = true;
            this.Po_Zjm_tb.Size = new System.Drawing.Size(151, 21);
            this.Po_Zjm_tb.TabIndex = 1;
            this.Po_Zjm_tb.Text = "无";
            this.toolTip1.SetToolTip(this.Po_Zjm_tb, "设置 显示隐藏 主界面的热键");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(11, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "显示帐套";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(11, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "隐藏帐套";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "主界面";
            // 
            // Po_Enter_bt
            // 
            this.Po_Enter_bt.BackColor = System.Drawing.Color.Transparent;
            this.Po_Enter_bt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Po_Enter_bt.Location = new System.Drawing.Point(379, 250);
            this.Po_Enter_bt.Name = "Po_Enter_bt";
            this.Po_Enter_bt.Size = new System.Drawing.Size(75, 23);
            this.Po_Enter_bt.TabIndex = 10;
            this.Po_Enter_bt.Text = "确定";
            this.Po_Enter_bt.UseVisualStyleBackColor = false;
            this.Po_Enter_bt.Click += new System.EventHandler(this.Po_Enter_bt_Click);
            // 
            // Po_App_bt
            // 
            this.Po_App_bt.BackColor = System.Drawing.Color.Transparent;
            this.Po_App_bt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Po_App_bt.Location = new System.Drawing.Point(460, 250);
            this.Po_App_bt.Name = "Po_App_bt";
            this.Po_App_bt.Size = new System.Drawing.Size(75, 23);
            this.Po_App_bt.TabIndex = 10;
            this.Po_App_bt.Text = "应用";
            this.Po_App_bt.UseVisualStyleBackColor = false;
            this.Po_App_bt.Click += new System.EventHandler(this.Po_App_bt_Click);
            // 
            // Po_Cancel_bt
            // 
            this.Po_Cancel_bt.BackColor = System.Drawing.Color.Transparent;
            this.Po_Cancel_bt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Po_Cancel_bt.Location = new System.Drawing.Point(298, 250);
            this.Po_Cancel_bt.Name = "Po_Cancel_bt";
            this.Po_Cancel_bt.Size = new System.Drawing.Size(75, 23);
            this.Po_Cancel_bt.TabIndex = 10;
            this.Po_Cancel_bt.Text = "取消";
            this.Po_Cancel_bt.UseVisualStyleBackColor = false;
            this.Po_Cancel_bt.Click += new System.EventHandler(this.Po_Cancel_bt_Click);
            // 
            // Po_Xtb_cb
            // 
            this.Po_Xtb_cb.AutoSize = true;
            this.Po_Xtb_cb.ForeColor = System.Drawing.Color.Black;
            this.Po_Xtb_cb.Location = new System.Drawing.Point(280, 19);
            this.Po_Xtb_cb.Name = "Po_Xtb_cb";
            this.Po_Xtb_cb.Size = new System.Drawing.Size(96, 16);
            this.Po_Xtb_cb.TabIndex = 9;
            this.Po_Xtb_cb.Text = "显示托盘图标";
            this.Po_Xtb_cb.UseVisualStyleBackColor = true;
            // 
            // Po_jd_cb
            // 
            this.Po_jd_cb.AutoSize = true;
            this.Po_jd_cb.ForeColor = System.Drawing.Color.Black;
            this.Po_jd_cb.Location = new System.Drawing.Point(280, 63);
            this.Po_jd_cb.Name = "Po_jd_cb";
            this.Po_jd_cb.Size = new System.Drawing.Size(72, 16);
            this.Po_jd_cb.TabIndex = 8;
            this.Po_jd_cb.Text = "显示进度";
            this.Po_jd_cb.UseVisualStyleBackColor = true;
            // 
            // Po_zrun_cb
            // 
            this.Po_zrun_cb.AutoSize = true;
            this.Po_zrun_cb.ForeColor = System.Drawing.Color.Black;
            this.Po_zrun_cb.Location = new System.Drawing.Point(280, 41);
            this.Po_zrun_cb.Name = "Po_zrun_cb";
            this.Po_zrun_cb.Size = new System.Drawing.Size(84, 16);
            this.Po_zrun_cb.TabIndex = 7;
            this.Po_zrun_cb.Text = "开机自运行";
            this.Po_zrun_cb.UseVisualStyleBackColor = true;
            // 
            // Po_Ycms_gb
            // 
            this.Po_Ycms_gb.Controls.Add(this.groupBox3);
            this.Po_Ycms_gb.Controls.Add(this.Po_r_Move);
            this.Po_Ycms_gb.Controls.Add(this.Po_r_Detach);
            this.Po_Ycms_gb.Controls.Add(this.Po_r_System);
            this.Po_Ycms_gb.Controls.Add(this.Po_r_Login);
            this.Po_Ycms_gb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Po_Ycms_gb.Location = new System.Drawing.Point(14, 152);
            this.Po_Ycms_gb.Name = "Po_Ycms_gb";
            this.Po_Ycms_gb.Size = new System.Drawing.Size(521, 92);
            this.Po_Ycms_gb.TabIndex = 6;
            this.Po_Ycms_gb.TabStop = false;
            this.Po_Ycms_gb.Text = "隐藏模式";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Bows_Path);
            this.groupBox3.Controls.Add(this.Po_Movetarget_tb);
            this.groupBox3.Location = new System.Drawing.Point(107, 41);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(401, 43);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "目标目录  为空,则移至软件运行目录";
            // 
            // Bows_Path
            // 
            this.Bows_Path.BackColor = System.Drawing.Color.Transparent;
            this.Bows_Path.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Bows_Path.Location = new System.Drawing.Point(343, 15);
            this.Bows_Path.Name = "Bows_Path";
            this.Bows_Path.Size = new System.Drawing.Size(49, 19);
            this.Bows_Path.TabIndex = 11;
            this.Bows_Path.Text = "选择";
            this.Bows_Path.UseVisualStyleBackColor = false;
            this.Bows_Path.Click += new System.EventHandler(this.Bows_Paths_Click);
            // 
            // Po_Movetarget_tb
            // 
            this.Po_Movetarget_tb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Po_Movetarget_tb.Location = new System.Drawing.Point(4, 15);
            this.Po_Movetarget_tb.Name = "Po_Movetarget_tb";
            this.Po_Movetarget_tb.Size = new System.Drawing.Size(332, 21);
            this.Po_Movetarget_tb.TabIndex = 0;
            // 
            // Po_r_Move
            // 
            this.Po_r_Move.AutoSize = true;
            this.Po_r_Move.Location = new System.Drawing.Point(11, 57);
            this.Po_r_Move.Name = "Po_r_Move";
            this.Po_r_Move.Size = new System.Drawing.Size(83, 16);
            this.Po_r_Move.TabIndex = 2;
            this.Po_r_Move.Text = "以上加移动";
            this.Po_r_Move.UseVisualStyleBackColor = true;
            this.Po_r_Move.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // Po_r_Detach
            // 
            this.Po_r_Detach.AutoSize = true;
            this.Po_r_Detach.Location = new System.Drawing.Point(227, 19);
            this.Po_r_Detach.Name = "Po_r_Detach";
            this.Po_r_Detach.Size = new System.Drawing.Size(287, 16);
            this.Po_r_Detach.TabIndex = 2;
            this.Po_r_Detach.Text = "登录+系统隐藏+数据库分离 !容易被新建帐套覆盖";
            this.Po_r_Detach.UseVisualStyleBackColor = true;
            // 
            // Po_r_System
            // 
            this.Po_r_System.AutoSize = true;
            this.Po_r_System.Location = new System.Drawing.Point(104, 19);
            this.Po_r_System.Name = "Po_r_System";
            this.Po_r_System.Size = new System.Drawing.Size(101, 16);
            this.Po_r_System.TabIndex = 1;
            this.Po_r_System.Text = "登录+系统隐藏";
            this.Po_r_System.UseVisualStyleBackColor = true;
            // 
            // Po_r_Login
            // 
            this.Po_r_Login.AutoSize = true;
            this.Po_r_Login.Location = new System.Drawing.Point(11, 19);
            this.Po_r_Login.Name = "Po_r_Login";
            this.Po_r_Login.Size = new System.Drawing.Size(71, 16);
            this.Po_r_Login.TabIndex = 0;
            this.Po_r_Login.Text = "登录隐藏";
            this.Po_r_Login.UseVisualStyleBackColor = true;
            // 
            // minButton1
            // 
            this.minButton1.BackColor = System.Drawing.Color.Transparent;
            this.minButton1.Location = new System.Drawing.Point(499, -2);
            this.minButton1.Name = "minButton1";
            this.minButton1.Size = new System.Drawing.Size(25, 18);
            this.minButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.minButton1.TabIndex = 20;
            this.minButton1.TabStop = false;
            this.toolTip1.SetToolTip(this.minButton1, "最小化到托盘");
            // 
            // closButton1
            // 
            this.closButton1.BackColor = System.Drawing.Color.Transparent;
            this.closButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.closButton1.Location = new System.Drawing.Point(522, -2);
            this.closButton1.Name = "closButton1";
            this.closButton1.Size = new System.Drawing.Size(38, 18);
            this.closButton1.TabIndex = 21;
            this.closButton1.TabStop = false;
            this.toolTip1.SetToolTip(this.closButton1, "关闭");
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tp_welcome);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.ImeMode = System.Windows.Forms.ImeMode.Close;
            this.tabControl1.ItemSize = new System.Drawing.Size(22, 22);
            this.tabControl1.Location = new System.Drawing.Point(0, 33);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tabControl1.RightToLeftLayout = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(560, 327);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tabPage4.Controls.Add(this.Three_panel);
            this.tabPage4.ImageIndex = 0;
            this.tabPage4.Location = new System.Drawing.Point(4, 26);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(552, 297);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "帐套";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tabPage3.Controls.Add(this.Options_Panel);
            this.tabPage3.ImageIndex = 1;
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabPage3.Size = new System.Drawing.Size(552, 297);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "设置";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tp_welcome
            // 
            this.tp_welcome.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tp_welcome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tp_welcome.Controls.Add(this.ONE__Panel);
            this.tp_welcome.ImageIndex = 0;
            this.tp_welcome.Location = new System.Drawing.Point(4, 26);
            this.tp_welcome.Margin = new System.Windows.Forms.Padding(0);
            this.tp_welcome.Name = "tp_welcome";
            this.tp_welcome.Size = new System.Drawing.Size(552, 297);
            this.tp_welcome.TabIndex = 0;
            this.tp_welcome.Text = "欢迎";
            this.tp_welcome.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "01.ico");
            this.imageList1.Images.SetKeyName(1, "1 (1).ico");
            this.imageList1.Images.SetKeyName(2, "1 (2).ico");
            this.imageList1.Images.SetKeyName(3, "1 (3).ico");
            // 
            // FormMoveRun
            // 
            this.AllowDrag = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(560, 358);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.minButton1);
            this.Controls.Add(this.closButton1);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMoveRun";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "My toos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.FormMoveRun_SizeChanged);
            this.IcoMenuStrip.ResumeLayout(false);
            this.ONE__Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.Three_panel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.Options_Panel.ResumeLayout(false);
            this.Options_Panel.PerformLayout();
            this.Po_hotkeys_gb.ResumeLayout(false);
            this.Po_hotkeys_gb.PerformLayout();
            this.Po_Ycms_gb.ResumeLayout(false);
            this.Po_Ycms_gb.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closButton1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tp_welcome.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private Button Bows_Path;
        private DataGridViewButtonColumn CAcc_hide;
        private DataGridViewTextBoxColumn cAcc_Id;
        private DataGridViewTextBoxColumn cAcc_live;
        private DataGridViewTextBoxColumn cAcc_Name;
        private DataGridViewTextBoxColumn cAcc_Year;
        private CheckBox checkBox3;
        private ClosButton closButton1;
        //private DataGridViewTextBoxColumn D_zth;
        //private DataGridViewTextBoxColumn D_ztm;
        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private HideConfigItem hide_cfi;
        private DataGridViewTextBoxColumn HideId;
        private ContextMenuStrip IcoMenuStrip;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private MinButton minButton1;
        private Panel ONE__Panel;
        private Panel Options_Panel;
        private Button P3_sx_bt;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Button Po_App_bt;
        private Button Po_Cancel_bt;
        private Button Po_Enter_bt;
        private GroupBox Po_hotkeys_gb;
        private CheckBox Po_jd_cb;
        private TextBox Po_Movetarget_tb;
        private RadioButton Po_r_Detach;
        private RadioButton Po_r_Login;
        private RadioButton Po_r_Move;
        private RadioButton Po_r_System;
        private HotKeyTextBox Po_xs_tb;
        private CheckBox Po_Xtb_cb;
        private HotKeyTextBox Po_Yc_tb;
        private GroupBox Po_Ycms_gb;
        private HotKeyTextBox Po_Zjm_tb;
        private CheckBox Po_zrun_cb;
        private Panel Three_panel;
        private Timer timer1;
        private ToolTip toolTip1;
        private NotifyIcon Txtb;
        private ConfigItemXML Uconfig;

        //DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        //DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        //DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
        //DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        private ToolStripMenuItem 设置SQLToolStripMenuItem;
        private ToolStripMenuItem 退出ToolStripMenuItem;
        // private ToolStripMenuItem 显示账套ToolStripMenuItem;
        private ToolStripMenuItem 关于ToolStripMenuItem;
        private ToolStripMenuItem 显示主界面ToolStripMenuItem;
        private TabControl tabControl1;
        private TabPage tp_welcome;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private Button P2_SQLSET_button;
        public ImageList imageList1;
        private Button button1;
        // private ToolStripMenuItem 隐藏帐套ToolStripMenuItem;
        #endregion
    }
}