using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DLExamMapping
/// </summary>
public class DLExamMapping
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    SqlCommand cmd;
    SqlDataAdapter da;
    DataTable dtExamMapping;
    DataSet dsExamMapping;

    internal DataSet BindAllDDl(PLExamMapping objExamMappPl)
    {
        try
        {
            cmd = new SqlCommand("SPSoftDataMapping", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", objExamMappPl.Ind);
            da = new SqlDataAdapter(cmd);
            dsExamMapping = new DataSet();
            da.Fill(dsExamMapping);
        }
        catch (Exception)
        {
            dsExamMapping = new DataSet();
            dsExamMapping.DataSetName = "error";
            return dsExamMapping;
        }
        return dsExamMapping;
    }

    internal DataSet GetAllUniAndDMSExam(PLExamMapping objExamMappPl)
    {
        try
        {
            cmd = new SqlCommand("SPSoftDataMapping", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", objExamMappPl.Ind);
            cmd.Parameters.AddWithValue("@ExamYear", objExamMappPl.ExamYear);
            cmd.Parameters.AddWithValue("@ExamSession", objExamMappPl.ExamSession);
            cmd.Parameters.AddWithValue("@ExamSessionID", objExamMappPl.ExamSessionID);
            cmd.Parameters.AddWithValue("@ExamFacultyCD", objExamMappPl.ExamFacultyCD);
            da = new SqlDataAdapter(cmd);
            dsExamMapping = new DataSet();
            da.Fill(dsExamMapping);
            dsExamMapping.DataSetName = "success";
        }
        catch (Exception)
        {
            dsExamMapping = new DataSet();
            dsExamMapping.DataSetName = "error";
            return dsExamMapping;
        }
        return dsExamMapping;
    }

    internal DataTable InsertUniAndDMSExamMappingData(PLExamMapping objExamMappPl)
    {
        try
        {
            cmd = new SqlCommand("SPSoftDataMapping", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", objExamMappPl.Ind);
            cmd.Parameters.AddWithValue("@ExamYear", objExamMappPl.ExamYear);
            cmd.Parameters.AddWithValue("@ExamSession", objExamMappPl.ExamSession);
            cmd.Parameters.AddWithValue("@ExamSessionID", objExamMappPl.ExamSessionID);
            cmd.Parameters.AddWithValue("@ExamFacultyCD", objExamMappPl.ExamFacultyCD); 
            cmd.Parameters.AddWithValue("@TblExamMapp", objExamMappPl.TblExamMapp);
            da = new SqlDataAdapter(cmd);
            dtExamMapping = new DataTable();
            da.Fill(dtExamMapping);
            dtExamMapping.TableName = "success";
        }
        catch (Exception)
        {
            dtExamMapping = new DataTable();
            dtExamMapping.TableName = "error";
            return dtExamMapping;
        }
        return dtExamMapping;
    }

    internal DataTable GetUneversityRecords(PLExamMapping objExamMappPl)
    {
        try
        {
            cmd = new SqlCommand("SPSoftDataMapping", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", objExamMappPl.Ind);
            cmd.Parameters.AddWithValue("@ExamYear", objExamMappPl.ExamYear);
            cmd.Parameters.AddWithValue("@ExamSession", objExamMappPl.ExamSession);
            cmd.Parameters.AddWithValue("@ExamSessionID", objExamMappPl.ExamSessionID);
            cmd.Parameters.AddWithValue("@ExamFacultyCD", objExamMappPl.ExamFacultyCD);
            cmd.Parameters.AddWithValue("@ExamName", objExamMappPl.ExamName);
            da = new SqlDataAdapter(cmd);
            dtExamMapping = new DataTable();
            da.Fill(dtExamMapping);
            dtExamMapping.TableName = "success";
        }
        catch (Exception)
        {
            dtExamMapping = new DataTable();
            dtExamMapping.TableName = "error";
            return dtExamMapping;
        }
        return dtExamMapping;
    }
}