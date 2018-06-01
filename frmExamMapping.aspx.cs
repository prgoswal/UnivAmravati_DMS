using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmExamMapping : System.Web.UI.Page
{
    #region Declaration

    BLExamMapping objExamMappBl = new BLExamMapping();
    PLExamMapping objExamMappPl;

    DataTable VsdtUniExam
    {
        get { return (DataTable)ViewState["dtUniExam"]; }
        set { ViewState["dtUniExam"] = value; }
    }

    DataTable VsdtDMSExam
    {
        get { return (DataTable)ViewState["dtDMSExam"]; }
        set { ViewState["dtDMSExam"] = value; }
    }

    DataTable VsdtUniAndDMSExam
    {
        get { return (DataTable)ViewState["dtUniAndDMSExam"]; }
        set { ViewState["dtUniAndDMSExam"] = value; }
    }

    DataTable VsdtFinalUniAndDMSExam
    {
        get { return (DataTable)ViewState["dtFinalUniAndDMSExam"]; }
        set { ViewState["dtFinalUniAndDMSExam"] = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        if (!IsPostBack)
        {
            BindAllDDL();
        }

        ddlExamYear.Focus();
    }

    /// <summary>
    /// Bind Exam Mapping (ExamYear,ExamSession And ExamFaculty) DropDownList
    /// </summary>
    void BindAllDDL()
    {
        try
        {
            objExamMappPl = new PLExamMapping();
            objExamMappPl.Ind = 2;
            DataSet dsExamMapp = objExamMappBl.BindAllDDl(objExamMappPl);
            if (dsExamMapp.Tables.Count > 0)
            {
                DataTable dtExamYear = dsExamMapp.Tables[0];
                DataTable dtSession = dsExamMapp.Tables[1];
                DataTable dtExamFaculty = dsExamMapp.Tables[2];

                if (dtExamYear.Rows.Count > 0)
                {
                    ddlExamYear.DataSource = dtExamYear;
                    ddlExamYear.DataValueField = "ItemID"; // To filter Exam Name 
                    ddlExamYear.DataTextField = "ItemDesc";
                    ddlExamYear.DataBind();
                    ddlExamYear.Items.Insert(0, new ListItem("-- Select Year --", "0"));
                }

                if (dtSession.Rows.Count > 0)
                {
                    ddlSession.DataSource = dtSession;
                    ddlSession.DataValueField = "ItemID"; // To filter Exam Name 
                    ddlSession.DataTextField = "ItemDesc";
                    ddlSession.DataBind();
                    ddlSession.Items.Insert(0, new ListItem("-- Select Session --", "0"));
                }

                if (dtExamFaculty.Rows.Count > 0)
                {
                    ddlExamFaculty.DataSource = dtExamFaculty;
                    ddlExamFaculty.DataValueField = "ItemID"; // To filter Exam Name 
                    ddlExamFaculty.DataTextField = "ItemDesc";
                    ddlExamFaculty.DataBind();
                    ddlExamFaculty.Items.Insert(0, new ListItem("-- Select Session --", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    /// <summary>
    ///    Show Available And Alloted Exam According To ExamYear, ExamSession And ExamFaculty
    /// </summary>
    /// /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShow_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        universityExamModal.Style.Add("display", "none");
        dmsExamModal.Style.Add("display", "none");
        universityRecordsModal.Style.Add("display", "none");

        objExamMappPl = new PLExamMapping();
        objExamMappPl.Ind = 11;
        objExamMappPl.ExamYear = Convert.ToInt32(ddlExamYear.SelectedItem.Text);
        objExamMappPl.ExamSession = ddlSession.SelectedItem.Text;
        objExamMappPl.ExamSessionID = Convert.ToInt32(ddlSession.SelectedValue);
        objExamMappPl.ExamFacultyCD = Convert.ToInt32(ddlExamFaculty.SelectedValue);
        DataSet dsFacultyMapp = objExamMappBl.GetAllUniAndDMSExam(objExamMappPl);
        if (dsFacultyMapp.Tables.Count > 0)
        {
            //Available
            VsdtUniExam = dsFacultyMapp.Tables[0];
            VsdtDMSExam = dsFacultyMapp.Tables[1];

            //Alloted
            VsdtFinalUniAndDMSExam = dsFacultyMapp.Tables[2];

            if (VsdtUniExam.Rows.Count <= 0 && VsdtDMSExam.Rows.Count <= 0 && VsdtFinalUniAndDMSExam.Rows.Count <= 0)
            {
                lblMsg.Text = "Record Not Found.";
                ddlExamYear.Focus();
                return;
            }

            if (VsdtDMSExam.Rows.Count > 0)
            {
                grdDMSUnMappedExam.DataSource = VsdtDMSExam;
                grdDMSUnMappedExam.DataBind();
                divDMSUnMappedExam.Style.Add("height", "200px");
                tblDMSGridHeader.Visible = true;
                btnShowDMSExam.Enabled = true;
            }

            if (VsdtUniExam.Rows.Count > 0)
            {
                grdUniversityUnMappedExam.DataSource = VsdtUniExam;
                grdUniversityUnMappedExam.DataBind();
                divUnivUnMappedExam.Style.Add("height", "200px");
                tblUniversityGridHeader.Visible = true;
                btnShowUnivExam.Enabled = true;
            }

            if (VsdtDMSExam.Rows.Count > 0 || VsdtUniExam.Rows.Count > 0)
                divUnMapped.Visible = true;
            else
                divUnMapped.Visible = false;

            if (VsdtFinalUniAndDMSExam.Rows.Count > 0)
            {
                VsdtFinalUniAndDMSExam.Columns["ExamYear"].SetOrdinal(0);
                VsdtFinalUniAndDMSExam.Columns["ExamSession"].SetOrdinal(1);
                VsdtFinalUniAndDMSExam.Columns["ExamSessionID"].SetOrdinal(2);
                VsdtFinalUniAndDMSExam.Columns["FacultyCD"].SetOrdinal(3);
                VsdtFinalUniAndDMSExam.Columns["UnivExamName"].SetOrdinal(4);
                VsdtFinalUniAndDMSExam.Columns["DMSExamCD"].SetOrdinal(5);
                VsdtFinalUniAndDMSExam.Columns["DMSExamName"].SetOrdinal(6);

                grdFinalUniAndDMSMappedExam.DataSource = VsdtFinalUniAndDMSExam;
                grdFinalUniAndDMSMappedExam.DataBind();
                divMapped.Visible = true;
            }
            else
                divMapped.Visible = false;

            //if (grdDMSUnMappedExam.Rows.Count <= 0)
            //    btnPrintUnivExam.Enabled = true;

            btnAdd.Visible = true;
            ddlExamYear.Enabled = ddlSession.Enabled = ddlExamFaculty.Enabled = btnShow.Enabled = false;
            btnSave.Enabled = btnClearAll.Enabled = true;
        }
        else
        {
            lblMsg.Text = "Record Not Found.";
            return;
        }
    }

    /// <summary>
    /// Add Both Un - Mapped (University And DMS) Exam In Mapped Exam GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        universityExamModal.Style.Add("display", "none");
        dmsExamModal.Style.Add("display", "none");
        universityRecordsModal.Style.Add("display", "none");

        VsdtUniAndDMSExam = null;
        VsdtUniAndDMSExam = new DataTable();

        bool uniCheckedOrNot = false;
        bool dmsCheckedOrNot = false;

        DataTable dtDMSRowIndex = new DataTable();

        if (VsdtUniAndDMSExam == null || VsdtUniAndDMSExam.Rows.Count <= 0)
        {
            VsdtUniAndDMSExam = new DataTable();
            VsdtUniAndDMSExam.Columns.Add("ExamYear", typeof(int));
            VsdtUniAndDMSExam.Columns.Add("ExamSession", typeof(string));
            VsdtUniAndDMSExam.Columns.Add("ExamSessionID", typeof(int));
            VsdtUniAndDMSExam.Columns.Add("FacultyCD", typeof(int));
            VsdtUniAndDMSExam.Columns.Add("UnivExamName", typeof(string));
            VsdtUniAndDMSExam.Columns.Add("DMSExamCD", typeof(int));
            VsdtUniAndDMSExam.Columns.Add("DMSExamName", typeof(string));
        }

        foreach (GridViewRow gvUniRow in grdUniversityUnMappedExam.Rows)
        {
            CheckBox chkUniExamFaculty = gvUniRow.FindControl("chkUniExam") as CheckBox;

            if (chkUniExamFaculty.Checked == true)
            {
                Label lblUnivExamName = gvUniRow.FindControl("lblUnivExamName") as Label;
                Label lblTotalRecord = gvUniRow.FindControl("lblTotalRecord") as Label;

                DataRow drUniAndDMSExamFaculty = VsdtUniAndDMSExam.NewRow();
                drUniAndDMSExamFaculty["ExamYear"] = Convert.ToInt32(ddlExamYear.SelectedItem.Text);
                drUniAndDMSExamFaculty["ExamSession"] = ddlSession.SelectedItem.Text;
                drUniAndDMSExamFaculty["ExamSessionID"] = Convert.ToInt32(ddlSession.SelectedValue);
                drUniAndDMSExamFaculty["FacultyCD"] = Convert.ToInt32(ddlExamFaculty.SelectedValue);
                drUniAndDMSExamFaculty["UnivExamName"] = lblUnivExamName.Text;

                foreach (GridViewRow gvDMSRow in grdDMSUnMappedExam.Rows)
                {
                    CheckBox chkDMSExamFaculty = gvDMSRow.FindControl("chkDMSExam") as CheckBox;
                    if (chkDMSExamFaculty.Checked == true)
                    {
                        Label lblCtrl = gvDMSRow.FindControl("lblCtrl") as Label;
                        Label lblDMSExamName = gvDMSRow.FindControl("lblExamName") as Label;

                        drUniAndDMSExamFaculty["DMSExamCD"] = Convert.ToInt32(lblCtrl.Text);
                        drUniAndDMSExamFaculty["DMSExamName"] = lblDMSExamName.Text;
                        VsdtUniAndDMSExam.Rows.Add(drUniAndDMSExamFaculty);

                        DataRow[] r1 = VsdtUniExam.Select("ExamName='" + lblUnivExamName.Text + "'");
                        if (r1.Count() > 0)
                        {
                            int rowIndex1 = VsdtUniExam.Rows.IndexOf(r1[0]);
                            VsdtUniExam.Rows.RemoveAt(rowIndex1);
                            grdUniversityUnMappedExam.DataSource = VsdtUniExam;
                            grdUniversityUnMappedExam.DataBind();
                        }

                        DataRow[] r2 = VsdtDMSExam.Select("ExamName='" + lblDMSExamName.Text + "'");
                        if (r2.Count() > 0)
                        {
                            int rowIndex = VsdtDMSExam.Rows.IndexOf(r2[0]);

                            if (!dtDMSRowIndex.Columns.Contains("DMSExamName"))
                                dtDMSRowIndex.Columns.Add("DMSExamName", typeof(string));

                            DataRow drDMSRowIndex = dtDMSRowIndex.NewRow();
                            drDMSRowIndex["DMSExamName"] = lblDMSExamName.Text;
                            dtDMSRowIndex.Rows.Add(drDMSRowIndex);
                        }
                        dmsCheckedOrNot = true;
                    }
                }
                uniCheckedOrNot = true;
            }
        }

        if (uniCheckedOrNot == false)
        {
            lblMsg.Text = "Select Atleast One University Exam.";
            return;
        }

        if (dmsCheckedOrNot == false)
        {
            lblMsg.Text = "Select Atleast One DMS Exam.";
            VsdtUniAndDMSExam = null;
            VsdtUniAndDMSExam = new DataTable();
            return;
        }

        if (uniCheckedOrNot == true && dmsCheckedOrNot == true)
        {
            foreach (DataRow dmsRow in dtDMSRowIndex.Rows)
            {
                DataRow[] r = VsdtDMSExam.Select("ExamName='" + dmsRow["DMSExamName"] + "'");
                if (r.Count() > 0)
                {
                    int rowIndex = VsdtDMSExam.Rows.IndexOf(r[0]);
                    VsdtDMSExam.Rows.RemoveAt(rowIndex);
                    grdDMSUnMappedExam.DataSource = VsdtDMSExam;
                    grdDMSUnMappedExam.DataBind();
                }
            }

            if (VsdtFinalUniAndDMSExam == null || VsdtFinalUniAndDMSExam.Rows.Count <= 0)
            {
                grdFinalUniAndDMSMappedExam.DataSource = VsdtFinalUniAndDMSExam = VsdtUniAndDMSExam;
                grdFinalUniAndDMSMappedExam.DataBind();
            }
            else
            {
                foreach (DataRow gvFinalRow in VsdtUniAndDMSExam.Rows)
                {
                    DataRow dr = VsdtFinalUniAndDMSExam.NewRow();
                    dr["ExamYear"] = gvFinalRow["ExamYear"];
                    dr["ExamSession"] = gvFinalRow["ExamSession"];
                    dr["ExamSessionID"] = gvFinalRow["ExamSessionID"];
                    dr["FacultyCD"] = gvFinalRow["FacultyCD"];
                    dr["UnivExamName"] = gvFinalRow["UnivExamName"];
                    dr["DMSExamCD"] = gvFinalRow["DMSExamCD"];
                    dr["DMSExamName"] = gvFinalRow["DMSExamName"];
                    VsdtFinalUniAndDMSExam.Rows.Add(dr);
                }
                grdFinalUniAndDMSMappedExam.DataSource = VsdtFinalUniAndDMSExam;
                grdFinalUniAndDMSMappedExam.DataBind();
            }
        }

        if (grdUniversityUnMappedExam.Rows.Count <= 0 && grdDMSUnMappedExam.Rows.Count <= 0)
        {
            btnAdd.Visible = false;
            divUnMapped.Visible = false;
        }

        if (grdUniversityUnMappedExam.Rows.Count <= 0)
        {
            tblUniversityGridHeader.Visible = false;
            btnShowUnivExam.Enabled = true;
        }

        if (grdDMSUnMappedExam.Rows.Count <= 0)
        {
            tblDMSGridHeader.Visible = false;
            btnShowDMSExam.Enabled = false;
        }

        if (grdFinalUniAndDMSMappedExam.Rows.Count > 0)
        {
            divMapped.Visible = true;
        }
    }

    /// <summary>
    /// Delete Mapped Exam Then Add In Un - Mapped (University And DMS) Exam GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdFinalUniAndDMSMappedExam_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        universityExamModal.Style.Add("display", "none");
        dmsExamModal.Style.Add("display", "none");
        universityRecordsModal.Style.Add("display", "none");

        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "DeleteRow")
        {
            DataTable dtFinalUniAndDMSExam = VsdtFinalUniAndDMSExam;
            DataRow drFinalUniAndDMSExam = dtFinalUniAndDMSExam.Rows[rowIndex];

            string UnivFacultyName = drFinalUniAndDMSExam["UnivExamName"].ToString();

            DataRow[] drUniv = VsdtUniExam.Select("ExamName='" + UnivFacultyName + "'");

            if (drUniv.Count() <= 0)
            {
                DataRow drUniExam = VsdtUniExam.NewRow();
                drUniExam["RecordCount"] = 0;
                drUniExam["ExamName"] = UnivFacultyName;
                VsdtUniExam.Rows.Add(drUniExam);
                VsdtUniExam.DefaultView.Sort = "ExamName Asc";
                grdUniversityUnMappedExam.DataSource = VsdtUniExam;
                grdUniversityUnMappedExam.DataBind();
            }

            int dmsExamCD = Convert.ToInt32(drFinalUniAndDMSExam["DMSExamCD"]);
            string dmsExamName = drFinalUniAndDMSExam["DMSExamName"].ToString();

            DataRow[] dr = VsdtDMSExam.Select("ExamName='" + dmsExamName + "'");

            if (dr.Count() <= 0)
            {
                DataRow drDMSExamFaculty = VsdtDMSExam.NewRow();
                drDMSExamFaculty["Ctrl"] = dmsExamCD;
                drDMSExamFaculty["ExamName"] = dmsExamName;
                VsdtDMSExam.Rows.Add(drDMSExamFaculty);
                VsdtDMSExam.DefaultView.Sort = "ExamName Asc";
                grdDMSUnMappedExam.DataSource = VsdtDMSExam;
                grdDMSUnMappedExam.DataBind();
            }

            VsdtFinalUniAndDMSExam.Rows.RemoveAt(rowIndex);

            SaveExamMappingOnDeleteBtn();
            btnShow_Click(sender, e);
            grdFinalUniAndDMSMappedExam.DataSource = VsdtFinalUniAndDMSExam;
            grdFinalUniAndDMSMappedExam.DataBind();


            if (grdUniversityUnMappedExam.Rows.Count > 0 || grdDMSUnMappedExam.Rows.Count > 0)
            {
                btnAdd.Visible = true;
                divUnMapped.Visible = true;
            }

            if (grdUniversityUnMappedExam.Rows.Count > 0)
            {
                tblUniversityGridHeader.Visible = true;
                btnShowUnivExam.Enabled = true;
            }

            if (grdDMSUnMappedExam.Rows.Count > 0)
            {
                tblDMSGridHeader.Visible = true;
                btnShowDMSExam.Enabled = true;
            }

            if (grdFinalUniAndDMSMappedExam.Rows.Count <= 0)
            {
                divMapped.Visible = false;
            }
        }
    }

    /// <summary>
    /// Save Mapped Exam GridView Records
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            btnShowUnivExam.Enabled = btnShowDMSExam.Enabled = false;

            universityExamModal.Style.Add("display", "none");
            dmsExamModal.Style.Add("display", "none");
            universityRecordsModal.Style.Add("display", "none");

            if (VsdtFinalUniAndDMSExam == null || VsdtFinalUniAndDMSExam.Rows.Count <= 0)
            {
                lblMsg.Text = "Select Atleast One University And  One DMS Exam, Then Press Add Button.";
                return;
            }

            objExamMappPl = new PLExamMapping();
            objExamMappPl.Ind = 12;
            objExamMappPl.ExamYear = Convert.ToInt32(ddlExamYear.SelectedItem.Text);
            objExamMappPl.ExamSession = ddlSession.SelectedItem.Text;
            objExamMappPl.ExamSessionID = Convert.ToInt32(ddlSession.SelectedValue);
            objExamMappPl.ExamFacultyCD = Convert.ToInt32(ddlExamFaculty.SelectedValue);

            objExamMappPl.TblExamMapp = VsdtFinalUniAndDMSExam;
            DataTable dtSave = objExamMappBl.InsertUniAndDMSExamMappingData(objExamMappPl);
            if (dtSave.Rows.Count > 0)
            {
                if (dtSave.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    ClearAll();
                    lblMsg.Text = "Record Save Successfully.";
                    lblMsg.Style.Add("color", "#1e983b");
                }
                else if (dtSave.Rows[0]["ReturnInd"].ToString() == "0")
                {
                    lblMsg.Text = "Record Not Save Successfully, Please Try Again.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    /// <summary>
    /// Save Mapped Exam GridView Records On Row Deleting
    /// </summary>
    void SaveExamMappingOnDeleteBtn()
    {
        objExamMappPl = new PLExamMapping();
        objExamMappPl.Ind = 12;
        objExamMappPl.ExamYear = Convert.ToInt32(ddlExamYear.SelectedItem.Text);
        objExamMappPl.ExamSession = ddlSession.SelectedItem.Text;
        objExamMappPl.ExamSessionID = Convert.ToInt32(ddlSession.SelectedValue);
        objExamMappPl.ExamFacultyCD = Convert.ToInt32(ddlExamFaculty.SelectedValue);

        objExamMappPl.TblExamMapp = VsdtFinalUniAndDMSExam;
        DataTable dtSave = objExamMappBl.InsertUniAndDMSExamMappingData(objExamMappPl);

        universityExamModal.Style.Add("display", "none");
        dmsExamModal.Style.Add("display", "none");
        universityRecordsModal.Style.Add("display", "none");
        btnShowUnivExam.Enabled = btnShowDMSExam.Enabled = false;
    }

    protected void btnClearAll_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    /// <summary>
    /// Clear All Controls
    /// </summary>
    void ClearAll()
    {
        lblMsg.Text = "";

        btnAdd.Visible = divMapped.Visible = divUnMapped.Visible = false;
        ddlExamYear.Enabled = ddlSession.Enabled = ddlExamFaculty.Enabled = btnShow.Enabled = true;
        btnSave.Enabled = btnClearAll.Enabled = btnShowUnivExam.Enabled = btnShowDMSExam.Enabled = false;
        ddlExamYear.ClearSelection();
        ddlSession.ClearSelection();
        ddlExamFaculty.ClearSelection();
        VsdtUniExam = VsdtDMSExam = VsdtUniAndDMSExam = VsdtFinalUniAndDMSExam = null;
        grdDMSUnMappedExam.DataSource = grdUniversityUnMappedExam.DataSource = grdFinalUniAndDMSMappedExam.DataSource =
            VsdtUniExam = VsdtDMSExam = VsdtUniAndDMSExam = VsdtFinalUniAndDMSExam = new DataTable();
        grdDMSUnMappedExam.DataBind(); grdUniversityUnMappedExam.DataBind(); grdFinalUniAndDMSMappedExam.DataBind();
        divUnivUnMappedExam.Style.Add("height", "0px");
        divDMSUnMappedExam.Style.Add("height", "0px");
        universityExamModal.Style.Add("display", "none");
        dmsExamModal.Style.Add("display", "none");
        universityRecordsModal.Style.Add("display", "none");
        tblUniversityGridHeader.Visible = tblDMSGridHeader.Visible = false;
        ddlExamYear.Focus();
    }

    /// <summary>
    /// Show University Exam Modal Popup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShowUnivExam_Click(object sender, EventArgs e)
    {
        grdPrintUnivExam.DataSource = new DataTable();
        grdPrintUnivExam.DataBind();

        lblUnivExamYear.Text = ddlExamYear.SelectedItem.Text;
        lblUnivExamSession.Text = ddlSession.SelectedItem.Text;
        lblUnivExamFaculty.Text = ddlExamFaculty.SelectedItem.Text;

        DataTable dtUnivExam = new DataTable();
        dtUnivExam.Columns.Add("ExamName", typeof(string));
        dtUnivExam.Columns.Add("RecordCount", typeof(string));

        foreach (GridViewRow row in grdUniversityUnMappedExam.Rows)
        {
            DataRow drUnivExam = dtUnivExam.NewRow();
            drUnivExam["ExamName"] = (row.FindControl("lblUnivExamName") as Label).Text;
            drUnivExam["RecordCount"] = (row.FindControl("lnkUnivRecords") as LinkButton).Text;
            dtUnivExam.Rows.Add(drUnivExam);
        }

        grdPrintUnivExam.DataSource = dtUnivExam;
        grdPrintUnivExam.DataBind();

        universityExamModal.Style.Add("display", "block");
        dmsExamModal.Style.Add("display", "none");
        universityRecordsModal.Style.Add("display", "none");
    }

    /// <summary>
    /// Show DMS Exam Modal Popup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShowDMSExam_Click(object sender, EventArgs e)
    {
        grdDMSExam.DataSource = new DataTable();
        grdDMSExam.DataBind();

        lblDMSExamYear.Text = ddlExamYear.SelectedItem.Text;
        lblDMSExamSession.Text = ddlSession.SelectedItem.Text;
        lblDMSExamFaculty.Text = ddlExamFaculty.SelectedItem.Text;

        DataTable dtDMSExam = new DataTable();
        dtDMSExam.Columns.Add("ExamName", typeof(string));

        foreach (GridViewRow row in grdDMSUnMappedExam.Rows)
        {
            DataRow drDMSExam = dtDMSExam.NewRow();
            drDMSExam["ExamName"] = (row.FindControl("lblExamName") as Label).Text;
            dtDMSExam.Rows.Add(drDMSExam);
        }

        grdDMSExam.DataSource = dtDMSExam;
        grdDMSExam.DataBind();

        dmsExamModal.Style.Add("display", "block");
        universityExamModal.Style.Add("display", "none");
        universityRecordsModal.Style.Add("display", "none");
    }

    /// <summary>
    /// Show University Records Popup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdUniversityUnMappedExam_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        grdUnivRecords.DataSource = new DataTable();
        grdUnivRecords.DataBind();

        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "UniversityRecords")
        {
            DataTable dtUnivExam = VsdtUniExam;
            DataRow drUnivExam = dtUnivExam.Rows[rowIndex];

            objExamMappPl = new PLExamMapping();
            objExamMappPl.Ind = 13;
            objExamMappPl.ExamYear = Convert.ToInt32(ddlExamYear.SelectedItem.Text);
            objExamMappPl.ExamSession = ddlSession.SelectedItem.Text;
            objExamMappPl.ExamSessionID = Convert.ToInt32(ddlSession.SelectedValue);
            objExamMappPl.ExamFacultyCD = Convert.ToInt32(ddlExamFaculty.SelectedValue);
            objExamMappPl.ExamName = drUnivExam["ExamName"].ToString();
            DataTable dtUnivRecords = objExamMappBl.GetUneversityRecords(objExamMappPl);
            if (dtUnivRecords.Rows.Count > 0)
            {
                lblUniRecExamYear.Text = ddlExamYear.SelectedItem.Text;
                lblUniRecExamSession.Text = ddlSession.SelectedItem.Text;
                lblUniRecExamFaculty.Text = ddlExamFaculty.SelectedItem.Text;
                lblUniRecExamName.Text = drUnivExam["ExamName"].ToString();

                grdUnivRecords.DataSource = dtUnivRecords;
                grdUnivRecords.DataBind();

                universityRecordsModal.Style.Add("display", "block");
                universityExamModal.Style.Add("display", "none");
                dmsExamModal.Style.Add("display", "none");
            }
        }
    }
}