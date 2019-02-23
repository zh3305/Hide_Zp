//****************************FrameCate********************************//
//FileName: DataAccess.cs
//Descript: Access数据库操作类
//Assembly: V 1.1.0.0
//CreateDate: 2009-01-14
//****************************Made By HongHu.DAL.Using******************************//
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

namespace HongHu.DAL.Using
{
    class DataAccess
    {
        //数据库连接字符串
        private static string strConn ="";// ConfigurationManager.AppSettings["Tree"].ToString();
        private OleDbConnection conn = new OleDbConnection(strConn);

        #region 检查数据库连接状态 checkConn
        /// <summary>
        /// 检查数据库连接状态
        /// </summary>
        private void checkConn()
        {
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    SysDataLog.log.Info(ex);
                }
            }
        }

        private void connClose()
        {
            if (conn.State == ConnectionState.Open && conn != null)
            {
                conn.Close();
            }
        }

        private void Dispose()
        {
            if (conn != null)
            {
                conn.Dispose();
                conn = null;
            }
        }
        #endregion

        #region 获取DataSet数据集 GetDataSet
        /// <summary>
        /// 获取DataSet数据集
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="arrParam">参数数组</param>
        /// <returns>DataSet数据集</returns>
        public DataSet GetDataSet(string strSql, OleDbParameter[] arrParam)// 获取DataSet数据集
        {
            checkConn();

            try
            {
                OleDbDataAdapter oda = createAdapter(strSql, arrParam);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                SysDataLog.log.Info(strSql,ex);
                throw;
            }
            finally
            {
                connClose();
            }

        }
        #endregion

        #region 获取DataTable数据集，并可以进行分页 GetDataTable
        /// <summary>
        /// 获取DataTable数据集，并可以进行分页
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>DataTable数据集</returns>
        public DataTable GetDataTable(string strSql)// 获取DataTable数据集
        {
            return GetDataTable(strSql, null, 0, 0);
        }
        #endregion

        #region 获取DataTable数据集，并可以进行分页 GetDataTable
        /// <summary>
        /// 获取DataTable数据集，并可以进行分页
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="arrParam">参数数组</param>
        /// <param name="nStartRecord">分页开始的记录，第一条信息从0开始</param>
        /// <param name="nMaxRecord">分页最大要读取的记录</param>
        /// <returns>DataTable数据集</returns>
        public DataTable GetDataTable(string strSql,OleDbParameter[] arrParam, int nStartRecord, int nMaxRecord)// 获取DataTable数据集，并可以进行分页
        {
            checkConn();

            try
            {
                OleDbDataAdapter oda = createAdapter(strSql, arrParam);
                DataSet ds = new DataSet();

                if (nStartRecord == 0 && nMaxRecord == 0)
                { oda.Fill(ds, "srcTable"); }
                else
                { oda.Fill(ds, nStartRecord, nMaxRecord, "srcTable"); }

                return ds.Tables["srcTable"];
            }
            catch (Exception ex)
            {
                SysDataLog.log.Info(strSql,ex);
                throw;
            }
            finally
            {
                connClose();
            }
        }
        #endregion

        #region 获取DataRow数据 GetDataRow
        /// <summary>
        /// 获取DataRow数据
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>DataRow数据</returns>
        public DataRow GetDataRow(String strSql)// 获取DataRow数据
        {
            checkConn();
            try
            {
                OleDbDataAdapter oda = new OleDbDataAdapter(strSql, conn);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0].Rows[0];
            }
            catch (Exception ex)
            {
                SysDataLog.log.Info(strSql,ex);
                throw;
            }
            finally
            {
                connClose();
            }
        }
        #endregion

        #region 获取DataReader对象 GetDataReader
        /// <summary>
        /// 获取DataReader对象
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="arrParam">参数数组</param>
        /// <returns></returns>
        public OleDbDataReader GetDataReader(string strSql, OleDbParameter[] arrParam)// 获取DataReader对象
        {
            checkConn();
            OleDbDataReader odr = null;

            try
            {
                OleDbCommand cmd = createCommand(strSql, arrParam);
                odr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return odr;
            }
            catch (Exception ex)
            {
                odr = null;
                SysDataLog.log.Info(strSql,ex);
                throw;
            }
            finally
            {
                connClose();
            }
        }
        #endregion

        #region 执行SQL语句 ExecuteNonQuery
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="arrParam">参数数组</param>
        /// <returns>正确执行返回真，否则假</returns>
        public bool ExecuteNonQuery(string strSql, OleDbParameter[] arrParam)// 执行SQL语句
        {
            checkConn();
            try
            {
                OleDbCommand cmd = createCommand(strSql, arrParam);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                SysDataLog.log.Info(strSql,ex);
                throw;
            }
            finally
            {
                connClose();
            }
        }
        #endregion

        #region 获取查询记录集的总行数 GetRowsCount
        /// <summary>
        /// 获取查询记录集的总行数
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="arrParam">参数数组</param>
        /// <returns></returns>
        public int GetRowsCount(string strSql, OleDbParameter[] arrParam)// 获取查询记录集的总行数
        {
            checkConn();

            try
            {
                OleDbDataAdapter oda = createAdapter(strSql, arrParam);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0].Rows.Count;
            }
            catch (Exception ex)
            {
                SysDataLog.log.Info(strSql,ex);
                throw;
            }
            finally
            {
                connClose();
            }
        }
        #endregion

        #region 获取记录集的总行数 GetRecordCount
        /// <summary>
        /// 获取记录集的总行数
        /// </summary>
        /// <param name="ssql">SQL语句,写法：select count(*) from [table] </param>
        /// <returns></returns>
        public int GetRecordCount(string strSql)// 获取记录集的总行数  SQL语句,写法：select count(*) from [table]
        {
            checkConn();
            try
            {
                int nTM1 = 0;
                OleDbCommand ocom = new OleDbCommand(strSql, conn);
                OleDbDataReader odr = ocom.ExecuteReader(CommandBehavior.CloseConnection);
                while (odr.Read())
                {
                    nTM1 = int.Parse(odr[0].ToString());
                }
                odr = null;
                return nTM1;
            }
            catch (Exception ex)
            {
                SysDataLog.log.Info(ex);
                throw new Exception(ex.Message);
            }
            finally
            {
                connClose();
            }
        }
        #endregion

        #region 创建命令类 createCommand
        /// <summary>
        /// 创建命令类
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="arrParam">参数数组</param>
        /// <returns></returns>
        private OleDbCommand createCommand(string strSql, OleDbParameter[] arrParam)// 创建命令类
        {
            OleDbCommand cmd = new OleDbCommand(strSql, conn);
            if (arrParam != null)
            {
                foreach (OleDbParameter parameter in arrParam)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            return cmd;
        }
        #endregion 

        #region 创建OleDbDataAdapter类 createAdapter
        /// <summary>
        /// 创建OleDbDataAdapter类
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="arrParam">参数数组</param>
        /// <returns></returns>
        private OleDbDataAdapter createAdapter(string strSql, OleDbParameter[] arrParam)// 创建OleDbDataAdapter类
        {
            OleDbDataAdapter oda = new OleDbDataAdapter(strSql, conn);
            if (arrParam != null)
            {
                foreach (OleDbParameter parameter in arrParam)
                {
                    oda.SelectCommand.Parameters.Add(parameter);
                }
            }
            return oda;
        }
        #endregion 

        #region 创建参数 CreateParam
        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">数据类型大小</param>
        /// <param name="value">值</param>
        /// <returns>返回参数</returns>
        public OleDbParameter CreateParam(string paramName, OleDbType dbType, int size, object value)// 创建参数
        {
            OleDbParameter param;
            if (size > 0)
            {
                param = new OleDbParameter(paramName, dbType, size);
            }
            else
            {
                param = new OleDbParameter(paramName, dbType);
            }
            param.Value = value;
            return param;
        }
        #endregion
    }
}
