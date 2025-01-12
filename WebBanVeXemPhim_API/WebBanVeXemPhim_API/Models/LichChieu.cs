using System;
using System.Collections.Generic;

namespace WebBanVeXemPhim_API.Models;

public partial class LichChieu
{
    public int MaLichChieu { get; set; }

    public int MaPhim { get; set; }

    public int MaPhong { get; set; }

    public DateOnly NgayChieu { get; set; }

    public TimeOnly GioChieu { get; set; }

    public decimal GiaVe { get; set; }

    public virtual Phim MaPhimNavigation { get; set; } = null!;

    public virtual Phong MaPhongNavigation { get; set; } = null!;

    public virtual ICollection<Ve> Ves { get; set; } = new List<Ve>();
}
