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

public partial class RptDailyWorkReport : System.Web.UI.Page
{
    Hashtable ht;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (rbtnDailyEntry.Checked == true)
        {
            ht = new Hashtable();
            ht.Add("Ind", 7);
            Session["ht"] = ht;
            Session["Report"] = "RptUserRegAllotment";
            Response.Redirect("RptShow.aspx");
        }
        else if (rbtnErrorValidity.Checked == true)
        {
            ht = new Hashtable();
            ht.Add("Ind", 5);
            Session["ht"] = ht;
            Session["Report"] = "RptRegValidity";
            Response.Redirect("RptShow.aspx");
        }
        else if (rbtnRegAllotment.Checked == true)
        {
            ht = new Hashtable();
            ht.Add("Ind", 1);
            Session["ht"] = ht;
            Session["Report"] = "RptLotAllotment";
            Response.Redirect("RptShow.aspx");
        }
    }
}
