using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HongHu.UI
{
    public partial class SetConnString : Form
    {

        /// <summary>
        /// 后台线程
        /// </summary>
        public BackgroundWorker backgroundWorker1;
        /// <summary>
        /// Loadingform
        /// </summary>
        Loading Loadingform;
        public SetConnString()
        {
            InitializeComponent();
        }
        /// <summary>
        /// LoadingFrom 
        /// 构造字符串
        /// </summary>
        /// <param name="SQLServer"></param>
        /// <param name="DBUser"></param>
        /// <param name="DBUPwd"></param>
        public SetConnString(string SQLServer,string DBUser,string DBUPwd)
        {
        	SQLServer_ComBox.Text =SQLServer;
        	DBUser_textBox.Text=DBUser_textBox.Text;
        	DBUPwdtextBox.Text=DBUPwd;
        	//SetConnString();
            InitializeComponent();
        }

        #region 测试连接


        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestConn(object sender, EventArgs e)
        {
            if (SQLServer_ComBox.Text == "" || SQLServer_ComBox.Text == null)
            {
                MessageBox.Show("请选择 数据库实例");
                return;
            }
            else if (DBUser_textBox.Text == "" || DBUser_textBox.Text == null)
            {
                MessageBox.Show("请输入用户");
                return;
            }
            else if (this.TianChong_button.Text == "")
            {
                MessageBox.Show("等待.上一个操作完成");
                return;
            }
            else
            {
                Loadingform = new HongHu.UI.Loading();
                Loadingform.Show(this);
                backgroundWorker1 = new BackgroundWorker();
                this.backgroundWorker1.WorkerReportsProgress = false;//获取或设置一个值，该值指示BackgroundWorker能否报告进度更新
                this.backgroundWorker1.WorkerSupportsCancellation = true;//获取或设置一个值，该值指示 BackgroundWorker 是否支持异步取消。

                backgroundWorker1.DoWork += new DoWorkEventHandler(TestConn_Statr);
                backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(TestConn_Completed);

                toolStripStatusLabel1.Text = "正在连接到SQL.";
                this.TianChong_button.Enabled = false;
                this.Test_button.Enabled = false;
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void TestConn_Statr(object sender, DoWorkEventArgs e)
        {
            string Tmsg = DAL.DBUtility.SqlHelper.TestConnStr(SQLServer_ComBox.Text, "master", DBUser_textBox.Text, DBUPwdtextBox.Text);
            if (Tmsg != null)
            {

                if (e.Cancel)
                {
                    return;
                }
                Loadingform.Hide();
               // throw new ApplicationException(Tmsg);
                MessageBox.Show(Tmsg);
               // backgroundWorker1.CancelAsync();
                e.Cancel = true;
            }
        }


        private void TestConn_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null) //处理异常被抛出
            {
                Loadingform.Hide();
                MessageBox.Show(e.Error.Message); //显示异常
            }
            else if (e.Cancelled)  //其次,用户取消了在处理事件
            {

            }
            else //执行成功
            {
                Loadingform.Hide();
                MessageBox.Show("连接成功!");
                Enter_button.Enabled = true;
            }
            Loadingform.Close();
            toolStripStatusLabel1.Text = "就绪.";
            this.TianChong_button.Enabled = true;
            this.Test_button.Enabled = true;
            this.Focus();
        }
        #endregion


        #region 枚举sql实例

        private void EnumeratorSQL(object sender, EventArgs e)
        {
            if (this.TianChong_button.Text == "")
            {
                return;
            }
            backgroundWorker1 = new BackgroundWorker();
            this.backgroundWorker1.WorkerReportsProgress = false;//获取或设置一个值，该值指示BackgroundWorker能否报告进度更新
            this.backgroundWorker1.WorkerSupportsCancellation = true;//获取或设置一个值，该值指示 BackgroundWorker 是否支持异步取消。

            backgroundWorker1.DoWork += new DoWorkEventHandler(GetSqlServer);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(GetSqlServer_Completed);
            toolStripStatusLabel1.Text = "正在查找网络上的SQL实例.";
            this.TianChong_button.Text = "";
            this.Test_button.Enabled = false;
            this.TianChong_button.Image = global::HongHu.Properties.Resource.loading2;
            backgroundWorker1.RunWorkerAsync();
        }

        private void GetSqlServer_Completed(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Error != null) //处理异常被抛出
            {
                MessageBox.Show(e.Error.Message); //显示异常

            }
            else if (e.Cancelled)  //其次,用户取消了在处理事件
            {

            }
            else //执行成功
            {

            }

            this.TianChong_button.Text = "填充";
            toolStripStatusLabel1.Text = "就绪.";
            this.TianChong_button.Image = null;
            this.Test_button.Enabled = true;
        }
        /// <summary>
        /// 获取SQL实例
        /// </summary>
        private void GetSqlServer(object sender, DoWorkEventArgs e)
        {
            DataTable table = DAL.DBUtility.SqlHelper.GetSqlServer();
            //comboBox1.Items.Add()
            SQLServer_ComBox.DisplayMember = "ServerName";
            SQLServer_ComBox.DataSource = table;
            /**********返回*************/
            e.Result = true;
        }

        #endregion

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close(object sender, EventArgs e)
        {
        	this.Close();
        }

        private void XBox_TextChanged(object sender, EventArgs e)
        {
            Enter_button.Enabled = false;
        }

        private void Enter_button_Click(object sender, EventArgs e)
        {
        	 HongHu.SetString.DBName=SQLServer_ComBox.Text;
            HongHu.SetString.SQLConn = DAL.DBUtility.SqlHelper.ConnStrFormat(SQLServer_ComBox.Text, "master", DBUser_textBox.Text, DBUPwdtextBox.Text);
            this.Close();
        }
        /// <summary>
        /// 关闭中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetConnString_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.backgroundWorker1 != null)
            {
                this.backgroundWorker1.CancelAsync();
                this.backgroundWorker1.Dispose();
            }
        }
    }
}