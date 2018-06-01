using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Collections;

public partial class Registermaster : System.Web.UI.Page
{
    BlTRRegiter BlTrReg = new BlTRRegiter();
    plTrRegister plTrReg = new plTrRegister();

    BlRegister BlReg = new BlRegister();
    PlRegister PlReg = new PlRegister();

    BlReports BlRpt = new BlReports();
    PlReports PlRpt = new PlReports();

    int j, TotalPg; Int64 i;
    //string PdfFilePath = "\\\\OCCWE04\\D$\\AmravatiTRImages";
    string PdfFilePath;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataTable dt, dt1;
    DataTable dtGrd = new DataTable();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToInt16(Request.QueryString["PageInd"]) == 1) // PageInd 1 For Regiser Insertion
            {
                lblRegNo.Visible = false;
                txtRegisterNo.Visible = false; linkSearch.Visible = false;
                pnlRegistrmasteUpdate.Visible = true;
                lblHeadeing.Text = "TR Register Entry Form";
                pnlAvailablePages.Visible = true;
            }
            else if (Convert.ToInt16(Request.QueryString["PageInd"]) == 2) // PageInd 2 For Regiser Updation
            {
                lblRegNo.Visible = true;
                txtRegisterNo.Visible = true; linkSearch.Visible = true;
                pnlRegistrmasteUpdate.Visible = false;
                lblHeadeing.Text = "TR Register Cancellation Form";
                pnlAvailablePages.Visible = false;
            }
            ddlFaculty.Focus();
            ddlExamname.Items.Insert(0, "Select Exam Name");
            ddlExamname.SelectedIndex = 0;
            BindFaculty(); BindYear(); BindSession(); BindExamType();
            BindRegisterGrid();
            lblTotalNoOfPages.Text = "0";
            pnlAvlbPgs.Visible = false;
        }
    }

    private DataTable BindFaculty()   // Bind Faculty Name Purpose
    {
        dt = BlTrReg.GetAllRegister(plTrReg, 1);
        dt.Rows.InsertAt(dt.NewRow(), 0);
        dt.Rows[0][0] = "0";
        dt.Rows[0][1] = "Select Faculty";
        ddlFaculty.DataSource = dt;
        ddlFaculty.DataValueField = "ItemID";
        ddlFaculty.DataTextField = "ItemDesc";
        ddlFaculty.DataBind();
        return dt;
    }
    private DataTable BindYear() // Bind Exam Year (like 2000,2001 etc.)
    {
        dt = BlTrReg.GetAllRegister(plTrReg, 3);
        dt.Rows.InsertAt(dt.NewRow(), 0);
        dt.Rows[0][0] = "0";
        dt.Rows[0][1] = "Select Year";
        ddlExamYear.DataSource = dt;
        ddlExamYear.DataValueField = "ItemDesc"; // To filter Exam Name 
        ddlExamYear.DataTextField = "ItemDesc";
        ddlExamYear.DataBind();
        return dt;
    }
    private DataTable BindSession()  // Bind Session (Winter,Summer)
    {
        dt = BlTrReg.GetAllRegister(plTrReg, 2);
        dt.Rows.InsertAt(dt.NewRow(), 0);
        dt.Rows[0][0] = "0";
        dt.Rows[0][1] = "Select Session";
        ddlSession.DataSource = dt;
        ddlSession.DataValueField = "ItemID";
        ddlSession.DataTextField = "ItemDesc";
        ddlSession.DataBind();
        return dt;
    }
    private DataTable BindExamType()  // Bind Exam Type  (Main,Reval etc.)
    {
        dt = BlTrReg.GetAllRegister(plTrReg, 4);
        dt.Rows.InsertAt(dt.NewRow(), 0);
        dt.Rows[0][0] = "0";
        dt.Rows[0][1] = "Select Exam Type";
        ddlExamType.DataSource = dt;
        ddlExamType.DataValueField = "ItemID";
        ddlExamType.DataTextField = "ItemDesc";
        ddlExamType.DataBind();
        return dt;
    }
    private DataTable BindExamName()  // Bind Exam Name (BA Part, Bcom Part etc.)
    {
        dt1 = BlTrReg.GetExamName(1, Convert.ToInt32(ddlFaculty.SelectedValue));
        dt1.Rows.InsertAt(dt1.NewRow(), 0);
        dt1.Rows[0][0] = "0";
        dt1.Rows[0][1] = "Select Exam Name";
        ddlExamname.DataSource = dt1;
        ddlExamname.DataValueField = "ctrl";
        ddlExamname.DataTextField = "examname";
        ddlExamname.DataBind();
        return dt1;
    }

    protected void linkSave_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt16(Request.QueryString["PageInd"]) == 1)
        {
            if (Convert.ToInt32(txtPageNoFrom.Text) > Convert.ToInt32(txtPageNoTo.Text))
            {
                lblmsg.Text = "Page No. From Value Must Be Less Than Page No. To";
                txtPageNoFrom.Focus();
                return;
            }
            if (Convert.ToInt32(txtRefBookNo.Text) < Convert.ToInt32(txtCurrentBookNo.Text))
            {
                lblmsg.Text = "No. Of Books Must Be Greater Or Equal To Current Book No. ";
                txtCurrentBookNo.Focus();
                return;
            }
            PlRpt.Ind = 8;
            dt1 = BlRpt.GetPhysicalPath(PlRpt);
            PdfFilePath = dt1.Rows[0][1].ToString();
            //int i = 0;
            PlReg.Ind = 2;
            PlReg.Ctrl = Convert.ToInt64(ddlExamname.SelectedValue);
            PlReg.ExamYear = Convert.ToInt32(ddlExamYear.SelectedValue);
            PlReg.Session = Convert.ToInt32(ddlSession.SelectedValue);
            PlReg.ExamType = Convert.ToInt32(ddlExamType.SelectedValue);
            PlReg.FoilCuonterfoil = Convert.ToInt32(ddlCopyType.SelectedValue);
            PlReg.NoOfBook = Convert.ToInt32(txtRefBookNo.Text);
            PlReg.CureentBookNo = Convert.ToInt32(txtCurrentBookNo.Text);
            PlReg.PageNoFrom = Convert.ToInt32(txtPageNoFrom.Text);
            PlReg.PageNoTo = Convert.ToInt32(txtPageNoTo.Text);
            PlReg.TotalPage = Convert.ToInt32(lblTotalNoOfPages.Text);
            PlReg.RegCreationDate = DateTime.Now;
            PlReg.UserID = Convert.ToInt32(Session["UserId"]);
            PlReg.IpAddress = Request.UserHostAddress;

            if (!Directory.Exists(PdfFilePath))
                Directory.CreateDirectory(PdfFilePath);
            if (!Directory.Exists(PdfFilePath + "//" + ddlExamYear.SelectedItem.Text + ""))
                Directory.CreateDirectory(PdfFilePath + "//" + ddlExamYear.SelectedItem.Text + "");

            string FoilOrCounterFoil = "";
            if (ddlCopyType.SelectedIndex == 1)
                FoilOrCounterFoil = "C";
            else if (ddlCopyType.SelectedIndex == 2)
                FoilOrCounterFoil = "F";
            if (!Directory.Exists(PdfFilePath + "\\" + ddlExamYear.SelectedItem.Text + "\\" + FoilOrCounterFoil))
                Directory.CreateDirectory(PdfFilePath + "//" + ddlExamYear.SelectedItem.Text + "\\" + FoilOrCounterFoil);
            PlReg.FilePath = PdfFilePath + "\\" + ddlExamYear.SelectedItem.Text + "\\" + FoilOrCounterFoil.ToString();
            j = ChecBookNo();
            if (j > 0)
            {
                lblmsg.Text = "Current Book No. Already Exists";
                txtCurrentBookNo.Focus(); 
                return;
            }
            i = BlReg.InsertRegister(PlReg);
            if (i > 0)
            {
                DataTable dtInsert = (DataTable)ViewState["AvailablePages"];
                if (dtInsert.Rows.Count > 0)
                {
                    for (int k = 0; k < dtInsert.Rows.Count; k++)
                    {
                        PlReg.Ind = 10;
                        PlReg.PageNoFrom = Convert.ToInt32(dtInsert.Rows[k]["PageNoFrom"]);
                        PlReg.PageNoTo = Convert.ToInt32(dtInsert.Rows[k]["PageNoTo"]);
                        PlReg.TotalPage = Convert.ToInt32(dtInsert.Rows[k]["TotalPages"]);
                        PlReg.RegNo = i;
                        PlReg.Ctrl = Convert.ToInt64(ddlExamname.SelectedValue);
                        PlReg.FoilCuonterfoil = Convert.ToInt32(ddlCopyType.SelectedValue);//PartNo In DB Table
                        PlReg.IpAddress = Request.UserHostAddress;
                        PlReg.CreationDate = DateTime.Now;
                        PlReg.CureentBookNo = Convert.ToInt32(txtCurrentBookNo.Text);
                        PlReg.TotalPage = Convert.ToInt32(lblTotalNoOfPages.Text);
                        int AvailPg = BlReg.InsertAvailablePages(PlReg);
                    }
                }
                Clear();
                lblmsg.Text = "Data Inserted Successfully For Register No. :-  " + i + "";
                PlReg.Ind = 21;
                PlReg.FilePath = PdfFilePath;// +"\\" + ddlExamYear.SelectedItem.Text + "\\" + FoilOrCounterFoil + "\\" + i;
                PlReg.RegNo = i;
                if (!Directory.Exists(PdfFilePath + "\\" + ddlExamYear.SelectedItem.Text + "\\" + FoilOrCounterFoil + "\\" + i))
                    Directory.CreateDirectory(PdfFilePath + "\\" + ddlExamYear.SelectedItem.Text + "\\" + FoilOrCounterFoil + "\\" + i);
                int FilePath = BlReg.UpdateRegisterFilePath(PlReg);

                BindRegisterGrid();
                grdAvailablePages.DataSource = null;
                grdAvailablePages.DataBind();
                lblTotalMsg.Visible = false; lblTotal.Visible = false;
                pnlAvlbPgs.Visible = true;
                dtGrd.Rows.Clear();
                dtGrd.Columns.Clear();
                ViewState["AvailablePages"] = null;
            }
        }
    }
    private DataTable BindRegisterGrid()  // Bind Gridview (Last 5 Records Displaying)
    {
        dt = BlReg.GetRegister();
        if (dt.Rows.Count> 0)
        {
            gridDetails.Visible = true;
            gridDetails.DataSource = dt;
            gridDetails.DataBind();
            lblLastRegNo.Text = "Last Register No. :- " + Convert.ToInt32(dt.AsEnumerable().Max(row => row["RegisterNo"])) + "";
        }
        else
            gridDetails.Visible = false;
        return dt;
    }
    private void Clear()  // Clear Textboxes and  Set DropDown Lists To SelectedIndex "0"
    {
        ddlCopyType.SelectedIndex = ddlExamname.SelectedIndex = ddlExamType.SelectedIndex = 0;
        ddlExamYear.SelectedIndex = ddlFaculty.SelectedIndex = ddlSession.SelectedIndex = 0;
        txtPageNoFrom.Text = txtPageNoTo.Text = txtRefBookNo.Text = txtCurrentBookNo.Text = lblTotalNoOfPages.Text = "";
        txtCnclReason.Text = "";
    }
    protected void linkCancel_Click(object sender, EventArgs e)
    {
        Clear();
        pnlRegistrmasteUpdate.Visible = false; txtRefBookNo.Enabled = false;
        txtRegisterNo.Enabled = true; lblmsg.Text = "";
        Response.Redirect("RegisterMaster.aspx?PageInd=" + Request.QueryString["PageInd"] + "");
        linkSave.Visible = false; linkSearch.Visible = true;
        pnlAvlbPgs.Visible = false;
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFaculty.SelectedIndex != 0)
        {
            BindExamName();
        }
        else
        {
            ddlExamYear.SelectedIndex = 0;
            ddlSession.SelectedIndex = 0;
            ddlExamType.SelectedIndex = 0;
            ddlCopyType.SelectedIndex = 0;
            ddlExamname.SelectedIndex = 0;
            txtRefBookNo.Text = "";
            txtRefBookNo.Enabled = true;

            ddlExamname.Items.Clear();
            ddlExamname.Items.Insert(0, "Select Exam Name");
        }
        ddlFaculty.Focus();
    }
    public void GetNoOfBooks()  // Get No. Of Books For Selected Exam,Exam Year,Session,Exam Type,TR Type
    {
        cmd = new SqlCommand("SPRegister", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", 7);
        cmd.Parameters.AddWithValue("@Ctrl", Convert.ToInt64(ddlExamname.SelectedValue));
        cmd.Parameters.AddWithValue("@ExamYear", Convert.ToInt32(ddlExamYear.SelectedValue));
        cmd.Parameters.AddWithValue("@SessionType", Convert.ToInt32(ddlSession.SelectedValue));
        cmd.Parameters.AddWithValue("@ExamType", Convert.ToInt32(ddlExamType.SelectedValue));
        cmd.Parameters.AddWithValue("@TRType", Convert.ToInt32(ddlCopyType.SelectedValue));
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtRefBookNo.Text = dt.Rows[0][0].ToString();
            txtRefBookNo.Enabled = false;
        }
        else
            txtRefBookNo.Enabled = true;
    }
    protected void ddlExamname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlExamYear.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlExamType.SelectedIndex = 0;
        ddlCopyType.SelectedIndex = 0;
        txtRefBookNo.Text = ""; txtRefBookNo.Enabled = true;
        ddlExamname.Focus();

    }
    protected void ddlExamYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSession.SelectedIndex = 0;
        ddlExamType.SelectedIndex = 0;
        ddlCopyType.SelectedIndex = 0;
        txtRefBookNo.Text = ""; txtRefBookNo.Enabled = true;
        ddlExamYear.Focus();

    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlExamType.SelectedIndex = 0;
        ddlCopyType.SelectedIndex = 0;
        txtRefBookNo.Text = ""; txtRefBookNo.Enabled = true;
        ddlSession.Focus();

    }
    protected void ddlExamType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCopyType.SelectedIndex = 0;
        txtRefBookNo.Text = ""; txtRefBookNo.Enabled = true;
        ddlExamType.Focus();
    }
    protected void ddlCopyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCopyType.SelectedIndex <= 0)
        {
            txtRefBookNo.Text = ""; txtRefBookNo.Enabled = true;
        }
        else
        {
            txtRefBookNo.Text = ""; txtCurrentBookNo.Focus();
            GetNoOfBooks();
        }
        ddlCopyType.Focus();
    }
    public int ChecBookNo()
    {
        cmd = new SqlCommand("SPRegister", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ind", 6);
        cmd.Parameters.AddWithValue("@Ctrl", Convert.ToInt64(ddlExamname.SelectedValue));
        cmd.Parameters.AddWithValue("@ExamYear", Convert.ToInt32(ddlExamYear.SelectedValue));
        cmd.Parameters.AddWithValue("@SessionType", Convert.ToInt32(ddlSession.SelectedValue));
        cmd.Parameters.AddWithValue("@ExamType", Convert.ToInt32(ddlExamType.SelectedValue));
        cmd.Parameters.AddWithValue("@TRType", Convert.ToInt32(ddlCopyType.SelectedValue));
        cmd.Parameters.AddWithValue("@CurrentBookNo", Convert.ToInt32(txtCurrentBookNo.Text));
        con.Open();
        j = Convert.ToInt32(cmd.ExecuteScalar());
        con.Close();
        return j;
    }
    protected void linkSearch_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt16(Request.QueryString["PageInd"]) == 2)
        {
            if (string.IsNullOrEmpty(txtRegisterNo.Text))
            {
                lblSrchRegNo.Text = "Please Enter Regiser No.";
                txtRegisterNo.Focus(); return;
            }
            if (!string.IsNullOrEmpty(txtRegisterNo.Text))
            {
                lblmsg.Text = ""; lblSrchRegNo.Text = ""; linkSave.Visible = false; linkCncl.Visible = true;
                BindFaculty(); BindYear();
                BindSession(); BindExamType();

                PlReg.Ind = 5;
                PlReg.RegNo = Convert.ToInt64(txtRegisterNo.Text);
                dt = BlReg.SearchRegister(PlReg);

                if (dt.Rows.Count > 0)
                {
                    ddlCopyType.SelectedValue = dt.Rows[0][11].ToString();
                    txtRefBookNo.Text = dt.Rows[0][2].ToString();
                    txtCurrentBookNo.Text = dt.Rows[0][3].ToString();
                    txtPageNoFrom.Text = dt.Rows[0][4].ToString();
                    txtPageNoTo.Text = dt.Rows[0][5].ToString();
                    lblTotalNoOfPages.Text = dt.Rows[0][6].ToString();
                    ddlFaculty.SelectedValue = dt.Rows[0][7].ToString();
                    ddlExamYear.SelectedValue = dt.Rows[0][8].ToString();
                    ddlSession.SelectedValue = dt.Rows[0][9].ToString();
                    ddlExamType.SelectedValue = dt.Rows[0][10].ToString();

                    if (ddlExamType.SelectedIndex > 0)
                    {
                        BindExamName();
                        ddlExamname.SelectedValue = dt.Rows[0][1].ToString();
                    }
                    pnlRegistrmasteUpdate.Visible = true;
                    gridDetails.Visible = false;
                    linkSave.Text = "Cancel";
                    txtRegisterNo.Enabled = false;

                    ddlFaculty.Enabled = false; ddlExamname.Enabled = false; ddlExamYear.Enabled = false;
                    ddlSession.Enabled = false; ddlExamType.Enabled = false; ddlCopyType.Enabled = false;
                    txtCurrentBookNo.Enabled = false; txtRefBookNo.Enabled = false;
                    txtPageNoFrom.Enabled = false; txtPageNoTo.Enabled = false;
                    txtCnclReason.Visible = lblCnclReason.Visible = true;
                    pnlAvailablePages.Visible = false; lblLastRegNo.Visible = false;
                }
                else
                    lblSrchRegNo.Text = "This Register No. Is Either Invalid Or Already Cancelled";
            }
        }
    }
    protected void linkCncl_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt16(Request.QueryString["PageInd"]) == 2)
        {
            PlReg.UserID = Convert.ToInt32(Session["UserId"]);
            PlReg.CancelDate = DateTime.Now;
            PlReg.RegNo = Convert.ToInt32(txtRegisterNo.Text);
            PlReg.CancelReason = txtCnclReason.Text;
            PlReg.CancelledIpAddress = Request.UserHostAddress;
            int i = BlReg.CancelRegister(PlReg);
            if (i > 0)
            {
                lblmsg.Text = "Registration No:- " + txtRegisterNo.Text + " Cancelled Successfully";
            }
            Clear();

        }
    }
    protected void linkAddAvailablePages_Click(object sender, EventArgs e)
    {
        lblAvalPgsMsg.Text = "";
        if (Convert.ToInt32(txtPageNoFromAvail.Text) > Convert.ToInt32(txtPageNoToAvail.Text))
        {
            lblAvalPgsMsg.Text = "Page No. From Value Must Be Less Than Page No. To";
            txtPageNoFromAvail.Focus(); return;
        }
        if (Convert.ToInt32(txtPageNoToAvail.Text) > Convert.ToInt32(txtPageNoTo.Text))
        {
            lblAvalPgsMsg.Text = "Available Pages Value Must Be Less Or Equal To No. Of Pages-  " + lblTotalNoOfPages.Text + "";
            txtPageNoToAvail.Focus(); txtPageNoToAvail.Text = ""; return;
        }
        else
        {
            pnlAvlbPgs.Visible = true; AddRowsToGrid(); txtPageNoFromAvail.Text = txtPageNoToAvail.Text = "";
            txtPageNoFromAvail.Focus(); lblTotalMsg.Visible = true; lblTotal.Visible = true;
        }
    }
    private void AddRowsToGrid()
    {
        if (ViewState["AvailablePages"] == null)
        {
            dtGrd.Columns.Add("RegisterNo", typeof(Int64));
            dtGrd.Columns.Add("PageNoFrom", typeof(int));
            dtGrd.Columns.Add("PageNoTo", typeof(int));
            dtGrd.Columns.Add("TotalPages", typeof(int));

            for (int k = 0; k < grdAvailablePages.Rows.Count; k++)
            {
                dtGrd.Rows.Add(k, Convert.ToInt32(grdAvailablePages.DataKeys[k].Values["PageNoFrom"]),
                    Convert.ToInt32(grdAvailablePages.DataKeys[k].Values["PageNoTo"]),
                    Convert.ToInt32(grdAvailablePages.DataKeys[k].Values["TotalPages"]));
            }
        }
        else
        {
            dtGrd = (DataTable)ViewState["AvailablePages"];
        }
        if (dtGrd.Rows.Count > 0)
        {
            int max = Convert.ToInt32(dtGrd.AsEnumerable().Max(row => row["PageNoTo"]));

            if (Convert.ToInt32(txtPageNoFromAvail.Text) <= max)
            {
                lblAvalPgsMsg.Text = "Page No. Already Exists.";
                return;
            }
        }
        dtGrd.Rows.Add(i, Convert.ToInt32(txtPageNoFromAvail.Text), Convert.ToInt32(txtPageNoToAvail.Text), Convert.ToInt32(txtPageNoToAvail.Text) - Convert.ToInt32(txtPageNoFromAvail.Text) + 1);
        grdAvailablePages.DataSource = dtGrd;
        grdAvailablePages.DataBind();
        ViewState["AvailablePages"] = dtGrd;
        TotalPg = Convert.ToInt32(lblTotal.Text);
        for (int l = 0; l < dtGrd.Rows.Count; l++)
        {
            lblTotal.Text = (TotalPg + Convert.ToInt32(dtGrd.Rows[l]["TotalPages"])).ToString();
        }
        dtGrd = null;
    }
    protected void grdAvailablePages_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        dtGrd = (DataTable)ViewState["AvailablePages"];
        TotalPg = Convert.ToInt32(lblTotal.Text);
        lblTotal.Text = (TotalPg - Convert.ToInt32(dtGrd.Rows[e.RowIndex]["TotalPages"])).ToString();
        dtGrd.Rows.Remove(dtGrd.Rows[e.RowIndex]);
        grdAvailablePages.DataSource = dtGrd;
        grdAvailablePages.DataBind();
    }
    protected void grdAvailablePages_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}


