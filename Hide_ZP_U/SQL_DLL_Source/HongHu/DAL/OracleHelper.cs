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

//        //�������ļ��������ִ�
//        public static readonly string ConnectionStringLocalTransaction = "";// ConfigurationManager.ConnectionStrings["OraConnString1"].ConnectionString;
//        public static readonly string ConnectionStringInventoryDistributedTransaction = "";//ConfigurationManager.ConnectionStrings["OraConnString2"].ConnectionString;
//        public static readonly string ConnectionStringOrderDistributedTransaction = "";// ConfigurationManager.ConnectionStrings["OraConnString3"].ConnectionString;
//        public static readonly string ConnectionStringProfile = "";// ConfigurationManager.ConnectionStrings["OraProfileConnString"].ConnectionString;
//        public static readonly string ConnectionStringMembership = "";// ConfigurationManager.ConnectionStrings["OraMembershipConnString"].ConnectionString;

//        //Ϊ�������� hashtable ����
//        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

//        /// <summary>
//        ///ִ�����������Ӱ�������  ָ�����ݿ�������ַ���
//        /// ʹ���ṩ�Ĳ�������commandParameters
//        /// </summary>
//        /// <param name="connString">���ݿ�������ַ���</param>
//        /// <param name="cmdType">ָ��cmdTexe�Ǵ洢������,���Ǳ�,����SQL���</param>
//        /// <param name="cmdText">����������� PLSQL ָ��</param>
//        /// <param name="commandParameters">SQL ����洢���̵Ĳ�������</param>
//        /// <returns>������Ӱ�������</returns>
//        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters) {
//            // ����һ���µ� command ����
//            OracleCommand cmd = new OracleCommand();

//            //����һ���µ�  connection ����
//            using (OracleConnection connection = new OracleConnection(connectionString)) {
//                //ִ�е� SQL ����洢���� OracleCommand ��׼������
//                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);

//                //����SQL ����洢���̲�������Ӱ�������
//                int val = cmd.ExecuteNonQuery();
//                cmd.Parameters.Clear();
//                return val;
//            }
//        }

//        /// <summary>
//        /// ִ�����������Ӱ�������  ָ�����ݿ������ɵ�����
//        /// ʹ���ṩ�Ĳ�������commandParameters
//        /// </summary>
//        /// <remarks>
//        /// e.g.:  
//        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
//        /// </remarks>
//        /// <param name="trans">�����ݿ������ɵ�����</param>
//        /// <param name="commandType">ָ��cmdTexe�Ǵ洢������,���Ǳ�,����SQL���</param>
//        /// <param name="commandText">����������� PLSQL ָ��</param>
//        /// <param name="commandParameters">SQL ����洢���̵Ĳ�������</param>
//        /// <returns>������Ӱ�������</returns>
//        public static int ExecuteNonQuery(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters) {
//            OracleCommand cmd = new OracleCommand();
//            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
//            int val = cmd.ExecuteNonQuery();
//            cmd.Parameters.Clear();
//            return val;
//        }

//        /// <summary>
//        /// ִ�����������Ӱ������� ָ�����ݿ�������ַ���
//        /// ʹ���ṩ�Ĳ�������commandParameters
//        /// </summary>
//        /// <remarks>
//        /// e.g.:  
//        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
//        /// </remarks>
//        /// <param name="conn">���ݿ�������ַ���</param>
//        /// <param name="commandType">ָ��cmdTexe�Ǵ洢������,���Ǳ�,����SQL���</param>
//        /// <param name="commandText">����������� PLSQL ָ��</param>
//        /// <param name="commandParameters">SQL ����洢���̵Ĳ�������</param>
//        /// <returns>������Ӱ�������</returns>
//        public static int ExecuteNonQuery(OracleConnection connection, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters) {

//            OracleCommand cmd = new OracleCommand();

//            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
//            int val = cmd.ExecuteNonQuery();
//            cmd.Parameters.Clear();
//            return val;
//        }

//        /// <summary>
//        /// ִ�в����� OracleDataReader ����
//        /// </summary>
//        /// <param name="connString">���ݿ�������ַ���</param>
//        /// <param name="commandType">ָ��cmdTexe�Ǵ洢������,���Ǳ�,����SQL���</param>
//        /// <param name="commandText">����������� PLSQL ָ��</param>
//        /// <param name="commandParameters">SQL ����洢���̵Ĳ�������</param>
//        /// <returns>OracleDataReader ������Դ��ȡ�����е�ֻ�����ķ���</returns>
//        public static OracleDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters) {

//            //����һ���µ�  connection ���� �� command  ����
//            OracleCommand cmd = new OracleCommand();
//            OracleConnection conn = new OracleConnection(connectionString);

//            try {
//                //ִ�е� SQL ����洢���� OracleCommand ��׼������
//                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);

//                //��ִ�и�����ʱ������رչ����� DataReader ����������� Connection ����Ҳ���رա� 
//                OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
//                cmd.Parameters.Clear();
//                return rdr;

//            }
//            catch {

//                //�����������,���ر�conn����
//                conn.Close();
//                throw;
//            }
//        }

