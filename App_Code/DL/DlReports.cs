using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


public class DlReports
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    SqlCommand cmd; SqlDataAdapter da; DataTable dt;

    public DataTable GetReport(PlReports plRpt)
    {
        cmd = new SqlCommand("SPReport", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", plRpt.Ind);
        //cmd.Parameters.AddWithValue("@RptInd", plRpt.RptInd);
        cmd.Parameters.AddWithValue("@DateFrom", plRpt.DateFrom);
        cmd.Parameters.AddWithValue("@DateTo", plRpt.DateTo);
        cmd.CommandTimeout = 0;
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public DataTable GetRptFoilCFValidity(PlReports plRpt)
    {
        cmd = new SqlCommand("SPReport", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", plRpt.Ind);
        //cmd.Parameters.AddWithValue("@RptInd", plRpt.RptInd);
        //cmd.Parameters.AddWithValue("@ExamYear", plRpt.ExamYear);
        //cmd.Parameters.AddWithValue("@Session", plRpt.Session);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public DataTable GetPhysicalPath(PlReports plRpt)
    {
        cmd = new SqlCommand("SPGetAllMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", plRpt.Ind);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public DataTable GetExamYear(PlReports plRpt)
    {
        cmd = new SqlCommand("SPGetAllMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", plRpt.Ind);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public DataTable GetExamSession(PlReports plrep)
    {
        cmd = new SqlCommand("SPGetAllMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", plrep.Ind);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public DataTable MISReport(PlReports plrep)
    {
        cmd = new SqlCommand("SPReport", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", plrep.Ind);
        int session = Convert.ToInt32(plrep.Session);
        int Year = Convert.ToInt32(plrep.ExamYear);
        cmd.Parameters.AddWithValue("@SessionType", session);//Convert.ToInt32(plrep.Session));@SessionType
        cmd.Parameters.AddWithValue("@ExamYear", Year);//Convert.ToInt32(plrep.ExamYear));ExamYear
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public DataTable ShowRegister(PlReports plrep)
    {
        cmd = new SqlCommand("SPReport", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", plrep.Ind);
        int session = Convert.ToInt32(plrep.Session);
        int Year = Convert.ToInt32(plrep.ExamYear);
        cmd.Parameters.AddWithValue("@SessionType", session);//Convert.ToInt32(plrep.Session));@SessionType
        cmd.Parameters.AddWithValue("@ExamYear", Year);//Convert.ToInt32(plrep.ExamYear));ExamYear
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    
    internal DataTable ShowErrorValidity(PlReports plrep)
    {
        cmd = new SqlCommand("SPReport", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 10000;
        cmd.Parameters.AddWithValue("@Ind", plrep.Ind);
        int EntryByRegNo = Convert.ToInt32(plrep.EntryByRegNo);
        cmd.Parameters.AddWithValue("@EntryByRegNo", EntryByRegNo);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;

    }

    internal DataTable ErroInDuplicateEntry(PlReports PlRpt)
    {
        cmd = new SqlCommand("SPDataValidity", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 10000;
        cmd.Parameters.AddWithValue("@RptInd", PlRpt.RptInd);
        cmd.Parameters.AddWithValue("@Ctrls", PlRpt.Ctrls);
        cmd.Parameters.AddWithValue("@AllCtrls", PlRpt.AllCtrls);
        cmd.Parameters.AddWithValue("@ExamYear", PlRpt.ExamYear);
        cmd.Parameters.AddWithValue("@ExamSessionID", Convert.ToInt16(PlRpt.Session));
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    
    internal DataTable ShowRegAllotedStatus(PlReports PlRpt)
    {
        cmd = new SqlCommand("SPReport", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 10000;
        cmd.Parameters.AddWithValue("@Ind", PlRpt.Ind);
        cmd.Parameters.AddWithValue("@RptInd", PlRpt.RptInd);
        cmd.Parameters.AddWithValue("@ExamYear", PlRpt.ExamYear);
        cmd.Parameters.AddWithValue("@SessionType", Convert.ToInt16(PlRpt.Session));
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

}