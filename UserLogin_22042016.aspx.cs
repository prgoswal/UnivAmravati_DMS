using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public partial class UserLogin : System.Web.UI.Page
{
    BlTRRegiter BlTrReg = new BlTRRegiter();
    plTrRegister plTrReg = new plTrRegister();

    DataTable dt;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        txtLoginId.Focus();
        if (!IsPostBack)
        {
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {

            plTrReg.UserLoginId = txtLoginId.Text;
            plTrReg.LoginPwd = EncodePassword(txtLoginPwd.Text);
            plTrReg.Ind = 2;
            int i = BlTrReg.CheckLockInd(plTrReg);
            if (i > 0)
            {
                Response.Write("<script> alert ('Enter Login ID Locked : Please Contact With Admin User');</script>");
                txtLoginPwd.Focus();
                return;
            }
            plTrReg.Ind = 1;
            dt = BlTrReg.GetLoginDetails(plTrReg);

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dt.Rows[0]["UserLoginStatus"]) == true)
                {
                    Response.Write("<script> alert ('Current Login Status is Active : Please Logout First From Other Logged-In System');</script>");
                    txtLoginPwd.Focus();
                    return;
                }
                else
                {
                    Session["UserTypeId"] = dt.Rows[0]["UserTypeID"].ToString();
                    Session["UserId"] = dt.Rows[0]["UserId"].ToString();
                    Session["UserType"] = dt.Rows[0]["ItemDesc"].ToString();
                    Session["USERNAME"] = dt.Rows[0]["USERNAME"].ToString();
                    Session["LoginDateTime"] = DateTime.Now;
                    Response.Redirect("Home.aspx");
                }
            }
            else
            {
                Response.Write("<script> alert ('Invalid User Id Or Password');</script>");
                txtLoginPwd.Focus();
            }

        }
        catch (Exception ex) { }
    }

    private String EncodePassword(String Pwd)
    {
        int Calc = 0; String FnlStr = "";
        for (int i = 1; i <= Pwd.Length; i++)
        {
            if (i < 2)
            {
                Calc = 100;
            }
            else if (i < 4)
            {
                Calc = 200;
            }
            else if (i < 6)
            {
                Calc = 300;
            }
            else if (i < 8)
            {
                Calc = 400;
            }
            else if (i < 10)
            {
                Calc = 500;
            }
            byte[] bt = Encoding.ASCII.GetBytes(Pwd.ToString().Substring(i - 1, 1));
            FnlStr = FnlStr + Convert.ToInt32(bt[0] + Calc + i).ToString();
        }
        return FnlStr;
    }



}