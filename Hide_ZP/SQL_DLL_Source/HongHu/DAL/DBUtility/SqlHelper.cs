using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Data.Sql;
using System.IO;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic;
using System.Text;

namespace HongHu.DAL.DBUtility
{

    /// <summary>
    /// SqlHelper����ר���ṩ������û����ڸ����ܡ��������������ϰ��sql���ݲ���
    /// </summary>
    public abstract class SqlHelper
    {

        //���ݿ������ַ���
        // public static string ConnectionStringLocalTransaction = "";
        //public static readonly string ConnectionStringLocalTransaction = "";// ConfigurationManager.ConnectionStrings["SQLConnString1"].ConnectionString;
        //public static readonly string ConnectionStringInventoryDistributedTransaction = ConfigurationManager.ConnectionStrings["SQLConnString2"].ConnectionString;
        //public static readonly string ConnectionStringOrderDistributedTransaction = ConfigurationManager.ConnectionStrings["SQLConnString3"].ConnectionString;
        //public static readonly string ConnectionStringProfile = ConfigurationManager.ConnectionStrings["SQLProfileConnString"].ConnectionString;
        private const string pgturn = "pgturn";
        // ���ڻ��������HASH��
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        public const string SQL_SELECT_UFDATA = "select [name] from master.dbo.sysdatabases where [name] LIKE 'UFDATA_[0987654321][0987654321][0987654321]_20[0987654321][0987654321]' ";
        //public static String IsConn = false;

        /// <summary>
        ///  ����sql ����
        ///   ���ӳɹ�����null,ʧ�ܷ��ش�����Ϣ
        /// </summary>
        /// <remarks>
        ///  ����:  
        ///  TestConnStr("sqlServer","master","sa","")
        /// </remarks>
        /// <param name="DBServer">������</param>
        /// <param name="DBName">���ӵ������ݿ�</param>
        /// <param name="DBUser">�û���</param>
        /// <param name="DBPass">����</param>
        /// <returns></returns>
        public static string TestConnStr(string DBServer, string DBName, string DBUser, string DBPass)
        {
            return TestConnStr(ConnStrFormat(DBServer, DBName, DBUser, DBPass));

        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="SqlConnStr">connStr</param>
        /// <returns></returns>
        public static string TestConnStr(string SqlConnStr)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = SqlConnStr + ";Connection Timeout=1";
            try
            {
                connection.Open();
                connection.Close();
                return null;
            }
            catch (SqlException exception1)
            {
                return exception1.Message;
            }
        }
        /// <summary>
        /// ����ConnStr
        /// </summary>
        /// <param name="DBServer">������</param>
        /// <param name="DBName">���ӵ������ݿ�</param>
        /// <param name="DBUser">�û���</param>
        /// <param name="DBPass">����</param>
        /// <returns></returns>
        public static string ConnStrFormat(string DBServer, string DBName, string DBUser, string DBPass)
        {
            string str = "Data Source=" + DBServer + ";Initial catalog=" + DBName + ";";
            //SSPI:
            //    str = "Server=" + DBServer + ";DataBase=" + DBName + ";Integrated Security=SSPI";
            //WIN:
            //     str = str + "Integrated Security=SSPI;";
            //SQL:
            str = (str + "Integrated Security=False;User ID=" + DBUser) + ";Password=" + DBPass + ";";

            return str;


        }

        /// <summary>
        /// ö��sql ʵ����Ϣ
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSqlServer()
        {
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            return instance.GetDataSources().DefaultView.ToTable(true, "ServerName");
        }

