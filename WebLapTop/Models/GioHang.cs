using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebLapTop.Models
{
    public class GioHang
    {
        MyDataDataContext data = new MyDataDataContext();
        public int malaptop { get; set; }

        [Display(Name = "Tên Sách ")]
        public string tenlaptop { get; set; }

        [Display(Name = "Ảnh Bìa")]
        public string hinh { get; set; }

        [Display(Name = "Giá Bán ")]
        public Double giaban { get; set; }

        [Display(Name = "Số Lượng ")]
        public int iSoLuong { get; set; }

        [Display(Name = "Thành Tiền ")]
        public double dThanhTien
        {
            get { return iSoLuong * giaban; }
        }

        public GioHang(int id)
        {
            malaptop = id;
            LapTop laptop = data.LapTops.Single(n => n.MaLapTop == malaptop);
            tenlaptop = laptop.TenLapTop;
            hinh = laptop.Hinh;
            giaban = double.Parse(laptop.GiaBan.ToString());
            iSoLuong = 1;
        }
    }
}