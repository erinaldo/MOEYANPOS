using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using MoeYanPOS.BOL;
using MoeYanPOS.Function;

namespace MoeYanPOS.DAL
{
    class DALDepartment
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string constr = MoeYanConfiguration.GetConnection();
        #endregion

        #region "GetDepartmentID"
        public int GetDepartmentID()
        {
            int departmentid = 0;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetDepartmentID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if(con.State==ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                departmentid=(int)cmd.ExecuteScalar();
                if(departmentid==-1 | departmentid==null)
                {
                    departmentid=1;
                }
                else
                {
                    departmentid+=1;
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
            return departmentid;
        }
        #endregion

        #region "SaveDepartment"
            public int SaveDepartment(BOLDepartment boldepartment)
            {
                int issaved = 0;
                try
                {
                    con = new SqlConnection(constr);
                    cmd = new SqlCommand("SP_InsertDepartment", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    con.Open();
                    cmd.Parameters.AddWithValue("@id", boldepartment.Id);
                    cmd.Parameters.AddWithValue("@DepartmentName", boldepartment.Departmentname);
                    cmd.Parameters.AddWithValue("@MBCDepartmentID", boldepartment.MBCDepartmentID);
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

        #region "ShowAllDepartment"
            public List<BOLDepartment> ShowAllDepartment()
            {
                List<BOLDepartment> lstdepartment = new List<BOLDepartment>();
                try
                {
                    con = new SqlConnection(constr);
                    cmd = new SqlCommand("SP_ShowAllDepartment", con);
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
                            boldepartment.MBCDepartmentID = reader["MBC_DepartmentID"].ToString();
                            lstdepartment.Add(boldepartment);
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
                return lstdepartment;
            }
        #endregion

        #region "DeleteDepartment"
            public int DeleteDepartment(int departmentid)
            {
                int isdelete = 0;
                try
                {
                    con = new SqlConnection(constr);
                    cmd = new SqlCommand("SP_DeleteDepartment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", departmentid);

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

        #region "UpdateDepartment"
            public int UpdateDepartment(BOLDepartment boldepartment)
            {
                int isupdated = 0;
                try
                {
                    con = new SqlConnection(constr);
                    cmd = new SqlCommand("SP_UpdateDepartment", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    con.Open();
                    cmd.Parameters.AddWithValue("@ID", boldepartment.Id);
                    cmd.Parameters.AddWithValue("@DepartmentName", boldepartment.Departmentname);
                    cmd.Parameters.AddWithValue("@mbcdepartmentid", boldepartment.MBCDepartmentID);

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

        #region "FillDepartment"
            public List<BOLDepartment> FillDepartment()
            {
                List<BOLDepartment> lstdepartment = new List<BOLDepartment>();
                try
                {
                    con = new SqlConnection(constr);
                    cmd = new SqlCommand("SP_ShowAllDepartment", con);
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
                            boldepartment.MBCDepartmentID = reader["MBC_DepartmentID"].ToString();
                            lstdepartment.Add(boldepartment);
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
                return lstdepartment;
            }
        #endregion
    }
}
