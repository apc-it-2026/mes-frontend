using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SJeMES_Framework.DBHelper
{
    /// <summary>
    /// 数据库连接器
    /// </summary>
    public abstract class DataBaseAccess
    {
        #region 变量
        private string dbType;
        private string connectiontext;
        private IDbCommand command;
        private IDbDataAdapter dataadapter;
        private IDbConnection connection;
        private IDbCommand commandoffline;
        private IDbDataAdapter dataadapteroffline;
        private IDbConnection connectionoffline;
        private IDataReader datareader;
        private IDbTransaction transaction;
        #endregion

        #region 属性

        #region string DBType 数据库类型
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string DBType
        {
            get
            {
                return dbType;
            }
            set
            {
                dbType = value;
            }

        }
        #endregion

        #region string ConnectionText 数据库连接字符串
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionText
        {
            get
            {
                return connectiontext;
            }
            set
            {
                connectiontext = value;
            }

        }
        #endregion

        #region System.Data.ConnectionState State 数据库连接状态
        /// <summary>
        /// 数据库连接状态
        /// </summary>
        public System.Data.ConnectionState State
        {
            get
            {
                if (this.connection != null)
                {
                    return this.connection.State;
                }
                else
                {
                    return ConnectionState.Closed;
                }
            
            }
          
        }
        #endregion

        #region System.Data.ConnectionState StateOffline 离线数据库连接状态
        /// <summary>
        /// 离线数据库连接状态
        /// </summary>
        public System.Data.ConnectionState StateOffline
        {
            get
            {
                if (this.connection != null)
                {
                    return this.connectionoffline.State;
                }
                else
                {
                    return ConnectionState.Closed;
                }

            }

        }
        #endregion

        #region IDbConnection Connection 数据库连接
        /// <summary>
        /// 数据库连接
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                return connection;
            }
            set { connection = value; }
        }
        #endregion 

        #region IDbConnection ConnectionOffline 离线数据库连接
        /// <summary>
        /// 离线数据库连接
        /// </summary>
        public IDbConnection ConnectionOffline
        {
            get
            {
                return connectionoffline;
            }
            set { connectionoffline = value; }
        }
        #endregion 

        #region IDbCommand Command 数据库命令
        /// <summary>
        /// 数据库命令
        /// </summary>
        public IDbCommand Command
        {
            get
            {
                return command;
            }
            set { command = value; }
        }
        #endregion 

        #region IDbCommand CommandOffline 离线数据库命令
        /// <summary>
        /// 离线数据库命令
        /// </summary>
        public IDbCommand CommandOffline
        {
            get
            {
                return commandoffline;
            }
            set { commandoffline = value; }
        }
        #endregion 

        #region IDbDataAdapter DataAdapter 数据库适配器
        /// <summary>
        /// 数据库适配器
        /// </summary>
        public IDbDataAdapter DataAdapter
        {
            get
            {
                return dataadapter;
            }
            set { dataadapter = value; }
        }
        #endregion 

        #region IDbDataAdapter DataAdapterOffline 离线数据库适配器
        /// <summary>
        /// 离线数据库适配器
        /// </summary>
        public IDbDataAdapter DataAdapterOffline
        {
            get
            {
                return dataadapteroffline;
            }
            set { dataadapteroffline = value; }
        }
        #endregion 

        #region IDataReader DataReader 数据库DataReader
        /// <summary>
        /// 数据库DataReader
        /// </summary>
        public IDataReader DataReader
        {
            get
            {
                return datareader;
            }
            set { datareader = value; }
        }
        #endregion 

        #region IDbTransaction Transaction 数据库事务
        /// <summary>
        /// 数据库事务
        /// </summary>
        public IDbTransaction Transaction
        {
            get
            {
                return transaction;
            }
            set { transaction = value; }
        }
        #endregion 

        #endregion

        #region 使用离线连接器的方法

        #region GetScalar

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取第一行第一列对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public virtual object GetScalar(string sql)
        {

            object ret = new object();

            try
            {
                this.ConnectionOffline.Open();
                this.CommandOffline.CommandType = CommandType.Text;
                this.CommandOffline.CommandText = sql;
                this.CommandOffline.Parameters.Clear();

                ret = this.CommandOffline.ExecuteScalar();
            }
            catch { throw; }
            finally
            {
                this.ConnectionOffline.Close();
            }

            return ret;
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取第一行第一列对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public virtual object GetScalar(string sql, Dictionary<string, object> Parameters)
        {
            object ret = new object();
  
            try
            {

                this.ConnectionOffline.Open();
                this.CommandOffline.CommandType = CommandType.Text;
                this.CommandOffline.CommandText = sql;
                this.CommandOffline.Parameters.Clear();
                foreach (string key in Parameters.Keys)
                {
                    if (DBType.ToLower() == "SqlServer".ToLower())
                    {
                        this.CommandOffline.Parameters.Add(new System.Data.SqlClient.SqlParameter(key, Parameters[key]));
                    }
                    else if (DBType.ToLower() == "MySql".ToLower())
                    {
                        this.CommandOffline.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter(key, Parameters[key]));
                    }
                    else
                    {
                        this.CommandOffline.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter(key, Parameters[key]));
                    }
                }

                ret = this.CommandOffline.ExecuteScalar();
            }
            catch { throw; }
            finally
            {
                this.ConnectionOffline.Close();
            }
            return ret;
        }

        #endregion

        #region GetDataTable

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取DataTable对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public virtual DataTable GetDataTable(string sql)
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            try
            {
                this.ConnectionOffline.Open();
                this.CommandOffline.CommandType = CommandType.Text;
                this.CommandOffline.CommandText = sql;
                this.CommandOffline.Parameters.Clear();

                this.DataAdapterOffline.SelectCommand = this.CommandOffline;

                this.DataAdapterOffline.Fill(ds);
                dt = ds.Tables[0];

                if (DBType.ToLower() == "Oracle".ToLower())
                {
                    if (dt.Columns.Contains("TIMESTAMP"))
                        dt.Columns.Remove("TIMESTAMP");
                }
            }
            catch { throw; }
            finally
            {
                ds.Dispose();
                this.ConnectionOffline.Close();
            }
            return dt;
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取DataTable对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public virtual DataTable GetDataTable(string sql, Dictionary<string, object> Parameters)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {

                this.ConnectionOffline.Open();
                this.CommandOffline.CommandType = CommandType.Text;
                this.CommandOffline.CommandText = sql;
                this.CommandOffline.Parameters.Clear();
                foreach (string key in Parameters.Keys)
                {
                    if (DBType.ToLower() == "SqlServer".ToLower())
                    {
                        this.CommandOffline.Parameters.Add(new System.Data.SqlClient.SqlParameter(key, Parameters[key]));
                    }
                    else if (DBType.ToLower() == "MySql".ToLower())
                    {
                        this.CommandOffline.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter(key, Parameters[key]));
                    }
                    else
                    {
                        if (sql.Contains(":" + key))
                        {
                            Oracle.ManagedDataAccess.Client.OracleParameter p = new Oracle.ManagedDataAccess.Client.OracleParameter(key, Oracle.ManagedDataAccess.Client.OracleDbType.NChar);
                            p.Value = Parameters[key];
                            this.CommandOffline.Parameters.Add(p);
                            
                        }
                    }
                }

                this.DataAdapterOffline.SelectCommand = this.CommandOffline;

                this.DataAdapterOffline.Fill(ds);
                dt = ds.Tables[0];

                

                if (DBType.ToLower() == "Oracle".ToLower())
                {
                    if (dt.Columns.Contains("TIMESTAMP"))
                        dt.Columns.Remove("TIMESTAMP");
                }
            }
            catch { throw; }
            finally
            {
                ds.Dispose();
                this.ConnectionOffline.Close();
            }
            return dt;
        }

        #endregion



        #region GetDictionary

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取Dictionary对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public virtual Dictionary<string,object> GetDictionary(string sql)
        {
            Dictionary<string,object> ret = new Dictionary<string, object>();

            DataTable dt = this.GetDataTable(sql);

            if(dt.Rows.Count>0)
            {
                foreach(DataColumn dc in dt.Columns)
                {
                    ret.Add(dc.ColumnName, dt.Rows[0][dc.ColumnName].ToString());
                }
            }

            return ret;
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取Dictionary对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public virtual Dictionary<string, object> GetDictionary(string sql, Dictionary<string, object> Parameters)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>();

       

            DataTable dt = this.GetDataTable(sql,Parameters);

            if (dt.Rows.Count > 0)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    ret.Add(dc.ColumnName, dt.Rows[0][dc.ColumnName].ToString());
                }
            }


            return ret;
        }

        #endregion

        #region GetDataTableReader

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取DataTableReader对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public virtual IDataReader GetDataTableReader(string sql)
        {
            IDataReader dr = null;

            dr = this.GetDataTable(sql).CreateDataReader();

            return dr;
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取DataTableReader对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public virtual IDataReader GetDataTableReader(string sql, Dictionary<string, object> Parameters)
        {
            IDataReader dr = null;

            dr = this.GetDataTable(sql, Parameters).CreateDataReader();

            return dr;
        }

        #endregion

        #region GetString

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取String对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public virtual string GetString(string sql)
        {
            string ret = string.Empty;

            IDataReader dr = this.GetDataTable(sql).CreateDataReader();

            if (dr.Read())
            {
                ret = dr[0].ToString();
            }

            return ret;
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取String对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public virtual string GetString(string sql, Dictionary<string, object> Parameters)
        {
            string ret = string.Empty;

            IDataReader dr = this.GetDataTable(sql, Parameters).CreateDataReader();

            if (dr.Read())
            {
                ret = dr[0].ToString();
            }

            return ret;
        }

        #endregion

        #region GetInt16

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取Int16对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public virtual Int16 GetInt16(string sql)
        {
            Int16 ret = new short();

            IDataReader dr = this.GetDataTable(sql).CreateDataReader();

            if (dr.Read())
            {
                ret = Convert.ToInt16(dr[0]);
            }

            return ret;
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取Int16对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public virtual Int16 GetInt16(string sql, Dictionary<string, object> Parameters)
        {
            Int16 ret = new short();

            IDataReader dr = this.GetDataTable(sql, Parameters).CreateDataReader();

            if (dr.Read())
            {
                ret = Convert.ToInt16(dr[0]);
            }

            return ret;
        }

        #endregion

        #region GetInt32

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取Int32对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public virtual Int32 GetInt32(string sql)
        {
            Int32 ret = new int();

            IDataReader dr = this.GetDataTable(sql).CreateDataReader();

            if (dr.Read())
            {
                ret = Convert.ToInt32(dr[0]);
            }

            return ret;
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取Int32对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public virtual Int32 GetInt32(string sql, Dictionary<string, object> Parameters)
        {
            Int32 ret = new int();

            IDataReader dr = this.GetDataTable(sql, Parameters).CreateDataReader();

            if (dr.Read())
            {
                ret = Convert.ToInt32(dr[0]);
            }

            return ret;
        }

        #endregion

        #region GetInt64

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取Int64对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public virtual Int64 GetInt64(string sql)
        {
            Int64 ret = new long();

            IDataReader dr = this.GetDataTable(sql).CreateDataReader();

            if (dr.Read())
            {
                ret = Convert.ToInt64(dr[0]);
            }

            return ret;
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取Int64对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public virtual Int64 GetInt64(string sql, Dictionary<string, object> Parameters)
        {
            Int64 ret = new long();

            IDataReader dr = this.GetDataTable(sql, Parameters).CreateDataReader();

            if (dr.Read())
            {
                ret = Convert.ToInt64(dr[0]);
            }

            return ret;
        }

        #endregion

        #region GetBool

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取Bool对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public virtual bool GetBool(string sql)
        {
            bool ret = new bool();

            IDataReader dr = this.GetDataTable(sql).CreateDataReader();

            if (dr.Read())
            {
                ret = Convert.ToBoolean(dr[0]);
            }

            return ret;
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取Bool对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public virtual bool GetBool(string sql, Dictionary<string, object> Parameters)
        {
            bool ret = new bool();

            IDataReader dr = this.GetDataTable(sql, Parameters).CreateDataReader();

            if (dr.Read())
            {
                ret = Convert.ToBoolean(dr[0]);
            }

            return ret;
        }

        #endregion

        #region GetDecimal

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取Decimal对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public virtual decimal GetDecimal(string sql)
        {
            decimal ret = new decimal();

            IDataReader dr = this.GetDataTable(sql).CreateDataReader();

            if (dr.Read())
            {
                ret = Convert.ToDecimal(dr[0]);
            }

            return ret;
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取Decimal对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public virtual decimal GetDecimal(string sql, Dictionary<string, object> Parameters)
        {
            decimal ret = new decimal();

            IDataReader dr = this.GetDataTable(sql, Parameters).CreateDataReader();

            if (dr.Read())
            {
                ret = Convert.ToDecimal(dr[0]);
            }

            return ret;
        }

        #endregion

        #region GetDouble

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取Double对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public virtual double GetDouble(string sql)
        {
            double ret = new double();

            IDataReader dr = this.GetDataTable(sql).CreateDataReader();

            if (dr.Read())
            {
                ret = Convert.ToDouble(dr[0]);
            }

            return ret;
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取Double对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public virtual double GetDouble(string sql, Dictionary<string, object> Parameters)
        {
            double ret = new double();

            IDataReader dr = this.GetDataTable(sql, Parameters).CreateDataReader();

            if (dr.Read())
            {
                ret = Convert.ToDouble(dr[0]);
            }

            return ret;
        }

        #endregion

        #region GetFloat

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取Float对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public virtual float GetFloat(string sql)
        {
            float ret = new float();

            IDataReader dr = this.GetDataTable(sql).CreateDataReader();

            if (dr.Read())
            {
                ret = (float)Convert.ToDecimal(dr[0]);
            }

            return ret;
        }

        /// <summary>
        ///使用离线数据库连接器，无需使用Open方法， 获取Float对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public virtual float GetFloat(string sql, Dictionary<string, object> Parameters)
        {
            float ret = new float();

            IDataReader dr = this.GetDataTable(sql, Parameters).CreateDataReader();

            if (dr.Read())
            {
                ret = (float)Convert.ToDecimal(dr[0]);
            }

            return ret;
        }

        #endregion

        #region GetChar

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取Char对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public virtual char GetChar(string sql)
        {
            char ret = new char();

            IDataReader dr = this.GetDataTable(sql).CreateDataReader();

            if (dr.Read())
            {
                ret = Convert.ToChar(dr[0]);
            }

            return ret;
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取Char对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public virtual char GetChar(string sql, Dictionary<string, object> Parameters)
        {
            char ret = new char();

            IDataReader dr = this.GetDataTable(sql, Parameters).CreateDataReader();

            if (dr.Read())
            {
                ret = Convert.ToChar(dr[0]);
            }

            return ret;
        }

        #endregion

        #region GetDateTime

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取DateTime对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public virtual DateTime GetDateTime(string sql)
        {
            DateTime ret = new DateTime();

            IDataReader dr = this.GetDataTable(sql).CreateDataReader();

            if (dr.Read())
            {
                ret = Convert.ToDateTime(dr[0]);
            }

            return ret;
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取DateTime对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public virtual DateTime GetDateTime(string sql, Dictionary<string, object> Parameters)
        {
            DateTime ret = new DateTime();

            IDataReader dr = this.GetDataTable(sql, Parameters).CreateDataReader();

            if (dr.Read())
            {
                ret = Convert.ToDateTime(dr[0]);
            }

            return ret;
        }

        #endregion

        #region ExecuteProcedureOffline

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，执行存储过程
        /// </summary>
        /// <param name="ProcedureName">存储过程名称</param>
        /// <returns></returns>
        public virtual Dictionary<string, object> ExecuteProcedureOffline(string ProcedureName)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>();
            int NORA = 0;
            ret.Add("IsSuccess", false);
            ret.Add("NORA", NORA);

            try
            {
                this.CommandOffline.CommandType = CommandType.StoredProcedure;

                this.CommandOffline.CommandText = ProcedureName;

                this.ConnectionOffline.Open();
                NORA = this.CommandOffline.ExecuteNonQuery();
                this.ConnectionOffline.Close();

                if (NORA > 0)
                {
                    ret["IsSuccess"] = true;
                    ret["NORA"] = NORA;
                }

                foreach (IDataParameter p in CommandOffline.Parameters)
                {
                    if (p.Direction == ParameterDirection.Output ||
                        p.Direction == ParameterDirection.InputOutput ||
                        p.Direction == ParameterDirection.ReturnValue)
                    {
                        ret.Add(p.ParameterName, p.Value);
                    }
                }

            }
            catch { throw; }
            finally { this.ConnectionOffline.Close(); }

            return ret;
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，执行存储过程
        /// </summary>
        /// <param name="ProcedureName">存储过程名称</param>
        /// <returns></returns>
        public virtual DataTable ExecuteProcedureOfflineToDataTable(string ProcedureName)
        {
            DataTable ret = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                this.CommandOffline.CommandType = CommandType.StoredProcedure;

                this.CommandOffline.CommandText = ProcedureName;

                this.ConnectionOffline.Open();
                this.DataAdapterOffline.SelectCommand = this.CommandOffline;
                this.DataAdapterOffline.Fill(ds);
                ret = ds.Tables[0];
                this.ConnectionOffline.Close();

            }
            catch { throw; }
            finally { this.ConnectionOffline.Close(); }

            return ret;
        }

        #endregion

        #region ExecuteNonQueryOffline

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，执行命令并返回影响行数
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public virtual int ExecuteNonQueryOffline(string sql)
        {

            int ret = new int();
            try
            {
                this.ConnectionOffline.Open();
                this.CommandOffline.CommandType = CommandType.Text;
                this.CommandOffline.CommandText = sql;
                this.CommandOffline.Parameters.Clear();

                ret = this.CommandOffline.ExecuteNonQuery();
            }
            catch { throw; }
            finally
            {
                this.ConnectionOffline.Close();
            }
            return ret;
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，执行命令并返回影响行数
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public virtual int ExecuteNonQueryOffline(string sql, Dictionary<string, object> Parameters)
        {
            int ret = new int();
            try
            {

                this.ConnectionOffline.Open();
                this.CommandOffline.CommandType = CommandType.Text;
                this.CommandOffline.CommandText = sql;
                this.CommandOffline.Parameters.Clear();
                foreach (string key in Parameters.Keys)
                {
                    if (DBType.ToLower() == "SqlServer".ToLower())
                    {
                        this.CommandOffline.Parameters.Add(new System.Data.SqlClient.SqlParameter(key, Parameters[key]));
                    }
                    else if (DBType.ToLower() == "MySql".ToLower())
                    {
                        this.CommandOffline.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter(key, Parameters[key]));
                    }
                    else
                    {
                        if (sql.Contains(":" + key))
                        {
                            this.CommandOffline.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter(key, Parameters[key]));
                        }
                    }
                }

                ret = this.CommandOffline.ExecuteNonQuery();
            }
            catch { throw; }
            finally
            {
                this.ConnectionOffline.Close();
            }
            return ret;
        }

        #endregion

        #region AddProcedureParameterOffline

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，添加存储过程参数
        /// </summary>
        /// <param name="ParameterName">参数名</param>
        /// <param name="ParameterValue">参数值</param>
        /// <param name="DbType">对象类型</param>
        /// <param name="Direction">参数类型</param>
        public void AddProcedureParameterOffline(string ParameterName, object ParameterValue, System.Data.DbType DbType, System.Data.ParameterDirection Direction)
        {
            System.Data.IDataParameter p = this.CommandOffline.CreateParameter();
            p.ParameterName = ParameterName;
            p.Value = ParameterValue;
            p.DbType = DbType;
            p.Direction = Direction;

            this.CommandOffline.Parameters.Add(p);
        }

        #endregion

        #region ProcedureParameterInitializeOffline

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，初始化存储过程参数
        /// </summary>
        public void ProcedureParameterInitializeOffline()
        {
            this.CommandOffline.Parameters.Clear();
        }

        #endregion

        #region GetDateTimeNow

        /// <summary>
        /// 获取数据库当前时间
        /// </summary>
        public abstract DateTime GetDateTimeNow();
        #endregion

        #endregion

        #region 使用在线连接器的方法

        #region ExecuteScalar

        /// <summary>
        /// 获取第一行第一列对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public virtual object ExecuteScalar(string sql)
        {

            object ret = new object();


            this.Command.CommandType = CommandType.Text;
            this.Command.CommandText = sql;
            this.Command.Parameters.Clear();

            ret = this.Command.ExecuteScalar();

            return ret;
        }

        /// <summary>
        /// 获取第一行第一列对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public virtual object ExecuteScalar(string sql, Dictionary<string, object> Parameters)
        {
            object ret = new object();

            this.Command.CommandType = CommandType.Text;
            this.Command.CommandText = sql;
            this.Command.Parameters.Clear();
            foreach (string key in Parameters.Keys)
            {
                if (DBType.ToLower() == "SqlServer".ToLower())
                {
                    this.Command.Parameters.Add(new System.Data.SqlClient.SqlParameter(key, Parameters[key]));
                }
                else if (DBType.ToLower() == "MySql".ToLower())
                {
                    this.Command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter(key, Parameters[key]));
                }
                else
                {
                    if (sql.Contains(":" + key))
                    {
                        this.Command.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter(key, Parameters[key]));
                    }
                }
            }

            ret = this.Command.ExecuteScalar();

            return ret;
        }

        #endregion

        #region ExecuteReader

        /// <summary>
        /// 使用在线连接器获取DataReader,使用完毕后记得用Close
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public virtual void ExecuteReader(string sql)
        {
            this.Command.CommandType = CommandType.Text;
            this.Command.CommandText = sql;
            this.Command.Parameters.Clear();

            this.DataReader = this.Command.ExecuteReader();
        }

        /// <summary>
        /// 使用在线连接器获取DataReader,使用完毕后记得用Close
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        public virtual void ExecuteReader(string sql, Dictionary<string, object> Parameters)
        {

            this.Command.CommandType = CommandType.Text;
            this.Command.CommandText = sql;
            this.Command.Parameters.Clear();
            foreach (string key in Parameters.Keys)
            {
                if (DBType.ToLower() == "SqlServer".ToLower())
                {
                    this.Command.Parameters.Add(new System.Data.SqlClient.SqlParameter(key, Parameters[key]));
                }
                else if (DBType.ToLower() == "MySql".ToLower())
                {
                    this.Command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter(key, Parameters[key]));
                }
                else
                {
                    if (sql.Contains(":" + key))
                    {
                        this.Command.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter(key, Parameters[key]));
                    }
                }
            }

            this.DataReader = this.Command.ExecuteReader();
        }

        #endregion

        #region ExecuteReaderSingleRow

        /// <summary>
        /// 使用在线连接器获取第一行的DataReader,使用完毕后记得用Close
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public virtual void ExecuteReaderSingleRow(string sql)
        {
            this.Command.CommandType = CommandType.Text;
            this.Command.CommandText = sql;
            this.Command.Parameters.Clear();

            this.DataReader = this.Command.ExecuteReader(CommandBehavior.SingleRow);
        }

        /// <summary>
        /// 使用在线连接器获取第一行的DataReader,使用完毕后记得用Close
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        public virtual void ExecuteReaderSingleRow(string sql, Dictionary<string, object> Parameters)
        {

            this.Command.CommandType = CommandType.Text;
            this.Command.CommandText = sql;
            this.Command.Parameters.Clear();
            foreach (string key in Parameters.Keys)
            {
                if (DBType.ToLower() == "SqlServer".ToLower())
                {
                    this.Command.Parameters.Add(new System.Data.SqlClient.SqlParameter(key, Parameters[key]));
                }
                else if (DBType.ToLower() == "MySql".ToLower())
                {
                    this.Command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter(key, Parameters[key]));
                }
                else
                {
                    if (sql.Contains(":" + key))
                    {
                        this.Command.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter(key, Parameters[key]));
                    }
                }
            }

            this.DataReader = this.Command.ExecuteReader(CommandBehavior.SingleRow);
        }

        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// 执行命令并返回影响行数
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public virtual int ExecuteNonQuery(string sql)
        {
            this.Command.CommandType = CommandType.Text;
            this.Command.CommandText = sql;
            this.Command.Parameters.Clear();

            return this.Command.ExecuteNonQuery();
        }

        /// <summary>
        /// 执行命令并返回影响行数
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        public virtual int ExecuteNonQuery(string sql, Dictionary<string, object> Parameters)
        {
            this.Command.CommandType = CommandType.Text;
            this.Command.CommandText = sql;
            this.Command.Parameters.Clear();
            foreach (string key in Parameters.Keys)
            {
                if (DBType.ToLower() == "SqlServer".ToLower())
                {
                    this.Command.Parameters.Add(new System.Data.SqlClient.SqlParameter(key, Parameters[key]));
                }
                else if (DBType.ToLower() == "MySql".ToLower())
                {
                    this.Command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter(key, Parameters[key]));
                }
                else
                {
                    if (sql.Contains(":" + key))
                    {
                        this.Command.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter(key, Parameters[key]));
                    }
                }
            }

            return this.Command.ExecuteNonQuery();
        }

        #endregion

        #region ExecuteProcedure

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="ProcedureName">存储过程名称</param>
        /// <returns></returns>
        public virtual Dictionary<string, object> ExecuteProcedure(string ProcedureName)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>();
            int NORA = 0;
            ret.Add("IsSuccess", false);
            ret.Add("NORA", NORA);


            this.Command.CommandType = CommandType.StoredProcedure;

            this.Command.CommandText = ProcedureName;

            NORA = this.Command.ExecuteNonQuery();

            if (NORA > 0)
            {
                ret["IsSuccess"] = true;
                ret["NORA"] = NORA;
            }

            foreach (IDataParameter p in Command.Parameters)
            {
                if (p.Direction == ParameterDirection.Output ||
                    p.Direction == ParameterDirection.InputOutput ||
                    p.Direction == ParameterDirection.ReturnValue)
                {
                    ret.Add(p.ParameterName, p.Value);
                }
            }


            return ret;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="ProcedureName">存储过程名称</param>
        /// <returns></returns>
        public virtual DataTable ExecuteProcedureToDataTable(string ProcedureName)
        {
            DataTable ret = new DataTable();
            DataSet ds = new DataSet();

            this.Command.CommandType = CommandType.StoredProcedure;

            this.Command.CommandText = ProcedureName;

            this.DataAdapter.SelectCommand = this.Command;
            this.DataAdapter.Fill(ds);
            ret = ds.Tables[0];


            return ret;
        }

        #endregion

        #region AddProcedureParameter

        /// <summary>
        /// 添加存储过程参数
        /// </summary>
        /// <param name="ParameterName">参数名</param>
        /// <param name="ParameterValue">参数值</param>
        /// <param name="DbType">对象类型</param>
        /// <param name="Direction">参数类型</param>
        public void AddProcedureParameter(string ParameterName, object ParameterValue, System.Data.DbType DbType, System.Data.ParameterDirection Direction)
        {
            System.Data.IDataParameter p = this.Command.CreateParameter();
            p.ParameterName = ParameterName;
            p.Value = ParameterValue;
            p.DbType = DbType;
            p.Direction = Direction;

            this.Command.Parameters.Add(p);
        }

        #endregion

        #region ProcedureParameterInitialize

        /// <summary>
        /// 初始化存储过程参数
        /// </summary>
        public void ProcedureParameterInitialize()
        {
            this.Command.Parameters.Clear();
        }

        #endregion

        #region BeginTransaction

        /// <summary>
        /// 数据库开始事务
        /// </summary>
        public void BeginTransaction()
        {
            this.Transaction = this.Connection.BeginTransaction();
            this.Command.Transaction = this.Transaction;
        }

        #endregion

        #region GetDataTable
        public virtual DataTable GetDataTablebyline(string sql)
        {

            DataTable dt = new DataTable();
            
            DataSet ds = new DataSet();
                this.Command.CommandType = CommandType.Text;
                this.Command.CommandText = sql;
                this.DataAdapter.SelectCommand = this.Command;
                this.DataAdapter.Fill(ds); 
                dt = ds.Tables[0];
               return dt;
        }

        public virtual DataTable GetDataTablebyline(string sql, Dictionary<string, object> Parameters)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
                this.Command.CommandType = CommandType.Text;
                this.Command.CommandText = sql;
                foreach (string key in Parameters.Keys)
                {
                    if (DBType.ToLower() == "SqlServer".ToLower())
                    {
                        this.Command.Parameters.Add(new System.Data.SqlClient.SqlParameter(key, Parameters[key]));
                    }
                    else if (DBType.ToLower() == "MySql".ToLower())
                    {
                        this.Command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter(key, Parameters[key]));
                    }
                    else
                    {
                        if (sql.Contains(":" + key))
                        {
                            Oracle.ManagedDataAccess.Client.OracleParameter p = new Oracle.ManagedDataAccess.Client.OracleParameter(key, Oracle.ManagedDataAccess.Client.OracleDbType.NChar);
                            p.Value = Parameters[key];
                            this.Command.Parameters.Add(p);

                        }
                    }
                }
                this.DataAdapter.SelectCommand = this.Command;
                this.DataAdapter.Fill(ds);
                dt = ds.Tables[0];
                //if (DBType.ToLower() == "Oracle".ToLower())
                //{
                //    if (dt.Columns.Contains("TIMESTAMP"))
                //        dt.Columns.Remove("TIMESTAMP");
                //}
            return dt;
        }
        #endregion

        #region GetString

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取String对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <returns></returns>
        public virtual string GetStringline(string sql)
        {
            string ret = string.Empty;

            IDataReader dr = this.GetDataTablebyline(sql).CreateDataReader();

            if (dr.Read())
            {
                ret = dr[0].ToString();
            }

            return ret;
        }

        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取String对象
        /// </summary>
        /// <param name="sql">命令</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public virtual string GetStringline(string sql, Dictionary<string, object> Parameters)
        {
            string ret = string.Empty;

            IDataReader dr = this.GetDataTablebyline(sql, Parameters).CreateDataReader();

            if (dr.Read())
            {
                ret = dr[0].ToString();
            }

            return ret;
        }

        #endregion


        #region Commit

        /// <summary>
        /// 提交数据库事务
        /// </summary>
        public void Commit()
        {
            this.Transaction.Commit();
        }

        #endregion

        #region Rollback

        /// <summary>
        /// 回滚数据库事务
        /// </summary>
        public void Rollback()
        {
            this.Transaction.Rollback();
        }

        #endregion

        #region Open

        /// <summary>
        /// 打开在线数据库连接
        /// </summary>
        public void Open()
        {
            this.Connection.Open();
        }

        #endregion

        #region Close

        /// <summary>
        /// 关闭在线数据库连接
        /// </summary>
        public void Close()
        {
            this.Connection.Close();

        }

        #endregion

        #endregion

        
    }
}
