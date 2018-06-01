using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmRenamePageNo : System.Web.UI.Page
{
    PlRegister plReg = new PlRegister();
    BlRegister BlReg = new BlRegister();
    DlImgEntry dlImg = new DlImgEntry(); 
    PlImgEntry plImg = new PlImgEntry();
    DataTable dtGrd = new DataTable();

    //DataTable dt, dt1; 
    //int pos; static int count; PagedDataSource adsource;
    //static string FilePath; int index;
    //int LastF, LastCF; static int TotalPages, CompletedPageNo;
    int i;
    DataSet ds; 
    static int frollno = 0;
    int verify;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            divRenamePage.Visible = false;
        }
    }
    protected void btnSearchImg_Click(object sender, EventArgs e)
    {        
        lblmsg.Text = "";
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
                return;
            }
            else 
            {
                plReg.Ind = 13;
                plReg.RegNo = Convert.ToInt64(txtRegNo.Text);
                plReg.PageNo = Convert.ToInt32(txtPageNo.Text);
                ds = BlReg.ShowBothImages(plReg);

                if (ds.Tables[0].Rows.Count <= 0)
                {
                    lblDetails.Text ="Register No. " + plReg.RegNo + " And Page No. " + plReg.PageNo + " Not Found In Foil Entry!";
                    return;
                }
                if (ds.Tables[1].Rows.Count <= 0)
                {
                    lblDetails.Text = "Register No. " + plReg.RegNo + " And Page No. " + plReg.PageNo + " Not Found In Counter Foil Entry!";
                    return;
                }
                lblFoilEntry.Text = "Foil Entry - " + ds.Tables[0].Rows[0]["RegNo"].ToString();
                lblCF.Text          = "Counter Foil Entry - " + ds.Tables[1].Rows[0]["RegNo"].ToString();
                image.ImageUrl = ds.Tables[0].Rows[0]["FilePath"].ToString();
                imageCF.ImageUrl = ds.Tables[1].Rows[0]["FilePath"].ToString();                    
                #region Comments
                //Session["dtFoil"] = ds.Tables[0];
                //Session["UserIdF"] = ds.Tables[0].Rows[0]["UserId"].ToString();
                //Session["CreationDateF"] = ds.Tables[0].Rows[0]["CreationDate"].ToString();

                //Session["dtCF"] = ds.Tables[1];
                //Session["UserIdCF"] = ds.Tables[1].Rows[0]["UserId"].ToString();
                //Session["CreationDateCF"] = ds.Tables[1].Rows[0]["CreationDate"].ToString();

                //GetImagePath(Convert.ToInt32(txtPageNo.Text));
                //GetImagePathCF(Convert.ToInt32(txtPageNo.Text));
                #endregion
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

                pnlImageViewer.Visible = true;
                btnSearchImg.Enabled = false;
                divRenamePage.Visible = true;
            }
            
        }
        catch (Exception ex)
        {
            lblDetails.Text = ex.Message;
        }
    }  
    public void UpdateData()
    {
        //if (txtRollNoFrom.Text == "")
        //{
        //    lblmsg.Text = "Please Enter Page Roll From";
        //    txtRollNoFrom.Focus();
        //    return;
        //}
        //else if (txtRollNoTo.Text == "")
        //{
        //    lblmsg.Text = "Please Enter Page Roll To";
        //    txtRollNoTo.Focus();
        //    return;
        //}
        //else if (Convert.ToInt32(txtRollNoTo.Text) < Convert.ToInt32(txtRollNoFrom.Text))
        //{
        //    lblmsg.Text = "Roll No From Must Be Less Than of Roll No To";
        //    txtRollNoFrom.Focus();
        //    return;
        //}
        int insrtImg = 0;
        lblmsg.Text = "";
        lblmsgCF.Text = "";
        plImg.Ind = 4;
        plImg.RegNo = Convert.ToInt64(txtRegNo.Text);
        plImg.CurrentPageNo = Convert.ToInt32(txtPageNo.Text);
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
                        //plImg.RegNo = Convert.ToInt64(RegNoFoil.Value);
                        plImg.RollNo = Convert.ToInt64(grdEntry.Rows[k].Cells[1].Text);
                        plImg.SName = grdEntry.Rows[k].Cells[2].Text.ToUpper();
                        //plImg.FilePath = FilePathImg.Value;
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
                            lblInd.Text = "1";
                            btnSearchImg.Enabled = true;
                        }
                        else
                        {
                            lblmsg.Text = "Roll No. - " + plImg.RollNo + " Is Already Exists";
                            return;
                        }
                    }
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
                ////Update Data Into Summary Table
                plImg.Ind = 12;
                //plImg.RegNo = Convert.ToInt64(RegNoFoil.Value);
                plImg.PhyPageno = Convert.ToInt32(txtPageNo.Text);
                plImg.RollNoFrom = Convert.ToInt64(IstRollNo);
                plImg.RollNoTo = Convert.ToInt64(LastRollNo);
                //plImg.FilePath = FilePathImg.Value;
                insrtImg = dlImg.InsertFoilImgEntryPageWise(plImg);


                plImg.Ind = 3;
                //plImg.RegNo = Convert.ToInt64(RegNoCF.Value);
                plImg.RollNoFrom = Convert.ToInt64(txtRollNoFrom.Text);
                plImg.RollNoTo = Convert.ToInt64(txtRollNoTo.Text);
                //plImg.SName = grdEntry.Rows[k].Cells[2].Text;
                //plImg.FilePath = FilePathImgCF.Value;
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

            Clear();
            pnlImageViewer.Visible = false;
        }

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //string s = Convert.ToString(hdnval.Value);
        ////  string confirmValue = Request.Form["confirm_value"];
        //if (s == "1")
        //{
        //    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
        //    verify = 1;
        //    UpdateData();
        //    hdnval.Value = null;

        //}
        //else if (s == "0")
        //{
        //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
        //    hdnval.Value = null;
        //    chkverified.Checked = false;
        //}
        //else
        //{
        //    verify = 0;
        //    UpdateData();
        //    hdnval.Value = null;
        //}
        //lblmsg.Text = "Insert Success";
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    public void Clear()
    {
        grdEntry.DataSource = null;
        grdEntry.DataBind();
        txtRegNo.Enabled = true;
        txtRegNo.Text = "";
        txtPageNo.Text = "";
        pnlImageViewer.Visible = divRenamePage.Visible = false;
        btnSearchImg.Enabled = true;
        lblDetails.Text = lblmsg.Text = string.Empty;
    }
}