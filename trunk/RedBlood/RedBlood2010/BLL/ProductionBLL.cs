using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
namespace RedBlood.BLL
{
    /// <summary>
    /// Summary description for ProductionBLL
    /// </summary>
    public class ProductionBLL
    {
        [Serializable]
        public class Division
        {
            public string Ext { get; set; }
            public int Volume { get; set; }
        }

        public List<string> ProductCodeInList { get; set; }
        public List<string> ProductCodeOutList { get; set; }
        public List<string> DINInList { get; set; }
        public List<Division> DivisionList { get; set; }

        public ProductionBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string ValidateAllList()
        {
            if (ProductCodeInList.Count == 0)
            {
                return "Không có sản phẩm đầu vào.";
            }

            if (ProductCodeOutList.Count == 0)
            {
                return "Không có sản phẩm đầu ra.";
            }

            if (DINInList.Count == 0)
            {
                return "Không có túi máu đầu vào.";
            }

            ReceiptBLL.ValidateOnTherapyReceipt(ProductCodeInList, ProductCodeOutList);

            if (ProductCodeInList.Where(r => ProductCodeOutList.Contains(r)).Count() != 0)
            {
                return "Danh sách sản phẩm đầu ra và đầu vào có sản phẩm trùng.";
            }

            return "";
        }

        public string ValidateAllList4Divide()
        {
            if (ProductCodeInList.Count == 0)
            {
                return "Không có sản phẩm đầu vào.";
            }

            if (DivisionList.Count == 0)
            {
                return "Không xác định thể tích.";
            }

            if (DINInList.Count == 0)
            {
                return "Không có túi máu đầu vào.";
            }

            return "";
        }

        public List<string> AddProductCodeIn(string productCode)
        {
            if (ProductCodeInList.Contains(productCode))
                throw new Exception("Sản phẩm đầu vào đã có trong danh sách đầu vào.");

            if (ProductCodeOutList.Contains(productCode))
                throw new Exception("Sản phẩm đầu vào đã có trong danh sách đầu ra.");

            if (ProductCodeInList.Count == 1)
                throw new Exception("Sản phẩm đầu vào chỉ được 1 loại.");

            List<string> tempList = ProductCodeInList.ToList();
            tempList.Add(productCode);
            ReceiptBLL.ValidateOnTherapyReceipt(tempList, ProductCodeOutList);

            RedBloodDataContext db = new RedBloodDataContext();
            int count = db.Packs.Where(r => r.ProductCode == productCode && DINInList.Contains(r.DIN)).Count();

            if (count < DINInList.Count)
                throw new Exception("Sản phẩm đầu vào không có túi máu.");

            if (count > DINInList.Count)
                throw new Exception("Sản phẩm đầu vào có túi máu bị trùng dữ liệu.");


            ProductCodeInList.Add(productCode);

            return ProductCodeInList;
        }

        public List<string> AddProductCodeIn4Divide(string productCode)
        {
            if (ProductCodeInList.Contains(productCode))
                throw new Exception("Sản phẩm đầu vào đã có trong danh sách đầu vào.");

            if (ProductCodeInList.Count == 1)
                throw new Exception("Sản phẩm đầu vào chỉ được 1 loại.");

            RedBloodDataContext db = new RedBloodDataContext();
            int count = db.Packs.Where(r => r.ProductCode == productCode && DINInList.Contains(r.DIN)).Count();

            if (count < DINInList.Count)
                throw new Exception("Sản phẩm đầu vào không có túi máu.");

            if (count > DINInList.Count)
                throw new Exception("Sản phẩm đầu vào có túi máu bị trùng dữ liệu.");

            ProductCodeInList.Add(productCode);

            return ProductCodeInList;
        }

        public List<string> AddProductCodeOut(string productCode)
        {
            if (ProductCodeInList.Contains(productCode))
                throw new Exception("Sản phẩm đầu ra đã có trong danh sách đầu vào.");

            if (ProductCodeOutList.Contains(productCode))
                throw new Exception("Sản phẩm đầu ra đã có trong danh sách đầu ra.");

            List<string> tempList = ProductCodeOutList.ToList();
            tempList.Add(productCode);
            ReceiptBLL.ValidateOnTherapyReceipt(ProductCodeInList, tempList);

            RedBloodDataContext db = new RedBloodDataContext();
            int count = db.Packs.Where(r => r.ProductCode == productCode && DINInList.Contains(r.DIN)).Count();
            if (count > 0)
                throw new Exception("Sản phẩm đầu ra đã sản xuất.");

            ProductCodeOutList.Add(productCode);

            return ProductCodeOutList;
        }

