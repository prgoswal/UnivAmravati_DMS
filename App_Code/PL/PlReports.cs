﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

public class PlReports
{

    public int Ind { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int EntryByRegNo { get; set; }

    /// <summary>
    /// For Data Validity.
    /// </summary>
    public string ExamYear { get; set; }
    public string Session { get; set; }
    public int RptInd { get; set; }
    public string Ctrls { get; set; }
    public bool AllCtrls { get; set; }
}              