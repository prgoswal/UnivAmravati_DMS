using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RptExamWisePunchedRec : System.Web.UI.Page
{
    BlReports Blrep = new BlReports();
    DataTable dt, dtgrid = new DataTable();
    PlReports plrep = new PlReports();
    int tempcounter = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (dtgrid != null && dtgrid.Rows.Count>1)
            divGrid.Visible = true;
        else
            divGrid.Visible = false;

        //GrdMis.UseAccessibleHeader = true;
        //GrdMis.HeaderRow.TableSection = TableRowSection.TableHeader;

        if (!IsPostBack)
        {
            GetExamYear();
            GetExamSession();
        }
    }

    public void GetExamSession()
    {
        plrep.Ind = 2;
        dt = Blrep.GetExamSession(plrep);
        ddlExamSession.DataTextField = "ItemDesc";
        ddlExamSession.DataValueField = "ItemID";
        ddlExamSession.DataSource = dt;

        ddlExamSession.DataBind();
        ddlExamSession.Items.Insert(0, "-- Select Session --");
    }


    public void GetExamYear()
    {
        plrep.Ind = 3;
        dt = Blrep.GetExamYear(plrep);
        ddlExamYear.DataTextField = "ItemDesc";
        ddlExamYear.DataValueField = "ItemID";
        ddlExamYear.DataSource = dt;
        ddlExamYear.DataBind();
        ddlExamYear.Items.Insert(0, "-- Select Year --");
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        AlertWarning.Visible = false;
        AlertRec.Visible = false;

        if (ddlExamSession.SelectedIndex == 0 || ddlExamYear.SelectedIndex == 0)
        {
            divGrid.Visible = false;
            AlertWarning.Visible = true;            
        }
        else
        {
            AlertWarning.Visible = false;            
            plrep.Ind = 8;
            plrep.Session = ddlExamSession.SelectedValue;
            plrep.ExamYear = ddlExamYear.SelectedItem.Text;
            dtgrid = Blrep.MISReport(plrep);

            if (dtgrid.Rows.Count > 1)
            {
                divGrid.Visible = true;
                lblTotalPage.Text = " - " + dtgrid.Rows[Convert.ToInt32(dtgrid.Rows.Count - 1)]["TotalPages"].ToString();
                lblPunchedPage.Text = " - " + dtgrid.Rows[Convert.ToInt32(dtgrid.Rows.Count - 1)]["PunchedPages"].ToString();
                lblDiff.Text = " - " + dtgrid.Rows[Convert.ToInt32(dtgrid.Rows.Count - 1)]["Diff"].ToString();
                lblRegNo.Text = " - " + dtgrid.Rows[Convert.ToInt32(dtgrid.Rows.Count - 1)]["RegisterNo"].ToString();
                dtgrid.Rows.RemoveAt(Convert.ToInt32(dtgrid.Rows.Count - 1));
                dtgrid.Columns.Remove("FacultyCD");
                dtgrid.Columns.Remove("Ctrl");
                dtgrid.Columns.Remove("TRType");
                GrdMis.DataSource = dtgrid;
                GrdMis.DataBind();
                //GrdMis.Columns[2].ItemStyle.Width = 50; 
            }
            else
            {
                divGrid.Visible = false; 
                AlertRec.Visible = true;
            }
            #region
            //DataTable grdDT = new DataTable();
            //grdDT = dt;
            //grdDT.Columns.Add("Diff");
            //int i = 0; Int32 SumTotalPages = 0, SumPunPages = 0 , SumDiff = 0;
            //foreach (DataRow item in dt.Rows)
            //{
            //    SumPunPages = SumPunPages + Convert.ToInt32(item["PunchedPages"]);
            //    SumTotalPages = SumTotalPages + Convert.ToInt32(item["TotalPages"]);
            //    grdDT.Rows[i]["Diff"] = Convert.ToInt32(item["TotalPages"]) - Convert.ToInt32(item["PunchedPages"]);
            //    SumDiff = SumDiff + Convert.ToInt32(grdDT.Rows[i]["Diff"]);
            //    i++;
            //}
            //grdDT.Columns.Remove("FacultyCD");
            //grdDT.Columns.Remove("Ctrl");
            //grdDT.Columns.Remove("TRType");
            //grdDT.Columns.Remove("ImgPath");
            //grdDT.Columns["RecID"].SetOrdinal(0);     
            //int relationshipCount = grdDT
            //                        .AsEnumerable()
            //                        .Select(r => r.ItemArray[4])//r.Field<string>("RegisterNo"))
            //                        .Distinct()
            //                        .Count();
            //grdDT.Rows.Add();
            //grdDT.Rows[Convert.ToInt32(dt.Rows.Count - 1)]["TotalPages"] = SumTotalPages;
            //grdDT.Rows[Convert.ToInt32(dt.Rows.Count - 1)]["PunchedPages"] = SumPunPages;
            //grdDT.Rows[Convert.ToInt32(dt.Rows.Count - 1)]["Diff"] = SumDiff;
            //grdDT.Rows[Convert.ToInt32(dt.Rows.Count - 1)]["RegisterNo"] = relationshipCount-1;
            //#region dt column header change
            ////grdDT.Columns["RecID"].ColumnName = "Rec Id";
            ////grdDT.Columns["FacultyName"].ColumnName = "Faculty Name";
            ////grdDT.Columns["ExamName"].ColumnName = "Rec Id";
            ////grdDT.Columns["TRTypeDesc"].ColumnName = "Rec Id";
            ////grdDT.Columns["RegisterNo"].ColumnName = "Rec Id";
            ////grdDT.Columns["TotalPages"].ColumnName = "Rec Id";
            ////grdDT.Columns["PunchedPages"].ColumnName = "Rec Id";
            ////grdDT.Columns["Diff"].ColumnName = "Rec Id";
            ////RemoveDuplicates(grdDT);
            //#endregion
            //GrdMis.DataSource = grdDT;
            //GrdMis.DataBind();
            //GrdMis.HeaderRow.Cells[0].Text = "Rec Id";
            //GrdMis.HeaderRow.Cells[1].Text = "Faculty Name";
            //GrdMis.HeaderRow.Cells[2].Text = "Exam Name";
            //GrdMis.HeaderRow.Cells[3].Text = "TR Type Desc";
            //GrdMis.HeaderRow.Cells[4].Text = "Register No.";
            //GrdMis.HeaderRow.Cells[5].Text = "Total Pages";
            //GrdMis.HeaderRow.Cells[6].Text = "Punched Pages";
            //GrdMis.HeaderRow.Cells[7].Text = "Diff.";         
            //GrdMis.Columns[2].ItemStyle.Width = 5;

            //GrdMis.HeaderRow.Cells[0].Width = 50;
            //GrdMis.HeaderRow.Cells[1].Width = 150;
            //GrdMis.HeaderRow.Cells[2].Width = 200;
            //GrdMis.HeaderRow.Cells[3].Width = 121;
            //GrdMis.HeaderRow.Cells[4].Width = 121;
            //GrdMis.HeaderRow.Cells[5].Width = 121;
            //GrdMis.HeaderRow.Cells[6].Width = 121;
            //GrdMis.HeaderRow.Cells[7].Width = 43;
            #endregion
        }
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("RptExamWisePunchedRec.aspx");
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void lnkexcel_Click(object sender, EventArgs e)
    {
        if (GrdMis.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";

            // grdListApplication.Columns.RemoveAt(5);
            string FileName = "2013_Summer" + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

            GrdMis.GridLines = GridLines.Both;
            GrdMis.HeaderStyle.Font.Bold = true;
            GrdMis.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
    }
    protected void GrdMis_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //     tempcounter = tempcounter + 1;
        //    if (tempcounter == 10)
        //    {
        //        e.Row.Attributes.Add("style", "page-break-after: always;");
        //        tempcounter = 0;
        //    }
        //}
        foreach (GridViewRow row in GrdMis.Rows)
        {
            if (row.RowIndex % 10 == 0 && row.RowIndex != 0)
            {
                row.Attributes["style"] = "page-break-after:always;";
            }
        }



    }
    protected void btnprint1_Click(object sender, EventArgs e)
    {
        GrdMis.UseAccessibleHeader = true;
        GrdMis.HeaderRow.TableSection = TableRowSection.TableHeader;
        GrdMis.FooterRow.TableSection = TableRowSection.TableFooter;
     
        string footer = "Register No. " + lblRegNo.Text + "   Total Pages" + lblTotalPage.Text+"   Punched Pages" + lblPunchedPage.Text +"   Diff" + lblDiff.Text;

        StringWriter sw = new StringWriter();        
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GrdMis.RenderControl(hw);
        string gridHTML = sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
        StringBuilder sb = new StringBuilder();

        //////////////////////////////////////////
        string Head = "Data Entry Report For " + ddlExamSession.SelectedItem.Text + "-" + ddlExamYear.SelectedItem.Text;
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload = new function(){");
        sb.Append("var printWin = window.open('', '', 'left=0");
        sb.Append("left=100,top=200,right=100,bottom=300,width=700,height=500,status=0');");        
        sb.Append("printWin.document.write(\""); 
        sb.Append("<html>");
        sb.Append("<head>");
        sb.Append("<style type='text/css'>");
        sb.Append("@media print {"+
                                "thead { display: table-header-group,inline; }"+
                                "}"+
                //"div.chapter, div.appendix {page-break-after: always;}"+
                      "p{ text-align:center;"+"font-size:large; "+
                          "font-family:'Calibri';" +
                          "color:black;"+"margin-left:120px;}"+
                       ".amr{ text-align:center;" + "font-size:x-large; " +
                          "font-family:'Calibri';" +
                          "color:black;" + "margin-left:105px;}"
                );
        sb.Append("</style>");
        sb.Append("</head>");
        sb.Append("<body>");
        sb.Append("<table>");
        sb.Append("<thead><tr><td>"+
                    "<div>" +
                           "<table>"+
                            "<tr>"+
                                "<td rowspan='3'>"+
                                      "<div>" +
                                        "<img class='logo' src='bootstrap-3.3.6-dist/images/logo.png' Width='60px' Height='60px'  />" + //Width='50px' Height='50px'
                                      "</div>" +
                                "</td>"+
                                
                            "</tr>"+
                            "<tr>" +
                            "<td>" +
                            "<p class='amr'>Sant Gadge Baba Amravati University</p>" +
                            "</td>" +
                            "</tr>" +
                            "<tr>"+
                            "<td>" +
                                   "<div>" +

                                    "<p>" + Head + "</p>" +
                                    "</div>" +
                                "</td>" +
                            "</tr>"+                            
                        "</table>"+                    
                    "</div>" +
                  "</td></tr></thead>"
                  );
        sb.Append("<tbody><tr><td>");
        sb.Append(gridHTML);
        sb.Append("</td></tr></tbody>");
        sb.Append("<tfoot><tr><td>" + footer + "</td></tr></tfoot>");
        sb.Append("</table>");
        sb.Append("</body>");
        sb.Append("</html>");

        sb.Append("\");");
        sb.Append("printWin.document.close();");
        sb.Append("printWin.focus();");
        sb.Append("printWin.print();");
        sb.Append("printWin.close();");
        sb.Append("};");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
        GrdMis.DataBind();


    }
}