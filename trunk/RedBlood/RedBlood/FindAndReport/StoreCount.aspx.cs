using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FindAndReport_StoreCount : System.Web.UI.Page
{
    class BGCount
    {
        public List<vw_ProductCount> _availableList;
        public List<vw_ProductCount> availableList
        {
            get
            {
                if (_availableList == null) _availableList = new List<vw_ProductCount>();
                return _availableList;
            }
            set
            {
                _availableList = value;

                O_RhD_Pos = availableList.Where(r => r.BloodGroup == BloodGroup.O_RhD_positive.Code
                    && (r.Volume == Volume || Volume == 0)).Count().ToStringRemoveZero();

                O_RhD_Neg = availableList.Where(r => r.BloodGroup == BloodGroup.O_RhD_negative.Code
                    && (r.Volume == Volume || Volume == 0)).Count().ToStringRemoveZero();

                A_RhD_Pos = availableList.Where(r => r.BloodGroup == BloodGroup.A_RhD_positive.Code
                    && (r.Volume == Volume || Volume == 0)).Count().ToStringRemoveZero();

                A_RhD_Neg = availableList.Where(r => r.BloodGroup == BloodGroup.A_RhD_negative.Code
                    && (r.Volume == Volume || Volume == 0)).Count().ToStringRemoveZero();

                B_RhD_Pos = availableList.Where(r => r.BloodGroup == BloodGroup.B_RhD_positive.Code
                    && (r.Volume == Volume || Volume == 0)).Count().ToStringRemoveZero();

                B_RhD_Neg = availableList.Where(r => r.BloodGroup == BloodGroup.B_RhD_negative.Code
                    && (r.Volume == Volume || Volume == 0)).Count().ToStringRemoveZero();

                AB_RhD_Pos = availableList.Where(r => r.BloodGroup == BloodGroup.AB_RhD_positive.Code
                    && (r.Volume == Volume || Volume == 0)).Count().ToStringRemoveZero();

                AB_RhD_Neg = availableList.Where(r => r.BloodGroup == BloodGroup.AB_RhD_negative.Code
                    && (r.Volume == Volume || Volume == 0)).Count().ToStringRemoveZero();

                //O_RhD_Pos = availableList.Where(r => r.BloodGroup == BloodGroup.O_RhD_positive.Code
                //    && (r.Volume == Volume || Volume == 0)).Count().ToStringRemoveZero();
            }
        }

        public int Volume { get; set; }
        public string VolumeText
        {
            get
            {
                if (Volume == 0) return "";
                else return Volume.ToString();
            }
        }
        public string O_RhD_Pos { get; set; }
        public string O_RhD_Neg { get; set; }
        public string A_RhD_Pos { get; set; }
        public string A_RhD_Neg { get; set; }
        public string B_RhD_Pos { get; set; }
        public string B_RhD_Neg { get; set; }
        public string AB_RhD_Pos { get; set; }
        public string AB_RhD_Neg { get; set; }
        public string Others { get; set; }
    }

    class ProductCount
    {
        private List<vw_ProductCount> _availableList;
        public List<vw_ProductCount> availableList
        {
            get
            {
                if (_availableList == null) _availableList = new List<vw_ProductCount>();
                return _availableList;
            }
            set
            {
                _availableList = value;

                TRNon = availableList.Where(r => r.TestResultStatus == Donation.TestResultStatusX.Non).Count().ToStringRemoveZero();
                TRPos = availableList.Where(r => r.TestResultStatus == Donation.TestResultStatusX.Positive
                        || r.TestResultStatus == Donation.TestResultStatusX.PositiveLocked).Count().ToStringRemoveZero();

                negList = availableList.Where(r => r.TestResultStatus == Donation.TestResultStatusX.Negative
                        || r.TestResultStatus == Donation.TestResultStatusX.NegativeLocked).ToList();
            }
        }

        private List<vw_ProductCount> _negList;
        public List<vw_ProductCount> negList
        {
            get
            {
                if (_negList == null) return _negList = new List<vw_ProductCount>();
                else return _negList;
            }
            set
            {
                _negList = value;

                TRNeg = negList.Count().ToStringRemoveZero();

                bgCountList = new List<BGCount>();

                if (volumeList == null || volumeList.Count == 0)
                {
                    volumeList = new List<int>();
                    volumeList.Add(0);
                }

                foreach (int item in volumeList)
                {
                    BGCount bgCount = new BGCount();
                    bgCount.Volume = item;
                    bgCount.availableList = negList;

                    bgCountList.Add(bgCount);
                }
            }
        }

        private List<vw_ProductCount> _expireList;
        public List<vw_ProductCount> expireList
        {
            get
            {
                if (_expireList == null) _expireList = new List<vw_ProductCount>();
                return _expireList;
            }
            set
            {
                _expireList = value;

                Expire = expireList.Count.ToStringRemoveZero();
            }
        }

        public string Name { get; set; }
        public string TRNon { get; set; }
        public string TRPos { get; set; }
        public string TRNeg { get; set; }
        public string Expire { get; set; }
        public List<int> volumeList { get; set; }
        public List<BGCount> bgCountList { get; set; }
    }

    List<vw_ProductCount> list = new List<vw_ProductCount>();
    List<ProductCount> countList = new List<ProductCount>();

    protected void Page_Load(object sender, EventArgs e)
    {
        Calc();
        GridView1.DataSource = countList;
        GridView1.DataBind();
    }

    void Calc()
    {
        RedBloodDataContext db = new RedBloodDataContext();

        List<Pack.StatusX> statusList = new List<Pack.StatusX>() { Pack.StatusX.Expired, Pack.StatusX.Product };
        list = db.vw_ProductCounts.Where(r => statusList.Contains(r.Status)).ToList();

        List<Product> bloodProductList = new List<Product>();

        //whole blood
        bloodProductList = new List<Product>();
        bloodProductList.Add(new Product() { Code = "E0009V00" });
        bloodProductList.Add(new Product() { Code = "E0023V00" });
        bloodProductList.Add(new Product() { Code = "E0037V00" });
        bloodProductList.Add(new Product() { Code = "E0052V00" });
        countList.Add(Calc4Product(bloodProductList, "Toàn phần", new List<int>() { 250, 350, 450 }));

        //RBC
        bloodProductList = new List<Product>();
        bloodProductList.Add(new Product() { Code = "E0150V00" });
        bloodProductList.Add(new Product() { Code = "E0195V00" });
        bloodProductList.Add(new Product() { Code = "E0244V00" });
        bloodProductList.Add(new Product() { Code = "E5017V00" });
        bloodProductList.Add(new Product() { Code = "E6051V00" });
        countList.Add(Calc4Product(bloodProductList, "HCL", new List<int>() { }));

        //FFP
        bloodProductList = new List<Product>();
        bloodProductList.Add(new Product() { Code = "E0701V00" });
        countList.Add(Calc4Product(bloodProductList, "HT Tươi", new List<int>() { }));

        //Plasma
        bloodProductList = new List<Product>();
        bloodProductList.Add(new Product() { Code = "E2528V00" });
        countList.Add(Calc4Product(bloodProductList, "HT dự trữ", new List<int>() { }));

        //Platelet
        bloodProductList = new List<Product>();
        bloodProductList.Add(new Product() { Code = "E2807V00" });
        countList.Add(Calc4Product(bloodProductList, "Tiểu cầu", new List<int>() { }));

        //Apheresis Platelet
        bloodProductList = new List<Product>();
        bloodProductList.Add(new Product() { Code = "E2940V00" });
        countList.Add(Calc4Product(bloodProductList, "Tiểu cầu Apheresis", new List<int>() { }));

        //Leukocytes
        bloodProductList = new List<Product>();
        bloodProductList.Add(new Product() { Code = "E3702V00" });
        countList.Add(Calc4Product(bloodProductList, "Bạch cầu", new List<int>() { }));

        //Cryoprecipitate
        bloodProductList = new List<Product>();
        bloodProductList.Add(new Product() { Code = "E5165V00" });
        countList.Add(Calc4Product(bloodProductList, "Kit tủa lạnh", new List<int>() { }));
    }

    ProductCount Calc4Product(List<Product> productList, string name, List<int> volList)
    {
        ProductCount productCount = new ProductCount();

        productCount.Name = name;
        productCount.volumeList = volList;

        productCount.availableList = list.Where(r => productList.Select(r1 => r1.Code).Contains(r.Code) && r.Status == Pack.StatusX.Product).ToList();
        productCount.expireList = list.Where(r => productList.Select(r1 => r1.Code).Contains(r.Code) && r.Status == Pack.StatusX.Expired).ToList();

        return productCount;
    }
}


