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
    /// SqlHelper类是专门提供给广大用户用于高性能、可升级和最佳练习的sql数据操作
    /// </summary>
    public abstract class SqlHelper
    {

        //数据库连接字符串
        // public static string ConnectionStringLocalTransaction = "";
        //public static readonly string ConnectionStringLocalTransaction = "";// ConfigurationManager.ConnectionStrings["SQLConnString1"].ConnectionString;
        //public static readonly string ConnectionStringInventoryDistributedTransaction = ConfigurationManager.ConnectionStrings["SQLConnString2"].ConnectionString;
        //public static readonly string ConnectionStringOrderDistributedTransaction = ConfigurationManager.ConnectionStrings["SQLConnString3"].ConnectionString;
        //public static readonly string ConnectionStringProfile = ConfigurationManager.ConnectionStrings["SQLProfileConnString"].ConnectionString;
        private const string pgturn = "pgturn";
        // 用于缓存参数的HASH表
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        public const string SQL_SELECT_UFDATA = "select [name] from master.dbo.sysdatabases where [name] LIKE 'UFDATA_[0987654321][0987654321][0987654321]_20[0987654321][0987654321]' ";
        //public static String IsConn = false;

        /// <summary>
        ///  测试sql 连接
        ///   连接成功返回null,失败返回错误消息
        /// </summary>
        /// <remarks>
        ///  举例:  
        ///  TestConnStr("sqlServer","master","sa","")
        /// </remarks>
        /// <param name="DBServer">服务器</param>
        /// <param name="DBName">连接到的数据库</param>
        /// <param name="DBUser">用户名</param>
        /// <param name="DBPass">密码</param>
        /// <returns></returns>
        public static string TestConnStr(string DBServer, string DBName, string DBUser, string DBPass)
        {
            return TestConnStr(ConnStrFormat(DBServer, DBName, DBUser, DBPass));

        }

        /// <summary>
        /// 测试连接
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
        /// 构建ConnStr
        /// </summary>
        /// <param name="DBServer">服务器</param>
        /// <param name="DBName">连接到的数据库</param>
        /// <param name="DBUser">用户名</param>
        /// <param name="DBPass">密码</param>
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
        /// 枚举sql 实例信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSqlServer()
        {
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            return instance.GetDataSources().DefaultView.ToTable(true, "ServerName");
        }

        /// <summary>
        ///  给定连接的数据库用假设参数执行一个sql命令（不返回数据集）
        /// </summary>
        /// <param name="connectionString">一个有效的连接字符串</param>
        /// <param name="commandType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="commandText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
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
        /// 用现有的数据库连接执行一个sql命令（不返回数据集）
        /// </summary>
        /// <param name="conn">一个现有的数据库连接</param>
        /// <param name="commandType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="commandText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        ///使用现有的SQL事务执行一个sql命令（不返回数据集）
        /// </summary>
        /// <remarks>
        ///举例:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">一个现有的事务</param>
        /// <param name="commandType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="commandText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
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
                throw new ApplicationException("执行SQL脚本失败。" + ex.Message);
                // this.ErrMsg = "执行SQL脚本失败。" + exception.Message;
                SysDataLog.log.Error("", ex);

            }
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 用执行的数据库连接执行一个返回数据集的sql命令
        /// </summary>
        /// <remarks>
        /// 举例:  
        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的连接字符串</param>
        /// <param name="commandType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="commandText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>包含结果的读取器</returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            //创建一个SqlCommand对象
            SqlCommand cmd = new SqlCommand();
            //创建一个SqlConnection对象
            SqlConnection conn = new SqlConnection(connectionString);

            //在这里我们用一个try/catch结构执行sql文本命令/存储过程，因为如果这个方法产生一个异常我们要关闭连接，因为没有读取器存在，
            //因此commandBehaviour.CloseConnection 就不会执行
            try
            {
                //调用 PrepareCommand 方法，对 SqlCommand 对象设置参数
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                //调用 SqlCommand  的 ExecuteReader 方法
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //清除参数
                cmd.Parameters.Clear();
                return reader;
            }
            catch (SqlException ex)
            {
                //关闭连接，抛出异常
                conn.Close();
                SysDataLog.log.Error("", ex);
                //throw;
            }
            return null;
        }

        /// <summary>
        /// 用指定的数据库连接字符串执行一个命令并返回一个数据集的第一列
        /// </summary>
        /// <remarks>
        ///例如:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        ///<param name="connectionString">一个有效的连接字符串</param>
        /// <param name="commandType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="commandText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns>
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
        /// 用指定的数据库连接执行一个命令并返回一个数据集的第一列
        /// </summary>
        /// <remarks>
        /// 例如:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">一个存在的数据库连接</param>
        /// <param name="commandType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="commandText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 将参数集合添加到缓存
        /// </summary>
        /// <param name="cacheKey">添加到缓存的变量</param>
        /// <param name="cmdParms">一个将要添加到缓存的sql参数集合</param>
        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// 找会缓存参数集合
        /// </summary>
        /// <param name="cacheKey">用于找回参数的关键字</param>
        /// <returns>缓存的参数集合</returns>
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
        /// 准备执行一个命令
        /// </summary>
        /// <param name="cmd">sql命令</param>
        /// <param name="conn">Sql连接</param>
        /// <param name="trans">Sql事务</param>
        /// <param name="cmdType">命令类型例如 存储过程或者文本</param>
        /// <param name="cmdText">命令文本,例如：Select * from Products</param>
        /// <param name="cmdParms">执行命令的参数</param>
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
        /// 执行通用分页存储过程填充指定的DataSet
        /// </summary>
        /// <param name="SqlConnection">指定的连接</param>
        /// <param name="selectlist">要查询的字段 不带 Select</param>
        /// <param name="tbName">要查询的表</param>
        /// <param name="pagesize">每页显示的数量</param>
        /// <param name="pageindex">指定当前第几页</param>
        /// <param name="whatNotIn">排序字段名</param>
        /// <param name="sort">指定升序 或者降序('asc' or 'desc')可空</param>
        /// <param name="where">指定查询的条件 可空</param>
        /// <param name="ds">指定填充的DataSet</param>
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
        /// 执行sql文件失败返回假
        /// </summary>
        /// <param name="SQLFile"></param>
        /// <returns></returns>
        public static bool ExecSQLFile(string SQLFile, string sqlconn)
        {
            bool flag;
            FileStream stream;
            if (StringType.StrCmp(FileSystem.Dir(SQLFile, FileAttribute.Normal), "", false) == 0)
            {
                throw new ApplicationException("执行SQL脚本失败。文件[" + SQLFile + "]不存在。");
            }
            try
            {
                stream = new FileStream(SQLFile, FileMode.Open);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                throw new ApplicationException("执行SQL脚本失败。" + exception1.Message);
                // this.ErrMsg = "执行SQL脚本失败。" + exception.Message;
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
               // throw new ApplicationException("执行SQL脚本失败。" + exception2.Message);
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
        /// 执行sql文件 将指定字符串 替换成指定的字符串
        /// </summary>
        /// <param name="SQLFile"></param>
        /// <param name="ValSouces">将指定要替换的字符串</param>
        /// <param name="ValReplaces">将要替换成的字符串</param>
        /// <returns></returns>
        public static bool ExecSQLFile(string SQLFile, string sqlconn, string[] ValSouces, string[] ValReplaces)
        {
            bool flag;
            FileStream stream;
            if (StringType.StrCmp(FileSystem.Dir(SQLFile, FileAttribute.Normal), "", false) == 0)
            {
                throw new ApplicationException("执行SQL脚本失败。文件[" + SQLFile + "]不存在。");
            }
            try
            {
                stream = new FileStream(SQLFile, FileMode.Open);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                throw new ApplicationException("执行SQL脚本失败。" + exception1.Message);
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
                //throw new ApplicationException("执行SQL脚本失败。" + exception3.Message);
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
        /// 执行通用分页存储过程
        /// </summary>
        /// <param name="connectionString">指定的连接字符串</param>
        /// <param name="selectlist">要查询的字段 不带 Select</param>
        /// <param name="tbName">要查询的表</param>
        /// <param name="pagesize">每页显示的数量</param>
        /// <param name="pageindex">指定当前第几页</param>
        /// <param name="whatNotIn">排序字段名</param>
        /// <param name="sort">指定升序 或者降序('asc' or 'desc')可空</param>
        /// <param name="where">指定查询的条件 可空</param>
        /// <param name="ds">指定填充的DataSet</param>
        public static void getpgturn(string connectionString, string selectlist, string tbName, int pagesize, int pageindex, string whatNotIn, string sort, string where, ref DataSet ds)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                getpgturn(connection, selectlist, tbName, pagesize, pageindex, whatNotIn, sort, where, ref ds);
            }
        }
        /// <summary>
        /// 获得分页存储过程需要的参数
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