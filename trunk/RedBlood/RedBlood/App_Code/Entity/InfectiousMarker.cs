using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InfectiousMarker
/// </summary>
public class InfectiousMarker
{
    public Donation donation { get; set; }
    
    public string Code
    {
        get
        {
            return donation.InfectiousMarkers;
        }
        private set
        {
            donation.InfectiousMarkers = value;
        }
    }

    public void Decode()
    {
        _HIV = Infection.HIV_Ab.Decode(this);
        _HCV_Ab = Infection.HCV_Ab.Decode(this);
        _HBs_Ag = Infection.HBs_Ag.Decode(this);
        _Syphilis = Infection.Syphilis.Decode(this);
        _Malaria = Infection.Malaria.Decode(this);
    }

    public InfectiousMarker()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public override string ToString()
    {
        return Code;
        //return base.ToString();
    }

    private string _HIV;
    public string HIV
    {
        get
        {
            return _HIV;
        }
        set
        {
            Code = Infection.HIV_Ab.Encode(this, value);
            Code = Infection.HIV_Ag.Encode(this, value);
        }
    }

    private string _HCV_Ab;
    public string HCV_Ab
    {
        get
        {
            return _HCV_Ab;
        }
        set
        {
            Code = Infection.HCV_Ab.Encode(this, value);
        }
    }

    private string _HBs_Ag;
    public string HBs_Ag
    {
        get
        {
            return _HBs_Ag;
        }
        set
        {
            Code = Infection.HBs_Ag.Encode(this, value);
        }
    }

    private string _Syphilis;
    public string Syphilis
    {
        get
        {
            return _Syphilis;
        }
        set
        {
            Code = Infection.Syphilis.Encode(this, value);
        }
    }

    private string _Malaria;
    public string Malaria
    {
        get
        {
            return _Malaria;
        }
        set
        {
            Code = Infection.Malaria.Encode(this, value);
        }
    }
}

public class TR
{
    public string Name { get; set; }

    public static TR na = new TR() { Name = "na" };
    public static TR neg = new TR() { Name = "neg" };
    public static TR pos = new TR() { Name = "pos" };

    public static List<TR> TRList = new List<TR>() { na, neg, pos };

    public static TR GetDefault(string name)
    {
        TR tr = TRList.Where(r => r.Name == name.Trim()).FirstOrDefault();

        return tr == null ? na : tr;
    }
}

public class Value2TR
{
    public int Value { get; set; }
    public TR Result { get; set; }

    public static List<Value2TR> Value2TRTemplate1 = new List<Value2TR>() { 
              new Value2TR() { Value = 0, Result = TR.na }
            , new Value2TR() { Value = 1, Result = TR.na }
            , new Value2TR() { Value = 2, Result = TR.na }
            , new Value2TR() { Value = 3, Result = TR.neg }
            , new Value2TR() { Value = 4, Result = TR.neg }
            , new Value2TR() { Value = 5, Result = TR.neg }
            , new Value2TR() { Value = 6, Result = TR.pos }
            , new Value2TR() { Value = 7, Result = TR.pos }
            , new Value2TR() { Value = 8, Result = TR.pos } };


    public static List<Value2TR> Value2TRTemplate2 = new List<Value2TR>() { 
              new Value2TR() { Value = 0, Result = TR.na }
            , new Value2TR() { Value = 1, Result = TR.neg }
            , new Value2TR() { Value = 2, Result = TR.pos }
            , new Value2TR() { Value = 3, Result = TR.na }
            , new Value2TR() { Value = 4, Result = TR.neg }
            , new Value2TR() { Value = 5, Result = TR.pos }
            , new Value2TR() { Value = 6, Result = TR.na }
            , new Value2TR() { Value = 7, Result = TR.neg }
            , new Value2TR() { Value = 8, Result = TR.pos } };
}

public class Infection
{
    public string Name { get; set; }
    public int Index { get; set; }
    public List<Value2TR> value2TR { get; set; }

    public Infection Coop
    {
        get
        {
            return Infection.InfectionList.Where(r => r.Index == Index && r.Name != Name).FirstOrDefault();
        }
    }

    public string Decode(InfectiousMarker marker)
    {
        if (marker == null
            || string.IsNullOrEmpty(marker.Code))
            return null;

        int value = marker.Code.Substring(this.Index, 1).ToInt();

        return this.value2TR.Where(r => r.Value == value).Select(r => r.Result.Name).FirstOrDefault();
    }

    public string Encode(InfectiousMarker marker, string resultName)
    {
        if (marker == null || string.IsNullOrEmpty(marker.Code))
            return null;

        TR tr = TR.TRList.Where(r => r.Name == resultName).FirstOrDefault();

        if (tr == null) return marker.Code;
        else return Encode(marker, tr);
    }

