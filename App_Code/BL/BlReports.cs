using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class BlReports
{
    DlReports dlRpt = new DlReports();
    DlRegister dlReg = new DlRegister();

    public DataTable GetReport(PlReports plRpt)
    {
        return dlRpt.GetReport(plRpt);
    }

    public DataTable GetRptFoilCFValidity(PlReports plRpt)
    {
        return dlRpt.GetRptFoilCFValidity(plRpt);
    }

    public DataTable GetPhysicalPath(PlReports plRpt)
    {
        return dlRpt.GetPhysicalPath(plRpt);
    }

    public DataTable GetExamYear(PlReports plRpt)
    {
        return dlRpt.GetExamYear(plRpt);
    }

    public DataTable GetExamSession(PlReports plrep)
    {
        return dlRpt.GetExamSession(plrep);
    }

    public DataTable MISReport(PlReports plrep)
    {
        return dlRpt.MISReport(plrep);
    }
    public DataTable ShowRegister(PlReports plrep)
    {
        return dlRpt.ShowRegister(plrep);
    }
    public DataTable ShowErrorValidity(PlReports plrep)
    {
        return dlRpt.ShowErrorValidity(plrep);
    }

    public DataTable ErroInDuplicateEntry(PlReports PlRpt)
    {
        return dlRpt.ErroInDuplicateEntry(PlRpt);
    }
    
    public DataTable ShowRegAllotedStatus(PlReports PlRpt)
    {
        return dlRpt.ShowRegAllotedStatus(PlRpt);
    }

}