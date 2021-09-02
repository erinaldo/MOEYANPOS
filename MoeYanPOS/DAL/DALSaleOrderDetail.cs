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
    class DALSaleOrderDetail
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr   = MoeYanConfiguration.GetConnection();
        #endregion

        #region "SaveOrderDetailData"
        public int SaveOrderDetailData(BOLSaleOrder bolsaleorderdetail)
        {
            int isSaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_InsertSaleOrderDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                cmd.Parameters.AddWithValue("@SaleOrderID", bolsaleorderdetail.Saleorderid);
                cmd.Parameters.AddWithValue("@ItemCode", bolsaleorderdetail.Itemcode);
                cmd.Parameters.AddWithValue("@Description", bolsaleorderdetail.Description);
                cmd.Parameters.AddWithValue("@Type", bolsaleorderdetail.Type);
                cmd.Parameters.AddWithValue("@Qty", bolsaleorderdetail.Qty);
                cmd.Parameters.AddWithValue("@SalePrice", bolsaleorderdetail.Saleprice);
                cmd.Parameters.AddWithValue("@Total", bolsaleorderdetail.Total);
                isSaved=cmd.ExecuteNonQuery();

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

        #region "UpdateSaleOrderDetailData"

        public int UpdateSaleOrderDetailData(BOLSaleOrder bolsaleorderdetail)
        {
            int isSaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdateSaleOrderDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();

                cmd.Parameters.AddWithValue("@ItemCode", bolsaleorderdetail.Itemcode);
                cmd.Parameters.AddWithValue("@Description", bolsaleorderdetail.Description);
                cmd.Parameters.AddWithValue("@Type", bolsaleorderdetail.Type);
                cmd.Parameters.AddWithValue("@Qty", bolsaleorderdetail.Qty);
                cmd.Parameters.AddWithValue("@SalePrice", bolsaleorderdetail.Saleprice);
                cmd.Parameters.AddWithValue("@Total", bolsaleorderdetail.Total);
                cmd.Parameters.AddWithValue("@SaleOrderDetailID", bolsaleorderdetail.Saleorderdetailid);

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

        
    }
}
