using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InfectiousMarker
/// </summary>
public class InfectiousMarker
{
    public class TR
    {
        public string Name { get; set; }
        public TR(string name)
        {
            Name = name;
        }
    }

    public static TR na = new TR("na");
    public static TR neg = new TR("neg");
    public static TR pos = new TR("pos");

    public static List<TR> TRList = new List<TR>() { 
             na
           ,neg
           ,pos
    };

    public InfectiousMarker()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}
