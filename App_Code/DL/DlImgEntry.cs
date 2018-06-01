using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


public class DlImgEntry
{
    public DlImgEntry()
    {
    }
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    SqlCommand cmd; SqlDataAdapter da; DataTable dt; DataSet ds;
    int i;
    public int InserImgEntry(PlImgEntry pl)
    {
            cmd = new SqlCommand("SPImgEntry", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", pl.Ind);
            cmd.Parameters.AddWithValue("@RegNo", pl.RegNo);
            cmd.Parameters.AddWithValue("@RollNo", pl.RollNo);
            cmd.Parameters.AddWithValue("@SName", pl.SName);
            cmd.Parameters.AddWithValue("@FilePath", pl.FilePath);
            cmd.Parameters.AddWithValue("@CompletedPages", pl.CompletedPages);
            cmd.Parameters.AddWithValue("@TotalPages", pl.TotalPages);
            cmd.Parameters.AddWithValue("@ErrorMark", pl.ErrorMark);
            cmd.Parameters.AddWithValue("@IpAddress", pl.IpAddress);
            cmd.Parameters.AddWithValue("@UserID", pl.UserId);
            cmd.Parameters.AddWithValue("@CreationDate", pl.CreationDate);
            cmd.Parameters.AddWithValue("@AllotmentNo", pl.AllotmentNo);
            cmd.Parameters.AddWithValue("@EntryByRegNo", pl.EntryByRegNo);
            cmd.Parameters.AddWithValue("@IsUpdated", pl.IsUpdated);
            cmd.Parameters.AddWithValue("@UpdatedBy", pl.UpdatedBy);
           // cmd.Parameters.AddWithValue("@UpdationDate", pl.UpdationDate);
            cmd.Parameters.AddWithValue("@PhyPageno",pl.PhyPageno);
            cmd.Parameters.AddWithValue("@IsDataVerified", pl.verifydata);
            con.Open();
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
          
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //if (dt.Rows.Count > 0 && dt != null)
            //{ 
            //i=Convert.ToInt16(dt.Rows[0]["value"]);
            //}

            int i = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return i;
    }
    public Int64 LastRollNo(PlImgEntry pl)
    {
       
        cmd = new SqlCommand("SPImgEntry",con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind",pl.Ind);
        cmd.Parameters.AddWithValue("@RegNo", pl.RegNo);
        con.Open();
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0 && dt != null)
        {
            pl.roll
                = Convert.ToInt64(dt.Rows[0][0]);
        }
        else
            pl.roll = 0;
        con.Close();
        return pl.roll;
    }
    public int InsertCFImgEntry(PlImgEntry pl)
    {
        cmd = new SqlCommand("SPImgEntry", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@RegNo", pl.RegNo);
        cmd.Parameters.AddWithValue("@RollNo", pl.RollNoFrom);
        cmd.Parameters.AddWithValue("@RollNoTo", pl.RollNoTo);
        cmd.Parameters.AddWithValue("@FilePath", pl.FilePath);
        cmd.Parameters.AddWithValue("@CompletedPages", pl.CompletedPages);
        cmd.Parameters.AddWithValue("@TotalPages", pl.TotalPages);
        cmd.Parameters.AddWithValue("@ErrorMark", pl.ErrorMark);
        cmd.Parameters.AddWithValue("@IpAddress", pl.IpAddress);
        cmd.Parameters.AddWithValue("@UserID", pl.UserId);
        cmd.Parameters.AddWithValue("@CreationDate", pl.CreationDate);
        cmd.Parameters.AddWithValue("@AllotmentNo", pl.AllotmentNo);
        cmd.Parameters.AddWithValue("@EntryByRegNo", pl.EntryByRegNo);
        cmd.Parameters.AddWithValue("@IsUpdated", pl.IsUpdated);
        cmd.Parameters.AddWithValue("@UpdatedBy", pl.UpdatedBy);
        cmd.Parameters.AddWithValue("@UpdationDate", pl.UpdationDate);
        cmd.Parameters.AddWithValue("@PhyPageno", pl.PhyPageno);
        cmd.Parameters.AddWithValue("@IsDataVerified", pl.verifydata);
        con.Open();
        int i = Convert.ToInt32(cmd.ExecuteScalar());
        con.Close();
        return i;

   
    }
    public DataTable  GetICompetedPages(PlImgEntry pl)
    {
        cmd = new SqlCommand("SPImgEntry", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@RegNo", pl.RegNo);
        SqlDataAdapter ad = new SqlDataAdapter(cmd);            
        DataTable dt=new DataTable();
        ad.Fill(dt);
        return dt;
        //con.Open();
        //int i = Convert.ToInt32(cmd.ExecuteScalar());
        //con.Close();
       // return i;
    }
    public DataSet LastPageNoWithLotNo(PlImgEntry pl)
    {
        cmd = new SqlCommand("SPRegister", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@RegisterNo", pl.RegNo);
        cmd.Parameters.AddWithValue("@UserId", pl.UserId);
        ds = new DataSet();
        da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        return ds;
    }
    public int DeletePageNoData(PlImgEntry pl)
    {
        cmd = new SqlCommand("SPImgEntry", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@RegNo", pl.RegNo);
        cmd.Parameters.AddWithValue("@CompletedPages", pl.CurrentPageNo);
        con.Open();
        int i = Convert.ToInt32(cmd.ExecuteScalar());
        con.Close();
        return i;
    }
    //////////////Changes In DL File
    public int InsertFoilImgEntryPageWise(PlImgEntry pl)
    {
        cmd = new SqlCommand("SPImgEntry", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);
        cmd.Parameters.AddWithValue("@RegNo", pl.RegNo);
        cmd.Parameters.AddWithValue("@PhyPageno", pl.PhyPageno);
        cmd.Parameters.AddWithValue("@RollNo", pl.RollNoFrom);
        cmd.Parameters.AddWithValue("@RollNoTo", pl.RollNoTo);
        cmd.Parameters.AddWithValue("@FilePath", pl.FilePath);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0 && dt != null)
            i = Convert.ToInt16(dt.Rows[0][0]);
            //i = Convert.ToInt16(dt.Rows[0]["value"]);

        con.Close();
        return i;
    }
    //////////////
    public DataTable SearchStudentDetail(PlImgEntry pl)
    {
        cmd = new SqlCommand("SPApplicationDetail", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", pl.Ind);

        cmd.Parameters.AddWithValue("@ExamSession", pl.ExamSession);
        cmd.Parameters.AddWithValue("@ExamYear", pl.Examyear);      
        cmd.Parameters.AddWithValue("@Rollno", pl.RollNo);

        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToInt32(dt.Rows[0]["RollNo"]) == 0)
            {
                dt = null;
            }

        }
        else
            dt = null;
        return dt;
    }

}