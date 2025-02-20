using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanVeXemPhim.Models;

public partial class LichChieu
{
    public int MaLichChieu { get; set; }
    [NotMapped]
    public int MaPhim { get; set; }
    [NotMapped]
    public int MaPhong { get; set; }

    public DateOnly NgayChieu { get; set; }

    public TimeOnly GioChieu { get; set; }

    public decimal GiaVe { get; set; }
    public bool TrangThai { get; set; }
    [NotMapped]
    public virtual Phim? MaPhimNavigation { get; set; } = null!;
    [NotMapped]
    public virtual Phong? MaPhongNavigation { get; set; } = null!;
    [NotMapped]
    public virtual ICollection<Ve> Ves { get; set; } = new List<Ve>();
}
