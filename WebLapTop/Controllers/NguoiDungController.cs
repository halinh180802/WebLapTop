using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLapTop.Models;

namespace WebLapTop.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        MyDataDataContext data = new MyDataDataContext();
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        public ActionResult DangKy(FormCollection collection, KhachHang kh)
        {
            var hoten = collection["HoTen"];
            var tendangnhap = collection["TenDangNhap"];
            var matkhau = collection["MatKhau"];
            var MatKhauXacNhan = collection["MatKhauXacNhan"];
            var email = collection["Email"];
            var diachi = collection["DiaChi"];
            var dienthoai = collection["DienThoai"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);
            if (String.IsNullOrEmpty(MatKhauXacNhan))
            {
                ViewData["NhapMKXN"] = "Phải nhập mật khẩu xác nhận!";
            }
            else
            {
                if (!matkhau.Equals(MatKhauXacNhan))
                {
                    ViewData["MatKhauGiongNhau"] = "Mật khẩu và mật khẩu xác nhận phải giống nhau";
                }
                else
                {
                    kh.HoTen = hoten;
                    kh.TenDangNhap = tendangnhap;
                    kh.MatKhau = matkhau;
                    kh.Email = email;
                    kh.DiaChi = diachi;
                    kh.DienThoai = dienthoai;
                    kh.NgaySinh = DateTime.Parse(ngaysinh);
                    data.KhachHangs.InsertOnSubmit(kh);
                    data.SubmitChanges();
                    return RedirectToAction("DangNhap");
                }
            }
            return this.DangKy();
        }


        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var tendangnhap = collection["TenDangNhap"];
            var matkhau = collection["MatKhau"];
            KhachHang kh = data.KhachHangs.SingleOrDefault(n => n.TenDangNhap == tendangnhap && n.MatKhau == matkhau);
            if (kh != null)
            {
                ViewBag.ThongBao = "Chúc mừng bạn đã đăng nhập thành công";
                Session["TaiKhoan"] = kh;
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return RedirectToAction("Index", "Home");
        }
    }
}