        public List<string> AddDIN(string DIN)
        {
            if (DINInList.Contains(DIN))
                throw new Exception("Mã túi máu này đã có.");

            Donation d = DonationBLL.Get(DIN);

            if (d == null)
                throw new Exception("Không có mã túi máu này.");

            if (d.TestResultStatus == Donation.TestResultStatusX.Positive)
            {
                throw new Exception("Xét nghiệm sàng lọc: Dương tính.");
            }

            RedBloodDataContext db = new RedBloodDataContext();
            int count = db.Packs.Where(r => ProductCodeInList.Contains(r.ProductCode) && r.DIN == DIN).Count();
            if (count == 0)
                throw new Exception("Mã túi máu này không có sản phẩm đầu vào.");

            count = db.Packs.Where(r => ProductCodeOutList.Contains(r.ProductCode) && r.DIN == DIN).Count();
            if (count > 0)
                throw new Exception("Mã túi máu này đã có sản phẩm đầu ra.");

            DINInList.Add(DIN);

            return DINInList;
        }

        public List<string> AddDIN4Divide(string DIN)
        {
            if (DINInList.Contains(DIN))
                throw new Exception("Mã túi máu này đã có.");

            Donation d = DonationBLL.Get(DIN);

            if (d == null)
                throw new Exception("Không có mã túi máu này.");

            if (d.TestResultStatus == Donation.TestResultStatusX.Positive)
            {
                throw new Exception("Xét nghiệm sàng lọc: Dương tính.");
            }

            RedBloodDataContext db = new RedBloodDataContext();
            int count = db.Packs.Where(r => ProductCodeInList.Contains(r.ProductCode) && r.DIN == DIN).Count();
            if (count == 0)
                throw new Exception("Mã túi máu này không có sản phẩm đầu vào.");

            DINInList.Add(DIN);

            return DINInList;
        }

        public void Extract()
        {
            string err = ValidateAllList();

            if (!string.IsNullOrEmpty(err))
                throw new Exception(err);

            List<Pack> packList = DINInList.Select(r => PackBLL.Get4Extract(r, ProductCodeInList.FirstOrDefault())).ToList();

            foreach (Pack item in packList)
            {
                foreach (string code in ProductCodeOutList)
                {
                    //TODO: display all err pack.
                    Extract(item.ID, code);
                }
            }
        }

        public void Extract(Guid srcPackID, string productCode)
        {
            Pack pack = PackBLL.Get4Extract(srcPackID);

            PackBLL.Add(pack.DIN, productCode, pack);

            if (pack.Status != Pack.StatusX.Produced)
                PackBLL.ChangeStatus(pack.ID, Pack.StatusX.Produced, PackTransaction.TypeX.Out_Product);
        }

        public void Divide()
        {
            string err = ValidateAllList4Divide();

            if (!string.IsNullOrEmpty(err))
                throw new Exception(err);

            List<Pack> packList = DINInList.Select(r => PackBLL.Get4Extract(r, ProductCodeInList.FirstOrDefault())).ToList();

            foreach (Pack item in packList)
            {
                foreach (var item1 in DivisionList)
                {
                    Divide(item.ID, item1.Ext, item1.Volume);
                }
            }
        }

        public void Divide(Guid srcPackID, string division, int volume)
        {
            Pack pack = PackBLL.Get4Extract(srcPackID);

            string newProductCode = pack.ProductCode.Substring(0, pack.ProductCode.Length - 2) + division;

            PackBLL.Add(pack.DIN, newProductCode, volume, orgPack: pack);

            if (pack.Status != Pack.StatusX.Produced)
                PackBLL.ChangeStatus(pack.ID, Pack.StatusX.Produced, PackTransaction.TypeX.Out_Product);
        }

        public static List<string> GetDivideList(string productCode)
        {
            List<string> divisionList = new List<string>();

            if (productCode.IsValidProductCode())
            {
                string lastTwoChars = productCode.Substring(BarcodeBLL.productLength - 2);
                if (lastTwoChars == "00")
                {
                    divisionList.Add("A0");
                    divisionList.Add("B0");
                    divisionList.Add("C0");
                    divisionList.Add("D0");
                }

                string pattern = "[A-C]{1}0";
                Regex regx = new Regex(pattern);
                if (regx.IsMatch(lastTwoChars))
                {
                    divisionList.Add(lastTwoChars.Substring(0, 1) + "a");
                    divisionList.Add(lastTwoChars.Substring(0, 1) + "b");
                    divisionList.Add(lastTwoChars.Substring(0, 1) + "c");
                    divisionList.Add(lastTwoChars.Substring(0, 1) + "d");
                }
            }

            return divisionList;
        }
    }

}