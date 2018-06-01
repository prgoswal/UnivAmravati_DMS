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
using System.Threading;
using System.ComponentModel;
using System.Runtime.InteropServices;

public partial class MovePhotos : System.Web.UI.Page
{
    PlMovePhotos plMovePhotos = new PlMovePhotos();
    BlMovePhotos blMovePhotos = new BlMovePhotos();
    DataTable dt;
    string TrType;

    int TotalParts;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetSrcPath();
            GetLastPartNo();
        }
    }

    void GetSrcPath()
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
    string GetDestPath()
    {
        string DestFilePath = "";
        plMovePhotos.Ind = 4;
        dt = blMovePhotos.GetMachineName(plMovePhotos);
        if (dt.Rows.Count > 0)
        {
            DestFilePath = "E:\\AmrawatiScanImages\\TR\\" + ddlExamYear.SelectedValue.ToString() + "\\" + TrType + "\\";
            //DestFilePath = "\\\\" + dt.Rows[0][1].ToString() + "\\E$\\Demo2\\TR\\" + ddlExamYear.SelectedValue.ToString() + "\\" + TrType;            
            //DestFilePath = "\\\\" + dt.Rows[0][1].ToString() + "\\E$\\AmrawatiScanImages\\TR\\" + ddlExamYear.SelectedValue.ToString() + "\\" + TrType + "\\";
        }
        return DestFilePath;
    }
    void GetLastPartNo()
    {
        //For Foil Last Record
        plMovePhotos.Ind = 7;
        dt = blMovePhotos.FindLastNo(plMovePhotos);
        if (dt.Rows.Count > 0)
            lblPartA.Text = "Last Move Reg. No. :- " + dt.Rows[0][1].ToString();

        //For Counter Foil Last Record
        plMovePhotos.Ind = 8;
        dt = blMovePhotos.FindLastNo(plMovePhotos);
        if (dt.Rows.Count > 0)
            lblPartB.Text = "Last No. Of Counter Foil :- " + dt.Rows[0][1].ToString();
    }
    DataSet MoveImgaes(string DestFilePath, string SrcFilePath)
    {
        plMovePhotos = new PlMovePhotos()
        {
            Ind = 9,
            PartNoFrom = Convert.ToInt32(txtPartFrom.Text),
            PartNoTo = Convert.ToInt32(txtPartTo.Text),
            DestPath = DestFilePath,
            SourcePath = SrcFilePath,
        };
        return blMovePhotos.MoveImages(plMovePhotos);
    }

    protected void linkMove_Click(object sender, EventArgs e)
    {
        string SrcFilePath = "", DestFilePath = ""; //, StrRemainingParts = "", folderpath, fileName;
        int count = 0; //, first = 0, LastRegNoDest = 0, FirstRegNoDest = 0;
        //string[] filePaths;
        TrType = ddlPartType.SelectedValue == "2" ? "CF" : "Foil";

        try
        {
            if (!string.IsNullOrEmpty(txtPartFrom.Text) && !string.IsNullOrEmpty(txtPartTo.Text))
            {
                plMovePhotos.Ind = 5;
                plMovePhotos.PartNoFrom = Convert.ToInt32(txtPartFrom.Text);
                plMovePhotos.PartNoTo = Convert.ToInt32(txtPartTo.Text);
                int j = blMovePhotos.GetPartNoFromTo(plMovePhotos);
                if (Convert.ToInt32(txtPartTo.Text) - Convert.ToInt32(txtPartFrom.Text) + 1 >= j)
                {
                    #region For Image Transfer By Source But Exception Of Path Accessing Permission.
                    //DestFilePath = GetDestPath();
                    //SrcFilePath = "\\\\" + ddlMachineNm.SelectedItem.Text + "\\E$\\AmrawatiCropImages\\TR\\" + ddlExamYear.SelectedValue.ToString() + "\\" + TrType;
                    //SrcFilePath = "\\\\" + ddlMachineNm.SelectedItem.Text + "\\E$\\AmrawatiCropImages\\TR\\" + ddlExamYear.SelectedValue.ToString() + "\\" + TrType + "\\";
                    //TotalParts = Convert.ToInt32(txtPartTo.Text) - Convert.ToInt32(txtPartFrom.Text) + 1;
                    //string SrcFilePathFolder = ""; 
                    //for (int i = 0; i < TotalParts; i++)
                    //{
                    //SrcFilePathFolder = SrcFilePath + "\\" + (Convert.ToInt32(txtPartFrom.Text) + i);
                    //if (!Directory.Exists(SrcFilePath)) //It Is Match Path For Foil or CFoil.
                    //{
                    //    //lblmsg.Text += SrcFilePath + "\n";
                    //    continue;
                    //}
                    //if (!Directory.Exists(SrcFilePathFolder)) // It Match Register No Exist or Not.
                    //{
                    //    //lblmsg.Text += SrcFilePathFolder + "\n";
                    //    continue;
                    //}

                    //folderpath = DestFilePath + "\\" + (Convert.ToInt32(txtPartFrom.Text) + i);
                    //if (!Directory.Exists(folderpath))  // FOR DESTINATION PATH
                    //    Directory.CreateDirectory(folderpath);

                    //// This For Take Register Name Starting And Last.
                    //FileInfo obj = new FileInfo(folderpath);
                    //if (first==0)
                    //{
                    //    FirstRegNoDest = Convert.ToInt32(obj.Name);
                    //    first++;
                    //}
                    //LastRegNoDest = Convert.ToInt32(obj.Name);

                    //filePaths = Directory.GetFiles(SrcFilePathFolder);
                    //foreach (string filePath in filePaths)
                    //{
                    //    fileName = Path.GetFileName(filePath);

                    //    if (fileName.ToString().Split('.')[1] == "JPG")
                    //    {
                    //        string DestiFilePath;
                    //        DestiFilePath = folderpath + "\\" + (Convert.ToInt32(txtPartFrom.Text) + i).ToString() + "_" + fileName.ToString().Substring(4, 4) + "_00.JPG";
                    //        if (!File.Exists(DestiFilePath))
                    //            File.Copy(filePath, DestiFilePath);
                    //    }
                    //}
                    //string[] files = Directory.GetFiles(folderpath);
                    //if (files.Length == 0)
                    //{
                    //    StrRemainingParts = StrRemainingParts + "," + (Convert.ToInt32(txtPartFrom.Text) + i).ToString();
                    //}
                    //else
                    //{
                    //    count = count + files.Count();
                    //}     
                    //}
                    #endregion

                    SrcFilePath = "E:\\AmrawatiCropImages\\TR\\" + ddlExamYear.SelectedValue.ToString() + "\\" + TrType + "\\";
                    DestFilePath = "E:\\AmrawatiScanImages\\TR\\" + ddlExamYear.SelectedValue.ToString() + "\\" + TrType + "\\";
                    
                    DataSet ds = MoveImgaes(DestFilePath, SrcFilePath); //For Image Transfer By SQL 
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        count = Convert.ToInt32(ds.Tables[ds.Tables.Count - 1].Rows[0][0]);

                        //this is only save all file information src,destination,machine name.
                        plMovePhotos.Ind = 1;
                        plMovePhotos.PartType = Convert.ToInt32(ddlPartType.SelectedValue.Trim());
                        plMovePhotos.PartNoFrom = Convert.ToInt32(txtPartFrom.Text);   //It Is First File From Source.
                        plMovePhotos.PartNoTo = Convert.ToInt32(txtPartTo.Text); //It Is Last File From Destination.
                        plMovePhotos.SourcePath = SrcFilePath;
                        plMovePhotos.DestPath = DestFilePath;
                        int k = blMovePhotos.InsertDestPath(plMovePhotos);
                        //Comments For RemainingPart
                        //if (StrRemainingParts.Length > 0)
                        //    StrRemainingParts = StrRemainingParts.ToString().Substring(1, StrRemainingParts.Length - 1);
                        //if (!string.IsNullOrEmpty(StrRemainingParts))
                        //{
                        //    plMovePhotos.Ind = 6;
                        //    plMovePhotos.PartNoFrom = Convert.ToInt32(txtPartFrom.Text);
                        //    plMovePhotos.PartNoTo = Convert.ToInt32(txtPartTo.Text);
                        //    plMovePhotos.RemainingPart = StrRemainingParts; //
                        //    plMovePhotos.MoveDate = DateTime.Now;
                        //    plMovePhotos.TotalImagesCopied = count;
                        //    int a = blMovePhotos.UpdateRemainingPart(plMovePhotos);
                        //    lblmsg.Text = "Remaining Part(s) :- " + StrRemainingParts + "<br/>" + "Total Images Transferred :- " + count.ToString();
                        //}
                        //else 
                        if (count > 0)
                            lblmsg.Text = "Total Images Transferred :- " + count.ToString();
                        else
                            lblmsg.Text = "Images Not Transferred";
                    }
                    else lblmsg.Text = "There Is Problem In Image Transfer.";
                }
                else lblmsg.Text = "This Part No. Not Exists In Master Table.";
                Clear();
                GetLastPartNo();
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    private void Clear()
    {
        ddlPartType.SelectedIndex = ddlMachineNm.SelectedIndex = ddlExamYear.SelectedIndex = 0;
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

//private void ConfirmMoveImages()
//{
//    filePaths = Directory.GetFiles(SrcFilePath);
//    foreach (string filePath in filePaths)
//    {
//        string fileName = Path.GetFileName(filePath);
//        if (fileName.ToString().Split('.')[1] == "JPG")
//        {
//            if (PartNo.ToString() == fileName.ToString().Substring(0, 5))
//            {
//                File.Delete(foldepathNew + "\\" + (Convert.ToInt32(txtPartFrom.Text) + i) + "\\" + fileName);
//                File.Copy(filePath, foldepathNew + "\\" + (Convert.ToInt32(txtPartFrom.Text) + i) + "\\" + fileName);
//            }
//        }
//    }
//    if (PartNo == 0)
//        PartNo = Convert.ToInt32(txtPartFrom.Text) + i;
//}
