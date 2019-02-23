using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HongHu.DAL;
using HongHu.DAL.DBUtility;

namespace HongHu
{

    public class SqlDal
    {
        #region 属性
        /// <summary>
        /// SQLite事务
        /// </summary>
        public static SQLiteTransaction sqlitetrans;
        /// <summary>
        /// Transact-SQL 语句
        /// 删除帐套在 Log_Account ，UA_Account_sub，UA_Account中的信息
        /// </summary>
        private static readonly string SQL_Delete_Log_Account = "use ufsystem \n begin transaction \n  DELETE FROM UA_Log WHERE cAcc_Id = '{0}'\n  DELETE FROM UA_Account_sub    WHERE cAcc_Id = '{0}'\n  DELETE FROM UA_Account  WHERE cAcc_Id = '{0}'\n if @@error<>0  --- @@error如果前一个 Transact-SQL 语句执行没有错误，则返回 0。\n   begin \n rollback transaction  --清除自事务的起点或到某个保存点所做的所有数据修改。\n   return \n end \n commit transaction  --标志一个成功事务的结束。";
        // private static readonly string SQL_Delete_Period_HoldAuth_BackupPlans = "use ufsystem \n begin transaction \n  DELETE FROM UA_HoldAuth WHERE cAcc_Id = '{0}'\n  DELETE FROM UA_Period   WHERE cAcc_Id = '{0}'\n if @@error<>0  --- @@error如果前一个 Transact-SQL 语句执行没有错误，则返回 0。\n   begin \n rollback transaction  --清除自事务的起点或到某个保存点所做的所有数据修改。\n   return \n end \n commit transaction  --标志一个成功事务的结束。";
       
