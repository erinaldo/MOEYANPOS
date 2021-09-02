using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MoeYanPOS.BOL;
using MoeYanPOS.Function;

namespace MoeYanPOS.DAL
{
    class DALStockTransfer
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr = MoeYanConfiguration.GetConnection();
        #endregion

        #region "SaveStockTransfer"
        public int SaveStockTransfer(BOLStockTransfer bolStockTransfer)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_InsertStockTransfer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@Date", bolStockTransfer.Date);
                cmd.Parameters.AddWithValue("@UserID", bolStockTransfer.UserID);               
                cmd.Parameters.AddWithValue("@LocationID", bolStockTransfer.LID);
                cmd.Parameters.AddWithValue("@LocationToID", bolStockTransfer.ToLID);
                cmd.Parameters.AddWithValue("@VoucherNo", bolStockTransfer.VoucherNo);
                cmd.Parameters.AddWithValue("@Times",bolStockTransfer.Times);
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

        #region "ShowAllStockTransfer"
        public List<BOLStockTransfer> ShowAllStockTransfer()
        {
            List<BOLStockTransfer> lstStockTransfer = new List<BOLStockTransfer>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("sp_showallStockTransfer", con);
                cmd.CommandType = CommandType.StoredProcedure;

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
                        BOLStockTransfer bolStockTransfer = new BOLStockTransfer();
                        bolStockTransfer.ID = long.Parse(reader["ID"].ToString());
                        bolStockTransfer.Date = DateTime.Parse(reader["Date"].ToString());
                        bolStockTransfer.UserID = Int32.Parse(reader["UserID"].ToString());
                        bolStockTransfer.LID = long.Parse(reader["LocationID"].ToString());
                        bolStockTransfer.ToLID = long.Parse(reader["LocationToID"].ToString());
                        bolStockTransfer.VoucherNo = reader["VoucherNo"].ToString();
                        lstStockTransfer.Add(bolStockTransfer);
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
            return lstStockTransfer;
        }
        #endregion

        #region "GetStockTransfer"
        public BOLStockTransfer GetStockTransfer(long ID)
        {
            BOLStockTransfer bolStockTransfer = new BOLStockTransfer();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetStockTransfer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
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
                        bolStockTransfer.ID = long.Parse(reader["ID"].ToString());
                        bolStockTransfer.Date = DateTime.Parse(reader["Date"].ToString());
                        bolStockTransfer.UserID = Int32.Parse(reader["UserID"].ToString());
                        bolStockTransfer.LID = long.Parse(reader["LocationID"].ToString());
                        bolStockTransfer.ToLID = long.Parse(reader["LocationToID"].ToString());
                        bolStockTransfer.VoucherNo = reader["VoucherNo"].ToString();
                        bolStockTransfer.Times = Int32.Parse(reader["Times"].ToString());
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
            return bolStockTransfer;
        }
        #endregion

        #region "DeleteStockTransfer"
        public int DeleteStockTransfer(long id)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeletStockTransfer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
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

        #region "UpdateStockTransfer"
        public int UpdateStockTransfer(BOLStockTransfer bolStockTransfer)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdateStockTransfer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@id", bolStockTransfer.ID);
                cmd.Parameters.AddWithValue("@Date", bolStockTransfer.Date);
                cmd.Parameters.AddWithValue("@LocationID", bolStockTransfer.LID);
                cmd.Parameters.AddWithValue("@LocationToID", bolStockTransfer.ToLID);
                cmd.Parameters.AddWithValue("@VoucherNo", bolStockTransfer.VoucherNo);
                cmd.Parameters.AddWithValue("@Times", bolStockTransfer.Times);
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

        #region "GetStockTransferID"
        public long GetStockTransferID()
        {
            long adjudtmentid = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetStockTransferID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                adjudtmentid = (long)cmd.ExecuteScalar();
                if (adjudtmentid == -1 | adjudtmentid == null)
                {
                    adjudtmentid = 1;
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
            return adjudtmentid;
        }
        #endregion

        #region "GetStockTransferMaxID"
        public long GetStockTransferMaxID()
        {
            long TransID = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetMaxStockTransferID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                object o = new object();
                o = cmd.ExecuteScalar();
                if (o.GetType() == typeof(long))
                {
                    TransID = (long)o;
                }

                if (TransID == 0 | TransID == -1)
                {
                    TransID = 1;
                }
                else
                {
                    TransID += 1;
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
            return TransID;
        }
        #endregion

        #region "SelectStockTransferVoucher"
        public DataSet SelectStockTransferVoucher(string VoucherNo)
        {
            DataSet ds = new DataSet();
            BOLPurchase bolpurchase = new BOLPurchase();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_SelectStockTransferVoucher_001", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
                // cmd.Parameters.AddWithValue("@action", action);

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
       
    }
}
