using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SingleViewCRUD.Models
{
    public class BalUser
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-2BC1JHR\\SQLEXPRESS;Initial Catalog=ProjectManagement;Integrated Security=True;");

        public void SaveData(Users objUser)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("MVCTask", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "SaveData");
            cmd.Parameters.AddWithValue("@Name", objUser.Name);
            cmd.Parameters.AddWithValue("@Gender", objUser.Gender);
            cmd.Parameters.AddWithValue("@Phone", objUser.Phone);
            cmd.Parameters.AddWithValue("@Email", objUser.Email);
            cmd.Parameters.AddWithValue("@Password", objUser.Password);
            cmd.Parameters.AddWithValue("@CityID", objUser.CityID);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public DataTable Country()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("MVCTask", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Country");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable State(Users objUser)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("MVCTask", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "State");
            cmd.Parameters.AddWithValue("@CountryId", objUser.CountryId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable City(Users objUser)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("MVCTask", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "City");
            cmd.Parameters.AddWithValue("@StateId", objUser.StateId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public List<Users> ViewData()
        {
            con.Open();
            List<Users> Lst = new List<Users>();
            SqlCommand cmd = new SqlCommand("MVCTask", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "ViewData");
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Users obj = new Users();
                obj.ID = Convert.ToInt32(dr["ID"].ToString());
                obj.Name = dr["Name"].ToString();
                obj.Gender = dr["Gender"].ToString();
                obj.Phone = dr["Phone"].ToString();
                obj.Email = dr["Email"].ToString();
                obj.Password = dr["Password"].ToString();
                obj.Country = dr["CountryName"].ToString();
                obj.State = dr["StateName"].ToString();
                obj.City = dr["CityName"].ToString();
                Lst.Add(obj);
            }
            return Lst;

        }
 
        public void Edit(Users objUser)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("MVCTask", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Edit");
            cmd.Parameters.AddWithValue("@ID", objUser.ID);
            cmd.Parameters.AddWithValue("@Name", objUser.Name);
            cmd.Parameters.AddWithValue("@Gender", objUser.Gender);
            cmd.Parameters.AddWithValue("@Phone", objUser.Phone);
            cmd.Parameters.AddWithValue("@Email", objUser.Email);
            cmd.Parameters.AddWithValue("@Password", objUser.Password);
            cmd.Parameters.AddWithValue("@CityID", objUser.CityID);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public bool DeleteUser(int id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("MVCTask", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "DeleteUser");
            cmd.Parameters.AddWithValue("@ID", id);
            int row = cmd.ExecuteNonQuery();
            if (row > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}