using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SJeMES_Framework_NETCore.DBHelper;

namespace IE_Efficiency_Eval
{
    internal class Cls_DBOpertions
    {

        string constrerp = "Data Source=(DESCRIPTION=" + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.3.0.227)(PORT=1521)))" + "(CONNECT_DATA=(SERVICE_NAME = APCMES)));" + "User Id=mes00;Password=dbmes00;";

        internal DataTable QueryIEEfficiencyDetails(string SearchIEDate,string process)
        {


            OracleConnection con = new OracleConnection(constrerp);
            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GET_IEEFFICIENCYDETAILS";
            cmd.Parameters.Add("P_OUTPUTDATE", OracleDbType.Varchar2).Value = SearchIEDate;
            cmd.Parameters.Add("P_PROCESS", OracleDbType.Varchar2).Value = process;
            cmd.Parameters.Add("P_RES", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;


        }




    }
}




