using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_IE
{
    class Efficiency
    {
        string constrerp = $@"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.3.0.227)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME = APCMES)));User Id=mes00;Password=dbmes00;";
        public OperationResult Efficienct_Target(DateTime proddate, string Plant,string ProdLine)
        {
            OperationResult result = new OperationResult();
            OracleConnection con = new OracleConnection(constrerp);
            OracleTransaction transaction = null;
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                transaction = con.BeginTransaction();
                string where = string.Empty;

                if(!string.IsNullOrEmpty(Plant))
                {
                    where += $@" and a.udf05 = '{Plant}'";
                }
                if (!string.IsNullOrEmpty(ProdLine))
                {
                    where += $@"and a.department_code = '{ProdLine}'";
                }

                string sql = $@"SELECT DEPARTMENT_CODE
  FROM BASE005M A
 WHERE A.FACTORY_SAP = '5001'
   AND A.UDF05 LIKE 'AP%'
   AND udf05 NOT in ('APA', 'APC', 'APEX', 'API', 'APO')
   AND UDF01 IN ('C', 'S', 'L')
   AND DEPARTMENT_CODE NOT LIKE '%MP%' {where}";
                OracleCommand cmd1 = new OracleCommand(sql, con);
                OracleDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    string DEPARTMENT_CODE = dr["DEPARTMENT_CODE"].ToString();
                    DateTime ProdDate = proddate;
                    using (OracleCommand cmd = new OracleCommand("SP_EFFICIENCY_TARGET", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_PROD_LINE", OracleDbType.Varchar2).Value = dr["DEPARTMENT_CODE"].ToString();
                        cmd.Parameters.Add("P_DATE", OracleDbType.Date).Value = ProdDate;
                        OracleParameter refCursorParam = new OracleParameter("V_CURSOR", OracleDbType.RefCursor);
                        refCursorParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(refCursorParam);
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {

                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }

                }
                transaction.Commit();
                result.Success = true;
                result.Message = "calculated Successfully";
                result.Data = dt;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                result.Success = false;
                result.Message = ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }
            return result;
        }

       
    }
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public DataTable Data { get; set; }

    }
}
