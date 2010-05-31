using System;
using System.Data;
using System.Data.Linq;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
namespace RedBlood.BLL
{
    /// <summary>
    /// Summary description for DepartmentBLL
    /// </summary>
    public class DepartmentBLL
    {
        public DepartmentBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string Insert(string name, int level, Guid? parentID)
        {
            RedBloodDataContext db = new RedBloodDataContext();

            Department geo = new Department();
            geo.Name = name.Trim();
            geo.Level = level;
            geo.ParentID = parentID;
            geo.HospitalID = HospitalBLL.GetFirst().ID;

            db.Departments.InsertOnSubmit(geo);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return geo.ID.ToString();
        }

        public static void UpdateFullname()
        {
            RedBloodDataContext db = new RedBloodDataContext();

            var r = from e in db.Departments
                    select e;

            foreach (Department e in r)
            {
                SetFullname(e);
            }

            db.SubmitChanges();
        }

        public static void SetFullname(Department e)
        {
            if (e.Level == 1)
            {
                e.Fullname = e.Name;
            }

            if (e.Level == 2)
            {
                e.Fullname = e.ParentDepartment.Name + ", " + e.Name;
            }

            if (e.Level == 3)
            {
                e.Fullname = e.ParentDepartment.ParentDepartment.Name + ", " + e.ParentDepartment.Name + ", " + e.Name;
            }

            e.FullnameNoDiacritics = e.Fullname.RemoveDiacritics();
        }

        static public Department GetByFullnameAndLevel(string fullname, int lvl)
        {
            RedBloodDataContext db = new RedBloodDataContext();

            return (from e in db.Departments
                    where e.Level.Value == lvl && e.Fullname == fullname.Trim()
                    select e).FirstOrDefault();
        }

        static public Department GetByFullname(string fullname)
        {
            RedBloodDataContext db = new RedBloodDataContext();

            return (from e in db.Departments
                    where e.Fullname.ToLower() == fullname.Trim().ToLower()
                    select e).FirstOrDefault();
        }

        static public Department GetByName(string name, int level)
        {
            RedBloodDataContext db = new RedBloodDataContext();

            return (from e in db.Departments
                    where e.Name.ToLower() == name.Trim().ToLower() && e.Level == level
                    select e).FirstOrDefault();
        }
    }
}