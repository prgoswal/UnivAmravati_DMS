using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PLExamMapping
/// </summary>
public class PLExamMapping
{
    public int Ind { get; set; }
    public int ExamYear { get; set; }
    public string ExamSession { get; set; }
    public int ExamSessionID { get; set; }
    public int ExamFacultyCD { get; set; }
    public string ExamName { get; set; }

    public DataTable TblExamMapp
    {
        get { return InitTblExamMapp; }
        set { InitTblExamMapp = value; }
    }

    private DataTable InitTblExamMapp = new DataTable();
}