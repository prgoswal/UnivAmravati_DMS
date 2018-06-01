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
public partial class FrmImageInsertion : System.Web.UI.Page
{
    SqlCommand cmd;
    SqlDataAdapter da;
    DataTable dt;
    DataRow dr;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["dtInSession"] = null;
        }
        dt = new DataTable();
        dt.Columns.Add("Image");
        dt.Columns.Add("ImageNo");
    }
    protected void UploadButton_Click(object sender, EventArgs e)
    {

    }
    protected void linkAdd_Click(object sender, EventArgs e)
    {
        string fileNm = ""; ;
        if (Session["dtInSession"] != null)
            dt = (DataTable)Session["dtInSession"]; //Getting datatable from session
        string fileName = "";
        if (FileUpload1.HasFile)
        {
            fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string ext = Path.GetExtension(fileName);
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
        
    }
    protected void linkCancel_Click(object sender, EventArgs e)
    {
        // Response.Redirect("Home.aspx");
    }
    string imageNo;
    void imageShow(string imageno)
    { 
    
    }
    protected void linkSave_Click(object sender, EventArgs e)
    {

    }
    protected void grdImagedetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
        if (e.CommandName == "ShowPopup")
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["dtInSession"];
             LinkButton btndetails = (LinkButton)e.CommandSource;
             GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
             int rwindex=gvrow.RowIndex;


             ImageSelected.ImageUrl = dt1.Rows[rwindex]["Image"].ToString();
           
          //  LinkButton lnk = (LinkButton)e.Row.FindControl("LinkButtonEdit");
           
          // lblID.Text = grdImagedetails.DataKeys[gvrow.RowIndex].Value.ToString();
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
    // protected void grdImagedetails_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //try
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {


    //        LinkButton lnk = (LinkButton)e.Row.FindControl("Link");

    //        string url = "popuppage.aspx?ProductID=" + lnk.Text;

    //        lnk.Attributes.Add("onClick", "JavaScript: window.open('" + url + "','','_blank','opacity=2,width=500,height=245,left=350,top=400')");

    //    }
    //}
    //catch (Exception)
    //{ }
    //}
}