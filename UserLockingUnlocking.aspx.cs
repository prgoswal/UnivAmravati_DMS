using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class UserLockingUnlocking : System.Web.UI.Page
{
    DlUserCreation dlUserCreation = new DlUserCreation();
    PlUserCreation plUserCreation = new PlUserCreation();

    DataTable dt;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetLockedUsers();
        }
    }
    private DataTable GetLockedUsers()
    {
        plUserCreation.Ind = 23;
        plUserCreation.UserId = Convert.ToInt32(Session["UserId"]);
        dt = dlUserCreation.GetLockedUsers(plUserCreation);
        if (dt.Rows.Count > 0)
        {
            grdUserUnlocking.Visible = true;
            grdUserUnlocking.DataSource = dt;
            grdUserUnlocking.DataBind();
        }
        else
        {
            grdUserUnlocking.Visible = false;
            grdUserUnlocking.DataSource = null;
            lblMsg.Text = "No User(s) To Unlock";
        }
        return dt;
    }
    protected void grdUserUnlocking_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        int i = Convert.ToInt32(grdUserUnlocking.DataKeys[e.RowIndex].Value);
        //string LnkBtn = grdUserUnlocking.FindControl("lnkUpdte").ToString();
        plUserCreation.Ind = 24;
        plUserCreation.UserId = i;
        i = dlUserCreation.UpdateUnlockedUser(plUserCreation);
        if (i > 0)
        {
            lblMsg.Text = "User Unlocked Successfully";
        }
        else
            lblMsg.Text = "User Could Not Be Unlocked Successfully";
    }
    protected void grdUserUnlocking_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk = (LinkButton)e.Row.FindControl("lnkUpdte");
            if (lnk.Text == "Lock")
            {
                lnk.Text = "Unlock";
            }
            else
            {
                lnk.Text = "Lock";
            }
        }
    }
    protected void grdUserUnlocking_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int Userid = Convert.ToInt32(grdUserUnlocking.DataKeys[e.NewSelectedIndex].Value);
        string Action = grdUserUnlocking.Rows[e.NewSelectedIndex].Cells[3].Text;
        if (Action == "Lock")
        {
            Action = "Unlock";
        }
        else
        {
            Action = "Lock";
        }
        plUserCreation.Ind = 24;
        plUserCreation.UserId = Userid;
        int i = dlUserCreation.UpdateUnlockedUser(plUserCreation);
        if (i > 0)
        {
            lblMsg.Text = "User Unlocked Successfully";
        }
        else
            lblMsg.Text = "User Could Not Be Unlocked Successfully";
    }
}