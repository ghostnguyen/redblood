using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Order
/// </summary>
public partial class Order
{
    public enum TypeX : int    
    { 
        ToOrg = 1,
        ToPeople = 2
    }

    public enum StatusX : int
    {
        Init = 1,
        Done = 2
    }

    partial void OnNameChanging(string value)
    {
        if (string.IsNullOrEmpty(value.Trim()))
            throw new Exception("Nhập tên.");
    }

    public void SetOrgID(string value)
    {
        value = value.Trim();
        if (String.IsNullOrEmpty(value))
        {
            OrgID = null;
        }
        else
        {
            Org g = OrgBLL.GetByName(value);
            if (g == null)
            {
                throw new Exception("Nhập sai tên đơn vị.");
            }
            else
            {
                OrgID = g.ID;
            }
        }
    }

    public void SetDepartment(string value)
    {
        value = value.Trim();
        if (String.IsNullOrEmpty(value))
        {
            DepartmentID1 = null;
            DepartmentID2 = null;
            DepartmentID3 = null;
        }
        else
        {
            Department g = DepartmentBLL.GetByFullname(value);
            if (g == null)
            {
                throw new Exception("Nhập sai triệu chứng.");
            }
            else
            {
                DepartmentID1 = null;
                DepartmentID2 = null;
                DepartmentID3 = null;

                if (g.Level == 1)
                {
                    DepartmentID1 = g.ID;
                }

                if (g.Level == 2)
                {
                    DepartmentID2 = g.ID;
                    DepartmentID1 = g.ParentDepartment.ID;
                }

                if (g.Level == 3)
                {
                    DepartmentID3 = g.ID;
                    DepartmentID2 = g.ParentDepartment.ID;
                    DepartmentID1 = g.ParentDepartment.ParentDepartment.ID;
                }
            }
        }
    }
    public string FullDepartment
    {
        get
        {
            string r = "";
            if (Department3 != null)
                r += Department3.Fullname;
            else if (Department2 != null)
                r += Department2.Fullname;
            else if (Department1 != null)
                r += Department1.Fullname;

            return r;
        }
    }
}
