using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class PlUserCreation
{
    public int Ind { get; set; }
    public int UserType { get; set; }
    public string UserName { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string ContactNo { get; set; }
    public string Email { get; set; }
    public string LoginId { get; set; }
    public string Pwd { get; set; }

    public int IsOutSideUser { get; set; }
    public int IsActive { get; set; }
    public DateTime CreationDate { get; set; }
    public int LoginStatus { get; set; }

    public int LockId { get; set; }
    public int UserId { get; set; }
    public int InvalidAttempt { get; set; }
}