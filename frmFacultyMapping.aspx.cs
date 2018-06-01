using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmFacultyMapping : System.Web.UI.Page
{
    #region Declaration

    BlFacultyMapping objFacultyMappBl = new BlFacultyMapping();
    PlFacultyMapping objFacultyMappPl;

    DataTable VsdtUniExamFaculty
    {
        get { return (DataTable)ViewState["dtUniExamFaculty"]; }
        set { ViewState["dtUniExamFaculty"] = value; }
    }

    DataTable VsdtDMSExamFaculty
    {
        get { return (DataTable)ViewState["dtDMSExamFaculty"]; }
        set { ViewState["dtDMSExamFaculty"] = value; }
    }

    DataTable VsdtUniAndDMSExamFaculty
    {
        get { return (DataTable)ViewState["dtUniAndDMSExamFaculty"]; }
        set { ViewState["dtUniAndDMSExamFaculty"] = value; }
    }

    DataTable VsdtFinalUniAndDMSExamFaculty
    {
        get { return (DataTable)ViewState["dtFinalUniAndDMSExamFaculty"]; }
        set { ViewState["dtFinalUniAndDMSExamFaculty"] = value; }
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
    /// Bind Faculty Mapping (ExamYear And ExamSession) DropDownList
    /// </summary>
    void BindAllDDL()
    {
        try
        {
            objFacultyMappPl = new PlFacultyMapping();
            objFacultyMappPl.Ind = 2;
            DataSet dsFacultyMapp = objFacultyMappBl.BindAllDDl(objFacultyMappPl);
            if (dsFacultyMapp.Tables.Count > 0)
            {
                DataTable dtExamYear = dsFacultyMapp.Tables[0];
                DataTable dtSession = dsFacultyMapp.Tables[1];

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
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    /// <summary>
    ///    Show Available And Alloted Faculty According To ExamYear And ExamSession
    /// </summary>
    /// /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShow_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        objFacultyMappPl = new PlFacultyMapping();
        objFacultyMappPl.Ind = 1;
        objFacultyMappPl.ExamYear = Convert.ToInt32(ddlExamYear.SelectedItem.Text);
        objFacultyMappPl.ExamSession = ddlSession.SelectedItem.Text;
        objFacultyMappPl.ExamSessionID = Convert.ToInt32(ddlSession.SelectedValue);
        DataSet dsFacultyMapp = objFacultyMappBl.GetAllUniAndDMSExamFaculty(objFacultyMappPl);
        if (dsFacultyMapp.Tables.Count > 0)
        {
            //Available
            VsdtUniExamFaculty = dsFacultyMapp.Tables[0];
            VsdtDMSExamFaculty = dsFacultyMapp.Tables[1];

            //Alloted
            VsdtFinalUniAndDMSExamFaculty = dsFacultyMapp.Tables[2];

            if (VsdtUniExamFaculty.Rows.Count <= 0 && VsdtDMSExamFaculty.Rows.Count <= 0 && VsdtFinalUniAndDMSExamFaculty.Rows.Count <= 0)
            {
                lblMsg.Text = "Record Not Found.";
                return;
            }

            if (VsdtDMSExamFaculty.Rows.Count > 0)
            {
                grdDMSExamFaculty.DataSource = VsdtDMSExamFaculty;
                grdDMSExamFaculty.DataBind();
            }

            if (VsdtUniExamFaculty.Rows.Count > 0)
            {
                grdUniversityExamFaculty.DataSource = VsdtUniExamFaculty;
                grdUniversityExamFaculty.DataBind();
            }

            if (VsdtDMSExamFaculty.Rows.Count > 0 || VsdtUniExamFaculty.Rows.Count > 0)
                divUnMapped.Visible = true;
            else
                divUnMapped.Visible = false;


            if (VsdtFinalUniAndDMSExamFaculty.Rows.Count > 0)
            {
                grdFinalUniAndDMSFaculty.DataSource = VsdtFinalUniAndDMSExamFaculty;
                grdFinalUniAndDMSFaculty.DataBind();
                divMapped.Visible = true;
            }
            else
                divMapped.Visible = false;

            btnAdd.Visible = true;
            ddlExamYear.Enabled = ddlSession.Enabled = btnShow.Enabled = false;
            btnSave.Enabled = btnClearAll.Enabled = true;
        }
        else
        {
            lblMsg.Text = "Record Not Found.";
            return;
        }
    }

    /// <summary>
    /// Add Both Un - Mapped (University And DMS) Faculty In Mapped Faculties GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        VsdtUniAndDMSExamFaculty = null;
        VsdtUniAndDMSExamFaculty = new DataTable();

        bool uniCheckedOrNot = false;
        bool dmsCheckedOrNot = false;

        DataTable dtDMSRowIndex = new DataTable();

        if (VsdtUniAndDMSExamFaculty == null || VsdtUniAndDMSExamFaculty.Rows.Count <= 0)
        {
            VsdtUniAndDMSExamFaculty = new DataTable();
            VsdtUniAndDMSExamFaculty.Columns.Add("ExamYear", typeof(int));
            VsdtUniAndDMSExamFaculty.Columns.Add("ExamSession", typeof(string));
            VsdtUniAndDMSExamFaculty.Columns.Add("ExamSessionID", typeof(int));
            VsdtUniAndDMSExamFaculty.Columns.Add("UnivFacultyName", typeof(string));
            VsdtUniAndDMSExamFaculty.Columns.Add("FacultyCD", typeof(int));
            VsdtUniAndDMSExamFaculty.Columns.Add("DMSFacultyName", typeof(string));
        }

        foreach (GridViewRow gvUniRow in grdUniversityExamFaculty.Rows)
        {
            CheckBox chkUniExamFaculty = gvUniRow.FindControl("chkUniExamFaculty") as CheckBox;

            if (chkUniExamFaculty.Checked == true)
            {
                Label lblSoftDataFacultyCD = gvUniRow.FindControl("lblSoftDataFacultyCD") as Label;
                Label lblSoftDataFacultyName = gvUniRow.FindControl("lblSoftDataFacultyName") as Label;

                DataRow drUniAndDMSExamFaculty = VsdtUniAndDMSExamFaculty.NewRow();
                drUniAndDMSExamFaculty["ExamYear"] = Convert.ToInt32(ddlExamYear.SelectedItem.Text);
                drUniAndDMSExamFaculty["ExamSession"] = ddlSession.SelectedItem.Text;
                drUniAndDMSExamFaculty["ExamSessionID"] = Convert.ToInt32(ddlSession.SelectedValue);
                drUniAndDMSExamFaculty["UnivFacultyName"] = lblSoftDataFacultyName.Text;

                foreach (GridViewRow gvDMSRow in grdDMSExamFaculty.Rows)
                {
                    CheckBox chkDMSExamFaculty = gvDMSRow.FindControl("chkDMSExamFaculty") as CheckBox;
                    if (chkDMSExamFaculty.Checked == true)
                    {
                        Label lblFacultyCD = gvDMSRow.FindControl("lblFacultyCD") as Label;
                        Label lblFacultyName = gvDMSRow.FindControl("lblFacultyName") as Label;

                        drUniAndDMSExamFaculty["FacultyCD"] = Convert.ToInt32(lblFacultyCD.Text);
                        drUniAndDMSExamFaculty["DMSFacultyName"] = lblFacultyName.Text;
                        VsdtUniAndDMSExamFaculty.Rows.Add(drUniAndDMSExamFaculty);

                        DataRow[] r1 = VsdtUniExamFaculty.Select("SoftDataFacultyName='" + lblSoftDataFacultyName.Text + "'");
                        if (r1.Count() > 0)
                        {
                            int rowIndex1 = VsdtUniExamFaculty.Rows.IndexOf(r1[0]);
                            VsdtUniExamFaculty.Rows.RemoveAt(rowIndex1);
                            grdUniversityExamFaculty.DataSource = VsdtUniExamFaculty;
                            grdUniversityExamFaculty.DataBind();
                        }

                        DataRow[] r2 = VsdtDMSExamFaculty.Select("FacultyName='" + lblFacultyName.Text + "'");
                        if (r2.Count() > 0)
                        {
                            int rowIndex = VsdtDMSExamFaculty.Rows.IndexOf(r2[0]);

                            if (!dtDMSRowIndex.Columns.Contains("DMSFacultyName"))
                                dtDMSRowIndex.Columns.Add("DMSFacultyName", typeof(string));

                            DataRow drDMSRowIndex = dtDMSRowIndex.NewRow();
                            drDMSRowIndex["DMSFacultyName"] = lblFacultyName.Text;
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
            lblMsg.Text = "Select Atleast One University Exam Faculty.";
            return;
        }

        if (dmsCheckedOrNot == false)
        {
            lblMsg.Text = "Select Atleast One DMS Exam Faculty.";
            VsdtUniAndDMSExamFaculty = null;
            VsdtUniAndDMSExamFaculty = new DataTable();
            return;
        }

        if (uniCheckedOrNot == true && dmsCheckedOrNot == true)
        {
            foreach (DataRow dmsRow in dtDMSRowIndex.Rows)
            {
                DataRow[] r = VsdtDMSExamFaculty.Select("FacultyName='" + dmsRow["DMSFacultyName"] + "'");
                if (r.Count() > 0)
                {
                    int rowIndex = VsdtDMSExamFaculty.Rows.IndexOf(r[0]);
                    VsdtDMSExamFaculty.Rows.RemoveAt(rowIndex);
                    grdDMSExamFaculty.DataSource = VsdtDMSExamFaculty;
                    grdDMSExamFaculty.DataBind();
                }
            }

            if (VsdtFinalUniAndDMSExamFaculty == null || VsdtFinalUniAndDMSExamFaculty.Rows.Count <= 0)
            {
                grdFinalUniAndDMSFaculty.DataSource = VsdtFinalUniAndDMSExamFaculty = VsdtUniAndDMSExamFaculty;
                grdFinalUniAndDMSFaculty.DataBind();
            }
            else
            {
                foreach (DataRow gvFinalRow in VsdtUniAndDMSExamFaculty.Rows)
                {
                    DataRow dr = VsdtFinalUniAndDMSExamFaculty.NewRow();
                    dr["ExamYear"] = gvFinalRow["ExamYear"];
                    dr["ExamSession"] = gvFinalRow["ExamSession"];
                    dr["ExamSessionID"] = gvFinalRow["ExamSessionID"];
                    dr["UnivFacultyName"] = gvFinalRow["UnivFacultyName"];
                    dr["FacultyCD"] = gvFinalRow["FacultyCD"];
                    dr["DMSFacultyName"] = gvFinalRow["DMSFacultyName"];
                    VsdtFinalUniAndDMSExamFaculty.Rows.Add(dr);
                }
                grdFinalUniAndDMSFaculty.DataSource = VsdtFinalUniAndDMSExamFaculty;
                grdFinalUniAndDMSFaculty.DataBind();
            }
        }

        if (grdUniversityExamFaculty.Rows.Count <= 0 && grdDMSExamFaculty.Rows.Count <= 0)
        {
            btnAdd.Visible = false;
            divUnMapped.Visible = false;
        }

        if (grdFinalUniAndDMSFaculty.Rows.Count > 0)
        {
            divMapped.Visible = true;
        }
    }

    /// <summary>
    /// Delete Mapped Faculties Then Add In Un - Mapped (University And DMS) Faculty GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdFinalUniAndDMSFaculty_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "DeleteRow")
        {
            DataTable dtFinalUniAndDMSExamFaculty = VsdtFinalUniAndDMSExamFaculty;
            DataRow drFinalUniAndDMSExamFaculty = dtFinalUniAndDMSExamFaculty.Rows[rowIndex];

            string UnivFacultyName = drFinalUniAndDMSExamFaculty["UnivFacultyName"].ToString();

            DataRow[] drUniv = VsdtUniExamFaculty.Select("SoftDataFacultyName='" + UnivFacultyName + "'");

            if (drUniv.Count() <= 0)
            {
                DataRow drUniExamFaculty = VsdtUniExamFaculty.NewRow();
                drUniExamFaculty["SoftDataFacultyCD"] = 0;
                drUniExamFaculty["SoftDataFacultyName"] = UnivFacultyName;
                VsdtUniExamFaculty.Rows.Add(drUniExamFaculty);
                VsdtUniExamFaculty.DefaultView.Sort = "SoftDataFacultyName Asc";
                grdUniversityExamFaculty.DataSource = VsdtUniExamFaculty;
                grdUniversityExamFaculty.DataBind();
            }

            int dmsFacultyCD = Convert.ToInt32(drFinalUniAndDMSExamFaculty["FacultyCD"]);
            string dmsFacultyName = drFinalUniAndDMSExamFaculty["DMSFacultyName"].ToString();

            DataRow[] dr = VsdtDMSExamFaculty.Select("FacultyName='" + dmsFacultyName + "'");

            if (dr.Count() <= 0)
            {
                DataRow drDMSExamFaculty = VsdtDMSExamFaculty.NewRow();
                drDMSExamFaculty["FacultyCD"] = dmsFacultyCD;
                drDMSExamFaculty["FacultyName"] = dmsFacultyName;
                VsdtDMSExamFaculty.Rows.Add(drDMSExamFaculty);
                VsdtDMSExamFaculty.DefaultView.Sort = "FacultyName Asc";
                grdDMSExamFaculty.DataSource = VsdtDMSExamFaculty;
                grdDMSExamFaculty.DataBind();
            }

            VsdtFinalUniAndDMSExamFaculty.Rows.RemoveAt(rowIndex);

            grdFinalUniAndDMSFaculty.DataSource = VsdtFinalUniAndDMSExamFaculty;
            grdFinalUniAndDMSFaculty.DataBind();


            if (grdUniversityExamFaculty.Rows.Count > 0 || grdDMSExamFaculty.Rows.Count > 0)
            {
                btnAdd.Visible = true;
                divUnMapped.Visible = true;
            }

            if (grdFinalUniAndDMSFaculty.Rows.Count <= 0)
            {
                divMapped.Visible = false;
            }
        }
    }

    /// <summary>
    /// Save Mapped Faculties GridView Records
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        if (VsdtFinalUniAndDMSExamFaculty == null || VsdtFinalUniAndDMSExamFaculty.Rows.Count <= 0)
        {
            lblMsg.Text = "Select Atleast One University And  One DMS Exam Faculty, Then Press Add Button.";
            return;
        }

        objFacultyMappPl = new PlFacultyMapping();
        objFacultyMappPl.Ind = 3;
        objFacultyMappPl.ExamYear = Convert.ToInt32(ddlExamYear.SelectedItem.Text);
        objFacultyMappPl.ExamSession = ddlSession.SelectedItem.Text;
        objFacultyMappPl.ExamSessionID = Convert.ToInt32(ddlSession.SelectedValue);

        objFacultyMappPl.TblFacultyMapp = VsdtFinalUniAndDMSExamFaculty;
        DataTable dtSave = objFacultyMappBl.InsertUniAndDMSExamFacultyData(objFacultyMappPl);
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
        ddlExamYear.Enabled = ddlSession.Enabled = btnShow.Enabled = true;
        btnSave.Enabled = btnClearAll.Enabled = false;
        ddlExamYear.ClearSelection();
        ddlSession.ClearSelection();
        VsdtUniExamFaculty = VsdtDMSExamFaculty = VsdtUniAndDMSExamFaculty = VsdtFinalUniAndDMSExamFaculty = null;
        grdDMSExamFaculty.DataSource = grdUniversityExamFaculty.DataSource = grdFinalUniAndDMSFaculty.DataSource =
            VsdtUniExamFaculty = VsdtDMSExamFaculty = VsdtUniAndDMSExamFaculty = VsdtFinalUniAndDMSExamFaculty = new DataTable();
        grdDMSExamFaculty.DataBind(); grdUniversityExamFaculty.DataBind(); grdFinalUniAndDMSFaculty.DataBind();
        ddlExamYear.Focus();
    }
}