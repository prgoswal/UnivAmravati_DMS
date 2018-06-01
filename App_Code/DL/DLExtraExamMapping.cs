using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DLExtraExamMapping
/// </summary>
public class DLExtraExamMapping
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    SqlCommand cmd;
    SqlDataAdapter da;
    DataTable dtRollNumberMapping;
    DataSet dsRollNumberExamMapping;

    internal DataSet BindAllDDl(PLExtraExamMapping objExtraExamMappPl)
    {
        try
        {
            cmd = new SqlCommand("SPSoftDataMapping", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", objExtraExamMappPl.Ind);
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

    internal DataSet GetExtraExamMapping(PLExtraExamMapping objExtraExamMappPl)
    {
        try
        {
            cmd = new SqlCommand("SPSoftDataMapping", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", objExtraExamMappPl.Ind);
            cmd.Parameters.AddWithValue("@ExamYear", objExtraExamMappPl.ExamYear);
            cmd.Parameters.AddWithValue("@ExamSession", objExtraExamMappPl.ExamSession);
            cmd.Parameters.AddWithValue("@ExamSessionID", objExtraExamMappPl.ExamSessionID);
            cmd.Parameters.AddWithValue("@ExamFacultyCD", objExtraExamMappPl.ExamFacultyCD);
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

    internal DataSet InsertExtraExamRecords(PLExtraExamMapping objExtraExamMappPl)
    {
        try
        {
            cmd = new SqlCommand("SPSoftDataMapping", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", objExtraExamMappPl.Ind);
            cmd.Parameters.AddWithValue("@ExamYear", objExtraExamMappPl.ExamYear);
            cmd.Parameters.AddWithValue("@ExamSession", objExtraExamMappPl.ExamSession);
            cmd.Parameters.AddWithValue("@ExamSessionID", objExtraExamMappPl.ExamSessionID);
            cmd.Parameters.AddWithValue("@ExamFacultyCD", objExtraExamMappPl.ExamFacultyCD);
            cmd.Parameters.AddWithValue("@ExamCtrlCD", objExtraExamMappPl.ExamCtrlCD);
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

    //internal DataTable MappedRollNumber(PLExtraExamMapping objExtraExamMappPl)
    //{
    //    try
    //    {
    //        cmd = new SqlCommand("SPSoftDataMapping", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@Ind", objExtraExamMappPl.Ind);
    //        cmd.Parameters.AddWithValue("@ExamYear", objExtraExamMappPl.ExamYear);
    //        cmd.Parameters.AddWithValue("@ExamSession", objExtraExamMappPl.ExamSession);
    //        cmd.Parameters.AddWithValue("@ExamSessionID", objExtraExamMappPl.ExamSessionID);
    //        cmd.Parameters.AddWithValue("@ExamFacultyCD", objExtraExamMappPl.ExamFacultyCD);
    //        cmd.Parameters.AddWithValue("@ExamCtrlCD", objExtraExamMappPl.ExamCtrlCD);
    //        da = new SqlDataAdapter(cmd);
    //        dtRollNumberMapping = new DataTable();
    //        da.Fill(dtRollNumberMapping);
    //        dtRollNumberMapping.TableName = "success";
    //    }
    //    catch (Exception)
    //    {
    //        dtRollNumberMapping = new DataTable();
    //        dtRollNumberMapping.TableName = "error";
    //        return dtRollNumberMapping;
    //    }
    //    return dtRollNumberMapping;
    //}

    //internal DataTable ShowMappedData(PLExtraExamMapping objExtraExamMappPl)
    //{
    //    try
    //    {
    //        cmd = new SqlCommand("SPSoftDataMapping", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@Ind", objExtraExamMappPl.Ind);
    //        cmd.Parameters.AddWithValue("@ExamYear", objExtraExamMappPl.ExamYear);
    //        cmd.Parameters.AddWithValue("@ExamSession", objExtraExamMappPl.ExamSession);
    //        cmd.Parameters.AddWithValue("@ExamSessionID", objExtraExamMappPl.ExamSessionID);
    //        cmd.Parameters.AddWithValue("@ExamFacultyCD", objExtraExamMappPl.ExamFacultyCD);
    //        cmd.Parameters.AddWithValue("@ExamCtrlCD", objExtraExamMappPl.ExamCtrlCD);
    //        da = new SqlDataAdapter(cmd);
    //        dtRollNumberMapping = new DataTable();
    //        da.Fill(dtRollNumberMapping);
    //        dtRollNumberMapping.TableName = "success";
    //    }
    //    catch (Exception)
    //    {
    //        dtRollNumberMapping = new DataTable();
    //        dtRollNumberMapping.TableName = "error";
    //        return dtRollNumberMapping;
    //    }
    //    return dtRollNumberMapping;
    //}

    //internal DataTable GetMissingPunchedRecord(PLExtraExamMapping objExtraExamMappPl)
    //{
    //    try
    //    {
    //        cmd = new SqlCommand("SPSoftDataMapping", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@Ind", objExtraExamMappPl.Ind);
    //        cmd.Parameters.AddWithValue("@ExamYear", objExtraExamMappPl.ExamYear);
    //        cmd.Parameters.AddWithValue("@ExamSession", objExtraExamMappPl.ExamSession);
    //        cmd.Parameters.AddWithValue("@ExamSessionID", objExtraExamMappPl.ExamSessionID);
    //        cmd.Parameters.AddWithValue("@ExamFacultyCD", objExtraExamMappPl.ExamFacultyCD);
    //        cmd.Parameters.AddWithValue("@ExamCtrlCD", objExtraExamMappPl.ExamCtrlCD);
    //        da = new SqlDataAdapter(cmd);
    //        dtRollNumberMapping = new DataTable();
    //        da.Fill(dtRollNumberMapping);
    //        dtRollNumberMapping.TableName = "success";
    //    }
    //    catch (Exception)
    //    {
    //        dtRollNumberMapping = new DataTable();
    //        dtRollNumberMapping.TableName = "error";
    //        return dtRollNumberMapping;
    //    }
    //    return dtRollNumberMapping;
    //}

    //internal DataTable GetExtraPunchedRecord(PLExtraExamMapping objExtraExamMappPl)
    //{
    //    try
    //    {
    //        cmd = new SqlCommand("SPSoftDataMapping", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@Ind", objExtraExamMappPl.Ind);
    //        cmd.Parameters.AddWithValue("@ExamYear", objExtraExamMappPl.ExamYear);
    //        cmd.Parameters.AddWithValue("@ExamSession", objExtraExamMappPl.ExamSession);
    //        cmd.Parameters.AddWithValue("@ExamSessionID", objExtraExamMappPl.ExamSessionID);
    //        cmd.Parameters.AddWithValue("@ExamFacultyCD", objExtraExamMappPl.ExamFacultyCD);
    //        cmd.Parameters.AddWithValue("@ExamCtrlCD", objExtraExamMappPl.ExamCtrlCD);
    //        da = new SqlDataAdapter(cmd);
    //        dtRollNumberMapping = new DataTable();
    //        da.Fill(dtRollNumberMapping);
    //        dtRollNumberMapping.TableName = "success";
    //    }
    //    catch (Exception)
    //    {
    //        dtRollNumberMapping = new DataTable();
    //        dtRollNumberMapping.TableName = "error";
    //        return dtRollNumberMapping;
    //    }
    //    return dtRollNumberMapping;
    //}
}