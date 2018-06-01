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
public partial class UserLevelMaster : System.Web.UI.Page
{
    SqlCommand cmd;
    DataTable dt;
    SqlDataAdapter da; static string ItemId; static string ActiveType; int i, RetValue;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        txtUserLavel.Focus();
        GetData();
        LnkbtnUpdate.Enabled = false;
    }
    public void GetData()
    {
        try
        {
            da = new SqlDataAdapter("SPGetAllMaster", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Ind", 10);
            dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                //dt.Columns.RemoveAt(0);
                grdDetails.DataSource = dt;
                grdDetails.DataBind();
                grdDetails.Columns[1].Visible = false;
            }
            else
                lblmsg.Text = "Data Not Found";
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    public int CheckLevel()
    {
        cmd = new SqlCommand("SPGetAllMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", 13);
        cmd.Parameters.AddWithValue("@ItemDesc", txtUserLavel.Text.ToUpper());
        con.Open();
        i = Convert.ToInt32(cmd.ExecuteScalar());
        return i;
    }
    protected void linkSave_Click(object sender, EventArgs e)
    {
        try
        {
            RetValue = CheckLevel();
            if (RetValue > 0)
            {
                lblmsg.Text = "Enter Level Allready Exists. Please Enter Other User Level Type";
                txtUserLavel.Focus();
                return;
            }
            if (ddlIsActive.SelectedIndex == 1)
                ddlIsActive.SelectedIndex = 1;
            else if (ddlIsActive.SelectedIndex == 2)
                ddlIsActive.SelectedIndex = 0;
            cmd = new SqlCommand("SPGetAllMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", 11);
            cmd.Parameters.AddWithValue("@ItemDesc", txtUserLavel.Text.ToUpper());
            cmd.Parameters.AddWithValue("@IsActive", ddlIsActive.SelectedIndex);
            con.Open();
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            if (i > -1)
            {
                lblmsg.Text = "Lavel Inserted Successfully.";
                txtUserLavel.Focus();
                GetData();
                txtUserLavel.Text = "";
                ddlIsActive.SelectedIndex = 0;
            }
            else
                lblmsg.Text = "Lavel Not Inserted Please Try Again";
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    protected void linkClear_Click(object sender, EventArgs e)
    {
        txtUserLavel.Text = "";
        ddlIsActive.SelectedIndex = 0;
        linkSave.Enabled = true;
        LnkbtnUpdate.Enabled = false;
    }

    protected void grdDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "More")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                ItemId = ((Label)grdDetails.Rows[row.RowIndex].Cells[1].FindControl("lblItemId")).Text.ToString();
                txtUserLavel.Text = ((LinkButton)grdDetails.Rows[row.RowIndex].Cells[1].FindControl("lnkLevel")).Text.ToString();
                ActiveType = ((Label)grdDetails.Rows[row.RowIndex].Cells[2].FindControl("lblActive")).Text.ToString();
                if (ActiveType == "False")
                    ddlIsActive.SelectedIndex = 2;
                else if (ActiveType == "True")
                    ddlIsActive.SelectedIndex = 1;
                LnkbtnUpdate.Enabled = true;
                linkSave.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }

    protected void LnkbtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            RetValue = CheckLevel();
            if (RetValue > 0)
            {
                lblmsg.Text = "Enter Level Allready Exists. Please Enter Other User Level Type";
                txtUserLavel.Focus();
                return;
            }
            if (ddlIsActive.SelectedIndex == 1)
                ddlIsActive.SelectedIndex = 1;
            else if (ddlIsActive.SelectedIndex == 2)
                ddlIsActive.SelectedIndex = 0;
            cmd = new SqlCommand("SPGetAllMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", 12);
            cmd.Parameters.AddWithValue("@ItmId", Convert.ToString(ItemId));
            cmd.Parameters.AddWithValue("@ItemDesc", txtUserLavel.Text.ToUpper());
            cmd.Parameters.AddWithValue("@IsActive", ddlIsActive.SelectedIndex);
            con.Open();
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            if (i > -1)
            {
                lblmsg.Text = "Lavel Update Successfully.";
                txtUserLavel.Focus();
                GetData();
                txtUserLavel.Text = "";
                ddlIsActive.SelectedIndex = 0;
                linkSave.Enabled = true;
            }
            else
                lblmsg.Text = "Lavel Not Update Please Try Again";
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    protected void grdDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
}
