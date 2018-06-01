using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLExamMapping
/// </summary>
public class BLExamMapping
{
    DLExamMapping objExamMapp = new DLExamMapping();

    public DataSet BindAllDDl(PLExamMapping objExamMappPl)
    {
        return objExamMapp.BindAllDDl(objExamMappPl);
    }

    public DataSet GetAllUniAndDMSExam(PLExamMapping objExamMappPl)
    {
        return objExamMapp.GetAllUniAndDMSExam(objExamMappPl);
    }

    public DataTable InsertUniAndDMSExamMappingData(PLExamMapping objExamMappPl)
    {
        return objExamMapp.InsertUniAndDMSExamMappingData(objExamMappPl);
    }

    public DataTable GetUneversityRecords(PLExamMapping objExamMappPl)
    {
        return objExamMapp.GetUneversityRecords(objExamMappPl);
    }
}