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
    class DALStaff
    {

        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr = MoeYanConfiguration.GetConnection();
        #endregion

        #region "GetStaffID"
        public int GetStaffID()
        {
            int staffid = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetStaffID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                staffid = Convert.ToInt32(cmd.ExecuteScalar());
                if (staffid == -1 | staffid == null)
                {
                    staffid = 1;
                }
                else
                {
                    staffid += 1;
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
            return staffid;
        }
        #endregion

        #region "SaveUser"
        public int SaveUser(BOLStaff bolstaff)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_InsertStaff", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                cmd.Parameters.AddWithValue("@staffid", bolstaff.StaffID);
                cmd.Parameters.AddWithValue("@staffname", bolstaff.StaffName);
                cmd.Parameters.AddWithValue("@departmentid", bolstaff.DepartmentID);
                cmd.Parameters.AddWithValue("@mbcstaffid", bolstaff.MCBStaffID);
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

        #region "FillDepartment"
        public List<BOLDepartment> FillDepartment()
        {
            List<BOLDepartment> lstDepartment = new List<BOLDepartment>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_SelectDepartment", con);
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
                        BOLDepartment boldepartment = new BOLDepartment();

                        boldepartment.Id = Int32.Parse(reader["DepartmentID"].ToString());
                        boldepartment.Departmentname = reader["DepartmentName"].ToString();
                        lstDepartment.Add(boldepartment);
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
            return lstDepartment;

        }
        #endregion

        #region "SaveStaff"
        public int SaveStaff(BOLStaff bolstaff)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_InsertStaff", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                cmd.Parameters.AddWithValue("@staffid", bolstaff.StaffID);
                cmd.Parameters.AddWithValue("@staffname", bolstaff.StaffName);
                cmd.Parameters.AddWithValue("@departmentid", bolstaff.DepartmentID);
                cmd.Parameters.AddWithValue("@mbcstaffid", bolstaff.MCBStaffID);

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

        #region "ShowAllStaff"
        public List<BOLStaff> ShowAllStaff()
        {
            List<BOLStaff> lststaff = new List<BOLStaff>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_ShowAllStaff", con);
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
                        BOLStaff bolstaff = new BOLStaff();
                        bolstaff.StaffID = Int32.Parse(reader["StaffID"].ToString());
                        bolstaff.StaffName = reader["StaffName"].ToString();
                        bolstaff.DepartmentID = Int32.Parse(reader["DepartmentID"].ToString());
                        bolstaff.DepartmentName = reader["DepartmentName"].ToString();
                        bolstaff.MCBStaffID = reader["MBC_StaffID"].ToString();
                        lststaff.Add(bolstaff);
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
            return lststaff;
        }
        #endregion

        #region "UpdateStaff"
        public int UpdateStaff(BOLStaff bolstaff)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdateStaff", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@staffid", bolstaff.StaffID);
                cmd.Parameters.AddWithValue("@staffname", bolstaff.StaffName);
                cmd.Parameters.AddWithValue("@departmentid", bolstaff.DepartmentID);
                cmd.Parameters.AddWithValue("@mbcstaffid", bolstaff.MCBStaffID);

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

        #region "DeleteStaff"
        public int DeleteStaff(int staffid)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeleteStaff", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@staffid", staffid);

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

        #region "GetStaffByDepartmentID"
        public List<BOLStaff> GetStaffByDepartmentID(int departmentid)
        {
            List<BOLStaff> lststaff = new List<BOLStaff>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetStaffByDepartmentID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentID", departmentid);
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
                        BOLStaff bolstaff = new BOLStaff();
                        bolstaff.StaffID = Int32.Parse(reader["StaffID"].ToString());
                        bolstaff.StaffName = reader["StaffName"].ToString();
                        bolstaff.DepartmentID = Int32.Parse(reader["DepartmentID"].ToString());
                        bolstaff.MCBStaffID = reader["MBC_StaffID"].ToString();
                        lststaff.Add(bolstaff);
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
            return lststaff;
        }
        #endregion

        #region "DuplicateStaffID"
        public List<BOLStaff> DuplicateStaff(int staffid)
        {
            List<BOLStaff> txtStaffID = new List<BOLStaff>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_CheckDuplicateStaff", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffID", staffid);

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
                        BOLStaff bolstaff = new BOLStaff();
                        bolstaff.StaffID = Int32.Parse(reader["StaffID"].ToString());
                        txtStaffID.Add(bolstaff);
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
            return txtStaffID;
        }
        #endregion

    }
}
