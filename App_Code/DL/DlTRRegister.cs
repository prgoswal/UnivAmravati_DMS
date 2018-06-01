using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public class DlTRRegister
{
    public DlTRRegister()
    {

    }

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    SqlCommand cmd;
    SqlDataAdapter da;
    DataTable dt;

    public DataTable GetAllRegister(plTrRegister pl, int Ind)
    {
        cmd = new SqlCommand("SPGetAllMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", Ind);
        if (Ind == 6)
            cmd.Parameters.AddWithValue("@ItmId", pl.ItemID);

        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public DataTable GetExamName(int Ind, int facultytype)
    {
        cmd = new SqlCommand("SPRegister", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", Ind);
        cmd.Parameters.AddWithValue("@Facultytype", facultytype);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public DataTable GetLoginDetails(plTrRegister pl)
    {
        cmd = new SqlCommand("SPLoginDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@LoginId", pl.UserLoginId);
        cmd.Parameters.AddWithValue("@LoginPwd", pl.LoginPwd);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    public int ChackLoginId(plTrRegister pl)
    {
        cmd = new SqlCommand("SPLoginDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@LoginId", pl.UserLoginId);
        cmd.Parameters.AddWithValue("@LoginPwd", pl.LoginPwd);
        con.Open();
        int i = Convert.ToInt32(cmd.ExecuteScalar());
        con.Close();
        return i;
    }
    public int ChangeLoginIdStatus(plTrRegister pl)
    {
        if (pl.UserId > 0) 
        {
            int i;
            cmd = new SqlCommand("SPLoginDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", pl.Ind);
            cmd.Parameters.AddWithValue("@UserId", pl.UserId);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return  i=Convert.ToInt32(dt.Rows[0][0]);
        
        }
        else
            return 0;
    }

    public int CheckLockInd(plTrRegister pl)
    {
        cmd = new SqlCommand("SPLoginDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@LoginId", pl.UserLoginId);
        con.Open();
        int i = Convert.ToInt32(cmd.ExecuteScalar());
        con.Close();
        return i;
    }
    public DataSet GetUserLoginID(plTrRegister pl)
    {
        DataSet ds = new DataSet();

        cmd = new SqlCommand("SPLoginDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@LoginId", pl.UserLoginId);
        cmd.Parameters.AddWithValue("@MacAddress", pl.MacAddress);
        da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        return ds;

    }
    public DataTable UpdateInvalidAttempts(plTrRegister pl)
    {
        cmd = new SqlCommand("SPLoginDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@LoginId", pl.UserLoginId);
        cmd.Parameters.AddWithValue("@LoginPwd", pl.UserLoginPwd);
        da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    public DataTable ChangePasswordFirst(plTrRegister pl)
    {
        cmd = new SqlCommand("SPLoginDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@NewPwd", pl.LoginPwd);
        cmd.Parameters.AddWithValue("@UserId", pl.UserId);
        da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
}