using DOANWEBDULICH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOANWEBDULICH.Controllers
{
    public class HomeController : Controller
    {

        dbQLDuLichDataContext data = new dbQLDuLichDataContext();
        public ActionResult TrangChu()
        {
            return View();
        }

       
        [HttpGet]   
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(FormCollection collection,KHACHHANG kh)
        {
            var hoten = collection["TenKH"];
            var taikhoan = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            var nhaplaimatkhau = collection["NhapLaiMatKhau"];
            var cmnd = collection["CMND"];
            var sdt = collection["SDT"];
            var email = collection["EMAIL"];
            if (string.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Quên Tên Nhập Tên";
            }
            else if (string.IsNullOrEmpty(taikhoan))
            {
                ViewData["Loi2"] = "Vui Lòng Nhập Tên Đăng Nhập ";
            }
            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Mật Khẩu Bạn Chưa Nhập Đấy :v";
            }
            else if (string.IsNullOrEmpty(nhaplaimatkhau))
            {
                ViewData["Loi4"] = "Chưa Nhập Lại Mật Khẩu";
            }
            else if (string.IsNullOrEmpty(cmnd))
            {
                ViewData["Loi5"] = "Chứng Minh Chưa Nhập Kìa !!! ";
            }
            else if (string.IsNullOrEmpty(sdt))
            {
                ViewData["Loi6"] = "Xin Số Điện Thoại ";
            }
            else if (string.IsNullOrEmpty(email))
            {
                ViewData["Loi7"] = "Xin Số Email luôn";
            }
            else
            {
                kh.TENKH = hoten;
                kh.Taikhoan = taikhoan;
                kh.Matkhau = matkhau;
                kh.CMND = cmnd;
                kh.SDT = sdt;
                kh.EMAIL = email;
                data.KHACHHANGs.InsertOnSubmit(kh);
                data.SubmitChanges();
                return RedirectToAction("DangNhap","Home");
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
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            if(String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Vui Lòng Nhập Tên Đăng Nhập";
            }
            else if(String.IsNullOrEmpty(matkhau))
             {
                ViewData["Loi2"] = "Vui Lòng Nhập Mật Khẩu";

            }
            else
            {
                KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.Taikhoan == tendn && n.Matkhau == matkhau);
                    if (kh != null)
                    {
                        Session["Taikhoan"] = kh;
                        return RedirectToAction("TrangChu", "Home");
                    }
                    else
                    {
                        ViewBag.Thongbao = "Nhập sai. Vui Lòng Nhập Lại!!";
                    }
            }
            return View();
        }
    }
}