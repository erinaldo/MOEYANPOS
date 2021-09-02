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
    class DALCategory
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr   = MoeYanConfiguration.GetConnection();
        #endregion

        #region "GetCategoryID"
        public int GetCategoryID()
        {
            int categoryid = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_GetCategoryID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                categoryid = Convert.ToInt32( cmd.ExecuteScalar());
                if (categoryid == -1 | categoryid == null)
                {
                    categoryid = 1;
                }
                else
                {
                    categoryid += 1;
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
            return categoryid;
        }
        #endregion

        #region "SaveUser"
        public int SaveUser(BOLCategory bolcategory)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_InsertCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                cmd.Parameters.AddWithValue("@classid", bolcategory.ClassID);
                cmd.Parameters.AddWithValue("@Category", bolcategory.CategoryName);
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

        #region "FillClass"
        public List<BOLClass> FillClass()
        {
            List<BOLClass> lstclass = new List<BOLClass>();
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_SelectClass", con);
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

        #region "SaveCategory"
        public int SaveCategory(BOLCategory bolcategory)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_InsertCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                cmd.Parameters.AddWithValue("@classid", bolcategory.ClassID);
                cmd.Parameters.AddWithValue("@Category", bolcategory.CategoryName);
                cmd.Parameters.AddWithValue("@ReportGroupID", bolcategory.ReportGroupID);
                cmd.Parameters.AddWithValue("@MBC_CategoryID", bolcategory.MBC_CategoryID);
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

        #region "ShowAllCategory"
        public List<BOLCategory> ShowAllCategory()
        {
            List<BOLCategory> lstcategory = new List<BOLCategory>();
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_ShowAllCategory", con);
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
                        BOLCategory bolcategory = new BOLCategory();
                        bolcategory.Id = Int32.Parse(reader["ID"].ToString());
                        bolcategory.Classname = reader["ClassName"].ToString();
                        bolcategory.CategoryName = reader["Category"].ToString();
                        bolcategory.ReportGroupID = reader["ReportGroupID"].ToString();
                        lstcategory.Add(bolcategory);
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
            return lstcategory;
        }
        #endregion

        #region "UpdateCategory"
        public int UpdateCategory(BOLCategory bolcategory)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_UpdateCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@ID", bolcategory.Id);
                cmd.Parameters.AddWithValue("@ClassID", bolcategory.ClassID);
                cmd.Parameters.AddWithValue("@Category", bolcategory.CategoryName);
                cmd.Parameters.AddWithValue("@ReportGroupID", bolcategory.ReportGroupID);
                cmd.Parameters.AddWithValue("@MBC_CategoryID", bolcategory.MBC_CategoryID);


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

        #region "DeleteCategory"
        public int DeleteCategory(int categoryid)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_DeleteCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", categoryid);

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

        #region "GetCategoryByClassID"
        public List<BOLCategory> GetCategoryByClassID(int Classid)
        {
            List<BOLCategory> lstcategory = new List<BOLCategory>();
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_GetCategoryByClassID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassID", Classid);
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
                        BOLCategory bolcategory = new BOLCategory();
                        bolcategory.Id = Int32.Parse(reader["ID"].ToString());
                        bolcategory.ClassID = Int32.Parse(reader["ClassID"].ToString());
                        bolcategory.CategoryName = reader["Category"].ToString();
                        lstcategory.Add(bolcategory);
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
            return lstcategory;
        }
        #endregion
    }
}
