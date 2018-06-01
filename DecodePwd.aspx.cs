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
using System.Text;
using System.Data.SqlClient;
public partial class DecodePwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string str =DecodePassword(txtDecodePwd.Text);
        lblEncode.Text = str;
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
}
