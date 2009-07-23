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

    public static Facility GetFirst()
    {
        RedBloodDataContext db = new RedBloodDataContext();

        return (from s in db.Facilities
                select s).FirstOrDefault();
    }
}
