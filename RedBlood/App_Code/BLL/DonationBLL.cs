using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DonationBLL
/// </summary>
public class DonationBLL
{
	public DonationBLL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static Donation[] New(RedBloodDataContext db, int count)
    {
        Facility f = FacilityBLL.GetFirst();

        int autonum = f.CountingNumber.Value;

        Donation[] l = new Donation[count];

        for (int i = 0; i < l.Length; i++)
        {           
            l[i] = new Donation();
            l[i].DIN = f.FIN + f.CountingYY + autonum++.ToString("D6");
            l[i].Status = Donation.StatusX.Init;
        }

        f.CountingNumber = autonum;

        db.Donations.InsertAllOnSubmit(l);

        return l;
    }

    public static Pack[] New(int count)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        Pack[] l = New(db, count);
        db.Packs.InsertAllOnSubmit(l);


        db.SubmitChanges();
        return l;
    }
}
