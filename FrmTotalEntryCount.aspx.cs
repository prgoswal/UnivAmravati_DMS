using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
public partial class FrmTotalEntryCount : System.Web.UI.Page
{  
    DlImgEntry dlImg = new DlImgEntry(); 
    PlImgEntry plImg = new PlImgEntry();
    DataTable dtGrd = new DataTable();
    int i;
    DataTable dt, dt1; DataSet ds; //SqlDataAdapter da; SqlCommand cmd;
    int pos; static int count; PagedDataSource adsource;
    SqlCommand cmd; SqlDataAdapter da;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {  
            BindGrid();       
    }
    public void BindGrid()
    {
        try
        {
            cmd = new SqlCommand("SpReport", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ind", 6);
            //cmd.Parameters.AddWithValue("@RptInd", 0);
            dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                if (Session["UserId"].ToString() != "1")
                {
                    DataView dv = new DataView(dt);
                    dv.RowFilter = " UserID=" + Session["UserId"].ToString();
                    dt1 = new DataTable();
                    dt1 = dv.ToTable();
                    int sum = dt1.AsEnumerable().Sum(row => row.Field<int>("Cnt"));
                    lblCount.Text = "Total No. of Records :- " + sum.ToString();
                    dt1.Columns.Remove("UserID");
                    dt1.Columns.Remove("TotalRecords");
                    grdEntry.DataSource = dt1;
                    grdEntry.DataBind();

                }
                else
                {
                    lblCount.Text = "Total No. of Records :- " + dt.Rows[0]["TotalRecords"].ToString();
                    dt.Columns.Remove("UserID");
                    dt.Columns.Remove("TotalRecords");
                    grdEntry.DataSource = dt;
                    grdEntry.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
       
    }
}
