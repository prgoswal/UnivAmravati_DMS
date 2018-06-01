using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO; 

public partial class _Default : System.Web.UI.Page 
{
    SqlCommand cmd;
    SqlDataAdapter da;
    DataTable dt;
    DataRow dr;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["dtInSession"] = null;
        }
        dt = new DataTable();
        dt.Columns.Add("FileName");
        dt.Columns.Add("ID");
        dr = dt.NewRow();
        dr["FileName"] = "InsertImages/" + "00000001.JPG";
        dr["ID"] = 1;
        dt.Rows.Add(dr);
        Session["dtInSession"] = dt;     //Saving Datatable To Session 
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
   
}
