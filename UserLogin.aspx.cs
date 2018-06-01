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
using System.Runtime.InteropServices;

public partial class UserLogin : System.Web.UI.Page
{
    #region 

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
                Session["UserTypeId"] = dt.Rows[0]["UserTypeID"].ToString();
                Session["UserId"] = dt.Rows[0]["UserId"].ToString();
                Session["UserType"] = dt.Rows[0]["ItemDesc"].ToString();
                Session["USERNAME"] = dt.Rows[0]["USERNAME"].ToString();
                Session["LoginDateTime"] = DateTime.Now;
                Response.Redirect("Home.aspx");
            }
            else
            {
                Response.Write("<script> alert ('Invalid User Id Or Password');</script>");
                txtLoginPwd.Focus();
            }
        }
        catch (Exception ex) 
        { 
        
        }
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

    #endregion

    #region New

    //BlTRRegiter BlTrReg = new BlTRRegiter();
    //plTrRegister plTrReg = new plTrRegister();

    //DataTable dt;
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    //Response.Write("<script>window.name=Date(); $('#hfvalue') .val(window .name);  </script>");
    //    txtLoginId.Focus();
    //    if (!IsPostBack)
    //    {
    //        pnlpassword.Visible = false;
    //        pnllogout.Visible = false;
    //        GetMACAddress();
    //    }
    //}
    //string MACADRESS;
    //[DllImport("Iphlpapi.dll")]
    //private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
    //[DllImport("Ws2_32.dll")]
    //private static extern Int32 inet_addr(string ip);

    //public string GetMACAddress()
    //{
    //    string userip = Request.UserHostAddress;
    //    string strClientIP = Request.UserHostAddress.ToString().Trim();
    //    Int32 ldest = inet_addr(strClientIP);
    //    Int32 lhost = inet_addr("");
    //    Int64 macinfo = new Int64();
    //    Int32 len = 6;
    //    int res = SendARP(ldest, 0, ref macinfo, ref len);
    //    string mac_src = macinfo.ToString("X");

    //    while (mac_src.Length < 12)
    //    {
    //        mac_src = mac_src.Insert(0, "0");
    //    }

    //    string mac_dest = "";

    //    for (int i = 0; i < 11; i++)
    //    {
    //        if (0 == (i % 2))
    //        {
    //            if (i == 10)
    //            {
    //                mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
    //            }
    //            else
    //            {
    //                mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
    //            }
    //        }
    //    }
    //    MACADRESS = mac_dest;
    //    return MACADRESS;


    //}

    //void PopupPrint(bool isDisplayPopupPrint)
    //{
    //    StringBuilder builder = new StringBuilder();
    //    if (isDisplayPopupPrint)
    //    {
    //        builder.Append("<script language=JavaScript> ShowPopupPrint(); </script>\n");
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPopup", builder.ToString());
    //    }
    //    else
    //    {
    //        builder.Append("<script language=JavaScript> HidePopupPrint(); </script>\n");
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "HidePopup", builder.ToString());
    //    }
    //}
    //protected void btnLogin_Click(object sender, EventArgs e)
    //{
    //    lblmsg1.Text = "";
    //    if (Convert.ToString(Session["UserLoginId"]) != "" && Convert.ToString(Session["UserLoginId"]) != null && Convert.ToString(Session["UserLoginPwd"]) != "" && Convert.ToString(Session["UserLoginPwd"]) != null)
    //    {
    //        plTrReg.Ind = 1;
    //        plTrReg.UserLoginId = Convert.ToString(Session["UserLoginId"]);
    //        plTrReg.UserLoginPwd = EncodePassword(txtLoginPwd.Text);
    //        dt = BlTrReg.UpdateInvalidAttempts(plTrReg);
    //        if (dt.Rows.Count > 0)
    //        {
    //            Session["UserType"] = Convert.ToString(dt.Rows[0]["ItemDesc"]);
    //            Session["tabId"] = hfvalue.Value.ToString();
    //            if (Convert.ToString(Session["HistoryCnt"]) == "0")
    //            {
    //                Session["st"] = "1";
    //                Response.Redirect("FrmDMSChangePassword.aspx");
    //            }
    //            else
    //            {
    //                Response.Redirect("Home.aspx");
    //            }
    //        }
    //        else
    //        {
    //            lblmsg1.Text = "Please Check Password!";
    //            txtLoginPwd.Text = "";
    //            txtLoginPwd.Focus();
    //            lblmsg1.Visible = true;
    //        }
    //    }
    //    else
    //    {
    //        Response.Redirect("UserLogin.aspx");
    //    }

    //}

    //private String EncodePassword(String Pwd)
    //{
    //    int Calc = 0; String FnlStr = "";
    //    for (int i = 1; i <= Pwd.Length; i++)
    //    {
    //        if (i < 2)
    //        {
    //            Calc = 100;
    //        }
    //        else if (i < 4)
    //        {
    //            Calc = 200;
    //        }
    //        else if (i < 6)
    //        {
    //            Calc = 300;
    //        }
    //        else if (i < 8)
    //        {
    //            Calc = 400;
    //        }
    //        else if (i < 10)
    //        {
    //            Calc = 500;
    //        }
    //        byte[] bt = Encoding.ASCII.GetBytes(Pwd.ToString().Substring(i - 1, 1));
    //        FnlStr = FnlStr + Convert.ToInt32(bt[0] + Calc + i).ToString();
    //    }
    //    return FnlStr;
    //}
    //protected void btnnext_Click(object sender, EventArgs e)
    //{
    //    lblmsg1.Text = "";
    //    GetMACAddress();
    //    //  Mac();

    //    DataSet mydataset = null;
    //    string loginid = txtLoginId.Text;
    //    plTrReg.Ind = 22;
    //    plTrReg.MacAddress = MACADRESS;
    //    plTrReg.UserLoginId = loginid;
    //    mydataset = BlTrReg.GetUserLoginID(plTrReg);

    //    if (mydataset != null)
    //    {


    //        if (mydataset.Tables[0].Rows.Count > 0)
    //        {
    //            if (mydataset.Tables[0].Rows[0]["MacAddress"].ToString() == plTrReg.MacAddress)
    //            {
    //                if (mydataset.Tables[0].Rows[0]["LockInd"].ToString().ToUpper() != "TRUE")
    //                {
    //                    if (Convert.ToDateTime(mydataset.Tables[0].Rows[0]["UserValidity"].ToString()) >= DateTime.Now.Date)
    //                    {
    //                        if (mydataset.Tables[0].Rows[0]["IsActive"].ToString().ToUpper() == "TRUE")
    //                        {
    //                            Session["UserId"] = mydataset.Tables[0].Rows[0]["UserId"].ToString();
    //                            Session["UserValidity"] = mydataset.Tables[0].Rows[0]["UserValidity"].ToString();
    //                            Session["UserTypeId"] = mydataset.Tables[0].Rows[0]["UserTypeId"].ToString();
    //                            Session["UserName"] = mydataset.Tables[0].Rows[0]["UserName"].ToString();
    //                            Session["LoginDateTime"] = DateTime.Now;
    //                            Session["HistoryCnt"] = mydataset.Tables[0].Rows[0]["HistoryCnt"].ToString();
    //                            Session["UserLoginPwd"] = mydataset.Tables[0].Rows[0]["UserLoginPwd"].ToString();
    //                            Session["UserLoginId"] = mydataset.Tables[0].Rows[0]["UserLoginId"].ToString();
    //                            if (mydataset.Tables[0].Rows[0]["UserLoginStatus"].ToString().ToUpper() == "TRUE")
    //                            {
    //                                lbllogout.Text = txtLoginId.Text;
    //                                pnllogout.Visible = true;
    //                                pnlpassword.Visible = false;
    //                                pnlusernmae.Visible = false;
    //                                txtlogoutpassword.Focus();
    //                            }
    //                            else
    //                            {
    //                                if (Convert.ToInt32(mydataset.Tables[1].Rows[0]["LoginCnt"]) == 0)
    //                                {
    //                                    lbluser.Text = "Welcome : " + txtLoginId.Text;
    //                                    pnlusernmae.Visible = false;
    //                                    pnlpassword.Visible = true;
    //                                    txtLoginPwd.Focus();
    //                                }
    //                                else
    //                                {
    //                                    lblmsg1.Text = "This Machine Is Already Used By Login-ID : " + mydataset.Tables[1].Rows[0]["UserLoginID"].ToString();
    //                                    return;
    //                                }
    //                            }
    //                        }
    //                        else
    //                        {
    //                            lblmsg1.Text = "User are not Active.";
    //                            return;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        lblmsg1.Text = "User Login Date Is Expired.Please Contact to Admin.";
    //                        return;
    //                    }
    //                }
    //                else
    //                {
    //                    lblmsg1.Text = "User Is Blocked. Please Contact to Admin";
    //                    return;
    //                }
    //            }
    //            else
    //            {
    //                lblmsg1.Text = "MAC Address Not Valid";
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            lblmsg1.Text = "Login ID Not Valid";
    //            return;
    //        }

    //        mydataset = null;
    //    }

    //}

    //protected void btnlogout_Click(object sender, EventArgs e)
    //{
    //    lblmsg1.Text = "";
    //    if (Convert.ToString(Session["UserLoginPwd"]) == Convert.ToString(EncodePassword(txtlogoutpassword.Text)))
    //    {
    //        plTrReg.Ind = 3;
    //        plTrReg.UserId = Convert.ToInt32(Session["UserId"]);
    //        int i = BlTrReg.ChangeLoginIdStatus(plTrReg);
    //        Session["USERNAME"] = null;
    //        Session["UserId"] = null;
    //        Session["UserType"] = null;
    //        Session.Clear();
    //        Session.Abandon();
    //        Response.Redirect("UserLogin.aspx");
    //    }
    //    else
    //    {
    //        lblmsg1.Text = "password not matched";
    //        return;
    //    }
    //}

    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //}
    //protected void btnsave_Click(object sender, EventArgs e)
    //{
    //}
    //public String DecodePassword(String Pwd)
    //{
    //    int Calc = 0; String FnlStr = "";
    //    int i = 1; int j = 1; int A = 0; byte[] bt; String Str = ""; ;
    //    while (i <= Pwd.Length)
    //    {
    //        if (j < 2)
    //        {
    //            Calc = 100;
    //        }
    //        else if (j < 4)
    //        {
    //            Calc = 200;
    //        }
    //        else if (j < 6)
    //        {
    //            Calc = 300;
    //        }
    //        else if (j < 8)
    //        {
    //            Calc = 400;
    //        }
    //        else if (j < 10)
    //        {
    //            Calc = 500;
    //        }
    //        A = Convert.ToInt32(Pwd.ToString().Substring(i - 1, 3)) - Calc - j;
    //        bt = new byte[1];
    //        bt[0] = (byte)A;
    //        Str = Encoding.ASCII.GetString(bt);
    //        FnlStr = FnlStr + Str;
    //        i = i + 3;
    //        j++;
    //    }
    //    return FnlStr;
    //}

    //protected void btnsavepassword_Click(object sender, EventArgs e)
    //{
    //    plTrReg.Ind = 16;
    //    plTrReg.UserId = Convert.ToInt32(Session["UserId"]);
    //    plTrReg.LoginPwd = EncodePassword(txtConfirmPwd.Text);
    //    dt = BlTrReg.ChangePasswordFirst(plTrReg);
    //}

    //protected void linklogin_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("UserLogin.aspx");
    //}
    //protected void linklogout_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("UserLogin.aspx");
    //}

    #endregion
}