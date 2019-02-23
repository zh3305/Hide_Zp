using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HongHu.UI
{
    public partial class Form_DaoRu : Form
    {
        public Form_DaoRu()
        {
            InitializeComponent();
        }

        private void glassButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ShuaXin_Click(object sender, EventArgs e)
        {
            SqlDal.GetUA_AccountItem(ref checkedListBox1);
        }
        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_DaoRu_Click(object sender, EventArgs e)
        {
            string cAcc_Id = "";
            string cAcc_Name = "";
            for (int intForTemp = 0; intForTemp < checkedListBox1.Items.Count; intForTemp++)
            {
                if (checkedListBox1.GetItemChecked(intForTemp))
                {
                    cAcc_Id = checkedListBox1.Items[intForTemp].ToString().Substring(1, 3);
                    cAcc_Name=checkedListBox1.Items[intForTemp].ToString().Substring(5, checkedListBox1.Items[intForTemp].ToString().Length-5);
                    if (!SqlDal.test_task(cAcc_Id))
                    {
                        SysDataLog.log.Info("帐套正在使用中,取消隐藏操作!");
                        MessageBox.Show("帐套正在使用中,请稍候在试!!!!_跳过当前帐套");
                        break;
                    }
                    int HidId = SqlDal.hide_data_login(cAcc_Id, cAcc_Name,Runfs.Move.ToString("d"), "All", "");//登陆隐藏
                    if (HidId != 0)
                    {
                        if (!(SqlDal.hide_data_system(cAcc_Id, cAcc_Name, "", "All", "", HidId)
                            &&
                            SqlDal.hide_data_Detach(cAcc_Id, cAcc_Name, Runfs.Move.ToString("d"), "All", "", HidId.ToString())
                            &&
                            SqlDal.hide_data_move(cAcc_Id, cAcc_Name, Runfs.Move.ToString("d"), "All", "", HidId.ToString(), "")
                            // &&
                            ))
                        {

                            MessageBox.Show(cAcc_Id + cAcc_Name+" 导入失败！");
                            HidId = 0;
                        }
                    }
                    MessageBox.Show(cAcc_Id + cAcc_Name);
                }
            }
        }
    }
}