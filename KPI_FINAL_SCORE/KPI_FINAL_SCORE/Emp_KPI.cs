using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace KPI_FINAL_SCORE
{
    class Emp_KPI
    {
        string constrerp = $@"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.3.0.227)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME = APCMES)));User Id=mes00;Password=dbmes00;";
        public OperationResult Calculate_Emp_KPI(string ProdMonth,string position)
        {
            OperationResult result = new OperationResult();
            OracleConnection con = new OracleConnection(constrerp);
            OracleTransaction transaction = null;
            bool IsUploadEmp = false;
            try
            {
                con.Open();
                transaction = con.BeginTransaction();
                string sql = $@"
select MONTH_NAME AS MONTH,BARCODE AS EMP_NO, EMP_NAME,EMP_ROLE,UNIT
  from production_line_info a
 where a.month_name = '{ProdMonth}' and EMP_ROLE='{position}' and a.factory='5001'
   and BARCODE is not null
   and work_flow in ('C2B', 'C2S','C', 'L', 'S') and LINE_TYPE='S'";
                OracleCommand cmd1 = new OracleCommand(sql, con);
                OracleDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    string EMP_ROLE = dr["EMP_ROLE"].ToString();
                    string MONTH = dr["MONTH"].ToString().Replace("/", "");
                    if (EMP_ROLE.ToLower() == "assistant" || EMP_ROLE.ToLower() == "supervisor")
                    {
                        IsUploadEmp = true;
                        using (OracleCommand cmd = new OracleCommand("KPI_NEW.KPI_CALC_ASST_AND_SUP", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("P_MONTH", OracleDbType.Varchar2).Value = MONTH;
                            cmd.Parameters.Add("P_EMP_NO", OracleDbType.Varchar2).Value = dr["EMP_NO"].ToString();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    if (EMP_ROLE.ToLower() == "section head")
                    {
                        using (OracleCommand cmd = new OracleCommand("KPI_NEW.KPI_CALC_SH", con))
                        {
                            IsUploadEmp = true;
                            string EMP = dr["EMP_NO"].ToString();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("P_MONTH", OracleDbType.Varchar2).Value = MONTH;
                            cmd.Parameters.Add("P_EMP_NO", OracleDbType.Varchar2).Value = dr["EMP_NO"].ToString();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    if (EMP_ROLE.ToLower() == "gpic")
                    {
                        IsUploadEmp = true;
                        DataTable dt = new DataTable();
                        string sql2 = $@"select * from kpi_employee_final where lower(position) ='section head' and month='{MONTH}'";
                        OracleCommand cmd2 = new OracleCommand(sql2, con);
                        OracleDataAdapter da = new OracleDataAdapter(cmd2);
                        da.Fill(dt);
                        if (dt.Rows.Count == 0)
                        {
                            result.Success = false;
                            result.Message = "Section Head Data is not yet calculated";
                            return result;  
                        }
                        else
                        {
                            using (OracleCommand cmd = new OracleCommand("KPI_NEW.KPI_CALC_INCHARGE", con))
                            {
                                string EMP = dr["EMP_NO"].ToString();
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("P_MONTH", OracleDbType.Varchar2).Value = MONTH;
                                cmd.Parameters.Add("P_EMP_NO", OracleDbType.Varchar2).Value = dr["EMP_NO"].ToString();
                                cmd.ExecuteNonQuery();
                            }
                        }
                        
                    }
                    if (EMP_ROLE.ToLower() == "captain")
                    {
                        IsUploadEmp = true;
                        DataTable dt = new DataTable(); 
                        string sql2 = $@"select * from kpi_employee_final where lower(position) ='gpic' and month='{MONTH}'";
                        OracleCommand cmd2 = new OracleCommand(sql2, con);
                        OracleDataAdapter da = new OracleDataAdapter(cmd2);
                        da.Fill(dt);
                        if (dt.Rows.Count == 0)
                        {
                            result.Success = false;
                            result.Message = "Plant Incharge Data is not yet calculated";
                            return result;
                        }
                        else
                        {
                            using (OracleCommand cmd = new OracleCommand("KPI_NEW.KPI_CALC_CAPTAIN", con))
                            {
                                string EMP = dr["EMP_NO"].ToString();
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("P_MONTH", OracleDbType.Varchar2).Value = MONTH;
                                cmd.Parameters.Add("P_EMP_NO", OracleDbType.Varchar2).Value = dr["EMP_NO"].ToString();
                                cmd.ExecuteNonQuery();
                            }
                        }
                        
                    }

                }
                transaction.Commit();
                result.Success = IsUploadEmp;
                if (IsUploadEmp)
                {
                    result.Message = "calculated Successfully";
                }
                else
                {
                    result.Message = "Employee Details not yet uploaded";
                }
                
            }
            catch(Exception ex)
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

        internal DataTable Get_Emp_KPI(string ProdMonth, string position,string Plant,string Type)
        {
            DataTable dt = new DataTable();
            OracleConnection con = new OracleConnection(constrerp);
            try
            {
                con.Open();
                string sql_KPI = string.Empty;
                string where = string.Empty;
                if (!string.IsNullOrEmpty(position))
                {
                    where += $@" And position='{position}'";
                }
                if (!string.IsNullOrEmpty(Plant))
                {
                    where += $@" And plant='{Plant}'";
                }
                if (!string.IsNullOrEmpty(Type))
                {
                    where += $@" And work_flow='{Type}'";
                }
                if (position.ToLower() == "assistant" || position.ToLower() == "supervisor" || position.ToLower() == "gpic" || position.ToLower() == "captain")
                {
                    sql_KPI = $@"select month,
       emp_no,
       emp_name,
       position,
       resposible_unit,
       output_target_score,
       po_finish_score,
       b_grade_score,
       rft_score,
       repacking_score,
       size_label_score,
       replacement_score,
       kaizen_score,
       haulting,
       bonding_score,
       ie_score,
       kpi_score,
       case
         when updated_at is null then
          created_at
         else
          updated_at
       end as updated_at
  from kpi_employee_final
where month = '{ProdMonth.Replace("/", "")}' {where} ";
                }

                if (position.ToLower() == "section head")
                {
                    if(Type=="C" || Type == "S")
                    {
                        sql_KPI = $@"select month,
       emp_no,
       emp_name,
       position,
       resposible_unit,
       output_target_score,
       po_finish_score,
       b_grade_score,
       rft_score,
       repacking_score,
       size_label_score,
       replacement_score,
       kaizen_score,
       haulting,
       bonding_score,
       ie_score,
       kpi_score,
       case
         when updated_at is null then
          created_at
         else
          updated_at
       end as updated_at
  from kpi_employee_final
where month = '{ProdMonth.Replace("/", "")}' {where} ";
                    }
                    else
                    {
                        sql_KPI = $@"select 
  month,
  emp_no,
  emp_name,
  position,
  resposible_unit,
  target,
  output,
  output_target_percent,
  output_target_score,
  target_po,
  finish_po,
  po_finish_percent,
  po_finish_score,
  b_grades,
  repairs,
  b_grade_percent,
  b_grade_score,
  inspection_qty,
  qualified_qty,
  rft,
  rft_score,
  repacking_qty,
  repacking_percent,
  repacking_score,
  size_label_count,
  size_label_score,
  replacement_amount,
  replacement_paircost,
  replacement_score,
  kaizen_percent,
  kaizen_score,
  haulting,
  bonding_percent,
  bonding_score,
  ie_percent,
  ie_score,
  kpi_score,
  case
   when updated_at is null then
    created_at
   else
    updated_at
 end as updated_at
from kpi_employee_final
where  month = '{ProdMonth.Replace("/", "")}'{where}";
                    }
                    
                }


                OracleCommand cmd2 = new OracleCommand(sql_KPI, con);
                OracleDataAdapter da = new OracleDataAdapter(cmd2);
                da.Fill(dt);
            }

            catch
            {

            }
            finally
            {
                con.Close();
            }
            return dt;

        }

        public class OperationResult
        {
            public bool Success { get; set; }
            public string Message { get; set; }

        }
    }
}
