using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HongHu;
using HongHu.DAL;
using HongHu.DBUtility;
//using HongHu.DAL.DBUtility;

namespace HongHu
{

    public class SqlDal
    {
        #region ����
        /// <summary>
        /// SQLite����
        /// </summary>
        public static  SQLiteTransaction sqlitetrans;
        /// <summary>
        /// Transact-SQL ���
        /// ɾ�������� Log_Account ��UA_Account_sub��UA_Account�е���Ϣ
        /// </summary>
        private static readonly string SQL_Delete_Log_Account = "use ufsystem \n begin transaction \n  DELETE FROM UA_Log WHERE cAcc_Id = '{0}'\n  DELETE FROM UA_Account_sub    WHERE cAcc_Id = '{0}'\n  DELETE FROM UA_Account  WHERE cAcc_Id = '{0}'\n if @@error<>0  --- @@error���ǰһ�� Transact-SQL ���ִ��û�д����򷵻� 0��\n   begin \n rollback transaction  --��������������ĳ����������������������޸ġ�\n   return \n end \n commit transaction  --��־һ���ɹ�����Ľ�����";
        // private static readonly string SQL_Delete_Period_HoldAuth_BackupPlans = "use ufsystem \n begin transaction \n  DELETE FROM UA_HoldAuth WHERE cAcc_Id = '{0}'\n  DELETE FROM UA_Period   WHERE cAcc_Id = '{0}'\n if @@error<>0  --- @@error���ǰһ�� Transact-SQL ���ִ��û�д����򷵻� 0��\n   begin \n rollback transaction  --��������������ĳ����������������������޸ġ�\n   return \n end \n commit transaction  --��־һ���ɹ�����Ľ�����";

