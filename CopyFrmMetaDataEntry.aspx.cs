using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CopyFrmMetaDataEntry : System.Web.UI.Page
{
    DlImgEntry dlImg = new DlImgEntry();
    PlImgEntry plImg; PlRegister plReg;
    DataTable dt;
    DataSet ds;

    protected void Page_Load(object sender, EventArgs e)
    {
        txtRegNo.Focus();
    }
    DataTable GetCompletedPageNo()
    {
        plImg = new PlImgEntry()
        {
            Ind = 2,
            RegNo = Convert.ToInt64(txtRegNo.Text),
        };
        dt = new DataTable();
        dt = dlImg.GetICompetedPages(plImg);
        return dt;
    }

    protected void btnSearchImg_Click(object sender, EventArgs e)
    {
        if (txtRegNo.Text == string.Empty)
        {
            lblError.Text = "Please Enter Register No.";
            txtRegNo.Focus();
            return;
        }

        plImg = new PlImgEntry()
        {
            Ind = 12,
            RegNo = Convert.ToInt64(txtRegNo.Text),
            UserId = Convert.ToInt32(Session["UserId"]),
        };

        plReg = new PlRegister()
        {
            Ind = 11,
            RegNo = Convert.ToInt64(txtRegNo.Text)
        };
        
        if (Session["UserId"].ToString() != "1")
        {
            ds = new DataSet();
            ds = dlImg.LastPageNoWithLotNo(plImg);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                lblError.Text = "Entered Register No. Not Assign In Your Lot";
                return;
            }
            //lblLotNo.Text = ds.Tables[0].Rows[0][0].ToString();
            if (ds.Tables[1].Rows.Count > 0)
            {
                
            }
        }
    }


}