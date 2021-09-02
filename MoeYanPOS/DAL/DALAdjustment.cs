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
    class DALAdjustment
    {
        #region "Declaration"
        public SqlConnection con;
        public SqlCommand cmd;
        string Constr = MoeYanConfiguration.GetConnection();
        #endregion        

        #region "SaveAdjustment"
        public int SaveAdjustment(BOLAdjustment bolAdjustment)
        {
            int issaved = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_InsertAdjustment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();                
                cmd.Parameters.AddWithValue("@AdjDate", bolAdjustment.AdjDate);
                cmd.Parameters.AddWithValue("@UserID", bolAdjustment.UserID);
                cmd.Parameters.AddWithValue("@AdjustmentTypeID", bolAdjustment.AdjustmentTypeID);
                cmd.Parameters.AddWithValue("@LocationID", bolAdjustment.LID);
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

        #region "ShowAllAdjustment"
        public List<BOLAdjustment> ShowAllAdjustment()
        {
            List<BOLAdjustment> lstAdjustmentType = new List<BOLAdjustment>();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetAllAdjustment", con);
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
                        BOLAdjustment bolAdjustment = new BOLAdjustment();
                        bolAdjustment.ID = long.Parse(reader["ID"].ToString());
                        bolAdjustment.AdjDate = DateTime.Parse( reader["AdjDate"].ToString());
                        bolAdjustment.UserID = Int32.Parse( reader["UserID"].ToString());
                        bolAdjustment.Header = reader["Header"].ToString();                 
                        lstAdjustmentType.Add(bolAdjustment);
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
            return lstAdjustmentType;
        }
        #endregion

        #region "GetAdjustment"
        public BOLAdjustment GetAdjustment(long ID)
        {
            BOLAdjustment bolAdjustment = new BOLAdjustment();
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetAdjustment", con);
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
                        bolAdjustment.ID = long.Parse(reader["ID"].ToString());
                        bolAdjustment.AdjDate = DateTime.Parse(reader["AdjDate"].ToString());
                        bolAdjustment.UserID = Int32.Parse(reader["UserID"].ToString());
                        bolAdjustment.Header = reader["Header"].ToString();
                        bolAdjustment.LID = long.Parse(reader["LocationID"].ToString()); 
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
            return bolAdjustment;
        }
        #endregion

        #region "DeleteAdjustment"
        public int DeleteAdjustment(long id)
        {
            int isdelete = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_DeletAdjustment", con);
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

        #region "UpdateAdjustment"
        public int UpdateAdjustment(BOLAdjustment bolAdjustment)
        {
            int isupdated = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_UpdateAdjustment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.Parameters.AddWithValue("@id", bolAdjustment.ID);
                cmd.Parameters.AddWithValue("@AdjDate", bolAdjustment.AdjDate);
                //cmd.Parameters.AddWithValue("@UserID", bolAdjustment.UserID);
                cmd.Parameters.AddWithValue("@AdjustmentTypeID", bolAdjustment.AdjustmentTypeID);
                cmd.Parameters.AddWithValue("@LocationID", bolAdjustment.LID);
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

        #region "GetAdjustmentID"
        public long GetAdjustmentID()
        {
            long adjudtmentid = 0;
            try
            {
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetAdjustmentID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                adjudtmentid = (long)cmd.ExecuteScalar();
                if (adjudtmentid == -1 | adjudtmentid == null)
                {
                    adjudtmentid = 1;
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
            return adjudtmentid;
        }
        #endregion

        #region "GetAdjustmentHistory"
        public DataSet GetAdjustmentHistory(DateTime startdate, DateTime enddate, string ItemCode, int AdjustmentTypeID, long LocationID)
        {
            DataSet ds = new DataSet();
            try
            {

                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetAdjustmentHistory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
                cmd.Parameters.AddWithValue("@AdjustmentTypeID", AdjustmentTypeID);
                cmd.Parameters.AddWithValue("@LocationID", LocationID);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return ds;
        }
        #endregion

        #region "GetAdjustmentReport"
        public DataSet GetAdjustmentReport(DateTime startdate, DateTime enddate, string ItemCode, int AdjustmentTypeID,long LocationID)
        {
            DataSet ds = new DataSet();
            try
            {

                SqlDataAdapter adapter = new SqlDataAdapter();
                con = new SqlConnection(Constr);
                cmd = new SqlCommand("SP_GetAdjustmentReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDate", startdate);
                cmd.Parameters.AddWithValue("@EndDate", enddate);
                cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
                cmd.Parameters.AddWithValue("@AdjustmentTypeID", AdjustmentTypeID);
                cmd.Parameters.AddWithValue("@LocationID", LocationID);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return ds;
        }
        #endregion
    }
}
