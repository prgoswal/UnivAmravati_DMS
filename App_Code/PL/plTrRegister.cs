using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for plTrRegister
/// </summary>
public class plTrRegister
{

    public int Ind { get; set; }
    public int GroupID { get; set; }
    public string GroupDesc { get; set; }
    public int ItemID { get; set; }
    public string ItemDesc { get; set; }
    public int OrderPriority { get; set; }
    public bool IsActive { get; set; }
    public int UserId { get; set; }

    public string UserLoginId { get; set; }
    public string LoginPwd { get; set; }
    public string MacAddress { get; set; }
    public string UserLoginPwd { get; set; }
}