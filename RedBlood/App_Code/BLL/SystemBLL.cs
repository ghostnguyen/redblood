using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SystemBLL
/// </summary>
public class SystemBLL
{
    public SystemBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //isSOD: isStartOfDate
    public static void ScanExp(bool isSOD)
    {
        if (isSOD && !LogBLL.IsScanEpxDone())
        {
            RedBloodDataContext db;

            Pack.StatusX[] statusList = new Pack.StatusX[] { 
                Pack.StatusX.Init, Pack.StatusX.Assign, Pack.StatusX.CommitReceived };

            Pack[] rs = PackBLL.GetByStatus(statusList, out db);

            foreach (Pack r in rs)
            {
                PackErr err = PackBLL.Validate(r);
                if (!err.Equals(PackErrList.Non))
                {
                    PackStatusHistory h = PackBLL.ChangeStatus(r, err.ToStatusX, SystemActor.SOD, err.Message);
                    db.PackStatusHistories.InsertOnSubmit(h);
                }
            }

            //db.SubmitChanges();
        }
    }
}
