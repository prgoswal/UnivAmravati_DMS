using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
public partial class RptMissingLots : System.Web.UI.Page
{
    BlReports Blrep = new BlReports();
    DataTable dt, dtgrid = new DataTable();
    PlReports plrep = new PlReports();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetExamYear();
        }
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

    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (ddlExamYear.SelectedIndex > 0)
        {
            Hashtable ht = new Hashtable();
            ht.Add("Ind",12);
            ht.Add("ExamYear", ddlExamYear.SelectedItem.Text);
            Session["ht"] = ht;
            Session["Report"] = "Rpt_MissingLot";
            Response.Redirect("RptShow.aspx");
        }



    }
}