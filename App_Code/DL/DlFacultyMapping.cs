using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DlFacultyMapping
/// </summary>
public class DlFacultyMapping
{
    public DlFacultyMapping()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    SqlCommand cmd;
    SqlDataAdapter da;
    DataTable dtFacultyMapping;
    DataSet dsFacultyMapping;

    internal DataSet BindAllDDl(PlFacultyMapping objFacultyMappPl)
    {
        try
        {
            cmd = new SqlCommand("SPSoftDataMapping", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", objFacultyMappPl.Ind);
            da = new SqlDataAdapter(cmd);
            dsFacultyMapping = new DataSet();
            da.Fill(dsFacultyMapping);
        }
        catch (Exception)
        {
            dsFacultyMapping = new DataSet();
            dsFacultyMapping.DataSetName = "error";
            return dsFacultyMapping;
        }
        return dsFacultyMapping;
    }

    internal DataSet GetAllUniAndDMSExamFaculty(PlFacultyMapping objFacultyMappPl)
    {
        try
        {
            cmd = new SqlCommand("SPSoftDataMapping", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", objFacultyMappPl.Ind);
            cmd.Parameters.AddWithValue("@ExamYear", objFacultyMappPl.ExamYear);
            cmd.Parameters.AddWithValue("@ExamSession", objFacultyMappPl.ExamSession);
            cmd.Parameters.AddWithValue("@ExamSessionID", objFacultyMappPl.ExamSessionID);
            da = new SqlDataAdapter(cmd);
            dsFacultyMapping = new DataSet();
            da.Fill(dsFacultyMapping);
            dsFacultyMapping.DataSetName = "success";
        }
        catch (Exception)
        {
            dsFacultyMapping = new DataSet();
            dsFacultyMapping.DataSetName = "error";
            return dsFacultyMapping;
        }
        return dsFacultyMapping;
    }

    internal DataTable InsertUniAndDMSExamFacultyData(PlFacultyMapping objFacultyMappPl)
    {
        try
        {
            cmd = new SqlCommand("SPSoftDataMapping", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", objFacultyMappPl.Ind);
            cmd.Parameters.AddWithValue("@ExamYear", objFacultyMappPl.ExamYear);
            cmd.Parameters.AddWithValue("@ExamSession", objFacultyMappPl.ExamSession);
            cmd.Parameters.AddWithValue("@ExamSessionID", objFacultyMappPl.ExamSessionID);
            cmd.Parameters.AddWithValue("@TblFacultyMapp", objFacultyMappPl.TblFacultyMapp);
            da = new SqlDataAdapter(cmd);
            dtFacultyMapping = new DataTable();
            da.Fill(dtFacultyMapping);
            dtFacultyMapping.TableName = "success";
        }
        catch (Exception)
        {
            dtFacultyMapping = new DataTable();
            dtFacultyMapping.TableName = "error";
            return dtFacultyMapping;
        }
        return dtFacultyMapping;
    }
}