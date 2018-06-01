using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BlFacultyMapping
/// </summary>
public class BlFacultyMapping
{
	public BlFacultyMapping()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    DlFacultyMapping objFacultyMapp = new DlFacultyMapping();

    public DataSet BindAllDDl(PlFacultyMapping objFacultyMappPl)
    {
        return objFacultyMapp.BindAllDDl(objFacultyMappPl);
    }

    public DataSet GetAllUniAndDMSExamFaculty(PlFacultyMapping objFacultyMappPl)
    {
        return objFacultyMapp.GetAllUniAndDMSExamFaculty(objFacultyMappPl);
    }

    public DataTable InsertUniAndDMSExamFacultyData(PlFacultyMapping objFacultyMappPl)
    {
        return objFacultyMapp.InsertUniAndDMSExamFacultyData(objFacultyMappPl);
    }
}