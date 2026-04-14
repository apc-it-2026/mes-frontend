using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI_FINAL_SCORE
{
    class RawData
    {
        string constrerp = $@"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.3.0.227)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME = APCMES)));User Id=mes00;Password=dbmes00;";
        public DataTable GetRawData(string Criteria, string ProdLine,string Month)
        {
          
            OracleConnection con = new OracleConnection(constrerp);
            OracleTransaction transaction = null;
            DataTable dt = new DataTable();

            try
            {
                con.Open();
                using (OracleCommand cmd = new OracleCommand("KPI_NEW.GET_RAWDATA", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_MONTH", OracleDbType.Varchar2).Value = Month;
                    cmd.Parameters.Add("P_CRITERIA", OracleDbType.Varchar2).Value = Criteria;
                    cmd.Parameters.Add("P_PRODLINE", OracleDbType.Varchar2).Value = ProdLine;
                    cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }


            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

    }
}
