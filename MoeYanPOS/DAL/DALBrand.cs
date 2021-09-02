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
    class DALBrand
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr   = MoeYanConfiguration.GetConnection();
        #endregion

        #region "GetBrandID"
        public int GetBrandID()
        {
            int brandid = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_GetBrandID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                brandid = (int)cmd.ExecuteScalar();
                if (brandid == -1 | brandid == null)
                {
                    brandid = 1;
                }
                else
                {
                    brandid += 1;
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
            return brandid;
        }
        #endregion

        #region "SaveBrand"
        public int SaveBrand(BOLBrand bolbrand)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_InsertBrand", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@id", bolbrand.Id);
                cmd.Parameters.AddWithValue("@BrandName", bolbrand.Brandname);
                cmd.Parameters.AddWithValue("@action", bolbrand.Action);
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

        #region "ShowAllBrand"
        public List<BOLBrand> ShowAllBrand(int id)
        {
            List<BOLBrand> lstBrand = new List<BOLBrand>();
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_GetAllBrand", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

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
                        BOLBrand bolbrand = new BOLBrand();
                        bolbrand.Id = Int32.Parse(reader["ID"].ToString());
                        bolbrand.Brandname = reader["Brandname"].ToString();                        
                        lstBrand.Add(bolbrand);
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
            return lstBrand;
        }
        #endregion

        #region "DeleteBrand"
        public int DeleteBrand(int id)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr  );
                cmd = new SqlCommand("SP_DeleteBrand", con);
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

        #region "UpdateBrand"
        public int UpdateBrand(BOLBrand bolbrand)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdateBrand", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@ID", bolbrand.Id);
                cmd.Parameters.AddWithValue("@BrandName", bolbrand.Brandname);
                
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
