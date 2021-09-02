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
    class DALSystem
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr   = MoeYanConfiguration.GetConnection();
        #endregion

        #region "SaveSystem"
        public int SaveSystem(BOLSystem bolsystem)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_InsertSystem", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                cmd.Parameters.AddWithValue("@CompanyName", bolsystem.Companyname);
                cmd.Parameters.AddWithValue("@Address", bolsystem.Address);
                cmd.Parameters.AddWithValue("@PhoneNo", bolsystem.Phno);
                cmd.Parameters.AddWithValue("@Web", bolsystem.Web);
                cmd.Parameters.AddWithValue("@Email", bolsystem.Email);
                cmd.Parameters.AddWithValue("@PrinterSlip", bolsystem.Printerslip);
                cmd.Parameters.AddWithValue("@PrinterVoucher", bolsystem.Printervoucher);
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

        #region "GetSystemID"
        public int GetSystemID()
        {
            int systemid = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_GetSystem", con);
                cmd.CommandType=CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                systemid = (int)cmd.ExecuteScalar();
                if (systemid == -1 | systemid == null)
                {
                    systemid = 1;
                }
                else
                {
                    systemid += 1;
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
            return systemid;
        }
        #endregion

        #region "ShowAllSystem"
        public List<BOLSystem> ShowAllSystem()
        {
            List<BOLSystem> lstsystem = new List<BOLSystem>();
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_SelectSystemSettings", con);
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
                        BOLSystem bolsystem = new BOLSystem();
                        bolsystem.Companyid = Int32.Parse(reader["CompanyID"].ToString());
                        bolsystem.Companyname = reader["CompanyName"].ToString();
                        bolsystem.Address = reader["Address"].ToString();
                        bolsystem.Phno = reader["PhoneNo"].ToString();
                        bolsystem.Web = reader["Web"].ToString();
                        bolsystem.Email = reader["Email"].ToString();
                        bolsystem.Printerslip = reader["PrinterSlip"].ToString();
                        bolsystem.Printervoucher = reader["PrinterVoucher"].ToString();
                        lstsystem.Add(bolsystem);
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
            return lstsystem;
        }      
        
        #endregion

        #region "updateSystem"
        public int UpdateSystem(BOLSystem bolsystem)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_UpdateSystemSetting", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.Parameters.AddWithValue("@CompanyID", bolsystem.Companyid);
                cmd.Parameters.AddWithValue("@CompanyName", bolsystem.Companyname);
                cmd.Parameters.AddWithValue("@Address", bolsystem.Address);
                cmd.Parameters.AddWithValue("@PhoneNo", bolsystem.Phno);
                cmd.Parameters.AddWithValue("@Web", bolsystem.Web);
                cmd.Parameters.AddWithValue("@Email", bolsystem.Email);
                cmd.Parameters.AddWithValue("@PrinterSlip", bolsystem.Printerslip);
                cmd.Parameters.AddWithValue("@PrinterVoucher", bolsystem.Printervoucher);

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

        #region "DeleeteSystem"
        public int DeleteSystem(int companyid)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_DeleteSystem", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@companyid", companyid);
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

        #region "CheckSystemSetting"
        public int CheckSystemSetting()
        {
            int systemid = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_CheckSystemSetting", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                systemid = (int)cmd.ExecuteScalar();
                if (systemid == -1 | systemid == null)
                {
                    systemid = 1;
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
            return systemid;
        }
        #endregion

    }
}