        /// <summary>
        ///  �������ӵ����ݿ��ü������ִ��һ��sql������������ݼ���
        /// </summary>
        /// <param name="connectionString">һ����Ч�������ַ���</param>
        /// <param name="commandType">��������(�洢����, �ı�, �ȵ�)</param>
        /// <param name="commandText">�洢�������ƻ���sql�������</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>ִ��������Ӱ�������</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            //System.Windows.Forms.MessageBox.Show(cmdText);
            //return 0;
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// �����е����ݿ�����ִ��һ��sql������������ݼ���
        /// </summary>
        /// <param name="conn">һ�����е����ݿ�����</param>
        /// <param name="commandType">��������(�洢����, �ı�, �ȵ�)</param>
        /// <param name="commandText">�洢�������ƻ���sql�������</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>ִ��������Ӱ�������</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        ///ʹ�����е�SQL����ִ��һ��sql������������ݼ���
        /// </summary>
        /// <remarks>
        ///����:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">һ�����е�����</param>
        /// <param name="commandType">��������(�洢����, �ı�, �ȵ�)</param>
        /// <param name="commandText">�洢�������ƻ���sql�������</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>ִ��������Ӱ�������</returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            int val;
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            try
            {
                 val = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                ProjectData.SetProjectError(ex);
                throw new ApplicationException("ִ��SQL�ű�ʧ�ܡ�" + ex.Message);
                // this.ErrMsg = "ִ��SQL�ű�ʧ�ܡ�" + exception.Message;
                SysDataLog.log.Error("", ex);

            }
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// ��ִ�е����ݿ�����ִ��һ���������ݼ���sql����
        /// </summary>
        /// <remarks>
        /// ����:  
        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">һ����Ч�������ַ���</param>
        /// <param name="commandType">��������(�洢����, �ı�, �ȵ�)</param>
        /// <param name="commandText">�洢�������ƻ���sql�������</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>��������Ķ�ȡ��</returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            //����һ��SqlCommand����
            SqlCommand cmd = new SqlCommand();
            //����һ��SqlConnection����
            SqlConnection conn = new SqlConnection(connectionString);

            //������������һ��try/catch�ṹִ��sql�ı�����/�洢���̣���Ϊ��������������һ���쳣����Ҫ�ر����ӣ���Ϊû�ж�ȡ�����ڣ�
            //���commandBehaviour.CloseConnection �Ͳ���ִ��
            try
            {
                //���� PrepareCommand �������� SqlCommand �������ò���
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                //���� SqlCommand  �� ExecuteReader ����
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //�������
                cmd.Parameters.Clear();
                return reader;
            }
            catch (SqlException ex)
            {
                //�ر����ӣ��׳��쳣
                conn.Close();
                SysDataLog.log.Error("", ex);
                //throw;
            }
            return null;
        }