//        /// <summary>
//        ///ִ�в�ѯ��������ѯ���صĽ�����е�һ�еĵ�һ����Ϊ .NET Framework �������ͷ��ء����Զ�����л��С� 
//        /// ʹ���ṩ�Ĳ�������commandParameters
//        /// </summary>
//        /// <remarks>
//        /// e.g.:  
//        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
//        /// </remarks>
//        /// <param name="connectionString">���ݿ�������ַ���</param>
//        /// <param name="commandType">ָ��cmdTexe�Ǵ洢������,���Ǳ�,����SQL���</param>
//        /// <param name="commandText">����������� PLSQL ָ��</param>
//        /// <param name="commandParameters">SQL ����洢���̵Ĳ�������</param>
//        /// <returns>����һ��object���͵�����</returns>
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
//        ///	ִ�в�ѯ��������ѯ���صĽ�����е�һ�еĵ�һ����Ϊ .NET Framework �������ͷ��ء����Զ�����л��С�ָ�����ݿ������ɵ�����
//        ///	ʹ���ṩ�Ĳ�������commandParameters
//        ///	</summary>
//        ///	<param name="transaction">һ��Ч�� ���ݿ������ɵ����� SqlTransaction</param>
//        ///	<param name="commandType">ָ��cmdTexe�Ǵ洢������,���Ǳ�,����SQL���</param>
//        ///	<param name="commandText">����������� PLSQL ָ��</param>
//        ///	<param name="commandParameters">SQL ����洢���̵Ĳ�������</param>
//        ///	<returns>A����һ��object���͵�����</returns>
//        public static object ExecuteScalar(OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters) {
//            if(transaction == null)
//                throw new ArgumentNullException("transaction");
//            if(transaction != null && transaction.Connection == null)
//                throw new ArgumentException("SqlTransaction����ع��������ṩ���ŵĴ�������", "transaction");

//            //����command�������׼��������
//            OracleCommand cmd = new OracleCommand();

//            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

//            // ����ָ����ؽ��
//            object retval = cmd.ExecuteScalar();

//            cmd.Parameters.Clear();
//            return retval;
//        }

//        /// <summary>
//        ///ִ�в�ѯ��������ѯ���صĽ�����е�һ�еĵ�һ����Ϊ .NET Framework �������ͷ��ء����Զ�����л��С� ָ�����ݿ�������ַ���
//        /// ʹ���ṩ�Ĳ�������commandParameters
//        /// </summary>
//        /// <remarks>
//        /// e.g.:  
//        ///  Object obj = ExecuteScalar(conn, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
//        /// </remarks>
//        /// <param name="conn">���ݿ�������ַ���</param>
//        /// <param name="commandType">ָ��cmdTexe�Ǵ洢������,���Ǳ�,����SQL���</param>
//        /// <param name="commandText">����������� PLSQL ָ��</param>
//        /// <param name="commandParameters">SQL ����洢���̵Ĳ�������</param>
//        /// <returns>����һ��object���͵�����</returns>
//        public static object ExecuteScalar(OracleConnection connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters) {
//            OracleCommand cmd = new OracleCommand();

//            PrepareCommand(cmd, connectionString, null, cmdType, cmdText, commandParameters);
//            object val = cmd.ExecuteScalar();
//            cmd.Parameters.Clear();
//            return val;
//        }

//        /// <summary>
//        /// ��һ���������뻺��
//        /// </summary>
//        /// <param name="cacheKey">�������ڴ� Cache �������Ψһ����</param>
//        /// <param name="commandParameters">Ҫ���뻺���parameter</param>
//        public static void CacheParameters(string cacheKey, params OracleParameter[] commandParameters) {
//            parmCache[cacheKey] = commandParameters;
//        }

//        /// <summary>
//        /// ��ȡ�����е�parameter
//        /// </summary>
//        /// <param name="cacheKey">���ڴ� Cache �������Ψһ����</param>
//        /// <returns></returns>
//        public static OracleParameter[] GetCachedParameters(string cacheKey) {
//            OracleParameter[] cachedParms = (OracleParameter[])parmCache[cacheKey];

//            if (cachedParms == null)
//                return null;

//            //�����Щparameters�ڻ�����
//            OracleParameter[] clonedparms = new OracleParameter[cachedParms.Length];

//            // ���� parameters 
//            for (int i = 0, j = cachedParms.Length; i < j; i++)
//                clonedparms[i] = (OracleParameter)((ICloneable)cachedParms[i]).Clone();

//            return clonedparms;
//        }

//        /// <summary>
//        /// Ϊִ�е� SQL ����洢���̵�OracleCommand ��׼������
//        /// </summary>
//        /// <param name="cmd">OracleCommand</param>
//        /// <param name="conn">���Ӷ����ݿ���ִ�</param>
//        /// <param name="trans">Ҫ�����ݿ������ɵ����� </param>
//        /// <param name="cmdType">ָ��cmdTexe�Ǵ洢������,���Ǳ�,����SQL���</param>
//        /// <param name="cmdText">����������� PLSQL ָ��</param>
//        /// <param name="commandParameters">SQL ����洢���̵Ĳ�������</param>
//        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameter[] commandParameters) {

//            //��������
//            if (conn.State != ConnectionState.Open)
//                conn.Open();

//            //���command�Ĳ���
//            cmd.Connection = conn;
//            cmd.CommandText = cmdText;
//            cmd.CommandType = cmdType;

//            //���������ݿ������ɵ��������������
//            if (trans != null)
//                cmd.Transaction = trans;

//            // ������SQL ����洢���̵Ĳ�������
//            if (commandParameters != null) {
//                foreach (OracleParameter parm in commandParameters)
//                    cmd.Parameters.Add(parm);
//            }
//        }

//        /// <summary>
//        /// ת��NET Framework ���������е� boolean ����ΪOracle���ݿ��еĲ�������
//        /// </summary>
//        /// <param name="value">Ҫת����ֵ</param>
//        /// <returns></returns>
//        public static string OraBit(bool value) {
//            if(value)
//                return "Y";
//            else
//                return "N";
//        }

//        /// <summary>
//        ///  ת��Oracle���ݿ��е�boolean����ΪNET Framework ���������е� boolean ����
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
