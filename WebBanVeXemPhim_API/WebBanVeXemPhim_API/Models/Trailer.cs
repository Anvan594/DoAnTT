using System;
using System.Collections.Generic;

namespace WebBanVeXemPhim_API.Models;

public partial class Trailer
{
    public int MaTrailer { get; set; }

    public int? MaPhim { get; set; }

    public string? DuongDanTrailer { get; set; }

    public string? MoTaTrailer { get; set; }

    public TimeOnly? ThoiGianTrailer { get; set; }

    public virtual Phim? MaPhimNavigation { get; set; }
}