        /// <summary>
        /// Transact-SQL ���
        /// ɾ�������� UA_HoldAuth ��UA_Period��UA_BackupPlans�е���Ϣ
        /// </summary>
        private static readonly string SQL_Delete_Period_HoldAuth_BackupPlans = "use ufsystem \n begin transaction \n  DELETE FROM UA_HoldAuth WHERE cAcc_Id = '{0}'\n  DELETE FROM UA_Period   WHERE cAcc_Id = '{0}'\n  DELETE FROM UA_BackupPlans   WHERE cAcc_Id = '{0}'\n if @@error<>0  --- @@error���ǰһ�� Transact-SQL ���ִ��û�д����򷵻� 0��\n   begin \n rollback transaction  --��������������ĳ����������������������޸ġ�\n   return \n end \n commit transaction  --��־һ���ɹ�����Ľ�����";
        /// <summary>
        /// Transact-SQL ���
        /// �������ݿ�
        /// </summary>
        private static readonly string SQL_detach = "use master declare @dbName nvarchar(50) \n set @dbName='{0}' --���ݿ��� \n declare   @spid   nvarchar(20)  \n declare   cur_lock   cursor   for  \n SELECT spID FROM master..sysprocesses WHERE  dbid= db_id(@dbName) \n open   cur_lock  \n fetch   cur_lock      into   @spid  \n while   @@fetch_status=0  \n     begin      \n     exec( 'kill '+@spid )  \n     fetch   Next From cur_lock into @spid \n     end      \n close   cur_lock \n deallocate   cur_lock \n exec sp_detach_db @dbName, N'true'";
        /// <summary>
        /// Transact-SQL ���
        /// �������ݿ�
        /// </summary>
        private static readonly string SQL_fjdata = "use master EXEC sp_attach_db '{0}' , '{1}' , '{2}' ";
        /// <summary>
        /// Transact-SQL ���
        /// �����������Ϣ����UA_Account����
        /// </summary>
        private static readonly string SQL_Insert_UA_Account = "use ufsystem INSERT INTO UA_Account(iSysID, cAcc_Id, cAcc_Name, cAcc_Path, iYear, iMonth, cAcc_Master, cCurCode, cCurName, cUnitName, cUnitAbbre, cUnitAddr, cUnitZap, cUnitTel, cUnitFax, cUnitEMail, cUnitTaxNo, cUnitLP, cFinKind, cFinType, cEntType, cTradeKind) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}')";
        /// <summary>
        /// Transact-SQL ���
        /// �����������Ϣ����UA_Account_sub����
        /// </summary>
        private static readonly string SQL_Insert_UA_Account_sub = "use ufsystem INSERT INTO UA_Account_sub(cAcc_Id, iYear, cSub_Id, bIsDelete, bClosing, iModiperi, dSubSysUsed)   values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
        /// <summary>
        /// Transact-SQL ���
        /// �����������Ϣ����UA_HoldAuth����
        /// </summary>
        private static readonly string SQL_Insert_UA_HoldAuth = "use ufsystem INSERT INTO UA_HoldAuth(cAcc_Id, iYear, cUser_Id, cAuth_Id) values('{0}','{1}','{2}','{3}')";
        /// <summary>
        /// Transact-SQL ���
        /// �����������Ϣ����UA_Log����
        /// </summary>
        private static readonly string SQL_Insert_UA_Log = "use ufsystem \tINSERT INTO UA_Log(cAcc_Id, cSub_Id, cUser_Id, cAuth_Id, cStation, dInTime, dOutTime, iLogId, iYear, Success)   values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')";
        /// <summary>
        /// Transact-SQL ���
        /// �����׻���ڼ����UA_Period����
        /// </summary>
        private static readonly string SQL_Insert_UA_Period = "use ufsystem INSERT INTO UA_Period(cAcc_Id, iYear, iId,dBegin, dEnd, bIsDelete)  values('{0}','{1}','{2}','{3}','{4}','{5}')";
        /// <summary>
        /// Transact-SQL ���
        /// �����������Ϣ����UA_BackupPlans����
        /// </summary>
        private static readonly string SQL_Insert_UA_BackupPlans = "use ufsystem INSERT INTO UA_BackupPlans( [cPlan_Id], [cAcc_Id], [iYear])  values('{0}','{1}','{2}')";
        /// <summary>
        /// Transact-SQL ���
        /// ��ѯ��������
        /// </summary>
        private static readonly string SQL_Select_UA_Account = "use ufsystem SELECT cAcc_Id ,cAcc_Name FROM UA_Account";
        /// <summary>
        /// Transact-SQL ���
        /// ��ѯ���ڲ������׵�����
        /// </summary>
        private static readonly string SQL_Select_UA_Task_count = "use ufsystem select count(*) as Count from ua_task where cAcc_Id in ('{0}')";
        /// <summary>
        /// Transact-SQL ���
        /// ��ѯ������UA_Account_sub���е�����
        /// </summary>
        private static readonly string SQL_Select_Account_sub = "use ufsystem \tSELECT cAcc_Id, iYear, cSub_Id, bIsDelete, bClosing, iModiperi, dSubSysUsed FROM UA_Account_sub WHERE cAcc_Id = '{0}'";
        /// <summary>
        /// Transact-SQL ���
        /// ��ѯ������UA_Account���е�����
        /// </summary>
        private static readonly string SQL_Select_UA_Account2 = "use ufsystem \tSELECT iSysID, cAcc_Id, cAcc_Name, cAcc_Path, iYear, iMonth, cAcc_Master, cCurCode, cCurName, cUnitName, cUnitAbbre, cUnitAddr, cUnitZap, cUnitTel, cUnitFax, cUnitEMail, cUnitTaxNo, cUnitLP, cFinKind, cFinType, cEntType, cTradeKind FROM UA_Account WHERE cAcc_Id = '{0}'";
        /// <summary>
        /// Transact-SQL ���
        /// ��ѯ�����������ݿ�����Ƽ�·��
        /// </summary>
        private static readonly string SQL_Select_DataFile = "SELECT [name],[filename]  FROM [master]..[sysdatabases]where name like 'Ufdata[_]{0}%'";
        /// <summary>
        /// Transact-SQL ���
        /// ��ѯ������UA_HoldAuth��Ȩ�ޣ����е�����
        /// </summary>
        private static readonly string SQL_Select_UA_HoldAuth = "use ufsystem SELECT  cAcc_Id, iYear, cUser_Id, cAuth_Id,1 FROM UA_HoldAuth WHERE cAcc_Id = '{0}'";
        /// <summary>
        /// Transact-SQL ���
        /// ��ѯ������UA_Log����־�����е�����
        /// </summary>
        private static readonly string SQL_Select_UA_Log = "use ufsystem \tSELECT cAcc_Id, cSub_Id, cUser_Id, cAuth_Id, cStation, dInTime, dOutTime, iLogId, iYear, Success FROM UA_Log WHERE cAcc_Id = '{0}'";
        /// <summary>
        /// Transact-SQL ���
        /// ��ѯ������UA_Period������ڼ䣩���е�����
        /// </summary>
        private static readonly string SQL_Select_UA_Period = "use ufsystem \tSELECT cAcc_Id, iYear, iId,dBegin, dEnd, bIsDelete FROM UA_Period WHERE cAcc_Id = '{0}'";
        /// <summary>
        /// Transact-SQL ���
        /// ��ѯ������UA_BackupPlans�����ݼƻ��ӱ��е�����
        /// </summary>
        private static readonly string SQL_Select_UA_BackupPlans = "use ufsystem \tSELECT [cPlan_Id], [cAcc_Id], [iYear] ---, [bOnline]--���õ��ֶ� \n FROM [UFSystem].[dbo].[UA_BackupPlans] WHERE cAcc_Id = '{0}'";
        /// <summary>
        /// Transact-SQLite ���
        /// �����׵��ӱ��ݼƻ������������ݿ��UA_BackupPlans����
        /// </summary>
        private static readonly string SQLite_Insert_UA_BackupPlans_Hide = "insert into [UA_BackupPlans_Hide] ([cPlan_Id] ,[cAcc_Id],[iYear],[HideId]) values('{0}','{1}','{2}','{3}')";
        /// <summary>
        /// Transact-SQLite ���
        /// �����׵����ݿ�·�������������ݿ��HideDataPath����
        /// </summary>
        private static readonly string SQLite_Insert_HideDataPath = "insert into [HideDataPath] ([Name] ,[Path],[HideId],[cAcc_Id]) values('{0}','{1}','{2}','{3}')";
        /// <summary>
        /// Transact-SQLite ���
        /// ���������Ϣ
        /// </summary>
        private static readonly string SQLite_Insert_HideInfo = "insert into  hideinfo(cAcc_Id,cAcc_Name,cAcc_live,NianDu,dDate,Data_path,IsDelete) values('{0}','{1}','{2}','{3}','{4}','{5}',0)";
        /// <summary>
        /// Transact-SQLite ���
        /// ��������Ϣ�����������ݿ��UA_Account_Hide����
        /// </summary>
        private static readonly string SQLite_Insert_UA_Account_Hide = " Insert Into [UA_Account_Hide] ([iSysID],[cAcc_Id],[cAcc_Name],[cAcc_Path],[iYear],[iMonth],[cAcc_Master],[cCurCode],[cCurName],[cUnitName],[cUnitAbbre],[cUnitAddr],[cUnitZap],[cUnitTel],[cUnitFax],[cUnitEMail],[cUnitTaxNo],[cUnitLP],[cFinKind],[cFinType],[cEntType],[cTradeKind],[HideInfoId])values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')";
        /// <summary>
        /// Transact-SQLite ���
        /// </summary>
        private static readonly string SQLite_Insert_UA_Account_Sub_Hide = "insert into [UA_Account_Sub_Hide] ([cAcc_Id],[iYear],[cSub_Id],[bIsDelete],[bClosing],[iModiPeri],[dSubSysUsed],[cUser_Id],[dSubOriDate],[HideInfoID]) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')";
        /// <summary>
        /// Transact-SQLite ���
        /// </summary>
        private static readonly string SQLite_Insert_UA_HoldAuth_Hide = "Insert Into [UA_HoldAuth_hide] ([cAcc_Id],[iYear],[cUser_Id],[cAuth_Id],[iIsUser],[HideInfoID]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')";
        /// <summary>
        /// Transact-SQLite ���
        /// </summary>
        private static readonly string SQLite_Insert_UA_Log_Hide = " Insert Into [UA_Log_Hide] ([cAcc_Id] ,[cSub_Id],[cUser_Id],[cAuth_Id],[cStation] ,[dInTime],[dOutTime],[iLogId],[iyear] ,[Success] ,[HideInfoID]) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')";
        /// <summary>
        /// Transact-SQLite ���
        /// </summary>
        private static readonly string SQLite_Insert_UA_Period_Hide = "Insert Into[UA_Period_Hide] ([cAcc_Id],[iYear],[iId],[dBegin],[dEnd],[bIsDelete],[HideInfoID]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
        /// <summary>
        /// Transact-SQLite ���
        /// </summary>
        private static readonly string SQLite_Select_HideDataPath = "SELECT [Name] ,[Path],[cAcc_Id] From  HideDataPath WHERE HideId =  '{0}'";
        /// <summary>
        /// Transact-SQLite ���
        /// </summary>
        private static readonly string SQLite_Select_UA_BackupPlans_Hide = "SELECT  [cPlan_Id], [cAcc_Id], [iYear] From  UA_BackupPlans_Hide WHERE HideId =  '{0}'";
        /// <summary>
        /// Transact-SQLite ���
        /// </summary>
        private static readonly string SQLite_Select_HideInfo = "SELECT cAcc_Id ,cAcc_Name,cAcc_live,Id FROM HideInfo where IsDelete=0";
        /// <summary>
        /// Transact-SQLite ���
        /// </summary>
        private static readonly string SQLite_Select_MaxhideId = "SELECT  Max(ID) FROM HideInfo";
        /// <summary>
        /// Transact-SQLite ���
        /// </summary>
        private static readonly string SQLite_Select_UA_Account_Hide = "SELECT iSysID, cAcc_Id, cAcc_Name, cAcc_Path, iYear, iMonth, cAcc_Master, cCurCode, cCurName, cUnitName, cUnitAbbre, cUnitAddr, cUnitZap, cUnitTel, cUnitFax, cUnitEMail, cUnitTaxNo, cUnitLP, cFinKind, cFinType, cEntType, cTradeKind FROM UA_Account_Hide   WHERE HideInfoID =  '{0}'";
        /// <summary>
        /// Transact-SQLite ���
        /// </summary>
        private static readonly string SQLite_Select_UA_Account_sub_Hide = "SELECT cAcc_Id, iYear, cSub_Id, bIsDelete, bClosing, iModiperi, dSubSysUsed FROM UA_Account_sub_Hide WHERE HideInfoID =  '{0}'";
        /// <summary>
        /// Transact-SQLite ���
        /// </summary>
        private static readonly string SQLite_Select_UA_HoldAuth_Hide = "SELECT cAcc_Id, iYear, cUser_Id, cAuth_Id FROM UA_HoldAuth_Hide WHERE HideInfoID = '{0}'";
        /// <summary>
        /// Transact-SQLite ���
        /// </summary>
        private static readonly string SQLite_Select_UA_Log_Hide = "\tSELECT cAcc_Id, cSub_Id, cUser_Id, cAuth_Id, cStation, dInTime, dOutTime, iLogId, iYear, Success FROM UA_Log_Hide   WHERE HideInfoID =  '{0}'";
        /// <summary>
        /// Transact-SQLite ���
        /// </summary>
        private static readonly string SQLite_Select_UA_Period_Hide = "SELECT cAcc_Id, iYear, iId,dBegin, dEnd, bIsDelete FROM UA_Period_Hide  WHERE HideInfoID =  '{0}'";
        /// <summary>
        /// Transact-SQLite ���
        /// </summary>
        private static readonly string SQLite_Update_HideInfo = "update [HideInfo] set [IsDelete]='1' where ID='{0}' ";
        /// <summary>
        /// Transact-SQLite ���
        /// </summary>
        private static readonly string SQLite_Update_HideInfo_Path = " update  [HideInfo] set [Data_path] ='{0}' where id={1}";
        /// <summary>
        /// Transact-SQLite ���
        /// </summary>
        private static readonly string SQLite_Select_cAcc_Path = "SELECT [cAcc_Path] FROM UA_Account_Hide  Where HideInfoID='{0}'";
        /// <summary>
        /// Transact-SQLite ���
        /// ��ѯ�Է�������ݿ�·
        /// </summary>
        private static readonly string SQLite_Select_HideInfo_Path = " SELECT   [Data_path] FROM [HideInfo] where id='{0}'";
        /// <summary>
        /// Transact-SQLite ���
        /// ���²���״̬
        /// </summary>
        private static readonly string SQLite_Update_HideInfo_Live = "update  [HideInfo] set [cAcc_live] ='{0}' where id='{1}'";
        #endregion

