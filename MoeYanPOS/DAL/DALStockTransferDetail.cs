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
    class DALStockTransferDetail
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr = MoeYanConfiguration.GetConnection();
        #endregion

        #region "InsertStockTransferDetail"
        public int InsertStockTransferDetail(BOLStockTransfer bolStockTransfer)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_InsertStockTransferDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@TransferID", bolStockTransfer.TransferID);
                cmd.Parameters.AddWithValue("@ItemCode", bolStockTransfer.ItemCode);
                cmd.Parameters.AddWithValue("@Qty", bolStockTransfer.Qty);
                cmd.Parameters.AddWithValue("@Remark", bolStockTransfer.Remark);
                cmd.Parameters.AddWithValue("@Price", bolStockTransfer.Price);
                cmd.Parameters.AddWithValue("@Amount", bolStockTransfer.Amount);
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
                cmd = new SqlCommand("SP_GetAllAdjustment", con);
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
                        bolStockTransfer.TransferID = long.Parse(reader["TransferID"].ToString());
                        bolStockTransfer.ItemCode = reader["ItemCode"].ToString();
                        bolStockTransfer.Qty = Int32.Parse(reader["Qty"].ToString());
                        bolStockTransfer.Remark = reader["Remark"].ToString();
                        bolStockTransfer.Price = decimal.Parse(reader["Price"].ToString());
                        bolStockTransfer.Amount = decimal.Parse(reader["Amount"].ToString());
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
        public DataSet GetStockTransfer(long ID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetStockTransferDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);

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

        #region "DeleteStockTransferDetail"
        public int DeleteStockTransferDetail(long id)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeletStockTransferDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
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

        #region "UpdateStockTransferDetail"
        public int UpdateStockTransferDetail(BOLStockTransfer bolStockTransfer)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdateStockTransferDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@ID", bolStockTransfer.ID);
                cmd.Parameters.AddWithValue("@TransferID", bolStockTransfer.TransferID);
                cmd.Parameters.AddWithValue("@ItemCode", bolStockTransfer.ItemCode);
                cmd.Parameters.AddWithValue("@Qty", bolStockTransfer.Qty);
                cmd.Parameters.AddWithValue("@Remark", bolStockTransfer.Remark);
                cmd.Parameters.AddWithValue("@Price", bolStockTransfer.Price);
                cmd.Parameters.AddWithValue("@Amount", bolStockTransfer.Amount);
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
    }
}
