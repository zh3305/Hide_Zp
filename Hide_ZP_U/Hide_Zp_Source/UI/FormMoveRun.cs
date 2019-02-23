using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HongHu.DLL.Config;

namespace HongHu.UI
{
    public partial class FormMoveRun : RunU_Form_Demo
    {

        #region 变量

        private ConfigItemXML Uconfig;
        /// <summary>
        /// 程序配置
        /// </summary>
        private HideConfigItem hide_cfi;
        /// <summary>
        /// 多线程操作
        /// </summary>
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        /// <summary>
        /// 多线程_Loading进度窗口
        /// </summary>
        public static System.ComponentModel.BackgroundWorker backgroundWorkerfrom_loading;
        /// <summary>
        /// 进度窗口
        /// </summary>
        UI.Loading form_loading;

        #endregion 变量

        #region 构造函数

        public FormMoveRun()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            //this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            //backgroundWorkerfrom_loading = new System.ComponentModel.BackgroundWorker();
            //InitializeBackgoundWorker();
        }


        #endregion 构造函数

        #region 初始化函数

        /// <summary>
        /// 初始化多线程组件
        /// </summary>
        private void InitializeBackgoundWorker()
        {
            //backgroundWorkerfrom_loading.WorkerReportsProgress = true;
            //backgroundWorkerfrom_loading.WorkerSupportsCancellation = true;
            //backgroundWorker1.WorkerReportsProgress = false;
            ////执行
            //backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            //backgroundWorkerfrom_loading.DoWork += new DoWorkEventHandler(backgroundWorkerfrom_loading_DoWork);
            ////完成
            //backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            //backgroundWorkerfrom_loading.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorkerfrom_loading_RunWorkerCompleted);
            ////报告
            //backgroundWorkerfrom_loading.ProgressChanged += new ProgressChangedEventHandler(backgroundWorkerfrom_loading_ProgressChanged);
        }

        #endregion  初始化函数

        #region 程序加载函数

        /// <summary>
        /// 程序加载函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.LoadUserConfif();

