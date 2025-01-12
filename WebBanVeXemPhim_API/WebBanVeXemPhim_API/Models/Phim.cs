using System;
using System.Collections.Generic;

namespace WebBanVeXemPhim_API.Models;

public partial class Phim
{
    public int MaPhim { get; set; }

    public string TenPhim { get; set; } = null!;

    public string TheLoai { get; set; } = null!;

    public int ThoiLuong { get; set; }

    public string? MoTa { get; set; }

    public DateOnly NgayKhoiChieu { get; set; }

    public string? Poster { get; set; }

    public bool? TrangThai { get; set; }

    public virtual ICollection<LichChieu> LichChieus { get; set; } = new List<LichChieu>();

    public virtual ICollection<Trailer> Trailers { get; set; } = new List<Trailer>();
}