        /// <summary>
        /// ��ָ�������ݿ������ַ���ִ��һ���������һ�����ݼ��ĵ�һ��
        /// </summary>
        /// <remarks>
        ///����:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        ///<param name="connectionString">һ����Ч�������ַ���</param>
        /// <param name="commandType">��������(�洢����, �ı�, �ȵ�)</param>
        /// <param name="commandText">�洢�������ƻ���sql�������</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>�� Convert.To{Type}������ת��Ϊ��Ҫ�� </returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// ��ָ�������ݿ�����ִ��һ���������һ�����ݼ��ĵ�һ��
        /// </summary>
        /// <remarks>
        /// ����:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">һ�����ڵ����ݿ�����</param>
        /// <param name="commandType">��������(�洢����, �ı�, �ȵ�)</param>
        /// <param name="commandText">�洢�������ƻ���sql�������</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>�� Convert.To{Type}������ת��Ϊ��Ҫ�� </returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// ������������ӵ�����
        /// </summary>
        /// <param name="cacheKey">��ӵ�����ı���</param>
        /// <param name="cmdParms">һ����Ҫ��ӵ������sql��������</param>
        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// �һỺ���������
        /// </summary>
        /// <param name="cacheKey">�����һز����Ĺؼ���</param>
        /// <returns>����Ĳ�������</returns>
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// ׼��ִ��һ������
        /// </summary>
        /// <param name="cmd">sql����</param>
        /// <param name="conn">Sql����</param>
        /// <param name="trans">Sql����</param>
        /// <param name="cmdType">������������ �洢���̻����ı�</param>
        /// <param name="cmdText">�����ı�,���磺Select * from Products</param>
        /// <param name="cmdParms">ִ������Ĳ���</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        /// <summary>
        /// ִ��ͨ�÷�ҳ�洢�������ָ����DataSet
        /// </summary>
        /// <param name="SqlConnection">ָ��������</param>
        /// <param name="selectlist">Ҫ��ѯ���ֶ� ���� Select</param>
        /// <param name="tbName">Ҫ��ѯ�ı�</param>
        /// <param name="pagesize">ÿҳ��ʾ������</param>
        /// <param name="pageindex">ָ����ǰ�ڼ�ҳ</param>
        /// <param name="whatNotIn">�����ֶ���</param>
        /// <param name="sort">ָ������ ���߽���('asc' or 'desc')�ɿ�</param>
        /// <param name="where">ָ����ѯ������ �ɿ�</param>
        /// <param name="ds">ָ������DataSet</param>
        public static void getpgturn(SqlConnection connection, string selectlist, string tbName, int pagesize, int pageindex, string whatNotIn, string sort, string where, ref DataSet ds)
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter[] Parameters = GetpgturnParameters();
            Parameters[0].Value = selectlist;
            Parameters[1].Value = tbName;
            Parameters[2].Value = pagesize;
            Parameters[3].Value = pageindex;
            Parameters[4].Value = whatNotIn;
            Parameters[5].Value = sort;
            Parameters[6].Value = where;
            PrepareCommand(cmd, connection, null, CommandType.StoredProcedure, pgturn, Parameters);

            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = cmd;
            adp.Fill(ds);
            cmd.Parameters.Clear();
            connection.Close();
        }
        /// <summary>
        /// ִ��sql�ļ�ʧ�ܷ��ؼ�
        /// </summary>
        /// <param name="SQLFile"></param>
        /// <returns></returns>
        public static bool ExecSQLFile(string SQLFile, string sqlconn)
        {
            bool flag;
            FileStream stream;
            if (StringType.StrCmp(FileSystem.Dir(SQLFile, FileAttribute.Normal), "", false) == 0)
            {
                throw new ApplicationException("ִ��SQL�ű�ʧ�ܡ��ļ�[" + SQLFile + "]�����ڡ�");
            }
            try
            {
                stream = new FileStream(SQLFile, FileMode.Open);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                throw new ApplicationException("ִ��SQL�ű�ʧ�ܡ�" + exception1.Message);
                // this.ErrMsg = "ִ��SQL�ű�ʧ�ܡ�" + exception.Message;
            }
            StreamReader reader = new StreamReader(stream, Encoding.Default);
            try
            {
                string str2 = "";
                for (object obj2 = reader.ReadLine(); obj2 != null; obj2 = reader.ReadLine())
                {
                    string str3 = StringType.FromObject(ObjectType.StrCatObj(obj2, "  "));
                    if (StringType.StrCmp(str3.Substring(0, 2).ToUpper(), "GO", false) != 0)
                    {
                        str2 = str2 + str3 + "\r" + "\n";
                    }
                    else if (StringType.StrCmp(Strings.Trim(str2), "", false) != 0)
                    {

                        //  ExecuteNonQuery(sqlconn, CommandType.Text, str2, null);
                    }
                }
                if (StringType.StrCmp(str2, "", false) != 0)
                {
                    ExecuteNonQuery(sqlconn, CommandType.Text, str2, null);
                    str2 = "";
                }
                flag = true;
            }
            catch (Exception exception3)
            {
                ProjectData.SetProjectError(exception3);
                Exception exception2 = exception3;
               // throw new ApplicationException("ִ��SQL�ű�ʧ�ܡ�" + exception2.Message);
                flag = false;
                ProjectData.ClearProjectError();
                return flag;
            }
            finally
            {

                reader.Close();
                stream.Close();
            }
            return flag;
        }

        /// <summary>
        /// ִ��sql�ļ� ��ָ���ַ��� �滻��ָ�����ַ���
        /// </summary>
        /// <param name="SQLFile"></param>
        /// <param name="ValSouces">��ָ��Ҫ�滻���ַ���</param>
        /// <param name="ValReplaces">��Ҫ�滻�ɵ��ַ���</param>
        /// <returns></returns>
        public static bool ExecSQLFile(string SQLFile, string sqlconn, string[] ValSouces, string[] ValReplaces)
        {
            bool flag;
            FileStream stream;
            if (StringType.StrCmp(FileSystem.Dir(SQLFile, FileAttribute.Normal), "", false) == 0)
            {
                throw new ApplicationException("ִ��SQL�ű�ʧ�ܡ��ļ�[" + SQLFile + "]�����ڡ�");
            }
            try
            {
                stream = new FileStream(SQLFile, FileMode.Open);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                throw new ApplicationException("ִ��SQL�ű�ʧ�ܡ�" + exception1.Message);
            }
            StreamReader reader = new StreamReader(stream, Encoding.Default);
            try
            {
                string str2 = "";
                for (object obj2 = reader.ReadLine(); obj2 != null; obj2 = reader.ReadLine())
                {
                    string str3 = StringType.FromObject(ObjectType.StrCatObj(obj2, "  "));
                    int upperBound = ValSouces.GetUpperBound(0);
                    int num5 = upperBound;
                    for (int i = 0; i <= num5; i++)
                    {
                        str3 = str3.Replace(ValSouces[i], ValReplaces[i]);
                    }
                    if (StringType.StrCmp(str3.Substring(0, 2).ToUpper(), "GO", false) != 0)
                    {
                        str2 = str2 + str3 + "\r" + "\n";
                    }
                    else if (StringType.StrCmp(Strings.Trim(str2), "", false) != 0)
                    {
                        //  ExecuteNonQuery(sqlconn, CommandType.Text, str2, null);
                    }
                }
                if (StringType.StrCmp(str2, "", false) != 0)
                {
                    ExecuteNonQuery(sqlconn, CommandType.Text, str2, null);
                    str2 = "";
                }
                reader.Close();
                stream.Close();
                flag = true;
            }
            catch (Exception exception3)
            {
                ProjectData.SetProjectError(exception3);
                Exception exception2 = exception3;
                //throw new ApplicationException("ִ��SQL�ű�ʧ�ܡ�" + exception3.Message);
                flag = false;
                ProjectData.ClearProjectError();
                return flag;
            }
            finally{            	
                reader.Close();
                stream.Close();
            }
            return flag;
        }
        /// <summary>
        /// ִ��ͨ�÷�ҳ�洢����
        /// </summary>
        /// <param name="connectionString">ָ���������ַ���</param>
        /// <param name="selectlist">Ҫ��ѯ���ֶ� ���� Select</param>
        /// <param name="tbName">Ҫ��ѯ�ı�</param>
        /// <param name="pagesize">ÿҳ��ʾ������</param>
        /// <param name="pageindex">ָ����ǰ�ڼ�ҳ</param>
        /// <param name="whatNotIn">�����ֶ���</param>
        /// <param name="sort">ָ������ ���߽���('asc' or 'desc')�ɿ�</param>
        /// <param name="where">ָ����ѯ������ �ɿ�</param>
        /// <param name="ds">ָ������DataSet</param>
        public static void getpgturn(string connectionString, string selectlist, string tbName, int pagesize, int pageindex, string whatNotIn, string sort, string where, ref DataSet ds)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                getpgturn(connection, selectlist, tbName, pagesize, pageindex, whatNotIn, sort, where, ref ds);
            }
        }
        /// <summary>
        /// ��÷�ҳ�洢������Ҫ�Ĳ���
        /// </summary>
        /// <returns></returns>
        private static SqlParameter[] GetpgturnParameters()
        {
            SqlParameter[] parms = SqlHelper.GetCachedParameters(pgturn);

            if (parms == null)
            {
                parms = new SqlParameter[] {
                	new SqlParameter("@selectlist", SqlDbType.VarChar, 80),
                    new SqlParameter("@tbName",  SqlDbType.VarChar, 80),
                     new SqlParameter("@pagesize",  SqlDbType.Int, 80),
                     new SqlParameter("@pageindex",  SqlDbType.Int, 80),
                    new SqlParameter("@whatNotIn" , SqlDbType.VarChar, 80),
                    new SqlParameter("@sort",  SqlDbType.VarChar, 80),
                    new SqlParameter("@where" , SqlDbType.VarChar, 80)};
                SqlHelper.CacheParameters(pgturn, parms);
            }

            return parms;
        }

    }
}