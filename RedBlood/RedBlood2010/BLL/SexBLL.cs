using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RedBlood;
/// <summary>
/// Summary description for SexBLL
/// </summary>
public class SexBLL
{
	public SexBLL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public Guid Insert(string name)
    {                     
        RedBloodDataContext db = new RedBloodDataContext();

        Sex e = new Sex();
        e.Name = name;
        
        db.Sexes.InsertOnSubmit(e);
        db.SubmitChanges();
        return e.ID;
    }
}
