using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLapTop.Models;
namespace WebLapTop.Controllers
{
    public class LoaiLapTopController : Controller
    {
        // GET: LoaiLapTop
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            var all_loailoptop = from llt in data.LoaiLapTops select llt;
            return View(all_loailoptop);
        }
        public ActionResult Detail(int id)
        {
            var D_theloai = data.LoaiLapTops.Where(m => m.MaLoaiLT == id).First();
            return View(D_theloai);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, LoaiLapTop llt)
        {
            var ten = collection["TenLoai"];
            if (string.IsNullOrEmpty(ten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                llt.TenLoai = ten;
                data.LoaiLapTops.InsertOnSubmit(llt);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public ActionResult Delete(int id)
        {
            var D_theloai = data.LoaiLapTops.First(m => m.MaLoaiLT == id);
            return View(D_theloai);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_theloai = data.LoaiLapTops.Where(m => m.MaLoaiLT == id).First();
            data.LoaiLapTops.DeleteOnSubmit(D_theloai);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var E_category = data.LoaiLapTops.First(m => m.MaLoaiLT == id);
            return View(E_category);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var theloai = data.LoaiLapTops.First(m => m.MaLoaiLT == id);
            var E_tenloai = collection["TenLoai"];
            theloai.MaLoaiLT = id;
            if (string.IsNullOrEmpty(E_tenloai))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                theloai.TenLoai = E_tenloai;
                UpdateModel(theloai);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
    }
}