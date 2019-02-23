namespace HongHu.DAL
{
    using System;
    using System.Data;
    using System.Data.SQLite;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class SQLiteHelper
    {
        private SQLiteHelper()
        {
        }

        protected void AssignParameterValues(IDataParameter[] commandParameters, params object[] parameterValues)
        {
            if ((commandParameters != null) && (parameterValues != null))
            {
                if (commandParameters.Length != parameterValues.Length)
                {
                    throw new ArgumentException("Parameter count does not match Parameter Value count.");
                }
                int i = 0;
                int j = commandParameters.Length;
                int k = 0;
                while (i < j)
                {
                    if (commandParameters[i].Direction != ParameterDirection.ReturnValue)
                    {
                        if (parameterValues[k] is IDataParameter)
                        {
                            IDataParameter paramInstance = (IDataParameter) parameterValues[k];
                            if (paramInstance.Direction == ParameterDirection.ReturnValue)
                            {
                                paramInstance = (IDataParameter) parameterValues[++k];
                            }
                            if (paramInstance.Value == null)
                            {
                                commandParameters[i].Value = DBNull.Value;
                            }
                            else
                            {
                                commandParameters[i].Value = paramInstance.Value;
                            }
                        }
                        else if (parameterValues[k] == null)
                        {
                            commandParameters[i].Value = DBNull.Value;
                        }
                        else
                        {
                            commandParameters[i].Value = parameterValues[k];
                        }
                        k++;
                    }
                    i++;
                }
            }
        }

        protected internal static void AssignParameterValues(IDataParameterCollection commandParameters, DataRow dataRow)
        {
            if ((commandParameters != null) && (dataRow != null))
            {
                DataColumnCollection columns = dataRow.Table.Columns;
                int i = 0;
                foreach (IDataParameter commandParameter in commandParameters)
                {
                    if ((commandParameter.ParameterName == null) || (commandParameter.ParameterName.Length <= 1))
                    {
                        throw new InvalidOperationException(string.Format("Please provide a valid parameter name on the parameter #{0},                            the ParameterName property has the following value: '{1}'.", i, commandParameter.ParameterName));
                    }
                    if (columns.Contains(commandParameter.ParameterName))
                    {
                        commandParameter.Value = dataRow[commandParameter.ParameterName];
                    }
                    else if (columns.Contains(commandParameter.ParameterName.Substring(1)))
                    {
                        commandParameter.Value = dataRow[commandParameter.ParameterName.Substring(1)];
                    }
                    i++;
                }
            }
        }

        protected void AssignParameterValues(IDataParameter[] commandParameters, DataRow dataRow)
        {
            if ((commandParameters != null) && (dataRow != null))
            {
                DataColumnCollection columns = dataRow.Table.Columns;
                int i = 0;
                foreach (IDataParameter commandParameter in commandParameters)
                {
                    if ((commandParameter.ParameterName == null) || (commandParameter.ParameterName.Length <= 1))
                    {
                        throw new InvalidOperationException(string.Format("Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: '{1}'.", i, commandParameter.ParameterName));
                    }
                    if (columns.Contains(commandParameter.ParameterName))
                    {
                        commandParameter.Value = dataRow[commandParameter.ParameterName];
                    }
                    else if (columns.Contains(commandParameter.ParameterName.Substring(1)))
                    {
                        commandParameter.Value = dataRow[commandParameter.ParameterName.Substring(1)];
                    }
                    i++;
                }
            }
        }

        private static SQLiteParameterCollection AttachParameters(SQLiteCommand cmd, string commandText, params object[] paramList)
        {
            if ((paramList == null) || (paramList.Length == 0))
            {
                return null;
            }
            SQLiteParameterCollection coll = cmd.Parameters;
            string parmString = commandText.Substring(commandText.IndexOf("@")).Replace(",", " ,");
            string pattern = @"(@)\S*(.*?)\b";
            MatchCollection mc = new Regex(pattern, RegexOptions.IgnoreCase).Matches(parmString);
            string[] paramNames = new string[mc.Count];
            int i = 0;
            foreach (Match m in mc)
            {
                paramNames[i] = m.Value;
                i++;
            }
            int j = 0;
            Type t = null;
            foreach (object o in paramList)
            {
                t = o.GetType();
                SQLiteParameter parm = new SQLiteParameter();
                switch (t.ToString())
                {
                    case "DBNull":
                    case "Char":
                    case "SByte":
                    case "UInt16":
                    case "UInt32":
                    case "UInt64":
                        throw new SystemException("Invalid data type");

                    case "System.String":
                        parm.DbType = DbType.String;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (string) paramList[j];
                        coll.Add(parm);
                        break;

                    case "System.Byte[]":
                        parm.DbType = DbType.Binary;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (byte[]) paramList[j];
                        coll.Add(parm);
                        break;

                    case "System.Int32":
                        parm.DbType = DbType.Int32;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (int) paramList[j];
                        coll.Add(parm);
                        break;

                    case "System.Boolean":
                        parm.DbType = DbType.Boolean;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (bool) paramList[j];
                        coll.Add(parm);
                        break;

                    case "System.DateTime":
                        parm.DbType = DbType.DateTime;
                        parm.ParameterName = paramNames[j];
                        parm.Value = Convert.ToDateTime(paramList[j]);
                        coll.Add(parm);
                        break;

                    case "System.Double":
                        parm.DbType = DbType.Double;
                        parm.ParameterName = paramNames[j];
                        parm.Value = Convert.ToDouble(paramList[j]);
                        coll.Add(parm);
                        break;

                    case "System.Decimal":
                        parm.DbType = DbType.Decimal;
                        parm.ParameterName = paramNames[j];
                        parm.Value = Convert.ToDecimal(paramList[j]);
                        break;

                    case "System.Guid":
                        parm.DbType = DbType.Guid;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (Guid) paramList[j];
                        break;

                    case "System.Object":
                        parm.DbType = DbType.Object;
                        parm.ParameterName = paramNames[j];
                        parm.Value = paramList[j];
                        coll.Add(parm);
                        break;

                    default:
                        throw new SystemException("Value is of unknown data type");
                }
                j++;
            }
            return coll;
        }

        public static SQLiteCommand CreateCommand(SQLiteConnection connection, string commandText, params SQLiteParameter[] commandParameters)
        {
            SQLiteCommand cmd = new SQLiteCommand(commandText, connection);
            if (commandParameters.Length > 0)
            {
                foreach (SQLiteParameter parm in commandParameters)
                {
                    cmd.Parameters.Add(parm);
                }
            }
            return cmd;
        }

        public static SQLiteCommand CreateCommand(string connectionString, string commandText, params SQLiteParameter[] commandParameters)
        {
            SQLiteConnection cn = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand(commandText, cn);
            if (commandParameters.Length > 0)
            {
                foreach (SQLiteParameter parm in commandParameters)
                {
                    cmd.Parameters.Add(parm);
                }
            }
            return cmd;
        }

        public static SQLiteParameter CreateParameter(string parameterName, DbType parameterType, object parameterValue)
        {
            SQLiteParameter parameter = new SQLiteParameter();
            parameter.DbType = parameterType;
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            return parameter;
        }

        public static DataSet ExecuteDataset(SQLiteCommand cmd)
        {
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            DataSet ds = new DataSet();
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            da.Fill(ds);
            da.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            return ds;
        }

        public static DataSet ExecuteDataset(SQLiteTransaction transaction, string commandText, params SQLiteParameter[] commandParameters)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rolled back or committed, please provide an open transaction.", "transaction");
            }
            IDbCommand cmd = transaction.Connection.CreateCommand();
            cmd.CommandText = commandText;
            foreach (SQLiteParameter parm in commandParameters)
            {
                cmd.Parameters.Add(parm);
            }
            if (transaction.Connection.State == ConnectionState.Closed)
            {
                transaction.Connection.Open();
            }
            return ExecuteDataset((SQLiteCommand) cmd);
        }

        public static DataSet ExecuteDataset(SQLiteTransaction transaction, string commandText, object[] commandParameters)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rolled back or committed,                                                          please provide an open transaction.", "transaction");
            }
            IDbCommand cmd = transaction.Connection.CreateCommand();
            cmd.CommandText = commandText;
            AttachParameters((SQLiteCommand) cmd, cmd.CommandText, commandParameters);
            if (transaction.Connection.State == ConnectionState.Closed)
            {
                transaction.Connection.Open();
            }
            return ExecuteDataset((SQLiteCommand) cmd);
        }

        public static DataSet ExecuteDataSet(SQLiteConnection cn, string commandText, object[] paramList)
        {
            SQLiteCommand cmd = cn.CreateCommand();
            cmd.CommandText = commandText;
            if (paramList != null)
            {
                AttachParameters(cmd, commandText, paramList);
            }
            DataSet ds = new DataSet();
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();
            cn.Close();
            return ds;
        }

        public static DataSet ExecuteDataSet(string connectionString, string commandText, object[] paramList)
        {
            SQLiteConnection cn = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = cn.CreateCommand();
            cmd.CommandText = commandText;
            if (paramList != null)
            {
                AttachParameters(cmd, commandText, paramList);
            }
            DataSet ds = new DataSet();
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();
            cn.Close();
            return ds;
        }

        public static int ExecuteNonQuery(IDbCommand cmd)
        {
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            int result = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            cmd.Dispose();
            return result;
        }

        public static int ExecuteNonQuery(SQLiteConnection cn, string commandText, params object[] paramList)
        {
            SQLiteCommand cmd = cn.CreateCommand();
            cmd.CommandText = commandText;
            AttachParameters(cmd, commandText, paramList);
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            int result = cmd.ExecuteNonQuery();
            cmd.Dispose();
            cn.Close();
            return result;
        }

        public static int ExecuteNonQuery(SQLiteTransaction transaction, string commandText, params object[] paramList)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rolled back or committed,                                                        please provide an open transaction.", "transaction");
            }
            IDbCommand cmd = transaction.Connection.CreateCommand();
            cmd.CommandText = commandText;
            AttachParameters((SQLiteCommand) cmd, cmd.CommandText, paramList);
            if (transaction.Connection.State == ConnectionState.Closed)
            {
                transaction.Connection.Open();
            }
            int result = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return result;
        }

        public static int ExecuteNonQuery(string connectionString, string commandText, params object[] paramList)
        {
            SQLiteConnection cn = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = cn.CreateCommand();
            cmd.CommandText = commandText;
            AttachParameters(cmd, commandText, paramList);
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            int result = cmd.ExecuteNonQuery();
            cmd.Dispose();
            cn.Close();
            return result;
        }

        public static int ExecuteNonQueryTypedParams(IDbCommand command, DataRow dataRow)
        {
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                AssignParameterValues(command.Parameters, dataRow);
                return ExecuteNonQuery(command);
            }
            return ExecuteNonQuery(command);
        }

        public static IDataReader ExecuteReader(SQLiteCommand cmd, string commandText, object[] paramList)
        {
            if (cmd.Connection == null)
            {
                throw new ArgumentException("Command must have live connection attached.", "cmd");
            }
            cmd.CommandText = commandText;
            AttachParameters(cmd, commandText, paramList);
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static object ExecuteScalar(string connectionString, string commandText, params object[] paramList)
        {
            SQLiteConnection cn = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = cn.CreateCommand();
            cmd.CommandText = commandText;
            AttachParameters(cmd, commandText, paramList);
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            object result = cmd.ExecuteScalar();
            cmd.Dispose();
            cn.Close();
            return result;
        }

        public static XmlReader ExecuteXmlReader(IDbCommand command)
        {
            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
            }
            SQLiteDataAdapter da = new SQLiteDataAdapter((SQLiteCommand) command);
            DataSet ds = new DataSet();
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            da.Fill(ds);
            StringReader stream = new StringReader(ds.GetXml());
            command.Connection.Close();
            return new XmlTextReader(stream);
        }

        public static void UpdateDataset(SQLiteCommand insertCommand, SQLiteCommand deleteCommand, SQLiteCommand updateCommand, DataSet dataSet, string tableName)
        {
            if (insertCommand == null)
            {
                throw new ArgumentNullException("insertCommand");
            }
            if (deleteCommand == null)
            {
                throw new ArgumentNullException("deleteCommand");
            }
            if (updateCommand == null)
            {
                throw new ArgumentNullException("updateCommand");
            }
            if ((tableName == null) || (tableName.Length == 0))
            {
                throw new ArgumentNullException("tableName");
            }
            using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter())
            {
                dataAdapter.UpdateCommand = updateCommand;
                dataAdapter.InsertCommand = insertCommand;
                dataAdapter.DeleteCommand = deleteCommand;
                dataAdapter.Update(dataSet, tableName);
                dataSet.AcceptChanges();
            }
        }
    }
}

