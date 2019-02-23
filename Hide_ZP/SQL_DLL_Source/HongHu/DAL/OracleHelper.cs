//using System;
//using System.Configuration;
//using System.Data;
//using System.Data.OracleClient;
//using System.Collections;

//namespace HongHu.DAL.DBUtility
//{

//    /// <summary>
//    /// A helper class used to execute queries against an Oracle database
//    /// </summary>
//    public abstract class OracleHelper {

//        //读配置文件的连接字串
//        public static readonly string ConnectionStringLocalTransaction = "";// ConfigurationManager.ConnectionStrings["OraConnString1"].ConnectionString;
//        public static readonly string ConnectionStringInventoryDistributedTransaction = "";//ConfigurationManager.ConnectionStrings["OraConnString2"].ConnectionString;
//        public static readonly string ConnectionStringOrderDistributedTransaction = "";// ConfigurationManager.ConnectionStrings["OraConnString3"].ConnectionString;
//        public static readonly string ConnectionStringProfile = "";// ConfigurationManager.ConnectionStrings["OraProfileConnString"].ConnectionString;
//        public static readonly string ConnectionStringMembership = "";// ConfigurationManager.ConnectionStrings["OraMembershipConnString"].ConnectionString;

//        //为叁数产生 hashtable 缓存
//        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

//        /// <summary>
//        ///执行命令并返回受影响的行数  指定数据库的连接字符串
//        /// 使用提供的参数集合commandParameters
//        /// </summary>
//        /// <param name="connString">数据库的连接字符串</param>
//        /// <param name="cmdType">指定cmdTexe是存储过程名,还是表,还是SQL语句</param>
//        /// <param name="cmdText">储存过程名或 PLSQL 指令</param>
//        /// <param name="commandParameters">SQL 语句或存储过程的参数集合</param>
//        /// <returns>返回受影响的行数</returns>
//        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters) {
//            // 创建一个新的 command 对象
//            OracleCommand cmd = new OracleCommand();

//            //创建一个新的  connection 对象
//            using (OracleConnection connection = new OracleConnection(connectionString)) {
//                //执行的 SQL 语句或存储过程 OracleCommand 的准备函数
//                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);

//                //运行SQL 语句或存储过程并返回受影响的行数
//                int val = cmd.ExecuteNonQuery();
//                cmd.Parameters.Clear();
//                return val;
//            }
//        }

//        /// <summary>
//        /// 执行命令并返回受影响的行数  指定数据库中生成的事务
//        /// 使用提供的参数集合commandParameters
//        /// </summary>
//        /// <remarks>
//        /// e.g.:  
//        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
//        /// </remarks>
//        /// <param name="trans">在数据库中生成的事务</param>
//        /// <param name="commandType">指定cmdTexe是存储过程名,还是表,还是SQL语句</param>
//        /// <param name="commandText">储存过程名或 PLSQL 指令</param>
//        /// <param name="commandParameters">SQL 语句或存储过程的参数集合</param>
//        /// <returns>返回受影响的行数</returns>
//        public static int ExecuteNonQuery(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters) {
//            OracleCommand cmd = new OracleCommand();
//            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
//            int val = cmd.ExecuteNonQuery();
//            cmd.Parameters.Clear();
//            return val;
//        }

//        /// <summary>
//        /// 执行命令并返回受影响的行数 指定数据库的连接字符串
//        /// 使用提供的参数集合commandParameters
//        /// </summary>
//        /// <remarks>
//        /// e.g.:  
//        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
//        /// </remarks>
//        /// <param name="conn">数据库的连接字符串</param>
//        /// <param name="commandType">指定cmdTexe是存储过程名,还是表,还是SQL语句</param>
//        /// <param name="commandText">储存过程名或 PLSQL 指令</param>
//        /// <param name="commandParameters">SQL 语句或存储过程的参数集合</param>
//        /// <returns>返回受影响的行数</returns>
//        public static int ExecuteNonQuery(OracleConnection connection, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters) {

