using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmDMSSearchRollNo : System.Web.UI.Page
{
    BlReports Blrep = new BlReports();
    DataTable dt, dtgrid = new DataTable();
    PlReports plrep = new PlReports();
    BlImgEntry blimg = new BlImgEntry();
    PlImgEntry plimg = new PlImgEntry();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divImage.Visible = false;
            GetExamSession();
            GetExamYear();
        }

    }
    public void GetExamSession()
    {
        plrep.Ind = 2;
        dt = Blrep.GetExamSession(plrep);
        ddlExamSession.DataTextField = "ItemDesc";
        ddlExamSession.DataValueField = "ItemID";
        ddlExamSession.DataSource = dt;

        ddlExamSession.DataBind();
        ddlExamSession.Items.Insert(0, "-- Select Session --");
    }
    public void GetExamYear()
    {
        plrep.Ind = 3;
        dt = Blrep.GetExamYear(plrep);
        ddlExamYear.DataTextField = "ItemDesc";
        ddlExamYear.DataValueField = "ItemID";
        ddlExamYear.DataSource = dt;
        ddlExamYear.DataBind();
        ddlExamYear.Items.Insert(0, "-- Select Year --");
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlExamSession.SelectedIndex == 0 || ddlExamYear.SelectedIndex == 0 || txtrollno.Text == "")
            AlertWarning.Visible = true;
        else
        {
            plimg.Ind = 50;
            plimg.ExamSession = Convert.ToInt32(ddlExamSession.SelectedIndex);
            plimg.Examyear = Convert.ToInt32(ddlExamYear.SelectedItem.Text);
            plimg.RollNo = Convert.ToInt32(txtrollno.Text);
            dt = blimg.SearchStudentDetail(plimg);
            if (dt!=null)
            {

                GridStudentDetail.DataSource =Session["dtSession"]= dt;
                GridStudentDetail.DataBind();
                pnlstudentDetail.Visible = true;
                //AlertRecordNotF.Visible = AlertWarning.Visible = false;
                //divImage.Visible = true;
                //ImageCF.ImageUrl = dt.Rows[0]["PartBImagePath"].ToString();
                //ImageFoil.ImageUrl = dt.Rows[0]["PartAImagePath"].ToString();
            }
            else
            {
                AlertRecordNotF.Visible = true;
            }
        }
    }

    protected void GridStudentDetail_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowPopup")
        {
            LinkButton btndetails = (LinkButton)e.CommandSource;
            GridViewRow GridStudentDetail = (GridViewRow)btndetails.NamingContainer;
            dt = new DataTable();
            dt = (DataTable)Session["dtSession"];
            int rwindex = GridStudentDetail.RowIndex;
            if (dt.Rows.Count > 0)
            {
                AlertRecordNotF.Visible = AlertWarning.Visible = false;
                divImage.Visible = true;
                ImageCF.ImageUrl = dt.Rows[rwindex]["PartBImagePath"].ToString();
                ImageFoil.ImageUrl = dt.Rows[rwindex]["PartAImagePath"].ToString();
                lblExamName.Text = "Exam Name - " + dt.Rows[rwindex]["ExamName"].ToString() + " || Exam Year - " + ddlExamYear.SelectedItem.ToString() + " || Session - " + ddlExamSession.SelectedItem.ToString() + " || Roll No - " + dt.Rows[rwindex]["RollNo"].ToString();
                lblFoilReg.Text = "(Register No. - " + dt.Rows[rwindex]["PartARegNo"].ToString() + ")";
                lblCFReg.Text = "(Register No. - " + dt.Rows[rwindex]["PartBRegNo"].ToString() + ")";

                Popup(true);
                
                return;
            }
        }
    }
    void Popup(bool isDisplay)
    {
        StringBuilder builder = new StringBuilder();
        if (isDisplay)
        {
            builder.Append("<script language=JavaScript> ShowPopup(); </script>\n");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPopup", builder.ToString());
        }
        else
        {
            builder.Append("<script language=JavaScript> HidePopup(); </script>\n");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "HidePopup", builder.ToString());
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        divImage.Visible = AlertRecordNotF.Visible = AlertWarning.Visible = false;
        ddlExamYear.SelectedIndex = 0;
        ddlExamSession.SelectedIndex = 0;
        txtrollno.Text = "";
        ImageCF.ImageUrl = ImageFoil.ImageUrl = null;
        
    }
}