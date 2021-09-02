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
    class DALSaleDetail
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr   = MoeYanConfiguration.GetConnection();
        #endregion

        #region "SaveSaleDetailData"

        public int SaveSaleDetailData(BOLSale bolsaledetail)
        {
            int isSaved = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_InsertSaleDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@SaleID", bolsaledetail.SaleID);
                cmd.Parameters.AddWithValue("@No", bolsaledetail.No);
                cmd.Parameters.AddWithValue("@ItemCode", bolsaledetail.ItemCode);
                cmd.Parameters.AddWithValue("@Description", bolsaledetail.Description);
                cmd.Parameters.AddWithValue("@Type", bolsaledetail.Mtype);
                cmd.Parameters.AddWithValue("@Qty", bolsaledetail.Qty);
                cmd.Parameters.AddWithValue("@SalePrice", bolsaledetail.SalePrice);
                cmd.Parameters.AddWithValue("@Charges", bolsaledetail.Charge);
                cmd.Parameters.AddWithValue("@Total", bolsaledetail.Total);
                cmd.Parameters.AddWithValue("@FOC", bolsaledetail.FOC);
                cmd.Parameters.AddWithValue("@Itemdiscount", bolsaledetail.ItemDiscount);
                cmd.Parameters.AddWithValue("@ItemDiscountPercent", bolsaledetail.ItemDiscountPercent);
                cmd.Parameters.AddWithValue("@action", bolsaledetail.Action);
                isSaved = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return isSaved;
        }

        #endregion

        #region "UpdateSaleDetailData"

        public int UpdateSaleDetailData(BOLSale bolsaledetail)
        {
            int isSaved = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_UpdateSaleDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                string str = "";
                str = " N'" + bolsaledetail.SystemVoucherNo + "'";
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@SaleDetailID", bolsaledetail.SaleDetailID);
                cmd.Parameters.AddWithValue("@No", bolsaledetail.No);
                cmd.Parameters.AddWithValue("@ItemCode", bolsaledetail.ItemCode);
                cmd.Parameters.AddWithValue("@Description", bolsaledetail.Description);
                cmd.Parameters.AddWithValue("@Type", bolsaledetail.Mtype);
                cmd.Parameters.AddWithValue("@Qty", bolsaledetail.Qty);
                cmd.Parameters.AddWithValue("@SalePrice", bolsaledetail.SalePrice);
                cmd.Parameters.AddWithValue("@Charges", bolsaledetail.Charge);
                cmd.Parameters.AddWithValue("@Total", bolsaledetail.Total);
                cmd.Parameters.AddWithValue("@FOC", bolsaledetail.FOC);
                cmd.Parameters.AddWithValue("@Itemdiscount", bolsaledetail.ItemDiscount);
                cmd.Parameters.AddWithValue("@ItemDiscountPercent", bolsaledetail.ItemDiscountPercent);
                cmd.Parameters.AddWithValue("@action", bolsaledetail.Action);
                cmd.Parameters.AddWithValue("@UserID", bolsaledetail.UserID);
                cmd.Parameters.AddWithValue("@Str", str);
                isSaved = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return isSaved;
        }

        #endregion

        #region "ChkIsTempSaleDetailSaved"
        public int ChkIsTempSaleDetailSaved(int No, int UserID, string SystemVoucherNo)
        {
            int maxID = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("ChkIsTempSaleDetailSaved", con);
                string str = "";
                str = " and ts.SystemVoucherNo=N'" + SystemVoucherNo + "'";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Str", str);
                cmd.Parameters.AddWithValue("@No", No);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();

                maxID = (int)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return maxID;
        }
        #endregion

        #region "DeleteTempSaleDetail"
        public void DeleteTempSaleDetail(int No, string SystemVoucherNo)
        {
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeleteTempSaleDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@No", No);
                cmd.Parameters.AddWithValue("@SystemVoucherNo", SystemVoucherNo);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

        }
        #endregion

        #region "DeleteSaleDetail"
        public void DeleteSaleDetail(long SaleDetailID, string SystemVoucherNo)
        {
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeleteSaleDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SaleDetailID", SaleDetailID);
                cmd.Parameters.AddWithValue("@SystemVoucherNo", SystemVoucherNo);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

        }
        #endregion
    }
}
