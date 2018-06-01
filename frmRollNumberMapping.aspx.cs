using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmRollNumberMapping : System.Web.UI.Page
{
    #region Declaration

    BLRollNumberMapping objRollNumberMappBl = new BLRollNumberMapping();
    PLRollNumberMapping objRollNumberMappPl;

    DataTable VsdtUniAndDMSExam
    {
        get { return (DataTable)ViewState["dtUniAndDMSExam"]; }
        set { ViewState["dtUniAndDMSExam"] = value; }
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
    /// Bind Roll Number Mapping (ExamYear,ExamSession And ExamFaculty) DropDownList
    /// </summary>
    void BindAllDDL()
    {
        try
        {
            objRollNumberMappPl = new PLRollNumberMapping();
            objRollNumberMappPl.Ind = 2;
            DataSet dsExamMapp = objRollNumberMappBl.BindAllDDl(objRollNumberMappPl);
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
            lblMsg.Style.Add("color", "red");
        }
    }

    /// <summary>
    /// Show Roll Number Mapping Status According To ExamYear, ExamSession And ExamFaculty
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShow_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        missingPunchedRecordModal.Style.Add("display", "none");
        extraPunchedRecordModal.Style.Add("display", "none");

        objRollNumberMappPl = new PLRollNumberMapping();
        objRollNumberMappPl.Ind = 20;
        objRollNumberMappPl.ExamYear = Convert.ToInt32(ddlExamYear.SelectedItem.Text);
        objRollNumberMappPl.ExamSession = ddlSession.SelectedItem.Text;
        objRollNumberMappPl.ExamSessionID = Convert.ToInt32(ddlSession.SelectedValue);
        objRollNumberMappPl.ExamFacultyCD = Convert.ToInt32(ddlExamFaculty.SelectedValue);
        DataSet dsRollNumberMapp = objRollNumberMappBl.GetAllUniAndDMSExam(objRollNumberMappPl);
        if (dsRollNumberMapp.Tables.Count > 0)
        {
            VsdtUniAndDMSExam = dsRollNumberMapp.Tables[0];
            if (VsdtUniAndDMSExam.Rows.Count > 0)
            {
                grdUniAndDMSRollNumberMapp.DataSource = VsdtUniAndDMSExam;
                grdUniAndDMSRollNumberMapp.DataBind();
                divMapped.Visible = true;

                ddlExamYear.Enabled = ddlSession.Enabled = ddlExamFaculty.Enabled = btnShow.Enabled = false;
                btnClearAll.Enabled = true;
            }
            else
            {
                divMapped.Visible = false;

                lblMsg.Text = "Record Not Found.";
                lblMsg.Style.Add("color", "red");
                return;
            }
        }
        else
        {
            lblMsg.Text = "Record Not Found.";
            lblMsg.Style.Add("color", "red");
            return;
        }
    }

    /// <summary>
    /// Mapped Roll Number Mapping Status (Change Mapped Button Color According To Partial Match), 
    /// Show Missing Punched Records Modal Popup And Show Extra Punched Records Modal Popup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdUniAndDMSRollNumberMapp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = "";
        missingPunchedRecordModal.Style.Add("display", "none");
        extraPunchedRecordModal.Style.Add("display", "none");

        int rowIndex = Convert.ToInt32(e.CommandArgument);

        DataTable dtUniAndDMSExam = VsdtUniAndDMSExam;
        DataRow drUniAndDMSExam = dtUniAndDMSExam.Rows[rowIndex];

        objRollNumberMappPl = new PLRollNumberMapping();
        objRollNumberMappPl.ExamYear = Convert.ToInt32(ddlExamYear.SelectedItem.Text);
        objRollNumberMappPl.ExamSession = ddlSession.SelectedItem.Text;
        objRollNumberMappPl.ExamSessionID = Convert.ToInt32(ddlSession.SelectedValue);
        objRollNumberMappPl.ExamFacultyCD = Convert.ToInt32(ddlExamFaculty.SelectedValue);
        objRollNumberMappPl.ExamCtrlCD = Convert.ToInt32(drUniAndDMSExam["DMSExamCD"].ToString());

        if (e.CommandName == "MappedRow")
        {
            objRollNumberMappPl.Ind = 21;
            DataTable dtMapped = objRollNumberMappBl.MappedRollNumber(objRollNumberMappPl);
            if (dtMapped.Rows.Count > 0)
            {
                GridViewRow row = grdUniAndDMSRollNumberMapp.Rows[rowIndex];
                Label lblUniversityRecords = (Label)row.FindControl("lblUniversityRecords");
                Label lblPunchedRecords = (Label)row.FindControl("lblPunchedRecords");
                LinkButton lnkMissingPunchedRecord = (LinkButton)row.FindControl("lnkMissingPunchedRecord");
                Label lblPartialMatch = (Label)row.FindControl("lblPartialMatch");
                LinkButton lnkExtraPunchedRecord = (LinkButton)row.FindControl("lnkExtraPunchedRecord");
                Button btnMapped = (Button)row.FindControl("btnMapped");
                Button btnInsertExtraPunchedRecord = (Button)row.FindControl("btnInsertExtraPunchedRecord");

                if (dtMapped.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    lblMsg.Text = "Roll Number Mapped Successfully.";
                    lblMsg.Style.Add("color", "#1e983b");

                    objRollNumberMappPl.Ind = 20;
                    DataTable dtShow = objRollNumberMappBl.ShowMappedData(objRollNumberMappPl);
                    if (dtShow.Rows.Count > 0)
                    {
                        DataRow row1 = dtShow.Rows[rowIndex];
                        if (row1 != null)
                        {
                            lblUniversityRecords.Text = row1[7].ToString();
                            lblPunchedRecords.Text = row1[8].ToString();
                            lnkMissingPunchedRecord.Text = row1[9].ToString();
                            lblPartialMatch.Text = row1[11].ToString();


                            if (Convert.ToInt16(lblPartialMatch.Text) == 1)
                            {
                                btnMapped.Enabled = false;
                                btnMapped.CssClass = "btn btn-success btn-xs height28widht54";
                                lnkExtraPunchedRecord.Text = row1[10].ToString();
                                lblPunchedRecords.Text = Convert.ToString(Convert.ToInt32(lblUniversityRecords.Text) - Convert.ToInt32(lblPunchedRecords.Text));
                            }
                            else if (Convert.ToInt16(lblPartialMatch.Text) == 2)
                            {
                                btnMapped.Enabled = true;
                                btnMapped.CssClass = "btn btn-warning btn-xs height28widht54";
                                lnkExtraPunchedRecord.Text = row1[10].ToString();
                                lblPunchedRecords.Text = Convert.ToString(Convert.ToInt32(lblUniversityRecords.Text) - Convert.ToInt32(lblPunchedRecords.Text));
                            }
                            else if (Convert.ToInt16(lblPartialMatch.Text) == 3)
                            {
                                btnMapped.Enabled = true;
                                lnkMissingPunchedRecord.Text = lnkExtraPunchedRecord.Text = "0";
                                btnMapped.CssClass = "btn btn-danger btn-xs height28widht54";
                            }

                            if (Convert.ToInt32(lnkExtraPunchedRecord.Text) <= 0)
                                btnInsertExtraPunchedRecord.Enabled = false;
                            else
                                btnInsertExtraPunchedRecord.Enabled = true;
                        }
                    }
                }
                else
                {
                    lblMsg.Text = "Roll Number Not Mapped Successfully, Please Try Again.";
                    lblMsg.Style.Add("color", "red");
                    btnMapped.Focus();
                }
            }
        }

        if (e.CommandName == "MissingPunchedRecord")
        {
            objRollNumberMappPl.Ind = 22;

            //DataTable dtUniAndDMSExam = VsdtUniAndDMSExam;
            //DataRow drUniAndDMSExam = dtUniAndDMSExam.Rows[rowIndex];
            //objRollNumberMappPl = new PLRollNumberMapping();
            //objRollNumberMappPl.ExamYear = Convert.ToInt32(ddlExamYear.SelectedItem.Text);
            //objRollNumberMappPl.ExamSession = ddlSession.SelectedItem.Text;
            //objRollNumberMappPl.ExamSessionID = Convert.ToInt32(ddlSession.SelectedValue);
            //objRollNumberMappPl.ExamFacultyCD = Convert.ToInt32(ddlExamFaculty.SelectedValue);
            //objRollNumberMappPl.ExamCtrlCD = Convert.ToInt32(drUniAndDMSExam["DMSExamCD"].ToString());

            DataTable dtMissingRecord = objRollNumberMappBl.GetMissingPunchedRecord(objRollNumberMappPl);
            if (dtMissingRecord.Rows.Count > 0)
            {
                GridViewRow row = grdUniAndDMSRollNumberMapp.Rows[rowIndex];
                Label lblUniExamFaculty = (Label)row.FindControl("lblUniExamFaculty");
                Label lblFacultyName = (Label)row.FindControl("lblFacultyName");

                lblMPRExamYear.Text = ddlExamYear.SelectedItem.Text;
                lblMPRExamSession.Text = ddlSession.SelectedItem.Text;
                lblMPRExamFaculty.Text = ddlExamFaculty.SelectedItem.Text;
                lblMPRUniversityExam.Text = lblUniExamFaculty.Text;
                lblMPRDMSExam.Text = lblFacultyName.Text;

                grdMissingPunchedRecord.DataSource = dtMissingRecord;
                grdMissingPunchedRecord.DataBind();

                missingPunchedRecordModal.Style.Add("display", "block");
                extraPunchedRecordModal.Style.Add("display", "none");
            }
        }

        if (e.CommandName == "ExtraPunchedRecord")
        {
            objRollNumberMappPl.Ind = 23;

            //DataTable dtUniAndDMSExam = VsdtUniAndDMSExam;
            //DataRow drUniAndDMSExam = dtUniAndDMSExam.Rows[rowIndex];
            //objRollNumberMappPl = new PLRollNumberMapping();
            //objRollNumberMappPl.ExamYear = Convert.ToInt32(ddlExamYear.SelectedItem.Text);
            //objRollNumberMappPl.ExamSession = ddlSession.SelectedItem.Text;
            //objRollNumberMappPl.ExamSessionID = Convert.ToInt32(ddlSession.SelectedValue);
            //objRollNumberMappPl.ExamFacultyCD = Convert.ToInt32(ddlExamFaculty.SelectedValue);
            //objRollNumberMappPl.ExamCtrlCD = Convert.ToInt32(drUniAndDMSExam["DMSExamCD"].ToString());

            DataTable dtExtraPunchedRecord = objRollNumberMappBl.GetExtraPunchedRecord(objRollNumberMappPl);
            if (dtExtraPunchedRecord.Rows.Count > 0)
            {
                GridViewRow row = grdUniAndDMSRollNumberMapp.Rows[rowIndex];
                Label lblUniExamFaculty = (Label)row.FindControl("lblUniExamFaculty");
                Label lblFacultyName = (Label)row.FindControl("lblFacultyName");

                lblEPRExamYear.Text = ddlExamYear.SelectedItem.Text;
                lblEPRExamSession.Text = ddlSession.SelectedItem.Text;
                lblEPRExamFaculty.Text = ddlExamFaculty.SelectedItem.Text;
                lblEPRUniversityExam.Text = lblUniExamFaculty.Text;
                lblEPRDMSExam.Text = lblFacultyName.Text;

                grdExtraPunchedRecord.DataSource = dtExtraPunchedRecord;
                grdExtraPunchedRecord.DataBind();

                extraPunchedRecordModal.Style.Add("display", "block");
                missingPunchedRecordModal.Style.Add("display", "none");
            }
        }

        if (e.CommandName == "InsertExtraPunchedRecord")
        {
            GridViewRow row = grdUniAndDMSRollNumberMapp.Rows[rowIndex];
            LinkButton lnkExtraPunchedRecord = (LinkButton)row.FindControl("lnkExtraPunchedRecord");
            Button btnInsertExtraPunchedRecord = (Button)row.FindControl("btnInsertExtraPunchedRecord");

            objRollNumberMappPl.Ind = 24;
            DataSet ds = objRollNumberMappBl.InsertExtraPuncgedRecords(objRollNumberMappPl);
            if (ds.Tables.Count > 0)
            {
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["RDCnt"].ToString()) > 0 || Convert.ToInt32(ds.Tables[1].Rows[0]["RSCnt"].ToString()) > 0)
                {
                    objRollNumberMappPl.Ind = 20;
                    DataTable dtShow = objRollNumberMappBl.ShowMappedData(objRollNumberMappPl);
                    if (dtShow.Rows.Count > 0)
                    {
                        DataRow[] row1 = dtShow.Select("DMSExamCD='" + objRollNumberMappPl.ExamCtrlCD + "'");
                        if (row1.Count() > 0)
                        {
                            lnkExtraPunchedRecord.Text = row1[0]["Extra"].ToString();

                            if (Convert.ToInt32(lnkExtraPunchedRecord.Text) <= 0)
                                btnInsertExtraPunchedRecord.Enabled = false;
                            else
                                btnInsertExtraPunchedRecord.Enabled = true;
                        }
                    }
                }
            }

            extraPunchedRecordModal.Style.Add("display", "none");
            missingPunchedRecordModal.Style.Add("display", "none");
        }
    }

    /// <summary>
    /// Change Mapped Button Color According To Partial Match
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdUniAndDMSRollNumberMapp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex >= 0)
            {
                Label lblPartialMatch = (Label)e.Row.FindControl("lblPartialMatch");
                Label lblUniversityRecords = (Label)e.Row.FindControl("lblUniversityRecords");
                Label lblPunchedRecords = (Label)e.Row.FindControl("lblPunchedRecords");
                Button btnMapped = (Button)e.Row.FindControl("btnMapped");
                LinkButton lnkMissingPunchedRecord = (LinkButton)e.Row.FindControl("lnkMissingPunchedRecord");
                LinkButton lnkExtraPunchedRecord = (LinkButton)e.Row.FindControl("lnkExtraPunchedRecord");
                Button btnInsertExtraPunchedRecord = (Button)e.Row.FindControl("btnInsertExtraPunchedRecord");

                if (Convert.ToInt16(lblPartialMatch.Text) == 1)
                {
                    btnMapped.Enabled = false;
                    btnMapped.CssClass = "btn btn-success btn-xs height28widht54";
                    lblPunchedRecords.Text = Convert.ToString(Convert.ToInt32(lblUniversityRecords.Text) - Convert.ToInt32(lblPunchedRecords.Text));
                }
                else if (Convert.ToInt16(lblPartialMatch.Text) == 2)
                {
                    btnMapped.Enabled = true;
                    btnMapped.CssClass = "btn btn-warning btn-xs height28widht54";
                    lblPunchedRecords.Text = Convert.ToString(Convert.ToInt32(lblUniversityRecords.Text) - Convert.ToInt32(lblPunchedRecords.Text));
                }
                else if (Convert.ToInt16(lblPartialMatch.Text) == 3)
                {
                    btnMapped.Enabled = true;
                    btnMapped.CssClass = "btn btn-danger btn-xs height28widht54";
                    lnkMissingPunchedRecord.Text = lnkExtraPunchedRecord.Text = "0";
                }

                if (Convert.ToInt32(lnkExtraPunchedRecord.Text) <= 0)
                    btnInsertExtraPunchedRecord.Enabled = false;
                else
                    btnInsertExtraPunchedRecord.Enabled = true;
            }
        }
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
        missingPunchedRecordModal.Style.Add("display", "none");
        extraPunchedRecordModal.Style.Add("display", "none");

        divMapped.Visible = false;
        ddlExamYear.Enabled = ddlSession.Enabled = ddlExamFaculty.Enabled = btnShow.Enabled = true;
        btnClearAll.Enabled = false;
        ddlExamYear.ClearSelection();
        ddlSession.ClearSelection();
        ddlExamFaculty.ClearSelection();
        VsdtUniAndDMSExam = null;
        grdUniAndDMSRollNumberMapp.DataSource = grdMissingPunchedRecord.DataSource =
            grdExtraPunchedRecord.DataSource = VsdtUniAndDMSExam = new DataTable();
        grdUniAndDMSRollNumberMapp.DataBind(); grdMissingPunchedRecord.DataBind();
        grdExtraPunchedRecord.DataBind();
        ddlExamYear.Focus();
    }
}