using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Data;
using System.IO;
public partial class ImageViewer : System.Web.UI.Page
{
    DlRegister dlReg = new DlRegister(); PlRegister plReg = new PlRegister();
    //BlRegister BlReg = new BlRegister();
    DlImgEntry dlImg = new DlImgEntry(); PlImgEntry plImg = new PlImgEntry();
    DataTable dtGrd = new DataTable();

    int i,LastF,LastCF; static int TotalPages, CompletedPageNo;
    DataTable dt, dt1; DataSet ds; //SqlDataAdapter da; SqlCommand cmd;
    int pos; static int count; PagedDataSource adsource;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    static string FilePath; int index;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ViewState["vs"] = 0;
            //btnnext.Visible = false;
            pnlcounter.Visible = true;
        }
        pos = (int)this.ViewState["vs"];

        // lblCuurentPage.Text = "";
        this.Form.DefaultButton = btnSearchImg.UniqueID;
    }
    private void AddRowsToGrid()
    {
        if (ViewState["AvailablePages"] == null)
        {
            dtGrd.Columns.Add("StudentRollNo", typeof(Int64));
            dtGrd.Columns.Add("StudentName", typeof(string));
            for (int k = 0; k < grdEntry.Rows.Count; k++)
            {
                dtGrd.Rows.Add(k, Convert.ToInt32(grdEntry.DataKeys[k].Values["StudentRollNo"].ToString()),
                    Convert.ToInt64(grdEntry.DataKeys[k].Values["StudentName"].ToString().ToUpper()));
            }
        }
        else
        {
            dtGrd = (DataTable)ViewState["AvailablePages"];
        }
        if (!string.IsNullOrEmpty(txtSRNo.Text) && !string.IsNullOrEmpty(txtStudentName.Text))
        {
            dtGrd.Rows.Add(Convert.ToInt64(txtSRNo.Text), txtStudentName.Text.ToString().ToUpper());
            grdEntry.DataSource = dtGrd;
            grdEntry.DataBind();
            ViewState["AvailablePages"] = dtGrd;
            dtGrd = null;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
       // grdEntry.DataBind();
        if (btnAdd.Text == "Add")
        {
            if (txtSRNo.Text == "")
            {
                lblmsg.Text = "Please Enter Student Roll No.";
                txtSRNo.Focus();
                return;
            }
            else if (txtStudentName.Text == "")
            {
                lblmsg.Text = "Please Enter Student Name";
                txtStudentName.Focus();
                return;
            }
            AddRowsToGrid();
            lblCuurentPage.Text = "Current Page:-  " + count.ToString() + "/" + TotalPages.ToString();
            lblEntryCompleted.Text = "Page Entry Completed :-" + GetCompletedPageNo() + "/" + TotalPages;          
           
        }
        else
        {
            grdEntry.Rows[Convert.ToInt32(Session["index"])].Cells[1].Text = txtSRNo.Text;
            grdEntry.Rows[Convert.ToInt32(Session["index"])].Cells[2].Text = txtStudentName.Text.ToUpper();
            //grdEntry.EditIndex = Convert.ToInt32(Session["index"]); 
            dtGrd.Rows.Clear();
            dtGrd.Columns.Clear();
            dtGrd.Columns.Add("StudentRollNo", typeof(Int64));
            dtGrd.Columns.Add("StudentName", typeof(string));
            for (int k = 0; k < grdEntry.Rows.Count; k++)
            {
                dtGrd.Rows.Add(Convert.ToInt64(grdEntry.Rows[k].Cells[1].Text),
                    (grdEntry.Rows[k].Cells[2]).Text.ToString().ToUpper());
            }
            ViewState["AvailablePages"] = dtGrd;
            btnAdd.Text = "Add";
        }
       
        Clear();
        txtSRNo.Focus();
        Session["index"] = null;
        
    }
    private DataTable GetCompletedPageNo()
    {
        plImg.Ind = 2;
        plImg.RegNo = Convert.ToInt64(txtRegNo.Text);
        DataTable dt = new DataTable();
        dt = dlImg.GetICompetedPages(plImg);
        return dt;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        //txtSRNo.Text = txtStudentName.Text = "";
    }
    protected void btnprev_Click(object sender, EventArgs e)
    {
        pos = (int)this.ViewState["vs"];
        pos -= 1;
        this.ViewState["vs"] = pos;
        TotalPages = GetImagePath(Convert.ToInt64(txtRegNo.Text));
        TotalPages = TotalPages / 2;
        count = pos + 1;
        lblCuurentPage.Text = "Page(s):-  " + count.ToString() + "/" + TotalPages.ToString();

    }
    protected void btnnext_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        if (Filetype.Value == lblInd.Text && lblInd.Text == "2")
        {
            radioCF.Checked = false;
            radioFoil.Checked = true;
            lblFoilEntry.Text = "Foil Entry";
            pnlcounter.Visible = true;
            pnlCf.Visible = false;
            pos = (int)this.ViewState["vs"];
            pos += 1;
            this.ViewState["vs"] = pos;
            TotalPages = GetImagePath(Convert.ToInt64(txtRegNo.Text));
            TotalPages = TotalPages / 2;
            count += 1 == 1 ? 0 : count;
            btnSave.Enabled = true;
            lblCuurentPage.Text = "Current Page:-  " + count.ToString() + "/" + TotalPages.ToString();
        }

        else if (Filetype.Value == lblInd.Text && lblInd.Text == "1")
        {
            radioCF.Checked = true;
            radioFoil.Checked = false;
            lblFoilEntry.Text = "Counter Foil Entry";
            pnlcounter.Visible = false;
            pnlCf.Visible = true;
            pos = (int)this.ViewState["vs"];
            // pos= pos == 0 ? 1 : pos;
            pos += 1 ;
            this.ViewState["vs"] = pos;
            TotalPages = GetImagePath(Convert.ToInt64(txtRegNo.Text));
            TotalPages = TotalPages / 2;
            count += 1;
            LbtnSaveCF.Enabled = true;
            //lblCuurentPage.Text = "Current Page:-  " + (count).ToString() + "/" + TotalPages.ToString();

        }
        else
        {
            lblmsg.Text = "Please Create Entry First";
            return;
        }
        // lblmsg.Text = Filetype.Value.ToString();

    }
    protected void btnSearchImg_Click(object sender, EventArgs e)
    {
        Session["dt"]=null;
        pnlcounter.Visible = true;
        count = 0;
       
        if (string.IsNullOrEmpty(txtRegNo.Text))
        {
            lblDetails.Text = "Please Enter Register No.";
            txtRegNo.Focus();
            dlImages.DataSource = null;
            dlImages.DataBind();
            lblCount.Text = "";
            pnlImageViewer.Visible = false;
            return;
        }
        else //if (!string.IsNullOrEmpty(txtRegNo.Text))
        {
            if (Session["UserId"].ToString() != "1")
            {
                plImg.Ind = 12;
                plImg.RegNo = Convert.ToInt64(txtRegNo.Text);
                plImg.UserId = Convert.ToInt32(Session["UserId"]);
                ds = dlImg.LastPageNoWithLotNo(plImg);
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblLotNo.Text = ds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        lblDetails.Text = "Entered Register No. Not Assign In Your Lot";
                        return;
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        LastF = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                        LastCF = Convert.ToInt32(ds.Tables[1].Rows[0][1]);
                        count = LastF + 1;
                        if (LastF == LastCF)
                        {
                            pos = (LastF * 2);
                            ViewState["vs"] = pos;
                            pnlcounter.Visible = true;
                            pnlCf.Visible = false;

                        }
                        else if (LastF > LastCF)
                        {
                            pos = (LastF * 2) - 1;
                            ViewState["vs"] = pos;
                            //count = LastF+1;
                            lblFoilEntry.Text = "Counter Foil Entry";
                            pnlcounter.Visible = false;
                            pnlCf.Visible = true;

                        }
                    }
                }
            }
            plReg.Ind = 11;
            plReg.RegNo = Convert.ToInt64(txtRegNo.Text);
            dt = dlReg.SearchRegister(plReg);
            Session["dt"] = dt;
            if (dt.Rows.Count > 0)
            {
                txtRegNo.Enabled = false;
                lblDetails.Text = "Exam Name :- " + dt.Rows[0]["ExamName"].ToString();
                // lblDetails.Text = "Exam Name:- Bacholer Of Engineering (Electronics and Communication Engineering Session 2014-15) - I Semester ";
                FilePath = dt.Rows[0]["FilePath"].ToString().Replace("/", "\\");
                pnlImageViewer.Visible = true;
                TotalPages = GetImagePath(Convert.ToInt64(txtRegNo.Text));
                TotalPages = TotalPages / 2;
                count = count == 0 ? 1 : count;
                if (LastF > LastCF)
                
                    lblCuurentPage.Text = "Current Page:-" + (count-1) + "/" + TotalPages.ToString();
                else
                    lblCuurentPage.Text = "Current Page:-" + count.ToString() + "/" + TotalPages.ToString();
                lblEntryCompleted.Text = "Last Page Completed :-" + GetCompletedPageNo() + "/" + TotalPages;
                lblType.Text = "TR Type";
                //pnlcounter.Visible = true;
                btnSearchImg.Enabled = false;
            }
            else
            {
                dlImages.DataSource = null;
                dlImages.DataBind();
                lblDetails.Text = "No Record Found";
                lblCount.Text = "";
                pnlImageViewer.Visible = false;
            }
        }
    }

    private void Clear()  // Clear Textboxes and  Set DropDown Lists To SelectedIndex "0"
    {
        lblmsg.Text = txtStudentName.Text = txtSRNo.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["AvailablePages"] = null;
    }
    private int GetImagePath(Int64 RegNo)
    {
        dt1 = new DataTable();
        dt1 = (DataTable)Session["dt"];

        adsource = new PagedDataSource();
        adsource.DataSource = dt1.DefaultView;
        adsource.PageSize = 1;
        adsource.AllowPaging = true;
        adsource.CurrentPageIndex = pos == 0 ? 1 : pos;
        Filetype.Value = dt1.Rows[pos]["FileType"].ToString();
        RegNoCF.Value = dt1.Rows[pos]["RegNo"].ToString();
        FilePathImg.Value = dt1.Rows[pos]["FilePath"].ToString();
        btnprev.Enabled = !adsource.IsFirstPage;
        btnnext.Enabled = !adsource.IsLastPage;
        dlImages.DataSource = adsource;
        dlImages.DataBind();

        return dt1.Rows.Count;
        //lblCount.Text = "Total Images:- " + dt1.Rows.Count.ToString() + "";
        // }
        //dlImages.FindControl("Filetype").ToString() = dt1.Rows[0]["FileType"].ToString();

    }

    private string GetFilePath()
    {
        return "";
    }

    protected void grdEntry_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        dtGrd = (DataTable)ViewState["AvailablePages"];
        dtGrd.Rows.Remove(dtGrd.Rows[e.RowIndex]);
        grdEntry.DataSource = dtGrd;
        grdEntry.DataBind();
    }

    protected void radioFoil_CheckedChanged(object sender, EventArgs e)
    {
        if (radioFoil.Checked == true)
        {
            pnlcounter.Visible = true;
            pnlCf.Visible = false;
            lblCuurentPage.Text = "Current Page:-  " + count.ToString() + "/" + TotalPages.ToString();
        }
    }

    protected void radioCF_CheckedChanged(object sender, EventArgs e)
    {
        if (radioCF.Checked == true)
        {
            pnlcounter.Visible = false;
            pnlCf.Visible = true;
            lblCuurentPage.Text = "Current Page:-  " + (count - 1).ToString() + "/" + TotalPages.ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int insrtImg = 0;
        lblmsg.Text = "";
        try
        {
            if (grdEntry.Rows.Count > 0)
            {
                for (int k = 0; k < grdEntry.Rows.Count; k++)
                {
                    plImg.Ind = 1;
                    plImg.RegNo = Convert.ToInt64(RegNoCF.Value);
                    plImg.RollNo = Convert.ToInt64(grdEntry.Rows[k].Cells[1].Text);
                    plImg.SName = grdEntry.Rows[k].Cells[2].Text.ToUpper();
                    plImg.FilePath = FilePathImg.Value;
                    plImg.CompletedPages = count;
                    plImg.TotalPages = TotalPages;
                    plImg.ErrorMark = 0;
                    plImg.IpAddress = Request.UserHostAddress;
                    plImg.UserId = Convert.ToInt32(Session["UserId"]);
                    plImg.CreationDate = DateTime.Now;
                    plImg.AllotmentNo = Convert.ToInt32(lblLotNo.Text);
                    plImg.EntryByRegNo = Convert.ToInt64(txtRegNo.Text);
                    insrtImg = dlImg.InserImgEntry(plImg);
                    if (insrtImg >= 0)
                    {
                        lblmsg.Text = "Foil Page Entry Completed";
                        lblInd.Text = "1";
                        btnSave.Enabled = false;


                    }
                    else
                    {
                        lblmsg.Text = "Roll No. - " + plImg.RollNo + " Is Already Exists";
                        return;
                    }
                }

                lblCuurentPage.Text = "Current Page:-  " + count.ToString() + "/" + TotalPages.ToString();
                lblEntryCompleted.Text = "Page Entry Completed :-" + GetCompletedPageNo() + "/" + TotalPages;
                ViewState["AvailablePages"] = null;
                grdEntry.DataSource = null;
                grdEntry.DataBind();
            }
            else
                lblmsg.Text = "Please Create Entry First";
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    protected void btnClear_Click1(object sender, EventArgs e)
    {
        txtRegNo.Enabled = true;
        Clear();
        txtRegNo.Text = "";
        pnlcounter.Visible = false;
        pnlCf.Visible = false;
        pnlImageViewer.Visible = false;
        lblDetails.Text = "";
        lblCuurentPage.Text = "";
        lblDetails.Text = "";
        lblEntryCompleted.Text = "";
        lblType.Text = "";
        btnSearchImg.Enabled = true;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void grdEntry_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
         index = e.RowIndex;
         Session["index"] = index;
        txtSRNo.Text = grdEntry.Rows[index].Cells[1].Text.ToString();
        txtStudentName.Text = grdEntry.Rows[index].Cells[2].Text.ToString();
        btnAdd.Text = "Save";
    }
    protected void LbtnSaveCF_Click(object sender, EventArgs e)
    {
        if (txtRollNoFrom.Text == "")
        {
            lblmsg.Text = "Please Enter Page Roll From";
            txtRollNoFrom.Focus();
            return;
        }
        else if (txtRollNoTo.Text == "")
        {
            lblmsg.Text = "Please Enter Page Roll To";
            txtRollNoTo.Focus();
            return;
        }
        try
        {
            plImg.Ind = 3;
            plImg.RegNo = Convert.ToInt64(RegNoCF.Value);
            plImg.RollNoFrom = Convert.ToInt64(txtRollNoFrom.Text);
            plImg.RollNoTo = Convert.ToInt64(txtRollNoTo.Text);
            //plImg.SName = grdEntry.Rows[k].Cells[2].Text;
            plImg.FilePath = FilePathImg.Value;
            plImg.CompletedPages = count-1;
            plImg.TotalPages = TotalPages;
            plImg.ErrorMark = 0;
            plImg.IpAddress = Request.UserHostAddress;
            plImg.UserId = Convert.ToInt32(Session["UserId"]);
            plImg.CreationDate = DateTime.Now;
            plImg.AllotmentNo = Convert.ToInt32(lblLotNo.Text);
            plImg.EntryByRegNo = Convert.ToInt64(txtRegNo.Text);
            i = dlImg.InsertCFImgEntry(plImg);
            if (i > 0)
            {
                LbtnSaveCF.Enabled = false;
                lblInd.Text = "2";
                lblmsg.Text = "Counter Foil Page Entry Completed";
            }
            else
                lblmsg.Text = "Counter Foil Page Entry Not Cumpleted";

            txtRollNoFrom.Text = "";
            txtRollNoTo.Text = "";
        }

        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    protected void btnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void btnError_Click(object sender, EventArgs e)
    {
        
    }
}

