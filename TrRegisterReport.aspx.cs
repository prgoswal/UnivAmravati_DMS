using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
public partial class TrRegisterReport : System.Web.UI.Page
{
    BlRegister BlReg = new BlRegister();
    PlRegister PlReg = new PlRegister();
    DataTable dt;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    protected void linkShowRpt_Click(object sender, EventArgs e)
    {
        Hashtable ht = new Hashtable();
        ht.Add("Ind", 9);
        ht.Add("TrSearchType", ddlTrType.SelectedValue);
        Session["ht"] = ht;
        Session["Report"] = "RptTrRegister";
        Response.Redirect("RptShow.aspx");
    }
}
