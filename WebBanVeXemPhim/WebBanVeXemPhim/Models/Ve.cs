using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebBanVeXemPhim.Models;

public partial class Ve
{
    public int MaVe { get; set; }

    public int MaLichChieu { get; set; }

    public int MaGhe { get; set; }

    public int MaKhachHang { get; set; }
    [DisplayFormat(DataFormatString = "{0:N0} đ")]
    public decimal GiaVe { get; set; }

    public DateTime? NgayDat { get; set; }=DateTime.Now;

    public virtual Ghe MaGheNavigation { get; set; } = null!;

    public virtual NguoiDung MaKhachHangNavigation { get; set; } = null!;

    public virtual LichChieu MaLichChieuNavigation { get; set; } = null!;

    public virtual ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();
}
