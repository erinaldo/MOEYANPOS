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
    class DALPayForCredit
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr = MoeYanConfiguration.GetConnection();
        #endregion

        #region "SavePayForCredit"
        public int SavePayForCredit(BOLPayForCredit bolPayForCredit)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_InsertPayForCredit", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@Date", bolPayForCredit.Date);
                cmd.Parameters.AddWithValue("@CustomerID", bolPayForCredit.Cid);
                cmd.Parameters.AddWithValue("@VoucherNo", bolPayForCredit.VoucherNo);
                cmd.Parameters.AddWithValue("@SaleVoucher", bolPayForCredit.SaleVoucher);
                cmd.Parameters.AddWithValue("@Amount", bolPayForCredit.Amt);
                cmd.Parameters.AddWithValue("@LocationID", bolPayForCredit.LocationID);
                cmd.Parameters.AddWithValue("@UserID", bolPayForCredit.UserID);
                cmd.Parameters.AddWithValue("@CashReceiveVoucherNo", bolPayForCredit.CashReceiveVoucherNo);
                cmd.Parameters.AddWithValue("@CRPaymentType", bolPayForCredit.CRPaymenttype);    
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

        #region "UpdatePayForCredit"
        public int UpdatePayForCredit(BOLPayForCredit bolPayForCredit)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdatePayForCredit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", bolPayForCredit.ID);
                cmd.Parameters.AddWithValue("@Date", bolPayForCredit.Date);
                cmd.Parameters.AddWithValue("@CustomerID", bolPayForCredit.Cid);
                cmd.Parameters.AddWithValue("@VoucherNo", bolPayForCredit.VoucherNo);
                cmd.Parameters.AddWithValue("@SaleVoucher", bolPayForCredit.SaleVoucher);
                cmd.Parameters.AddWithValue("@Amount", bolPayForCredit.Amt);
                cmd.Parameters.AddWithValue("@LocationID", bolPayForCredit.LocationID);
                cmd.Parameters.AddWithValue("@CRPaymentType", bolPayForCredit.CRPaymenttype);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();

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

        #region "GetPayByPayID"
        public List<BOLPayForCredit> GetPayByPayID(long payid)
        {
            List<BOLPayForCredit> lstpaycredit = new List<BOLPayForCredit>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetPayByPayID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@payID",payid);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    BOLPayForCredit bolpaycredit = new BOLPayForCredit();
                    bolpaycredit.ID = long.Parse(reader["ID"].ToString());
                    bolpaycredit.Date = DateTime.Parse(reader["Date"].ToString());
                    bolpaycredit.Cid = long.Parse(reader["PCID"].ToString());
                    bolpaycredit.Name = reader["Name"].ToString();
                    bolpaycredit.VoucherNo = reader["VoucherNo"].ToString();
                    bolpaycredit.SaleVoucher = reader["SaleVoucher"].ToString();
                    bolpaycredit.Amt = Decimal.Parse(reader["Amount"].ToString());
                    bolpaycredit.LocationID = long.Parse(reader["LocationID"].ToString());
                    bolpaycredit.UserID = Int32.Parse(reader["UserID"].ToString());
                    bolpaycredit.CashReceiveVoucherNo = reader["CashReceiveVoucherNo"].ToString();
                    bolpaycredit.CRPaymenttype = reader["CRPaymentType"].ToString();

                    lstpaycredit.Add(bolpaycredit);
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
            return lstpaycredit;
        }
        #endregion

        #region "GetTotalCreditAmount"
        public decimal GetTotalCreditAmount(string VoucherNo)
        {
            decimal TotalCreditAmt = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetTotalCreditAmount", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
               
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                object o = new object();
                o = cmd.ExecuteScalar();
                if (o.GetType() == typeof(decimal))
                {
                    TotalCreditAmt = (decimal)o;
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
            return TotalCreditAmt;
        }
        #endregion

        #region "GetPayForCredits"
        public List<BOLPayForCredit> GetPayForCredits(long LocationID, long Classid, long CustomerID, string VoucherNo, DateTime StartDate, DateTime EndDate, bool IsByDate)
        {
            List<BOLPayForCredit> list = new List<BOLPayForCredit>();
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                this.con = new SqlConnection(this.Constr);
                this.cmd = new SqlCommand("SP_GetPayForCreditList", this.con);
                this.cmd.Parameters.AddWithValue("@LocationID", LocationID);
                this.cmd.Parameters.AddWithValue("@ClassID", Classid);
                this.cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                this.cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
                this.cmd.Parameters.AddWithValue("@StartDate", StartDate);
                this.cmd.Parameters.AddWithValue("@EndDate", EndDate);
                this.cmd.Parameters.AddWithValue("@IsByDate", IsByDate);
                this.cmd.CommandType = CommandType.StoredProcedure;
                if (this.con.State == ConnectionState.Open)
                {
                    this.con.Close();
                }
                this.con.Open();
                SqlDataReader sqlDataReader = this.cmd.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        list.Add(new BOLPayForCredit
                        {
                            ID = long.Parse(sqlDataReader["ID"].ToString()),
                            Date = DateTime.Parse(sqlDataReader["Date"].ToString()),
                            CustomerID = sqlDataReader["UserID"].ToString(),
                            Name = sqlDataReader["Name"].ToString(),
                            VoucherNo = sqlDataReader["VoucherNo"].ToString(),
                            Amt = decimal.Parse(sqlDataReader["Amount"].ToString()),
                            Location = sqlDataReader["Location"].ToString(),
                            CRPaymenttype = sqlDataReader["CRPaymentType"].ToString()
                            //SaleVoucher = sqlDataReader["SaleVoucher"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.con.Close();
            }
            return list;
        }
        #endregion

        #region "GetPayForCreditsHistory"
        public DataSet GetPayForCreditsHistory(long LocationID, long Classid, long CustomerID, string VoucherNo, DateTime StartDate, DateTime EndDate, bool IsByDate)
        {
            DataSet dataSet = new DataSet();
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                this.con = new SqlConnection(this.Constr);
                this.cmd = new SqlCommand("SP_GetPayForCreditList", this.con);
                this.cmd.Parameters.AddWithValue("@LocationID", LocationID);
                this.cmd.Parameters.AddWithValue("@ClassID", Classid);
                this.cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                this.cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
                this.cmd.Parameters.AddWithValue("@StartDate", StartDate);
                this.cmd.Parameters.AddWithValue("@EndDate", EndDate);
                this.cmd.Parameters.AddWithValue("@IsByDate", IsByDate);
                this.cmd.CommandType = CommandType.StoredProcedure;
                if (this.con.State == ConnectionState.Open)
                {
                    this.con.Close();
                }
                this.con.Open();
                sqlDataAdapter.SelectCommand = this.cmd;
                sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.con.Close();
            }
            return dataSet;
        }
        #endregion

        #region "DeletePayForCreditHistory"
        public int DeletePayForCreditHistory(long id)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeletePayForCreditHistory", con);
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

        #region "GetCreditAmountByVoucher"
        public DataSet GetCreditAmountByVoucher(long LocationID, long ClassID, long CustomerID, string VoucherNo, DateTime StartDate, DateTime EndDate, bool IsByDate)
        {
            DataSet dataSet = new DataSet();
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                this.con = new SqlConnection(this.Constr);
                this.cmd = new SqlCommand("SP_GetCreditAmountByVoucher", this.con);
                this.cmd.CommandType = CommandType.StoredProcedure;
                this.cmd.Parameters.AddWithValue("@LocationID", LocationID);
                this.cmd.Parameters.AddWithValue("@ClassID", ClassID);
                this.cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                this.cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
                this.cmd.Parameters.AddWithValue("@StartDate", StartDate);
                this.cmd.Parameters.AddWithValue("@EndDate", EndDate);
                this.cmd.Parameters.AddWithValue("@IsByDate", IsByDate);
                if (this.con.State == ConnectionState.Open)
                {
                    this.con.Close();
                }
                this.con.Open();
                sqlDataAdapter.SelectCommand = this.cmd;
                sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.con.Close();
            }
            return dataSet;
        }
        #endregion
    }
}
