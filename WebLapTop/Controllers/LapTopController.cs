using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLapTop.Models;


namespace WebLapTop.Controllers
{
    public class LapTopController : Controller
    {
        // GET: LapTop
        MyDataDataContext data = new MyDataDataContext();

        public ActionResult ListLapTop()
        {
            var all_laptop = from lt in data.LapTops select lt;
            return View(all_laptop);
        }
        public ActionResult Detail(int id)
        {
            var D_loptop = data.LapTops.Where(m => m.MaLapTop == id).First();
            return View(D_loptop);
        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, LapTop lt)
        {
            var E_tenlaptop = collection["TenLapTop"];
            var E_hinh = collection["hinh"];
            var E_giaban = Convert.ToDecimal(collection["GiaBan"]);
            var E_ngaycapnhat = Convert.ToDateTime(collection["NgayCapNhap"]);
            var E_soluongton = Convert.ToInt32(collection["SoLuongTon"]);
            if (string.IsNullOrEmpty(E_tenlaptop))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                lt.TenLapTop = E_tenlaptop.ToString();
                lt.Hinh = E_hinh.ToString();
                lt.GiaBan = E_giaban;
                lt.NgayCapNhap = E_ngaycapnhat;
                lt.SoLuongTon = E_soluongton;
                data.LapTops.InsertOnSubmit(lt);
                data.SubmitChanges();
                return RedirectToAction("ListLapTop");
            }
            return this.Create();
        }
        public ActionResult Delete(int id)
        {
            var D_laptop = data.LapTops.First(m => m.MaLapTop == id);
            return View(D_laptop);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_laptop = data.LapTops.Where(m => m.MaLapTop == id).First();
            data.LapTops.DeleteOnSubmit(D_laptop);
            data.SubmitChanges();
            return RedirectToAction("ListLapTop");
        }
        public ActionResult Edit(int id)
        {
            var E_laptop = data.LapTops.First(m => m.MaLapTop == id);
            return View(E_laptop);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_laptop = data.LapTops.First(m => m.MaLapTop == id);
            var E_maloailt = Convert.ToInt32(collection["MaLoaiLT"]);
            var E_tenslaptop = collection["TenLapTop"];
            var E_hinh = collection["Hinh"];
            var E_giaban = Convert.ToDecimal(collection["GiaBan"]);
            var E_ngaycapnhat = Convert.ToDateTime(collection["NgayCapNhap"]);
            var E_soluongton = Convert.ToInt32(collection["SoLuongTon"]);
            var E_machip = Convert.ToInt32(collection["MaChip"]);
            E_laptop.MaLapTop = id;
            if (string.IsNullOrEmpty(E_tenslaptop))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_laptop.MaLoaiLT = E_maloailt;
                E_laptop.TenLapTop = E_tenslaptop;
                E_laptop.Hinh = E_hinh;
                E_laptop.GiaBan = E_giaban;
                E_laptop.NgayCapNhap = E_ngaycapnhat;
                E_laptop.SoLuongTon = E_soluongton;
                E_laptop.MaChip = E_machip;

                UpdateModel(E_laptop);
                data.SubmitChanges();
                return RedirectToAction("ListLapTop");
            }
            return this.Edit(id);
        }
    }
}