using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmErrorValidity : System.Web.UI.Page
{
    BlReports BlRpt = new BlReports();
    PlReports PlRpt = new PlReports();
    BlTRRegiter BlTrReg = new BlTRRegiter();
    plTrRegister plTrReg = new plTrRegister();
    DataTable dt; SqlDataAdapter da; SqlCommand cmd; DataSet1 ds1;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindYear();
            BindSession();
        }
    }
    private DataTable BindYear() // Bind Exam Year (like 2000,2001 etc.)
    {
        dt = BlTrReg.GetAllRegister(plTrReg, 3);
        dt.Rows.InsertAt(dt.NewRow(), 0);
        dt.Rows[0][0] = "0";
        dt.Rows[0][1] = "Select Year";
        ddlExamYear.DataSource = dt;
        ddlExamYear.DataValueField = "ItemDesc"; // To filter Exam Name 
        ddlExamYear.DataTextField = "ItemDesc";
        ddlExamYear.DataBind();
        return dt;
    }
    private DataTable BindSession()  // Bind Session (Winter,Summer)
    {
        dt = BlTrReg.GetAllRegister(plTrReg, 2);
        dt.Rows.InsertAt(dt.NewRow(), 0);
        dt.Rows[0][0] = "0";
        dt.Rows[0][1] = "Select Session";
        ddlSession.DataSource = dt;
        ddlSession.DataValueField = "ItemID";
        ddlSession.DataTextField = "ItemDesc";
        ddlSession.DataBind();
        return dt;
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
      
            if (ddlExamYear.SelectedIndex > 0)
            {
                if (ddlSession.SelectedIndex > 0)
                {
                    //spreport for loading register no.
                    lblmsg.Text = "";
                    ddlregno.Enabled = true;
                    PlRpt.Ind = 9;
                    PlRpt.Session = ddlSession.SelectedValue;
                    PlRpt.ExamYear = ddlExamYear.SelectedItem.Text;
                    dt = BlRpt.ShowRegister(PlRpt);
                    ddlregno.DataSource = dt;
                    ddlregno.DataTextField = "RegisterNo";
                    ddlregno.DataBind();
                    ddlregno.Items.Insert(0, "Register No");
                }
                else
                    lblmsg.Text = "Please Select Exam Session.";
            }
            else
                lblmsg.Text = "Please Select Exam Year.";    

    }
    protected void ddlExamYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlExamYear.SelectedIndex == 0)
        {
            ddlSession.SelectedValue = 0.ToString();
        }
        if (ddlExamYear.SelectedIndex > 0)
        {      
            ddlregno.Enabled = false;
        }
    }
  
    protected void linkCancel_Click(object sender, EventArgs e)
    {
        ddlExamYear.Items.Clear();
        ddlSession.Items.Clear();
        ddlregno.Items.Clear();
    }
    protected void linkShow_Click(object sender, EventArgs e)
    {
        if (ddlregno.SelectedIndex > 0 && ddlExamYear.SelectedIndex>0 && ddlSession.SelectedIndex>0 )
        {
            PlRpt.Ind = 10;
            PlRpt.EntryByRegNo = Convert.ToInt32(ddlregno.SelectedItem.Text);
            dt = new DataTable();
            dt = BlRpt.ShowErrorValidity(PlRpt);
            grddata.DataSource = dt;
            grddata.DataBind();
        }
        else
        {
            lblmsg.Text = "Please Select Proper Value!";
        }
    }
}