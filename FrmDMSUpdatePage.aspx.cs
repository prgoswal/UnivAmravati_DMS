using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmDMSUpdatePage : System.Web.UI.Page
{
    BlRegister blReg = new BlRegister();
    DlRegister dl = new DlRegister();
    PlRegister pl; DataTable dt;
    static List<int> RightPageNo;
    static int rowIndex;
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    void BindRegister()
    {
        if (string.IsNullOrEmpty(txtRegNo.Text))
        {
            lblMsg.Text = "Please Enter Register No.";
            return;
        }
        RightPageNo = new List<int>();
        lblMsg.Text = "";
        txtRegNo.ReadOnly = false;
        divHideRegi.Visible = false;

        pl = new PlRegister()
        {
            RegNo = Convert.ToInt64(txtRegNo.Text),
        };

        if (RblRegType.SelectedIndex == 0) pl.Ind = 24;
        else pl.Ind = 27;

        ViewState["DataTable"] = dt = dl.GetPageNo(pl);
        if (dt.Rows.Count > 0)
        {
            //DataTable dtError = dt;
            if (RblRegType.SelectedIndex == 1) //For Register With In Range
            {
                int c = dt.Rows.Count;
                int pos = 0;
                for (int i = 0; i < c; i++)
                {
                    DataRow item = dt.Rows[pos];
                    string s = item["FoilFilePath"].ToString();
                    if (string.IsNullOrEmpty(s))
                    {
                        dt.Rows.RemoveAt(pos);
                        //pos++;
                        continue;
                    }
                    s = s.Substring(s.LastIndexOf("/"), s.LastIndexOf(".") - s.LastIndexOf("/"));
                    s = s.Substring(s.IndexOf("_") + 1, s.LastIndexOf("_") - s.IndexOf("_") - 1);

                    if (Convert.ToInt16(s) == Convert.ToInt16(item["FoilPageNo"])) //For Matching FilePath To Page No
                        dt.Rows.RemoveAt(pos);
                    else
                    {
                        pos++;
                        RightPageNo.Add(Convert.ToInt16(s));
                    }
                }
                #region By ForEach
                //foreach (DataRow item in dt.Rows)
                //{
                //    string s = item["FoilFilePath"].ToString();
                //    s = s.Substring(s.LastIndexOf("/"), s.LastIndexOf(".") - s.LastIndexOf("/"));
                //    s = s.Substring(s.IndexOf("_") + 1, s.LastIndexOf("_") - s.IndexOf("_") - 1);
                //    if (Convert.ToInt16(s) == Convert.ToInt16(item["FoilPageNo"]))
                //    {
                //       dt.Rows[dt.Rows.IndexOf(item)].Delete();
                //    }
                //}
                #endregion
            }

            if (dt.Rows.Count <= 0)
            {
                AllClear();
                lblMsg.Text = "No Data Found!";
                return;
            }
            gvPages.DataSource = dt;
            gvPages.DataBind();
            txtRegNo.ReadOnly = true;
            divHideRegi.Visible = true;
        }

        else
        {
            AllClear();
            lblMsg.Text = "No Data Found!";
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            dt = (DataTable)ViewState["DataTable"];
            pl = new PlRegister()
            {
                RegNo = Convert.ToInt64(txtRegNo.Text),
                TRType = Convert.ToInt16(dt.Rows[rowIndex]["ErrorIn"]),
                FilePath = imgFilePath.ImageUrl,
            };

            if (Convert.ToInt16(dt.Rows[rowIndex]["ErrorIn"]) == 1)
            {
                pl.PageNoTo = Convert.ToInt32(txtFoilUpdate.Text);
                pl.PageNoFrom = Convert.ToInt32(dt.Rows[rowIndex]["FoilPageNo"]);
            }
            else
            {
                pl.PageNoTo = Convert.ToInt32(txtCFUPdate.Text);
                pl.PageNoFrom = Convert.ToInt32(dt.Rows[rowIndex]["CFPageNo"]);
            }

            dt = new DataTable();
            dt = blReg.UpdatePages(pl);
            if (dt.Rows.Count > 0)
            {
                lblMsg.Text = "Page No. Updated Successfully!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Page No. Updated Successfully!')", true);
            }
            else Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Page No. Not Update!')", true);
            
            BindRegister();
            divEditEntry.Visible = false;

        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        AllClear();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            BindRegister();
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void gvPages_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowImg")
        {
            try
            {
                dt = (DataTable)ViewState["DataTable"];
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                rowIndex = row.RowIndex;
                imgFilePath.ImageUrl = dt.Rows[rowIndex]["FoilFilePath"].ToString();

                if (RblRegType.SelectedIndex == 0)
                {
                    txtFoilUpdate.Text = dt.Rows[rowIndex]["FoilPageNo"].ToString();
                    txtCFUPdate.Text = dt.Rows[rowIndex]["CFPageNo"].ToString();
                }
                else
                {
                    txtFoilUpdate.Text = RightPageNo[rowIndex].ToString();
                    txtCFUPdate.Text = RightPageNo[rowIndex].ToString();
                }

                txtFoilCur.Text = dt.Rows[rowIndex]["FoilPageNo"].ToString();
                txtCFCur.Text = dt.Rows[rowIndex]["CFPageNo"].ToString();
                txtFoilCur.ReadOnly = true;
                txtCFCur.ReadOnly = true;
                divEditEntry.Visible = true;

                if (Convert.ToInt16(dt.Rows[rowIndex]["ErrorIn"]) == 1)
                {
                    pnlFoil.Visible = true;
                    pnlCF.Visible = false;
                }
                else if (Convert.ToInt16(dt.Rows[rowIndex]["ErrorIn"]) == 2)
                {
                    pnlCF.Visible = true;
                    pnlFoil.Visible = false;
                }
            }
            catch (Exception ex)
            {
                BindRegister();
                gvPages_RowCommand(sender, e);
                //lblMsg.Text = ex.Message;
            }
        }
    }

    void AllClear()
    {
        divEditEntry.Visible = false;
        divHideRegi.Visible = false;
        imgFilePath.ImageUrl = "";
        txtRegNo.ReadOnly = false;
        lblMsg.Text = "";
        txtCFCur.Text = txtCFUPdate.Text = txtFoilCur.Text = txtFoilUpdate.Text = txtRegNo.Text = "";
    }
    protected void RblRegType_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvPages.DataSource = null;
        gvPages.DataBind();
        divEditEntry.Visible = false;
        divHideRegi.Visible = false;
        lblMsg.Text = "";
    }
}