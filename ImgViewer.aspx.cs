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

    int i, LastF, LastCF; int TotalPages, CompletedPageNo; int insrtImg;
    DataTable dt, dt1; DataSet ds; //SqlDataAdapter da; SqlCommand cmd;
    int pos; static int count; PagedDataSource adsource; static string filepathforCF = "";
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    static string FilePath; int index;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ViewState["vs"] = 0;
            //pos = 0;
            pos = (int)this.ViewState["vs"];

        }
        //  pnlCf.Visible = true;
        //btnnext.Visible = false;
        pnlfoilentry.Visible = true;
        // btnAdd.Enabled = true;
        btnSave.Enabled = true;
        LbtnSaveCF.Enabled = true;
        // lblCuurentPage.Text = "";
        this.Form.DefaultButton = btnSearchImg.UniqueID;
    }
    //private void AddRowsToGrid()
    //{
    //    int from = Convert.ToInt32(txtfrom.Text);
    //    int to = Convert.ToInt32(txtto.Text);
    //    int count = (to - from) + 1;
    //    if (ViewState["AvailablePages"] == null)
    //    {
    //        dtGrd.Columns.Add("StudentRollNo", typeof(Int64));
    //        //  dtGrd.Columns.Add("StudentName", typeof(string));
    //        dtGrd.Columns.Add("PhyPageno", typeof(Int64));
    //        for (int k = 0; k < count; k++)
    //        {
    //            //dtGrd.Rows.Add(k, Convert.ToInt32(grdEntry.DataKeys[k].Values["StudentRollNo"].ToString()),
    //            // Convert.ToString(grdEntry.DataKeys[k].Values["StudentName"].ToString().ToUpper()),
    //            //  Convert.ToInt64(grdEntry.DataKeys[k].Values["PhyPageno"].ToString().ToUpper()));
    //        }
    //    }
    //    else
    //    {
    //        dtGrd = (DataTable)ViewState["AvailablePages"];
    //    }

    //if (!string.IsNullOrEmpty(txtfrom.Text) && !string.IsNullOrEmpty(txtto.Text))
    //{
    //    // dtGrd.Rows.Add(Convert.ToInt64(txtSRNo.Text), txtStudentName.Text.ToString().ToUpper());

    //    if (count + 1 < 12)
    //    {
    //        for (int i = 1; i <= count + 1; i++)
    //        {
    //            dtGrd.Rows.Add(Convert.ToInt64(from), Convert.ToInt64(txtpageno.Text));

    //            grdEntry.DataSource = dtGrd;
    //            grdEntry.DataBind();
    //            //  ViewState["AvailablePages"] = dtGrd;
    //            //   dtGrd = null;
    //            from++;
    //        }
    //    }
    //    else
    //    {
    //        lblmsg.Text = "Roll No. Difference Does Not Allow Greater than 12";
    //    }
    //}
    // }
    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    if (btnAdd.Text == "Add")
    //    {

    //        int to = Convert.ToInt32(txtto.Text);
    //        int from = Convert.ToInt32(txtfrom.Text);

    //        if (txtto.Text == "" && txtfrom.Text=="")
    //        {
    //            lblmsg.Text = "Please Enter Student Roll No.";
    //            txtfrom.Focus();
    //            return;
    //        }             
    //        else if (to >from || to==from)
    //        {
    //            AddRowsToGrid();
    //           // lblmsg.Text = "";
    //        }
    //        else {
    //            lblmsg.Text = "Please check ToRollNo. must to greater from FromRollNo.";
    //        }


    //        //else if (txtStudentName.Text == "")
    //        //{
    //        //    lblmsg.Text = "Please Enter Student Name";
    //        //    txtStudentName.Focus();
    //        //    return;
    //        //}

    //        //lblCuurentPage.Text = "Current Page:-  " + count.ToString() + "/" + lblTotalPages.Text;
    //        //lblEntryCompleted.Text = "Page Entry Completed :-" + GetCompletedPageNo() + "/" + lblTotalPages.Text;

    //    }
    //    else
    //    {
    //     //   grdEntry.Rows[Convert.ToInt32(Session["index"])].Cells[1].Text = txtfrom.Text;
    //     //   grdEntry.Rows[Convert.ToInt32(Session["index"])].Cells[2].Text =txtpageno.Text;
    //     //  // grdEntry.Rows[Convert.ToInt32(Session["index"])].Cells[2].Text = txtStudentName.Text.ToUpper();

    //     //   dtGrd.Rows.Clear();
    //     //   dtGrd.Columns.Clear();
    //     //   dtGrd.Columns.Add("StudentRollNo", typeof(Int64));
    //     ////   dtGrd.Columns.Add("StudentName", typeof(string));
    //     //   dtGrd.Columns.Add("PhyPageno", typeof(Int64));
    //     //   for (int k = 0; k < grdEntry.Rows.Count; k++)
    //     //   {
    //     //       dtGrd.Rows.Add(Convert.ToInt64(grdEntry.Rows[k].Cells[1].Text),
    //     //           (grdEntry.Rows[k].Cells[2]).Text.ToString().ToUpper());
    //     //   }
    //     //   ViewState["AvailablePages"] = dtGrd;
    //     //   btnAdd.Text = "Add";
    //    }

    //    Clear();
    //    txtfrom.Focus();
    //    Session["index"] = null;

    //}
    private DataTable GetCompletedPageNo()
    {
        DataTable dt = new DataTable();
        plImg.Ind = 2;
        plImg.RegNo = Convert.ToInt64(txtRegNo.Text);
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
        //TotalPages = TotalPages / 2;
        count = pos + 1;
        lblCuurentPage.Text = "Page(s):-  " + count.ToString() + "/" + lblTotalPages.Text;

    }
    //protected void btnnext_Click(object sender, EventArgs e)
    //{
    //    lblmsg.Text = "";

    //    if (Filetype.Value == lblInd.Text && lblInd.Text == "2")
    //    {
    //        radioCF.Checked = false;
    //        radioFoil.Checked = true;
    //        lblFoilEntry.Text = "Foil Entry";
    //        pnlcounter.Visible = true;
    //        pnlCf.Visible = false;
    //        btnError.Enabled = true;
    //        pos = (int)this.ViewState["vs"];
    //        pos += 1;
    //        this.ViewState["vs"] = pos;
    //        TotalPages = GetImagePath(Convert.ToInt64(txtRegNo.Text));
    //        //lblTotalPages.Text = (TotalPages / 2).ToString();
    //        // CompletedPageNo += 1 == 1 ? 0 : CompletedPageNo;
    //        btnSave.Enabled = true;
    //       // btnAdd.Enabled = true;
    //        lblCuurentPage.Text = "Current Page:-  " + lblEntryPage.Text + "/" + lblTotalPages.Text;
    //        lblEntryCompleted.Text = "Page Entry Completed :-" + GetCompletedPageNo() + "/" + lblTotalPages.Text;
    //    }

    //    else if (Filetype.Value == lblInd.Text && lblInd.Text == "1")
    //    {
    //        radioCF.Checked = true;
    //        radioFoil.Checked = false;
    //        lblFoilEntry.Text = "Counter Foil Entry";
    //        pnlcounter.Visible = false;
    //        pnlCf.Visible = true;
    //        btnError.Enabled = true;
    //        pos = (int)this.ViewState["vs"];
    //        // pos= pos == 0 ? 1 : pos;
    //        pos += 1;
    //        this.ViewState["vs"] = pos;
    //        TotalPages = GetImagePath(Convert.ToInt64(txtRegNo.Text));
    //        //TotalPages = TotalPages / 2;
    //        lblEntryPage.Text = (Convert.ToInt32(lblEntryPage.Text) + 1).ToString();
    //        LbtnSaveCF.Enabled = true;
    //        //lblCuurentPage.Text = "Current Page:-  " + (count).ToString() + "/" + TotalPages.ToString();

    //    }
    //    else
    //    {
    //        if (Filetype.Value == "1")
    //            pnlCf.Visible = false;
    //        if (Filetype.Value == "2")
    //            pnlcounter.Visible = false;

    //        lblmsg.Text = "Please Create Entry First";
    //        return;
    //    }
    //    // lblmsg.Text = Filetype.Value.ToString();

    //}
    protected void btnSearchImg_Click(object sender, EventArgs e)
    {
        Session["dt"] = null;
        pnlfoilentry.Visible = true;
        count = 0;
        try
        {
            if (string.IsNullOrEmpty(txtRegNo.Text))
            {
                lblDetails.Text = "Please Enter Register No.";
                txtRegNo.Focus();
                //dlImages.DataSource = null;
                //dlImages.DataBind();
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
                            // count = LastF + 1;
                            if (LastF == LastCF)
                            {
                                pos = (LastF * 2);
                                ViewState["vs"] = pos;
                                pnlfoilentry.Visible = true;
                                pnlCf.Visible = false;

                            }
                            else if (LastF > LastCF)
                            {
                                pos = (LastF * 2) - 1;
                                ViewState["vs"] = pos;
                                //count = LastF+1;
                                lblFoilEntry.Text = "Counter Foil Entry";
                                pnlfoilentry.Visible = false;
                                pnlCf.Visible = true;

                            }
                        }
                    }
                }
                plReg.Ind = 11;
                plReg.RegNo = Convert.ToInt64(txtRegNo.Text);
                dt = dlReg.SearchRegister(plReg);
                Session["dt"] = dt;
                if (dt.Rows[0]["FileType"].ToString() == "1" && dt.Rows[1]["FileType"].ToString() == "2")
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtRegNo.Enabled = false;

                        lblDetails.Text = "EXAM NAME :- " + dt.Rows[0]["ExamName"].ToString();
                        // lblDetails.Text = "Exam Name:- Bacholer Of Engineering (Electronics and Communication Engineering Session 2014-15) - I Semester ";
                        FilePath = dt.Rows[0]["FilePath"].ToString().Replace("/", "\\");
                        pnlImageViewer.Visible = true;
                        TotalPages = GetImagePath(Convert.ToInt64(txtRegNo.Text));
                        lblTotalPages.Text = (TotalPages / 2).ToString();
                        // count = count == 0 ? 1 : count;
                        lblEntryCompleted.Text = "Last Page Completed :-" + GetCompletedPageNo() + "/" + lblTotalPages.Text;
                        if (LastF > LastCF)
                        {
                            lblEntryPage.Text = (CompletedPageNo).ToString();
                            lblCuurentPage.Text = "Current Page:-" + lblEntryPage.Text + "/" + lblTotalPages.Text;
                        }
                        else
                        {
                            lblEntryPage.Text = (CompletedPageNo + 1).ToString();
                            lblCuurentPage.Text = "Current Page:-" + lblEntryPage.Text + "/" + lblTotalPages.Text;
                        }
                        lblType.Text = "TR Type";
                        //pnlcounter.Visible = true;
                        btnSearchImg.Enabled = false;
                        btnError.Enabled = true;
                    }
                    else
                    {
                        //dlImages.DataSource = null;
                        //dlImages.DataBind();
                        lblDetails.Text = "No Record Found";
                        lblCount.Text = "";
                        pnlImageViewer.Visible = false;
                    }
                }

                else
                    lblDetails.Text = "Images Not Found According to Foil And Counter Foil";
            }
        }
        catch (Exception ex)
        {
            lblDetails.Text = ex.Message;
        }
    }

    private void Clear()  // Clear Textboxes and  Set DropDown Lists To SelectedIndex "0"
    {
        // lblmsg.Text = txtStudentName.Text = txtSRNo.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["AvailablePages"] = null;
    }
    private int GetImagePath(Int64 RegNo)
    {
        //dt1 = new DataTable();
        //dt1 = (DataTable)Session["dt"];

        //adsource = new PagedDataSource();
        //adsource.DataSource = dt1.DefaultView;
        //adsource.PageSize = 1;
        //adsource.AllowPaging = true;
        //adsource.CurrentPageIndex = pos == 0 ? 1 : pos;
        //Filetype.Value = dt1.Rows[pos]["FileType"].ToString();
        //RegNoCF.Value = dt1.Rows[pos]["RegNo"].ToString();
        //FilePathImg.Value = dt1.Rows[pos]["FilePath"].ToString();
        //btnprev.Enabled = !adsource.IsFirstPage;
        //// btnnext.Enabled = !adsource.IsLastPage;
        //dlImages.DataSource = adsource;
        //dlImages.DataBind();

        //return dt1.Rows.Count;
        dt1 = new DataTable();
        dt1 = (DataTable)Session["dt"];
        adsource = new PagedDataSource();
        adsource.DataSource = dt1.DefaultView;
        adsource.PageSize = 1;
        adsource.AllowPaging = true;
        adsource.CurrentPageIndex = pos;
        Filetype.Value = dt1.Rows[pos]["FileType"].ToString();
        RegNoCF.Value = dt1.Rows[pos]["RegNo"].ToString();
        FilePathImg.Value = dt1.Rows[pos]["FilePath"].ToString();
        lblpath.Text = FilePathImg.Value;

        FilePath = dt1.Rows[pos]["FilePath"].ToString().Replace("/", "\\");
        imgfile.ImageUrl = FilePathImg.Value;
        //btnprev.Enabled = !adsource.IsFirstPage;
        //btnnext.Enabled = !adsource.IsLastPage;
        //dlImages.DataSource = adsource;
        //dlImages.DataBind();
        return dt1.Rows.Count;
    }

    private string GetFilePath()
    {
        return "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //    AddRowsToGrid();                
        lblmsg.Text = "";
        try
        {
            long rollfrom = 0;
            count = (Convert.ToInt32(txtto.Text) - Convert.ToInt32(txtfrom.Text)) + 1;
            rollfrom = Convert.ToInt32(txtfrom.Text);
            for (int k = 0; k < count; k++)
            {
                plImg.Ind = 1;
                plImg.RegNo = Convert.ToInt64(RegNoCF.Value);
                plImg.RollNo = Convert.ToInt64(rollfrom);
                plImg.PhyPageno = Convert.ToInt64(txtpageno.Text);
                plImg.SName = "";
                plImg.FilePath = FilePathImg.Value;
                plImg.CompletedPages = count;
                plImg.TotalPages = Convert.ToInt32(lblTotalPages.Text);
                plImg.ErrorMark = 0;
                plImg.IpAddress = Request.UserHostAddress;
                plImg.UserId = Convert.ToInt32(Session["UserId"]);
                plImg.CreationDate = DateTime.Now;
                //    plImg.AllotmentNo = Convert.ToInt32(lblLotNo.Text);
                plImg.AllotmentNo = 1;
                plImg.EntryByRegNo = Convert.ToInt64(txtRegNo.Text);
                plImg.IsUpdated = 0;
                plImg.UpdatedBy = 0;
                plImg.UpdationDate = DateTime.Now;
                insrtImg = dlImg.InserImgEntry(plImg);
                if (insrtImg >= 0)
                {
                    lblmsg.Text = "Foil Page Entry Completed";
                    lblInd.Text = "1";
                    btnSave.Enabled = false;
                    //  btnAdd.Enabled = false;
                    //pnlCf.Visible = true;
                    //pnlcounter.Visible = false;
                }
                else
                {
                    lblmsg.Text = "Roll No. - " + plImg.RollNo + " Is Already Exists";
                    return;
                }
                rollfrom++;
            }

            ViewState["AvailablePages"] = null;

            next();

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
        pnlfoilentry.Visible = false;
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
        // txtSRNo.Text = grdEntry.Rows[index].Cells[1].Text.ToString();
        //  txtStudentName.Text = grdEntry.Rows[index].Cells[2].Text.ToString();
        // btnAdd.Text = "Save";
    }
    public void next()
    {

        //lblmsg.Text = "";

        if (lblInd.Text =="2")
        {
            //  radioCF.Checked = false;
            //  radioFoil.Checked = true;
            lblFoilEntry.Text = "Foil Entry";
            pnlfoilentry.Visible = true;
            pnlCf.Visible = false;
            btnError.Enabled = true;
            pos = (int)this.ViewState["vs"];
            pos += 1;
            this.ViewState["vs"] = pos;
            TotalPages = GetImagePath(Convert.ToInt64(txtRegNo.Text));
            count = pos + 1;
            lblCuurentPage.Text = "Page(s):-  " + count.ToString() + "/" + TotalPages.ToString();
        }

        else if (lblInd.Text == "1")
        {
            //   radioCF.Checked = true;
            //  radioFoil.Checked = false;
            lblFoilEntry.Text = "Counter Foil Entry";
            pnlfoilentry.Visible = false;
            pnlCf.Visible = true;
            btnError.Enabled = true;
            pos = (int)this.ViewState["vs"];
            pos += 1;
            this.ViewState["vs"] = pos;
            TotalPages = GetImagePath(Convert.ToInt64(txtRegNo.Text));
            count = pos + 1;
            lblCuurentPage.Text = "Page(s):-  " + count.ToString() + "/" + TotalPages.ToString();

        }
        else
        {
            if (Filetype.Value == "1")
            {
                pnlCf.Visible = true;
                pnlfoilentry.Visible = false ;
            }
            else if (Filetype.Value == "2")
            {
                pnlfoilentry.Visible = true;
                pnlCf.Visible = false ;
            }

            return;
        }

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
        else if (Convert.ToInt32(txtRollNoTo.Text) < Convert.ToInt32(txtRollNoFrom.Text))
        {
            lblmsg.Text = "Roll No From Must Be Less Than of Roll No To";
            txtRollNoFrom.Focus();
            pnlfoilentry.Visible = false;
            return;
        }
        try
        {
            plImg.Ind = 3;
            plImg.RegNo = Convert.ToInt64(RegNoCF.Value);
            plImg.RollNoFrom = Convert.ToInt64(txtRollNoFrom.Text);
            plImg.RollNoTo = Convert.ToInt64(txtRollNoTo.Text);
            //plImg.SName = grdEntry.Rows[k].Cells[2].Text;
            plImg.SName = "";
            plImg.PhyPageno = Convert.ToInt64(txtCFpageno.Text);
            plImg.FilePath = FilePathImg.Value;
            plImg.CompletedPages = count - 1;
            plImg.TotalPages = Convert.ToInt32(lblTotalPages.Text);
            plImg.ErrorMark = 0;
            plImg.IpAddress = Request.UserHostAddress;
            plImg.UserId = Convert.ToInt32(Session["UserId"]);
            plImg.CreationDate = DateTime.Now;
            //  plImg.AllotmentNo = Convert.ToInt32(lblLotNo.Text);
            plImg.AllotmentNo = 1;
            plImg.EntryByRegNo = Convert.ToInt64(txtRegNo.Text);
            plImg.IsUpdated = 0;
            plImg.UpdatedBy = 0;
            plImg.UpdationDate = DateTime.Now;
            i = dlImg.InsertCFImgEntry(plImg);
            if (i > 0)
            {
                LbtnSaveCF.Enabled = false;
                pnlfoilentry.Visible = true;
                pnlCf.Visible = false;
                lblInd.Text = "2";
                lblmsg.Text = "Counter Foil Page Entry Completed";
                //txtRollNoFrom.Text = "";
                //txtRollNoTo.Text = "";
            }
            else
                lblmsg.Text = "Counter Foil Page Entry Not Cumpleted";

            txtRollNoFrom.Text = "";
            txtRollNoTo.Text = "";

            next();

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
        try
        {
            if (txtRegNo.Text == RegNoCF.Value)
            
            {
                plImg.Ind = 1;
                plImg.RegNo = Convert.ToInt64(RegNoCF.Value);
                plImg.RollNo = 0;
                plImg.SName = "";
                plImg.FilePath = FilePathImg.Value;
                plImg.CompletedPages = count;
                plImg.TotalPages = Convert.ToInt32(lblTotalPages.Text);
                plImg.ErrorMark = 1;
                plImg.IpAddress = Request.UserHostAddress;
                plImg.UserId = Convert.ToInt32(Session["UserId"]);
                plImg.CreationDate = DateTime.Now;
                //  plImg.AllotmentNo = Convert.ToInt32(lblLotNo.Text);
                plImg.AllotmentNo = 1;
                plImg.EntryByRegNo = Convert.ToInt64(txtRegNo.Text);
                plImg.IsUpdated = 0;
                plImg.UpdatedBy = 0;
                plImg.UpdationDate = DateTime.Now;
                insrtImg = dlImg.InserImgEntry(plImg);
                if (insrtImg >= 0)
                {
                    lblmsg.Text = "Foil Page Entry Completed";
                    lblInd.Text = "1";
                    btnSave.Enabled = false;
                    // btnAdd.Enabled = false;
                    pnlCf.Visible = false;
                    btnError.Enabled = false;
                }
                else
                {
                    lblmsg.Text = "Roll No. - " + plImg.RollNo + " Is Already Exists";
                    return;
                }
            }
            else
            {
                plImg.Ind = 3;
                plImg.RegNo = Convert.ToInt64(RegNoCF.Value) + 1;
                plImg.RollNoFrom = 0;
                plImg.RollNoTo = 0;
                plImg.FilePath = FilePathImg.Value;
                plImg.CompletedPages = count - 1;
                plImg.TotalPages = Convert.ToInt32(lblTotalPages.Text);
                plImg.ErrorMark = 1;
                plImg.IpAddress = Request.UserHostAddress;
                plImg.UserId = Convert.ToInt32(Session["UserId"]);
                plImg.CreationDate = DateTime.Now;
                plImg.AllotmentNo = Convert.ToInt32(lblLotNo.Text);
                plImg.EntryByRegNo = Convert.ToInt64(txtRegNo.Text);
                plImg.IsUpdated = 0;
                plImg.UpdatedBy = 0;
                plImg.UpdationDate = DateTime.Now;
                i = dlImg.InsertCFImgEntry(plImg);
                if (i > 0)
                {
                    LbtnSaveCF.Enabled = false;
                    pnlfoilentry.Visible = false;
                    lblInd.Text = "2";
                    lblmsg.Text = "Counter Foil Page Entry Completed";
                    btnError.Enabled = false;
                }
                else
                    lblmsg.Text = "Counter Foil Page Entry Not Cumpleted";

                txtRollNoFrom.Text = "";
                txtRollNoTo.Text = "";
            }
        }

        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }

    protected void dlImages_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnnext_Click(object sender, EventArgs e)
    {

        pos = (int)this.ViewState["vs"];
        pos += 1;
        this.ViewState["vs"] = pos;
        TotalPages = GetImagePath(Convert.ToInt64(txtRegNo.Text));
        count = pos + 1;
        lblCuurentPage.Text = "Page(s):-  " + count.ToString() + "/" + TotalPages.ToString();


    }
}