//            OracleCommand cmd = new OracleCommand();

//            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
//            int val = cmd.ExecuteNonQuery();
//            cmd.Parameters.Clear();
//            return val;
//        }

//        /// <summary>
//        /// 执行并返回 OracleDataReader 对象
//        /// </summary>
//        /// <param name="connString">数据库的连接字符串</param>
//        /// <param name="commandType">指定cmdTexe是存储过程名,还是表,还是SQL语句</param>
//        /// <param name="commandText">储存过程名或 PLSQL 指令</param>
//        /// <param name="commandParameters">SQL 语句或存储过程的参数集合</param>
//        /// <returns>OracleDataReader 从数据源读取数据行的只进流的方法</returns>
//        public static OracleDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters) {

//            //创建一个新的  connection 对象 和 command  对象
//            OracleCommand cmd = new OracleCommand();
//            OracleConnection conn = new OracleConnection(connectionString);

//            try {
//                //执行的 SQL 语句或存储过程 OracleCommand 的准备函数
//                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);

//                //在执行该命令时，如果关闭关联的 DataReader 对象，则关联的 Connection 对象也将关闭。 
//                OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
//                cmd.Parameters.Clear();
//                return rdr;

//            }
//            catch {

//                //如果发生错误,将关闭conn连接
//                conn.Close();
//                throw;
//            }
//        }

//        /// <summary>
//        ///执行查询，并将查询返回的结果集中第一行的第一列作为 .NET Framework 数据类型返回。忽略额外的列或行。 
//        /// 使用提供的参数集合commandParameters
//        /// </summary>
//        /// <remarks>
//        /// e.g.:  
//        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
//        /// </remarks>
//        /// <param name="connectionString">数据库的连接字符串</param>
//        /// <param name="commandType">指定cmdTexe是存储过程名,还是表,还是SQL语句</param>
//        /// <param name="commandText">储存过程名或 PLSQL 指令</param>
//        /// <param name="commandParameters">SQL 语句或存储过程的参数集合</param>
//        /// <returns>返回一个object类型的数据</returns>
//        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters) {
//            OracleCommand cmd = new OracleCommand();

//            using (OracleConnection conn = new OracleConnection(connectionString)) {
//                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
//                object val = cmd.ExecuteScalar();
//                cmd.Parameters.Clear();
//                return val;
//            }
//        }

//        ///	<summary>
//        ///	执行查询，并将查询返回的结果集中第一行的第一列作为 .NET Framework 数据类型返回。忽略额外的列或行。指定数据库中生成的事务
//        ///	使用提供的参数集合commandParameters
//        ///	</summary>
//        ///	<param name="transaction">一有效的 数据库中生成的事务 SqlTransaction</param>
//        ///	<param name="commandType">指定cmdTexe是存储过程名,还是表,还是SQL语句</param>
//        ///	<param name="commandText">储存过程名或 PLSQL 指令</param>
//        ///	<param name="commandParameters">SQL 语句或存储过程的参数集合</param>
//        ///	<returns>A返回一个object类型的数据</returns>
//        public static object ExecuteScalar(OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters) {
//            if(transaction == null)
//                throw new ArgumentNullException("transaction");
//            if(transaction != null && transaction.Connection == null)
//                throw new ArgumentException("SqlTransaction事务回滚出错，请提供开着的处理事物", "transaction");

//            //产生command对象而且准备运行它
//            OracleCommand cmd = new OracleCommand();

//            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

//            // 运行指令并返回结果
//            object retval = cmd.ExecuteScalar();

//            cmd.Parameters.Clear();
//            return retval;
//        }