        /// <summary>
        /// Transact-SQL 语句
        /// 删除帐套在 UA_HoldAuth ，UA_Period，UA_BackupPlans中的信息
        /// </summary>
        private static readonly string SQL_Delete_Period_HoldAuth_BackupPlans = "use ufsystem \n begin transaction \n  DELETE FROM UA_HoldAuth WHERE cAcc_Id = '{0}'\n  DELETE FROM UA_Period   WHERE cAcc_Id = '{0}'\n  DELETE FROM UA_BackupPlans   WHERE cAcc_Id = '{0}'\n if @@error<>0  --- @@error如果前一个 Transact-SQL 语句执行没有错误，则返回 0。\n   begin \n rollback transaction  --清除自事务的起点或到某个保存点所做的所有数据修改。\n   return \n end \n commit transaction  --标志一个成功事务的结束。";
        /// <summary>
        /// Transact-SQL 语句
        /// 分离数据库
        /// </summary>
        private static readonly string SQL_detach = "use master declare @dbName nvarchar(50) \n set @dbName='{0}' --数据库名 \n declare   @spid   nvarchar(20)  \n declare   cur_lock   cursor   for  \n SELECT spID FROM master..sysprocesses WHERE  dbid= db_id(@dbName) \n open   cur_lock  \n fetch   cur_lock      into   @spid  \n while   @@fetch_status=0  \n     begin      \n     exec( 'kill '+@spid )  \n     fetch   Next From cur_lock into @spid \n     end      \n close   cur_lock \n deallocate   cur_lock \n exec sp_detach_db @dbName, N'true'";
        /// <summary>
        /// Transact-SQL 语句
        /// 附加数据库
        /// </summary>
        private static readonly string SQL_fjdata = "use master EXEC sp_attach_db '{0}' , '{1}' , '{2}' ";
        /// <summary>
        /// Transact-SQL 语句
        /// 将帐套相关信息插入UA_Account表中
        /// </summary>
        private static readonly string SQL_Insert_UA_Account = "use ufsystem INSERT INTO UA_Account(iSysID, cAcc_Id, cAcc_Name, cAcc_Path, iYear, iMonth, cAcc_Master, cCurCode, cCurName, cUnitName, cUnitAbbre, cUnitAddr, cUnitZap, cUnitTel, cUnitFax, cUnitEMail, cUnitTaxNo, cUnitLP, cFinKind, cFinType, cEntType, cTradeKind) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}')";
        /// <summary>
        /// Transact-SQL 语句
        /// 将帐套相关信息插入UA_Account_sub表中
        /// </summary>
        private static readonly string SQL_Insert_UA_Account_sub = "use ufsystem INSERT INTO UA_Account_sub(cAcc_Id, iYear, cSub_Id, bIsDelete, bClosing, iModiperi, dSubSysUsed)   values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
        /// <summary>
        /// Transact-SQL 语句
        /// 将帐套相关信息插入UA_HoldAuth表中
        /// </summary>
        private static readonly string SQL_Insert_UA_HoldAuth = "use ufsystem INSERT INTO UA_HoldAuth(cAcc_Id, iYear, cUser_Id, cAuth_Id) values('{0}','{1}','{2}','{3}')";
        /// <summary>
        /// Transact-SQL 语句
        /// 将帐套相关信息插入UA_Log表中
        /// </summary>
        private static readonly string SQL_Insert_UA_Log = "use ufsystem \tINSERT INTO UA_Log(cAcc_Id, cSub_Id, cUser_Id, cAuth_Id, cStation, dInTime, dOutTime, iLogId, iYear, Success)   values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')";
        /// <summary>
        /// Transact-SQL 语句
        /// 将帐套会计期间插入UA_Period表中
        /// </summary>
        private static readonly string SQL_Insert_UA_Period = "use ufsystem INSERT INTO UA_Period(cAcc_Id, iYear, iId,dBegin, dEnd, bIsDelete)  values('{0}','{1}','{2}','{3}','{4}','{5}')";
        /// <summary>
        /// Transact-SQL 语句
        /// 将帐套相关信息插入UA_BackupPlans表中
        /// </summary>
        private static readonly string SQL_Insert_UA_BackupPlans = "use ufsystem INSERT INTO UA_BackupPlans( [cPlan_Id], [cAcc_Id], [iYear])  values('{0}','{1}','{2}')";
        /// <summary>
        /// Transact-SQL 语句
        /// 查询现有帐套
        /// </summary>
        private static readonly string SQL_Select_UA_Account = "use ufsystem SELECT cAcc_Id ,cAcc_Name FROM UA_Account";
        /// <summary>
        /// Transact-SQL 语句
        /// 查询正在操作帐套的总数
        /// </summary>
        private static readonly string SQL_Select_UA_Task_count = "use ufsystem select count(*) as Count from ua_task where cAcc_Id in ('{0}')";
        /// <summary>
        /// Transact-SQL 语句
        /// 查询帐套在UA_Account_sub表中的数据
        /// </summary>
        private static readonly string SQL_Select_Account_sub = "use ufsystem \tSELECT cAcc_Id, iYear, cSub_Id, bIsDelete, bClosing, iModiperi, dSubSysUsed FROM UA_Account_sub WHERE cAcc_Id = '{0}'";
        /// <summary>
        /// Transact-SQL 语句
        /// 查询帐套在UA_Account表中的数据
        /// </summary>
        private static readonly string SQL_Select_UA_Account2 = "use ufsystem \tSELECT iSysID, cAcc_Id, cAcc_Name, cAcc_Path, iYear, iMonth, cAcc_Master, cCurCode, cCurName, cUnitName, cUnitAbbre, cUnitAddr, cUnitZap, cUnitTel, cUnitFax, cUnitEMail, cUnitTaxNo, cUnitLP, cFinKind, cFinType, cEntType, cTradeKind FROM UA_Account WHERE cAcc_Id = '{0}'";
        /// <summary>
        /// Transact-SQL 语句
        /// 查询帐套所有数据库的名称及路径
        /// </summary>
        private static readonly string SQL_Select_DataFile = "SELECT [name],[filename]  FROM [master]..[sysdatabases]where name like 'Ufdata[_]{0}%'";
        /// <summary>
        /// Transact-SQL 语句
        /// 查询帐套在UA_HoldAuth（权限）表中的数据
        /// </summary>
        private static readonly string SQL_Select_UA_HoldAuth = "use ufsystem SELECT  cAcc_Id, iYear, cUser_Id, cAuth_Id,1 FROM UA_HoldAuth WHERE cAcc_Id = '{0}'";
        /// <summary>
        /// Transact-SQL 语句
        /// 查询帐套在UA_Log（日志）表中的数据
        /// </summary>
        private static readonly string SQL_Select_UA_Log = "use ufsystem \tSELECT cAcc_Id, cSub_Id, cUser_Id, cAuth_Id, cStation, dInTime, dOutTime, iLogId, iYear, Success FROM UA_Log WHERE cAcc_Id = '{0}'";
        /// <summary>
        /// Transact-SQL 语句
        /// 查询帐套在UA_Period（会计期间）表中的数据
        /// </summary>
        private static readonly string SQL_Select_UA_Period = "use ufsystem \tSELECT cAcc_Id, iYear, iId,dBegin, dEnd, bIsDelete FROM UA_Period WHERE cAcc_Id = '{0}'";
        /// <summary>
        /// Transact-SQL 语句
        /// 查询帐套在UA_BackupPlans（备份计划子表）中的数据
        /// </summary>
        private static readonly string SQL_Select_UA_BackupPlans = "use ufsystem \tSELECT [cPlan_Id], [cAcc_Id], [iYear] ---, [bOnline]--不用的字段 \n FROM [UFSystem].[dbo].[UA_BackupPlans] WHERE cAcc_Id = '{0}'";
        /// <summary>
        /// Transact-SQLite 语句
        /// 将帐套的子备份计划插入隐藏数据库的UA_BackupPlans表中
        /// </summary>
        private static readonly string SQLite_Insert_UA_BackupPlans_Hide = "insert into [UA_BackupPlans_Hide] ([cPlan_Id] ,[cAcc_Id],[iYear],[HideId]) values('{0}','{1}','{2}','{3}')";
        /// <summary>
        /// Transact-SQLite 语句
        /// 将帐套的数据库路径插入隐藏数据库的HideDataPath表中
        /// </summary>
        private static readonly string SQLite_Insert_HideDataPath = "insert into [HideDataPath] ([Name] ,[Path],[HideId],[cAcc_Id]) values('{0}','{1}','{2}','{3}')";
        /// <summary>
        /// Transact-SQLite 语句
        /// 添加隐藏信息
        /// </summary>
        private static readonly string SQLite_Insert_HideInfo = "insert into  hideinfo(cAcc_Id,cAcc_Name,cAcc_live,NianDu,dDate,Data_path,IsDelete) values('{0}','{1}','{2}','{3}','{4}','{5}',0)";
        /// <summary>
        /// Transact-SQLite 语句
        /// 将帐套信息插入隐藏数据库的UA_Account_Hide表中
        /// </summary>
        private static readonly string SQLite_Insert_UA_Account_Hide = " Insert Into [UA_Account_Hide] ([iSysID],[cAcc_Id],[cAcc_Name],[cAcc_Path],[iYear],[iMonth],[cAcc_Master],[cCurCode],[cCurName],[cUnitName],[cUnitAbbre],[cUnitAddr],[cUnitZap],[cUnitTel],[cUnitFax],[cUnitEMail],[cUnitTaxNo],[cUnitLP],[cFinKind],[cFinType],[cEntType],[cTradeKind],[HideInfoId])values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')";
        /// <summary>
        /// Transact-SQLite 语句
        /// </summary>
        private static readonly string SQLite_Insert_UA_Account_Sub_Hide = "insert into [UA_Account_Sub_Hide] ([cAcc_Id],[iYear],[cSub_Id],[bIsDelete],[bClosing],[iModiPeri],[dSubSysUsed],[cUser_Id],[dSubOriDate],[HideInfoID]) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')";
        /// <summary>
        /// Transact-SQLite 语句
        /// </summary>
        private static readonly string SQLite_Insert_UA_HoldAuth_Hide = "Insert Into [UA_HoldAuth_hide] ([cAcc_Id],[iYear],[cUser_Id],[cAuth_Id],[iIsUser],[HideInfoID]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')";
        /// <summary>
        /// Transact-SQLite 语句
        /// </summary>
        private static readonly string SQLite_Insert_UA_Log_Hide = " Insert Into [UA_Log_Hide] ([cAcc_Id] ,[cSub_Id],[cUser_Id],[cAuth_Id],[cStation] ,[dInTime],[dOutTime],[iLogId],[iyear] ,[Success] ,[HideInfoID]) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')";
        /// <summary>
        /// Transact-SQLite 语句
        /// </summary>
        private static readonly string SQLite_Insert_UA_Period_Hide = "Insert Into[UA_Period_Hide] ([cAcc_Id],[iYear],[iId],[dBegin],[dEnd],[bIsDelete],[HideInfoID]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
        /// <summary>
        /// Transact-SQLite 语句
        /// </summary>
        private static readonly string SQLite_Select_HideDataPath = "SELECT [Name] ,[Path],[cAcc_Id] From  HideDataPath WHERE HideId =  '{0}'";
        /// <summary>
        /// Transact-SQLite 语句
        /// </summary>
        private static readonly string SQLite_Select_UA_BackupPlans_Hide = "SELECT  [cPlan_Id], [cAcc_Id], [iYear] From  UA_BackupPlans_Hide WHERE HideId =  '{0}'";
        /// <summary>
        /// Transact-SQLite 语句
        /// </summary>
        private static readonly string SQLite_Select_HideInfo = "SELECT cAcc_Id ,cAcc_Name,cAcc_live,Id FROM HideInfo where IsDelete=0";
        /// <summary>
        /// Transact-SQLite 语句
        /// </summary>
        private static readonly string SQLite_Select_MaxhideId = "SELECT  Max(ID) FROM HideInfo";
        /// <summary>
        /// Transact-SQLite 语句
        /// </summary>
        private static readonly string SQLite_Select_UA_Account_Hide = "SELECT iSysID, cAcc_Id, cAcc_Name, cAcc_Path, iYear, iMonth, cAcc_Master, cCurCode, cCurName, cUnitName, cUnitAbbre, cUnitAddr, cUnitZap, cUnitTel, cUnitFax, cUnitEMail, cUnitTaxNo, cUnitLP, cFinKind, cFinType, cEntType, cTradeKind FROM UA_Account_Hide   WHERE HideInfoID =  '{0}'";
        /// <summary>
        /// Transact-SQLite 语句
        /// </summary>
        private static readonly string SQLite_Select_UA_Account_sub_Hide = "SELECT cAcc_Id, iYear, cSub_Id, bIsDelete, bClosing, iModiperi, dSubSysUsed FROM UA_Account_sub_Hide WHERE HideInfoID =  '{0}'";
        /// <summary>
        /// Transact-SQLite 语句
        /// </summary>
        private static readonly string SQLite_Select_UA_HoldAuth_Hide = "SELECT cAcc_Id, iYear, cUser_Id, cAuth_Id FROM UA_HoldAuth_Hide WHERE HideInfoID = '{0}'";
        /// <summary>
        /// Transact-SQLite 语句
        /// </summary>
        private static readonly string SQLite_Select_UA_Log_Hide = "\tSELECT cAcc_Id, cSub_Id, cUser_Id, cAuth_Id, cStation, dInTime, dOutTime, iLogId, iYear, Success FROM UA_Log_Hide   WHERE HideInfoID =  '{0}'";
        /// <summary>
        /// Transact-SQLite 语句
        /// </summary>
        private static readonly string SQLite_Select_UA_Period_Hide = "SELECT cAcc_Id, iYear, iId,dBegin, dEnd, bIsDelete FROM UA_Period_Hide  WHERE HideInfoID =  '{0}'";
        /// <summary>
        /// Transact-SQLite 语句
        /// </summary>
        private static readonly string SQLite_Update_HideInfo = "update [HideInfo] set [IsDelete]='1' where ID='{0}' ";
        /// <summary>
        /// Transact-SQLite 语句
        /// </summary>
        private static readonly string SQLite_Update_HideInfo_Path = " update  [HideInfo] set [Data_path] ='{0}' where id={1}";
        /// <summary>
        /// Transact-SQLite 语句
        /// </summary>
        private static readonly string SQLite_Select_cAcc_Path = "SELECT [cAcc_Path] FROM UA_Account_Hide  Where HideInfoID='{0}'";
        /// <summary>
        /// Transact-SQLite 语句
		/// 查询以分离的数据库路
		/// </summary>
        private static readonly string SQLite_Select_HideInfo_Path = " SELECT   [Data_path] FROM [HideInfo] where id='{0}'";
        /// <summary>
        /// Transact-SQLite 语句
        /// 更新操作状态
        /// </summary>
        private static readonly string SQLite_Update_HideInfo_Live = "update  [HideInfo] set [cAcc_live] ='{0}' where id='{1}'"; 
        #endregion

