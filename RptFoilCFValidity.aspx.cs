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

public partial class RptFoilCFValidity : System.Web.UI.Page
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
            ddlregno.Enabled = false;
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

    protected void linkShowRpt_Click(object sender, EventArgs e)
    {
        if (chklist.Checked == true)
        {
           //For individual Register no. if checked
            Hashtable ht = new Hashtable();
            ht.Add("Ind", 11);//spreport for all Register no.

            ht.Add("ExamYear", ddlExamYear.SelectedValue);
            ht.Add("SessionType", ddlSession.SelectedValue);
            ht.Add("EntryByRegNo", ddlregno.SelectedItem.Text);

            Session["ht"] = ht;
            Session["Report"] = "RptFoilCounterFoilValidity";
            Response.Redirect("RptShow.aspx");

        }
        else
        {
            Hashtable ht = new Hashtable();
            ht.Add("Ind", 3);//spreport for all Register no.

            ht.Add("ExamYear", ddlExamYear.SelectedValue);
            ht.Add("SessionType", ddlSession.SelectedValue);
            //ht.Add("EntryByRegNo", 0);
            Session["ht"] = ht;
            Session["Report"] = "RptFoilCounterFoilValidity";
            Response.Redirect("RptShow.aspx");
        }
    }
    protected void ddlExamYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlExamYear.SelectedIndex == 0)
        {       
            ddlSession.SelectedValue = 0.ToString();
        }
        if(ddlExamYear.SelectedIndex>0)
        {
            chklist.Checked = false;
            ddlregno.Enabled = false;

        }
    }
    protected void chklist_CheckedChanged(object sender, EventArgs e)
    {
        if (chklist.Checked == true)
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
        else if (chklist.Checked == false)
        {
            ddlregno.Enabled = false;
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            chklist.Checked = false;
            ddlregno.Enabled = false;

        }
    }
}