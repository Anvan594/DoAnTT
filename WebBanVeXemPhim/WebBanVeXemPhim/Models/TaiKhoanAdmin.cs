using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebBanVeXemPhim.Models;

public partial class TaiKhoanAdmin
{
    public int MaAdmin { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public string MatKhau { get; set; } = null!;
    [Display(Name = "Họ và Tên")]
    public string HoTen { get; set; } = null!;
    
    public string Email { get; set; } = null!;
    [Display(Name = "Số điện thoại")]
    public string? SoDienThoai { get; set; }

    public DateTime? NgayTao { get; set; }
    [Display(Name = "Trạng thái")]
    public bool? TrangThai { get; set; }
}
