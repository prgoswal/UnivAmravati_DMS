using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class UserCreation : System.Web.UI.Page
{
    PlUserCreation plUserCreation = new PlUserCreation();
    BlUserCreation blUserCreation = new BlUserCreation();

    BlTRRegiter BlTrReg = new BlTRRegiter();
    plTrRegister plTrReg = new plTrRegister();

    BlChangePwd BlChngPwd = new BlChangePwd();
    PlChangePwd PlChngPwd = new PlChangePwd();

    DataTable dt;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlUserType.Focus();
            BindUserType();
        }
    }

    private DataTable BindUserType()   // Bind User Type Purpose From "SPGetAllMaster", Ind=6, @ItemId From UserType
    {
        plTrReg.ItemID = Convert.ToInt32(Session["UserTypeId"]);
        dt = BlTrReg.GetAllRegister(plTrReg, 6);
        dt.Rows.InsertAt(dt.NewRow(), 0);
        dt.Rows[0][0] = "0";
        dt.Rows[0][1] = "Select User Type";
        ddlUserType.DataSource = dt;
        ddlUserType.DataValueField = "ItemID";
        ddlUserType.DataTextField = "ItemDesc";
        ddlUserType.DataBind();
        return dt;
    }

    protected void linkSave_Click(object sender, EventArgs e)
    {
        plUserCreation.Ind = 8;
        plUserCreation.UserType = Convert.ToInt32(ddlUserType.SelectedValue);
        plUserCreation.UserName = txtUserName.Text;
        plUserCreation.State = txtUserState.Text;
        plUserCreation.City = txtUserCity.Text;
        plUserCreation.Address = txtAddress.Text;
        plUserCreation.ContactNo = txtContactNo.Text;
        plUserCreation.Email = txtEmailAddress.Text;
        plUserCreation.LoginId = txtLoginId.Text;

        plUserCreation.Pwd = EncodePassword(txtLoginId.Text.ToString().Substring(0, 5) + "@" + txtContactNo.Text.ToString().Substring(7, 3));

        plUserCreation.IsOutSideUser = 0;
        plUserCreation.IsActive = 1;
        plUserCreation.CreationDate = DateTime.Now;
        plUserCreation.LoginStatus = 0;
        int i = blUserCreation.InserUser(plUserCreation);
        if (i >= 0)
        {
            lblmsg.Text = "User Created Successfully With Login Password :- " + DecodePassword(plUserCreation.Pwd) + "";
            Clear();
        }
    }

    protected void linkClear_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        if (ddlUserType.SelectedIndex > 0)
            ddlUserType.SelectedIndex = 0;
        txtAddress.Text = txtContactNo.Text = txtEmailAddress.Text = txtLoginId.Text = txtUserCity.Text = "";
        txtUserName.Text = txtUserState.Text = "";
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
}
