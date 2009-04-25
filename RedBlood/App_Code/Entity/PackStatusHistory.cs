using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PackStatusHistory
/// </summary>
public partial class PackStatusHistory
{
    public PackStatusHistory(Pack p, Pack.StatusX from, Pack.StatusX to, string actor, string note)
    {
        PackID = p.ID;
        FromStatus = from;        
        ToStatus = to;
        Actor = actor;
        Note = note;
        Date = DateTime.Now;
    }
}