//        /// <summary>
//        ///执行查询，并将查询返回的结果集中第一行的第一列作为 .NET Framework 数据类型返回。忽略额外的列或行。 指定数据库的连接字符串
//        /// 使用提供的参数集合commandParameters
//        /// </summary>
//        /// <remarks>
//        /// e.g.:  
//        ///  Object obj = ExecuteScalar(conn, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
//        /// </remarks>
//        /// <param name="conn">数据库的连接字符串</param>
//        /// <param name="commandType">指定cmdTexe是存储过程名,还是表,还是SQL语句</param>
//        /// <param name="commandText">储存过程名或 PLSQL 指令</param>
//        /// <param name="commandParameters">SQL 语句或存储过程的参数集合</param>
//        /// <returns>返回一个object类型的数据</returns>
//        public static object ExecuteScalar(OracleConnection connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters) {
//            OracleCommand cmd = new OracleCommand();

//            PrepareCommand(cmd, connectionString, null, cmdType, cmdText, commandParameters);
//            object val = cmd.ExecuteScalar();
//            cmd.Parameters.Clear();
//            return val;
//        }

//        /// <summary>
//        /// 把一组叁数加入缓存
//        /// </summary>
//        /// <param name="cacheKey">设置用于从 Cache 检索项的唯一键。</param>
//        /// <param name="commandParameters">要加入缓存的parameter</param>
//        public static void CacheParameters(string cacheKey, params OracleParameter[] commandParameters) {
//            parmCache[cacheKey] = commandParameters;
//        }

//        /// <summary>
//        /// 获取缓存中的parameter
//        /// </summary>
//        /// <param name="cacheKey">用于从 Cache 检索项的唯一键。</param>
//        /// <returns></returns>
//        public static OracleParameter[] GetCachedParameters(string cacheKey) {
//            OracleParameter[] cachedParms = (OracleParameter[])parmCache[cacheKey];

//            if (cachedParms == null)
//                return null;

//            //如果这些parameters在缓存中
//            OracleParameter[] clonedparms = new OracleParameter[cachedParms.Length];

//            // 返回 parameters 
//            for (int i = 0, j = cachedParms.Length; i < j; i++)
//                clonedparms[i] = (OracleParameter)((ICloneable)cachedParms[i]).Clone();

//            return clonedparms;
//        }

//        /// <summary>
//        /// 为执行的 SQL 语句或存储过程的OracleCommand 的准备函数
//        /// </summary>
//        /// <param name="cmd">OracleCommand</param>
//        /// <param name="conn">连接对数据库的字串</param>
//        /// <param name="trans">要在数据库中生成的事务。 </param>
//        /// <param name="cmdType">指定cmdTexe是存储过程名,还是表,还是SQL语句</param>
//        /// <param name="cmdText">储存过程名或 PLSQL 指令</param>
//        /// <param name="commandParameters">SQL 语句或存储过程的参数集合</param>
//        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameter[] commandParameters) {

//            //开启连接
//            if (conn.State != ConnectionState.Open)
//                conn.Open();

//            //添加command的参数
//            cmd.Connection = conn;
//            cmd.CommandText = cmdText;
//            cmd.CommandType = cmdType;

//            //绑它在数据库中生成的事务，如果它存在
//            if (trans != null)
//                cmd.Transaction = trans;

//            // 绑它在SQL 语句或存储过程的参数集合
//            if (commandParameters != null) {
//                foreach (OracleParameter parm in commandParameters)
//                    cmd.Parameters.Add(parm);
//            }
//        }

//        /// <summary>
//        /// 转换NET Framework 数据类型中的 boolean 类型为Oracle数据库中的布尔类型
//        /// </summary>
//        /// <param name="value">要转换的值</param>
//        /// <returns></returns>
//        public static string OraBit(bool value) {
//            if(value)
//                return "Y";
//            else
//                return "N";
//        }

//        /// <summary>
//        ///  转换Oracle数据库中的boolean类型为NET Framework 数据类型中的 boolean 类型
//        /// </summary>
//        /// <param name="value">Value to convert</param>
//        /// <returns></returns>
//        public static bool OraBool(string value) {
//            if(value.Equals("Y"))
//                return true;
//            else
//                return false;
//        } 
//    }
//}
