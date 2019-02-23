//****************************FrameCate********************************//
//FileName: DataAccess.cs
//Descript: Access���ݿ������
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
        //���ݿ������ַ���
        private static string strConn ="";// ConfigurationManager.AppSettings["Tree"].ToString();
        private OleDbConnection conn = new OleDbConnection(strConn);

        #region ������ݿ�����״̬ checkConn
        /// <summary>
        /// ������ݿ�����״̬
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

        #region ��ȡDataSet���ݼ� GetDataSet
        /// <summary>
        /// ��ȡDataSet���ݼ�
        /// </summary>
        /// <param name="strSql">SQL���</param>
        /// <param name="arrParam">��������</param>
        /// <returns>DataSet���ݼ�</returns>
        public DataSet GetDataSet(string strSql, OleDbParameter[] arrParam)// ��ȡDataSet���ݼ�
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

        #region ��ȡDataTable���ݼ��������Խ��з�ҳ GetDataTable
        /// <summary>
        /// ��ȡDataTable���ݼ��������Խ��з�ҳ
        /// </summary>
        /// <param name="strSql">SQL���</param>
        /// <returns>DataTable���ݼ�</returns>
        public DataTable GetDataTable(string strSql)// ��ȡDataTable���ݼ�
        {
            return GetDataTable(strSql, null, 0, 0);
        }
        #endregion

        #region ��ȡDataTable���ݼ��������Խ��з�ҳ GetDataTable
        /// <summary>
        /// ��ȡDataTable���ݼ��������Խ��з�ҳ
        /// </summary>
        /// <param name="strSql">SQL���</param>
        /// <param name="arrParam">��������</param>
        /// <param name="nStartRecord">��ҳ��ʼ�ļ�¼����һ����Ϣ��0��ʼ</param>
        /// <param name="nMaxRecord">��ҳ���Ҫ��ȡ�ļ�¼</param>
        /// <returns>DataTable���ݼ�</returns>
        public DataTable GetDataTable(string strSql,OleDbParameter[] arrParam, int nStartRecord, int nMaxRecord)// ��ȡDataTable���ݼ��������Խ��з�ҳ
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

        #region ��ȡDataRow���� GetDataRow
        /// <summary>
        /// ��ȡDataRow����
        /// </summary>
        /// <param name="strSql">SQL���</param>
        /// <returns>DataRow����</returns>
        public DataRow GetDataRow(String strSql)// ��ȡDataRow����
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

        #region ��ȡDataReader���� GetDataReader
        /// <summary>
        /// ��ȡDataReader����
        /// </summary>
        /// <param name="strSql">SQL���</param>
        /// <param name="arrParam">��������</param>
        /// <returns></returns>
        public OleDbDataReader GetDataReader(string strSql, OleDbParameter[] arrParam)// ��ȡDataReader����
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

        #region ִ��SQL��� ExecuteNonQuery
        /// <summary>
        /// ִ��SQL���
        /// </summary>
        /// <param name="strSql">SQL���</param>
        /// <param name="arrParam">��������</param>
        /// <returns>��ȷִ�з����棬�����</returns>
        public bool ExecuteNonQuery(string strSql, OleDbParameter[] arrParam)// ִ��SQL���
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

        #region ��ȡ��ѯ��¼���������� GetRowsCount
        /// <summary>
        /// ��ȡ��ѯ��¼����������
        /// </summary>
        /// <param name="strSql">SQL���</param>
        /// <param name="arrParam">��������</param>
        /// <returns></returns>
        public int GetRowsCount(string strSql, OleDbParameter[] arrParam)// ��ȡ��ѯ��¼����������
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

        #region ��ȡ��¼���������� GetRecordCount
        /// <summary>
        /// ��ȡ��¼����������
        /// </summary>
        /// <param name="ssql">SQL���,д����select count(*) from [table] </param>
        /// <returns></returns>
        public int GetRecordCount(string strSql)// ��ȡ��¼����������  SQL���,д����select count(*) from [table]
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

        #region ���������� createCommand
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="strSql">SQL���</param>
        /// <param name="arrParam">��������</param>
        /// <returns></returns>
        private OleDbCommand createCommand(string strSql, OleDbParameter[] arrParam)// ����������
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

        #region ����OleDbDataAdapter�� createAdapter
        /// <summary>
        /// ����OleDbDataAdapter��
        /// </summary>
        /// <param name="strSql">SQL���</param>
        /// <param name="arrParam">��������</param>
        /// <returns></returns>
        private OleDbDataAdapter createAdapter(string strSql, OleDbParameter[] arrParam)// ����OleDbDataAdapter��
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

        #region �������� CreateParam
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="paramName">������</param>
        /// <param name="dbType">��������</param>
        /// <param name="size">�������ʹ�С</param>
        /// <param name="value">ֵ</param>
        /// <returns>���ز���</returns>
        public OleDbParameter CreateParam(string paramName, OleDbType dbType, int size, object value)// ��������
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
