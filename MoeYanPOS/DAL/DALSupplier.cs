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
    class DALSupplier
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        String constr = MoeYanConfiguration.GetConnection();
        #endregion

        #region "SaveSupplier"
        public int SaveSupplier(BOLSupplier bolsupplier)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_InsertSupplier", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();

                cmd.Parameters.AddWithValue("@suppliername", bolsupplier.SupplierName);
                cmd.Parameters.AddWithValue("@Address", bolsupplier.Address);
                cmd.Parameters.AddWithValue("@Phone", bolsupplier.Phone);
                cmd.Parameters.AddWithValue("@EMail", bolsupplier.Email);
                cmd.Parameters.AddWithValue("@Township", bolsupplier.TownshipID);
                cmd.Parameters.AddWithValue("@CreditLimit", bolsupplier.Creditlimit);
                cmd.Parameters.AddWithValue("@iscash", bolsupplier.Iscash);
                cmd.Parameters.AddWithValue("@iscredit", bolsupplier.Iscredit);

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

        #region "ShowAllSupplier"
        public List<BOLSupplier> ShowAllSupplier()
        {
            List<BOLSupplier> lstsupplier = new List<BOLSupplier>();
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_ShowAllSupplier", con);
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
                        BOLSupplier bolsupplier = new BOLSupplier();
                        bolsupplier.Supplierid = Int32.Parse(reader["SupplierID"].ToString());
                        bolsupplier.SupplierName = reader["SupplierName"].ToString();
                        bolsupplier.Address = reader["Address"].ToString();
                        bolsupplier.Phone = reader["Phone"].ToString();
                        bolsupplier.Email = reader["EMail"].ToString();
                        bolsupplier.Township = reader["Township"].ToString();
                        bolsupplier.TownshipID = Int32.Parse(reader["TownshipID"].ToString());
                        bolsupplier.Creditlimit = Decimal.Parse(reader["CreditLimit"].ToString());
                        bolsupplier.Iscash = Boolean.Parse(reader["IsCash"].ToString());
                        bolsupplier.Iscredit = Boolean.Parse(reader["IsCredit"].ToString());

                        lstsupplier.Add(bolsupplier);
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
            return lstsupplier;
        }
        #endregion

        #region "EditSupplier"
        public int EditSupplier(BOLSupplier bolsupplier)
        {
            int isupdate = 0;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_UpdateSupplier", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();

                cmd.Parameters.AddWithValue("@supplierid", bolsupplier.Supplierid);
                cmd.Parameters.AddWithValue("@suppliername", bolsupplier.SupplierName);
                cmd.Parameters.AddWithValue("@Address", bolsupplier.Address);
                cmd.Parameters.AddWithValue("@Phone", bolsupplier.Phone);
                cmd.Parameters.AddWithValue("@EMail", bolsupplier.Email);
                cmd.Parameters.AddWithValue("@TownshipID", bolsupplier.TownshipID);
                cmd.Parameters.AddWithValue("@CreditLimit", bolsupplier.Creditlimit);
                cmd.Parameters.AddWithValue("@IsCash", bolsupplier.Iscash);
                cmd.Parameters.AddWithValue("@IsCredit", bolsupplier.Iscredit);

                isupdate = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return isupdate;
        }
        #endregion
        
        #region "DeleteSupplier"
        public int DeleteSupplier(int supplierid)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_DeleteSupplier", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", supplierid);

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

        #region "GetSupplier"
        public List<BOLSupplier> GetSupplier(string suppliername)
        {
            List<BOLSupplier> lstsupplier = new List<BOLSupplier>();
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetAllSupplier", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", suppliername);

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
                        BOLSupplier bolsupplier = new BOLSupplier();
                        bolsupplier.Supplierid = Int32.Parse(reader["SupplierID"].ToString());
                        bolsupplier.SupplierName = reader["SupplierName"].ToString();
                        bolsupplier.Phone = reader["Phone"].ToString();
                        bolsupplier.Email = reader["Address"].ToString();
                        //bolsupplier.TownshipID = Int32.Parse(reader["TownshipID"].ToString());
                        bolsupplier.Township = reader["Township"].ToString();
                        lstsupplier.Add(bolsupplier);
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
            return lstsupplier;
        }
        #endregion

        #region "ChkPaymentType"
        public bool ChkPaymentType(int supplierid)
        {
            bool paymenttype = false;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_ChkSupplierPaymentType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SupplierID", supplierid);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                object o = new object();
                o = cmd.ExecuteScalar();
                if (o.GetType() == typeof(Boolean))
                {
                    paymenttype = (bool)o;
                }
                else
                {
                    paymenttype = false;
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
            return paymenttype;
        }
        #endregion

        #region "GetSupplier"
        public List<BOLSupplier> SearchSupplier(string suppliername)//,string division,string township
        {
            List<BOLSupplier> lstsupplier = new List<BOLSupplier>();
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_SearchSupplier", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", suppliername);

                //cmd.Parameters.AddWithValue("@Division", division);
                //cmd.Parameters.AddWithValue("@Township", township);

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
                        BOLSupplier bolsupplier = new BOLSupplier();
                        bolsupplier.Supplierid = Int32.Parse(reader["SupplierID"].ToString());
                        bolsupplier.SupplierName = reader["SupplierName"].ToString();
                        bolsupplier.Address = reader["Address"].ToString();
                        bolsupplier.Phone = reader["Phone"].ToString();
                        bolsupplier.Email = reader["EMail"].ToString();
                        bolsupplier.Township = reader["Township"].ToString();
                        bolsupplier.TownshipID = Int32.Parse(reader["TownshipID"].ToString());
                        bolsupplier.Creditlimit = Decimal.Parse(reader["creditLimit"].ToString());
                        bolsupplier.Iscash = bool.Parse(reader["IsCash"].ToString());
                        bolsupplier.Iscredit = bool.Parse(reader["IsCredit"].ToString());
                        bolsupplier.TownshipID = Int32.Parse(reader["TownshipID"].ToString());
                        lstsupplier.Add(bolsupplier);
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
            return lstsupplier;
        }
        #endregion 
    }
}
