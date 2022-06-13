using KiemTra_TrinhVanLong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KiemTra_TrinhVanLong.Controllers
{
    public class Test01Controller : Controller
    {
        // GET: Test01
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            var all_sach = from ss in data.SinhViens select ss;
            return View(all_sach);
        }
        public ActionResult Detail(string id)
        {
            var sinhVien = data.SinhViens.Where(m => m.MaSV == id).First();
            return View(sinhVien);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, SinhVien s)
        {
            var E_MaSV = collection["MaSV"];
            var E_HoTen = collection["tensinhvien"];
            var E_gioitinh = collection["gioitinh"];
            var E_ngaysinh = Convert.ToDateTime(collection["ngaysinh"]);
            var E_hinh = collection["hinh"];
            var E_manganh = collection["manganh"];
            if (string.IsNullOrEmpty(E_MaSV))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.MaSV = E_MaSV;
                s.HoTen = E_HoTen;
                s.GioiTinh = E_gioitinh.ToString();
                s.NgaySinh = E_ngaysinh;
                s.Hinh = E_hinh.ToString();
                s.MaNganh = E_manganh.ToString();
                data.SinhViens.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public ActionResult Edit(string id)
        {
            var sinhVien = data.SinhViens.First(m => m.MaSV == id);
            return View(sinhVien);
        }
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var E_MaSV = data.SinhViens.First(m => m.MaSV == id);
            var E_HoTen = collection["tensinhvien"];
            var E_gioitinh = collection["gioitinh"];
            var E_ngaysinh = Convert.ToDateTime(collection["ngaysinh"]);
            var E_hinh = collection["hinh"];
            var E_manganh = collection["manganh"];
            E_MaSV.MaSV = id;
            if (string.IsNullOrEmpty(E_HoTen))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_MaSV.HoTen = E_HoTen; 
                E_MaSV.GioiTinh = E_gioitinh;
                E_MaSV.NgaySinh = E_ngaysinh;
                E_MaSV.Hinh = E_hinh;
                E_MaSV.MaNganh = E_manganh;
                UpdateModel(E_MaSV);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }
        public ActionResult Delete(string id)
        {
            var sinhVien = data.SinhViens.First(m => m.MaSV == id);
            return View(sinhVien);
        }
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var sinhVien = data.SinhViens.Where(m => m.MaSV == id).First();
            data.SinhViens.DeleteOnSubmit(sinhVien);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}
