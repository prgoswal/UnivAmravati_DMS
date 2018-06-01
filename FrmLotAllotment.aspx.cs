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

public partial class FrmLotAllotment : System.Web.UI.Page
{
    SqlCommand cmd; DataTable dt; DataSet ds; SqlDataAdapter da;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    int i, MaxId; CheckBox chk; string CountPages; int AllotedRegNo;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDdlUserType();
            BindGrid();
        }
    }
    public void BindDdlUserType()
    {
        cmd = new SqlCommand("SpGetAllMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ind", 9);
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0][0] = 0;
            dt.Rows[0][1] = ".....Select.....";
            ddlUserName.DataSource = dt;
            ddlUserName.DataTextField = "UserName";
            ddlUserName.DataValueField = "UserID";
            ddlUserName.DataBind();
        }
    }
    public void BindGrid()
    {
        cmd = new SqlCommand("SpReport", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ind", 4);
        cmd.Parameters.AddWithValue("@RptInd", 0);
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            dt.Columns.Remove("RegCreationDate");
            dt.Columns.Remove("ExamYear");
            dt.Columns.Remove("SessionType");
            dt.Columns.Remove("ExamType");
            dt.Columns.Remove("ctrl");
            dt.Columns.Remove("CurrentBookNo");
            dt.Columns.Remove("TotalBooks");
            dt.Columns.Remove("TotalBooks2");
            //dt.Columns.Remove("TotalPages");
            dt.Columns.Remove("TrType");
            dt.Columns.Remove("RNo");
            dt.Columns.Remove("RegCreationDate2");
            dt.Columns.Remove("ExamYear2");
            dt.Columns.Remove("SessionType2");
            // dt.Columns.Remove("ctrl2");
            dt.Columns.Remove("CurrentBookNo2");
            //dt.Columns.Remove("TotalPages2");
            dt.Columns.Remove("TrType2");
            dt.Columns.Remove("RNo2");
            dt.Columns.Remove("RegNoFoil2");
            dt.Columns.Remove("RecNo");
            dt.Columns.Remove("ErrorData");
            dt.Columns.Remove("ExamName");
            grdEntry.DataSource = dt;
            grdEntry.DataBind();
        }
    }
    public int GetMaxLotId()
    {
        cmd = new SqlCommand("SPAllotment", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", 2);
        con.Open();
        i = Convert.ToInt32(cmd.ExecuteScalar());
        con.Close();
        return i;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        int k = 0;
        i = 0;
        try
        {
            if (ddlUserName.SelectedValue == "0")
            {
                lblmsg.Text = "Please Select User Name";
                ddlUserName.Focus();
                return;
            }
            else if (ddlAllotmentNo.SelectedIndex == 0)
            {
                lblmsg.Text = "Please SelectAllotment No.";
                ddlUserName.Focus();
                return;
            }
            DataTable dtMobNo = new DataTable();
            //dtMobNo.Columns.Add("RegNo", typeof(int));
            //dtMobNo.Columns.Add("MemberName", typeof(string));
            dtMobNo.Columns.Add("MobileNo", typeof(string));
          
            foreach (GridViewRow gvrow in grdEntry.Rows)
            {
                chk = (CheckBox)gvrow.FindControl("chkselect");
                if (chk.Checked == true)
                {
                    dtMobNo.Rows.Add(grdEntry.Rows[i].Cells[1].Text.ToString());
                    //CountPages += Convert.ToInt32(grdEntry.Rows[i].Cells[3].Text.ToString());
                }
                i++;
            }
            if (dtMobNo.Rows.Count < 1)
            {
                lblmsg.Text = "Please select atleast one receipient.";
                return;
            }
            MaxId = GetMaxLotId();
            //MobEntry = MobEntry.Substring(0, MobEntry.Length - 1);
            for (i = 0; i < dtMobNo.Rows.Count; i++)
            {
                AllotedRegNo = Convert.ToInt32(dtMobNo.Rows[i][0].ToString());
                cmd = new SqlCommand("SPAllotment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ind", 1);
                cmd.Parameters.AddWithValue("@RegNo", AllotedRegNo);
                cmd.Parameters.AddWithValue("@LotNo", MaxId);
                cmd.Parameters.AddWithValue("@AllotmentNo", ddlAllotmentNo.SelectedIndex);
                cmd.Parameters.AddWithValue("@AllottedByUser", Session["UserId"]);
                cmd.Parameters.AddWithValue("@AllottedToUser", Convert.ToInt32(ddlUserName.SelectedValue));
                con.Open();
                k = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            if (k >= 0)
            {
                lblmsg.Text = "Register Allotment Successfully For User " + ddlUserName.SelectedItem.ToString();
                BindGrid();
                ddlUserName.SelectedValue = "0";
                ddlAllotmentNo.SelectedIndex = 0;
                dtMobNo.Rows.Clear();
                // dtMobNo.Columns.Clear();

            }
            else
                lblmsg.Text = "Register Not Allotment";
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlUserName.SelectedValue = "0";
        ddlAllotmentNo.SelectedIndex = 0;
    }
}
