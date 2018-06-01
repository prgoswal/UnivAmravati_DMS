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

public partial class FrmMetaDataEntry : System.Web.UI.Page
{
    DlRegister dlReg = new DlRegister(); PlRegister plReg = new PlRegister();
    //BlRegister BlReg = new BlRegister();
    DlImgEntry dlImg = new DlImgEntry(); PlImgEntry plImg = new PlImgEntry();
    DataTable dtGrd = new DataTable();

    int i, LastF, LastCF; static int TotalPages, CompletedPageNo; int insrtImg;
    DataTable dt, dt1;
    int pos; static int count; PagedDataSource adsource;    
    DataSet ds = new DataSet();
    static int frollno=0;
    static string FilePath;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (!IsPostBack)
        {
            this.ViewState["vs"] = 0;
        }
        txtRegNo.Focus();
        btnError.Visible = false;
        btnnext.Visible = false;
        btnprev.Visible = false;
        pos = (int)this.ViewState["vs"];
        lblCuurentPage.Text = "";
        this.Form.DefaultButton = btnSearchImg.UniqueID;
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
        try
        {
            pos = (int)this.ViewState["vs"];
            pos -= 1;
            this.ViewState["vs"] = pos;
            TotalPages = GetImagePath(Convert.ToInt64(txtRegNo.Text));

            count = pos + 1;
            if (Filetype.Value == "2")
            {
                count = (count / 2);
                lblCuurentPage.Text = "Page(s):-  " + count.ToString() + "/" + TotalPages.ToString();
            }
            else
            {
                count = (count / 2) + 1;
                lblCuurentPage.Text = "Page(s):-  " + count.ToString() + "/" + TotalPages.ToString();

            }
        }
        catch
        { }
    }
    public void next()
    {
        try
        {
            btnSave.Enabled = true;

            pos = (int)this.ViewState["vs"];
            pos += 1;
            this.ViewState["vs"] = pos;
            TotalPages = GetImagePath(Convert.ToInt64(txtRegNo.Text));

            count = pos + 1;
            if (Filetype.Value == "2")
            {
                count = (count / 2);
                lblCuurentPage.Text = "Page(s):-  " + count.ToString() + "/" + TotalPages.ToString();
            }
            else
            {
                count = (count / 2) + 1;
                lblCuurentPage.Text = "Page(s):-  " + count.ToString() + "/" + TotalPages.ToString();

            }

        }
        catch (Exception ex)
        {
            if (pos > TotalPages) // If All Pages Entry Completed
            {
                lblDetails.Text = "Data Entry Completed.";
                pnlcounterFoil.Visible = false;
                pnlcounter.Visible = false;
            }
            else // If Any Pages Images Not Available
                lblDetails.Text = "Current Image Not Found.";             
        }
    }

    protected void btnnext_Click(object sender, EventArgs e)
    {
        btnSave.Enabled = true;
        next();

    }
    protected void btnSearchImg_Click(object sender, EventArgs e)
    {
        Session["dt"] = null;
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
                        // count = LastF + 1;
                        if (LastF == LastCF)
                        {
                            pos = (LastF * 2);
                            ViewState["vs"] = pos;
                            lblFoilEntry.Text = "Foil Entry";
                            pnlcounterFoil.Visible = false;
                            pnlcounter.Visible = true;
                        }
                        else if (LastF > LastCF)
                        {
                            pos = (LastF * 2) - 1;
                            ViewState["vs"] = pos;
                            lblFoilEntry.Text = "Counter Foil Entry";
                            pnlcounterFoil.Visible = true;
                            pnlcounter.Visible = false;
                        }
                    }
                }
            }
            plReg.Ind = 11;
            plReg.RegNo = Convert.ToInt64(txtRegNo.Text);
            dt = dlReg.SearchRegister(plReg);
            Session["dt"] = dt;
            if (LastCF + LastF < dt.Rows.Count)
            {
                if (dt.Rows[0]["FileType"].ToString() == "1" && dt.Rows[1]["FileType"].ToString() == "2")
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtRegNo.Enabled = false;
                        lblDetails.Text = "Exam Name :- " + dt.Rows[0]["ExamName"].ToString();
                        // lblDetails.Text = "Exam Name:- Bacholer Of Engineering (Electronics and Communication Engineering Session 2014-15) - I Semester ";
                        // FilePath = dt.Rows[0]["FilePath"].ToString().Replace("/", "\\");
                        pnlImageViewer.Visible = true;

                        TotalPages = GetImagePath(Convert.ToInt64(txtRegNo.Text));
                        lblTotalPages.Text = (TotalPages).ToString();
                        // count = count == 0 ? 1 : count;
                        // lblEntryCompleted.Text = "Last Page Completed :-" + GetCompletedPageNo() + "/" + lblTotalPages.Text;
                        // int k = Convert.ToInt32(GetCompletedPageNo() + 1);
                        DataTable dt1 = new DataTable();
                        dt1 = GetCompletedPageNo();
                        int k;
                        if (dt1.Rows.Count > 0)
                        {
                            k = Convert.ToInt32(dt1.Rows[0]["CompletedPages"]);
                            lblrollnocompleted.Text = "Last Roll No. Enterd" + " " + dt1.Rows[0]["RollNo"].ToString();
                            lblEntryPage.Text = Convert.ToString(k);
                            lblCuurentPage.Text = "Current Page:-" + lblEntryPage.Text + "/" + lblTotalPages.Text;
                        }
                        else
                        {
                            k = 0;
                            lblrollnocompleted.Text = "Last Roll No. Enterd" + " " + "0";
                            lblEntryPage.Text = Convert.ToString(k + 1);
                            lblCuurentPage.Text = "Current Page:-" + lblEntryPage.Text + "/" + lblTotalPages.Text;
                        }
                        FilePath = dt.Rows[pos]["FilePath"].ToString().Replace("/", "\\");
                        //FilePath = dt.Rows[0]["FilePath"].ToString().Replace("/", "\\");
                        btnSearchImg.Enabled = false;
                        btnError.Enabled = true;
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
                else
                    lblDetails.Text = "Images Not Found According to Foil And Counter Foil";
            }
            else
                lblDetails.Text = "Data Entry Completed";
        }
        txtpageno.Focus();
        //if (dt.Rows[0]["FileType"].ToString() == "1")
        //{
        //    pnlcounter.Visible = true;
        //    pnlcounterFoil.Visible = false;
        //}
        //else
        //{
            //pnlcounter.Visible = false;
            //pnlcounterFoil.Visible = true;
        //}

    }

    private void Clear()  // Clear Textboxes and  Set DropDown Lists To SelectedIndex "0"
    {
        ViewState["AvailablePages"] = null;
        txtfrom.Text = txtto.Text  = "";
        Response.Redirect("FrmMetaDataEntry.aspx");
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
        adsource.CurrentPageIndex = pos;
        btnprev.Enabled = !adsource.IsFirstPage;
        btnnext.Enabled = !adsource.IsLastPage;

        //string s = dt1.Rows[pos]["FilePath"].ToString();
        //string str = "";
        //str = s.Substring(27, 4);// dt1.Rows[pos]["FilePath"].ToString();
        //string ss = str.Substring(0, 1);
        //if (ss == "F")
        
        if (Convert.ToInt32(dt1.Rows[pos]["FileType"].ToString()) == 1)
        {
            lblpath.Text = "FOIL REGISTRATION NO." + " : " + dt1.Rows[pos]["RegNo"].ToString();
            lblFoilEntry.Text = "Foil Entry";
        }
        else if (Convert.ToInt32(dt1.Rows[pos]["FileType"].ToString()) == 2)
        {
            lblpath.Text = "COUNTER FOIL REGISTRATION NO." + " : " + dt1.Rows[pos]["RegNo"].ToString();
            lblFoilEntry.Text = "Counter Foil Entry";
        }

        Filetype.Value = dt1.Rows[pos]["FileType"].ToString();

        RegNoCF.Value = dt1.Rows[pos]["RegNo"].ToString();

        FilePathImg.Value = dt1.Rows[pos]["FilePath"].ToString();

        dlImages.DataSource = adsource;
        dlImages.DataBind();

        return dt1.Rows.Count / 2;//here we count only foil images.
      
    }
    protected void btnClear_Click1(object sender, EventArgs e)
    {
        // txtRegNo.Enabled = true;
        Clear(); 
    }
    private string GetFilePath()
    {
        return "";
    }
    protected void btnSave_Click1(object sender, EventArgs e)
    {
        try
        {
            if (Filetype.Value == "1")
            {
                int insrtImg = 0;
                lblmsg.Text = "";
               
                long IstRollNo = 0; long LastRollNo = 0;
                frollno = 1;
                // IstRollNo = Convert.ToInt32(txtfrom.Text);
               //   count = (Convert.ToInt32(txtto.Text) - Convert.ToInt32(txtfrom.Text)) + 1;
              //   rollfrom = Convert.ToInt32(txtfrom.Text);

                foreach (GridViewRow rw in grdEntry.Rows)
                {
                  //  Label txtfoilpageno = (Label)rw.FindControl("lblRowNumber");
                    Label txtrollnofoil = (Label)rw.FindControl("lblrollno");
                  //  Label txtdelete = (Label)rw.FindControl("lnkDelete");
                    if (grdEntry.Rows.Count > 0)
                    {
                   
                        if (frollno == 1)
                        {
                            IstRollNo =Convert.ToInt64(txtrollnofoil.Text);
                        }
                        if (grdEntry.Rows.Count == frollno)
                        {
                          LastRollNo=  Convert.ToInt64(txtrollnofoil.Text);
                        }
                        plImg.Ind = 1;
                        plImg.RegNo = Convert.ToInt64(txtRegNo.Text);
                        plImg.EntryByRegNo = Convert.ToInt64(txtRegNo.Text);
                        plImg.RollNo = Convert.ToInt64(txtrollnofoil.Text);//Convert.ToInt64(txtrollnofoil);
                        plImg.SName = "";
                        plImg.AllotmentNo = 1;
                        plImg.FilePath = FilePathImg.Value;
                        // plImg.CompletedPages = count;
                        plImg.TotalPages = TotalPages;
                        plImg.ErrorMark = 0;
                        plImg.IpAddress = Request.UserHostAddress;
                        plImg.UserId = Convert.ToInt32(Session["UserId"]);
                        plImg.CreationDate = DateTime.Now;
                        plImg.PhyPageno = Convert.ToInt32(txtpagenofoil.Text);
                        insrtImg = dlImg.InserImgEntry(plImg);
                        frollno++;
                    }
                    else
                        lblmsg.Text = "Please Add Roll No. First.";
                }

                plImg.Ind = 11;
                plImg.RegNo = Convert.ToInt64(txtRegNo.Text);
                plImg.PhyPageno = Convert.ToInt32(txtpagenofoil.Text);
                plImg.RollNoFrom = Convert.ToInt64(IstRollNo);
                plImg.RollNoTo = Convert.ToInt64(LastRollNo);
                plImg.FilePath = FilePathImg.Value;
                insrtImg = dlImg.InsertFoilImgEntryPageWise(plImg);
                if (insrtImg == 1)
                    lblmsg.Text = "Foil Page Entry Completed";

                txtpagenofoil.Text = "";
                txtSRNo.Text = "";     
                btnnext.Enabled = true;
                frollno = 0;
                IstRollNo = 0;
                LastRollNo = 0;

                DataTable NullDataTable = new DataTable();
                NullDataTable = null;
                grdEntry.DataSource = NullDataTable; // I'm trying to clear gridview datasource however this is not working for me
                grdEntry.DataBind();

                //grdEntry.DataSource = null;
                //grdEntry.DataBind();
                btnnext_Click(sender, e);

                DataTable dt1 = new DataTable();
                dt1 = GetCompletedPageNo();
                if (dt1.Rows.Count > 0)
                {
                    lblrollnocompleted.Text = "Last Roll No. Enterd" + " " + dt1.Rows[0]["RollNo"].ToString();
                }
                pnlcounterFoil.Visible = true;
                pnlcounter.Visible = false;

                #region Comments
                //if (count > 0)
                //{
                //    for (int k = 0; k < count; k++)
                //    {
                //        plImg.Ind = 1;
                //        plImg.RegNo = Convert.ToInt64(txtRegNo.Text);
                //        plImg.EntryByRegNo = Convert.ToInt64(txtRegNo.Text);
                //        plImg.RollNo = Convert.ToInt64(rollfrom);
                //        plImg.SName = "";
                //        plImg.AllotmentNo = 1;
                //        plImg.FilePath = FilePathImg.Value;
                //        plImg.CompletedPages = count;
                //        plImg.TotalPages = TotalPages;
                //        plImg.ErrorMark = 0;
                //        plImg.IpAddress = Request.UserHostAddress;
                //        plImg.UserId = Convert.ToInt32(Session["UserId"]);
                //        plImg.CreationDate = DateTime.Now;
                //        plImg.PhyPageno = Convert.ToInt32(txtpageno.Text);
                //        insrtImg = dlImg.InserImgEntry(plImg);


                //        if (insrtImg == 1)
                //        {
                //            lblmsg.Text = "Foil Page Entry Completed";

                //        }
                //        else
                //        {
                //            lblmsg.Text = "Roll No. - " + plImg.RollNo + " Is Already Exists";
                //            return;
                //        }
                //        LastRollNo = rollfrom;
                //        rollfrom++;
                //    }

                //    //For Foil Page Wise Single Entry Having RegNo.+PageNo.+IstRollNo+LastRollNo Into Table
                //    plImg.Ind = 11;
                //    plImg.RegNo = Convert.ToInt64(txtRegNo.Text);
                //    plImg.PhyPageno = Convert.ToInt32(txtpageno.Text);
                //    plImg.RollNoFrom = Convert.ToInt64(IstRollNo);
                //    plImg.RollNoTo = Convert.ToInt64(LastRollNo);
                //    plImg.FilePath = FilePathImg.Value;
                //    insrtImg = dlImg.InsertFoilImgEntryPageWise(plImg);
                //    if (insrtImg == 1)
                //        lblmsg.Text = "Foil Page Entry Completed";
                //    //////////Changes Done By Ashish On 07/11/2016 @ 11 AM

                //    // pnlcounter.Visible = false;
                //    //  pnlCf.Visible = true;
                //    //  btnSave.Visible = false;
                //    //   LbtnSaveCF.Visible = true;
                //    lblFoilEntry.Text = "Counter Foil";
                //    txtpageno.Text = "";
                //    txtfrom.Text = "";
                //    txtto.Text = "";
                //    txtpageno.Focus();
                //    btnnext.Enabled = true;
                //    btnnext_Click(sender, e);

                //    DataTable dt1 = new DataTable();
                //    dt1 = GetCompletedPageNo();
                //    if (dt1.Rows.Count > 0)
                //    {
                //        lblrollnocompleted.Text = "Last Roll No. Enterd" + " " + dt1.Rows[0]["RollNo"].ToString();
                //    }


                //}
                //else
                //{
                //    lblmsg.Text = "Roll No From Must Be Less Than of Roll No To";
                //}
                ////next();
                //lblCuurentPage.Text = "Page(s):-  " + count.ToString() + "/" + TotalPages.ToString();
                #endregion
                ViewState["AvailablePages"] = null;//this code done  
            }
            else
            {
                if (txtfrom.Text == "")
                {
                    lblmsg.Text = "Please Enter Page Roll From";
                    txtfrom.Focus();
                    return;
                }
                else if (txtto.Text == "")
                {
                    lblmsg.Text = "Please Enter Page Roll To";
                    txtto.Focus();
                    return;
                }
                else if (Convert.ToInt32(txtto.Text) < Convert.ToInt32(txtfrom.Text))
                {
                    lblmsg.Text = "Roll No From Must Be Less Than of Roll No To";
                    txtfrom.Focus();
                    //  pnlcounter.Visible = false;
                    return;
                }
                else
                {
                    
                    plImg.Ind = 3;
                    plImg.RegNo = Convert.ToInt64(RegNoCF.Value);
                    plImg.RollNoFrom = Convert.ToInt64(txtfrom.Text);
                    plImg.RollNoTo = Convert.ToInt64(txtto.Text);
                    //plImg.SName = grdEntry.Rows[k].Cells[2].Text;
                    plImg.SName = "";
                    plImg.PhyPageno = Convert.ToInt64(txtpageno.Text);
                    plImg.FilePath = FilePathImg.Value;
                    plImg.CompletedPages = count - 1;
                    plImg.TotalPages = TotalPages;//Convert.ToInt32(lblTotalPages.Text);
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
                    if (i == 1)
                    {
                        lblmsg.Text = "Counter Foil Page Entry Completed";
                        lblFoilEntry.Text = "Foil Entry";
                        txtpageno.Text = "";
                        txtfrom.Text = "";
                        txtto.Text = "";
                        txtpageno.Focus();
                        btnSave.Enabled = true;
                        btnnext_Click(sender, e);
                        pnlcounterFoil.Visible = false;
                        pnlcounter.Visible = true;
                    }
                    else
                    {
                        lblmsg.Text = "Roll No. - " + plImg.RollNo + " Is Already Exists";
                        return;
                    }
                }
                ViewState["AvailablePages"] = null;
            }

        }
        catch (Exception ex)
        {
            if (ex != null)
            {
                lblmsg.Text = ex.Message;//"Roll No. Already Exist";
            }
        }
    }
    private void AddRowsToGrid()
    {
        if (ViewState["AvailablePages"] == null)
        {
            dtGrd.Columns.Add("RollNo", typeof(Int64));
            // dtGrd.Columns.Add("SName", typeof(string));
            for (int k = 0; k < grdEntry.Rows.Count; k++)
            {
                dtGrd.Rows.Add(grdEntry.Rows[k].Cells[1].Text.ToString());
                //  grdEntry.Rows[k].Cells[2].Text.ToString().ToUpper() );                   
            }
            ViewState["AvailablePages"] = dtGrd;
        }
        else
        {
            dtGrd = (DataTable)ViewState["AvailablePages"];
        }
        if (!string.IsNullOrEmpty(txtSRNo.Text))
        {
            dtGrd.Rows.Add(Convert.ToInt64(txtSRNo.Text));
            grdEntry.DataSource = dtGrd;
            grdEntry.DataBind();
            ViewState["AvailablePages"] = dtGrd;
          //  dtGrd = null;
        }
    }
    protected void LinkDeleteAll_Click(object sender, EventArgs e)
    {

        //AddRowsToGrid();
        DataTable dtl = new DataTable();

        int pos = 0;
        dtl = (DataTable)ViewState["AvailablePages"];
        for (int i = 0; i < grdEntry.Rows.Count; i++)
        {
            CheckBox check = (CheckBox)grdEntry.Rows[i].FindControl("ChkGridRow");
            if (check != null && check.Checked)
            {
                dtl.Rows[pos].Delete();
                //grdEntry.DeleteRow(pos);
            }
            else
            {
                pos++;
            }
        }
        grdEntry.DataSource = dtl;
        grdEntry.DataBind();


    }
    protected void grdEntry_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        AddRowsToGrid();
        //dtGrd = (DataTable)ViewState["AvailablePages"];
        //grdEntry.DataSource = dtGrd;
        //grdEntry.DataBind();

        foreach (GridViewRow dr in grdEntry.Rows)
        {
            CheckBox chkBx = (CheckBox)dr.FindControl("ChkGridRow");
            if (chkBx.Checked == true)
            {
                dtGrd = (DataTable)ViewState["AvailablePages"];
                dtGrd.Rows.Remove(dtGrd.Rows[e.RowIndex]);
                grdEntry.DataSource = dtGrd;
                grdEntry.DataBind();
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (btnAdd.Text == "Add")
        {
            if (txtSRNo.Text == "")
            {
                lblmsg.Text = "Please Enter Student Roll No.";
                txtSRNo.Focus();
                return;
            }

            AddRowsToGrid();
        }
        else
        {
            grdEntry.Rows[Convert.ToInt32(Session["index"])].Cells[1].Text = txtSRNo.Text;
            //  grdEntry.Rows[Convert.ToInt32(Session["index"])].Cells[2].Text = txtStudentName.Text.ToUpper();
            dtGrd.Rows.Clear();
            dtGrd.Columns.Clear();
            dtGrd.Columns.Add("StudentRollNo", typeof(Int64));
            
            // dtGrd.Columns.Add("StudentName", typeof(string));
            for (int k = 0; k < grdEntry.Rows.Count; k++)
            {

                dtGrd.Rows.Add(Convert.ToInt64(grdEntry.Rows[k].Cells[1].Text)
                    //(grdEntry.Rows[k].Cells[2]).Text.ToString().ToUpper()
                    );
            }
            ViewState["AvailablePages"] = dtGrd;
            btnAdd.Text = "Add";
        }
        txtSRNo.Text = "";
        //Clear();
        txtSRNo.Focus();
        Session["index"] = null;

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
    }
    protected void LbtnSaveCF_Click(object sender, EventArgs e)
    {
    }
    protected void btnError_Click(object sender, EventArgs e)
    {       
    }
    protected void dlImages_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
}