using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PlFacultyMapping
/// </summary>
public class PlFacultyMapping
{
    public int Ind { get; set; }
    public int ExamYear { get; set; }
    public string ExamSession { get; set; }
    public int ExamSessionID { get; set; }

    public DataTable TblFacultyMapp
    {
        get { return InitTblFacultyMapp; }
        set { InitTblFacultyMapp = value; }
    }

    private DataTable InitTblFacultyMapp = new DataTable();
}