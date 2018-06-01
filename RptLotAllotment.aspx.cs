using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Data;
public partial class RptLotAllotment : System.Web.UI.Page
{
    Hashtable ht; SqlCommand cmd; SqlDataAdapter da; DataTable dt;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    string Date;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDdlUserType();
            pnlDateWise.Visible = false;
            pnluserWise.Visible = false;
            pnlTableHdng.Visible = false;
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        ht = new Hashtable();
        ht.Add("Ind", 6);
        if (rbtnAll.Checked == true)
        {
            Session["ht"] = ht;
            Session["Report"] = "RptAllValidity";
            Response.Redirect("RptShow.aspx");
        }
        else if (rbtnDateWise.Checked == true)
        {
            DateTime dateTo, dateFrom;
            if (!DateTime.TryParse(txtDateTo.Text, out dateTo))
            {
                lblmsg.Text = "Invalid Date.";
                return;
            }
            if (!DateTime.TryParse(txtDateFrom.Text, out dateFrom))
            {
                lblmsg.Text = "Invalid Date.";
                return;
            }


            Date = Convert.ToDateTime(txtDateTo.Text).AddDays(+1).ToString("dd/MM/yyyy");
            ht.Add("DateFrom", Convert.ToDateTime(txtDateFrom.Text));
            ht.Add("DateTo", Convert.ToDateTime(Date));
            ht.Add("RptInd", 1);
            Session["ht"] = ht;
            Session["Report"] = "RptAllValidity";
            Response.Redirect("RptShow.aspx");
        }
        else if (rbtnUserWise.Checked == true)
        {
            if (ddlUserWise.SelectedValue == "0")
            {
                lblmsg.Text = "Please Select User First";
                return;
            }
            ht.Add("ExamYear", ddlUserWise.SelectedValue.ToString());
            ht.Add("RptInd", 2);
            Session["ht"] = ht;
            Session["Report"] = "RptAllValidity";
            Response.Redirect("RptShow.aspx");
        }
        else if (rbtnUserWithDate.Checked == true)
        {
            if (ddlUser.SelectedValue == "0")
            {
                lblmsg.Text = "Please Select User First";
                return;
            }
            DateTime dateTo, dateFrom;
            if (!DateTime.TryParse(txtDateTo.Text, out dateTo))
            {
                lblmsg.Text = "Invalid Date.";
                return;
            }
            if (!DateTime.TryParse(txtDateFrom.Text, out dateFrom))
            {
                lblmsg.Text = "Invalid Date.";
                return;
            }
            Date = Convert.ToDateTime(txtDateTo.Text).AddDays(+1).ToString("dd/MM/yyyy");
            ht.Add("DateFrom", Convert.ToDateTime(txtDateFrom.Text));
            ht.Add("DateTo", Convert.ToDateTime(Date));
            ht.Add("ExamYear", ddlUser.SelectedValue.ToString());
            ht.Add("RptInd", 3);
            Session["ht"] = ht;
            Session["Report"] = "RptAllValidity";
            Response.Redirect("RptShow.aspx");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {

        lblmsg.Text = "";
        cmd = new SqlCommand("SPReport", con);
        cmd.Parameters.AddWithValue("@Ind", 6);
        if (rbtnAll.Checked)
        {
            lblCount.Text = "";
            grdDetails.DataSource = null;
            grdDetails.DataBind();
            cmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                int sum = dt.AsEnumerable().Sum(row => row.Field<int>("Cnt"));
                lblCount.Text = "Total No. of Records :- " + sum.ToString();
                pnlTableHdng.Visible = true;
                dt.Columns.Remove("UserID");
                dt.Columns.Remove("TotalRecords");
                grdDetails.DataSource = dt;
                grdDetails.DataBind();
            }
            else
            {
                pnlTableHdng.Visible = false;
                lblmsg.Text = "Data Not Found";
                grdDetails.DataSource = null;
                grdDetails.DataBind();
                lblCount.Text = "";
            }
        }
        else if (rbtnDateWise.Checked == true)
        {
            //Date = txtDateTo.Text;
            //if (txtDateFrom.Text == txtDateTo.Text)
            //{
            DateTime dateTo, dateFrom;
            if (!DateTime.TryParse(txtDateTo.Text, out dateTo))
            {
                lblmsg.Text = "Invalid Date.";
                return;
            }
            if (!DateTime.TryParse(txtDateFrom.Text, out dateFrom))
            {
                lblmsg.Text = "Invalid Date.";
                return;
            }


            Date = Convert.ToDateTime(txtDateTo.Text).AddDays(+1).ToString("dd/MM/yyyy");
            // }
            lblCount.Text = "";
            grdDetails.DataSource = null;
            grdDetails.DataBind();
            cmd.Parameters.AddWithValue("@DateFrom", Convert.ToDateTime(txtDateFrom.Text));
            cmd.Parameters.AddWithValue("@DateTo", Convert.ToDateTime(Date));
            cmd.Parameters.AddWithValue("@RptInd", 1);
            cmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                int sum = dt.AsEnumerable().Sum(row => row.Field<int>("Cnt"));
                lblCount.Text = "Total No. of Records :- " + sum.ToString();
                pnlTableHdng.Visible = true;
                dt.Columns.Remove("UserID");
                dt.Columns.Remove("TotalRecords");
                grdDetails.DataSource = dt;
                grdDetails.DataBind();
            }
            else
            {
                pnlTableHdng.Visible = false;
                lblmsg.Text = "Data Not Found";
                grdDetails.DataSource = null;
                grdDetails.DataBind();
                lblCount.Text = "";
            }
        }
        else if (rbtnUserWise.Checked == true)
        {
            if (ddlUserWise.SelectedValue == "0")
            {
                lblmsg.Text = "Please Select User First";
                return;
            }
            lblCount.Text = "";
            grdDetails.DataSource = null;
            grdDetails.DataBind();
            cmd.Parameters.AddWithValue("@ExamYear", ddlUserWise.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@RptInd", 2);
            cmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                int sum = dt.AsEnumerable().Sum(row => row.Field<int>("Cnt"));
                lblCount.Text = "Total No. of Records :- " + sum.ToString();
                pnlTableHdng.Visible = true;
                dt.Columns.Remove("UserID");
                dt.Columns.Remove("TotalRecords");
                grdDetails.DataSource = dt;
                grdDetails.DataBind();
            }
            else
            {
                pnlTableHdng.Visible = false;
                lblmsg.Text = "Data Not Found";
                grdDetails.DataSource = null;
                grdDetails.DataBind();
                lblCount.Text = "";
            }
        }
        else if (rbtnUserWithDate.Checked == true)
        {
            if (ddlUser.SelectedValue == "0")
            {
                lblmsg.Text = "Please Select User First";
                return;
            }

            DateTime dateTo, dateFrom;
            if (!DateTime.TryParse(txtDateTo.Text, out dateTo))
            {
                lblmsg.Text = "Invalid Date.";
                return;
            }
            if (!DateTime.TryParse(txtDateFrom.Text, out dateFrom))
            {
                lblmsg.Text = "Invalid Date.";
                return;
            }

            Date = Convert.ToDateTime(txtDateTo.Text).AddDays(+1).ToString("dd/MM/yyyy");
            lblCount.Text = "";
            grdDetails.DataSource = null;
            grdDetails.DataBind();
            cmd.Parameters.AddWithValue("@DateFrom", Convert.ToDateTime(txtDateFrom.Text));
            cmd.Parameters.AddWithValue("@DateTo", Convert.ToDateTime(Date));
            cmd.Parameters.AddWithValue("@ExamYear", ddlUser.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@RptInd", 3);
            cmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                int sum = dt.AsEnumerable().Sum(row => row.Field<int>("Cnt"));
                lblCount.Text = "Total No. of Records :- " + sum.ToString();
                pnlTableHdng.Visible = true;
                dt.Columns.Remove("UserID");
                dt.Columns.Remove("TotalRecords");
                grdDetails.DataSource = dt;
                grdDetails.DataBind();
            }
            else
            {
                pnlTableHdng.Visible = false;
                lblmsg.Text = "Data Not Found";
                grdDetails.DataSource = null;
                grdDetails.DataBind();
                lblCount.Text = "";
            }
        }
    }
    protected void rbtnDateWise_CheckedChanged(object sender, EventArgs e)
    {
        pnlDateWise.Visible = true;
        pnluserWise.Visible = false;
        lblUser.Visible = false;
        ddlUser.Visible = false;
        pnlTableHdng.Visible = false;
        grdDetails.DataSource = null;
        grdDetails.DataBind();
        lblCount.Text = "";
        lblmsg.Text = "";
    }
    protected void rbtnAll_CheckedChanged(object sender, EventArgs e)
    {
        pnluserWise.Visible = false;
        pnlDateWise.Visible = false;
        pnlTableHdng.Visible = false;
        grdDetails.DataSource = null;
        grdDetails.DataBind();
        lblCount.Text = "";
        lblmsg.Text = "";
    }
    protected void rbtnUserWise_CheckedChanged(object sender, EventArgs e)
    {
        pnlDateWise.Visible = false;
        pnlTableHdng.Visible = false;
        pnluserWise.Visible = true;
        grdDetails.DataSource = null;
        grdDetails.DataBind();
        lblCount.Text = "";
        lblmsg.Text = "";
    }
    protected void rbtnUserWithDate_CheckedChanged(object sender, EventArgs e)
    {
        pnlDateWise.Visible = true;
        pnluserWise.Visible = false;
        pnlTableHdng.Visible = false;
        lblUser.Visible = true;
        ddlUser.Visible = true;
        grdDetails.DataSource = null;
        grdDetails.DataBind();
        lblCount.Text = "";
        lblmsg.Text = "";
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
            ddlUser.DataSource = dt;
            ddlUser.DataTextField = "UserName";
            ddlUser.DataValueField = "UserID";
            ddlUser.DataBind();

            ddlUserWise.DataSource = dt;
            ddlUserWise.DataTextField = "UserName";
            ddlUserWise.DataValueField = "UserID";
            ddlUserWise.DataBind();
        }
    }
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}