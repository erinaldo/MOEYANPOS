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
    class DALCustomer
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr   = MoeYanConfiguration.GetConnection();
        #endregion

        #region "SelectAll"
        public List<BOLCustomer> SelectAllUser()
        {
            List<BOLCustomer> lstcustomer = new List<BOLCustomer>();
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("", con);
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
                        BOLCustomer bolcustomer = new BOLCustomer();
                        bolcustomer.ID = long.Parse(reader["ID"].ToString());
                        bolcustomer.CustomerID = reader["CustomerID"].ToString();
                        bolcustomer.Name = reader["Name"].ToString();
                        bolcustomer.MyanmarName = reader["MyanmarName"].ToString();
                        bolcustomer.Memberid = reader["MemberID"].ToString();
                        bolcustomer.Address = reader["Address"].ToString();
                        bolcustomer.Phone = reader["Phone"].ToString();
                        bolcustomer.Email = reader["EMail"].ToString();
                        bolcustomer.Customerlevel = reader["CustomerLevel"].ToString();
                        bolcustomer.Township = Int32.Parse(reader["Township"].ToString());
                        bolcustomer.Divisionid = Int32.Parse(reader["DivisionID"].ToString());
                        bolcustomer.Creditlimit = Decimal.Parse(reader["CreditLimit"].ToString());
                        bolcustomer.Iscash =bool.Parse( reader["IsCash"].ToString());
                        bolcustomer.Iscredit = bool.Parse(reader["IsCredit"].ToString());
                        bolcustomer.Shop = reader["Shop"].ToString();
                        bolcustomer.SortingID = Int32.Parse(reader["SortingID"].ToString());
                        lstcustomer.Add(bolcustomer);
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
            return lstcustomer;
        }
        #endregion

        #region "SaveCustomer"
            public int SaveCustomer(BOLCustomer bolcustomer)
            {
                int isSaved = 0;
                try
                {
                    con = new SqlConnection(Constr  );
                    cmd = new SqlCommand("SP_InsertCustomer", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();

                    cmd.Parameters.AddWithValue("@CustomerID", bolcustomer.CustomerID);
                    cmd.Parameters.AddWithValue("@Name", bolcustomer.Name);
                    cmd.Parameters.AddWithValue("@MyanmarName", bolcustomer.MyanmarName);
                    cmd.Parameters.AddWithValue("@MemberID", bolcustomer.Memberid);
                    cmd.Parameters.AddWithValue("@Address", bolcustomer.Address);
                    cmd.Parameters.AddWithValue("@Phone", bolcustomer.Phone);
                    cmd.Parameters.AddWithValue("@DateOfBirth", bolcustomer.Dateofbirth);
                    cmd.Parameters.AddWithValue("@JoinDate", bolcustomer.Joindate);
                    cmd.Parameters.AddWithValue("@CurrentDate", bolcustomer.Currentdate);
                    cmd.Parameters.AddWithValue("@NRC", bolcustomer.Nrc);
                    cmd.Parameters.AddWithValue("@EMail", bolcustomer.Email);
                    cmd.Parameters.AddWithValue("@CustomerLevel", bolcustomer.Customerlevel);
                    cmd.Parameters.AddWithValue("@Township", bolcustomer.Township);
                    cmd.Parameters.AddWithValue("@Division", bolcustomer.Divisionid);
                    cmd.Parameters.AddWithValue("@CreditLimit", bolcustomer.Creditlimit);
                    cmd.Parameters.AddWithValue("@IsCash", bolcustomer.Iscash);
                    cmd.Parameters.AddWithValue("@IsCredit", bolcustomer.Iscredit);
                    cmd.Parameters.AddWithValue("@Shop", bolcustomer.Shop);
                    cmd.Parameters.AddWithValue("@SortingID", bolcustomer.SortingID);
                    cmd.Parameters.AddWithValue("@DepositAmount", bolcustomer.DepositAmount);
                    cmd.Parameters.AddWithValue("@Department", bolcustomer.Departmentid);
                    cmd.Parameters.AddWithValue("@ContactPerson", bolcustomer.Contactperson);
                    cmd.Parameters.AddWithValue("@SaleTarget", bolcustomer.Saletarget);
                    cmd.Parameters.AddWithValue("@WholeSaelPrice", bolcustomer.Wholesaleprice);
                    cmd.Parameters.AddWithValue("@RetailSalePrice", bolcustomer.Retailsaleprice);
                    cmd.Parameters.AddWithValue("@CreditOpeningAmt", bolcustomer.CreditOpeningAmt);
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

        #region "DuplicateCustomer"
            public List<BOLCustomer> SelectCustomer(String customername,String EMail)
            {
                List<BOLCustomer> lstcustomer = new List<BOLCustomer>();
                try
                {
                    con = new SqlConnection(Constr  );
                    cmd = new SqlCommand("SP_CheckDuplicateCustomer", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name",customername);
                    cmd.Parameters.AddWithValue("@EMail", EMail);
                    

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
                            BOLCustomer bolcustomer = new BOLCustomer();
                            bolcustomer.Name = reader["Name"].ToString();
                            bolcustomer.Email = reader["EMail"].ToString();
                            lstcustomer.Add(bolcustomer);
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
                return lstcustomer;
            }
        #endregion

        #region "ChkPaymentType"
            public bool ChkPaymentType(long id)
            {
                bool paymenttype = false;
                try
                {
                    con = new SqlConnection(Constr);
                    cmd = new SqlCommand("SP_ChkPaymentType", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

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

        #region "ShowAllCustomer"
            public List<BOLCustomer> ShowAllCustomer()
            {
                List<BOLCustomer> lstCustomer = new List<BOLCustomer>();
                try
                {
                    con = new SqlConnection(Constr  );
                    cmd = new SqlCommand("SP_SelectAllCustomer", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@Name", customername);

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
                            BOLCustomer bolcustomer = new BOLCustomer();
                            bolcustomer.ID = long.Parse(reader["ID"].ToString());
                            bolcustomer.CustomerID= reader["CustomerID"].ToString();
                            bolcustomer.Name = reader["Name"].ToString();
                            bolcustomer.MyanmarName = reader["MyanmarName"].ToString();
                            bolcustomer.Memberid = reader["MemberID"].ToString();
                            bolcustomer.Address = reader["Address"].ToString();
                            bolcustomer.Phone = reader["Phone"].ToString();
                            bolcustomer.Dateofbirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                            bolcustomer.Joindate = DateTime.Parse(reader["JoinDate"].ToString());
                            bolcustomer.Currentdate = DateTime.Parse(reader["CurrentDate"].ToString());
                            bolcustomer.Nrc = reader["NRC"].ToString();
                            bolcustomer.Email = reader["EMail"].ToString();
                            bolcustomer.Customerlevel = reader["CustomerLevel"].ToString();
                            bolcustomer.Township =  Int32.Parse(reader["Township"].ToString());
                            bolcustomer.Divisionid = Int32.Parse(reader["Division"].ToString());
                            bolcustomer.Creditlimit = Decimal.Parse(reader["CreditLimit"].ToString());
                            bolcustomer.Iscash = bool.Parse(reader["IsCash"].ToString());
                            bolcustomer.Iscredit = bool.Parse(reader["IsCredit"].ToString());
                            bolcustomer.Shop = reader["Shop"].ToString();
                            bolcustomer.SortingID = Int32.Parse(reader["SortingID"].ToString());
                            bolcustomer.DepositAmount = Decimal.Parse(reader["DepositAmount"].ToString());
                            bolcustomer.Departmentid = Int32.Parse(reader["DepartmentName"].ToString());
                            bolcustomer.Contactperson = reader["ContactPerson"].ToString();
                            bolcustomer.Saletarget = Decimal.Parse(reader["SaleTraget"].ToString());
                            bolcustomer.Wholesaleprice = bool.Parse(reader["WholeSalePrice"].ToString());
                            bolcustomer.Retailsaleprice = bool.Parse(reader["RetailSalePrice"].ToString());
                            lstCustomer.Add(bolcustomer);
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
                return lstCustomer;
            }
            #endregion

        #region "ShowAllCustomerList"
            public List<BOLCustomer> ShowAllCustomerList()
            {
                List<BOLCustomer> lstCustomer = new List<BOLCustomer>();
                try
                {
                    con = new SqlConnection(Constr);
                    cmd = new SqlCommand("SP_ShowAllCustomer", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@Name", customername);

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
                            BOLCustomer bolcustomer = new BOLCustomer();
                            bolcustomer.ID = long.Parse(reader["ID"].ToString());
                            bolcustomer.CustomerID = reader["CustomerID"].ToString();
                            bolcustomer.Name = reader["Name"].ToString();
                            bolcustomer.MyanmarName = reader["MyanmarName"].ToString();
                            bolcustomer.Memberid = reader["MemberID"].ToString();
                            bolcustomer.Address = reader["Address"].ToString();
                            bolcustomer.Phone = reader["Phone"].ToString();
                            bolcustomer.Dateofbirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                            bolcustomer.Joindate = DateTime.Parse(reader["JoinDate"].ToString());
                            bolcustomer.Currentdate = DateTime.Parse(reader["CurrentDate"].ToString());
                            bolcustomer.Nrc = reader["NRC"].ToString();
                            bolcustomer.Email = reader["EMail"].ToString();
                            bolcustomer.Customerlevel = reader["CustomerLevel"].ToString();
                            bolcustomer.TownshipName = reader["Township"].ToString();
                            bolcustomer.Division = reader["Division"].ToString();
                            bolcustomer.Creditlimit = Decimal.Parse(reader["CreditLimit"].ToString());
                            bolcustomer.Iscash = bool.Parse(reader["IsCash"].ToString());
                            bolcustomer.Iscredit = bool.Parse(reader["IsCredit"].ToString());
                            bolcustomer.Shop = reader["Shop"].ToString();
                            bolcustomer.SortingID = Int32.Parse(reader["SortingID"].ToString());
                            bolcustomer.DepositAmount = Decimal.Parse(reader["DepositAmount"].ToString());
                            bolcustomer.DepartmentName = reader["DepartmentName"].ToString();
                            bolcustomer.Contactperson = reader["ContactPerson"].ToString();
                            bolcustomer.Saletarget = Decimal.Parse(reader["SaleTraget"].ToString());
                            bolcustomer.Wholesaleprice = bool.Parse(reader["WholeSalePrice"].ToString());
                            bolcustomer.Retailsaleprice = bool.Parse(reader["RetailSalePrice"].ToString());
                            bolcustomer.Township = Int32.Parse(reader["TownshipID"].ToString());
                            bolcustomer.Divisionid = Int32.Parse(reader["DivisionID"].ToString());
                            bolcustomer.Departmentid = Int32.Parse(reader["DepartmentID"].ToString());
                            bolcustomer.CreditOpeningAmt = Int32.Parse(reader["CreditOpeningAmt"].ToString());
                            lstCustomer.Add(bolcustomer);
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
                return lstCustomer;
            }
            #endregion

        #region "CustomerList"

            public List<BOLCustomer> GetCustomer(string customername, string address)
            {
                List<BOLCustomer> lstcustomer = new List<BOLCustomer>();
                try
                {
                    con = new SqlConnection(Constr  );
                    cmd = new SqlCommand("sp_GetCustomerList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerName", customername);
                    cmd.Parameters.AddWithValue("@Address", address);

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
                            BOLCustomer bolcustomer = new BOLCustomer();
                            bolcustomer.ID = long.Parse(reader["ID"].ToString());
                            bolcustomer.CustomerID = reader["CustomerID"].ToString();
                            bolcustomer.Name = reader["Name"].ToString();
                            bolcustomer.MyanmarName = reader["MyanmarName"].ToString();
                            bolcustomer.Address = reader["Address"].ToString();
                            bolcustomer.Phone = reader["Phone"].ToString();

                            lstcustomer.Add(bolcustomer);
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
                return lstcustomer;
            }
            #endregion

        #region "GetCustomerByCustomerID"
            public BOLCustomer GetCustomerByCustomerID(long customerid)
            {
                BOLCustomer bolcustomer = new BOLCustomer();
                try
                {
                    con = new SqlConnection(Constr  );
                    cmd = new SqlCommand("SP_GetCustomerByCustomerID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", customerid);

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
                            bolcustomer.ID = long.Parse(reader["ID"].ToString());
                            bolcustomer.CustomerID = reader["CustomerID"].ToString();
                            bolcustomer.Name = reader["Name"].ToString();
                            bolcustomer.MyanmarName = reader["MyanmarName"].ToString();
                            bolcustomer.Memberid = reader["MemberID"].ToString();
                            bolcustomer.Address = reader["Address"].ToString();
                            bolcustomer.Phone = reader["Phone"].ToString();
                            bolcustomer.Dateofbirth = Convert.ToDateTime(reader["DateOfBirth"].ToString());
                            bolcustomer.Joindate = Convert.ToDateTime(reader["JoinDate"].ToString());
                            bolcustomer.Currentdate=Convert.ToDateTime(reader["CurrentDate"].ToString());
                            bolcustomer.Nrc = reader["NRC"].ToString();
                            bolcustomer.Email = reader["Email"].ToString();
                            bolcustomer.Customerlevel = reader["Customerlevel"].ToString();
                            bolcustomer.Township = Int32.Parse(reader["Township"].ToString());
                            bolcustomer.Divisionid = Int32.Parse(reader["DivisionID"].ToString());
                            bolcustomer.Creditlimit = Decimal.Parse(reader["Creditlimit"].ToString());
                            bolcustomer.Iscash = bool.Parse(reader["IsCash"].ToString());
                            bolcustomer.Iscredit = bool.Parse(reader["IsCredit"].ToString());
                            bolcustomer.Shop = reader["Shop"].ToString();
                            bolcustomer.SortingID = Int32.Parse(reader["SortingID"].ToString());
                            bolcustomer.DepositAmount = Decimal.Parse(reader["DepositAmount"].ToString());
                            bolcustomer.Departmentid = Int32.Parse(reader["DepartmentID"].ToString());
                            bolcustomer.Contactperson = reader["ContactPerson"].ToString();
                            bolcustomer.Saletarget = Decimal.Parse(reader["SaleTraget"].ToString());
                            bolcustomer.CreditOpeningAmt = Decimal.Parse(reader["CreditOpeningAmt"].ToString());
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
                return bolcustomer;
            }
            #endregion

        #region "GetCustomer"
            public List<BOLCustomer> GetCustomer(string customername,string Type,Int32 nothing)//Int32 nothing is because of ambiguous method with upper GetCustomer from customerList
            {
                List<BOLCustomer> lstCustomer = new List<BOLCustomer>();
                try
                {
                    con = new SqlConnection(Constr  );
                    cmd = new SqlCommand("SP_GetAllCustomer", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", customername);
                    cmd.Parameters.AddWithValue("@Type", Type);

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
                            BOLCustomer bolcustomer = new BOLCustomer();
                            bolcustomer.ID = long.Parse(reader["ID"].ToString());
                            bolcustomer.CustomerID = reader["CustomerID"].ToString();
                            bolcustomer.Name = reader["Name"].ToString();
                            bolcustomer.MyanmarName = reader["MyanmarName"].ToString();
                            bolcustomer.Phone = reader["Phone"].ToString();
                            bolcustomer.Email = reader["Address"].ToString();
                            bolcustomer.Wholesaleprice = bool.Parse(reader["Wholesaleprice"].ToString());
                            bolcustomer.Creditlimit = decimal.Parse(reader["CreditLimit"].ToString()); 
                            lstCustomer.Add(bolcustomer);
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
                return lstCustomer;
            }
            #endregion        

            #region "GetCustomer2"
            public List<BOLCustomer> GetCustomer2(string customername)
            {
                List<BOLCustomer> lstCustomer = new List<BOLCustomer>();
                try
                {
                    con = new SqlConnection(Constr);
                    cmd = new SqlCommand("SP_GetAllCustomer_002", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", customername);

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
                            BOLCustomer bolcustomer = new BOLCustomer();
                            bolcustomer.ID = long.Parse(reader["ID"].ToString());
                            bolcustomer.CustomerID = reader["CustomerID"].ToString();
                            bolcustomer.Name = reader["Name"].ToString();
                            bolcustomer.MyanmarName = reader["MyanmarName"].ToString();
                            bolcustomer.Creditlimit = decimal.Parse(reader["CreditLimit"].ToString());
                            bolcustomer.GrandTotal = decimal.Parse(reader["GrandTotal"].ToString());
                            bolcustomer.PaidCreditAmount = decimal.Parse(reader["PaidCreditAmount"].ToString());
                            bolcustomer.CreditAmount = decimal.Parse(reader["CreditAmount"].ToString());
                            lstCustomer.Add(bolcustomer);
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
                return lstCustomer;
            }
            #endregion        

        #region "UpdateCustomerByCustomer"
            public int UpdateCustomerByCustomerID(BOLCustomer bolcustomer)
            {
                int isupdated = 0;
                try
                {
                    con = new SqlConnection(Constr  );
                    cmd = new SqlCommand("SP_UpdateCustomer", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    cmd.Parameters.AddWithValue("@ID", bolcustomer.ID);
                    cmd.Parameters.AddWithValue("@customerid", bolcustomer.CustomerID);
                    cmd.Parameters.AddWithValue("@name", bolcustomer.Name);
                    cmd.Parameters.AddWithValue("@MyanmarName", bolcustomer.MyanmarName);
                    cmd.Parameters.AddWithValue("@MemberID", bolcustomer.Memberid);
                    cmd.Parameters.AddWithValue("@Address", bolcustomer.Address);
                    cmd.Parameters.AddWithValue("@Phone", bolcustomer.Phone);
                    cmd.Parameters.AddWithValue("@DateOfBirth", bolcustomer.Dateofbirth);
                    cmd.Parameters.AddWithValue("@JoinDate", bolcustomer.Joindate);
                    cmd.Parameters.AddWithValue("@CurrentDate", bolcustomer.Currentdate);
                    cmd.Parameters.AddWithValue("@NRC", bolcustomer.Nrc);
                    cmd.Parameters.AddWithValue("@EMail", bolcustomer.Email);
                    cmd.Parameters.AddWithValue("@customerLevel", bolcustomer.Customerlevel);
                    cmd.Parameters.AddWithValue("@Township", bolcustomer.Township);
                    cmd.Parameters.AddWithValue("@Division", bolcustomer.Divisionid);
                    cmd.Parameters.AddWithValue("@CreditLimit", bolcustomer.Creditlimit);
                    cmd.Parameters.AddWithValue("@IsCash", bolcustomer.Iscash);
                    cmd.Parameters.AddWithValue("@IsCredit", bolcustomer.Iscredit);
                    cmd.Parameters.AddWithValue("@Shop", bolcustomer.Shop);                                  
                    cmd.Parameters.AddWithValue("@SortingID", bolcustomer.SortingID);
                    cmd.Parameters.AddWithValue("@DepositAmount", bolcustomer.DepositAmount);
                    cmd.Parameters.AddWithValue("@Department", bolcustomer.Departmentid);
                    cmd.Parameters.AddWithValue("@ContactPerson", bolcustomer.Contactperson);
                    cmd.Parameters.AddWithValue("@SaleTarget", bolcustomer.Saletarget);
                    cmd.Parameters.AddWithValue("@WholeSalePrice", bolcustomer.Wholesaleprice);
                    cmd.Parameters.AddWithValue("@RetailSalePrice", bolcustomer.Retailsaleprice);
                    cmd.Parameters.AddWithValue("@CreditOpeningAmt", bolcustomer.CreditOpeningAmt);

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
            public int UpdateCreditAmountByCustomerID(BOLCustomer bolcustomer)
            {
                int isupdated = 0;
                try
                {
                    con = new SqlConnection(Constr);
                    cmd = new SqlCommand("SP_UpdateCreditAmount", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();

                    cmd.Parameters.AddWithValue("@ID", bolcustomer.CustomerID);
                    cmd.Parameters.AddWithValue("@CreditOpeningAmt", bolcustomer.CreditOpeningAmt);

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

        #region "GetCustomerID"
            public long GetCustomerID()
            {
                long customerid = 0;
                try
                {
                    con = new SqlConnection(Constr  );
                    cmd = new SqlCommand("SP_GetCustomerID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    customerid = Convert.ToInt64( cmd.ExecuteScalar());
                    if (customerid == -1 | customerid == null)
                    {
                        customerid = 1;
                    }
                    else
                    {
                        customerid += 1;
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
                return customerid;

            }
            #endregion

        #region "DeleteCustomer"
            public int DeleteCustomer(long id)
            {
                int isdelete = 0;
                try
                {
                    con = new SqlConnection(Constr  );
                    cmd = new SqlCommand("SP_DeleteCustomer", con);
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

        #region "CustomerVoucher"
            public List<BOLCustomer> SelectCustomerVoucher(long CustomerID)
            {
                List<BOLCustomer> lstcustomer = new List<BOLCustomer>();
                try
                {
                    con = new SqlConnection(Constr  );
                    cmd = new SqlCommand("SP_CustomerVoucher", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                BOLCustomer bolcustomer = new BOLCustomer();

                                bolcustomer.ID = long.Parse(reader["ID"].ToString());
                                bolcustomer.CustomerID = reader["CustomerID"].ToString();
                                bolcustomer.Name = reader["Name"].ToString();
                                bolcustomer.Memberid = reader["MemberID"].ToString();
                                bolcustomer.Address = reader["Address"].ToString();
                                bolcustomer.Phone = reader["Phone"].ToString();
                                bolcustomer.Email = reader["EMail"].ToString();
                                bolcustomer.Customerlevel = reader["CustomerLevel"].ToString();
                                bolcustomer.Township = Int32.Parse(reader["Township"].ToString());
                                bolcustomer.Divisionid = Int32.Parse(reader["DivisionID"].ToString());
                                bolcustomer.Creditlimit = Decimal.Parse(reader["CreditLimit"].ToString());
                                bolcustomer.Iscash = bool.Parse(reader["IsCash"].ToString());
                                bolcustomer.Iscredit = bool.Parse(reader["IsCredit"].ToString());
                                bolcustomer.Shop = reader["Shop"].ToString();
                                bolcustomer.SortingID = Int32.Parse(reader["SortingID"].ToString());
                                lstcustomer.Add(bolcustomer);
                            }
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
                return lstcustomer;
            }
        #endregion

        #region "GetSystemID"
            public int GetSystemID()
            {
                int systemid = 0;
                try
                {
                    con = new SqlConnection(Constr  );
                    cmd = new SqlCommand("SP_GetSystem", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    systemid = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return systemid;
            }
        #endregion

            #region "GetCustomerCode"
            public string GetCustomerCode()
            {
                string customerid = "";
                try
                {
                    con = new SqlConnection(Constr);
                    cmd = new SqlCommand("GetCustomerID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    customerid = Convert.ToString(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return customerid;
            }
            #endregion

        #region "GetDivision"
            public List<BOLCustomer> GetCustomer(int division)
            {
                List<BOLCustomer> searchcustomer = new List<BOLCustomer>();
                try
                {
                    con = new SqlConnection(Constr);
                    cmd = new SqlCommand("SP_SearchDivision", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Division", division);
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
                            BOLCustomer bolcustomer = new BOLCustomer();
                            bolcustomer.ID = long.Parse(reader["ID"].ToString());
                            bolcustomer.CustomerID = reader["CustomerID"].ToString();
                            bolcustomer.Name = reader["Name"].ToString();
                            bolcustomer.Memberid = reader["MemberID"].ToString();
                            bolcustomer.Address = reader["Address"].ToString();
                            bolcustomer.Phone = reader["Phone"].ToString();
                            bolcustomer.Dateofbirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                            bolcustomer.Joindate = DateTime.Parse(reader["JoinDate"].ToString());
                            bolcustomer.Currentdate = DateTime.Parse(reader["CurrentDate"].ToString());
                            bolcustomer.Nrc = reader["NRC"].ToString();
                            bolcustomer.Email = reader["EMail"].ToString();
                            bolcustomer.Customerlevel = reader["CustomerLevel"].ToString();
                            bolcustomer.Township = Int32.Parse(reader["Township"].ToString());
                            bolcustomer.Division = reader["Division"].ToString();
                            bolcustomer.Creditlimit = Decimal.Parse(reader["CreditLimit"].ToString());
                            bolcustomer.Iscash = bool.Parse(reader["IsCash"].ToString());
                            bolcustomer.Iscredit = bool.Parse(reader["IsCredit"].ToString());
                            bolcustomer.Shop = reader["Shop"].ToString();
                            bolcustomer.SortingID = Int32.Parse(reader["SortingID"].ToString());
                            bolcustomer.DepositAmount = Decimal.Parse(reader["DepositAmount"].ToString());
                            bolcustomer.DepartmentName = reader["DepartmentName"].ToString();
                            bolcustomer.Contactperson = reader["ContactPerson"].ToString();
                            bolcustomer.Saletarget = Decimal.Parse(reader["SaleTraget"].ToString());
                            searchcustomer.Add(bolcustomer);
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
                return searchcustomer;
            }
        #endregion

        #region "GetCustomer"
            public List<BOLCustomer> SearchCustomer(string customername)//,string division,string township
            {
                List<BOLCustomer> lstCustomer = new List<BOLCustomer>();
                try
                {
                    con = new SqlConnection(Constr);
                    cmd = new SqlCommand("SP_SearchCustomer", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", customername);
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
                            BOLCustomer bolcustomer = new BOLCustomer();
                            bolcustomer.ID = long.Parse(reader["ID"].ToString());
                            bolcustomer.CustomerID = reader["CustomerID"].ToString();
                            bolcustomer.Name = reader["Name"].ToString();
                            bolcustomer.MyanmarName = reader["MyanmarName"].ToString();
                            bolcustomer.Memberid = reader["MemberID"].ToString();
                            bolcustomer.Address = reader["Address"].ToString();
                            bolcustomer.Phone = reader["Phone"].ToString();
                            bolcustomer.Dateofbirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                            bolcustomer.Joindate = DateTime.Parse(reader["JoinDate"].ToString());
                            bolcustomer.Currentdate = DateTime.Parse(reader["CurrentDate"].ToString());
                            bolcustomer.Nrc = reader["NRC"].ToString();
                            bolcustomer.Email = reader["EMail"].ToString();
                            bolcustomer.Customerlevel = reader["CustomerLevel"].ToString();
                            bolcustomer.TownshipName = reader["Township"].ToString();
                            bolcustomer.Township =  Int32.Parse( reader["TownshipID"].ToString());
                            bolcustomer.Division = reader["Division"].ToString();
                            bolcustomer.Divisionid = Int32.Parse(reader["DivisionID"].ToString());
                            bolcustomer.Creditlimit = Decimal.Parse(reader["CreditLimit"].ToString());
                            bolcustomer.Iscash = bool.Parse(reader["IsCash"].ToString());
                            bolcustomer.Iscredit = bool.Parse(reader["IsCredit"].ToString());
                            bolcustomer.Shop = reader["Shop"].ToString();
                            bolcustomer.SortingID = Int32.Parse(reader["SortingID"].ToString());
                            bolcustomer.DepositAmount = Decimal.Parse(reader["DepositAmount"].ToString());
                            bolcustomer.DepartmentName = reader["DepartmentName"].ToString();
                            bolcustomer.Departmentid = Int32.Parse(reader["DepartmentID"].ToString());
                            bolcustomer.Contactperson = reader["ContactPerson"].ToString();
                            bolcustomer.Saletarget = Decimal.Parse(reader["SaleTraget"].ToString());
                            bolcustomer.Wholesaleprice = bool.Parse(reader["WholeSalePrice"].ToString());
                            bolcustomer.Retailsaleprice = bool.Parse(reader["RetailSalePrice"].ToString());
                            bolcustomer.Divisionid = Int32.Parse(reader["Divisionid"].ToString());
                            bolcustomer.Departmentid = Int32.Parse(reader["Departmentid"].ToString());
                            bolcustomer.CreditOpeningAmt = Decimal.Parse(reader["CreditOpeningAmt"].ToString());
                            lstCustomer.Add(bolcustomer);
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
                return lstCustomer;
            }
            #endregion 

        #region "CheckWholeSale"
            public bool CheckWholeSale(long ID)
            {
                bool IsWholeSale=false ;
                try
                {
                    con = new SqlConnection(Constr);
                    cmd = new SqlCommand("SP_CheckWholeSale", con);
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
                            IsWholeSale = bool.Parse( reader["WholeSalePrice"].ToString());
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
                return IsWholeSale;
            }
            #endregion
    }

}