    public string Encode(InfectiousMarker marker, TR result)
    {
        if (marker == null || string.IsNullOrEmpty(marker.Code))
            return null;

        if (Coop == null) return marker.Code;

        string coopTRName = Coop.Decode(marker);

        int value = value2TR.Where(r => r.Result.Name == result.Name)
            .Join(Coop.value2TR.Where(r => r.Result.Name == coopTRName),
                    r1 => r1.Value,
                    r2 => r2.Value,
                    (r1, r2) => r1.Value).FirstOrDefault();

        return marker.Code.Substring(0, Index) + value.ToString() + marker.Code.Substring(Index + 1);
    }

    public static Infection HIV_Ab = new Infection()
    {
        Name = "HIV_Ab",
        Index = 1 - 1,
        value2TR = Value2TR.Value2TRTemplate1
    };

    public static Infection HIV_Ag = new Infection()
    {
        Name = "HIV_Ag",
        Index = 1 - 1,
        value2TR = Value2TR.Value2TRTemplate2
    };

    public static Infection HIV_Gen = new Infection()
    {
        Name = "HIV_Gen",
        Index = 2 - 1,
        value2TR = Value2TR.Value2TRTemplate1
    };

    public static Infection HCV_Ab = new Infection()
    {
        Name = "HCV_Ab",
        Index = 2 - 1,
        value2TR = Value2TR.Value2TRTemplate2
    };

    public static Infection HCV_Ag = new Infection()
    {
        Name = "HCV_Ag",
        Index = 3 - 1,
        value2TR = Value2TR.Value2TRTemplate1
    };

    public static Infection HCV_Gen = new Infection()
    {
        Name = "HCV_Gen",
        Index = 3 - 1,
        value2TR = Value2TR.Value2TRTemplate2
    };

    public static Infection HBc_Ab = new Infection()
    {
        Name = "HBc_Ab",
        Index = 4 - 1,
        value2TR = Value2TR.Value2TRTemplate1
    };

    public static Infection HBs_Ag = new Infection()
    {
        Name = "HBs_Ag",
        Index = 4 - 1,
        value2TR = Value2TR.Value2TRTemplate2
    };

    public static Infection HBV_Gen = new Infection()
    {
        Name = "HBV_Gen",
        Index = 5 - 1,
        value2TR = Value2TR.Value2TRTemplate1
    };

    public static Infection HTLV_Ab = new Infection()
    {
        Name = "HIV_Ab",
        Index = 5 - 1,
        value2TR = Value2TR.Value2TRTemplate2
    };

    public static Infection Syphilis = new Infection()
    {
        Name = "Syphilis",
        Index = 6 - 1,
        value2TR = Value2TR.Value2TRTemplate1
    };

    public static Infection CMV_Ab = new Infection()
    {
        Name = "CMV_Ab",
        Index = 6 - 1,
        value2TR = Value2TR.Value2TRTemplate2
    };

    public static Infection CMV_Gen = new Infection()
    {
        Name = "CMV_Gen",
        Index = 7 - 1,
        value2TR = Value2TR.Value2TRTemplate1
    };

    public static Infection EBV_Gen = new Infection()
    {
        Name = "EBV_Gen",
        Index = 7 - 1,
        value2TR = Value2TR.Value2TRTemplate2
    };

    public static Infection WNV = new Infection()
    {
        Name = "WNV",
        Index = 8 - 1,
        value2TR = Value2TR.Value2TRTemplate1
    };

    public static Infection ParvoB19_Ab = new Infection()
    {
        Name = "ParvoB19_Ab",
        Index = 8 - 1,
        value2TR = Value2TR.Value2TRTemplate2
    };

    public static Infection ParvoB19_Gen = new Infection()
    {
        Name = "ParvoB19_Gen",
        Index = 9 - 1,
        value2TR = Value2TR.Value2TRTemplate1
    };

    public static Infection Chagas = new Infection()
    {
        Name = "Chagas",
        Index = 9 - 1,
        value2TR = Value2TR.Value2TRTemplate2
    };

    public static Infection Malaria = new Infection()
    {
        Name = "Malaria",
        Index = 18 - 1,
        value2TR = Value2TR.Value2TRTemplate1
    };

    public static Infection TBD = new Infection()
    {
        Name = "TBD",
        Index = 18 - 1,
        value2TR = Value2TR.Value2TRTemplate2
    };

    public static List<Infection> InfectionList = new List<Infection>() 
    {
        HIV_Ab, HIV_Ag, 
        HIV_Gen, HCV_Ab,
        HCV_Ag, HCV_Gen,
        HBc_Ab, HBs_Ag,
        HBV_Gen, HTLV_Ab,
        Syphilis, CMV_Ab,
        CMV_Gen,EBV_Gen,
        WNV,ParvoB19_Ab,
        ParvoB19_Gen,Chagas,
        Malaria,TBD
    };

}




