using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.AbnormityFrame;
using HongHu;
using HongHu.DLL;
using HongHu.DLL.Config;
using HongHu.UI;
using Microsoft.Win32;
using System.Reflection;

namespace HongHu
{
    public partial class FormMoveRun : AbnormityForm
    {
        #region 变量

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

        /// <summary>
        /// 构造函数
        /// </summary>
        public FormMoveRun()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            InitializeBackgoundWorker();

            form_loading = new Loading();
            #region 关于

            //this.labelProductName.Text = AssemblyProduct;
            //this.labelVersion.Text = String.Format("版本 {0}", AssemblyVersion);
            //this.labelCopyright.Text = AssemblyCopyright;
            //this.labelCompanyName.Text = AssemblyCompany;
            //this.textBoxDescription.Text = AssemblyDescription;
            #endregion
        }


        #region 程序集属性访问器

        public string AssemblyTitle
        {
            get
            {
                // 获取此程序集上的所有 Title 属性
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // 如果至少有一个 Title 属性
                if (attributes.Length > 0)
                {
                    // 请选择第一个属性
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // 如果该属性为非空字符串，则将其返回
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // 如果没有 Title 属性，或者 Title 属性为一个空字符串，则返回 .exe 的名称
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                // 获取此程序集的所有 Description 属性
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                // 如果 Description 属性不存在，则返回一个空字符串
                if (attributes.Length == 0)
                    return "";
                // 如果有 Description 属性，则返回该属性的值
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                // 获取此程序集上的所有 Product 属性
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // 如果 Product 属性不存在，则返回一个空字符串
                if (attributes.Length == 0)
                    return "";
                // 如果有 Product 属性，则返回该属性的值
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                // 获取此程序集上的所有 Copyright 属性
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // 如果 Copyright 属性不存在，则返回一个空字符串
                if (attributes.Length == 0)
                    return "";
                // 如果有 Copyright 属性，则返回该属性的值
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                // 获取此程序集上的所有 Company 属性
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                // 如果 Company 属性不存在，则返回一个空字符串
                if (attributes.Length == 0)
                    return "";
                // 如果有 Company 属性，则返回该属性的值
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
        #endregion 构造函数

        #region 初始化函数

        /// <summary>
        /// 初始化多线程组件
        /// </summary>
        private void InitializeBackgoundWorker()
        {
            InilodfromBackWork();
            IniBackWork();
        }

        private void InilodfromBackWork()
        {
            backgroundWorkerfrom_loading = new BackgroundWorker();
            backgroundWorkerfrom_loading.WorkerReportsProgress = true;
            backgroundWorkerfrom_loading.WorkerSupportsCancellation = true;
            backgroundWorkerfrom_loading.DoWork += new DoWorkEventHandler(backgroundWorkerfrom_loading_DoWork);
            backgroundWorkerfrom_loading.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorkerfrom_loading_RunWorkerCompleted);
            //报告
            backgroundWorkerfrom_loading.ProgressChanged += new ProgressChangedEventHandler(backgroundWorkerfrom_loading_ProgressChanged);
        }
        private void IniBackWork()
        {
            this.backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerReportsProgress = false;
            //执行
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            //完成
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
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

            if (((SetString.SqlConn != "") && (SetString.SqlConn.Length > 2)))
            {
                P3_sx_bt_Click(sender, e);
                // ONE__Panel.Visible = false;
                // Two_Panel.Visible = false;
                //GoToPanel3(P2_next_bt, e);
            }
            else
            {
                MessageBox.Show("首次使用请先设置sql 连接!!");
                SetSqlConn(sender, e);
                tabControl1.SelectedTab = tp_welcome;//切换到欢迎页
            }


        }

        #endregion  程序加载函数

        #region  执行显示隐藏操作

        #region 隐藏数据

        /// <summary>
        /// 隐藏数据
        /// </summary>
        /// <param name="cAcc_Id">帐套号</param>
        /// <param name="cAcc_Name">名称</param>
        /// <param name="cAcc_live">隐藏方式</param>
        /// <param name="NianDu">年度</param>
        /// <param name="Data_path">路径</param>
        public void HideData(string cAcc_Id, string cAcc_Name, string cAcc_live, string NianDu, string Data_path)
        {
            if (!SqlDal.test_task(cAcc_Id))
            {
                SysDataLog.log.Info("帐套正在使用中,取消隐藏操作!");
                MessageBox.Show("帐套正在使用中,请稍候在试!!!!");
                return;
            }

            int HidId = SqlDal.hide_data_login(cAcc_Id, cAcc_Name, cAcc_live, NianDu, Data_path);//登陆隐藏
            switch (this.hide_cfi.Rf)
            {
                case Runfs.Login://登陆隐藏


                    break;
                case Runfs.System://系统隐藏
                    if (HidId != 0)
                    {
                        if (!SqlDal.hide_data_system(cAcc_Id, cAcc_Name, cAcc_live, NianDu, Data_path, HidId))
                        {
                            HidId = 0;
                        }
                    }
                    break;
                case Runfs.Detach://路径隐藏
                    if (HidId != 0)
                    {
                        if (!(SqlDal.hide_data_system(cAcc_Id, cAcc_Name, cAcc_live, NianDu, Data_path, HidId)
                            &&
                            SqlDal.hide_data_Detach(cAcc_Id, cAcc_Name, cAcc_live, NianDu, Data_path, HidId.ToString())
                            ))
                        {
                            HidId = 0;
                        }
                    }
                    break;
                case Runfs.Move://移动隐藏
                    if (HidId != 0)
                    {
                        if (!(SqlDal.hide_data_system(cAcc_Id, cAcc_Name, cAcc_live, NianDu, Data_path, HidId)
                            &&
                            SqlDal.hide_data_Detach(cAcc_Id, cAcc_Name, cAcc_live, NianDu, Data_path, HidId.ToString())
                            &&
                            SqlDal.hide_data_move(cAcc_Id, cAcc_Name, cAcc_live, NianDu, Data_path, HidId.ToString(), this.hide_cfi.Movetarget)
                            // &&
                            ))
                        {
                            HidId = 0;
                        }
                    }
                    break;
            }
        }

        #endregion 隐藏数据

        #region 显示帐套

        /// <summary>
        /// 显示帐套
        /// </summary>
        /// <param name="cAcc_live"></param>
        /// <param name="HideId"></param>
        public void ShowData(string cAcc_live, string HideId, string cAcc_Id)
        {
            string Temp1 = cAcc_live;//为什么要多此一举呢？  忘了啊！！！！！！！  为什么呢？
            switch (Temp1)
            {
                case "登录隐藏":
                    SqlDal.Show_Log(HideId);
                    SqlDal.Del_hidinfo(HideId);
                    break;
                case "系统隐藏":
                    SqlDal.Show_System(HideId);
                    SqlDal.Show_Log(HideId);
                    SqlDal.Del_hidinfo(HideId);
                    break;
                case "分离隐藏":
                    SqlDal.Show_Detach(HideId);
                    SqlDal.Show_System(HideId);
                    SqlDal.Show_Log(HideId);
                    SqlDal.Del_hidinfo(HideId);
                    break;
                case "移动隐藏":
                    SqlDal.Show_Move(HideId, cAcc_Id);
                    SqlDal.Show_Detach(HideId);
                    SqlDal.Show_System(HideId);
                    SqlDal.Show_Log(HideId);
                    SqlDal.Del_hidinfo(HideId);
                    break;
            }
        }

        #endregion

        #region dataGridView1单击事件
        /// <summary>
        /// dataGridView1单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.ColumnIndex == this.dataGridView1.Columns["CAcc_hide"].Index))
            {
                //开始后台线程
                dataGridView1.Enabled = false;
                //P3_back_bt.Enabled = false; ;
                P3_sx_bt.Enabled = false;
                DoRunWorkerAsync(new HongHUWorkArgs(HongHUWorkArgs.EnumWork.XSYC, e));
            }
        }
        #endregion dataGridView1单击事件

        #endregion 执行显示隐藏操作

        #region 执行多线程操作

        #region 执行多线程操作
        /// <summary>
        /// 执行多线程显示隐藏操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoRunWorkerAsync(HongHUWorkArgs e)
        {
            if (backgroundWorkerfrom_loading.IsBusy)
            {
                MessageBox.Show("前一个任务backgroundWorkerfrom_loading尚未执行完成，请耐心等候..");
                SysDataLog.log.Error("前一个任务backgroundWorkerfrom_loading尚未执行完成");
                backgroundWorkerfrom_loading.CancelAsync();
                InilodfromBackWork();//重新初始化
            }
            else if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
                MessageBox.Show("前一个任务backgroundWorker1尚未执行完成，请耐心等候..");
                SysDataLog.log.Error("前一个任务backgroundWorker1尚未执行完成");
                IniBackWork(); //重新初始化

            }

                backgroundWorkerfrom_loading.RunWorkerAsync();
                backgroundWorker1.RunWorkerAsync(e);
        }
        #endregion 执行多线程操作

