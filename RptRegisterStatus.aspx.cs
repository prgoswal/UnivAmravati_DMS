using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RptRegisterStatus : System.Web.UI.Page
{
    BlReports BlRpt = new BlReports();
    BlRegister blReg = new BlRegister();
    DataTable dt, dtgrid = new DataTable();
    PlReports PlRpt = new PlReports();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetExamYear();
            GetExamSession();
        }
    }
    protected void GetExamSession()
    {
        PlRpt.Ind = 2;
        dt = BlRpt.GetExamSession(PlRpt);
        ddlSession.DataTextField = "ItemDesc";
        ddlSession.DataValueField = "ItemID";
        ddlSession.DataSource = dt;

        ddlSession.DataBind();
        ddlSession.Items.Insert(0, "-- Select Session --");
    }
    protected void GetExamYear()
    {
        PlRpt.Ind = 3;
        dt = BlRpt.GetExamYear(PlRpt);
        ddlExamYear.DataTextField = "ItemDesc";
        ddlExamYear.DataValueField = "ItemID";
        ddlExamYear.DataSource = dt;
        ddlExamYear.DataBind();
        ddlExamYear.Items.Insert(0, "-- Select Year --");
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (ddlExamYear.SelectedIndex == 0)
        {
            lblMsg.Text = "Please Select Exam Name";
            return;
        }
        if (ddlSession.SelectedIndex == 0)
        {
            lblMsg.Text = "Please Select Exam Session";
            return;
        }
        PlRpt = new PlReports()
        {
            Ind = 14,
            ExamYear = ddlExamYear.SelectedItem.Text,
            Session = ddlSession.SelectedIndex.ToString(),
            RptInd = rbFilterStatus.SelectedIndex + 1,
        };
        dt = BlRpt.ShowRegAllotedStatus(PlRpt);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.NewRow();
            //dr[0] = 0;
            dr[1] = dt.Rows.Count;
            //dr[2] = "";
            dt.Rows.Add(dr);

            gvAllotmentStatus.DataSource = dt;
            gvAllotmentStatus.DataBind();
            //ArrangeGrid();
            divGrid.Visible = true;
            grdHeader.Text = "Status For "+ rbFilterStatus.SelectedItem.Text + " Registers";
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "PrintDiv", "PrintDiv()", true);
    }
    protected void lnkexcel_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (gvAllotmentStatus.Rows.Count > 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "GenerateExcel", "GenerateExcel()", true);
        }
        else Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Data Not Be Null Or Empty.')", true);
    }    
    protected void ddlExamYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlExamYear.SelectedIndex == 0)
            lblMsg.Text = "Please Select Exam Year!";
        else lblMsg.Text = "";

        gvAllotmentStatus.DataSource = null;
        gvAllotmentStatus.DataBind();
        divGrid.Visible = false;
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex == 0)
            lblMsg.Text = "Please Select Exam Session!";
        else lblMsg.Text = "";

        divGrid.Visible = false;
        gvAllotmentStatus.DataSource = null;
        gvAllotmentStatus.DataBind();
    }
    protected void gvAllotmentStatus_DataBound(object sender, EventArgs e)
    {
        //if (group == null)
        //{
        //    return;
        //}
        //foreach (ListItem item in group)
        //{
        //    GridViewRow row1 = gvAllotmentStatus.Rows[Convert.ToInt16(item.Value)];
        //    row1.Cells[0].ColumnSpan = 7;
        //    row1.Cells[0].Text = item.Text;
        //}
    }
    List<ListItem> group;
    void ArrangeGrid()
    {
        group = new List<ListItem>();
        dtgrid = gvAllotmentStatus.DataSource as DataTable;
        for (int i = gvAllotmentStatus.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = gvAllotmentStatus.Rows[i];
            GridViewRow previousRow = gvAllotmentStatus.Rows[i - 1];
            int j = 0;
            if (row.Cells[j].Text == previousRow.Cells[j].Text)
            {
                //if (previousRow.Cells[j].RowSpan == 0)
                //{
                //    if (row.Cells[j].RowSpan == 0)
                //    {
                //        previousRow.Cells[j].RowSpan += 2;
                //    }
                //    else
                //    {
                //        previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                //    }
                //    row.Cells[j].Visible = false;
                //}
            }
            else
            {
                DataRow dr = dtgrid.NewRow();
                dr[1] = row.Cells[j].Text;
                dtgrid.Rows.InsertAt(dr, i);
                gvAllotmentStatus.DataSource = dtgrid;
                GridViewRow NewRow = gvAllotmentStatus.Rows[i];
                NewRow.Cells[j].ColumnSpan = 7;
                group.Add(new ListItem { Text = row.Cells[j].Text, Value = (i).ToString() });
            }
        }        
        gvAllotmentStatus.DataBind();
    }
    void Clear()
    {
        lblMsg.Text = "";
        ddlExamYear.SelectedIndex = ddlSession.SelectedIndex = rbFilterStatus.SelectedIndex = 0;
        gvAllotmentStatus.DataSource = null;
        gvAllotmentStatus.DataBind();
        divGrid.Visible = false;

    }
}