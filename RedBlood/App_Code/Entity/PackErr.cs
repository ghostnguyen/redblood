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

public class PackErrList
{
    public static PackErr Non = new PackErr("");
    public static PackErr NonExist = new PackErr("Không tìm thấy túi máu.");
    public static PackErr NonExistInCam = new PackErr("Không tìm thấy túi máu trong đợt thu này.");
    public static PackErr DataErr = new PackErr("Lỗi dữ liệu.", Pack.StatusX.DataErr);
    public static PackErr EnterPackMulti = new PackErr("Lỗi dữ liệu. Túi máu đã nhập nhiều hơn 1.", Pack.StatusX.DataErr);
    public static PackErr EnterPackExp = new PackErr("Có túi máu đã nhập nhưng chưa xử lý.", Pack.StatusX.ExpireEnter);
    public static PackErr Expired = new PackErr("Túi máu quá hạn sử dụng.", Pack.StatusX.Expire);
    public static PackErr Deleted = new PackErr("Túi máu đã hủy.");

    public static PackErr CanNotOrder = new PackErr("Không thể cấp phát. Túi máu chưa nhập kết quả xét nghiệm.");
    public static PackErr Ordering = new PackErr("Đang cấp phát.");
    public static PackErr Dilivered = new PackErr("Đã cấp phát");
    public static PackErr NonExistOrder = new PackErr("Sai đợt cấp phát.");
}
