using System;
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
using System.Collections;
using System.Drawing;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    DataTable dt;
    DataRow dr;
    string[] col;
    string[] row;
    public static int[] colorrow = new int[30];
    public static int[] colorcol = new int[30];
    static int[] Tcolorrow = new int[30];
    static int[] Tcolorcol = new int[30];
    static int[] ArrDenyRow;
    static int[] ArrDenyCol;
    static int ind = 0;
    static int[] temp;
    public static int beforeind = 1;
    static int arind = 0; int rind; Constraint deny;
    Hashtable ht; SqlCommand cmd; SqlDataAdapter da;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            arind = 0;
            Array.Clear(colorrow, 0, colorrow.Length);
            Array.Clear(colorcol, 0, colorrow.Length);
            Array.Clear(Tcolorrow, 0, Tcolorrow.Length);
            Array.Clear(Tcolorcol, 0, Tcolorrow.Length);
            //Array.Clear(ArrDenyCol, 0, ArrDenyCol.Length);
            // Array.Clear(ArrDenyRow, 0, ArrDenyRow.Length);
            beforeind = 1;
        }

        // int beforeSelect = 1;
        BindDdlUserLevel();
        BindDdlSubMenu();
        //if (!IsPostBack)
        //{
        AddColumns();
        dr = dt.NewRow();
        GetDtRow();
        dt.Rows.Add(dr);
        //ViewState["dt"] = dt;
        BindGrid();

        //GetSelectPermission();
        if (beforeind == 1)
        {
            beforeselect();
            beforeind = 0;
        }

    }
    protected void beforeselect()
    {
        beforepermission();
        ArrDenyCol = new int[0];
        ArrDenyRow = new int[0];
        if (dt.Rows.Count > 0)
        {
            //if (beforeind == 1)
            //{
            ArrDenyCol = new int[dt.Rows.Count];
            ArrDenyRow = new int[dt.Rows.Count];
            for (int k = 0; k < dt.Rows.Count; k++)
            {

                int col = Convert.ToInt32(dt.Rows[k]["AllowMenu"]);
                int row = Convert.ToInt32(dt.Rows[k]["LevelProfile"]);
                row = row - 1;
                // Response.Write("<br/> Index =" + rind + " " + Tcolorcol[k]);
                GridView1.Rows[row - 1].Cells[col].BackColor = Color.LightGreen;
                colorcol[arind] = col;
                colorrow[arind] = row;
                ArrDenyRow[k] = row;
                ArrDenyCol[k] = col;
                arind++;
            }
            beforeind = 0;
            // }
        }
        else
        {
            // ArrDenyRow = new int[1];
            //ArrDenyCol = new int[1];
        }

    }


    protected void beforepermission()
    {
        SqlCommand cmd1 = new SqlCommand("SpMenus", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@ind", 3);
        dt = new DataTable();
        da = new SqlDataAdapter(cmd1);
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {

        }

    }
    //public void GetSelectPermission()
    //{
    //    LinkButton hlDeny = null;

    //    SqlCommand cmd1 = new SqlCommand("SpMenus", con);
    //    cmd1.CommandType = CommandType.StoredProcedure;
    //    cmd1.Parameters.AddWithValue("@ind", 3);
    //    dt = new DataTable();
    //    da = new SqlDataAdapter(cmd1);
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        temp = new int[dt.Rows.Count];
    //        for (int k = 0; k <= dt.Rows.Count - 1; k++)
    //        {
    //            temp[k] = Convert.ToInt32(dt.Rows[k]["AllowMenu"]);
    //            int col = Convert.ToInt32(dt.Rows[k]["LevelProfile"]);
    //            int row = Convert.ToInt32(dt.Rows[k]["AllowMenu"]);
    //            //lblvalue.Text = GridView1.Rows[7].Cells[2].Text;
    //            // Response.Write("<br/> Index =" + rind + " " + Tcolorcol[k]);
    //            GridView1.Rows[col - 1].Cells[row].BackColor = Color.Yellow;
    //            GridView1.Rows[col - 1].Cells[row].Text = "";
    //            hlDeny = new LinkButton();
    //            hlDeny.Text = "";
    //            hlDeny.CommandName = "Click";
    //            hlDeny.Text = "Deny";
    //            hlDeny.CommandArgument = k.ToString();

    //            // add hyperlink control
    //            // GridView1.Rows[col - 1].Cells[row].Text = ""; // clear it out

    //            GridView1.Rows[col - 1].Cells[row].Controls.Add(hlDeny);

    //            //GridView1.Rows[k].Cells[col].ForeColor = Color.Red;
    //        }
    //    }
    //}
    public void BindDdlUserLevel()
    {
        cmd = new SqlCommand("SpGetAllMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ind", 10);
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            dt.Rows.RemoveAt(0);
            row = new string[dt.Rows.Count];
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                row[i] = dt.Rows[i]["LevelDescription"].ToString();
            }
        }
    }
    public void BindDdlSubMenu()
    {
        cmd = new SqlCommand("SpGetAllMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ind", 14);
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0][0] = 0;
            dt.Rows[0][1] = "0";
            col = new string[dt.Rows.Count];

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {

                col[i] = dt.Rows[i]["LevelDescription"].ToString();
            }
        }
    }
    private void AddRow()
    {

        if (ViewState["dt"] == null)
        {

            ViewState["dt"] = dt;

        }
    }


    private void GetDtRow()
    {
        for (int r = 0; r < row.Length; r++)
        {
            for (int i = 0; i < col.Length; i++)
            {
                string ind = (col[i]).ToString();
                if (i == 0)
                    dr[" "] = row[r];
                else
                {

                    dr[ind] = "+";
                }
            }
            if (r < row.Length - 1)
            {
                dt.Rows.Add(dr);
                dr = dt.NewRow();
            }

        }
    }

    private void AddColumns()
    {
        dt = new DataTable();
        DataColumn coll = null;
        for (int i = 0; i < col.Length; i++)
        {
            if (i == 0)
            {
                coll = new DataColumn(" ");
            }
            else
            {
                coll = new DataColumn(col[i]);
            }
            dt.Columns.Add(coll);

        }

    }
    public void BindGrid()
    {

        GridView1.DataSource = dt;
        GridView1.DataBind();
        if (ViewState["dt"] == null)
        {

            ViewState["dt"] = dt;

        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // determine cell you want to set as a hyperlink

            LinkButton hl = null;
            for (int i = 1; i < col.Length; i++)
            {
                // e.Row.Cells[i].Attributes.Add("Style", "background: url(~/Images/correct.png) no-repeat 5px center;");
                hl = new LinkButton();


                //hl.NavigateUrl = e.Row.Cells[i].Text; // take the data from the datatable and make it the URL
                hl.Text = "Allow";

                //e.Row.Cells[4].BackColor = System.Drawing.Color.Red; 
                hl.CommandName = "Click";
                hl.CommandArgument = i.ToString();
                e.Row.Cells[i].Text = ""; // clear it out
                e.Row.Cells[i].Controls.Add(hl);
            }


        }
    }



    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Click")
        {
            int f = 0;
            int r = 0;
            int remrow = -1;
            int remcol = -1;
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int RowIndex = (gvr.RowIndex) + 1;

            int columnindex = Convert.ToInt32(e.CommandArgument);


            int l = RowIndex;

            int m = columnindex;

            if (arind > 0)
            {
                for (int j = 0; j < arind; j++)
                {
                    int ro = colorcol[j];
                    int cl = colorrow[j];
                    if ((colorrow[j] == RowIndex) && (colorcol[j] == columnindex))
                    {
                        remrow = RowIndex;
                        remcol = columnindex;
                        f = 1;
                        break;

                    }

                }
            }
            if (arind < 1)
            {

                colorrow[arind] = RowIndex;
                colorcol[arind] = columnindex;

                arind++;
                f = 1;
            }

            if (f != 1 && arind > 0)
            {
                colorrow[arind] = RowIndex;
                colorcol[arind] = columnindex;
                f = 0;
                arind++;
            }
            for (int j = 0; j < arind; j++)
            {
                {
                    if (colorrow[j] == remrow && colorcol[j] == remcol)
                    {

                    }
                    else
                    {
                        Tcolorrow[r] = colorrow[j];
                        Tcolorcol[r] = colorcol[j];
                        r++;
                    }
                }
            }
            for (int k = 0; k <= r - 1; k++)
            {
                int Tcol = Tcolorcol[k];
                int Trow = Tcolorrow[k];
                colorrow[k] = Tcolorrow[k];
                colorcol[k] = Tcolorcol[k];
                int rind = Tcolorrow[k] + 1;
                GridView1.Rows[Trow - 1].Cells[Tcol].BackColor = Color.Yellow;
            }
            int DltIndR = -1;

            if (ArrDenyCol.Length != null)
            {
                for (int s = 0; s < ArrDenyCol.Length; s++)
                {
                    if (RowIndex == ArrDenyRow[s] && columnindex == ArrDenyCol[s])
                    {
                        GridView1.Rows[ArrDenyRow[s] - 1].Cells[ArrDenyCol[s]].BackColor = Color.White;
                        DltIndR = s;

                    }
                    else
                    {
                        GridView1.Rows[ArrDenyRow[s] - 1].Cells[ArrDenyCol[s]].BackColor = Color.LightGreen;
                    }


                }
            }
            if (DltIndR >= 0)
            {
                ArrDenyCol = ArrDenyCol.Where((source, index) => index != DltIndR).ToArray();
                ArrDenyRow = ArrDenyRow.Where((source, index) => index != DltIndR).ToArray();
            }
            arind = r;

            ind += 1;

        }
    }

    protected void Btnselect_Click(object sender, EventArgs e)
    {
        lblvalue.Text = "";
        try
        {
            //for (int k = 0; k <= arind - 1; k++)
            //{
            //    int DltRind = colorrow[k];
            cmd = new SqlCommand("SpMenus", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Ind", 1);
            //  cmd.Parameters.AddWithValue("@LevelProfile", DltRind);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            // }
            //for (int k = 0; k <= arind - 1; k++)
            //{
            if (arind != 0)
            {
                for (int k = 0; k <= arind - 1; k++)
                {
                    rind = Tcolorrow[k];


                    GridView1.Rows[colorrow[k] - 1].Cells[colorcol[k]].BackColor = Color.LightGreen;
                    //GridView1.Rows[colorrow[k]].Cells[colorcol[k]].ForeColor = Color.Red;

                    cmd = new SqlCommand("SpMenus", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Ind", 2);
                    cmd.Parameters.AddWithValue("@ParentMenuId", 0);
                    cmd.Parameters.AddWithValue("@Title", "Menus");
                    cmd.Parameters.AddWithValue("@Url", "Home.aspx");
                    cmd.Parameters.AddWithValue("@LevelProfile", rind+1);
                    cmd.Parameters.AddWithValue("@AllowMenu", Tcolorcol[k]);
                    // lblvalue.Text = lblvalue.Text + ("<br/> Index =" + rind + " " + Tcolorcol[k]);
                    con.Open();
                    int j = cmd.ExecuteNonQuery();
                    con.Close();
                    if (j > 0)
                    {
                        lblvalue.Text = "Permission Granted Successfully";                        
                    }
                    else
                        lblvalue.Text = "Permission Not Granted Successfully";

                }
            }
            else
            {
                lblvalue.Text = "All Users Authority Deny Successfully";
                beforeind = 0;
                return;
            }
            arind = 0;
            beforeind = 1;

            //Response.Redirect("Home.aspx");

        }
        catch (Exception ex)
        {
            lblvalue.Text = ex.Message;
        }

    }
}
