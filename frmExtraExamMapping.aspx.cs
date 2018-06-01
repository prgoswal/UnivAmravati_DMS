using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmExtraExamMapping : System.Web.UI.Page
{
    #region Declaration

    BLExtraExamMapping objExtraExamMappBl = new BLExtraExamMapping();
    PLExtraExamMapping objExtraExamMappPl;

    DataTable VsdtExtraExamMapping
    {
        get { return (DataTable)ViewState["dtExtraExamMapping"]; }
        set { ViewState["dtExtraExamMapping"] = value; }
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
    /// Bind Extra Exam Mapping (ExamYear,ExamSession) DropDownList
    /// </summary>
    void BindAllDDL()
    {
        try
        {
            objExtraExamMappPl = new PLExtraExamMapping();
            objExtraExamMappPl.Ind = 2;
            DataSet dsExamMapp = objExtraExamMappBl.BindAllDDl(objExtraExamMappPl);
            if (dsExamMapp.Tables.Count > 0)
            {
                DataTable dtExamYear = dsExamMapp.Tables[0];
                DataTable dtSession = dsExamMapp.Tables[1];
                DataTable dtExamFaculty = dsExamMapp.Tables[2];

                if (dtExamYear.Rows.Count > 0)
                {
                    ddlExamYear.DataSource = dtExamYear;
                    ddlExamYear.DataValueField = "ItemID"; 
                    ddlExamYear.DataTextField = "ItemDesc";
                    ddlExamYear.DataBind();
                    ddlExamYear.Items.Insert(0, new ListItem("-- Select Year --", "0"));
                }

                if (dtSession.Rows.Count > 0)
                {
                    ddlSession.DataSource = dtSession;
                    ddlSession.DataValueField = "ItemID"; 
                    ddlSession.DataTextField = "ItemDesc";
                    ddlSession.DataBind();
                    ddlSession.Items.Insert(0, new ListItem("-- Select Session --", "0"));
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
    /// Show Extra Exam Mapping Status According To ExamYear, ExamSession And ExamFaculty
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShow_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        objExtraExamMappPl = new PLExtraExamMapping();
        objExtraExamMappPl.Ind = 25;
        objExtraExamMappPl.ExamYear = Convert.ToInt32(ddlExamYear.SelectedItem.Text);
        objExtraExamMappPl.ExamSession = ddlSession.SelectedItem.Text;
        objExtraExamMappPl.ExamSessionID = Convert.ToInt32(ddlSession.SelectedValue);
        DataSet dsRollNumberMapp = objExtraExamMappBl.GetExtraExamMapping(objExtraExamMappPl);
        if (dsRollNumberMapp.Tables.Count > 0)
        {
            VsdtExtraExamMapping = dsRollNumberMapp.Tables[0];
            if (VsdtExtraExamMapping.Rows.Count > 0)
            {
                grdExtraExamMapping.DataSource = VsdtExtraExamMapping;
                grdExtraExamMapping.DataBind();

                ddlExamYear.Enabled = ddlSession.Enabled = btnShow.Enabled = false;
                btnClearAll.Enabled = true;
            }
            else
            {
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
    /// Insert Extra Exam One By One (Row-Wise)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdExtraExamMapping_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = "";

        int rowIndex = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "InsertExtraExam")
        {
            DataTable dtExtraExam = VsdtExtraExamMapping;
            DataRow drExtraExam = dtExtraExam.Rows[rowIndex];

            objExtraExamMappPl = new PLExtraExamMapping();
            objExtraExamMappPl.Ind = 26;
            objExtraExamMappPl.ExamYear = Convert.ToInt32(ddlExamYear.SelectedItem.Text);
            objExtraExamMappPl.ExamSession = ddlSession.SelectedItem.Text;
            objExtraExamMappPl.ExamSessionID = Convert.ToInt32(ddlSession.SelectedValue);
            objExtraExamMappPl.ExamFacultyCD = Convert.ToInt32(drExtraExam["FacultyCD"].ToString());
            objExtraExamMappPl.ExamCtrlCD = Convert.ToInt32(drExtraExam["Ctrl"].ToString());

            DataSet dsInsertExtraExam = objExtraExamMappBl.InsertExtraExamRecords(objExtraExamMappPl);
            if (dsInsertExtraExam.Tables.Count > 0)
            {
                if (Convert.ToInt32(dsInsertExtraExam.Tables[0].Rows[0]["RDCnt"].ToString()) > 0 
                    || Convert.ToInt32(dsInsertExtraExam.Tables[1].Rows[0]["RSCnt"].ToString()) > 0)
                {   
                    btnShow_Click(sender, e);

                    lblMsg.Text = "Record Insert Successfully.";
                    lblMsg.Style.Add("color", "#1e983b");
                }
            }
            else
            {
                lblMsg.Text = "Record Not Inserted.";
                lblMsg.Style.Add("color", "red");
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdExtraExamMapping_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex >= 0)
            {
                //Label lblPartialMatch = (Label)e.Row.FindControl("lblPartialMatch");
                //Label lblUniversityRecords = (Label)e.Row.FindControl("lblUniversityRecords");
                //Label lblPunchedRecords = (Label)e.Row.FindControl("lblPunchedRecords");
                //Button btnMapped = (Button)e.Row.FindControl("btnMapped");
                //LinkButton lnkMissingPunchedRecord = (LinkButton)e.Row.FindControl("lnkMissingPunchedRecord");
                //LinkButton lnkExtraPunchedRecord = (LinkButton)e.Row.FindControl("lnkExtraPunchedRecord");
                //Button btnInsertExtraPunchedRecord = (Button)e.Row.FindControl("btnInsertExtraPunchedRecord");

                //if (Convert.ToInt16(lblPartialMatch.Text) == 1)
                //{
                //    btnMapped.Enabled = false;
                //    btnMapped.CssClass = "btn btn-success btn-xs height28widht54";
                //    lblPunchedRecords.Text = Convert.ToString(Convert.ToInt32(lblUniversityRecords.Text) - Convert.ToInt32(lblPunchedRecords.Text));
                //}
                //else if (Convert.ToInt16(lblPartialMatch.Text) == 2)
                //{
                //    btnMapped.Enabled = true;
                //    btnMapped.CssClass = "btn btn-warning btn-xs height28widht54";
                //    lblPunchedRecords.Text = Convert.ToString(Convert.ToInt32(lblUniversityRecords.Text) - Convert.ToInt32(lblPunchedRecords.Text));
                //}
                //else if (Convert.ToInt16(lblPartialMatch.Text) == 3)
                //{
                //    btnMapped.Enabled = true;
                //    btnMapped.CssClass = "btn btn-danger btn-xs height28widht54";
                //    lnkMissingPunchedRecord.Text = lnkExtraPunchedRecord.Text = "0";
                //}

                //if (Convert.ToInt32(lnkExtraPunchedRecord.Text) <= 0)
                //    btnInsertExtraPunchedRecord.Enabled = false;
                //else
                //    btnInsertExtraPunchedRecord.Enabled = true;
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
        ddlExamYear.Enabled = ddlSession.Enabled = btnShow.Enabled = true;
        btnClearAll.Enabled = false;
        ddlExamYear.ClearSelection();
        ddlSession.ClearSelection();
        VsdtExtraExamMapping = null;
        grdExtraExamMapping.DataSource = VsdtExtraExamMapping = new DataTable();
        grdExtraExamMapping.DataBind();
        ddlExamYear.Focus();
    }
}