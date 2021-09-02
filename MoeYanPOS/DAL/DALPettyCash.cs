using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoeYanPOS.BOL;
using System.Data;
using System.Data.SqlClient;
using MoeYanPOS.Function;

namespace MoeYanPOS.DAL
{
    class DALPettyCash
    {

        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string constr = MoeYanConfiguration.GetConnection();
        #endregion

        #region  "SavePettyCash"
        public int SavePettyCash(BOLPettyCash bolpettycash)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_InsertPettyCash", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@Date", bolpettycash.Date);
                cmd.Parameters.AddWithValue("@LocationID", bolpettycash.LocationID);
                cmd.Parameters.AddWithValue("@Amount", bolpettycash.Amount);
                cmd.Parameters.AddWithValue("@Remark", bolpettycash.Remark);
                cmd.Parameters.AddWithValue("@UserID", bolpettycash.UserID);
                cmd.Parameters.AddWithValue("@IsGetAmt", bolpettycash.IsGetAmt);
                cmd.Parameters.AddWithValue("@IsPaidAmt", bolpettycash.IsPaidAmt);
                cmd.Parameters.AddWithValue("@Type", bolpettycash.Type);
                cmd.Parameters.AddWithValue("@VoucherNo", bolpettycash.VoucherNo);

                issaved = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return issaved;
        }
        #endregion

        #region "GetPettyCashByPettyCashID"
        public List<BOLPettyCash> GetPettyCashByPettyCashID(long pettycashid)
        {
            List<BOLPettyCash> lstpettycashlist = new List<BOLPettyCash>();
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetPettyCashByPettyCashID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pettycashid", pettycashid);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    BOLPettyCash bolpettycash = new BOLPettyCash();
                    bolpettycash.ID = long.Parse(reader["ID"].ToString());
                    bolpettycash.Date = DateTime.Parse(reader["Date"].ToString());
                    bolpettycash.Amount = Decimal.Parse(reader["Amount"].ToString());
                    bolpettycash.Remark = reader["Remark"].ToString();
                    bolpettycash.UserID = Int32.Parse(reader["UserID"].ToString());
                    bolpettycash.LocationID = long.Parse(reader["LocationID"].ToString());
                    bolpettycash.IsGetAmt = Boolean.Parse(reader["IsGetAmt"].ToString());
                    bolpettycash.Type = reader["Type"].ToString();
                    bolpettycash.VoucherNo = reader["VoucherNo"].ToString();
                    
                    lstpettycashlist.Add(bolpettycash);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return lstpettycashlist;
        }
        #endregion

        #region "UpdatePettyCashByPettyCashID"
        public int UpdatePettyCashByPettyCashID(BOLPettyCash bolpettycash)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_UpdatePettyCashByPettyCashID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@ID", bolpettycash.ID);
                cmd.Parameters.AddWithValue("@Date", bolpettycash.Date);
                cmd.Parameters.AddWithValue("@LocationID", bolpettycash.LocationID);
                cmd.Parameters.AddWithValue("@Amount", bolpettycash.Amount);
                cmd.Parameters.AddWithValue("@Remark", bolpettycash.Remark);
                cmd.Parameters.AddWithValue("@IsGetAmt", bolpettycash.IsGetAmt);
                cmd.Parameters.AddWithValue("@IsPaidAmt", bolpettycash.IsPaidAmt);
                cmd.Parameters.AddWithValue("@Type", bolpettycash.Type);
                cmd.Parameters.AddWithValue("@VoucherNo", bolpettycash.VoucherNo);

                isupdated = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return isupdated;
        }
        #endregion

        #region "GetSearchPettyCashHistory"
        public DataSet GetSearchPettyCashHistory(DateTime startdate, DateTime enddate, string remark, long LocationID)
        {
            DataSet ds = new DataSet();
            try
            {

                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetPettyCashHistoryList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                cmd.Parameters.AddWithValue("@LocationID", LocationID);
                cmd.Parameters.AddWithValue("@Remark", remark);
               
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return ds;
        }
        #endregion

        #region "GetPettyCashHistroy"  
        public List<BOLPettyCash> GetPettyCashHistroy(DateTime StartDate, DateTime EndDate, long LocationID, string remark, Boolean isbydate)
        {
            List<BOLPettyCash> lstpettycashlist = new List<BOLPettyCash>();
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetPettyCashHistoryList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDate", StartDate);
                cmd.Parameters.AddWithValue("@EndDate", EndDate);
                cmd.Parameters.AddWithValue("@LocationID", LocationID);
                cmd.Parameters.AddWithValue("@Remark", remark);
                //cmd.Parameters.AddWithValue("@IsByDate", isbydate);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BOLPettyCash bolpettycash = new BOLPettyCash();
                        bolpettycash.ID = Int32.Parse(reader["ID"].ToString());
                        bolpettycash.Date = DateTime.Parse(reader["Date"].ToString());
                        bolpettycash.LocationID = long.Parse(reader["ID"].ToString());
                        bolpettycash.Location = reader["Location"].ToString();
                        bolpettycash.Amount = decimal.Parse(reader["Amount"].ToString());
                        bolpettycash.Remark = reader["Remark"].ToString();
                        bolpettycash.Type = reader["Type"].ToString();
                        bolpettycash.VoucherNo = reader["Type"].ToString();
                        lstpettycashlist.Add(bolpettycash);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return lstpettycashlist;
        }

        #endregion

        #region "DeletePettyCash"
        public int DeletePettyCash(int ID)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_DeletePettyCash", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                isdelete = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return isdelete;
        }
        #endregion
    }
}
