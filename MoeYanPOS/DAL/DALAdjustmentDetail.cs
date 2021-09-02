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
    class DALAdjustmentDetail
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr = MoeYanConfiguration.GetConnection();
        #endregion   

        #region "InsertAdjustmentDetail"
        public int InsertAdjustmentDetail(BOLAdjustment bolAdjustment)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_InsertAdjustmentDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();                
                cmd.Parameters.AddWithValue("@AdjustmentID", bolAdjustment.AdjustmentID);
                cmd.Parameters.AddWithValue("@ItemCode", bolAdjustment.ItemCode);
               // cmd.Parameters.AddWithValue("@AdjustmentTypeID", bolAdjustment.AdjustmentTypeID);
                cmd.Parameters.AddWithValue("@Qty", bolAdjustment.Qty);
                cmd.Parameters.AddWithValue("@Remark", bolAdjustment.Remark);
                cmd.Parameters.AddWithValue("@Price", bolAdjustment.Price);
                cmd.Parameters.AddWithValue("@Amount", bolAdjustment.Amount);
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

        #region "ShowAllAdjustment"
        public List<BOLAdjustment> ShowAllAdjustment()
        {
            List<BOLAdjustment> lstAdjustmentType = new List<BOLAdjustment>();
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
                        BOLAdjustment bolAdjustment = new BOLAdjustment();
                        bolAdjustment.ID = long.Parse(reader["ID"].ToString());
                        bolAdjustment.AdjustmentID = long.Parse(reader["AdjustmentID"].ToString());
                        bolAdjustment.ItemCode = reader["ItemCode"].ToString();
                        bolAdjustment.AdjustmentTypeID = Int32.Parse(reader["AdjustmentTypeID"].ToString());
                        bolAdjustment.Qty = Int32.Parse(reader["Qty"].ToString());
                        bolAdjustment.Remark = reader["Remark"].ToString();
                        bolAdjustment.Price = decimal.Parse( reader["Price"].ToString());
                        bolAdjustment.Amount = decimal.Parse(reader["Amount"].ToString());
                        lstAdjustmentType.Add(bolAdjustment);
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
            return lstAdjustmentType;
        }
        #endregion

        #region "GetAdjustmentDetail"
        public DataSet GetAdjustmentDetail(long ID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetAdjustmentDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AdjustmentID", ID);

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

        #region "DeleteAdjustmentDetail"
        public int DeleteAdjustmentDetail(long id)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeletAdjustmentDetail", con);
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

        #region "UpdateAdjustmentDetail"
        public int UpdateAdjustmentDetail(BOLAdjustment bolAdjustment)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdateAdjustmentDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@ID", bolAdjustment.ID);
                cmd.Parameters.AddWithValue("@AdjustmentID", bolAdjustment.AdjustmentID);
                cmd.Parameters.AddWithValue("@ItemCode", bolAdjustment.ItemCode);
                //cmd.Parameters.AddWithValue("@AdjustmentTypeID", bolAdjustment.AdjustmentTypeID);
                cmd.Parameters.AddWithValue("@Qty", bolAdjustment.Qty);
                cmd.Parameters.AddWithValue("@Remark", bolAdjustment.Remark);
                cmd.Parameters.AddWithValue("@Price", bolAdjustment.Price);
                cmd.Parameters.AddWithValue("@Amount", bolAdjustment.Amount);
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
