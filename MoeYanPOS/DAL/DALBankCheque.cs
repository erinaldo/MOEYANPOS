using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoeYanPOS.DAL;
using MoeYanPOS.BOL;
using System.Data;
using System.Data.SqlClient;
using MoeYanPOS.Function;

namespace MoeYanPOS.DAL
{
    class DALBankCheque
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr = MoeYanConfiguration.GetConnection();
        #endregion

        #region "GetBankChequeID"
        public int GetBankChequeID()
        {
            int bankchequeid = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetBankChequeID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                bankchequeid = Convert.ToInt32(cmd.ExecuteScalar());
                if (bankchequeid == -1 | bankchequeid == null)
                {
                    bankchequeid = 1;
                }
                else
                {
                    bankchequeid += 1;
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
            return bankchequeid;
        }
        #endregion

        #region "SaveUser"
        public int SaveUser(BOLBankCheque bolbankcheque)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_InsertBankCheque", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                cmd.Parameters.AddWithValue("@bankchequeid", bolbankcheque.BankChequeID);
                cmd.Parameters.AddWithValue("@bankchequename", bolbankcheque.BankChequeName);
                cmd.Parameters.AddWithValue("@myanmarname", bolbankcheque.MyanmarName);
                cmd.Parameters.AddWithValue("@locationid", bolbankcheque.LocationID);
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

        #region "FillLocation"
        public List<BolLocation> FillLocation()
        {
            List<BolLocation> lstLocation = new List<BolLocation>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_SelectLocation", con);
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
                        BolLocation bollocation = new BolLocation();

                        bollocation.ID = Int32.Parse(reader["ID"].ToString());
                        bollocation.Location = reader["Location"].ToString();
                        lstLocation.Add(bollocation);
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
            return lstLocation;

        }
        #endregion

        #region "SaveBankCheque"
        public int SaveBankCheque(BOLBankCheque bolbankcheque)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_InsertBankCheque", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                cmd.Parameters.AddWithValue("@bankchequeid", bolbankcheque.BankChequeID);
                cmd.Parameters.AddWithValue("@bankchequename", bolbankcheque.BankChequeName);
                cmd.Parameters.AddWithValue("@myamarname", bolbankcheque.MyanmarName);
                cmd.Parameters.AddWithValue("@locationid", bolbankcheque.LocationID);

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

        #region "ShowAllBankCheque"
        public List<BOLBankCheque> ShowAllBankCheque()
        {
            List<BOLBankCheque> lstbankcheque = new List<BOLBankCheque>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_ShowAllBankCheque", con);
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
                        BOLBankCheque bolbankcheque = new BOLBankCheque();
                        bolbankcheque.BankChequeID = Int32.Parse(reader["Bank_Cheque_ID"].ToString());
                        bolbankcheque.BankChequeName = reader["Description"].ToString();
                        bolbankcheque.MyanmarName = reader["Myanmar_Description"].ToString();
                        bolbankcheque.LocationID = Int32.Parse(reader["LocationID"].ToString());
                        bolbankcheque.LocationName = reader["Location"].ToString();
                        lstbankcheque.Add(bolbankcheque);
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
            return lstbankcheque;
        }
        #endregion

        #region "UpdateBankCheque"
        public int UpdateBankCheque(BOLBankCheque bolbankcheque)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdateBankCheque", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@bankchequeid", bolbankcheque.BankChequeID);
                cmd.Parameters.AddWithValue("@bankchequename", bolbankcheque.BankChequeName);
                cmd.Parameters.AddWithValue("@myanmarname", bolbankcheque.MyanmarName);
                cmd.Parameters.AddWithValue("@locationID", bolbankcheque.LocationID);

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

        #region "DeleteBankCheque"
        public int DeleteBankCheque(int bankchequeid)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeleteBankCheque", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bankchequeid", bankchequeid);

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

        #region "GetBankChequeByLocationID"
        public List<BOLBankCheque> GetBankChequeByLocationID(int locationid)
        {
            List<BOLBankCheque> lstbankcheque = new List<BOLBankCheque>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetBankChequeByLocationID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LocationID", locationid);
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
                        BOLBankCheque bolbankcheque = new BOLBankCheque();
                        bolbankcheque.BankChequeID = Int32.Parse(reader["Bank_Cheque_ID"].ToString());
                        bolbankcheque.BankChequeName = reader["Description"].ToString();
                        bolbankcheque.MyanmarName = reader["Myanmar_Description"].ToString();
                        bolbankcheque.LocationID = Int32.Parse(reader["LocationID"].ToString());
                        lstbankcheque.Add(bolbankcheque);
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
            return lstbankcheque;
        }
        #endregion

        #region "DuplicateBankChequeID"
        public List<BOLBankCheque> DuplicateBankCheque(String bankchequeid)
        {
            List<BOLBankCheque> txtBankChequeID = new List<BOLBankCheque>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_CheckDuplicateBankCheque", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BankChequeID", bankchequeid);

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
                        BOLBankCheque bolbankcheque = new BOLBankCheque();
                        bolbankcheque.BankChequeID = Convert.ToInt32(reader["Bank_Cheque_ID"].ToString());
                        txtBankChequeID.Add(bolbankcheque);
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
            return txtBankChequeID;
        }
        #endregion

    }
}
