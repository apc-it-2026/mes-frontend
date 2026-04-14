using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;



namespace SJeMES_Framework.DBHelper
{
    /// <summary>
    /// SqlServer数据库连接器
    /// </summary>
    public class SqlServer:DataBaseAccess
    {
        

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlServer()
        {
            this.Connection = null;
            this.Command = null;
            this.DataAdapter = null;
            this.ConnectionOffline = null;
            this.CommandOffline = null;
            this.DataAdapterOffline = null;
            this.DataReader = null;
            this.DBType = "SqlServer";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ConnectionText">连接字符串</param>
        public SqlServer(string ConnectionText)
        {
            this.ConnectionText = ConnectionText;
            this.Connection = new System.Data.SqlClient.SqlConnection(ConnectionText);
            this.Command = this.Connection.CreateCommand();
            this.Command.CommandTimeout = 900;
            this.DataAdapter = new System.Data.SqlClient.SqlDataAdapter();
            this.ConnectionOffline = new System.Data.SqlClient.SqlConnection(ConnectionText);
            this.CommandOffline = this.ConnectionOffline.CreateCommand();
            this.CommandOffline.CommandTimeout = 900;
            this.DataAdapterOffline = new System.Data.SqlClient.SqlDataAdapter();
            this.DataReader = null;
            this.DBType = "SqlServer";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Server">服务器</param>
        /// <param name="DataBaseName">数据库名称</param>
        /// <param name="UserId">数据库用户</param>
        /// <param name="Password">用户密码</param>
        public SqlServer(string Server, string DataBaseName, string UserId, string Password)
        {
            this.ConnectionText = "server=" + Server + @";database=" + DataBaseName + @";user id=" + UserId + @";pwd=" + Password + ";";
            this.Connection = new System.Data.SqlClient.SqlConnection(ConnectionText);
            this.Command = this.Connection.CreateCommand();
            this.Command.CommandTimeout = 900;
            this.DataAdapter = new System.Data.SqlClient.SqlDataAdapter();
            this.ConnectionOffline = new System.Data.SqlClient.SqlConnection(ConnectionText);
            this.CommandOffline = this.ConnectionOffline.CreateCommand();
            this.CommandOffline.CommandTimeout = 900;
            this.DataAdapterOffline = new System.Data.SqlClient.SqlDataAdapter();
            this.DataReader = null;
            this.DBType = "SqlServer";

        } 

        #endregion

        #region GetDateTimeNow
        /// <summary>
        /// 获取数据库当前时间
        /// </summary>
        public override DateTime GetDateTimeNow()
        {
            DateTime ret = DateTime.Now;
            using(System.Data.SqlClient.SqlConnection con=new System.Data.SqlClient.SqlConnection(this.ConnectionText))
            {
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("select getdate()",con))
                {
                    con.Open();
                    ret = Convert.ToDateTime(cmd.ExecuteScalar());
                    con.Close();
                }
            }

            return ret;
        }
        #endregion
    }
}
