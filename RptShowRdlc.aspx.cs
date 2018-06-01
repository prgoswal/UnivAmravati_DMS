using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class RptShow : System.Web.UI.Page
{
    DataTable dt; Hashtable ht;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dt"] != null)
            {
                ht = new Hashtable();
                ht = (Hashtable)Session["ht"];
                RptTrPageDetails.ProcessingMode = ProcessingMode.Local;
                RptTrPageDetails.LocalReport.ReportPath = Server.MapPath("~/Reports/" + Session["ReportName"].ToString() + "");
                dt = GetData();
                //int TotalParameters = RptTrPageDetails.LocalReport.GetParameters().Count();
                ReportParameter[] parm = new ReportParameter[ht.Count];
                int i = 0;
                foreach (DictionaryEntry Dt in ht)
                {
                    parm[i] = new ReportParameter(Convert.ToString(Dt.Key), Convert.ToString(Dt.Value));
                    i++;
                }
                RptTrPageDetails.LocalReport.SetParameters(parm);
                ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
                RptTrPageDetails.LocalReport.DataSources.Clear();
                RptTrPageDetails.LocalReport.DataSources.Add(datasource);
            }
            else
            {
                Response.Redirect("Home.aspx");
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["Report"] = null;
        Session["ReportName"] = null;
        Response.Redirect(Session["PageName"].ToString(), false);
        Session["PageName"] = null;
        Session["dt"] = null;
    }
    private DataTable GetData()
    {
        dt = (DataTable)Session["dt"];
        return dt;
    }
}