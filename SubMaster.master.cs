using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class SubMaster : System.Web.UI.MasterPage
{
    BlTRRegiter BlTrReg = new BlTRRegiter();
    plTrRegister plTrReg = new plTrRegister();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["USERNAME"] != null)
            {
                if (Session["UserType"].ToString() == "ADMIN" || Session["UserType"].ToString() == "ENTRY LEVEL")
                {
                    lblUserName.Text = Session["USERNAME"].ToString() + " [" + Session["UserType"] + "] " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                    //panelEntryLevel.Visible = false;
                }
                else
                {
                    //lblUser.Text = Session["USERNAME"].ToString() + " [" + Session["UserType"] + "] " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                    panelAdmin.Visible = false;
                }
            }
            else
            {
                plTrReg.Ind = 3;
                plTrReg.UserId = Convert.ToInt32(Session["UserId"]);
                int i = BlTrReg.ChangeLoginIdStatus(plTrReg);
                Response.Redirect("UserLogin.aspx");
            }
        }
    }
    protected void lnkLogOut_Click(object sender, EventArgs e)
    {
        plTrReg.Ind = 3;
        plTrReg.UserId = Convert.ToInt32(Session["UserId"]);
        int i = BlTrReg.ChangeLoginIdStatus(plTrReg);
        Session["USERNAME"] = null;
        Session["UserId"] = null;
        Session["UserType"] = null;
        Session.Clear();
        Session.Abandon();
        Response.Redirect("UserLogin.aspx");
    }
}
