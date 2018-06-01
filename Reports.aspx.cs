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
using System.Drawing;


public partial class Reports : System.Web.UI.Page
{
    DataTable dt;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string script = "$(document).ready(function () { $('[id*=btnShowReport]').click(); });";
            ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
        }
    }
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        DateTime dateTo, dateFrom;
        if (!DateTime.TryParse(txtDateFrom.Text, out dateFrom))
        {
            lblMsg.Text = "Invalid Date.";
            return;
        }
        if (!DateTime.TryParse(txtDateTo.Text, out dateTo))
        {
            lblMsg.Text = "Invalid Date.";
            return;
        }
        

        Hashtable ht = new Hashtable();
        ht.Add("Ind", 2);
        ht.Add("DateFrom", Convert.ToDateTime(txtDateFrom.Text));
        ht.Add("DateTo", Convert.ToDateTime(txtDateTo.Text));
        ht.Add("SessionType",0);
        ht.Add("ExamYear",0);
        ht.Add("RptInd", 0);
        Session["ht"] = ht;
        Session["Report"] = "RptTrDetails";
        Response.Redirect("RptShow.aspx");
    }
}




//for (int i = 0; i < dt.Rows.Count; i++)
//{
//    FilePath = dt.Rows[i]["FilePath"].ToString();//.Replace("BOI7", "OCCWEB04");
//    string f1 = FilePath.ToString().Substring(0, 20);
//    string f2 = FilePath.ToString().Substring(21, FilePath.Length - 21).Replace("/", @"\");
//    PlRpt.Ind = 8;
//    dt1 = BlRpt.GetPhysicalPath(PlRpt);
//    //string f = @"\\boi7\D\TR\" + f2;
//    string f = dt1.Rows[0][1].ToString() + "\\" + f2;
//    if (Directory.Exists(f))
//    {
//        string[] filePaths = Directory.GetFiles(f);
//        files = new ListItemCollection();
//        foreach (string filePath in filePaths)
//        {
//            string fileName = Path.GetFileName(filePath);
//            //if (fileName.ToString().Split('.')[1] == "JPG")
//            //{
//            files.Add(new ListItem(fileName, FilePath + "/" + fileName));
//            //}
//        }
//        filesError = new ListItemCollection();
//        foreach (string filePath in filePaths)
//        {
//            string fileName = Path.GetFileName(filePath);
//            if (fileName.ToString().Split('.')[1] == "JPG" && fileName.ToString().Length > 12)
//            {
//                filesError.Add(new ListItem(fileName, FilePath + "/" + fileName));
//            }
//        }
//        count = files.Count;
//        ErrorrImages = filesError.Count;
//    }
//    //if (i == 0)
//    //    dt.Columns.Add("ErrorPages", typeof(int));
//    dt.Rows[i]["ActualPages"] = count;
//    dt.Rows[i]["ErrorPages"] = ErrorrImages;
//    dt.AcceptChanges();
//    count = 0; ErrorrImages = 0;
//}