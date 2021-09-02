using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoeYanPOS.BOL;
using System.Data.SqlClient;
using System.Data;
using MoeYanPOS.Function;

namespace MoeYanPOS.DAL
{
    class DALClass
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr   = MoeYanConfiguration.GetConnection();
        #endregion

        #region "SaveClass"
        public int SaveClass(BOLClass bolclass)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_InsertClass", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.Parameters.AddWithValue("@class", bolclass.ClassName);
                cmd.Parameters.AddWithValue("@mbcclassid", bolclass.MBCClassID);
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

        #region "GetClassID"
        public int GetClassID()
        {
            int classid = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_GetClassID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                classid = (int)cmd.ExecuteScalar();
                if (classid == -1 | classid == null)
                {
                    classid = 1;
                }
                else
                {
                    classid += 1;
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
            return classid;
        }
        #endregion

        #region "SelectAllUser"
        public List<BOLClass> SelectAllClass()
        {
            List<BOLClass> lstclass = new List<BOLClass>();
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_ShowAllClass", con);
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
                        BOLClass bolclass = new BOLClass();
                        bolclass.Id = Int32.Parse(reader["ID"].ToString());
                        bolclass.ClassName = reader["ClassName"].ToString();
                        bolclass.MBCClassID = reader["MBC_ClassID"].ToString();
                        lstclass.Add(bolclass);
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
            return lstclass;
        }
        #endregion

        #region "UpdateClassByClassID"
        public int updateClassByClassID(BOLClass bolclass)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_UpdateClass", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.Parameters.AddWithValue("@classid", bolclass.Id);
                cmd.Parameters.AddWithValue("@class", bolclass.ClassName);
                cmd.Parameters.AddWithValue("@MBCClassID", bolclass.MBCClassID);

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

        #region "DeleteClass"
        public int DeleteClass(int classid)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_DeleteClass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", classid);
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

    }
}
