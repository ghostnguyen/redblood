using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for Donation
/// </summary>
public partial class Donation
{

    

    public enum StatusX : int
    {
        Non = -2,
        All = -1,
        Init = 0,

        Đã_thu = 1,
        Assigned = 1,

        //CommitReceived = 2,
        //Delete = 4,
        //Hủy = 4,

        DataErr = 49
    }

    public enum TestResultStatusX : int
    {
        Chưa_có = 0,
        Non = 0,

        Âm_tính = 1,
        Negative = 1,

        Dương_tính = 2,
        Positive = 2,

        Âm_tính_K = 3,
        NegativeLocked = 3,

        Dương_tính_K = 4,
        PositiveLocked = 4
    }

   

    private InfectiousMarker _markers;
    public InfectiousMarker Markers 
    { 
        get
        {
            if (_markers == null)
                _markers = new InfectiousMarker() { donation = this };
            
            return _markers;
        }
    }

    partial void OnLoaded()
    {
        Markers.Decode();
    }
    
    partial void OnInfectiousMarkersChanged()
    {
        Markers.Decode();
    }

    partial void OnValidate(System.Data.Linq.ChangeAction action)
    {
        
        
    }





}
