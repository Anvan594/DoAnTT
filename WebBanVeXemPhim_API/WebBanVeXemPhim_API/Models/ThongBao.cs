using System;
using System.Collections.Generic;

namespace WebBanVeXemPhim_API.Models;

public partial class ThongBao
{
    public int MaThongBao { get; set; }

    public int MaNguoiDung { get; set; }

    public string NoiDung { get; set; } = null!;

    public DateTime? NgayGui { get; set; }

    /*public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;*/
}