        #region 线程结束
        /// <summary>
        /// 线程结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                //resultLabel.Text = "Canceled";
            }
            else
            {
                //resultLabel.Text = e.Result.ToString();
            }
            if ((HongHUWorkArgs.EnumWork)e.Result == HongHUWorkArgs.EnumWork.XSYC)
            {
                //刷新显示
                ComputeFibonacci(new DoWorkEventArgs(new HongHUWorkArgs(HongHUWorkArgs.EnumWork.SXZT, null)));
            }
            backgroundWorkerfrom_loading.CancelAsync();
            form_loading.Close();
            //form_loading.Hide();
            dataGridView1.Enabled = true;
            //  P3_back_bt.Enabled = true;
            P3_sx_bt.Enabled = true;
            //this.P3_sx_bt_Click(sender, new EventArgs());
        }
        /// <summary>
        /// 进度线程结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerfrom_loading_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                SysDataLog.log.Error(e.Error);
            }
            //else if (e.Cancelled){  resultLabel.Text = "Canceled";}
            else
            {
                //resultLabel.Text = e.Result.ToString();
            }
        }
        #endregion 线程结束

        #region 执行线程

        #region 进度线程
        /// <summary>
        /// 执行进度线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerfrom_loading_DoWork(object sender, DoWorkEventArgs e)
        {
            //Application.Run(form_loading);
            //this.from_loading.Show();
            if (hide_cfi.Jdt)
            {
                if (form_loading.Visible != false)
                {
                    MessageBox.Show("form_loading.Visible != false");
                    form_loading.Visible = false;
                }
                    form_loading.ShowDialog();
            }
        }

        #endregion

        #region 执行工作主线程
        /// <summary>
        /// 执行工作主线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = ((HongHUWorkArgs)e.Argument).WorkDoEnumWork;
            //backgroundWorkerfrom_loading = worker;
            ComputeFibonacci(e); //e.Result = ComputeFibonacci((DataGridViewCellEventArgs)e.Argument, worker, e);
        }
        /// <summary>
        /// 执行主要工作
        /// </summary>
        /// <param name="dgvcea"></param>
        /// <param name="worker"></param>
        /// <param name="dwea"></param>
        void ComputeFibonacci(DoWorkEventArgs dwea)
        {
            HongHUWorkArgs hhwkags = (HongHUWorkArgs)dwea.Argument;
            switch (hhwkags.WorkDoEnumWork)
            {
                case HongHUWorkArgs.EnumWork.XSYC:
                    DataGridViewCellEventArgs dgvcea = (DataGridViewCellEventArgs)hhwkags.Args;
                    // switch
                    if (this.dataGridView1["CAcc_hide", dgvcea.RowIndex].Value.ToString().Trim() == "隐藏")
                    {
                        //执行显示操作
                        this.HideData(this.dataGridView1["cAcc_Id", dgvcea.RowIndex].Value.ToString().Trim(), this.dataGridView1["cAcc_Name", dgvcea.RowIndex].Value.ToString().Trim(), this.hide_cfi.Rf.ToString("d"), "All", "");
                    }
                    else
                    {
                        //执行隐藏操作
                        this.ShowData(this.dataGridView1["cAcc_live", dgvcea.RowIndex].Value.ToString().Trim(), this.dataGridView1["HideId", dgvcea.RowIndex].Value.ToString().Trim(), this.dataGridView1["cAcc_Id", dgvcea.RowIndex].Value.ToString().Trim());
                    }
                    break;
                case HongHUWorkArgs.EnumWork.SXZT:
                    this.dataGridView1.AutoGenerateColumns = false;
                    this.dataGridView1.DataSource = SqlDal.GetUA_AccountItem();
                    break;
            }
            //取消后台工作
            //if (worker.CancellationPending)
            //{
            //    dwea.Cancel = true;
            //}
            //报告进度
            //worker.ReportProgress(percentComplete);
            //return result;
        }

        #endregion

        #endregion 执行线程

        #region 进度报告

        /// <summary>
        /// 进度报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerfrom_loading_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //this.progressBar1.Value = e.ProgressPercentage;
            //sender.GetType();
            //e.GetType();
            try
            {
                form_loading.LoadingStr_lab.Text = e.UserState.ToString();
            }
            catch (Exception ex)
            {
                SysDataLog.log.Error("进度报告出错", ex);
            }
        }

        #endregion 进度报告

        #endregion 执行多线程操作

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
            ConfigItemXML.SaveConfig();
        }


        #endregion 退出程序

        #region 面版切换函数

        //private void GoToPanel_Options(object sender, EventArgs e)
        //{
        //    //this.Options_Panel.Visible = true;
        //   // ((Button)sender).Parent.Visible = false;
        //   // this.groupBox3.Enabled = this.Po_r_Move.Checked;
        //}

        //private void GoToPanel1(object sender, EventArgs e)
        //{
        //    //this.ONE__Panel.Visible = true;
        //   // ((Button)sender).Parent.Visible = false;
        //}

        //private void GoToPanel2(object sender, EventArgs e)
        //{
        //    //this.Two_Panel.Visible = true;
        //    //((Button)sender).Parent.Visible = false;
        //}

        //private void GoToPanel3(object sender, EventArgs e)
        //{
        //    if (!((SetString.SqlConn != "") && (SetString.SqlConn.Length > 2)))
        //    {
        //        SetSqlConn(sender, e);
        //        //MessageBox.Show("请先设置sql 连接!!");
        //        //GoToPanel3(sender,e);
        //    }
        //    else
        //    {
        //        //this.Three_panel.Visible = true;
        //        //((Button)sender).Parent.Visible = false;
        //        P3_sx_bt_Click(sender, e);
        //    }
        //}

        /// <summary>
        /// 面版背景切换函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void ONE__Panel_VisibleChanged(object sender, EventArgs e)
        //{
        //    Panel pl = (Panel)sender;
        //    if ((pl.Name == "ONE__Panel") && pl.Visible)
        //    {
        //        //this.BackgroundImage = HongHu.Properties.Resources.背景_1;
        //    }
        //    else if ((pl.Name == "Two_Panel") && pl.Visible)
        //    {
        //       // this.BackgroundImage = HongHu.Properties.Resources.背景_2_设置SQL;
        //    }
        //    else if (((pl.Name != "ONE__Panel") && (pl.Name != "Two_Panel")) && pl.Visible)
        //    {
        //       // this.BackgroundImage = HongHu.Properties.Resources.背景_3;
        //    }
        //}
        #endregion  面版切换函数

        #region 热键回调涵数
        /// <summary>
        /// 热键回调涵数
        /// </summary>
        /// <param name="HotKeyID"></param>
        public void hk_OnHotkey(int HotKeyID)
        {
            foreach (Kuaijijian kjj in this.hide_cfi.UHotkeys)
            {
                if (kjj.regid == HotKeyID)
                {
                    string Temp = kjj.Hname;
                    if (Temp != null)
                    {
                        if (!(Temp == "Po_Zjm_tb"))
                        {
                            if ((Temp == "Po_Yc_tb") || (Temp == "Po_xs_tb"))
                            {
                                break;
                            }
                        }
                        else
                        {
                            if (this.Visible==true)
                            {
                                this.Hide();
                            }
                            else
                            {
                                /// <summary>
                                /// 显示主界面
                                /// </summary>
                                this.showmainfrom();
                                return;
                            }
                        }
                    }
                }
            }
        }

        #endregion 热键回调涵数

        #region 用户设置

        #region 加载用户配置文件
        /// <summary>
        /// 加载用户配置文件
        /// </summary>
        public void LoadUserConfif()
        {
            this.Uconfig = new ConfigItemXML();
            this.hide_cfi = (SetString.GetConfigJson<HideConfigItem>() != null) ? SetString.GetConfigJson<HideConfigItem>() : new HideConfigItem();
            this.Txtb.Visible = this.hide_cfi.Xtb;
            this.Po_Xtb_cb.Checked = this.hide_cfi.Xtb;
            this.Po_zrun_cb.Checked = this.hide_cfi.ZRun;
            this.Po_jd_cb.Checked = this.hide_cfi.Jdt;
            foreach (Control ctrl in this.Po_Ycms_gb.Controls)
            {
                if ((ctrl is RadioButton) && (this.hide_cfi.Rf == ((Runfs)Enum.Parse(typeof(Runfs), ((RadioButton)ctrl).Name.Replace("Po_r_", "")))))
                {
                    ((RadioButton)ctrl).Checked = true;
                    this.Po_Movetarget_tb.Text = this.hide_cfi.Movetarget;
                }
            }
            if (this.hide_cfi.UHotkeys != null)
            {
                foreach (Kuaijijian kjj in this.hide_cfi.UHotkeys)
                {
                    foreach (Control ctrl in this.Po_hotkeys_gb.Controls)
                    {
                        if ((ctrl is HotKeyTextBox) && (((HotKeyTextBox)ctrl).Name == kjj.Hname))
                        {
                            ((HotKeyTextBox)ctrl).Hkey = kjj.Key;
                            ((HotKeyTextBox)ctrl).KeyModifiers = kjj.keyModifiers;
                            ((HotKeyTextBox)ctrl).ShowHotKey();
                        }
                    }
                    kjj.reghok(new HotkeyEventHandler(this.hk_OnHotkey), base.Handle);
                }
            }
            else
            {
                foreach (Control ctrl in this.Po_hotkeys_gb.Controls)
                {
                    if (ctrl is HotKeyTextBox)
                    {
                        ((HotKeyTextBox)ctrl).Hkey = Keys.Escape;
                        ((HotKeyTextBox)ctrl).KeyModifiers = 0;
                        ((HotKeyTextBox)ctrl).ShowHotKey();
                    }
                }
            }
            foreach (Kuaijijian kjj in this.hide_cfi.UHotkeys)
            {
                foreach (Control ctrl in this.Po_hotkeys_gb.Controls)
                {
                    if ((ctrl is HotKeyTextBox) && (((HotKeyTextBox)ctrl).Name == kjj.Hname))
                    {
                        ((HotKeyTextBox)ctrl).Hkey = kjj.Key;
                        ((HotKeyTextBox)ctrl).KeyModifiers = kjj.keyModifiers;
                        ((HotKeyTextBox)ctrl).ShowHotKey();
                    }
                }
            }
        }
        #endregion  加载用户配置文件

        #region 应用设置
        /// <summary>
        /// 应用设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Po_App_bt_Click(object sender, EventArgs e)
        {
            #region 改变小图标显示方式

            if (this.hide_cfi.Xtb != this.Po_Xtb_cb.Checked)//改变小图标显示方式
            {
                this.Txtb.Visible = this.Po_Xtb_cb.Checked;
                this.hide_cfi.Xtb = this.Po_Xtb_cb.Checked;
            }

            #endregion 改变小图标显示方式

            #region 自运行
            if (this.hide_cfi.ZRun != this.Po_zrun_cb.Checked)//自运行
            {
                RegistryKey reg = null;
                string fileName = Application.ExecutablePath;
                try
                {
                    reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                    if (reg == null)
                    {
                        reg = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    }
                    if (this.Po_zrun_cb.Checked)
                    {
                        reg.SetValue("HongHu", fileName + " /start");
                    }
                    else
                    {
                        reg.SetValue("HongHu", false);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("设置_自启动失败! " + ex.ToString());
                    SysDataLog.log.Error("设置_自启动失败! " + ex);
                    this.Po_zrun_cb.Checked = !this.Po_zrun_cb.Checked;
                }
                finally
                {
                    if (reg != null)
                    {
                        reg.Close();
                    }
                    this.hide_cfi.ZRun = this.Po_zrun_cb.Checked;
                }
            }

            #endregion 自运行

            #region 是否显示进度条

            if (this.hide_cfi.Jdt != this.Po_jd_cb.Checked)
            {
                this.hide_cfi.Jdt = this.Po_jd_cb.Checked;
            }

            #endregion  是否显示进度条

            #region 隐藏方式
            foreach (Control ctrl in this.Po_Ycms_gb.Controls)
            {
                if ((ctrl is RadioButton) && ((RadioButton)ctrl).Checked)
                {
                    this.hide_cfi.Rf = (Runfs)Enum.Parse(typeof(Runfs), ((RadioButton)ctrl).Name.Replace("Po_r_", ""));
                    if ((this.hide_cfi.Rf == Runfs.Move) && (this.Po_Movetarget_tb.Text.Trim() != ""))
                    {
                        this.hide_cfi.Movetarget = this.Po_Movetarget_tb.Text;
                    }
                }
            }

            #endregion 隐藏方式

            if (this.hide_cfi.UHotkeys != null)
            {
                foreach (Kuaijijian kjj in this.hide_cfi.UHotkeys)
                {
                    kjj.Hotkey.UnregisterHotkeys();
                }
            }
            this.hide_cfi.UHotkeys = new List<Kuaijijian>();
            foreach (Control ctrl in this.Po_hotkeys_gb.Controls)
            {
                if ((ctrl is HotKeyTextBox) && (((HotKeyTextBox)ctrl).Text.Trim() != "无"))
                {
                    HotKeyTextBox _ht = (HotKeyTextBox)ctrl;
                    this.hide_cfi.UHotkeys.Add(new Kuaijijian(_ht.Name, _ht.Hkey, _ht.KeyModifiers));
                }
            }
            foreach (Kuaijijian kjj in this.hide_cfi.UHotkeys)
            {
                kjj.reghok(new HotkeyEventHandler(this.hk_OnHotkey), base.Handle);
            }
        }


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
            this.Po_App_bt_Click(sender, e);
            // base.Hide();
        }
        #endregion

        #region 选择移动文件夹
        /// <summary>
        /// 选择移动文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bows_Paths_Click(object sender, EventArgs e)
        {
            using (OpenFolderDialog openFolderDlg = new OpenFolderDialog())
            {
                if (openFolderDlg.ShowDialog() == DialogResult.OK)
                {
                    this.Po_Movetarget_tb.Text = openFolderDlg.Path;
                }
                else
                {
                    this.Po_Movetarget_tb.Text = "";
                }
            }
        }

        #endregion 选择移动文件夹


        /// <summary>
        /// 设置隐藏目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBox3.Enabled = this.Po_r_Move.Checked;
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
            if (SetString.SqlConn.Trim() == "")
            {
                MessageBox.Show("请先设置sql连接!");
                SetSqlConn(sender, e);
            }

            if ((SetString.SqlConn.Trim() != ""))
            {
                dataGridView1.Enabled = false;
                //P3_back_bt.Enabled = false; ;
                P3_sx_bt.Enabled = false;
                this.dataGridView1.AutoGenerateColumns = false;
                try
                {
                    DoRunWorkerAsync(new HongHUWorkArgs(HongHUWorkArgs.EnumWork.SXZT, e));
                }
                catch (Exception ex)
                {
                    SysDataLog.log.Error(ex);
                    MessageBox.Show("刷新数据失败!\n" + ex.Message);
                }
                //this.dataGridView1.DataSource = SqlDal.GetUA_AccountItem();
            }
        }

        #endregion 刷新帐套

        #region 显示界面
        /// <summary>
        /// 显示界面
        /// </summary>
        private void showmainfrom()
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            //	base.Hide();
           // base.Show();
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

        #region 历史遗留
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (base.Size.Width < 560)
            {
                base.Size += new Size(40, 0);
            }
            else if (base.Size.Height < 0x166)
            {
                base.Size += new Size(0, 14);
            }
            else
            {
                this.timer1.Enabled = false;
                MessageBox.Show("OK");
            }
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
        private void 设置SQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SetSqlConn(sender, e);
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void 显示主界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showmainfrom();
        }
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new HongHu.UI.AboutBox1().ShowDialog();
            // new Client.frmAbout().ShowDialog();
        }
        #endregion

        /// <summary>
        /// 最小化到图标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMoveRun_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}