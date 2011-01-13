using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace RedBlood.BLL
{
    /// <summary>
    /// Summary description for ProductBLL
    /// </summary>
    public class ProductBLL
    {
        public ProductBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static Product Get(string code)
        {
            RedBloodDataContext db = new RedBloodDataContext();
            return Get(db, code);
        }

        public static Product Get(RedBloodDataContext db, string code)
        {
            string codeOnly = code.Substring(0, code.Length - 2);
            return db.Products.Where(r => r.Code.Contains(codeOnly)).FirstOrDefault();
        }

        public static string GetDesc(string code)
        {
            var v = Get(code);
            return v == null ? "" : v.Description;
        }

        public static string GetFinalLabelDesc(string code)
        {
            var v = Get(code);
            return v == null ? "" : v.FinalLabelDesc;
        }
    }
}