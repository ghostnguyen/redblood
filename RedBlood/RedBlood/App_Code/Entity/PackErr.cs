using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PackErr
/// </summary>
public class PackErr
{
    public string Message { get; set; }
    public Pack.StatusX ToStatusX { get; set; }

    public PackErr(string mess, Pack.StatusX status)
    {
        Message = mess;
        ToStatusX = status;
    }

    public PackErr(string mess)
    {
        Message = mess;
        ToStatusX = Pack.StatusX.Non;
    }
}

public class PackErrEnum
{
    public static PackErr Non = new PackErr("");
    public static PackErr NonExist = new PackErr("Không tìm thấy túi máu.");
    //public static PackErr NonExistInCam = new PackErr("Không tìm thấy túi máu trong đợt thu này.");
    public static PackErr DataErr = new PackErr("Lỗi dữ liệu.", Pack.StatusX.DataErr);
    //public static PackErr EnterPackMulti = new PackErr("Lỗi dữ liệu. Túi máu đã nhập nhiều hơn 1.", Pack.StatusX.DataErr);
    //public static PackErr EnterPackExp = new PackErr("Có túi máu đã nhập nhưng chưa xử lý.", Pack.StatusX.ExpireEnter);
    public static PackErr Expired = new PackErr("Túi máu quá hạn sử dụng.", Pack.StatusX.Expired);
    public static PackErr Deleted = new PackErr("Túi máu đã hủy.");

    public static PackErr CanNotOrder = new PackErr("Không thể cấp phát.");
    public static PackErr Positive = new PackErr("Không thể cấp phát. Có kết quả dương tính.");
    public static PackErr Ordering = new PackErr("Đang cấp phát.");
    public static PackErr Delivered = new PackErr("Đã cấp phát");
    public static PackErr NonExistOrder = new PackErr("Sai đợt cấp phát.");
    public static PackErr OrderClose = new PackErr("Khóa sổ đợt cấp phát này.");


    public static PackErr DonationGotPack = new PackErr("Đã thu máu");

    //Extract 2 RBC, Plasma
    public static PackErr Extracted = new PackErr("Túi máu đã sản xuất.");
    public static PackErr Valid4Extract = new PackErr("Valid4Extract");
    public static PackErr Invalid4Extract = new PackErr("Không thể sản xuất.");
    public static PackErr SelectNoExtract = new PackErr("Chưa chọn loại chế phẩm.");


    //Combine 2 Platelet
    public static PackErr Combined2Platelet = new PackErr("Combined2Platelet");
    public static PackErr Init4Platelet = new PackErr("Init4Platelet");
    public static PackErr IsPlatelet = new PackErr("IsPlatelet");
    public static PackErr Valid4Platelet = new PackErr("Valid4Platelet");
    public static PackErr Invalid4Platelet = new PackErr("Không thể sản xuất tiểu cầu.");


}
