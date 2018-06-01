using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
/// <summary>
/// Summary description for DataAcces
/// </summary>
public class DataAcces
{
    
    public DataAcces()
    {

    }
    public static SqlConnection Occconnection()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        if (con.State == ConnectionState.Closed)
            con.Open();
        return con;
    }
}