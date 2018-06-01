using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;
//using System.Windows.Forms;
using System.Threading;

public partial class MovePhotos : System.Web.UI.Page
{
    PlMovePhotos plMovePhotos = new PlMovePhotos();
    BlMovePhotos blMovePhotos = new BlMovePhotos();
        
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    DataTable dt; static string SrcFilePath, folderpath, DestHistoryFilePath, DestFilePath, StrRemainingParts, foldepathNew;
    //static string DestFilePath = "D:\\MLCVOMR\\";
    int TotalParts; static int i, count; static Int64 PartNo; static string[] filePaths; string fileName, LastPartA, LastPartB;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string script = "$(document).ready(function () { $('[id*=linkMove]').click(); });";
            ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
            GetSrcPath();
            GetLastPartNo();
        }
    }

    private void GetSrcPath()
    {
        plMovePhotos.Ind = 3;
        dt = blMovePhotos.GetMachineName(plMovePhotos);
        if (dt.Rows.Count > 0)
        {
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0][0] = "0";
            dt.Rows[0][1] = "-- Select Machine Name --";

            ddlMachineNm.DataSource = dt; // Machine Name
            ddlMachineNm.DataValueField = "MachineID"; ;
            ddlMachineNm.DataTextField = "MachineName";
            ddlMachineNm.DataBind();
            ddlMachineNm.SelectedIndex = 0;
        }
    }
    private string GetDestPath()
    {
        plMovePhotos.Ind = 4;
        dt = blMovePhotos.GetMachineName(plMovePhotos);
        if (dt.Rows.Count > 0)
        {
            DestFilePath = "\\\\" + dt.Rows[0][1].ToString() + "\\D$\\AmrawatiScanImages\\TR\\" + ddlExamYear.SelectedValue.ToString() + "\\" + ddlPartType.SelectedItem.Text.ToString();
            // DestFilePath = "D:\\MLCVOMRImages";
        }
        return DestFilePath;
    }
    private void GetLastPartNo()
    {   
        //For Foil Last Record
        plMovePhotos.Ind = 7;
        dt= blMovePhotos.FindLastNo(plMovePhotos);
        if (dt.Rows.Count > 0)
            lblPartA.Text = "Last No. Of Foil :- " + dt.Rows[0][1].ToString();

        //For Counter Foil Last Record
        plMovePhotos.Ind = 8;
        dt = blMovePhotos.FindLastNo(plMovePhotos);
        if (dt.Rows.Count > 0)
        lblPartB.Text = "Last No. Of Counter Foil :- " + dt.Rows[1][1].ToString();        
    }
    protected void linkMove_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtPartFrom.Text) && !string.IsNullOrEmpty(txtPartTo.Text))
        {
            //if (ddlPartType.SelectedIndex == 1 && Convert.ToInt32(txtPartTo.Text) > 50000 || Convert.ToInt32(txtPartFrom.Text) > 50000)
            //{
            //    lblmsg.Text = "Part From & To Value Must Be Less Than 50000 For Part A";
            //    return;
            //}
            //else if (ddlPartType.SelectedIndex == 2 && Convert.ToInt32(txtPartTo.Text) < 50000 || Convert.ToInt32(txtPartTo.Text) < 50000)
            //{
            //    lblmsg.Text = "Part From & To Value Must Be Greater Than 50000 For Part B";
            //    return;
            //}            

            StrRemainingParts = ""; count = 0;
            plMovePhotos.Ind = 5;
            plMovePhotos.PartNoFrom = Convert.ToInt32(txtPartFrom.Text);
            plMovePhotos.PartNoTo = Convert.ToInt32(txtPartTo.Text);
            int j = blMovePhotos.GetPartNoFromTo(plMovePhotos);
            if (Convert.ToInt32(txtPartTo.Text) - Convert.ToInt32(txtPartFrom.Text) + 1 == j)
            {
                System.Threading.Thread.Sleep(5000);
                //SrcFilePath = "\\\\" + ddlMachineNm.SelectedItem.Text + "\\D$\\MLCVIMAGES" + ddlMachineNm.SelectedItem.Text.ToString() + "\\" + ddlPartType.SelectedItem.Text + ddlMachineNm.SelectedItem.Text.ToString();
                SrcFilePath = "\\\\" + ddlMachineNm.SelectedItem.Text + "\\D$\\AmrawatiCropImages\\TR\\" + ddlExamYear.SelectedValue.ToString()+"\\" + ddlPartType.SelectedItem.Text.ToString();
                plMovePhotos.Ind = 2;
                plMovePhotos.PartNoFrom = Convert.ToInt32(txtPartFrom.Text);
                plMovePhotos.PartNoTo = Convert.ToInt32(txtPartTo.Text);
                TotalParts = Convert.ToInt32(txtPartTo.Text) - Convert.ToInt32(txtPartFrom.Text) + 1;
                GetDestPath();

                plMovePhotos.Ind = 1;
                plMovePhotos.PartType = Convert.ToInt32(ddlPartType.SelectedValue);
                plMovePhotos.PartNoFrom = Convert.ToInt32(txtPartFrom.Text);
                plMovePhotos.PartNoTo = Convert.ToInt32(txtPartTo.Text);
                plMovePhotos.SourcePath = SrcFilePath;
                plMovePhotos.DestPath = DestFilePath;
                int k = blMovePhotos.InsertDestPath(plMovePhotos);
                //}
                
                
                //if (Directory.Exists(SrcFilePath))
                //{
                    //bool arr = filePaths.ToString().Split('.')[0].ToString().Substring(0, 5).Contains((Convert.ToInt32(txtPartFrom.Text) + 1).ToString());
                //    filePaths = Directory.GetFiles(SrcFilePath);
                //}

                string SrcFilePathFolder;
                for (i = 0; i < TotalParts; i++)
                {
                    SrcFilePathFolder = SrcFilePath + "\\" + (Convert.ToInt32(txtPartFrom.Text) + i);
                    if (Directory.Exists(SrcFilePath))
                        filePaths = Directory.GetFiles(SrcFilePathFolder);
                    

                    //foldepathNew = DestFilePath + "\\" + ddlPartType.SelectedItem.Text + "\\" + (Convert.ToInt32(txtPartFrom.Text)) + "-" + (Convert.ToInt32(txtPartTo.Text));
                    //folderpath = DestFilePath + "\\" + ddlPartType.SelectedItem.Text + "\\" + (Convert.ToInt32(txtPartFrom.Text) + i);
                    folderpath = DestFilePath + "\\" + (Convert.ToInt32(txtPartFrom.Text) + i);
                    //DestHistoryFilePath = "\\\\" + ddlMachineNm.SelectedItem.Text + "\\D$\\" + ddlMachineNm.SelectedItem.Text + "HISTORY" + "\\" + (Convert.ToInt32(txtPartFrom.Text) + i);
                                        

                    //if (!Directory.Exists(DestHistoryFilePath))
                    //    Directory.CreateDirectory(DestHistoryFilePath);  // FOR ANOTHER FOLDER (HISTORY FOLDER) IN SAME MACHINE OF SOURCE MACHINE

                    foreach (string filePath in filePaths)
                    {
                        fileName = Path.GetFileName(filePath);
                        //if (fileName.Contains((Convert.ToInt32(txtPartFrom.Text) + i).ToString()))
                        //{
                            if (fileName.ToString().Split('.')[1] == "JPG")
                            {
                                if (!Directory.Exists(folderpath))  // FOR DESTINATION PATH
                                    Directory.CreateDirectory(folderpath);

                                string DestiFilePath;
                                DestiFilePath = folderpath + "\\" + (Convert.ToInt32(txtPartFrom.Text) + i).ToString() + "_" + fileName.ToString().Substring(4, 4) + "_00.JPG";
                                if (!File.Exists(DestiFilePath))
                                    File.Copy(filePath, DestiFilePath);
                            }

                                //if ((Convert.ToInt32(txtPartFrom.Text) + i).ToString() == fileName.ToString().Substring(0, 5))
                                //{
                                //    if (!File.Exists(folderpath + "\\" + fileName))
                                //        File.Copy(filePath, folderpath + "\\" + fileName);

                                    //if (!File.Exists(DestHistoryFilePath + "\\" + fileName))
                                    //    File.Copy(filePath, DestHistoryFilePath + "\\" + fileName);
                                //}
                            //}
                        //}
                    }

                    
                    //foreach (string filePath in filePaths)
                    //{
                    //    fileName = Path.GetFileName(filePath);
                    //    if (fileName.ToString().Split('.')[1] == "JPG")
                    //    {
                    //        if ((Convert.ToInt32(txtPartFrom.Text) + i).ToString() == fileName.ToString().Substring(0, 5))
                    //        {
                    //            if (File.Exists(filePath))
                    //                File.Delete(filePath);
                    //        }
                    //    }
                    //}                     
                 
                    string[] files = Directory.GetFiles(folderpath);
                    if (files.Length == 0)
                        StrRemainingParts = StrRemainingParts + "," + (Convert.ToInt32(txtPartFrom.Text) + i).ToString();
                    else                    
                        count = count + files.Count();
                    
                }
                if (StrRemainingParts.Length > 0)
                    StrRemainingParts = StrRemainingParts.ToString().Substring(1, StrRemainingParts.Length - 1);
                if (!string.IsNullOrEmpty(StrRemainingParts))
                {
                    plMovePhotos.Ind = 6;
                    plMovePhotos.PartNoFrom = Convert.ToInt32(txtPartFrom.Text);
                    plMovePhotos.PartNoTo = Convert.ToInt32(txtPartTo.Text);
                    plMovePhotos.RemainingPart = StrRemainingParts;
                    plMovePhotos.MoveDate = DateTime.Now;
                    plMovePhotos.TotalImagesCopied = count;
                    int a = blMovePhotos.UpdateRemainingPart(plMovePhotos);
                    lblmsg.Text = "Remaining Part(s) :- " + StrRemainingParts + "<br/>" + "Total Images Transferred :- " + count.ToString();
                }
                else if (count > 0)
                {
                    lblmsg.Text = "Total Images Transferred :- " + count.ToString();
                }
            }
            else
                lblmsg.Text = "This Part No. Not Exists In Master Table.";
            Clear();
            GetLastPartNo();
        }
    }
    private void ConfirmMoveImages()
    {
        filePaths = Directory.GetFiles(SrcFilePath);
        foreach (string filePath in filePaths)
        {
            string fileName = Path.GetFileName(filePath);
            if (fileName.ToString().Split('.')[1] == "JPG")
            {
                if (PartNo.ToString() == fileName.ToString().Substring(0, 5))
                {
                    File.Delete(foldepathNew + "\\" + (Convert.ToInt32(txtPartFrom.Text) + i) + "\\" + fileName);
                    File.Copy(filePath, foldepathNew + "\\" + (Convert.ToInt32(txtPartFrom.Text) + i) + "\\" + fileName);
                }
            }
        }
        if (PartNo == 0)
            PartNo = Convert.ToInt32(txtPartFrom.Text) + i;
    }
    private void Clear()
    {
        ddlPartType.SelectedIndex = ddlMachineNm.SelectedIndex = 0;
        txtPartFrom.Text = txtPartTo.Text = "";
    }
    protected void linkCancel_Click(object sender, EventArgs e)
    {
        Clear();
        lblConfirm.Text = lblmsg.Text = "";
    }
    protected void ddlMachineNm_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
    }
}

