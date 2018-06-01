using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLRollNumberMapping
/// </summary>
public class BLRollNumberMapping
{
    DLRollNumberMapping objRollNumberMappDl = new DLRollNumberMapping();

    public DataSet BindAllDDl(PLRollNumberMapping objRollNumberMappPl)
    {
        return objRollNumberMappDl.BindAllDDl(objRollNumberMappPl);
    }

    public DataSet GetAllUniAndDMSExam(PLRollNumberMapping objRollNumberMappPl)
    {
        return objRollNumberMappDl.GetAllUniAndDMSExam(objRollNumberMappPl);
    }

    public DataTable MappedRollNumber(PLRollNumberMapping objRollNumberMappPl)
    {
        return objRollNumberMappDl.MappedRollNumber(objRollNumberMappPl);
    }

    public DataTable ShowMappedData(PLRollNumberMapping objRollNumberMappPl)
    {
        return objRollNumberMappDl.ShowMappedData(objRollNumberMappPl);
    }

    public DataTable GetMissingPunchedRecord(PLRollNumberMapping objRollNumberMappPl)
    {
        return objRollNumberMappDl.GetMissingPunchedRecord(objRollNumberMappPl);
    }

    public DataTable GetExtraPunchedRecord(PLRollNumberMapping objRollNumberMappPl)
    {
        return objRollNumberMappDl.GetExtraPunchedRecord(objRollNumberMappPl);
    }

    public DataSet InsertExtraPuncgedRecords(PLRollNumberMapping objRollNumberMappPl)
    {
        return objRollNumberMappDl.InsertExtraPuncgedRecords(objRollNumberMappPl);
    }
}