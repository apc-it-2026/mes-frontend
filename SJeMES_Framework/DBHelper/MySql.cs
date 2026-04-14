using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace SJeMES_Framework.DBHelper
{
    /// <summary>
    /// MySQL数据库连接器
    /// </summary>
    public class MySQL:DataBaseAccess
    {
    

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public MySQL()
        {
            this.Connection = null;
            this.Command = null;
            this.DataAdapter = null;
            this.ConnectionOffline = null;
            this.CommandOffline = null;
            this.DataAdapterOffline = null;
            this.DataReader = null;
            this.DBType = "MySql";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ConnectionText">连接字符串</param>
        public MySQL(string ConnectionText)
        {
            this.ConnectionText = ConnectionText;
            this.Connection = new MySql.Data.MySqlClient.MySqlConnection(ConnectionText);
            this.Command = this.Connection.CreateCommand();
            this.Command.CommandTimeout = 900;
            this.DataAdapter = new MySql.Data.MySqlClient.MySqlDataAdapter();
            this.ConnectionOffline = new MySql.Data.MySqlClient.MySqlConnection(ConnectionText);
            this.CommandOffline = this.ConnectionOffline.CreateCommand();
            this.CommandOffline.CommandTimeout = 900;
            this.DataAdapterOffline = new MySql.Data.MySqlClient.MySqlDataAdapter();
            this.DataReader = null;
            this.DBType = "MySql";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Server">服务器</param>
        /// <param name="DataBaseName">数据库名称</param>
        /// <param name="UserId">数据库用户</param>
        /// <param name="Password">用户密码</param>
        public MySQL(string Server, string DataBaseName, string UserId, string Password)
        {
            ConnectionText = "server=" + Server + @";database=" + DataBaseName + @";uid=" + UserId + @";pwd=" + Password + ";Charset=utf8;allow user variables=TRUE;";
            this.Connection = new MySql.Data.MySqlClient.MySqlConnection(ConnectionText);
            this.Command = this.Connection.CreateCommand();
            this.Command.CommandTimeout = 900;
            this.DataAdapter = new MySql.Data.MySqlClient.MySqlDataAdapter();
            this.ConnectionOffline = new MySql.Data.MySqlClient.MySqlConnection(ConnectionText);
            this.CommandOffline = this.ConnectionOffline.CreateCommand();
            this.CommandOffline.CommandTimeout = 900;
            this.DataAdapterOffline = new MySql.Data.MySqlClient.MySqlDataAdapter();
            this.DataReader = null;
            this.DBType = "MySql";

        } 

        #endregion

        #region GetDateTimeNow
        /// <summary>
        /// 使用离线数据库连接器，无需使用Open方法，获取数据库当前时间
        /// </summary>
        public override DateTime GetDateTimeNow()
        {
            DateTime ret = DateTime.Now;
            using (MySql.Data.MySqlClient.MySqlConnection con =new MySql.Data.MySqlClient.MySqlConnection(this.ConnectionText))
            {
                using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select now()", con))
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
