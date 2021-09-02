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
    class DALDivision
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string constr = MoeYanConfiguration.GetConnection();
        #endregion

        #region GetDivisionID"
        public int GetDivisionID()
        {
            int divisionid = 0;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_GetDivisionID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                divisionid = (int)cmd.ExecuteScalar();
                if (divisionid == -1 | divisionid == null)
                {
                    divisionid = 1;
                }
                else
                {
                    divisionid += 1;
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
            return divisionid;
        }
        #endregion

        #region "SaveDivision"
        public int SaveDivision(BOLDivision boldivision)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_InsertDivision", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.Parameters.AddWithValue("@id", boldivision.Id);
                cmd.Parameters.AddWithValue("@division", boldivision.Division);
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

        #region "SelectAllDivision"
        public List<BOLDivision> SelectAllDivision()
        {
            List<BOLDivision> lstdivision = new List<BOLDivision>();
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_ShowAllDivision", con);
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
                        BOLDivision boldivision = new BOLDivision();
                        boldivision.Id=Int32.Parse(reader["DivisionID"].ToString());
                        boldivision.Division = reader["Division"].ToString();
                        lstdivision.Add(boldivision);
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
            return lstdivision;
        }
        #endregion

        #region "DeleteClass"
        public int DeleteDivision(int divisionid)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_DeleteDivision", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", divisionid);

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

        #region "UpdateDivision"
        public int UpdateDivision(BOLDivision boldivision)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand("SP_UpdateDivision", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@id", boldivision.Id);
                cmd.Parameters.AddWithValue("@division", boldivision.Division);

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
    }
}
