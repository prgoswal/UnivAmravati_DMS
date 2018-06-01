using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Net;
using System.Drawing.Printing;

public partial class RptShow : System.Web.UI.Page
{
    int RptInd;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ReportViewer1.ShowCredentialPrompts = true;
            //Microsoft.Reporting.WebForms.IReportServerCredentials irsc = new CustomReportCredentials("administrator", "a", "http://mlcv004/ReportServer");
            //Microsoft.Reporting.WebForms.IReportServerCredentials irsc = new CustomReportCredentials("administrator", "a2.", "http://SGBAU/ReportServer");
            Microsoft.Reporting.WebForms.IReportServerCredentials irsc = new CustomReportCredentials(ConfigurationManager.AppSettings["ReportLoginName"], ConfigurationManager.AppSettings["Password"], ConfigurationManager.AppSettings["Reportserver"]); // "http://SGBAU/ReportServer"
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;           
            Hashtable ht = new Hashtable();
            ht = (Hashtable)Session["ht"];
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"].ToString());
            ReportViewer1.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportProjectName"].ToString() + Session["Report"].ToString();
            if (ht != null)
            {
                ReportParameter[] parm = new ReportParameter[ht.Count];
                int i = 0;
                foreach (DictionaryEntry Dt in ht)
                {
                    parm[i] = new ReportParameter(Convert.ToString(Dt.Key), Convert.ToString(Dt.Value));
                    i++;
                }
                //System.Drawing.Printing.PageSettings pg=new System.Drawing.Printing.PageSettings();
                // pg.Margins.Top = 0;
                // pg.Margins.Bottom = 0;
                // pg.Margins.Left = 0;
                // pg.Margins.Right = 0;
                // pg.Landscape = false;
                // System.Drawing.Printing.PaperSize size = new PaperSize();
                // size.RawKind = (int)PaperKind.A4;
                // pg.PaperSize = size;
                // ReportViewer1.SetPageSettings(pg);
                 this.ReportViewer1.ServerReport.Refresh();
                ReportViewer1.ServerReport.SetParameters(parm);
                ReportViewer1.ServerReport.Refresh();
            }
        }
    }
    public class CustomReportCredentials : Microsoft.Reporting.WebForms.IReportServerCredentials
    {
        // From: http://community.discountasp.net/default.aspx?f=14&m=15967
        // local variable for network credential.
        private string _UserName;
        private string _PassWord;
        private string _DomainName;
        public CustomReportCredentials(string UserName, string PassWord, string DomainName)
        {
            _UserName = UserName;
            _PassWord = PassWord;
            _DomainName = DomainName;
        }
        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;  // not use ImpersonationUser
            }
        }
        public ICredentials NetworkCredentials
        {
            get
            {
                // use NetworkCredentials
                return new NetworkCredential(_UserName, _PassWord, _DomainName);
            }
        }
        public bool GetFormsCredentials(out Cookie authCookie, out string user,
            out string password, out string authority)
        {
            // not use FormsCredentials unless you have implements a custom autentication.
            authCookie = null;
            user = password = authority = null;
            return false;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["ht"] = null;
        Session["Report"] = null;
        Response.Redirect("Home.aspx", false);
    }
}