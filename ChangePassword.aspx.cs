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
using System.Data.OleDb;
using System.Text;
public partial class ChangePassword : System.Web.UI.Page
{
    BlChangePwd BlChngPwd = new BlChangePwd();
    PlChangePwd PlChngPwd = new PlChangePwd();
    DataSet Ds; SqlCommand cmd; SqlDataAdapter Da; DataTable dt, dtnull;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

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
    public String DecodePassword(String Pwd)
    {
        int Calc = 0; String FnlStr = "";
        int i = 1; int j = 1; int A = 0; byte[] bt; String Str = ""; ;
        while (i <= Pwd.Length)
        {
            if (j < 2)
            {
                Calc = 100;
            }
            else if (j < 4)
            {
                Calc = 200;
            }
            else if (j < 6)
            {
                Calc = 300;
            }
            else if (j < 8)
            {
                Calc = 400;
            }
            else if (j < 10)
            {
                Calc = 500;
            }
            A = Convert.ToInt32(Pwd.ToString().Substring(i - 1, 3)) - Calc - j;
            bt = new byte[1];
            bt[0] = (byte)A;
            Str = Encoding.ASCII.GetString(bt);
            FnlStr = FnlStr + Str;
            i = i + 3;
            j++;
        }
        return FnlStr;
    }
    protected void linkSave_Click(object sender, EventArgs e)
    {
        try
        {
            linkSave.Enabled = false;
            PlChngPwd.UserLoginId = Convert.ToInt32(Session["UserId"].ToString());
            PlChngPwd.OldPwd = EncodePassword(txtOldPwd.Text);
            PlChngPwd.NewPwd = EncodePassword(txtNewPwd.Text);
            PlChngPwd.Ind = 4;
            int i = BlChngPwd.SelectOldPwd(PlChngPwd);
            if (i > 0)
            {
                dt = CheckOldPwd();
                if (dt.Rows.Count > 0)
                {
                    lblmsg.Text = "You Have Already Used Of This Password : Enter Other Password";
                    return;
                }
                PlChngPwd.Ind = 5;
                int j = BlChngPwd.ChangePwd(PlChngPwd);

                if (j > 0)
                    lblmsg.Text = "Password Change Succesfully";
                else
                    lblmsg.Text = "Password Not Change : Error";
            }
            else
            {
                lblmsg.Text = "Wrong Old Password)";
            }
        }
        catch (Exception ex) { }
        Clear();
        linkSave.Enabled = true;
    }
    private DataTable CheckOldPwd()
    {
        PlChngPwd.Ind = 6;
        PlChngPwd.UserLoginId = Convert.ToInt32(Session["UserId"]);
        PlChngPwd.NewPwd = EncodePassword(txtNewPwd.Text);
        dt = new DataTable();
        dt = BlChngPwd.CheckPassword(PlChngPwd);
        return dt;
    }

    public void Clear()
    {
        txtOldPwd.Text = txtNewPwd.Text = txtConfirmPwd.Text = "";
        linkSave.Enabled = true;
    }

    protected void linkCancel_Click(object sender, EventArgs e)
    {
        Clear();
        lblmsg.Text = "";
    }
}