        #region  ��������б�

        /// <summary>
        /// ��������б�
        /// </summary>
        /// <returns></returns>
        public static List<UA_AccountItem> GetUA_AccountItem()
        {
            SysDataLog.log.Info("���ڻ�ȡ��������б�!");
            HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "���ڻ�ȡ��������б�..");
            UA_AccountItem oim;
            List<UA_AccountItem> accountitem = new List<UA_AccountItem>();
            using (SQLiteDataReader rdr = SQLiteHelper_Baet.ExecuteReader(SetString.SqliteConn, SQLite_Select_HideInfo, CommandType.Text))
            {
                HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "���ڻ�ȡ��������������б�..");
                SysDataLog.log.Info("���ڻ�ȡ��������������б�!");
                while (rdr.Read())
                {
                    oim = new UA_AccountItem(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), true, rdr.GetInt32(3).ToString());
                    accountitem.Add(oim);
                }
                HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "������������б�...��ѯ������");
               // SysDataLog.log.Info("������������б�...��ѯ������");
            }
            StringBuilder SB = new StringBuilder(SQL_Select_UA_Account);
            SB.Append(" where 1=1");
            foreach (UA_AccountItem ua in accountitem)
            {
                SB.Append(" And cAcc_Id<>'" + ua.CAcc_Id + "' ");
            }
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SetString.SqlConn, CommandType.Text, SB.ToString(), null))
            {
                HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "���ڻ�ȡ�����ϵͳ�����б�.");
                SysDataLog.log.Info("���ڻ�ȡ�����ϵͳ�����б�!");
                while (rdr.Read())
                {
                    oim = new UA_AccountItem(rdr.GetString(0), rdr.GetString(1), "0", false);
                    accountitem.Add(oim);
                }
                HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "��ȡ�����ϵͳ�����б����..");
               // SysDataLog.log.Info("���ڻ�ȡ��������б�.��������!");
            }
            return accountitem;
        }
        #endregion  ��������б�

        #region ���²���״̬
        private static bool UpHideLive(string cAcc_live, string Hide_Id)
        {
            HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "���ڸ��²���״̬������Ϊ:" + Hide_Id);
            SysDataLog.log.Info("���²���״̬ �� Hide_Id=��" + Hide_Id + "�������ݸ���Ϊ:" + cAcc_live);
            if (SQLiteHelper_Baet.ExecuteNonQuery(SetString.SqliteConn, string.Format(SQLite_Update_HideInfo_Live, new object[] { cAcc_live, Hide_Id }), CommandType.Text) == 1)
            {
                HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "���²���״̬������Ϊ:" + cAcc_live + "  �ɹ���");
                return true;
            }
            return false;
        }

        #endregion ���²���״̬

        #region ���ز���

        #region ���ڼ�������Ƿ�����ʹ��

        /// <summary>
        /// ���ڼ�������Ƿ�����ʹ��
        /// </summary>
        /// <param name="cAcc_Id">����ID</param>
        /// <returns></returns>
        public static bool test_task(string cAcc_Id)
        {
            SysDataLog.log.Info("���ڼ�������Ƿ�����ʹ��!");
            if ("0" != SqlHelper.ExecuteScalar(SetString.SqlConn, CommandType.Text, string.Format(SQL_Select_UA_Task_count, cAcc_Id), null).ToString())
            {
                return false;
            }
            return true;
        }

        #endregion

        #region ��½����
        /// <summary>
        /// ��½����
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="cAcc_Name"></param>
        /// <param name="cAcc_live"></param>
        /// <param name="NianDu"></param>
        /// <param name="Data_path"></param>
        /// <returns></returns>
        public static int hide_data_login(string cAcc_Id, string cAcc_Name, string cAcc_live, string NianDu, string Data_path)
        {
            SysDataLog.log.Info("����ִ�е�½���ء�������cAcc_Id=" + cAcc_Id + " cAcc_Name=" + cAcc_Name + " cAcc_live=" + cAcc_live + " NianDu=" + NianDu + " Data_path=" + Data_path);
            int HideId = 0;
            // if (addHideinfo(cAcc_Id, cAcc_Name, cAcc_live, NianDu, Data_path) == 1)
            if (addHideinfo(cAcc_Id, cAcc_Name, "", NianDu, Data_path) == 1)
            {
                SysDataLog.log.Info("��ʼ��sqlite���񡣡�����");
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
                            SysDataLog.log.Info("����sqlite����ĸ��ġ�������");
                            UpHideLive(Runfs.Login.ToString(), HideId.ToString());
                            return HideId;
                        }
                    }
                    HongHu.SqlDal.sqlitetrans.Rollback(); // <-------------------
                    SysDataLog.log.Info("����sqlite����ĸ��ġ�������");
                    MessageBox.Show("ϵͳ����δ֪��������ϵ������Ա..."); // <-------------------
                }
            }
            SysDataLog.log.Info("������ص���ʧ�ܡ�������");
            return 0;
        }

        #endregion ��½����

        #region ϵͳ����

        /// <summary>
        /// ϵͳ����
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
            SysDataLog.log.Info("��ʼ��sqlite���񡣡�����");
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
                    SysDataLog.log.Info("����sqlite����ĸ��ġ�������");
                    UpHideLive(Runfs.System.ToString(), HideId.ToString());
                    return true;
                }
                HongHu.SqlDal.sqlitetrans.Rollback(); // <-------------------
                SysDataLog.log.Info("����sqlite����ĸ��ġ�������");
                MessageBox.Show("ϵͳ����δ֪��������ϵ������Ա..."); // <-------------------
                return false;
            }
        }
        #endregion ϵͳ����

        #region ��������
        /// <summary>
        /// ��������
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

        #endregion ��������

        #region  �ƶ�����
        /// <summary>
        /// �ƶ�����
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

        #endregion �ƶ�����

        #region ���������ݿ�д���������� UA_Account����Ϣ

        /// <summary>
        /// ���������ݿ�д���������� UA_Account����Ϣ
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool addAccount(string cAcc_Id, int HideId)
        {
            int relus = 0;
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SetString.SqlConn, CommandType.Text, string.Format(SQL_Select_UA_Account2, cAcc_Id), null))
            {
                while (rdr.Read())
                {
                    HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "���������ݿ�д���������� UA_Accountд���" + (relus++) + "����Ϣ");
                    SysDataLog.log.Info("���������ݿ�д���������� UA_Account����Ϣ!");
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
        #endregion ���������ݿ�д���������� UA_Account����Ϣ

        #region ���������ݿ�д��UA_Account_sub����Ϣ!

        /// <summary>
        /// ���������ݿ�д��UA_Account_sub����Ϣ!
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool addAccount_sub(string cAcc_Id, int HideId)
        {
            int relus = 0;
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SetString.SqlConn, CommandType.Text, string.Format(SQL_Select_Account_sub, cAcc_Id), null))
            {
                while (rdr.Read())
                {
                    HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "���������ݿ�д��UA_Account_subд���" + (relus++) + "����Ϣ");
                    SysDataLog.log.Info("���������ݿ�д��UA_Account_sub����Ϣ!");
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

        #endregion ���������ݿ�д��UA_Account_sub����Ϣ!

        #region ���������ݿ�д��Ȩ����Ϣ

        /// <summary>
        /// ���������ݿ�д��Ȩ����Ϣ
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool addHideholdauth(string cAcc_Id, int HideId)
        {
            int relus = 0;
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SetString.SqlConn, CommandType.Text, string.Format(SQL_Select_UA_HoldAuth, cAcc_Id), null))
            {
                while (rdr.Read())
                {
                    HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "���������ݿ�д��Ȩ����Ϣ��" + (relus++) + "��");
                    SysDataLog.log.Info("��Ӳ���Ȩ����Ϣ!");
                    if (SQLiteHelper_Baet.ExecuteNonQuery(sqlitetrans, string.Format(SQLite_Insert_UA_HoldAuth_Hide, new object[] { rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2), rdr.GetValue(3), rdr.GetValue(4), HideId }), null) != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion ���������ݿ�д��Ȩ����Ϣ

        #region ������ص���

        /// <summary>
        /// ������ص���
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="cAcc_Name"></param>
        /// <param name="cAcc_live"></param>
        /// <param name="NianDu"></param>
        /// <param name="Data_path"></param>
        /// <returns></returns>
        public static int addHideinfo(string cAcc_Id, string cAcc_Name, string cAcc_live, string NianDu, string Data_path)
        {
            HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "������ص���!");
            SysDataLog.log.Info("������ص���!");
            return SQLiteHelper_Baet.ExecuteNonQuery(SetString.SqliteConn, string.Format(SQLite_Insert_HideInfo, new object[] { cAcc_Id, cAcc_Name, cAcc_live, NianDu, DateTime.Now, Data_path }), CommandType.Text);
        }

        #endregion ������ص���

        #region ���������ݿ�д����־ UA_Log!��Ϣ

        /// <summary>
        /// ���������ݿ�д����־ UA_Log!��Ϣ
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool addLog(string cAcc_Id, int HideId)
        {
            int relus = 0;
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SetString.SqlConn, CommandType.Text, string.Format(SQL_Select_UA_Log, cAcc_Id), null))
            {
                while (rdr.Read())
                {
                    HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "���������ݿ�д����־ UA_Log!��Ϣ��" + (relus++) + "��");
                    SysDataLog.log.Info("���������ݿ������־ UA_Log!");
                    if (SQLiteHelper_Baet.ExecuteNonQuery(sqlitetrans, string.Format(SQLite_Insert_UA_Log_Hide, new object[] { rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2), rdr.GetValue(3), rdr.GetValue(4), rdr.GetValue(5), rdr.GetValue(6), rdr.GetValue(7), rdr.GetValue(8), rdr.GetValue(9), HideId }), null) != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion ���������ݿ�д����־ UA_Log!��Ϣ

        #region ���������ݿ�д�뱸�ݼƻ�UA_BackupPlans!��Ϣ

        /// <summary>
        /// ���������ݿ�д�뱸�ݼƻ�UA_BackupPlans!��Ϣ
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool addBackupPlans(string cAcc_Id, int HideId)
        {
            int relus = 0;
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SetString.SqlConn, CommandType.Text, string.Format(SQL_Select_UA_BackupPlans, cAcc_Id), null))
            {
                while (rdr.Read())
                {
                    HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "���������ݿ�д�뱸�ݼƻ�UA_BackupPlans!��Ϣ��" + (relus++) + "��");
                    SysDataLog.log.Info("���������ݿ�д�뱸�ݼƻ�UA_BackupPlans!��Ϣ");
                    if (SQLiteHelper_Baet.ExecuteNonQuery(sqlitetrans, string.Format(SQLite_Insert_UA_BackupPlans_Hide, new object[] { rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2), HideId }), null) != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion ���������ݿ�д�뱸�ݼƻ�UA_BackupPlans!��Ϣ

        #region ���������ݿ�д�����ڼ���Ϣ

        /// <summary>
        /// ���������ݿ�д�����ڼ���Ϣ
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool addPeriod(string cAcc_Id, int HideId)
        {
            int relus = 0;
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SetString.SqlConn, CommandType.Text, string.Format(SQL_Select_UA_Period, cAcc_Id), null))
            {
                while (rdr.Read())
                {
                    HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "���������ݿ�д�����ڼ���Ϣ��" + (relus++) + "��");
                    SysDataLog.log.Info("���������ݿ�д�����ڼ���Ϣ��" + (relus++) + "��");
                    if (SQLiteHelper_Baet.ExecuteNonQuery(sqlitetrans, string.Format(SQLite_Insert_UA_Period_Hide, new object[] { rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2), rdr.GetValue(3), rdr.GetValue(4), rdr.GetValue(5), HideId }), null) != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion ���������ݿ�д�����ڼ���Ϣ

        #region ɾ����־_������������

        /// <summary>
        /// ɾ����־_������������
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <returns></returns>
        public static bool Del_Log_Account(string cAcc_Id)
        {
            HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "����ɾ����־_������������ cAcc_Id=" + cAcc_Id);
            SysDataLog.log.Info("ɾ����־_������������ cAcc_Id=" + cAcc_Id);
            try
            {
                if (SqlHelper.ExecuteNonQuery(SetString.SqlConn, CommandType.Text, string.Format(SQL_Delete_Log_Account, cAcc_Id), null) < 0)
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
        #endregion ɾ����־_������������

        #region ɾ������ڼ���û�Ȩ����Ϣ�����ݼƻ���Ϣ

        public static bool Del_Pi_Ha(string cAcc_Id)
        {
            HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "ɾ������ڼ���û�Ȩ����Ϣ�����ݼƻ���Ϣ cAcc_Id=" + cAcc_Id);
            SysDataLog.log.Info("ɾ������ڼ���û�Ȩ����Ϣ�����ݼƻ���Ϣ cAcc_Id=" + cAcc_Id);
            try
            {
                if (SqlHelper.ExecuteNonQuery(SetString.SqlConn, CommandType.Text, string.Format(SQL_Delete_Period_HoldAuth_BackupPlans, cAcc_Id), null) < 0)
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

        #endregion ɾ������ڼ���û�Ȩ����Ϣ

        #region �������ݿ�

        /// <summary>
        /// �������ݿ�
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool hide_Detach(string cAcc_Id, string HideId)
        {
            HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "����ִ�з������ݿ����!");
            SysDataLog.log.Info("����ִ�з������ݿ����");
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SetString.SqlConn, CommandType.Text, string.Format(SQL_Select_DataFile, cAcc_Id), null))
            {
                while (rdr.Read())
                {
                    HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "�������������ݿ��в���������ݿ�·��! " + rdr.GetValue(1));
                    SysDataLog.log.Info("�������������ݿ��в���������ݿ�·��! " + rdr.GetValue(1));
                    if (SQLiteHelper.ExecuteNonQuery(SetString.SqliteConn, string.Format(SQLite_Insert_HideDataPath, rdr.GetValue(0), rdr.GetValue(1), HideId, cAcc_Id), null) != 1)
                    {
                        HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "����������ݿ�·��ʧ�ܣ� ϵͳ�˳��� \n·����" + rdr.GetValue(1));
                        SysDataLog.log.Info("����������ݿ�·��ʧ�ܣ� ϵͳ�˳��� \n·����" + rdr.GetValue(1));
                        return false;
                    }
                    HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "���ڷ������ݿ�!" + rdr.GetValue(0));
                    SysDataLog.log.Info("���ڷ������ݿ�!" + rdr.GetValue(0));
                    if (SqlHelper.ExecuteNonQuery(SetString.SqlConn, CommandType.Text, string.Format(SQL_detach, rdr.GetValue(0)), new SqlParameter[0]) == null)
                    {
                        MessageBox.Show("�������ݿ�" + rdr.GetValue(0) +" ʧ��!");
                        SysDataLog.log.Error("�������ݿ�ʧ��!" + rdr.GetValue(0));
                    }
                }
            }
            HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "�������ݿ���ɣ�");
            SysDataLog.log.Info("�������ݿ���ɣ�");
            UpHideLive(Runfs.Detach.ToString(), HideId.ToString());
            return true;
        }

        #endregion �������ݿ�

        #region  �ƶ�����
        /// <summary>
        /// �ƶ�����
        /// </summary>
        /// <param name="cAcc_Id"></param>
        /// <param name="HideId"></param>
        /// <param name="Movetarget"></param>
        /// <returns></returns>
        public static bool Hide_move(string cAcc_Id, string HideId, string Movetarget)
        {
            HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "����׼���ƶ������ļ��У�");
            SysDataLog.log.Info("����׼���ƶ������ļ��У�");

            string DemoPath = SQLiteHelper.ExecuteScalar(SetString.SqliteConn, string.Format(SQLite_Select_cAcc_Path, HideId), null).ToString() + "ZT" + cAcc_Id + "\\";
            if ((Movetarget == null || Movetarget == ""))
            {
                Movetarget = Application.StartupPath + @"\DataBases";
            }
            Movetarget += "\\zt" + cAcc_Id + "\\";
            HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "���ڼ�������ļ���:" + Movetarget);
            SysDataLog.log.Info("���ڼ�������ļ���:" + Movetarget);
            if (new DirectoryInfo(Movetarget).Exists)
            {
                if (DialogResult.No == MessageBox.Show("Ŀ¼�Դ��ڣ��Ƿ񸲸ǣ�\n ѡ���ǡ� ����    ѡ�����Զ��ı�Ŀ���ļ���", "���棡", MessageBoxButtons.YesNo))
                {
                    Movetarget += Guid.NewGuid().ToString("N");
                    SysDataLog.log.Info("�ƶ������ļ��У�����Ŀ���ļ���Ϊ��" + Movetarget);
                }
                else
                {
                    SysDataLog.log.Info("�ƶ������ļ��У�_����ԭ�ļ�");
                }
            }
            try
            {
                HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "���ڽ������ļ���" + DemoPath + "\n  �ƶ��� :" + Movetarget);
                SysDataLog.log.Info("���ڽ������ļ���" + DemoPath + "\n  �ƶ��� :" + Movetarget);
                FileHelper.MoveFiles(DemoPath, Movetarget, true, true);
                HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "����ɾ��ԭ�������ļ��� ��\n·����" + DemoPath);
                SysDataLog.log.Info("����ɾ��ԭ�������ļ���" + DemoPath);
                new DirectoryInfo(DemoPath).Delete();
            }
            catch (Exception ex)
            {
                SysDataLog.log.Error("�ƶ��ļ�ʱ�������󣡣�", ex);
                HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "�ƶ��ļ���" + Movetarget + "ʱ��������  �����˳���");
                return false;
            }
            HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "�ƶ��ɹ��������ƶ�·��" + Movetarget);
            if (SQLiteHelper.ExecuteNonQuery(SetString.SqliteConn, string.Format(SQLite_Update_HideInfo_Path, Movetarget, HideId), null) != 1)
            {
                SysDataLog.log.Info("�����ƶ�·����" + Movetarget + "����");
                HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "�����ƶ�·����" + Movetarget + "���� �����˳���");
                return false;
            }
            UpHideLive(Runfs.Move.ToString(), HideId.ToString());
            return true;
        }
        #endregion  �ƶ�����

        #endregion ���ز���

        #region ��ʾ����

        #region ɾ�����ص��������Ǹ����Ƿ�ɾ���ֶΣ�

        /// <summary>
        /// ɾ�����ص��������Ǹ����Ƿ�ɾ���ֶΣ�
        /// </summary>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool Del_hidinfo(string HideId)
        {
            return (SQLiteHelper_Baet.ExecuteNonQuery(SetString.SqliteConn, string.Format(SQLite_Update_HideInfo, HideId), CommandType.Text) == 1);
        }

        #endregion ɾ�����ص��������Ǹ����Ƿ�ɾ���ֶΣ�

        #region ��ʾ���ݷ�������

        /// <summary>
        /// ��ʾ���ݷ�������
        /// </summary>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool Show_Detach(string HideId)
        {
            return Show_databases(HideId);
        }
        #endregion ��ʾ��������

        #region ��ʾ�����ƶ�����

        /// <summary>
        /// ��ʾ�����ƶ�����
        /// </summary>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool Show_Move(string HideId, string cAcc_Id)
        {
            return Rollback_Move(HideId, cAcc_Id);
        }
        #endregion ��ʾ�����ƶ�����

        #region ���ز���Ȩ����Ϣ

        /// <summary>
        /// ���ز���Ȩ����Ϣ
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
                    HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "���ڵ���Ȩ��Ϣ��" + (relus++) + "��");
                    SysDataLog.log.Info("���ز���Ȩ����Ϣ!");
                    if (SqlHelper.ExecuteNonQuery(SetString.SqlConn, CommandType.Text, string.Format(SQL_Insert_UA_HoldAuth, new object[] { rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2), rdr.GetValue(3) }), null) != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion ���ز���Ȩ����Ϣ

        #region ��ʾ������־��Ȩ��
        /// <summary>
        /// ��ʾ������־��Ȩ��
        /// </summary>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool Show_Log(string HideId)
        {
            return (show_holdauth(HideId) && show_UA_Period(HideId));
        }

        #endregion ��ʾ������־��Ȩ��

        #region ��ʾϵͳ����

        /// <summary>
        /// ��ʾϵͳ����
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
        #endregion ��ʾϵͳ����

        #region ��������������Ϣ
        /// <summary>
        /// ��������������Ϣ
        /// </summary>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool show_UA_Account(string HideId)
        {
            using (SQLiteDataReader rdr = SQLiteHelper_Baet.ExecuteReader(SetString.SqliteConn, string.Format(SQLite_Select_UA_Account_Hide, HideId), CommandType.Text))
            {
                while (rdr.Read())
                {
                    HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "��������������Ϣ!");
                    SysDataLog.log.Info("��������������Ϣ!");
                    if (SqlHelper.ExecuteNonQuery(SetString.SqlConn, CommandType.Text, string.Format(SQL_Insert_UA_Account, new object[] { 
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

        #endregion ��������������Ϣ

        #region �����������ӱ���Ϣ
        /// <summary>
        /// �����������ӱ���Ϣ
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
                    HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "���ڵ����������ӱ�Ϣ��" + (relus++) + "��");
                    SysDataLog.log.Info("�����������ӱ���Ϣ!");
                    if (SqlHelper.ExecuteNonQuery(SetString.SqlConn, CommandType.Text, string.Format(SQL_Insert_UA_Account_sub, new object[] { rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2), bool.Parse(rdr.GetString(3)) ? 1 : 0, bool.Parse(rdr.GetString(4)) ? 1 : 0, rdr.GetValue(5), rdr.GetString(6) }), null) != 1)
                    {
                        return false;
                    }
                }
            }
            UpHideLive(Runfs.Login.ToString(), HideId.ToString());
            return true;
        }

        #endregion �����������ӱ���Ϣ

        #region ������־��Ϣ

        /// <summary>
        /// ������־��Ϣ
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
                    HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "���ڵ�����־��Ϣ��" + (relus++) + "��");
                    SysDataLog.log.Info("������־��Ϣ!");
                    if (SqlHelper.ExecuteNonQuery(SetString.SqlConn, CommandType.Text, string.Format(SQL_Insert_UA_Log, new object[] { rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2), rdr.GetValue(3), rdr.GetValue(4), rdr.GetString(5), rdr.GetString(6), rdr.GetValue(7), rdr.GetValue(8), rdr.GetValue(9) }), null) != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion ������־��Ϣ

        #region ���ػ���ڼ���Ϣ
        /// <summary>
        /// ���ػ���ڼ���Ϣ
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
                    HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "���ڵ��ػ���ڼ���Ϣ��" + (relus++) + "��");
                    SysDataLog.log.Info("���ػ���ڼ���Ϣ!");
                    if (SqlHelper.ExecuteNonQuery(SetString.SqlConn,
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
            UpHideLive(Runfs.None.ToString(), HideId.ToString());
            return true;
        }

        #endregion ���ػ���ڼ���Ϣ

        #region ���ر��ݼƻ���UA_BackupPlans

        /// <summary>
        /// ���ر��ݼƻ���UA_BackupPlans
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
                    HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "���ر��ݼƻ���UA_BackupPlans ��" + (relus++) + "��");
                    SysDataLog.log.Info("���ر��ݼƻ���UA_BackupPlans!");
                    if (SqlHelper.ExecuteNonQuery(SetString.SqlConn, CommandType.Text, string.Format(SQL_Insert_UA_BackupPlans, new object[] { rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2) }), null) != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion ������־��Ϣ

        #region �������ݿ�
        /// <summary>
        /// �������ݿ�
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
                    HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "�������ݿ�!" + rdr.GetValue(0));
                    SysDataLog.log.Info("�������ݿ�!" + rdr.GetValue(0));

                    if (!File.Exists(rdr.GetString(1)))
                    {
                        SysDataLog.log.Info("�������ݿ�!" + rdr.GetValue(0) + "ʧ�ܣ�" + rdr.GetString(1));
                        HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "�������ݿ�!" + rdr.GetValue(0) + "ʧ�ܣ�\n" + rdr.GetString(1));
                        return false;
                    }
                    if (!FileHelper.CheckOpen(rdr.GetString(1)))
                    {
                        SysDataLog.log.Info("�������ݿ�!" + rdr.GetValue(0) + "ʧ�ܣ�" + rdr.GetString(1) + "�ļ�����ʹ����");
                        HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "�������ݿ�!" + rdr.GetValue(0) + "ʧ�ܣ�\n" + rdr.GetString(1) + "�ļ�����ʹ����");
                        return false;
                    }
                    //if (SqlHelper.ExecuteNonQuery(SetString.SqlConn, CommandType.Text, string.Format(SQL_fjdata, rdr.GetValue(0), rdr.GetValue(1), temppath + "\\ufdata.ldf"), null) != 1)
                    SqlHelper.ExecuteNonQuery(SetString.SqlConn, CommandType.Text, string.Format(SQL_fjdata, rdr.GetValue(0), rdr.GetValue(1), temppath + "\\ufdata.ldf"), null);
                    //{
                    //    return false;
                    //}
                }
            }
            SysDataLog.log.Info("�������ݿ���ɣ���");
            HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "�������ݿ���ɣ���");
            UpHideLive(Runfs.System.ToString(), HideId.ToString());
            return true;
        }
        #endregion �������ݿ�

        #region �ƻ�ԭ����λ��
        /// <summary>
        /// �ƻ�ԭ����λ��
        /// </summary>
        /// <param name="HideId"></param>
        /// <returns></returns>
        public static bool Rollback_Move(string HideId, string cAcc_Id)
        {
            HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "����׼���ƻ�ԭ�����ļ��У�");
            SysDataLog.log.Info("����׼���ƻ�ԭ�����ļ��У�");
            string DemoPath = SQLiteHelper.ExecuteScalar(SetString.SqliteConn, string.Format(SQLite_Select_HideInfo_Path, HideId), null).ToString();
            string Movetarget = SQLiteHelper.ExecuteScalar(SetString.SqliteConn, string.Format(SQLite_Select_cAcc_Path, HideId), null).ToString() + "ZT" + cAcc_Id + "\\";
            if (new DirectoryInfo(Movetarget).Exists)
            {
                if (DialogResult.No == MessageBox.Show("Ŀ��Ŀ¼�Դ��ڣ��Ƿ�ȡ��������\n ѡ���ǡ� ����    ѡ����ȡ������", "���棡", MessageBoxButtons.YesNo))
                {
                    SysDataLog.log.Info("�ƶ������ļ��У�Ŀ���ļ����Դ���_Ŀ���ļ���:" + Movetarget + "  ȡ����ʾ��������");
                }
                {
                    SysDataLog.log.Info("�ƻ�ԭ�����ļ��У�Ŀ���ļ����Դ���_����ԭ�ļ���Ŀ���ļ���:" + Movetarget);
                }
            }
            HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "�����ƻ������ļ���" + DemoPath + "\n  �ƻ� :" + Movetarget);
            SysDataLog.log.Info("�����ƻ������ļ���" + DemoPath + "\n  �ƻ� :" + Movetarget);
            try
            {
                FileHelper.MoveFiles(DemoPath, Movetarget, true, true);
                HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "����ɾ�����������ļ���" + DemoPath);
                SysDataLog.log.Info("����ɾ�����������ļ���" + DemoPath);
                new DirectoryInfo(DemoPath).Delete();
            }
            catch (Exception ex)
            {
                SysDataLog.log.Error("�ƻ�ԭ����λ�ã���", ex);
                HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "�ƻ��ļ���" + Movetarget + "ʱ��������  �����˳���");
                MessageBox.Show(ex.ToString());
                return false;
            }
            HongHu.FormMoveRun.backgroundWorkerfrom_loading.ReportProgress(0, "�ƶ��ɹ��������ƶ�·��" + Movetarget);
            //if (SQLiteHelper.ExecuteNonQuery(SetString.SqliteConn, string.Format(SQLite_Update_HideInfo_Path, Movetarget, HideId), null) != 1)
            //{
            //    SysDataLog.log.Info("�����ƶ�·����" + Movetarget + "����");
            //    HongHu.Form1.backgroundWorkerfrom_loading.ReportProgress(0, "�����ƶ�·����" + Movetarget + "���� �����˳���");
            //    return false;
            //}
            UpHideLive(Runfs.Detach.ToString(), HideId.ToString());
            return true;
        }
        #endregion �ƻ�ԭ����λ��

        #endregion ��ʾ����
    }
}
