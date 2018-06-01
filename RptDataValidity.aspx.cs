using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RptDataValidity : System.Web.UI.Page
{
    BlReports BlRpt = new BlReports();
    BlRegister blReg = new BlRegister();
    DataTable dt, dtgrid = new DataTable();
    PlReports PlRpt = new PlReports();
    PlRegister plReg = new PlRegister();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetExamYear();
            GetExamSession();
        }
        //ddlExamYear.Items.Insert(0, "select");
        //ddlSession.Items.Insert(0, "select");
    }
    void GetExamSession()
    {
        PlRpt.Ind = 2;
        dt = new DataTable();
        dt = BlRpt.GetExamSession(PlRpt);
        ddlSession.DataTextField = "ItemDesc";
        ddlSession.DataValueField = "ItemID";
        ddlSession.DataSource = dt;

        ddlSession.DataBind();
        ddlSession.Items.Insert(0, "-- Select Session --");
    }
    void GetExamYear()
    {
        PlRpt.Ind = 3;
        dt = new DataTable();
        dt = BlRpt.GetExamYear(PlRpt);
        ddlExamYear.DataTextField = "ItemDesc";
        ddlExamYear.DataValueField = "ItemID";
        ddlExamYear.DataSource = dt;
        ddlExamYear.DataBind();
        ddlExamYear.Items.Insert(0, "-- Select Year --");
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridsBlank();
        try
        {
            cbSelectAll.Checked = false;
            
            if (ddlExamYear.SelectedIndex > 0)
            {
                if (ddlSession.SelectedIndex > 0)
                {
                    //spreport for loading register no.
                    PlRpt.Ind = 9;
                    PlRpt.Session = ddlSession.SelectedValue;
                    PlRpt.ExamYear = ddlExamYear.SelectedItem.Text;
                    dt = new DataTable();
                    dt = BlRpt.ShowRegister(PlRpt);
                    dt.NewRow();
                    cbRegNo.DataSource = dt;
                    cbRegNo.DataTextField = "RegisterNo";
                    cbRegNo.DataBind();
                }
                else
                {
                    cbRegNo.DataSource = null; cbRegNo.DataBind();
                }
            }
            else
            {
                cbRegNo.DataSource = null; cbRegNo.DataBind();
            }
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + "There Is Problem Because Of - " + ex.Message + "')", true);
        }
    }

    protected void ddlExamYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridsBlank();
        cbSelectAll.Checked = false;
        if (ddlExamYear.SelectedIndex == 0)
        {
            //ddlExamYear.CssClass += " has-error";
        }
        if (ddlExamYear.SelectedIndex > 0)
        {
            ddlSession.SelectedIndex = 0;
        }
    }

    protected void ddlValidityReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridsBlank();
    }

    string ExamYear, ExamSession;
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = ""; UpdatedList = new List<int>();//for Update Row Color Change in grid In gvWrongRollno.
            if (ddlExamYear.SelectedIndex == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please Select Exam Year')", true);
                return;
            }
            if (ddlSession.SelectedIndex == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert1", "alert('Please Select Exam Session')", true);
                return;
            }
            if (ddlValidityReport.SelectedIndex == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert2", "alert('Please Select Validity Report')", true);
                return;
            }
            GridsBlank();
            //Get Selected CheckBox.
            List<ListItem> lstItem = new List<ListItem>();
            Register.Value = "";
            int count = 0;
            foreach (ListItem item in cbRegNo.Items)
            {
                if (item.Selected)
                {
                    lstItem.Add(item);
                    if (count > 0) Register.Value += ", ";
                    Register.Value += item.Text;
                    count++;
                }
            }
            if (lstItem.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please Check Register')", true);
                return;
            }

            // Exam Year & Session Value Get.
            ExamYear = ddlExamYear.SelectedItem.Text;
            ExamSession = ddlSession.SelectedItem.Text;

            dt = new DataTable();
            switch (ddlValidityReport.SelectedIndex)
            {
                case 1:  ///Foil & CF Roll No. Mismatch
                    dt = BindFoilCFMismatch();
                    break;
                case 2:  ///Duplicate Page No. In Foil (Within Reg. No.)
                    dt = BindDuplicatePage();
                    break;
                case 3:  ///Duplicate Roll No. In Foil (Within Reg. No.)
                    dt = BindWrongRollNo();
                    break;
                case 4:  ///Missing Page No. In Foil (Within Reg. No.)
                    dt = BindMissingPageNo();
                    break;
                case 5:  ///Wrong Roll No. Entry
                    dt = BindWrongRollNo();
                    break;
            }

            if (dt.Rows.Count <= 0)
            {
                lblMsg.Text = "Data Not Found!";
                divGrid.Visible = false;
                return;
            }

            grdHeader.Text = ddlValidityReport.SelectedItem.Text;
            lblTotalRecord.Text = dt.Rows.Count.ToString();
            divGrid.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Closedrop", "Closedrop(1)", true);
    }

    DataTable BindWrongRollNo()
    {
        PlRpt = new PlReports()
        {
            RptInd = ddlValidityReport.SelectedIndex == 3 ? 3 : 5, //RptInd: 3 For Duplicate Roll No / 5 For Roll Length > 5.
            ExamYear = ddlExamYear.SelectedItem.Text,
            Session = ddlSession.SelectedIndex.ToString(),
            AllCtrls = cbSelectAll.Checked ? true : false,
            Ctrls = cbSelectAll.Checked ? "" : Register.Value,
        };
        dt = new DataTable();
        dt = BlRpt.ErroInDuplicateEntry(PlRpt);
        if (dt.Rows.Count > 0)
        {
            gvWrongRollNo.DataSource = dt;
            gvWrongRollNo.DataBind();
            tblGvWrongRollEntry.Visible = true;
        }
        return dt;
    }
    DataTable BindFoilCFMismatch()
    {
        PlRpt = new PlReports()
        {
            RptInd = 1,
            ExamYear = ddlExamYear.SelectedItem.Text,
            Session = ddlSession.SelectedIndex.ToString(),
            AllCtrls = cbSelectAll.Checked ? true : false,
            Ctrls = cbSelectAll.Checked ? "" : Register.Value,
        };
        dt = new DataTable();
        dt = BlRpt.ErroInDuplicateEntry(PlRpt);
        if (dt.Rows.Count > 0)
        {
            gvFoilCFMismatch.DataSource = dt;
            gvFoilCFMismatch.DataBind();
            tblGvFoilCFMismatch.Visible = true;
        }
        return dt;
    }
    DataTable BindDuplicatePage()
    {

        PlRpt = new PlReports()
        {
            RptInd = 2,
            ExamYear = ddlExamYear.SelectedItem.Text,
            Session = ddlSession.SelectedIndex.ToString(),
            AllCtrls = cbSelectAll.Checked ? true : false,
            Ctrls = cbSelectAll.Checked ? "" : Register.Value,
        };
        dt = new DataTable();
        dt = BlRpt.ErroInDuplicateEntry(PlRpt);
        if (dt.Rows.Count > 0)
        {
            gvFoilCF.DataSource = dt;
            gvFoilCF.DataBind();
            tblGvFoilCF.Visible = true;
            tblGvWrongRollEntry.Visible = false;
        }
        return dt;
    }
    DataTable BindMissingPageNo()
    {
        PlRpt = new PlReports()
        {
            RptInd = 4,
            ExamYear = ddlExamYear.SelectedItem.Text,
            Session = ddlSession.SelectedIndex.ToString(),
            AllCtrls = cbSelectAll.Checked ? true : false,
            Ctrls = cbSelectAll.Checked ? "" : Register.Value,
        };
        dt = new DataTable();
        dt = BlRpt.ErroInDuplicateEntry(PlRpt);
        if (dt.Rows.Count > 0)
        {
            gvMissingPage.DataSource = dt;
            gvMissingPage.DataBind();
            tblGvMissingPage.Visible = true;
        }
        return dt;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        AllClear();
    }
    protected void lnkexcel_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "GenerateExcel", "GenerateExcel();", true);
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "PrintDiv", "PrintDiv()", true);
    }

    static int RowIndex;
    protected void gvFoilCF_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            RowIndex = 0; divRollGv.Visible = false;
            if (e.CommandName == "ShowData")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                RowIndex = row.RowIndex;
                txtRegNo.Text = row.Cells[0].Text;

                divDuplicateRoll.Visible = divDuplicatePage.Visible = false;

                if (ddlValidityReport.SelectedIndex == 2)
                {
                    divDuplicatePage.Visible = true;
                    txtFoilPage.Text = row.Cells[1].Text;
                    txtFoilPageFrom.Text = row.Cells[1].Text;
                }

                else if (ddlValidityReport.SelectedIndex == 3 ||
                         ddlValidityReport.SelectedIndex == 5)
                {
                    divDuplicateRoll.Visible = divRollGv.Visible = true;
                    txtRollTo.Text = row.Cells[1].Text;
                    txtRollFrom.Text = row.Cells[1].Text;
                    string s = row.Cells[2].Text;
                    s = s.Substring(s.LastIndexOf("/"), s.LastIndexOf(".") - s.LastIndexOf("/"));
                    s = s.Substring(s.IndexOf("_") + 1, s.LastIndexOf("_") - s.IndexOf("_") - 1);

                    plReg.Ind = 13;
                    plReg.RegNo = Convert.ToInt64(txtRegNo.Text);
                    plReg.PageNo = Convert.ToInt32(s);
                    DataSet ds = blReg.ShowBothImages(plReg);
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        gvRollOfPage.DataSource = ds.Tables[0];
                        gvRollOfPage.DataBind();
                    }
                }
                imgFoil.ImageUrl =
                lblImgPath.Text = row.Cells[2].Text;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "modalOnEdit", "modalOnEdit()", true);
            }
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + "There Is Problem Because Of - " + ex.Message + "')", true);
        }
    }
    protected void cbSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        GridsBlank();
        if (cbSelectAll.Checked)
        {
            foreach (ListItem item in cbRegNo.Items)
            {
                item.Selected = true;
                item.Enabled = false;
            }
        }
        else
        {
            foreach (ListItem item in cbRegNo.Items)
            {
                item.Selected = false;
                item.Enabled = true;
            }
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Closedrop", "Closedrop(2)", true);
    }

    static List<int> UpdatedList;
    protected void btnUpdatePage_Click(object sender, EventArgs e)
    {
        try
        {
            dt = new DataTable();
            lblMsg.Text = "";
            if (ddlValidityReport.SelectedIndex == 2) //For Update Wrong Entry Of Page No.
            {
                PlRegister PlReg = new PlRegister()
                {
                    Ind = 26,
                    RegNo = Convert.ToInt64(txtRegNo.Text),
                    PageNoFrom = Convert.ToInt32(gvFoilCF.Rows[RowIndex].Cells[1].Text),
                    PageNoTo = Convert.ToInt32(txtFoilPage.Text),
                    FilePath = lblImgPath.Text,
                };
                dt = blReg.UpdateDuplicatePage(PlReg);
                if (dt.Rows.Count > 0)
                    lblMsg.Text = "Page No. Updated Successfully!";
            }

            else if (ddlValidityReport.SelectedIndex == 3 || //For Update Wrong Entry Of Roll No. 
                     ddlValidityReport.SelectedIndex == 5) //Roll No Length Is Grater Than 5
            {
                PlRegister PlReg = new PlRegister()
                {
                    Ind = 28,
                    RegNo = Convert.ToInt64(txtRegNo.Text),
                    PageNoFrom = Convert.ToInt32(txtRollFrom.Text), //Convert.ToInt32(gvDuplicateRoll.Rows[RowIndex].Cells[1].Text),
                    PageNoTo = Convert.ToInt32(txtRollTo.Text),
                    FilePath = lblImgPath.Text,
                };
                dt = blReg.UpdateDuplicateRoll(PlReg);
                if (dt.Rows.Count > 0)
                    lblMsg.Text = "Roll No. Updated Successfully!";
            }

            if (dt.Rows.Count > 0)
            {
                UpdatedList.Add(RowIndex);
                foreach (int item in UpdatedList)
                {
                    gvWrongRollNo.Rows[item].BackColor = Color.FromName("#ffff99");
                }
                //btnShow_Click(sender, e);
            }
            else Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Page No. Not Update!')", true);

        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + "There Is Problem Because Of - " + ex.Message + "')", true);
        }

    }
    void AllClear()
    {
        ddlExamYear.SelectedIndex = ddlSession.SelectedIndex = ddlValidityReport.SelectedIndex = 0;
        foreach (ListItem item in cbRegNo.Items)
        {
            item.Selected = false;
        }
        gvFoilCF.DataSource = gvWrongRollNo.DataSource = gvMissingPage.DataSource = gvFoilCFMismatch.DataSource = null;
        gvFoilCF.DataBind(); gvWrongRollNo.DataBind(); gvMissingPage.DataBind(); gvFoilCFMismatch.DataBind();

        divGrid.Visible = tblGvFoilCF.Visible = tblGvWrongRollEntry.Visible = false;

        UpdatedList = new List<int>();
    }
    void GridsBlank()
    {
        gvWrongRollNo.DataSource = gvRollOfPage.DataSource = gvMissingPage.DataSource = gvFoilCFMismatch.DataSource = gvFoilCF.DataSource = null;
        gvWrongRollNo.DataBind(); gvRollOfPage.DataBind(); gvMissingPage.DataBind(); gvFoilCFMismatch.DataBind(); gvFoilCF.DataBind();

        tblGvFoilCF.Visible = tblGvFoilCFMismatch.Visible = tblGvMissingPage.Visible = tblGvWrongRollEntry.Visible = false;

        divGrid.Visible = false;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Closedrop", "Closedrop(1)", true);
    }
    void Validation(string id)
    {

        bool state = false;
        switch (id)
        {
            case "btnShow":

                foreach (ListItem item in cbRegNo.Items)
                {
                    if (item.Selected == true)
                    {
                        state = true;
                        break;
                    }  
                }
                if (!state)
                {
                    // btnRegToggle.Attributes["class"] += " has-error";
                }
                if (ddlExamYear.SelectedIndex == 0)
                {
                    ddlExamYear.CssClass += " has-error";
                }
                if (ddlSession.SelectedIndex == 0)
                {
                    ddlSession.CssClass += " has-error";
                }
                if (ddlValidityReport.SelectedIndex == 0)
                {
                    ddlValidityReport.CssClass += " has-error";
                }

                break;

            case "ddlSessionChang":

                if (ddlExamYear.SelectedIndex == 0)
                {

                }

                break;

            default:
                //ddlExamYear.BorderColor
                break;
        }

    }
}