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
using System.Text;

public partial class FrmInsertDataPending : System.Web.UI.Page
{
    DlRegister dlReg = new DlRegister(); PlRegister plReg = new PlRegister();
    BlRegister BlReg = new BlRegister();
    DlImgEntry dlImg = new DlImgEntry(); PlImgEntry plImg = new PlImgEntry();
    DataTable dtGrd = new DataTable();

    int i, LastF, LastCF; static int TotalPages, CompletedPageNo;
    DataTable dt, dt1; DataSet ds; //SqlDataAdapter da; SqlCommand cmd;
    int pos; static int count; PagedDataSource adsource;
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    static string FilePath; int index;
    int verify;
    static int frollno = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ViewState["vs"] = 0;
            pos = (int)this.ViewState["vs"];
        }
        btnAdd.Enabled = true;
        btnUp.Visible = btnDown.Visible = grdEntry.Rows.Count > 1 ? true : false;

        this.Form.DefaultButton = btnSearchImg.UniqueID;//22122016
    }

    void AddRowsToGrid()
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
            grdEntry.DataSource = ViewState["grdEntry"] = dtGrd;
            grdEntry.DataBind();
            ViewState["AvailablePages"] = dtGrd;
            dtGrd = null;
        }
    }
    int GetImagePath(int RegNo)
    {
        dt = new DataTable();
        dt = (DataTable)Session["dtFoil"];

        adsource = new PagedDataSource();
        adsource.DataSource = dt.DefaultView;
        adsource.PageSize = 1;
        adsource.AllowPaging = true;
        //pos = (pos - 1);
        adsource.CurrentPageIndex = pos;
        //Filetype.Value = dt.Rows[pos]["FileType"].ToString();
        RegNoFoil.Value = dt.Rows[pos]["RegNo"].ToString();
        FilePathImg.Value = dt.Rows[pos]["FilePath"].ToString();
        dlImages.DataSource = adsource;
        dlImages.DataBind();
        return dt.Rows.Count;
    }
    int GetImagePathCF(int RegNo)
    {
        dt1 = new DataTable();
        dt1 = (DataTable)Session["dtCF"];

        adsource = new PagedDataSource();
        adsource.DataSource = dt1.DefaultView;
        adsource.PageSize = 1;
        adsource.AllowPaging = true;
        adsource.CurrentPageIndex = pos;
        //Filetype.Value = dt1.Rows[pos]["FileType"].ToString();
        RegNoCF.Value = dt1.Rows[pos]["RegNo"].ToString();
        FilePathImgCF.Value = dt1.Rows[pos]["FilePath"].ToString();
        dlImagesCF.DataSource = adsource;
        dlImagesCF.DataBind();
        return dt1.Rows.Count;
    }
    DataTable GetCompletedPageNo()
    {
        plImg.Ind = 2;
        plImg.RegNo = Convert.ToInt64(Session["RegNo"]);
        DataTable dt = new DataTable();
        dt = dlImg.GetICompetedPages(plImg);
        return dt;
    }

    protected void btnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void btnSearchImg_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";        
        Session["dt"] = null;
        count = 0;
        lblDetails.Text = "";
        grdEntry.DataSource = ViewState["grdEntry"] = null;
        grdEntry.DataBind();
        try
        {
            if (string.IsNullOrEmpty(txtRegNo.Text))
            {
                lblDetails.Text = "Please Enter Register No.";
                return;
            }
            else if (string.IsNullOrEmpty(txtPageNo.Text))
            {
                lblDetails.Text = "Please Enter Page No.";
                txtRegNo.Focus();
                dlImages.DataSource = null;
                dlImages.DataBind();
                return;
            }
            else //if (!string.IsNullOrEmpty(txtRegNo.Text))
            {
                plReg.Ind = 13;
                plReg.RegNo = Convert.ToInt64(txtRegNo.Text);
                plReg.PageNo = Convert.ToInt32(txtPageNo.Text);
                ds = BlReg.ShowBothImages(plReg);

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    lblFoil.Text =   "Foil Entry - " + ds.Tables[0].Rows[0]["RegNo"].ToString();
                    lblCF.Text =     "Counter Foil Entry - " + ds.Tables[1].Rows[0]["RegNo"].ToString();
                    Session["dtFoil"] = ds.Tables[0];
                    Session["UserIdF"] = ds.Tables[0].Rows[0]["UserId"].ToString();
                    Session["CreationDateF"] = ds.Tables[0].Rows[0]["CreationDate"].ToString();

                    Session["dtCF"] = ds.Tables[1];
                    Session["UserIdCF"] = ds.Tables[1].Rows[0]["UserId"].ToString();
                    Session["CreationDateCF"] = ds.Tables[1].Rows[0]["CreationDate"].ToString();

                    //pos = Convert.ToInt32(txtPageNo.Text);
                    //ViewState["vs"] = pos;
                    //lblDetails.Text = "Exam Name :- " + dt.Rows[0]["ExamName"].ToString();
                    pnlImageViewer.Visible = true;
                    GetImagePath(Convert.ToInt32(txtPageNo.Text));
                    GetImagePathCF(Convert.ToInt32(txtPageNo.Text));
                    btnSearchImg.Enabled = false;
                    ds.Tables[0].Columns.RemoveAt(0);
                    ds.Tables[0].Columns.RemoveAt(0);
                    ds.Tables[0].Columns.RemoveAt(2);
                    grdEntry.DataSource = ViewState["grdEntry"] = ds.Tables[0];
                    grdEntry.DataBind();

                    ds.Tables[1].Columns.RemoveAt(0);
                    ds.Tables[1].Columns.RemoveAt(0);
                    ds.Tables[1].Columns.RemoveAt(2);

                    txtRollNoFrom.Text = ds.Tables[1].Rows[0][0].ToString();
                    txtRollNoTo.Text = ds.Tables[1].Rows[0][1].ToString();
                    pnlcounter.Visible = true;
                    pnlCf.Visible = true;
          
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Session["dtFoil"] = ds.Tables[0];
                        Session["UserIdF"] = ds.Tables[0].Rows[0]["UserId"].ToString();
                        Session["CreationDateF"] = ds.Tables[0].Rows[0]["CreationDate"].ToString();
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        Session["dtCF"] = ds.Tables[1];
                        Session["UserIdCF"] = ds.Tables[1].Rows[0]["UserId"].ToString();
                        Session["CreationDateCF"] = ds.Tables[1].Rows[0]["CreationDate"].ToString();
                    }
                    plReg.Ind = 23;//For Insert Pending Entry.
                    plReg.RegNo = Convert.ToInt64(txtRegNo.Text);
                    plReg.PageNo = Convert.ToInt32(txtPageNo.Text);
                    dt = new DataTable();
                    dt = dlReg.ShowImagesFoilandCfoil(plReg);
                    if (dt.Rows.Count>0)
                    {
                        adsource = new PagedDataSource();
                        adsource.DataSource = dt.DefaultView;
                        adsource.PageSize = 1;
                        adsource.AllowPaging = true;
                        adsource.CurrentPageIndex = pos;
                        RegNoFoil.Value = dt.Rows[0]["RegNo"].ToString();
                        FilePathImg.Value = dt.Rows[0]["FilePath"].ToString();
                        dlImages.DataSource = adsource;
                        dlImages.DataBind();                 

                        adsource = new PagedDataSource();
                        adsource.DataSource = dt.DefaultView;
                        adsource.PageSize = 1;
                        adsource.AllowPaging = true;               
                        adsource.CurrentPageIndex = pos;
                        RegNoCF.Value = dt.Rows[1]["RegNo"].ToString();
                        FilePathImgCF.Value = dt.Rows[1]["FilePath"].ToString();
                        dlImagesCF.DataSource = adsource;
                        dlImagesCF.DataBind();

                        ds.Tables[0].Columns.RemoveAt(0);
                        ds.Tables[0].Columns.RemoveAt(0);
                        ds.Tables[0].Columns.RemoveAt(2);
                        grdEntry.DataSource = ViewState["grdEntry"] = ds.Tables[0];
                        grdEntry.DataBind();
                        ds.Tables[1].Columns.RemoveAt(0);
                        ds.Tables[1].Columns.RemoveAt(0);
                        ds.Tables[1].Columns.RemoveAt(2);

                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            txtRollNoFrom.Text = ds.Tables[1].Rows[0][0].ToString();
                            txtRollNoTo.Text = ds.Tables[1].Rows[0][1].ToString();
                        }
                        pnlcounter.Visible = true;
                        pnlCf.Visible = true;
                        btnSearchImg.Enabled = false;
                        pnlImageViewer.Visible = true;
                        //FilePathImgCF.Value = ds.Tables[0].Rows[1]["FilePath"].ToString();                      
                    }
                    else lblDetails.Text = "Folder Or Images Not Found Please Check Image !";
                }
                
            }
            btnUp.Visible = btnDown.Visible = grdEntry.Rows.Count > 1 ? true : false;
        }
        catch (Exception ex)
        {
            lblDetails.Text = ex.Message;
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

        lblmsg.Text = txtSRNo.Text = "";
        txtSRNo.Focus();
        Session["index"] = null;

    }    

    protected void grdEntry_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        AddRowsToGrid();
        try
        {
            foreach (GridViewRow dr in grdEntry.Rows)
            {
                CheckBox chkBx = (CheckBox)dr.FindControl("ChkGridRow");
                if (chkBx.Checked == true)
                {
                    dtGrd = (DataTable)ViewState["AvailablePages"];
                    dtGrd.Rows.Remove(dtGrd.Rows[e.RowIndex]);
                    grdEntry.DataSource = ViewState["grdEntry"] = dtGrd;
                    grdEntry.DataBind();
                }
            }
        }
        catch(Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }    

    protected void LinkDeleteAll_Click(object sender, EventArgs e)
    {
        AddRowsToGrid();
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
        grdEntry.DataSource = ViewState["grdEntry"] = dtl;
        grdEntry.DataBind();        
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string s = Convert.ToString(hdnval.Value);
        //  string confirmValue = Request.Form["confirm_value"];
        if (s == "1")
        {
            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
            verify = 1;
            UpdateData();
            hdnval.Value = null;

        }
        else if (s == "0")
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            hdnval.Value = null;
            chkverified.Checked = false;
        }
        else
        {
            verify = 0;
            UpdateData();
            hdnval.Value = null;
        }
        lblmsg.Text = "Insert Success";
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {

    }

    protected void btnUp_Click(object sender, EventArgs e)
    {
        int index;// = 0;
        List<int> LstCBChecked = new List<int>();
        DataTable dt = new DataTable();
        
        dt = (DataTable)ViewState["grdEntry"];
        foreach (GridViewRow gvrow in grdEntry.Rows)
        {
            CheckBox chk = (CheckBox)gvrow.FindControl("ChkGridRow");
            if (chk != null & chk.Checked)
            {
                index = gvrow.RowIndex;
                DataRow selectedRow = dt.Rows[index];
                DataRow newRow = dt.NewRow();
                newRow.ItemArray = selectedRow.ItemArray; // copy data
                dt.Rows.Remove(selectedRow);
                int newPos = index + 1 / -1;
                dt.Rows.InsertAt(newRow, newPos);
                LstCBChecked.Add(newPos);
            }
        }
        grdEntry.DataSource = ViewState["grdEntry"] = dt;        
        grdEntry.DataBind();
        CheckedBound(LstCBChecked);

    }

    protected void btnDown_Click(object sender, EventArgs e)
    {
        List<int> LstCBChecked = new List<int>();
        List<int> LstNewPos = new List<int>();
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["grdEntry"];
        foreach (GridViewRow gvrow in grdEntry.Rows)
        {
            CheckBox chk = (CheckBox)gvrow.FindControl("ChkGridRow");
            if (chk != null & chk.Checked)
            {
                LstCBChecked.Add(gvrow.RowIndex);
            }
        }

        for (int i = LstCBChecked.Count - 1 ; i >= 0; i--)
        {
            int index = LstCBChecked[i];
            DataRow selectedRow = dt.Rows[index];
            DataRow newRow = dt.NewRow();
            newRow.ItemArray = selectedRow.ItemArray; // copy data
            dt.Rows.Remove(selectedRow);
            int newPos = index + 1;
            dt.Rows.InsertAt(newRow, newPos);
            LstNewPos.Add(newPos);
        }
        grdEntry.DataSource = ViewState["grdEntry"] = dt;
        grdEntry.DataBind();
        CheckedBound(LstNewPos);
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        clear();
    }

    void UpdateData()
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
            return;
        }
        int insrtImg = 0;
        lblmsg.Text = "";
        lblmsgCF.Text = "";
        plImg.Ind = 4;
        plImg.RegNo = Convert.ToInt64(txtRegNo.Text);// Convert.ToInt64(Session["RegNo"]);
        plImg.CurrentPageNo = Convert.ToInt32(txtPageNo.Text);//Convert.ToInt32(Session["PageNo"]);
        i = dlImg.DeletePageNoData(plImg);

        long IstRollNo = 0; long LastRollNo = 0;
        frollno = 1;

        if (i >= 0)
        {
            try
            {
                if (grdEntry.Rows.Count > 0)
                {
                    for (int k = 0; k < grdEntry.Rows.Count; k++)
                    {
                        if (frollno == 1)
                        {
                            IstRollNo = Convert.ToInt64(grdEntry.Rows[k].Cells[1].Text);
                        }
                        if (grdEntry.Rows.Count == frollno)
                        {
                            LastRollNo = Convert.ToInt64(grdEntry.Rows[k].Cells[1].Text);
                        }

                        plImg.Ind = 1;
                        plImg.RegNo = Convert.ToInt64(RegNoFoil.Value);
                        plImg.RollNo = Convert.ToInt64(grdEntry.Rows[k].Cells[1].Text);
                        plImg.SName = grdEntry.Rows[k].Cells[2].Text.ToUpper();
                        plImg.FilePath = FilePathImg.Value;
                        plImg.CompletedPages = Convert.ToInt32(txtPageNo.Text);//Session["PageNo"]);
                        plImg.TotalPages = 0;
                        plImg.ErrorMark = 0;
                        plImg.IpAddress = Request.UserHostAddress;
                        plImg.UserId = Convert.ToInt32(Session["UserId"]);
                        plImg.CreationDate = Convert.ToDateTime(DateTime.Now.Date);                           
                        plImg.AllotmentNo = 1;
                        plImg.EntryByRegNo = Convert.ToInt64(txtRegNo.Text);//Session["RegNo"]);
                        plImg.IsUpdated = 1;
                        plImg.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                        plImg.UpdationDate = DateTime.Now;
                        plImg.PhyPageno = Convert.ToInt32(txtPageNo.Text);//Session["PageNo"]);
                        plImg.verifydata = verify;
                        insrtImg = dlImg.InserImgEntry(plImg);
                        frollno++;

                        if (insrtImg >= 0)
                        {
                            //lblmsg.Text = "Foil Page Entry Completed";
                            lblInd.Text = "1";
                            // btnSave.Enabled = false;
                            btnAdd.Enabled = false;
                            btnSearchImg.Enabled = true;//22122016
                            // pnlCf.Visible = false;
                        }
                        else
                        {
                            lblmsg.Text = "Roll No. - " + plImg.RollNo + " Is Already Exists";
                            return;
                        }
                    }
                    ViewState["AvailablePages"] = null;
                    grdEntry.DataSource = ViewState["grdEntry"] = null;
                    grdEntry.DataBind();
                }
                else
                {
                    lblmsg.Text = "Please Create Entry First";
                    return;
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }

            try
            {
                ////Update Data Into Summary Table
                plImg.Ind = 12;
                plImg.RegNo = Convert.ToInt64(RegNoFoil.Value);
                plImg.PhyPageno = Convert.ToInt32(txtPageNo.Text);
                plImg.RollNoFrom = Convert.ToInt64(IstRollNo);
                plImg.RollNoTo = Convert.ToInt64(LastRollNo);
                plImg.FilePath = FilePathImg.Value;
                insrtImg = dlImg.InsertFoilImgEntryPageWise(plImg);


                plImg.Ind = 3;
                plImg.RegNo = Convert.ToInt64(RegNoCF.Value);
                plImg.RollNoFrom = Convert.ToInt64(txtRollNoFrom.Text);
                plImg.RollNoTo = Convert.ToInt64(txtRollNoTo.Text);
                //plImg.SName = grdEntry.Rows[k].Cells[2].Text;
                plImg.FilePath = FilePathImgCF.Value;
                plImg.CompletedPages = Convert.ToInt32(txtPageNo.Text);//Session["PageNo"]);
                plImg.TotalPages = 0;
                plImg.ErrorMark = 0;
                plImg.IpAddress = Request.UserHostAddress;
                plImg.UserId = Convert.ToInt32(Session["UserId"]);
                plImg.CreationDate = Convert.ToDateTime(DateTime.Now.Date);
                plImg.AllotmentNo = 1;
                plImg.EntryByRegNo = Convert.ToInt64(txtRegNo.Text);//Session["RegNo"]);
                plImg.IsUpdated = 1;
                plImg.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                plImg.UpdationDate = DateTime.Now;
                plImg.PhyPageno = Convert.ToInt32(txtPageNo.Text);//Session["PageNo"]);
                plImg.verifydata = verify;
                i = dlImg.InsertCFImgEntry(plImg);
                if (i > 0)
                {
                    lblInd.Text = "2";
                    //lblmsgCF.Text = "Counter Foil Page Entry Completed";
                    pnlImageViewer.Visible = false;
                    lblDetails.Text = "Data Updated Succesfullly";
                 
                }
                else
                    lblmsgCF.Text = "Counter Foil Page Entry Not Cumpleted";
           
                txtRollNoFrom.Text = "";
                txtRollNoTo.Text = "";
            }

            catch (Exception ex)
            {
                lblmsgCF.Text = ex.Message;
            } 
            
            clear();
            pnlImageViewer.Visible = false;
        }

    }
    
      void UpdateDatad()
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
            return;
        }
        int insrtImg = 0;
        lblmsg.Text = "";
        lblmsgCF.Text = "";
        plImg.Ind = 4;
        plImg.RegNo = Convert.ToInt64(txtRegNo.Text);// Convert.ToInt64(Session["RegNo"]);
        plImg.CurrentPageNo = Convert.ToInt32(txtPageNo.Text);//Convert.ToInt32(Session["PageNo"]);
        i = dlImg.DeletePageNoData(plImg);

        long IstRollNo = 0; long LastRollNo = 0;
        frollno = 1;

        if (i >= 0)
        {
            try
            {
                if (grdEntry.Rows.Count > 0)
                {
                    for (int k = 0; k < grdEntry.Rows.Count; k++)
                    {
                        if (frollno == 1)
                        {
                            IstRollNo = Convert.ToInt64(grdEntry.Rows[k].Cells[1].Text);
                        }
                        if (grdEntry.Rows.Count == frollno)
                        {
                            LastRollNo = Convert.ToInt64(grdEntry.Rows[k].Cells[1].Text);
                        }

                        plImg.Ind = 1;
                        plImg.RegNo = Convert.ToInt64(RegNoFoil.Value);
                        plImg.RollNo = Convert.ToInt64(grdEntry.Rows[k].Cells[1].Text);
                        plImg.SName = grdEntry.Rows[k].Cells[2].Text.ToUpper();
                        plImg.FilePath = FilePathImg.Value;
                        plImg.CompletedPages = Convert.ToInt32(txtPageNo.Text);//Session["PageNo"]);
                        plImg.TotalPages = 0;
                        plImg.ErrorMark = 0;
                        plImg.IpAddress = Request.UserHostAddress;
                        plImg.UserId = Convert.ToInt32(Session["UserId"]);
                        plImg.CreationDate = Convert.ToDateTime(DateTime.Now.Date);                           
                        plImg.AllotmentNo = 1;
                        plImg.EntryByRegNo = Convert.ToInt64(txtRegNo.Text);//Session["RegNo"]);
                        plImg.IsUpdated = 1;
                        plImg.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                        plImg.UpdationDate = DateTime.Now;
                        plImg.PhyPageno = Convert.ToInt32(txtPageNo.Text);//Session["PageNo"]);
                        plImg.verifydata = verify;
                        insrtImg = dlImg.InserImgEntry(plImg);
                        frollno++;

                        if (insrtImg >= 0)
                        {
                            //lblmsg.Text = "Foil Page Entry Completed";
                            lblInd.Text = "1";
                            // btnSave.Enabled = false;
                            btnAdd.Enabled = false;
                            btnSearchImg.Enabled = true;//22122016
                            // pnlCf.Visible = false;
                        }
                        else
                        {
                            lblmsg.Text = "Roll No. - " + plImg.RollNo + " Is Already Exists";
                            return;
                        }
                    }
                    ViewState["AvailablePages"] = null;
                    grdEntry.DataSource = ViewState["grdEntry"] = null;
                    grdEntry.DataBind();
                }
                else
                {
                    lblmsg.Text = "Please Create Entry First";
                    return;
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }

            try
            {
                ////Update Data Into Summary Table
                plImg.Ind = 12;
                plImg.RegNo = Convert.ToInt64(RegNoFoil.Value);
                plImg.PhyPageno = Convert.ToInt32(txtPageNo.Text);
                plImg.RollNoFrom = Convert.ToInt64(IstRollNo);
                plImg.RollNoTo = Convert.ToInt64(LastRollNo);
                plImg.FilePath = FilePathImg.Value;
                insrtImg = dlImg.InsertFoilImgEntryPageWise(plImg);


                plImg.Ind = 3;
                plImg.RegNo = Convert.ToInt64(RegNoCF.Value);
                plImg.RollNoFrom = Convert.ToInt64(txtRollNoFrom.Text);
                plImg.RollNoTo = Convert.ToInt64(txtRollNoTo.Text);
                //plImg.SName = grdEntry.Rows[k].Cells[2].Text;
                plImg.FilePath = FilePathImgCF.Value;
                plImg.CompletedPages = Convert.ToInt32(txtPageNo.Text);//Session["PageNo"]);
                plImg.TotalPages = 0;
                plImg.ErrorMark = 0;
                plImg.IpAddress = Request.UserHostAddress;
                plImg.UserId = Convert.ToInt32(Session["UserId"]);
                plImg.CreationDate = Convert.ToDateTime(DateTime.Now.Date);
                plImg.AllotmentNo = 1;
                plImg.EntryByRegNo = Convert.ToInt64(txtRegNo.Text);//Session["RegNo"]);
                plImg.IsUpdated = 1;
                plImg.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                plImg.UpdationDate = DateTime.Now;
                plImg.PhyPageno = Convert.ToInt32(txtPageNo.Text);//Session["PageNo"]);
                plImg.verifydata = verify;
                i = dlImg.InsertCFImgEntry(plImg);
                if (i > 0)
                {
                    lblInd.Text = "2";
                    //lblmsgCF.Text = "Counter Foil Page Entry Completed";
                    pnlImageViewer.Visible = false;
                    lblDetails.Text = "Data Updated Succesfullly";
                 
                }
                else
                    lblmsgCF.Text = "Counter Foil Page Entry Not Cumpleted";
           
                txtRollNoFrom.Text = "";
                txtRollNoTo.Text = "";
            }

            catch (Exception ex)
            {
                lblmsgCF.Text = ex.Message;
            } 
            
            clear();
            pnlImageViewer.Visible = false;
        }

    }
    void CheckedBound(List<int> LstCBChecked)
    {
        foreach (int item in LstCBChecked)
        //for (int i = 0; i < LstCBChecked.Count; i++)
        {
            CheckBox chk = (CheckBox)grdEntry.Rows[item].FindControl("ChkGridRow");
            chk.Checked = true;
            //grdEntry.Rows[i].FindControl("").Controls = chk;
            grdEntry.Rows[item].Cells[3].Controls.Add(chk);

        }
        
    }
    void clear()
    {
        grdEntry.DataSource = ViewState["grdEntry"] = ViewState["AvailablePages"] = null;
        grdEntry.DataBind();
        txtRegNo.Enabled = true;
        txtRegNo.Text = "";
        txtPageNo.Text = "";
        pnlcounter.Visible = false;
        pnlCf.Visible = false;
        pnlImageViewer.Visible = false;
        btnSearchImg.Enabled = true;
    }


    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    ViewState["AvailablePages"] = null;
    //}
    //private void Clear()  // Clear Textboxes and  Set DropDown Lists To SelectedIndex "0"
    //{
    //    lblmsg.Text = txtSRNo.Text = "";
    //}
}
