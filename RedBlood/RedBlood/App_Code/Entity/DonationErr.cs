using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DonationErr
/// </summary>
public class DonationErr
{
    public string Message { get; set; }
    public Donation.StatusX ToStatusX { get; set; }

    public DonationErr(string mess, Donation.StatusX status)
    {
        Message = mess;
        ToStatusX = status;
    }

    public DonationErr(string mess)
    {
        Message = mess;
        ToStatusX = Donation.StatusX.Non;
    }
}

public class DonationErrEnum
{
    public static DonationErr Non = new DonationErr("");
    public static DonationErr NonExist = new DonationErr("Không tìm thấy túi máu.");
    public static DonationErr TRLocked = new DonationErr("Đã có KQNX.");
    public static DonationErr Unknown = new DonationErr("Có lỗi.");
    
    //public static DonationErr NonExistInCam = new DonationErr("Không tìm thấy túi máu trong đợt thu này.");
    public static DonationErr DataErr = new DonationErr("Lỗi dữ liệu.", Donation.StatusX.DataErr);
    //public static DonationErr EnterPackMulti = new DonationErr("Lỗi dữ liệu. Túi máu đã nhập nhiều hơn 1.", Donation.StatusX.DataErr);
    ////public static DonationErr EnterPackExp = new DonationErr("Có túi máu đã nhập nhưng chưa xử lý.", Pack.StatusX.ExpireEnter);
    //public static DonationErr Expired = new DonationErr("Túi máu quá hạn sử dụng.", Donation.StatusX.Expire);
    //public static DonationErr Deleted = new DonationErr("Túi máu đã hủy.");

    //public static DonationErr CanNotOrder = new DonationErr("Không thể cấp phát.");
    //public static DonationErr Positive = new DonationErr("Không thể cấp phát. Có kết quả dương tính.");
    //public static DonationErr Ordering = new DonationErr("Đang cấp phát.");
    //public static DonationErr Delivered = new DonationErr("Đã cấp phát");
    //public static DonationErr NonExistOrder = new DonationErr("Sai đợt cấp phát.");
    //public static DonationErr OrderClose = new DonationErr("Khóa sổ đợt cấp phát này.");

    ////Extract 2 RBC, Plasma
    //public static DonationErr Extracted = new DonationErr("Túi máu đã sản xuất.");
    //public static DonationErr Valid4Extract = new DonationErr("Valid4Extract");
    //public static DonationErr Invalid4Extract = new DonationErr("Không thể sản xuất.");
    //public static DonationErr SelectNoExtract = new DonationErr("Chưa chọn loại chế phẩm.");


    ////Combine 2 Platelet
    //public static DonationErr Combined2Platelet = new DonationErr("Combined2Platelet");
    //public static DonationErr Init4Platelet = new DonationErr("Init4Platelet");
    //public static DonationErr IsPlatelet = new DonationErr("IsPlatelet");
    //public static DonationErr Valid4Platelet = new DonationErr("Valid4Platelet");
    //public static DonationErr Invalid4Platelet = new DonationErr("Không thể sản xuất tiểu cầu.");


}
