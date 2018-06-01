using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UniteGalary2 : System.Web.UI.Page
{

    DlRegister dlReg = new DlRegister();
    PlRegister plReg = new PlRegister();
    DataTable dtGrd = new DataTable(); int  TotalPages;
    DataTable dt; //SqlDataAdapter da; SqlCommand cmd; int pos; int count; PagedDataSource adsource;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    static string FilePath; 
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSearchImg_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtRegNo.Text))
        {
            lblDetails.Text = "Please Enter Register No.";
            txtRegNo.Focus(); return;
        }
        else if (!string.IsNullOrEmpty(txtRegNo.Text))
        {
            plReg.Ind = 11;
            plReg.RegNo = Convert.ToInt64(txtRegNo.Text);
            dt = dlReg.SearchRegister(plReg);
            Session["dt"] = dt;
            if (dt.Rows.Count > 0)
            {
                lblDetails.Text = "Exam Name :- " + dt.Rows[0]["ExamName"].ToString();
                FilePath = dt.Rows[0]["FilePath"].ToString().Replace("OCCWEB04", "BOI7");
                pnlImageViewer.Visible = true;
                TotalPages = GetImagePath(Convert.ToInt64(txtRegNo.Text));
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

    private int GetImagePath(Int64 RegNo)
    {
        dt = new DataTable();
        dt = (DataTable)Session["dt"];       
        dlImages.DataSource = dt;
        dlImages.DataBind();
        return dt.Rows.Count;
    }
}