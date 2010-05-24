using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PackStatusHistory
/// </summary>
public partial class DonationStatusLog
{
    public DonationStatusLog(Donation e, Donation.StatusX from, Donation.StatusX to, string actor, string note)
    {
        DIN = e.DIN;
        FromStatus = from;        
        ToStatus = to;
        Actor = actor;
        Note = note;
        Date = DateTime.Now;
    }
}