        #region  获得帐套列表

        /// <summary>
        /// 获得帐套列表
        /// </summary>
        /// <returns></returns>
        public static List<UA_AccountItem> GetUA_AccountItem()
        {
            SysDataLog.log.Info("正在获取获得帐套列表!");
            //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在获取获得帐套列表..");
            UA_AccountItem oim;
            List<UA_AccountItem> accountitem = new List<UA_AccountItem>();
            using (SQLiteDataReader rdr = SQLiteHelper_Baet.ExecuteReader(SetString.SqliteConn, SQLite_Select_HideInfo, CommandType.Text))
            {
                //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在获取获得以隐藏帐套列表..");
                SysDataLog.log.Info("正在获取获得以隐藏帐套列表!");
                while (rdr.Read())
                {
                    oim = new UA_AccountItem(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), true, rdr.GetInt32(3).ToString());
                    accountitem.Add(oim);
                }
                //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "获得隐藏帐套列表...查询结束！");
                SysDataLog.log.Info("获得隐藏帐套列表...查询结束！");
            }
            StringBuilder SB = new StringBuilder(SQL_Select_UA_Account);
            SB.Append(" where 1=1");
            foreach (UA_AccountItem ua in accountitem)
            {
                SB.Append(" And cAcc_Id<>'" + ua.CAcc_Id + "' ");
            }
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SetString.SQLConn, CommandType.Text, SB.ToString(), null))
            {
                //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在获取获得帐套列表.操作结束.");
                SysDataLog.log.Info("正在获取获得帐套列表操作结束!");
                while (rdr.Read())
                {
                    oim = new UA_AccountItem(rdr.GetString(0), rdr.GetString(1), "0", false);
                    accountitem.Add(oim);
                }
                //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在获取获得以系统帐套列表..");
                SysDataLog.log.Info("正在获取获得以系统帐套列表!");
            }
            return accountitem;
        }
        #endregion  获得帐套列表

        #region  获得帐套列表

        /// <summary>
        /// 获得帐套列表
        /// </summary>
        /// <returns></returns>
        public static void GetUA_AccountItem(ref CheckedListBox cklb)
        {
            cklb.Items.Clear();
            SysDataLog.log.Info("正在获取获得帐套列表!");
            //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在获取获得帐套列表..");
         
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SetString.SQLConn, CommandType.Text, SQL_Select_UA_Account, null))
            {
                while (rdr.Read())
                {
                    cklb.Items.Add("[" + rdr.GetString(0) + "] " + rdr.GetString(1));
                }
                //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在获取获得以系统帐套列表..");
                SysDataLog.log.Info("正在获取获得以系统帐套列表!");
            }
        }
        #endregion  获得帐套列表

        #region 更新操作状态
        private static bool UpHideLive(string cAcc_live,string Hide_Id)
        {
            //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在更新操作状态！更新为:"+Hide_Id);
        	SysDataLog.log.Info("正在更新操作状态！将 Hide_Id=“"+Hide_Id+"”的数据更新为:"+Hide_Id);
        	if(SQLiteHelper_Baet.ExecuteNonQuery(SetString.SqliteConn, string.Format(SQLite_Update_HideInfo_Live, new object[] {  cAcc_live, Hide_Id}), CommandType.Text)== 1)
        	{       		
            //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "更新操作状态！更新为:"+Hide_Id  +"  成功！");
        		return true;
        	}
        return false;
        }
        
        #endregion 更新操作状态
                
        #region 隐藏操作

        #region 正在检测帐套是否正被使用

        /// <summary>
        /// 正在检测帐套是否正被使用
        /// </summary>
        /// <param name="cAcc_Id">帐套ID</param>
        /// <returns></returns>
        public static bool test_task(string cAcc_Id)
        {
            SysDataLog.log.Info("正在检测帐套是否正被使用!");
            if ("0" != SqlHelper.ExecuteScalar(SetString.SQLConn, CommandType.Text, string.Format(SQL_Select_UA_Task_count, cAcc_Id), null).ToString())
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 登陆隐藏
        /// <summary>
        /// 登陆隐藏
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="cAcc_Name"></param>
        /// <param name="cAcc_live"></param>
        /// <param name="NianDu"></param>
        /// <param name="Data_path"></param>
        /// <returns></returns>
        public static int hide_data_login(string cAcc_Id, string cAcc_Name, string cAcc_live, string NianDu, string Data_path)
        {
            SysDataLog.log.Info("正在执行登陆隐藏。。。。cAcc_Id="+cAcc_Id+" cAcc_Name="+cAcc_Name+" cAcc_live="+cAcc_live+" NianDu="+NianDu+" Data_path="+Data_path);
            int HideId = 0;
           // if (addHideinfo(cAcc_Id, cAcc_Name, cAcc_live, NianDu, Data_path) == 1)
            if (addHideinfo(cAcc_Id, cAcc_Name, "", NianDu, Data_path) == 1)
            {
                SysDataLog.log.Info("初始化sqlite事务。。。。");
                using (System.Data.SQLite.SQLiteConnection sqliteconn = new System.Data.SQLite.SQLiteConnection(HongHu.SetString.SqliteConn))
                {
                    sqliteconn.Open();
                    HongHu.SqlDal.sqlitetrans = sqliteconn.BeginTransaction();
                    HideId = int.Parse(SQLiteHelper_Baet.ExecuteScalar(SetString.SqliteConn, SQLite_Select_MaxhideId, CommandType.Text).ToString());
                    if (HideId != 0)
                    {
                        if (((addHideholdauth(cAcc_Id, HideId) && addPeriod(cAcc_Id, HideId)) && Del_Pi_Ha(cAcc_Id)))
                        {
                            HongHu.SqlDal.sqlitetrans.Commit();
                            SysDataLog.log.Info("保存sqlite事务的更改。。。。");
                            UpHideLive(Runfs.Login.ToString(),HideId.ToString());
                            return HideId;
                        }
                    }
                    HongHu.SqlDal.sqlitetrans.Rollback(); // <-------------------
                    SysDataLog.log.Info("撤消sqlite事务的更改。。。。");
                    MessageBox.Show("系统发生未知错误！请联系开发人员..."); // <-------------------
                }
            }
            SysDataLog.log.Info("添加隐藏档案失败。。。。");
            return 0;
        }

        #endregion 登陆隐藏

        #region 系统隐藏

        /// <summary>
        /// 系统隐藏
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="cAcc_Name"></param>
        /// <param name="cAcc_live"></param>
        /// <param name="NianDu"></param>
        /// <param name="Data_path"></param>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool hide_data_system(string cAcc_Id, string cAcc_Name, string cAcc_live, string NianDu, string Data_path, int HideId)
        {
            SysDataLog.log.Info("初始化sqlite事务。。。。");
            using (System.Data.SQLite.SQLiteConnection sqliteconn = new System.Data.SQLite.SQLiteConnection(HongHu.SetString.SqliteConn))
            {
                sqliteconn.Open();
                HongHu.SqlDal.sqlitetrans = sqliteconn.BeginTransaction();
                if ((
                        (
                        addLog(cAcc_Id, HideId)
                        && addBackupPlans(cAcc_Id, HideId)
                        && addAccount_sub(cAcc_Id, HideId)
                        )

                    && addAccount(cAcc_Id, HideId))
                    && Del_Log_Account(cAcc_Id))
                {
                    HongHu.SqlDal.sqlitetrans.Commit();
                    SysDataLog.log.Info("保存sqlite事务的更改。。。。");                    
                    UpHideLive(Runfs.System.ToString(),HideId.ToString());
                    return true;
                }
                HongHu.SqlDal.sqlitetrans.Rollback(); // <-------------------
                SysDataLog.log.Info("撤消sqlite事务的更改。。。。");
                MessageBox.Show("系统发生未知错误！请联系开发人员..."); // <-------------------
                return false;
            }
        }
        #endregion 系统隐藏

        #region 分离隐藏
        /// <summary>
        /// 分离隐藏
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="cAcc_Name"></param>
        /// <param name="cAcc_live"></param>
        /// <param name="NianDu"></param>
        /// <param name="Data_path"></param>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool hide_data_Detach(string cAcc_Id, string cAcc_Name, string cAcc_live, string NianDu, string Data_path, string HideId)
        {
            return hide_Detach(cAcc_Id, HideId);
        }

        #endregion 分离隐藏

        #region  移动隐藏
        /// <summary>
        /// 移动隐藏
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="cAcc_Name"></param>
        /// <param name="cAcc_live"></param>
        /// <param name="NianDu"></param>
        /// <param name="Data_path"></param>
        /// <param name="HideId"></param>
        /// <param name="Movetarget"></param>
        /// <returns></returns>
        public static bool hide_data_move(string cAcc_Id, string cAcc_Name, string cAcc_live, string NianDu, string Data_path, string HideId, string Movetarget)
        {
            return Hide_move(cAcc_Id, HideId, Movetarget);
        }

        #endregion 移动隐藏

        #region 向隐藏数据库写入帐套主表 UA_Account表信息

        /// <summary>
        /// 向隐藏数据库写入帐套主表 UA_Account表信息
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool addAccount(string cAcc_Id, int HideId)
        {
            int relus = 0;
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SetString.SQLConn, CommandType.Text, string.Format(SQL_Select_UA_Account2, cAcc_Id), null))
            {
                while (rdr.Read())
                {
                    //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "向隐藏数据库写入帐套主表 UA_Account写入第" + (relus++) + "条信息");
                    SysDataLog.log.Info("向隐藏数据库写入帐套主表 UA_Account表信息!");
                    if (SQLiteHelper_Baet.ExecuteNonQuery(sqlitetrans, string.Format(SQLite_Insert_UA_Account_Hide, new object[] { 
                        rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2), rdr.GetValue(3), rdr.GetValue(4), rdr.GetValue(5), rdr.GetValue(6), rdr.GetValue(7), rdr.GetValue(8), rdr.GetValue(9), rdr.GetValue(10), rdr.GetValue(11), rdr.GetValue(12), rdr.GetValue(13), rdr.GetValue(14), rdr.GetValue(15), 
                        rdr.GetValue(0x10), rdr.GetValue(0x11), rdr.GetValue(0x12), rdr.GetValue(0x13), rdr.GetValue(20), rdr.GetValue(0x15), HideId
                     }), null) != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion 向隐藏数据库写入帐套主表 UA_Account表信息

        #region 向隐藏数据库写入UA_Account_sub表信息!

        /// <summary>
        /// 向隐藏数据库写入UA_Account_sub表信息!
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool addAccount_sub(string cAcc_Id, int HideId)
        {
            int relus = 0;
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SetString.SQLConn, CommandType.Text, string.Format(SQL_Select_Account_sub, cAcc_Id), null))
            {
                while (rdr.Read())
                {
                    //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "向隐藏数据库写入UA_Account_sub写入第" + (relus++) + "条信息");
                    SysDataLog.log.Info("向隐藏数据库写入UA_Account_sub表信息!");
                    if (SQLiteHelper_Baet.ExecuteNonQuery(
                        sqlitetrans,
                        string.Format(SQLite_Insert_UA_Account_Sub_Hide, new object[] { rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2), rdr.GetValue(3), rdr.GetValue(4), rdr.GetValue(5), rdr.GetValue(6), "", "", HideId }),
                       null
                        ) != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion 向隐藏数据库写入UA_Account_sub表信息!

        #region 向隐藏数据库写入添加插入权限信息

        /// <summary>
        /// 向隐藏数据库写入添加插入权限信息
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool addHideholdauth(string cAcc_Id, int HideId)
        {
            int relus = 0;
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SetString.SQLConn, CommandType.Text, string.Format(SQL_Select_UA_HoldAuth, cAcc_Id), null))
            {
                while (rdr.Read())
                {
                    //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "向隐藏数据库写入添加插入权限信息第" + (relus++) + "条");
                    SysDataLog.log.Info("添加插入权限信息!");
                    if (SQLiteHelper_Baet.ExecuteNonQuery(sqlitetrans, string.Format(SQLite_Insert_UA_HoldAuth_Hide, new object[] { rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2), rdr.GetValue(3), rdr.GetValue(4), HideId }), null) != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion 向隐藏数据库写入添加插入权限信息

        #region 添加隐藏档案

        /// <summary>
        /// 添加隐藏档案
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="cAcc_Name"></param>
        /// <param name="cAcc_live"></param>
        /// <param name="NianDu"></param>
        /// <param name="Data_path"></param>
        /// <returns></returns>
        public static int addHideinfo(string cAcc_Id, string cAcc_Name, string cAcc_live, string NianDu, string Data_path)
        {
            //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "添加隐藏档案!");
            SysDataLog.log.Info("添加隐藏档案!");
            return SQLiteHelper_Baet.ExecuteNonQuery(SetString.SqliteConn, string.Format(SQLite_Insert_HideInfo, new object[] { cAcc_Id, cAcc_Name, cAcc_live, NianDu, DateTime.Now, Data_path }), CommandType.Text);
        }

        #endregion 添加隐藏档案

        #region 向隐藏数据库写入添加插入日志 UA_Log!信息

        /// <summary>
        /// 向隐藏数据库写入添加插入日志 UA_Log!信息
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool addLog(string cAcc_Id, int HideId)
        {
            int relus = 0;
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SetString.SQLConn, CommandType.Text, string.Format(SQL_Select_UA_Log, cAcc_Id), null))
            {
                while (rdr.Read())
                {
                    //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "向隐藏数据库写入添加插入日志 UA_Log!信息第" + (relus++) + "条");
                    SysDataLog.log.Info("向隐藏数据库插入日志 UA_Log!");
                    if (SQLiteHelper_Baet.ExecuteNonQuery(sqlitetrans, string.Format(SQLite_Insert_UA_Log_Hide, new object[] { rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2), rdr.GetValue(3), rdr.GetValue(4), rdr.GetValue(5), rdr.GetValue(6), rdr.GetValue(7), rdr.GetValue(8), rdr.GetValue(9), HideId }), null) != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion 向隐藏数据库写入添加插入日志 UA_Log!信息

        #region 向隐藏数据库写入备份计划UA_BackupPlans!信息

        /// <summary>
        /// 向隐藏数据库写入备份计划UA_BackupPlans!信息
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool addBackupPlans(string cAcc_Id, int HideId)
        {
            int relus = 0;
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SetString.SQLConn, CommandType.Text, string.Format(SQL_Select_UA_BackupPlans, cAcc_Id), null))
            {
                while (rdr.Read())
                {
                    //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "向隐藏数据库写入备份计划UA_BackupPlans!信息第" + (relus++) + "条");
                    SysDataLog.log.Info("向隐藏数据库写入备份计划UA_BackupPlans!信息");
                    if (SQLiteHelper_Baet.ExecuteNonQuery(sqlitetrans, string.Format(SQLite_Insert_UA_BackupPlans_Hide, new object[] { rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2), HideId }), null) != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion 向隐藏数据库写入备份计划UA_BackupPlans!信息

        #region 向隐藏数据库写入添加插入添加插入会计期间信息

        /// <summary>
        /// 向隐藏数据库写入添加插入添加插入会计期间信息
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool addPeriod(string cAcc_Id, int HideId)
        {
            int relus = 0;
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SetString.SQLConn, CommandType.Text, string.Format(SQL_Select_UA_Period, cAcc_Id), null))
            {
                while (rdr.Read())
                {
                    //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "向隐藏数据库写入添加插入添加插入会计期间信息第" + (relus++) + "条");
                    SysDataLog.log.Info("添加插入会计期间 !");
                    if (SQLiteHelper_Baet.ExecuteNonQuery(sqlitetrans, string.Format(SQLite_Insert_UA_Period_Hide, new object[] { rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2), rdr.GetValue(3), rdr.GetValue(4), rdr.GetValue(5), HideId }), null) != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion 向隐藏数据库写入添加插入添加插入会计期间信息

        #region 删除日志_帐套主表及附表

        /// <summary>
        /// 删除日志_帐套主表及附表
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <returns></returns>
        public static bool Del_Log_Account(string cAcc_Id)
        {
            //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在删除日志_帐套主表及附表 cAcc_Id=" + cAcc_Id);
            SysDataLog.log.Info("删除日志_帐套主表及附表 cAcc_Id=" + cAcc_Id);
            try
            {
                if (SqlHelper.ExecuteNonQuery(SetString.SQLConn, CommandType.Text, string.Format(SQL_Delete_Log_Account, cAcc_Id), null) < 0)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                SysDataLog.log.Error("commandText", ex);
                return false;
            }
            return true;
        }
        #endregion 删除日志_帐套主表及附表

        #region 删除会计期间和用户权限信息及备份计划信息

        public static bool Del_Pi_Ha(string cAcc_Id)
        {
            //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "删除会计期间和用户权限信息及备份计划信息 cAcc_Id=" + cAcc_Id);
            SysDataLog.log.Info("删除会计期间和用户权限信息及备份计划信息 cAcc_Id=" + cAcc_Id);
            try
            {
                if (SqlHelper.ExecuteNonQuery(SetString.SQLConn, CommandType.Text, string.Format(SQL_Delete_Period_HoldAuth_BackupPlans, cAcc_Id), null) < 0)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                SysDataLog.log.Error("commandText", ex);
                return false;
            }
            return true;
        }

        #endregion 删除会计期间和用户权限信息

        #region 分离数据库

        /// <summary>
        /// 分离数据库
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool hide_Detach(string cAcc_Id, string HideId)
        {
            //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在执行分离数据库操作!");
            SysDataLog.log.Info("正在执行分离数据库操作");
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SetString.SQLConn, CommandType.Text, string.Format(SQL_Select_DataFile, cAcc_Id), null))
            {
                while (rdr.Read())
                {
                    //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在向隐藏数据库中插入分离数据库路径! " + rdr.GetValue(1));
                    SysDataLog.log.Info("正在向隐藏数据库中插入分离数据库路径! " + rdr.GetValue(1));
                    if (SQLiteHelper.ExecuteNonQuery(SetString.SqliteConn, string.Format(SQLite_Insert_HideDataPath, rdr.GetValue(0), rdr.GetValue(1), HideId, cAcc_Id), null) != 1)
                    {
                        //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "插入分离数据库路径失败！ 系统退出！ \n路径：" + rdr.GetValue(1));
                        SysDataLog.log.Info("插入分离数据库路径失败！ 系统退出！ \n路径：" + rdr.GetValue(1));
                        return false;
                    }
                    //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在分离数据库!" + rdr.GetValue(0));
                    SysDataLog.log.Info("正在分离数据库!" + rdr.GetValue(0));
                    SqlHelper.ExecuteNonQuery(SetString.SQLConn, CommandType.Text, string.Format(SQL_detach, rdr.GetValue(0)), new SqlParameter[0]);
                }
            }
            //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "分离数据库完成！");
            SysDataLog.log.Info("分离数据库完成！");                            
            UpHideLive(Runfs.Detach.ToString(),HideId.ToString());
            return true;
        }

        #endregion 分离数据库

        #region  移动数据
        /// <summary>
        /// 移动数据
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="HideId"></param>
        /// <param name="Movetarget"></param>
        /// <returns></returns>
        public static bool Hide_move(string cAcc_Id, string HideId, string Movetarget)
        {
            //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在准备移动数据文件夹！");
            SysDataLog.log.Info("正在准备移动数据文件夹！");

            string DemoPath = SQLiteHelper.ExecuteScalar(SetString.SqliteConn, string.Format(SQLite_Select_cAcc_Path, HideId), null).ToString() + "ZT" + cAcc_Id + "\\";
        	if ((Movetarget == null || Movetarget == ""))
            {
                Movetarget = Application.StartupPath +@"\DataBases";
            }
        	Movetarget+= "\\zt" + cAcc_Id+"\\"; 
            //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在检测数据文件夹:" + Movetarget);
            SysDataLog.log.Info("正在检测数据文件夹:" + Movetarget);
            if (new DirectoryInfo(Movetarget).Exists)
            {
                if (DialogResult.No == MessageBox.Show("目录以存在！是否覆盖？\n 选“是” 覆盖    选“否”自动改变目标文件夹", "警告！", MessageBoxButtons.YesNo))
                {
                    Movetarget += Guid.NewGuid().ToString("N");
                    SysDataLog.log.Info("移动数据文件夹！更改目标文件夹为："+Movetarget); 
                }
                {
                    SysDataLog.log.Info("移动数据文件夹！_覆盖原文件"); 
                }
            }
            try
            {
                //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在将数据文件夹" + DemoPath + "\n  移动到 :" + Movetarget);
                SysDataLog.log.Info("正在将数据文件夹" + DemoPath + "\n  移动到 :" + Movetarget);
                FileHelper.MoveFiles(DemoPath, Movetarget, true, true);
                //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在删除原将数据文件夹 ！\n路径：" + DemoPath );
                SysDataLog.log.Info("正在删除原将数据文件夹" + DemoPath);
                new DirectoryInfo(DemoPath).Delete();
            }
            catch (Exception ex)
            {
                SysDataLog.log.Error("移动文件时发生错误！！", ex);
                //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "移动文件到" + Movetarget + "时发生错误！  程序退出！");
                return false;
            }
            //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "移动成功！保存移动路径" + Movetarget);
            if (SQLiteHelper.ExecuteNonQuery(SetString.SqliteConn, string.Format(SQLite_Update_HideInfo_Path, Movetarget, HideId), null) != 1)
            {
                SysDataLog.log.Info("更新移动路径到" + Movetarget + "出错！");
                //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "更新移动路径到" + Movetarget + "出错！ 程序退出！");
                return false;
            }
            UpHideLive(Runfs.Move.ToString(),HideId.ToString());
            return true;
        }
        #endregion  移动数据

        #endregion 隐藏操作

        #region 显示操作

        #region 删除隐藏档案（自是更新是否删除字段）

        /// <summary>
        /// 删除隐藏档案（自是更新是否删除字段）
        /// </summary>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool Del_hidinfo(string HideId)
        {
            return (SQLiteHelper_Baet.ExecuteNonQuery(SetString.SqliteConn, string.Format(SQLite_Update_HideInfo, HideId), CommandType.Text) == 1);
        }

        #endregion 删除隐藏档案（自是更新是否删除字段）

        #region 显示数据分离隐藏

        /// <summary>
        /// 显示数据分离隐藏
        /// </summary>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool Show_Detach(string HideId)
        {
            return Show_databases(HideId);
        }
        #endregion 显示分离数据

        #region 显示数据移动隐藏

        /// <summary>
        /// 显示数据移动隐藏
        /// </summary>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool Show_Move(string HideId,string cAcc_Id)
        {
            return Rollback_Move(HideId, cAcc_Id);
        }
        #endregion 显示数据移动隐藏

        #region 倒回插入权限信息

        /// <summary>
        /// 倒回插入权限信息
        /// </summary>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool show_holdauth(string HideId)
        {
            int relus = 0;
            using (SQLiteDataReader rdr = SQLiteHelper_Baet.ExecuteReader(SetString.SqliteConn, string.Format(SQLite_Select_UA_HoldAuth_Hide, HideId), CommandType.Text))
            {
                while (rdr.Read())
                {
                    //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在倒回权限息第" + (relus++) + "条");
                    SysDataLog.log.Info("倒回插入权限信息!");
                    if (SqlHelper.ExecuteNonQuery(SetString.SQLConn, CommandType.Text, string.Format(SQL_Insert_UA_HoldAuth, new object[] { rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2), rdr.GetValue(3) }), null) != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion 倒回插入权限信息

        #region 显示隐藏日志和权限
        /// <summary>
        /// 显示隐藏日志和权限
        /// </summary>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool Show_Log(string HideId)
        {
            return (show_holdauth(HideId) && show_UA_Period(HideId));
        }

        #endregion 显示隐藏日志和权限

        #region 显示系统隐藏

        /// <summary>
        /// 显示系统隐藏
        /// </summary>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool Show_System(string HideId)
        {
            return ((
                show_UA_Account(HideId) 
                && show_UA_Log(HideId)
                && show_UA_BackupPlans(HideId)
                ) 
                && show_UA_Account_sub(HideId));
        }
        #endregion 显示系统隐藏

        #region 倒回帐套主表信息
        /// <summary>
        /// 倒回帐套主表信息
        /// </summary>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool show_UA_Account(string HideId)
        {
            using (SQLiteDataReader rdr = SQLiteHelper_Baet.ExecuteReader(SetString.SqliteConn, string.Format(SQLite_Select_UA_Account_Hide, HideId), CommandType.Text))
            {
                while (rdr.Read())
                {
                    //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "倒回帐套主表信息!");
                    SysDataLog.log.Info("倒回帐套主表信息!");
                    if (SqlHelper.ExecuteNonQuery(SetString.SQLConn, CommandType.Text, string.Format(SQL_Insert_UA_Account, new object[] { 
                        rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2), rdr.GetValue(3), rdr.GetValue(4), rdr.GetValue(5), rdr.GetValue(6), rdr.GetValue(7), rdr.GetValue(8), rdr.GetValue(9), rdr.GetValue(10), rdr.GetValue(11), rdr.GetValue(12), rdr.GetValue(13), rdr.GetValue(14), rdr.GetValue(15), 
                        rdr.GetValue(0x10), rdr.GetValue(0x11), rdr.GetValue(0x12), rdr.GetValue(0x13), rdr.GetValue(20), rdr.GetValue(0x15)
                     }), null) != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion 倒回帐套主表信息

        #region 倒回帐帐套子表信息
        /// <summary>
        /// 倒回帐帐套子表信息
        /// </summary>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool show_UA_Account_sub(string HideId)
        {
            int relus = 0;
            using (SQLiteDataReader rdr = SQLiteHelper_Baet.ExecuteReader(SetString.SqliteConn, string.Format(SQLite_Select_UA_Account_sub_Hide, HideId), CommandType.Text))
            {
                while (rdr.Read())
                {
                    //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在倒回帐帐套子表息第" + (relus++) + "条");
                    SysDataLog.log.Info("倒回帐帐套子表信息!");
                    if (SqlHelper.ExecuteNonQuery(SetString.SQLConn, CommandType.Text, string.Format(SQL_Insert_UA_Account_sub, new object[] { rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2), bool.Parse(rdr.GetString(3)) ? 1 : 0, bool.Parse(rdr.GetString(4)) ? 1 : 0, rdr.GetValue(5), rdr.GetString(6) }), null) != 1)
                    {
                        return false;
                    }
                }
            }
            UpHideLive(Runfs.Login.ToString(),HideId.ToString());
            return true;
        }

        #endregion 倒回帐帐套子表信息

        #region 倒回日志信息

        /// <summary>
        /// 倒回日志信息
        /// </summary>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool show_UA_Log(string HideId)
        {
            int relus = 0;
            using (SQLiteDataReader rdr = SQLiteHelper_Baet.ExecuteReader(SetString.SqliteConn, string.Format(SQLite_Select_UA_Log_Hide, HideId), CommandType.Text))
            {
                while (rdr.Read())
                {
                    //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在倒回日志信息第" + (relus++) + "条");
                    SysDataLog.log.Info("倒回日志信息!");
                    if (SqlHelper.ExecuteNonQuery(SetString.SQLConn, CommandType.Text, string.Format(SQL_Insert_UA_Log, new object[] { rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2), rdr.GetValue(3), rdr.GetValue(4), rdr.GetString(5), rdr.GetString(6), rdr.GetValue(7), rdr.GetValue(8), rdr.GetValue(9) }), null) != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion 倒回日志信息

        #region 倒回会计期间信息
        /// <summary>
        /// 倒回会计期间信息
        /// </summary>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool show_UA_Period(string HideId)
        {
            int relus = 0;
            using (SQLiteDataReader rdr = SQLiteHelper_Baet.ExecuteReader(SetString.SqliteConn, string.Format(SQLite_Select_UA_Period_Hide, HideId), CommandType.Text))
            {
                while (rdr.Read())
                {
                    //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在倒回会计期间信息第" + (relus++) + "条");
                    SysDataLog.log.Info("倒回会计期间信息!");
                    if (SqlHelper.ExecuteNonQuery(SetString.SQLConn,
                        CommandType.Text,
                        string.Format(
                         SQL_Insert_UA_Period, new object[] 
{ rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2), rdr.GetString(3), rdr.GetString(4), bool.Parse(rdr.GetString(5))?1:0
}), null) != 1)
                    {
                        return false;
                    }
                }
            }
            UpHideLive(Runfs.None.ToString(),HideId.ToString());
            return true;
        }

        #endregion 倒回会计期间信息
        
        #region 倒回备份计划表UA_BackupPlans

        /// <summary>
        /// 倒回备份计划表UA_BackupPlans
        /// </summary>
        /// <param name="HideId"></param>
        /// <returns> </returns>
        public static bool show_UA_BackupPlans(string HideId)
        {
            int relus = 0;
            using (SQLiteDataReader rdr = SQLiteHelper_Baet.ExecuteReader(SetString.SqliteConn, string.Format(SQLite_Select_UA_BackupPlans_Hide, HideId), CommandType.Text))
            {
                while (rdr.Read())
                {
                    //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "倒回备份计划表UA_BackupPlans 第" + (relus++) + "条");
                    SysDataLog.log.Info("倒回备份计划表UA_BackupPlans!");
                    if (SqlHelper.ExecuteNonQuery(SetString.SQLConn, CommandType.Text, string.Format(SQL_Insert_UA_BackupPlans, new object[] { rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2) }), null) != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion 倒回日志信息
        
        #region 附加数据库
        /// <summary>
        /// 附加数据库
        /// </summary>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool Show_databases(string HideId)
        {
            using (SQLiteDataReader rdr = SQLiteHelper_Baet.ExecuteReader(SetString.SqliteConn, string.Format(SQLite_Select_HideDataPath, HideId), CommandType.Text))
            {
                while (rdr.Read())
                {
                    string temppath = rdr.GetString(1).Substring(0, rdr.GetString(1).LastIndexOf(@"\"));
                    //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "附加数据库!" + rdr.GetValue(0));
                    SysDataLog.log.Info("附加数据库!" + rdr.GetValue(0));

                    if (!File.Exists(rdr.GetString(1)))
                    {
                        SysDataLog.log.Info("附加数据库!" + rdr.GetValue(0) + "失败！" + rdr.GetString(1));
                        //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "附加数据库!" + rdr.GetValue(0) + "失败！\n" + rdr.GetString(1));
                        return false;
                    }
                    if (!FileHelper.CheckOpen( rdr.GetString(1)))
                    {
                        SysDataLog.log.Info("附加数据库!" + rdr.GetValue(0) + "失败！" +  rdr.GetString(1)+ "文件正被使用中");
                        //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "附加数据库!" + rdr.GetValue(0) + "失败！\n"  +rdr.GetString(1) + "文件正被使用中");
                        return false;
                    }
                    //if (SqlHelper.ExecuteNonQuery(SetString.SQLConn, CommandType.Text, string.Format(SQL_fjdata, rdr.GetValue(0), rdr.GetValue(1), temppath + "\\ufdata.ldf"), null) != 1)
                    SqlHelper.ExecuteNonQuery(SetString.SQLConn, CommandType.Text, string.Format(SQL_fjdata, rdr.GetValue(0), rdr.GetValue(1), temppath + "\\ufdata.ldf"), null);
                    //{
                    //    return false;
                    //}
                }
            }
            SysDataLog.log.Info("附加数据库完成！！");
            //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "附加数据库完成！！");                                
            UpHideLive(Runfs.System.ToString(),HideId.ToString());
            return true;
        }
        #endregion 附加数据库

        #region 移回原数据位置
        /// <summary>
        /// 移回原数据位置
        /// </summary>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool Rollback_Move(string HideId, string cAcc_Id)
        {
            //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在准备移回原数据文件夹！");
            SysDataLog.log.Info("正在准备移回原数据文件夹！");
            string DemoPath = SQLiteHelper.ExecuteScalar(SetString.SqliteConn, string.Format(SQLite_Select_HideInfo_Path, HideId), null).ToString();
            string Movetarget = SQLiteHelper.ExecuteScalar(SetString.SqliteConn, string.Format(SQLite_Select_cAcc_Path, HideId), null).ToString() + "ZT" + cAcc_Id + "\\";
            if (new DirectoryInfo(Movetarget).Exists)
            {
                if (DialogResult.No == MessageBox.Show("目标目录以存在！是否取消操作？\n 选“是” 覆盖    选“否”取消操作", "警告！", MessageBoxButtons.YesNo))
                {
                    SysDataLog.log.Info("移动数据文件夹！目标文件夹以存在_目标文件夹:" + Movetarget+"  取消显示操作！！");
                }
                {
                    SysDataLog.log.Info("移回原数据文件夹！目标文件夹以存在_覆盖原文件！目标文件夹:" + Movetarget );
                }
            }
            //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在移回数据文件夹" + DemoPath + "\n  移回 :" + Movetarget);
            SysDataLog.log.Info("正在移回数据文件夹" + DemoPath + "\n  移回 :" + Movetarget);
            try
            {
                FileHelper.MoveFiles(DemoPath, Movetarget, true, true);
                //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "正在删除隐藏数据文件夹" + DemoPath);
                SysDataLog.log.Info("正在删除隐藏数据文件夹" + DemoPath);
                new DirectoryInfo(DemoPath).Delete();
            }
            catch (Exception ex)
            {
                SysDataLog.log.Error("移回原数据位置！！", ex);
                //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "移回文件到" + Movetarget + "时发生错误！  程序退出！");
                MessageBox.Show(ex.ToString());
                return false;
            }
            //HongHu.UI.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "移动成功！保存移动路径" + Movetarget);
            //if (SQLiteHelper.ExecuteNonQuery(SetString.SqliteConn, string.Format(SQLite_Update_HideInfo_Path, Movetarget, HideId), null) != 1)
            //{
            //    SysDataLog.log.Info("更新移动路径到" + Movetarget + "出错！");
            //    HongHu.Form1.backgroundWorkerfrom_loading.ReportProgress(0, "更新移动路径到" + Movetarget + "出错！ 程序退出！");
            //    return false;
            //}
            UpHideLive(Runfs.Detach.ToString(),HideId.ToString());
            return true;
        }
        #endregion 移回原数据位置

        #endregion 显示操作
    }
}
