using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


public class BlTRRegiter
{
    public BlTRRegiter()
    {

    }

    DlTRRegister dl = new DlTRRegister();
    public DataTable GetAllRegister(plTrRegister pl, int Ind)
    {
        return dl.GetAllRegister(pl, Ind);
    }

    public DataTable GetExamName(int Ind, int facultytype)
    {
        return dl.GetExamName(Ind, facultytype);
    }


    public DataTable GetLoginDetails(plTrRegister pl)
    {
        return dl.GetLoginDetails(pl);
    }
    public int CheckLockInd(plTrRegister pl)
    {
        return dl.CheckLockInd(pl);
    }

    public int ChangeLoginIdStatus(plTrRegister pl)
    {
        return dl.ChangeLoginIdStatus(pl);
    }
    public DataSet GetUserLoginID(plTrRegister pl)
    {
        DataSet ds = new DataSet();
        ds = dl.GetUserLoginID(pl);
        return ds;
    }
    public DataTable UpdateInvalidAttempts(plTrRegister pl)
    {
        DataTable dt = new DataTable();
        dt = dl.UpdateInvalidAttempts(pl);
        return dt;

    }
    public DataTable ChangePasswordFirst(plTrRegister pl)
    {

        DataTable dt = new DataTable();
        dt = dl.ChangePasswordFirst(pl);
        return dt;
    }
}