            //if (((SetString.SQLConn != "") && (SetString.SQLConn.Length > 2)))
            //{
            //SetSqlConn(sender, e);
            //MessageBox.Show("请先设置sql 连接!!");
            //ONE__Panel.Visible = false;
            //Two_Panel.Visible = false;
            //GoToPanel3(P2_next_bt, e);
            //}
        }

        #endregion  程序加载函数


        #region 退出程序

        /// <summary>
        /// 退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SysDataLog.log.Info("程序退出");
            SetString.SetConJson(this.hide_cfi);
            this.Uconfig.SaveConfig();
        }


        #endregion 退出程序

        #region 用户设置

        #region 加载用户配置文件
        /// <summary>
        /// 加载用户配置文件
        /// </summary>
        public void LoadUserConfif()
        {
            this.Uconfig = new ConfigItemXML();
            this.hide_cfi = (SetString.GetConfigJson<HideConfigItem>() != null) ? SetString.GetConfigJson<HideConfigItem>() : new HideConfigItem();
            //this.Txtb.Visible = this.hide_cfi.Xtb;
            //this.Po_Xtb_cb.Checked = this.hide_cfi.Xtb;
            //this.Po_zrun_cb.Checked = this.hide_cfi.ZRun;
            //this.Po_jd_cb.Checked = this.hide_cfi.Jdt;
            //foreach (Control ctrl in this.Po_Ycms_gb.Controls)
            //{
            //    if ((ctrl is RadioButton) && (this.hide_cfi.Rf == ((Runfs)Enum.Parse(typeof(Runfs), ((RadioButton)ctrl).Name.Replace("Po_r_", "")))))
            //    {
            //        ((RadioButton)ctrl).Checked = true;
            //        this.Po_Movetarget_tb.Text = this.hide_cfi.Movetarget;
            //    }
            //}
            //if (this.hide_cfi.UHotkeys != null)
            //{
            //    foreach (kuaijijian kjj in this.hide_cfi.UHotkeys)
            //    {
            //        foreach (Control ctrl in this.Po_hotkeys_gb.Controls)
            //        {
            //            if ((ctrl is HotKeyTextBox) && (((HotKeyTextBox)ctrl).Name == kjj.Hname))
            //            {
            //                ((HotKeyTextBox)ctrl).Hkey = kjj.Key;
            //                ((HotKeyTextBox)ctrl).KeyModifiers = kjj.KeyModifiers;
            //                ((HotKeyTextBox)ctrl).ShowHotKey();
            //            }
            //        }
            //        kjj.reghok(new HotkeyEventHandler(this.hk_OnHotkey), base.Handle);
            //    }
            //}
            //else
            //{
            //    foreach (Control ctrl in this.Po_hotkeys_gb.Controls)
            //    {
            //        if (ctrl is HotKeyTextBox)
            //        {
            //            ((HotKeyTextBox)ctrl).Hkey = Keys.Escape;
            //            ((HotKeyTextBox)ctrl).KeyModifiers = 0;
            //            ((HotKeyTextBox)ctrl).ShowHotKey();
            //        }
            //    }
            //}
            //foreach (kuaijijian kjj in this.hide_cfi.UHotkeys)
            //{
            //    foreach (Control ctrl in this.Po_hotkeys_gb.Controls)
            //    {
            //        if ((ctrl is HotKeyTextBox) && (((HotKeyTextBox)ctrl).Name == kjj.Hname))
            //        {
            //            ((HotKeyTextBox)ctrl).Hkey = kjj.Key;
            //            ((HotKeyTextBox)ctrl).KeyModifiers = kjj.KeyModifiers;
            //            ((HotKeyTextBox)ctrl).ShowHotKey();
            //        }
            //    }
            //}
        }
        #endregion  加载用户配置文件

        #region 应用设置
        ///// <summary>
        ///// 应用设置
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void Po_App_bt_Click(object sender, EventArgs e)
        //{
        //    #region 改变小图标显示方式

        //    if (this.hide_cfi.Xtb != this.Po_Xtb_cb.Checked)//改变小图标显示方式
        //    {
        //        this.Txtb.Visible = this.Po_Xtb_cb.Checked;
        //        this.hide_cfi.Xtb = this.Po_Xtb_cb.Checked;
        //    }

        //    #endregion 改变小图标显示方式

        //    #region 自运行
        //    if (this.hide_cfi.ZRun != this.Po_zrun_cb.Checked)//自运行
        //    {
        //        RegistryKey reg = null;
        //        string fileName = Application.ExecutablePath;
        //        try
        //        {
        //            reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
        //            if (reg == null)
        //            {
        //                reg = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
        //            }
        //            if (this.Po_zrun_cb.Checked)
        //            {
        //                reg.SetValue("HongHu", fileName + " /start");
        //            }
        //            else
        //            {
        //                reg.SetValue("HongHu", false);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("设置_自启动失败! " + ex.ToString());
        //            SysDataLog.log.Error("设置_自启动失败! " + ex);
        //            this.Po_zrun_cb.Checked = !this.Po_zrun_cb.Checked;
        //        }
        //        finally
        //        {
        //            if (reg != null)
        //            {
        //                reg.Close();
        //            }
        //            this.hide_cfi.ZRun = this.Po_zrun_cb.Checked;
        //        }
        //    }

        //    #endregion 自运行

        //    #region 是否显示进度条

        //    if (this.hide_cfi.Jdt != this.Po_jd_cb.Checked)
        //    {
        //        this.hide_cfi.Jdt = this.Po_jd_cb.Checked;
        //    }

        //    #endregion  是否显示进度条

        //    #region 隐藏方式
        //    foreach (Control ctrl in this.Po_Ycms_gb.Controls)
        //    {
        //        if ((ctrl is RadioButton) && ((RadioButton)ctrl).Checked)
        //        {
        //            this.hide_cfi.Rf = (Runfs)Enum.Parse(typeof(Runfs), ((RadioButton)ctrl).Name.Replace("Po_r_", ""));
        //            if ((this.hide_cfi.Rf == Runfs.Move) && (this.Po_Movetarget_tb.Text.Trim() != ""))
        //            {
        //                this.hide_cfi.Movetarget = this.Po_Movetarget_tb.Text;
        //            }
        //        }
        //    }

        //    #endregion 隐藏方式

        //    if (this.hide_cfi.UHotkeys != null)
        //    {
        //        foreach (kuaijijian kjj in this.hide_cfi.UHotkeys)
        //        {
        //            kjj.hotkey.UnregisterHotkeys();
        //        }
        //    }
        //    this.hide_cfi.UHotkeys = new List<kuaijijian>();
        //    foreach (Control ctrl in this.Po_hotkeys_gb.Controls)
        //    {
        //        if ((ctrl is HotKeyTextBox) && (((HotKeyTextBox)ctrl).Text.Trim() != "无"))
        //        {
        //            HotKeyTextBox _ht = (HotKeyTextBox)ctrl;
        //            this.hide_cfi.UHotkeys.Add(new kuaijijian(_ht.Name, _ht.Hkey, _ht.KeyModifiers));
        //        }
        //    }
        //    foreach (kuaijijian kjj in this.hide_cfi.UHotkeys)
        //    {
        //        kjj.reghok(new HotkeyEventHandler(this.hk_OnHotkey), base.Handle);
        //    }
        //}


        #endregion

        #region 取消设置

        /// <summary>
        /// 取消设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Po_Cancel_bt_Click(object sender, EventArgs e)
        {
            this.LoadUserConfif();
        }
        #endregion

        #region 确定设置

        /// <summary>
        /// 确定设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Po_Enter_bt_Click(object sender, EventArgs e)
        {
            //this.Po_App_bt_Click(sender, e);
            // base.Hide();
        }
        #endregion

        /// <summary>
        /// 设置隐藏目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            //this.groupBox3.Enabled = this.Po_r_Move.Checked;
        }

        #endregion 用户设置

        #region 刷新帐套
        /// <summary>
        /// 刷新帐套
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void P3_sx_bt_Click(object sender, EventArgs e)
        {
            if (SetString.SQLConn.Trim() == "")
            {
                MessageBox.Show("请先设置sql连接!");
                SetSqlConn(sender, e);
            }
            else
            {
                //dataGridView1.Enabled = false;
                //P3_back_bt.Enabled = false; ;
                //P3_sx_bt.Enabled = false;
                //this.dataGridView1.AutoGenerateColumns = false;
                //DoRunWorkerAsync(new HongHuWorkArgs(HongHuWorkArgs.EnumWork.SXZT, e));
                ////this.dataGridView1.DataSource = SqlDal.GetUA_AccountItem();
            }
        }

        #endregion 刷新帐套

        #region 显示界面
        /// <summary>
        /// 显示界面
        /// </summary>
        private void showmainfrom()
        {
            //	base.Hide();
            base.Show();
        }

        #endregion 显示界面

        #region 设置SQL连接

        /// <summary>
        /// 设置SQL连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetSqlConn(object sender, EventArgs e)
        {
            new SetConnString().ShowDialog(this);
        }

        #endregion

        #region 图标右键
        /// <summary>
        /// 显示界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txtb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.showmainfrom();
        }
        /// <summary>
        /// 设置SQL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_Set_SQL_Click(object sender, EventArgs e)
        {
            this.SetSqlConn(sender, e);
        }
        #endregion

        #region 互交按纽事件

        #region 关于按纽事件
        /// <summary>
        /// 关于
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_About_Click(object sender, EventArgs e)
        {
            base.关于ToolStripMenuItem_Click(sender, e);
        }
        #endregion  关于按纽事件


        #region 导入帐套
        private void pictureBox_DaoRu_Click(object sender, EventArgs e)
        {
            new Form_DaoRu().ShowDialog();
        }

        #endregion 导入帐套
        #endregion 互交按纽事件
    }
}
