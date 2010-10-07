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
using RedBlood.BLL;
namespace RedBlood
{
    /// <summary>
    /// Summary description for Cat
    /// </summary>


    public partial class Geo
    {
        public static Guid BRVT = new Guid("9cfb06ed-137c-493c-9585-8ea6d4fc5b7b");
        public static Guid TayNinh = new Guid("e33105ec-306b-4567-8c40-f81b04e8fa71");
        public static Guid DongNai = new Guid("e22eff09-5ee9-4d25-805a-d9c6f9b1b4ab");
        public static Guid HCMC = new Guid("25e0e30d-c463-4699-945a-39a06e937423");
        public static Guid BinhDuong = new Guid("6e0c4f0d-95ce-42e9-a3e7-99efb59366d5");
        public static Guid BinhPhuoc = new Guid("8d06a7fc-d621-439e-af97-faa8e26a4ad2");

        partial void OnValidate(System.Data.Linq.ChangeAction action)
        {
            if (action == System.Data.Linq.ChangeAction.Insert
                || action == System.Data.Linq.ChangeAction.Update)
            {
                if (string.IsNullOrEmpty(this.Name) ||
                 string.IsNullOrEmpty(this.Name.Trim()))
                    throw new Exception("Nhập danh mục");

                RedBloodDataContext db = new RedBloodDataContext();

                int count = (from cats in db.Geos
                             where cats.ID != this.ID && object.Equals(cats.ParentID, this.ParentID)
                             && cats.Name.Trim() == this.Name.Trim()
                             select cats).Count();

                if (count > 0)
                {
                    throw new Exception("Trùng tên");
                }

                GeoBLL.SetFullname(this);
            }
        }
    }
}