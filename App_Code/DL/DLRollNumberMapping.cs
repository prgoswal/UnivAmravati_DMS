using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DLRollNumberMapping
/// </summary>
public class DLRollNumberMapping
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    SqlCommand cmd;
    SqlDataAdapter da;
    DataTable dtRollNumberMapping;
    DataSet dsRollNumberExamMapping;

    internal DataSet BindAllDDl(PLRollNumberMapping objRollNumberMappPl)
    {
        try
        {
            cmd = new SqlCommand("SPSoftDataMapping", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", objRollNumberMappPl.Ind);
            da = new SqlDataAdapter(cmd);
            dsRollNumberExamMapping = new DataSet();
            da.Fill(dsRollNumberExamMapping);
        }
        catch (Exception)
        {
            dsRollNumberExamMapping = new DataSet();
            dsRollNumberExamMapping.DataSetName = "error";
            return dsRollNumberExamMapping;
        }
        return dsRollNumberExamMapping;
    }

    internal DataSet GetAllUniAndDMSExam(PLRollNumberMapping objRollNumberMappPl)
    {
        try
        {
            cmd = new SqlCommand("SPSoftDataMapping", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", objRollNumberMappPl.Ind);
            cmd.Parameters.AddWithValue("@ExamYear", objRollNumberMappPl.ExamYear);
            cmd.Parameters.AddWithValue("@ExamSession", objRollNumberMappPl.ExamSession);
            cmd.Parameters.AddWithValue("@ExamSessionID", objRollNumberMappPl.ExamSessionID);
            cmd.Parameters.AddWithValue("@ExamFacultyCD", objRollNumberMappPl.ExamFacultyCD);
            da = new SqlDataAdapter(cmd);
            dsRollNumberExamMapping = new DataSet();
            da.Fill(dsRollNumberExamMapping);
            dsRollNumberExamMapping.DataSetName = "success";
        }
        catch (Exception)
        {
            dsRollNumberExamMapping = new DataSet();
            dsRollNumberExamMapping.DataSetName = "error";
            return dsRollNumberExamMapping;
        }
        return dsRollNumberExamMapping;
    }

    internal DataTable MappedRollNumber(PLRollNumberMapping objRollNumberMappPl)
    {
        try
        {
            cmd = new SqlCommand("SPSoftDataMapping", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", objRollNumberMappPl.Ind);
            cmd.Parameters.AddWithValue("@ExamYear", objRollNumberMappPl.ExamYear);
            cmd.Parameters.AddWithValue("@ExamSession", objRollNumberMappPl.ExamSession);
            cmd.Parameters.AddWithValue("@ExamSessionID", objRollNumberMappPl.ExamSessionID);
            cmd.Parameters.AddWithValue("@ExamFacultyCD", objRollNumberMappPl.ExamFacultyCD);
            cmd.Parameters.AddWithValue("@ExamCtrlCD", objRollNumberMappPl.ExamCtrlCD);
            da = new SqlDataAdapter(cmd);
            dtRollNumberMapping = new DataTable();
            da.Fill(dtRollNumberMapping);
            dtRollNumberMapping.TableName = "success";
        }
        catch (Exception)
        {
            dtRollNumberMapping = new DataTable();
            dtRollNumberMapping.TableName = "error";
            return dtRollNumberMapping;
        }
        return dtRollNumberMapping;
    }

    internal DataTable ShowMappedData(PLRollNumberMapping objRollNumberMappPl)
    {
        try
        {
            cmd = new SqlCommand("SPSoftDataMapping", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", objRollNumberMappPl.Ind);
            cmd.Parameters.AddWithValue("@ExamYear", objRollNumberMappPl.ExamYear);
            cmd.Parameters.AddWithValue("@ExamSession", objRollNumberMappPl.ExamSession);
            cmd.Parameters.AddWithValue("@ExamSessionID", objRollNumberMappPl.ExamSessionID);
            cmd.Parameters.AddWithValue("@ExamFacultyCD", objRollNumberMappPl.ExamFacultyCD);
            cmd.Parameters.AddWithValue("@ExamCtrlCD", objRollNumberMappPl.ExamCtrlCD);
            da = new SqlDataAdapter(cmd);
            dtRollNumberMapping = new DataTable();
            da.Fill(dtRollNumberMapping);
            dtRollNumberMapping.TableName = "success";
        }
        catch (Exception)
        {
            dtRollNumberMapping = new DataTable();
            dtRollNumberMapping.TableName = "error";
            return dtRollNumberMapping;
        }
        return dtRollNumberMapping;
    }

    internal DataTable GetMissingPunchedRecord(PLRollNumberMapping objRollNumberMappPl)
    {
        try
        {
            cmd = new SqlCommand("SPSoftDataMapping", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", objRollNumberMappPl.Ind);
            cmd.Parameters.AddWithValue("@ExamYear", objRollNumberMappPl.ExamYear);
            cmd.Parameters.AddWithValue("@ExamSession", objRollNumberMappPl.ExamSession);
            cmd.Parameters.AddWithValue("@ExamSessionID", objRollNumberMappPl.ExamSessionID);
            cmd.Parameters.AddWithValue("@ExamFacultyCD", objRollNumberMappPl.ExamFacultyCD);
            cmd.Parameters.AddWithValue("@ExamCtrlCD", objRollNumberMappPl.ExamCtrlCD);
            da = new SqlDataAdapter(cmd);
            dtRollNumberMapping = new DataTable();
            da.Fill(dtRollNumberMapping);
            dtRollNumberMapping.TableName = "success";
        }
        catch (Exception)
        {
            dtRollNumberMapping = new DataTable();
            dtRollNumberMapping.TableName = "error";
            return dtRollNumberMapping;
        }
        return dtRollNumberMapping;
    }

    internal DataTable GetExtraPunchedRecord(PLRollNumberMapping objRollNumberMappPl)
    {
        try
        {
            cmd = new SqlCommand("SPSoftDataMapping", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", objRollNumberMappPl.Ind);
            cmd.Parameters.AddWithValue("@ExamYear", objRollNumberMappPl.ExamYear);
            cmd.Parameters.AddWithValue("@ExamSession", objRollNumberMappPl.ExamSession);
            cmd.Parameters.AddWithValue("@ExamSessionID", objRollNumberMappPl.ExamSessionID);
            cmd.Parameters.AddWithValue("@ExamFacultyCD", objRollNumberMappPl.ExamFacultyCD);
            cmd.Parameters.AddWithValue("@ExamCtrlCD", objRollNumberMappPl.ExamCtrlCD);
            da = new SqlDataAdapter(cmd);
            dtRollNumberMapping = new DataTable();
            da.Fill(dtRollNumberMapping);
            dtRollNumberMapping.TableName = "success";
        }
        catch (Exception)
        {
            dtRollNumberMapping = new DataTable();
            dtRollNumberMapping.TableName = "error";
            return dtRollNumberMapping;
        }
        return dtRollNumberMapping;
    }

    internal DataSet InsertExtraPuncgedRecords(PLRollNumberMapping objRollNumberMappPl)
    {
        try
        {
            cmd = new SqlCommand("SPSoftDataMapping", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", objRollNumberMappPl.Ind);
            cmd.Parameters.AddWithValue("@ExamYear", objRollNumberMappPl.ExamYear);
            cmd.Parameters.AddWithValue("@ExamSession", objRollNumberMappPl.ExamSession);
            cmd.Parameters.AddWithValue("@ExamSessionID", objRollNumberMappPl.ExamSessionID);
            cmd.Parameters.AddWithValue("@ExamFacultyCD", objRollNumberMappPl.ExamFacultyCD);
            cmd.Parameters.AddWithValue("@ExamCtrlCD", objRollNumberMappPl.ExamCtrlCD);
            da = new SqlDataAdapter(cmd);
            dsRollNumberExamMapping = new DataSet();
            da.Fill(dsRollNumberExamMapping);
            dsRollNumberExamMapping.DataSetName = "success";
        }
        catch (Exception)
        {
            dsRollNumberExamMapping = new DataSet();
            dsRollNumberExamMapping.DataSetName = "error";
            return dsRollNumberExamMapping;
        }
        return dsRollNumberExamMapping;
    }
}