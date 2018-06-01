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
public partial class BothImgsShow : System.Web.UI.Page
{
    DlRegister dlReg = new DlRegister(); PlRegister plReg = new PlRegister();
    //BlRegister BlReg = new BlRegister();
    DlImgEntry dlImg = new DlImgEntry(); PlImgEntry plImg = new PlImgEntry();
    DataTable dtGrd = new DataTable();

    int i, LastF, LastCF; static int TotalPages, CompletedPageNo;
    DataTable dt, dt1; DataSet ds; //SqlDataAdapter da; SqlCommand cmd;
    int pos; static int count; PagedDataSource adsource;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    static string FilePath; int index;
    int verify;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            this.ViewState["vs"] = 0;
            pos = (int)this.ViewState["vs"];
        }
        btnAdd.Enabled = true;

        this.Form.DefaultButton = btnSearchImg.UniqueID;
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
        if (!string.IsNullOrEmpty(txtSRNo.Text) )
        {
            dtGrd.Rows.Add(Convert.ToInt64(txtSRNo.Text));
            grdEntry.DataSource = dtGrd;
            grdEntry.DataBind();
            ViewState["AvailablePages"] = dtGrd;
            dtGrd = null;
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
        txtRegNo.Enabled = true;
        txtRegNo.Text = "";
        pnlcounter.Visible = false;
        pnlCf.Visible = false;
        pnlImageViewer.Visible = false;
        lblDetails.Text = "";
        btnSearchImg.Enabled = true;
        txtPageNo.Text = "";
    }
    protected void btnSearchImg_Click(object sender, EventArgs e)
    {
        Session["dt"] = null;
        count = 0;
        lblDetails.Text = "";
        grdEntry.DataSource = null;
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
                ds = dlReg.ShowBothImages(plReg);

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
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
                    grdEntry.DataSource = ds.Tables[0];
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
                    dlImages.DataSource = null;
                    dlImages.DataBind();
                   lblDetails.Text = "No Record Found";                

                    //lblCount.Text = "";
                    //pnlImageViewer.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            lblDetails.Text = ex.Message;
        }
    }
    private void Clear()  // Clear Textboxes and  Set DropDown Lists To SelectedIndex "0"
    {
        lblmsg.Text =  txtSRNo.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["AvailablePages"] = null;
    }
    private int GetImagePath(int RegNo)
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
    private int GetImagePathCF(int RegNo)
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
    protected void btnClear_Click1(object sender, EventArgs e)
    {
        txtRegNo.Enabled = true;
        Clear();
        txtRegNo.Text = "";
        pnlcounter.Visible = false;
        pnlCf.Visible = false;
        pnlImageViewer.Visible = false;
        lblDetails.Text = "";
        //lblCuurentPage.Text = "";
        lblDetails.Text = "";
        // lblEntryCompleted.Text = "";
        //lblType.Text = "";
        btnSearchImg.Enabled = true;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    //protected void grdEntry_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    index = e.RowIndex;
    //    Session["index"] = index;
    //    txtSRNo.Text = grdEntry.Rows[index].Cells[1].Text.ToString();
    //  //  txtStudentName.Text = grdEntry.Rows[index].Cells[2].Text.ToString();
    //    btnAdd.Text = "Save";
    //}
    protected void btnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    public void UpdateData()
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
        plImg.RegNo = Convert.ToInt64(txtRegNo.Text);
        plImg.CurrentPageNo = Convert.ToInt32(txtPageNo.Text);
        i = dlImg.DeletePageNoData(plImg);
        if (i >= 0)
        {
            try
            {
                if (grdEntry.Rows.Count > 0)
                {
                    for (int k = 0; k < grdEntry.Rows.Count; k++)
                    {
                        plImg.Ind = 1;
                        plImg.RegNo = Convert.ToInt64(RegNoFoil.Value);
                        plImg.RollNo = Convert.ToInt64(grdEntry.Rows[k].Cells[1].Text);
                        plImg.SName = grdEntry.Rows[k].Cells[2].Text.ToUpper();
                        plImg.FilePath = FilePathImg.Value;
                        plImg.CompletedPages = Convert.ToInt32(txtPageNo.Text);
                        plImg.TotalPages = 0;
                        plImg.ErrorMark = 0;
                        plImg.IpAddress = Request.UserHostAddress;
                        plImg.UserId = Convert.ToInt32(Session["UserIdF"]);
                        plImg.CreationDate = Convert.ToDateTime(Session["CreationDateF"].ToString());
                        plImg.AllotmentNo = 1;
                        plImg.EntryByRegNo = Convert.ToInt64(txtRegNo.Text);
                        plImg.IsUpdated = 1;
                        plImg.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                        plImg.UpdationDate = DateTime.Now;
                        plImg.PhyPageno = Convert.ToInt32(txtPageNo.Text);
                        plImg.verifydata = verify;
                        insrtImg = dlImg.InserImgEntry(plImg);
                        if (insrtImg >= 0)
                        {
                            //lblmsg.Text = "Foil Page Entry Completed";
                            lblInd.Text = "1";
                            // btnSave.Enabled = false;
                            btnAdd.Enabled = false;
                            btnSearchImg.Enabled = true;
                            // pnlCf.Visible = false;


                        }
                        else
                        {
                            lblmsg.Text = "Roll No. - " + plImg.RollNo + " Is Already Exists";
                            return;
                        }
                    }
                    ViewState["AvailablePages"] = null;
                    grdEntry.DataSource = null;
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
                plImg.Ind = 3;
                plImg.RegNo = Convert.ToInt64(RegNoCF.Value);
                plImg.RollNoFrom = Convert.ToInt64(txtRollNoFrom.Text);
                plImg.RollNoTo = Convert.ToInt64(txtRollNoTo.Text);
                //plImg.SName = grdEntry.Rows[k].Cells[2].Text;
                plImg.FilePath = FilePathImgCF.Value;
                plImg.CompletedPages = Convert.ToInt32(txtPageNo.Text);
                plImg.TotalPages = 0;
                plImg.ErrorMark = 0;
                plImg.IpAddress = Request.UserHostAddress;
                plImg.UserId = Convert.ToInt32(Session["UserIdCF"]);
                plImg.CreationDate = Convert.ToDateTime(Session["CreationDateCF"]);
                plImg.AllotmentNo = 1;
                plImg.EntryByRegNo = Convert.ToInt64(txtRegNo.Text);
                plImg.IsUpdated = 1;
                plImg.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                plImg.UpdationDate = DateTime.Now;
                plImg.PhyPageno = Convert.ToInt32(txtPageNo.Text);
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
        }

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
        
    }
    protected void LinkDeleteAll_Click(object sender, EventArgs e)
    {

        AddRowsToGrid(); 
        DataTable dtl=new DataTable();

        #region Comment Foreach Old

        //foreach (GridViewRow dr in grdEntry.Rows)
        //{
        //    CheckBox chkBx = (CheckBox)dr.FindControl("ChkGridRow");
        //    if (chkBx.Checked == true)
        //    {
        //        dtGrd = (DataTable)ViewState["AvailablePages"];
        //        dtGrd.Rows.RemoveAt(dr.RowIndex);
        //        grdEntry.DataSource = dtGrd;
        //        grdEntry.DataBind();
        //    }            
        //}

        //-----------------------------------------------------

        //foreach (GridViewRow gvrow in grdEntry.Rows)
        //{
        //    CheckBox chk = (CheckBox)gvrow.FindControl("ChkGridRow");
        //    dtl = (DataTable)ViewState["AvailablePages"];
        //    if (chk != null && chk.Checked)
        //    {
        //        dtl.Rows[gvrow.RowIndex].Delete();
        //        grdEntry.DeleteRow(gvrow.RowIndex);
                
        //       //dtl.Rows.RemoveAt(gvrow.RowIndex);               
        //    } 
        //}
        #endregion

        int pos=0;
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

        #region Comments Old Code
        //for (int i = grdEntry.Rows.Count-1; i > 0; i--)
        //{
        //    CheckBox check = (CheckBox)grdEntry.Rows[i].FindControl("ChkGridRow");
        //    if (check != null && check.Checked)
        //    {
        //        dtl.Rows.RemoveAt(i);
        //    } 
        //}

        

        //string RollNo = "";
        //foreach (GridViewRow gvrow in grdEntry.Rows)
        //{
        //    CheckBox chk = (CheckBox)gvrow.FindControl("ChkGridRow");
        //    if (chk != null && chk.Checked)            
        //        RollNo += grdEntry.Rows[gvrow.RowIndex].Cells[2].Text.ToString()+",";            
        //}
        //RollNo = RollNo.Substring(0, RollNo.Length - 1);

        //if (RollNo.Length > 0)
        //{
        //    int DeletedStatus=0;
        //    plReg.Ind = 13;
        //    plReg.RegNo = Convert.ToInt64(txtRegNo.Text);
        //    plReg.PageNo = Convert.ToInt32(txtPageNo.Text);
        //    plReg.DeletedRollNo = RollNo;
        //    DeletedStatus = dlReg.DeleteRollNoFromRegister(plReg);            

        //    //bindgrid(); 
        //}

        #endregion
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
      
    }
}

