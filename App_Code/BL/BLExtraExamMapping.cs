using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLExtraExamMapping
/// </summary>
public class BLExtraExamMapping
{
    DLExtraExamMapping objExtraExamMappDl = new DLExtraExamMapping();

    public DataSet BindAllDDl(PLExtraExamMapping objExtraExamMappPl)
    {
        return objExtraExamMappDl.BindAllDDl(objExtraExamMappPl);
    }

    public DataSet GetExtraExamMapping(PLExtraExamMapping objExtraExamMappPl)
    {
        return objExtraExamMappDl.GetExtraExamMapping(objExtraExamMappPl);
    }

    public DataSet InsertExtraExamRecords(PLExtraExamMapping objExtraExamMappPl)
    {
        return objExtraExamMappDl.InsertExtraExamRecords(objExtraExamMappPl);
    }

    //public DataTable MappedRollNumber(PLExtraExamMapping objExtraExamMappPl)
    //{
    //    return objExtraExamMappDl.MappedRollNumber(objExtraExamMappPl);
    //}

    //public DataTable ShowMappedData(PLExtraExamMapping objExtraExamMappPl)
    //{
    //    return objExtraExamMappDl.ShowMappedData(objExtraExamMappPl);
    //}

    //public DataTable GetMissingPunchedRecord(PLExtraExamMapping objExtraExamMappPl)
    //{
    //    return objExtraExamMappDl.GetMissingPunchedRecord(objExtraExamMappPl);
    //}

    //public DataTable GetExtraPunchedRecord(PLExtraExamMapping objExtraExamMappPl)
    //{
    //    return objExtraExamMappDl.GetExtraPunchedRecord(objExtraExamMappPl);
    //}
}