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
    class DALSaleReturnDetail
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr   = MoeYanConfiguration.GetConnection();
        #endregion

        #region "SaveSaleReturnDetail"

       /* public int SaveSaleReturnDetail(BOLSaleReturn bolsaleReturn)
        {
            int isSaved = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_InsertSaleReturn", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();

                cmd.Parameters.AddWithValue("@Date", bolsaleReturn.Date);
                cmd.Parameters.AddWithValue("@CustomerID", bolsaleReturn.Customerid);
                cmd.Parameters.AddWithValue("@VoucherNo", bolsaleReturn.Voucherno);
                cmd.Parameters.AddWithValue("@PaymentType", bolsaleReturn.Paymenttype);
                cmd.Parameters.AddWithValue("@Currency", bolsaleReturn.Currencyid);
                cmd.Parameters.AddWithValue("@SaleVoucher", bolsaleReturn.SaleVoucher);
                cmd.Parameters.AddWithValue("@TotalAmt", bolsaleReturn.TotalAmt);
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
        }*/

        #endregion        

        #region "DeleteSaleReturnDetail"
        public int DeleteSaleReturnDetail(int SaleReturnid)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_DeleteSaleReturn", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SaleReturnID", SaleReturnid);
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

        #region "UpdateSaleReturnDetail"
        /*public int UpdateSaleReturnDetail(BOLSaleReturn boluser)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_UpdateSaleReturn", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.Parameters.AddWithValue("@Date", boluser.Date);
                cmd.Parameters.AddWithValue("@CustomerID", boluser.CustomerID);
                cmd.Parameters.AddWithValue("@VoucherNo", boluser.VoucherNo);
                cmd.Parameters.AddWithValue("@PaymentType", boluser.PaymentType);
                cmd.Parameters.AddWithValue("@CurrencyID", boluser.CurrencyID);
                cmd.Parameters.AddWithValue("@TotalAmt", boluser.TotalAmt);
                cmd.Parameters.AddWithValue("@UserID", boluser.UserID);
                cmd.Parameters.AddWithValue("@SystemVoucherNo", boluser.SystemVoucherNo);

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
        }*/
        #endregion
    }
}
