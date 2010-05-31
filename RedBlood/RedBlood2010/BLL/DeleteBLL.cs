using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace RedBlood.BLL
{
    /// <summary>
    /// Summary description for OrderBLL
    /// </summary>
    public class DeleteBLL
    {
        public DeleteBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static Delete Get(int ID)
        {
            RedBloodDataContext db = new RedBloodDataContext();
            return Get(ID, db);
        }

        public static Delete Get(int ID, RedBloodDataContext db)
        {
            Delete e = db.Deletes.Where(r => r.ID == ID).FirstOrDefault();

            if (e == null)
                throw new Exception("Không tìm thấy đợt hủy.");

            return e;
        }

        public static int Add(List<Guid> packIDList, string note)
        {
            RedBloodDataContext db = new RedBloodDataContext();

            List<Pack> poL = PackBLL.Get4Delete(db, packIDList);

            Delete re = new Delete();
            re.Note = note;

            db.Deletes.InsertOnSubmit(re);
            db.SubmitChanges();

            foreach (var item in packIDList)
            {
                PackBLL.Delete(re.ID, item, note);
            }

            return re.ID;
        }
    }
}