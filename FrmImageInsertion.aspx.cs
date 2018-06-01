using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using Org.BouncyCastle.Asn1.Ocsp;
public partial class FrmImageInsertion : System.Web.UI.Page
{
    SqlCommand cmd;
    SqlDataAdapter da;
    DataTable dt;
    DataRow dr;
    static int MstImgsCount; static int FolderImgsCnt; static int GrdCount;
    static String[] FilePath;

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["dtInSession"] = null;
        }
        lblMsg.Text = "";
        dt = new DataTable();
        dt.Columns.Add("Image");
        dt.Columns.Add("ImageNo");
        // dt.Columns.Add("ImagePath");

    }

    protected void linkAdd_Click(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(TotalDiff.Text) > 0)
        //{
        //if (grdImagedetails.Rows.Count == 0)
        //    GrdCount = 1;
        //else
        //    GrdCount = GrdCount + grdImagedetails.Rows.Count;
        //if (GrdCount <= Convert.ToInt32(TotalDiff.Text))
        //{

        string fileNm = ""; ;
        if (Session["dtInSession"] != null)
            dt = (DataTable)Session["dtInSession"]; //Getting datatable from session
        string fileName = "";
        if (FileUpload1.HasFile)
        {
            fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            //dr["ImagePath"] = Directory.GetFiles(FileUpload1.PostedFile.FileName);
            string ext = Path.GetExtension(fileName);
            ext = ext.ToUpper();
            if (ext != ".JPG")
            {
                lblMsg.Text = "Image Extention is Not Valid Plz Select .JPG File";
                return;
            }
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/InsertImages/") + fileName);
        }
        else
        {
            lblMsg.Text = "Please Select Image First";
            return;
        }
        string[] filePaths = Directory.GetFiles(Server.MapPath("~/InsertImages/"));

        List<ListItem> files = new List<ListItem>();
        foreach (string filePath in filePaths)
        {
            fileNm = Path.GetFileName(filePath);
            if (fileName == fileNm)
                files.Add(new ListItem(fileNm, "~/InsertImages/" + fileNm));
        }

        dr = dt.NewRow();
        dr["Image"] = "InsertImages/" + fileName;
        dr["ImageNo"] = txtPageNo.Text;
        dt.Rows.Add(dr);
        Session["dtInSession"] = dt;     //Saving Datatable To Session 
        grdImagedetails.DataSource = dt;
        grdImagedetails.DataBind();
        txtLotNo.Enabled = false;
        txtImageNo.Enabled = false;
        //txtPageNo.Text = "";
        //}
        //else
        //    lblMsg.Text = "You Have Inserted Images Equal of Total Difference, No More Images Inserted";
        //}
        // else
        //     lblMsg.Text = "Total Difference '0' So Not Insert Image";

    }
    protected void linkCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    public void masterTblRegImgsCount()
    {
        SqlCommand cmd1 = new SqlCommand("SPInsertImages", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@Ind", 1);
        cmd1.Parameters.AddWithValue("@LotNo", txtLotNo.Text);
        con.Open();
        MstImgsCount = Convert.ToInt32(cmd1.ExecuteScalar());
        con.Close();
        //if (i > 0)
        lblTotalLotImages.Text = MstImgsCount.ToString();
    }
    public void FillAvailablePageGrid()
    {
        SqlCommand cmd1 = new SqlCommand("SPInsertImages", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@Ind", 4);
        cmd1.Parameters.AddWithValue("@LotNo", txtLotNo.Text);
        da = new SqlDataAdapter(cmd1);
        dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            grdAvailablePages.DataSource = dt;
            grdAvailablePages.DataBind();
        }
    }
    public void AvalaiblePageDetails()
    {
        SqlCommand cmd1 = new SqlCommand("Select * from Tbl", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@Ind", 1);
        cmd1.Parameters.AddWithValue("@LotNo", txtLotNo.Text);
        con.Open();
        MstImgsCount = Convert.ToInt32(cmd1.ExecuteScalar());
        con.Close();
        //if (i > 0)
        lblTotalLotImages.Text = MstImgsCount.ToString();
    }

    protected void linkSave_Click(object sender, EventArgs e)
    {
        if (grdImagedetails.Rows.Count > 0)
        {
            for (int i = 0; i < grdImagedetails.Rows.Count; i++)
            {
                cmd = new SqlCommand("SPInsertImages", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ind", 3);
                cmd.Parameters.AddWithValue("@LotNo", txtLotNo.Text);
                cmd.Parameters.AddWithValue("@ImageNo", txtImageNo.Text);
                cmd.Parameters.AddWithValue("@PageNo", txtPageNo.Text);
                cmd.Parameters.AddWithValue("@ImagePath", grdImagedetails.Rows[i].Cells[1].Text.ToString());
                cmd.Parameters.AddWithValue("@InsertBy", Session["UserId"]);
                cmd.Parameters.AddWithValue("@InsertByIp", Request.UserHostAddress);
                con.Open();
                int InsertRow = Convert.ToInt16(cmd.ExecuteNonQuery());
                con.Close();
                if (InsertRow > 0)
                    lblMsg.Text = "Images Send Successfully";
            }
        }
        else
            lblMsg.Text = "Please Insert Image First";

        Clear();
    }
    protected void grdImagedetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "ShowPopup")
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["dtInSession"];
            LinkButton btndetails = (LinkButton)e.CommandSource;
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
            int rwindex = gvrow.RowIndex;
            LblPopupHeading.Text = "Image Details :-    " + "Lot No.= " + txtLotNo.Text + "    Image No.= " + txtImageNo.Text + "    PageNo.= " + dt1.Rows[rwindex]["ImageNo"].ToString();

            ImageSelected.ImageUrl = dt1.Rows[rwindex]["Image"].ToString();

            Popup(true);
        }
    }
    void Popup(bool isDisplay)
    {

        StringBuilder builder = new StringBuilder();
        if (isDisplay)
        {
            builder.Append("<script language=JavaScript> ShowPopup(); </script>\n");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPopup", builder.ToString());
        }
        else
        {
            builder.Append("<script language=JavaScript> HidePopup(); </script>\n");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "HidePopup", builder.ToString());
        }
    }

    protected void LinkClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    public void Clear()
    {
        txtLotNo.Enabled = true;
        txtLotNo.Text = "";
        txtImageNo.Enabled = true;
        txtImageNo.Text = "";
        txtPageNo.Text = "";
        lblMsg.Text = "";
        lblTotalFolderImage.Text = "";
        lblTotalLotImages.Text = "";
        TotalDiff.Text = "";
        grdAvailablePages.DataSource = null;
        grdAvailablePages.DataBind();
        grdImagedetails.DataSource = null;
        grdImagedetails.DataBind();
        // grdImagedetails.Columns.Clear();
        // grdAvailablePages.Columns.Clear();
    }
    public void FolderImgsCount()
    {
        SqlCommand cmd1 = new SqlCommand("SPInsertImages", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@Ind", 2);
        cmd1.Parameters.AddWithValue("@LotNo", txtLotNo.Text);
        con.Open();
        FolderImgsCnt = Convert.ToInt32(cmd1.ExecuteScalar());
        con.Close();
        //if (i > 0)
        lblTotalFolderImage.Text = FolderImgsCnt.ToString();
    }

    protected void txtLotNo_TextChanged(object sender, EventArgs e)
    {
        masterTblRegImgsCount();
        FolderImgsCount();
        TotalDiff.Text = (MstImgsCount - FolderImgsCnt).ToString();
        FillAvailablePageGrid();
    }
}