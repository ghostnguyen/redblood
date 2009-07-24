using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FacilityBLL
/// </summary>
public class FacilityBLL
{
	public FacilityBLL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static Facility Get(string FIN)
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return (from s in db.Facilities
                where s.FIN == FIN
                select s).FirstOrDefault();
    }

    public static Facility GetFirst(RedBloodDataContext db)
    {
        return (from s in db.Facilities
                select s).FirstOrDefault();
    }

    public static void ResetCounting()
    {
        RedBloodDataContext db = new RedBloodDataContext();
        Facility f = GetFirst(db);
        
        string YY = DateTime.Now.Year.ToString().Substring(2, 2);

        if (f.CountingYY != YY)
        {
            f.CountingYY = YY;
            f.CountingNumber = 0;
        }
        db.SubmitChanges();
    }
}
