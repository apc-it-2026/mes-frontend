using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SJeMES_Framework_NETCore.DBHelper
{
    public class InsertData
    {
        public int RowCount = 0;
        public int OKRowCount = 0;
        public int OKTranCount = 0;
        public int OKRetCount = 0;
        public int TranCount = 0;
        public string DBType = string.Empty;
        public string DBServer = string.Empty;
        public string DBName = string.Empty;
        public string DBUser = string.Empty;
        public string DBPwd = string.Empty;
        public string DBFile = string.Empty;

        //public void DoWork(System.Data.DataTable dt,int TranCount)
        //{

        //    int TranRow = 0;
        //    TranRow = dt.Rows.Count / TranCount;
        //    RowCount = dt.Rows.Count;
        //    List<System.Data.DataTable> dts = new List<System.Data.DataTable>();

        //    for(int i=0;i<TranCount;i++)
        //    {
        //        System.Data.DataTable dttmp = dt.Copy();
        //        dttmp.TableName = dt.TableName;
        //        dttmp.Clear();

        //        if (i == TranCount - 1)
        //        {
        //            for (int k = i * TranRow; k < dt.Rows.Count; k++)
        //            {
        //                dttmp.Rows.Add(dt.Rows[k].ItemArray);
        //            }
        //        }
        //        else
        //        {
        //            for (int k = i * TranRow; k < TranRow * (i + 1); k++)
        //            {
        //                dttmp.Rows.Add(dt.Rows[k].ItemArray);
        //            }
        //        }

        //        dts.Add(dttmp);
        //    }

        //    foreach(System.Data.DataTable dttmp in dts)
        //    {

        //        Thread thread = new Thread(new ParameterizedThreadStart(ThreadWork));
        //        thread.IsBackground = true;
        //        thread.Start(dttmp);
        //    }



        //}


        public void DoWork(System.Data.DataTable dt, int TranCount)
        {

            int TranRow = 0;
            if(TranCount>=20)
            {
                TranCount = 1;
            }
            else
            {
                TranCount = 20 - TranCount;
            }

            this.TranCount = TranCount;
            TranRow = dt.Rows.Count / TranCount;
            RowCount = dt.Rows.Count;
            List<System.Data.DataTable> dts = new List<System.Data.DataTable>();

            for (int i = 0; i < TranCount; i++)
            {
                List<object> obj = new List<object>();
                obj.Add(dt);
                obj.Add(i);
                obj.Add(TranRow);

                Thread thread = new Thread(new ParameterizedThreadStart(ThreadWork));
                thread.IsBackground = true;
                thread.Start(obj);

            }




        }

        public void ThreadWork(object obj)
        {
            System.Data.DataTable dt = ((List<object>)obj)[0] as System.Data.DataTable;
            int i = Convert.ToInt32(((List<object>)obj)[1]);
            int TranRow = Convert.ToInt32(((List<object>)obj)[2]);

            DataBase DB = new DBHelper.DataBase(DBType, DBServer, DBName, DBUser, DBPwd, DBFile);

            if (i == TranCount - 1)
            {
                for (int k = i * TranRow; k < RowCount; k++)
                {
                    string sql = @"
INSERT INTO [" + dt.TableName + @"]
(";
                    string IC = string.Empty;
                    string IV = string.Empty;

                    foreach (System.Data.DataColumn dc in dt.Columns)
                    {
                        if (dc.ColumnName != "id" && dc.ColumnName != "guid" && dc.ColumnName != "timestamp")
                        {
                            IC += dc.ColumnName + ",";
                            IV += "'" + dt.Rows[k][dc.ColumnName].ToString() + "',";
                        }
                    }

                    IC = IC.Remove(IC.Length - 1);
                    IV = IV.Remove(IV.Length - 1);

                    sql += IC + ") VALUES (" + IV + @")";

                    try
                    {
                        if (DB.ExecuteNonQueryOffline(sql) == 1)
                        {
                            OKRetCount++;
                        }
                    }
                    catch { }

                    OKRowCount++;
                }
            }
            else
            {
                for (int k = i * TranRow; k < TranRow * (i + 1); k++)
                {
                    string sql = @"
INSERT INTO [" + dt.TableName + @"]
(";
                    string IC = string.Empty;
                    string IV = string.Empty;

                    foreach (System.Data.DataColumn dc in dt.Columns)
                    {
                        if (dc.ColumnName != "id" && dc.ColumnName != "guid" && dc.ColumnName != "timestamp")
                        {
                            IC += dc.ColumnName + ",";
                            IV += "'" + dt.Rows[k][dc.ColumnName].ToString() + "',";
                        }
                    }

                    IC = IC.Remove(IC.Length - 1);
                    IV = IV.Remove(IV.Length - 1);

                    sql += IC + ") VALUES (" + IV + @")";

                    try
                    {
                        if (DB.ExecuteNonQueryOffline(sql) == 1)
                        {
                            OKRetCount++;
                        }
                    }
                    catch { }

                    OKRowCount++;
                }
            }

            OKTranCount++;
        }
    }
}
