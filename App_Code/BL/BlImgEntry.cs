using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


public class BlImgEntry
{
    DlImgEntry dl = new DlImgEntry();
    public BlImgEntry()
    {
    }

    public int InserImgEntry(PlImgEntry pl)
    {
        int i = dl.InserImgEntry(pl);
        return i;
    }
    public int InsertCFImgEntry(PlImgEntry pl)
    {
        int i = dl.InsertCFImgEntry(pl);
        return i;
    }
    public DataTable GetCompletedPageNo(PlImgEntry pl)
    {
        DataTable dt = new DataTable();
         dt = dl.GetICompetedPages(pl);
        return dt;
    }
    public DataSet LastPageNoWithLotNo(PlImgEntry pl)
    {
        return dl.LastPageNoWithLotNo(pl);
    }
    public int DeletePageNoData(PlImgEntry pl)
    {
        int i = dl.DeletePageNoData(pl);
        return i;
    }

    public DataTable SearchStudentDetail(PlImgEntry pl)
    {
        DataTable dt = new DataTable();
        return dt = dl.SearchStudentDetail(pl);
  
    }
}