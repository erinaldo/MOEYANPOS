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
    class DALAuthorization
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr = MoeYanConfiguration.GetConnection();
        #endregion

        #region "SaveAuthorize"
        public int saveauthorize(BOLAuthorization bolauthorize)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_InsertAuthorization", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                cmd.Parameters.AddWithValue("@UserID", bolauthorize.Userid);
                cmd.Parameters.AddWithValue("@SaleEntry", bolauthorize.Saleentry);
                cmd.Parameters.AddWithValue("@PurchaseEntry",bolauthorize.Purchaseentry);
                cmd.Parameters.AddWithValue("@SaleReturn",bolauthorize.Salereturn);
                cmd.Parameters.AddWithValue("@SaleOrder",bolauthorize.Saleorder);
                cmd.Parameters.AddWithValue("@StockAjustment",bolauthorize.Stockajustment);
                cmd.Parameters.AddWithValue("@StockTransfer",bolauthorize.Stocktransfer);
                cmd.Parameters.AddWithValue("@OpeningStock",bolauthorize.Openingstock);
                cmd.Parameters.AddWithValue("@StockBlance",bolauthorize.Stockbalance);
                cmd.Parameters.AddWithValue("@PayForCredit",bolauthorize.Payforcredit);
                cmd.Parameters.AddWithValue("@PettyCash",bolauthorize.Pettycash);
                cmd.Parameters.AddWithValue("@SaleHistory",bolauthorize.Salehistory);
                cmd.Parameters.AddWithValue("@PurchaseHistory",bolauthorize.Purchasehistory);
                cmd.Parameters.AddWithValue("@SaleReturnHistory",bolauthorize.Salereturnhistory);
                cmd.Parameters.AddWithValue("@SaleOrderHistory",bolauthorize.Saleorderhistory);
                cmd.Parameters.AddWithValue("@StockAjustmentHistory",bolauthorize.Stockajustmenthistory);
                cmd.Parameters.AddWithValue("@StockTransferHistory",bolauthorize.Stocktransferhistory);
                cmd.Parameters.AddWithValue("@OpeningStockHistory",bolauthorize.Openingstockhistory);
                cmd.Parameters.AddWithValue("@StockHistory",bolauthorize.Stockhistory);
                cmd.Parameters.AddWithValue("@PayForCreditHistory",bolauthorize.Payforcredithistory);
                cmd.Parameters.AddWithValue("@PettyCashHistory",bolauthorize.Pettycashhistory);
                cmd.Parameters.AddWithValue("@User",bolauthorize.User);
                cmd.Parameters.AddWithValue("@Division",bolauthorize.Division);
                cmd.Parameters.AddWithValue("@Department", bolauthorize.Department);
                cmd.Parameters.AddWithValue("@Township", bolauthorize.Township);
                cmd.Parameters.AddWithValue("@Location", bolauthorize.Location);
                cmd.Parameters.AddWithValue("@Customer", bolauthorize.Customer);
                cmd.Parameters.AddWithValue("@Supplier", bolauthorize.Supplier);
                cmd.Parameters.AddWithValue("@Measurement", bolauthorize.Measurement);
                cmd.Parameters.AddWithValue("@Brand", bolauthorize.Brand);
                cmd.Parameters.AddWithValue("@Class", bolauthorize.Class_);
                cmd.Parameters.AddWithValue("@Category", bolauthorize.Category);
                cmd.Parameters.AddWithValue("@Stock", bolauthorize.Stock);
                cmd.Parameters.AddWithValue("@Currency", bolauthorize.Currency);
                cmd.Parameters.AddWithValue("@SystemSetting", bolauthorize.Systemsetting);
                cmd.Parameters.AddWithValue("@CashReport", bolauthorize.Cashreport);
                cmd.Parameters.AddWithValue("@SaleReport",bolauthorize.Salereport);
                cmd.Parameters.AddWithValue("@PurchaseReport",bolauthorize.Purchasereport);
                cmd.Parameters.AddWithValue("@StockReport",bolauthorize.Stockreport);
                cmd.Parameters.AddWithValue("@CustomerReport",bolauthorize.Customerreport);
                cmd.Parameters.AddWithValue("@SupplierReport",bolauthorize.Supplierreport);
                cmd.Parameters.AddWithValue("@setlocation",bolauthorize.Setlocation);
                cmd.Parameters.AddWithValue("@exportnimport",bolauthorize.Exportnimport);
                cmd.Parameters.AddWithValue("@authorization", bolauthorize.Authorization);
                cmd.Parameters.AddWithValue("@vouchersetting", bolauthorize.VoucherSetting);
                cmd.Parameters.AddWithValue("@stockadjustmenttype", bolauthorize.StockAdjustmentType);
                cmd.Parameters.AddWithValue("@outletcashheader", bolauthorize.OutletCashHeader);

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

        #region "UpdateAuthorize"
        public int updateauthorize(BOLAuthorization bolauthorize)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdateAuthorization", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();

                cmd.Parameters.AddWithValue("@UserID", bolauthorize.Userid);
                cmd.Parameters.AddWithValue("@SaleEntry", bolauthorize.Saleentry);
                cmd.Parameters.AddWithValue("@PurchaseEntry", bolauthorize.Purchaseentry);
                cmd.Parameters.AddWithValue("@SaleReturn", bolauthorize.Salereturn);
                cmd.Parameters.AddWithValue("@SaleOrder", bolauthorize.Saleorder);
                cmd.Parameters.AddWithValue("@StockAjustment", bolauthorize.Stockajustment);
                cmd.Parameters.AddWithValue("@StockTransfer", bolauthorize.Stocktransfer);
                cmd.Parameters.AddWithValue("@OpeningStock", bolauthorize.Openingstock);
                cmd.Parameters.AddWithValue("@StockBlance", bolauthorize.Stockbalance);
                cmd.Parameters.AddWithValue("@PayForCredit", bolauthorize.Payforcredit);
                cmd.Parameters.AddWithValue("@PettyCash", bolauthorize.Pettycash);
                cmd.Parameters.AddWithValue("@SaleHistory", bolauthorize.Salehistory);
                cmd.Parameters.AddWithValue("@PurchaseHistory", bolauthorize.Purchasehistory);
                cmd.Parameters.AddWithValue("@SaleReturnHistory", bolauthorize.Salereturnhistory);
                cmd.Parameters.AddWithValue("@SaleOrderHistory", bolauthorize.Saleorderhistory);
                cmd.Parameters.AddWithValue("@StockAjustmentHistory", bolauthorize.Stockajustmenthistory);
                cmd.Parameters.AddWithValue("@StockTransferHistory", bolauthorize.Stocktransferhistory);
                cmd.Parameters.AddWithValue("@OpeningStockHistory", bolauthorize.Openingstockhistory);
                cmd.Parameters.AddWithValue("@StockHistory", bolauthorize.Stockhistory);
                cmd.Parameters.AddWithValue("@PayForCreditHistory", bolauthorize.Payforcredithistory);
                cmd.Parameters.AddWithValue("@PettyCashHistory", bolauthorize.Pettycashhistory);
                cmd.Parameters.AddWithValue("@User", bolauthorize.User);
                cmd.Parameters.AddWithValue("@Division", bolauthorize.Division);
                cmd.Parameters.AddWithValue("@Department", bolauthorize.Department);
                cmd.Parameters.AddWithValue("@Township", bolauthorize.Township);
                cmd.Parameters.AddWithValue("@Location", bolauthorize.Location);
                cmd.Parameters.AddWithValue("@Customer", bolauthorize.Customer);
                cmd.Parameters.AddWithValue("@Supplier", bolauthorize.Supplier);
                cmd.Parameters.AddWithValue("@Measurement", bolauthorize.Measurement);
                cmd.Parameters.AddWithValue("@Brand", bolauthorize.Brand);
                cmd.Parameters.AddWithValue("@Class", bolauthorize.Class_);
                cmd.Parameters.AddWithValue("@Category", bolauthorize.Category);
                cmd.Parameters.AddWithValue("@Stock", bolauthorize.Stock);
                cmd.Parameters.AddWithValue("@Currency", bolauthorize.Currency);
                cmd.Parameters.AddWithValue("@SystemSetting", bolauthorize.Systemsetting);
                cmd.Parameters.AddWithValue("@CashReport", bolauthorize.Cashreport);
                cmd.Parameters.AddWithValue("@SaleReport", bolauthorize.Salereport);
                cmd.Parameters.AddWithValue("@PurchaseReport", bolauthorize.Purchasereport);
                cmd.Parameters.AddWithValue("@StockReport", bolauthorize.Stockreport);
                cmd.Parameters.AddWithValue("@CustomerReport", bolauthorize.Customerreport);
                cmd.Parameters.AddWithValue("@SupplierReport", bolauthorize.Supplierreport);
                cmd.Parameters.AddWithValue("@setlocation", bolauthorize.Setlocation);
                cmd.Parameters.AddWithValue("@exportnimport", bolauthorize.Exportnimport);
                cmd.Parameters.AddWithValue("@authorization", bolauthorize.Authorization);
                cmd.Parameters.AddWithValue("@vouchersetting", bolauthorize.VoucherSetting);
                cmd.Parameters.AddWithValue("@stockadjustmenttype", bolauthorize.StockAdjustmentType);
                cmd.Parameters.AddWithValue("@outletcashheader", bolauthorize.OutletCashHeader);

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

        //#region "GetUserByUserID"
        //public BOLUser GetUserByUserID(int Userid)
        //{
        //    BOLUser boluser = new BOLUser();
        //    try
        //    {

        //        con = new SqlConnection(Constr);
        //        cmd = new SqlCommand("SP_GetUserByUserID", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@UserID", Userid);

        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }

        //        con.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {

        //                boluser.UserID = Int32.Parse(reader["UserID"].ToString());
        //                boluser.UserName = reader["Name"].ToString();
        //                boluser.CardID = reader["CardID"].ToString();
        //                boluser.Password = reader["Password"].ToString();
        //                boluser.NRC = reader["NRC"].ToString();
        //                boluser.Township = reader["Township"].ToString();
        //                boluser.IsSavePrint = bool.Parse(reader["IsSavePrint"].ToString());
        //                boluser.IsmsgforVoucher = bool.Parse(reader["IsmsgforVoucher"].ToString());
        //                boluser.TownshipID = Int32.Parse(reader["TownshipID"].ToString());
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }

        //    return boluser;
        //}
        //#endregion

        #region "SelectAllAuth"
        public List<BOLAuthorization> SelectAllAuth(int userid)
        {
            List<BOLAuthorization> lstauth = new List<BOLAuthorization>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("sp_ShowAllAuth", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userid);
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
                        BOLAuthorization bolauthorize = new BOLAuthorization();
                        bolauthorize.Userid = Int32.Parse(reader["UserID"].ToString());
                        bolauthorize.Saleentry = bool.Parse(reader["SaleEntry"].ToString());
                        bolauthorize.Purchaseentry = bool.Parse(reader["PurchaseEntry"].ToString());
                        bolauthorize.Salereturn = bool.Parse(reader["SaleReturn"].ToString());
                        bolauthorize.Saleorder = bool.Parse(reader["SaleOrder"].ToString());
                        bolauthorize.Stockajustment = bool.Parse(reader["StockAjustment"].ToString());
                        bolauthorize.Stocktransfer = bool.Parse(reader["StockTransfer"].ToString());
                        bolauthorize.Openingstock = bool.Parse(reader["OpeningStock"].ToString());
                        bolauthorize.Stockbalance = bool.Parse(reader["StockBalance"].ToString());
                        bolauthorize.Payforcredit = bool.Parse(reader["PayForCredit"].ToString());
                        bolauthorize.Pettycash = bool.Parse(reader["PettyCash"].ToString());
                        bolauthorize.Salehistory = bool.Parse(reader["SaleHistory"].ToString());
                        bolauthorize.Purchasehistory = bool.Parse(reader["PurchaseHistory"].ToString());
                        bolauthorize.Salereturnhistory = bool.Parse(reader["SaleReturnHistory"].ToString());
                        bolauthorize.Saleorderhistory = bool.Parse(reader["SaleOrderHistory"].ToString());
                        bolauthorize.Stockajustmenthistory = bool.Parse(reader["StockAjustmentHistory"].ToString());
                        bolauthorize.Stocktransferhistory = bool.Parse(reader["StockTransferHistory"].ToString());
                        bolauthorize.Openingstockhistory = bool.Parse(reader["OpeningStockHistory"].ToString());
                        bolauthorize.Stockhistory = bool.Parse(reader["StockHistory"].ToString());
                        bolauthorize.Payforcredithistory = bool.Parse(reader["PayForCreditHistory"].ToString());
                        bolauthorize.Pettycashhistory = bool.Parse(reader["PettyCashHistory"].ToString());
                        bolauthorize.User = bool.Parse(reader["User"].ToString());
                        bolauthorize.Division = bool.Parse(reader["Division"].ToString());
                        bolauthorize.Department = bool.Parse(reader["Department"].ToString());
                        bolauthorize.Township = bool.Parse(reader["Township"].ToString());
                        bolauthorize.Location = bool.Parse(reader["Location"].ToString());
                        bolauthorize.Customer = bool.Parse(reader["Customer"].ToString());
                        bolauthorize.Supplier = bool.Parse(reader["Supplier"].ToString());
                        bolauthorize.Measurement = bool.Parse(reader["Measurement"].ToString());
                        bolauthorize.Brand = bool.Parse(reader["Brand"].ToString());
                        bolauthorize.Class_ = bool.Parse(reader["Class"].ToString());
                        bolauthorize.Category = bool.Parse(reader["Category"].ToString());
                        bolauthorize.Stock = bool.Parse(reader["Stock"].ToString());
                        bolauthorize.Currency = bool.Parse(reader["Currency"].ToString());
                        bolauthorize.Systemsetting = bool.Parse(reader["SystemSetting"].ToString());
                        bolauthorize.Cashreport = bool.Parse(reader["CashReport"].ToString());
                        bolauthorize.Salereport = bool.Parse(reader["SaleReport"].ToString());
                        bolauthorize.Purchasereport = bool.Parse(reader["PurchaseReport"].ToString());
                        bolauthorize.Stockreport = bool.Parse(reader["StockReport"].ToString());
                        bolauthorize.Customerreport = bool.Parse(reader["CustomerReport"].ToString());
                        bolauthorize.Supplierreport = bool.Parse(reader["SupplierReport"].ToString());
                        bolauthorize.Setlocation = bool.Parse(reader["setlocation"].ToString());
                        bolauthorize.Exportnimport = bool.Parse(reader["exportnimport"].ToString());
                        bolauthorize.Authorization = bool.Parse(reader["authorization"].ToString());
                        bolauthorize.VoucherSetting = bool.Parse(reader["vouchersetting"].ToString());
                        bolauthorize.StockAdjustmentType = bool.Parse(reader["stockadjustmenttype"].ToString());
                        bolauthorize.OutletCashHeader = bool.Parse(reader["outletcashheader"].ToString());

                        lstauth.Add(bolauthorize);
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
            return lstauth;
        }

        #endregion
    }
}
