
using DOANWEBDULICH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity;

namespace DOANWEBDULICH.Controllers
{
    public class QuanTriController : Controller
    {
        dbQLDuLichDataContext data = new dbQLDuLichDataContext();
        // GET: QuanTri
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult LGAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LGAdmin(FormCollection collection)
        {
            // Gán các giá trị người dùng nhập liệu cho các biến 
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                //Gán giá trị cho đối tượng được tạo mới (ad)        

                Admin ad = data.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    // ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "QuanTri");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        // Du Lich ===========================================================================================
        public ActionResult DuLich(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            //return View(db.SACHes.ToList());
            return View(data.TOURDULICHes.ToList().OrderBy(n => n.IDTOUR).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult ThemmoiTour()
        {
            //Dua du lieu vao dropdownList
            //Lay ds tu tabke chu de, sắp xep tang dan trheo ten chu de, chon lay gia tri Ma CD, hien thi thi Tenchude
            ViewBag.IDLOAI = new SelectList(data.LOAITOURs.ToList().OrderBy(n => n.TENLOAI), "IDLOAI", "TENLOAI");
            ViewBag.IDMIEN = new SelectList(data.MIENs.ToList().OrderBy(n => n.TENMIEN), "IDMIEN", "TENMIEN");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemmoiTour(TOURDULICH tour, HttpPostedFileBase fileUpload)
        {
            //Dua du lieu vao dropdownload
            ViewBag.IDLOAI = new SelectList(data.LOAITOURs.ToList().OrderBy(n => n.TENLOAI), "IDLOAI", "TENLOAI");
            ViewBag.IDMIEN = new SelectList(data.MIENs.ToList().OrderBy(n => n.TENMIEN), "IDMIEN", "TENMIEN");

            //Kiem tra duong dan file
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh";
                return View();
            }
            //Them vao CSDL
            else
            {
                if (ModelState.IsValid)
                {
                    //Luu ten fie, luu y bo sung thu vien using System.IO;
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //Luu duong dan cua file
                    var path = Path.Combine(Server.MapPath("~/PIC/PicHome/TourPic"), fileName);
                    //Kiem tra hình anh ton tai chua?
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        //Luu hinh anh vao duong dan
                        fileUpload.SaveAs(path);
                    }
                    tour.ANH = fileName;
                    //Luu vao CSDL
                    data.TOURDULICHes.InsertOnSubmit(tour);
                    data.SubmitChanges();
                }
                return RedirectToAction("DuLich");
            }
        }

        //Hiển thị tour
        public ActionResult Chitiettour(string id)
        {
            //Lay ra doi tuong tou theo ma
            TOURDULICH tour = data.TOURDULICHes.SingleOrDefault(n => n.IDTOUR == id);
            ViewBag.IDTOUR = tour.IDTOUR;
            if (tour == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tour);
        }

        //Xóa sản phẩm
        [HttpGet]
        public ActionResult XoaTour(string id)
        {
            //Lay ra doi tuong sach can xoa theo ma
            TOURDULICH tour = data.TOURDULICHes.SingleOrDefault(n => n.IDTOUR == id);
            ViewBag.IDTOUR = tour.IDTOUR;
            if (tour == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tour);
        }

        [HttpPost, ActionName("XoaTour")]
        public ActionResult Xacnhanxoa(string id)
        {
            //Lay ra doi tuong sach can xoa theo ma
            TOURDULICH tour = data.TOURDULICHes.SingleOrDefault(n => n.IDTOUR == id);
            ViewBag.IDTOUR = tour.IDTOUR;
            if (tour == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.TOURDULICHes.DeleteOnSubmit(tour);
            data.SubmitChanges();
            return RedirectToAction("DuLich");
        }

        //Chinh sửa sản phẩm
        [HttpGet]
        public ActionResult SuaTour(string id)
        {
            //Lay ra doi tuong sach theo ma
            TOURDULICH tour = data.TOURDULICHes.SingleOrDefault(n => n.IDTOUR == id);
            ViewBag.IDTOUR = tour.IDTOUR;
            if (tour == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Dua du lieu vao dropdownList
            //Lay ds tu tabke chu de, sắp xep tang dan trheo ten chu de, chon lay gia tri Ma CD, hien thi thi Tenchude
            ViewBag.IDMIEN = new SelectList(data.MIENs.ToList().OrderBy(n => n.TENMIEN), "IDMIEN", "TENMIEN");
            return View(tour);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaTour(TOURDULICH tour, HttpPostedFileBase fileUpload)
        {
            //Dua du lieu vao dropdownload
            ViewBag.IDLOAI = new SelectList(data.LOAITOURs.ToList().OrderBy(n => n.TENLOAI), "IDLOAI", "TENLOAI");
            ViewBag.IDMIEN = new SelectList(data.MIENs.ToList().OrderBy(n => n.TENMIEN), "IDMIEN", "TENMIEN");
            //Kiem tra duong dan file
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            //Them vao CSDL
            else
            {
                if (ModelState.IsValid)
                {
                    //Luu ten fie, luu y bo sung thu vien using System.IO;
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //Luu duong dan cua file
                    var path = Path.Combine(Server.MapPath("~/PIC/PicHome/TourPic"), fileName);
                    //Kiem tra hình anh ton tai chua?
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        //Luu hinh anh vao duong dan
                        fileUpload.SaveAs(path);
                    }
                    tour.ANH = fileName;
                    //Luu vao CSDL   
                    UpdateModel(tour);
                    data.SubmitChanges();

                }
                return RedirectToAction("DuLich");
            }
        }

        // Khac San ===================================================================================

        public ActionResult KhachSan(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            //return View(db.SACHes.ToList());
            return View(data.KHACHSANs.ToList().OrderBy(n => n.IDKS).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult ThemmoiKS()
        {
            //Dua du lieu vao dropdownList
            //Lay ds tu tabke chu de, sắp xep tang dan trheo ten chu de, chon lay gia tri Ma CD, hien thi thi Tenchude
            ViewBag.IDMIEN = new SelectList(data.MIENs.ToList().OrderBy(n => n.TENMIEN), "IDMIEN", "TENMIEN");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemmoiKS(KHACHSAN ks, HttpPostedFileBase fileUpload)
        {
            //Dua du lieu vao dropdownload
            ViewBag.IDMIEN = new SelectList(data.MIENs.ToList().OrderBy(n => n.TENMIEN), "IDMIEN", "TENMIEN");

            //Kiem tra duong dan file
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh";
                return View();
            }
            //Them vao CSDL
            else
            {
                if (ModelState.IsValid)
                {
                    //Luu ten fie, luu y bo sung thu vien using System.IO;
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //Luu duong dan cua file
                    var path = Path.Combine(Server.MapPath("~/PIC/PicHome/KsPic"), fileName);
                    //Kiem tra hình anh ton tai chua?
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        //Luu hinh anh vao duong dan
                        fileUpload.SaveAs(path);
                    }
                    ks.ANH = fileName;
                    //Luu vao CSDL
                    data.KHACHSANs.InsertOnSubmit(ks);
                    data.SubmitChanges();
                }
                return RedirectToAction("KhachSan");
            }
        }
        //Hiển thị tour
        public ActionResult Chitietks(string id)
        {
            //Lay ra doi tuong tou theo ma
            KHACHSAN ks = data.KHACHSANs.SingleOrDefault(n => n.IDKS == id);
            ViewBag.IDKS = ks.IDKS;
            if (ks == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ks);
        }
        //Xóa sản phẩm
        [HttpGet]
        public ActionResult XoaKS(string id)
        {
            //Lay ra doi tuong sach can xoa theo ma
            KHACHSAN ks = data.KHACHSANs.SingleOrDefault(n => n.IDKS == id);
            ViewBag.IDKS = ks.IDKS;
            if (ks == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ks);
        }

        [HttpPost, ActionName("XoaKS")]
        public ActionResult XacNhanXoaKS(string id)
        {
            //Lay ra doi tuong sach can xoa theo ma
            KHACHSAN ks = data.KHACHSANs.SingleOrDefault(n => n.IDKS == id);
            ViewBag.IDKS = ks.IDKS;
            if (ks == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.KHACHSANs.DeleteOnSubmit(ks);
            data.SubmitChanges();
            return RedirectToAction("KhachSan");
        }

        [HttpGet]
        public ActionResult SuaKS(string id)
        {
            //Lay ra doi tuong sach theo ma
            KHACHSAN ks = data.KHACHSANs.SingleOrDefault(n => n.IDKS == id);
            ViewBag.IDKS = ks.IDKS;
            if (ks == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Dua du lieu vao dropdownList
            //Lay ds tu tabke chu de, sắp xep tang dan trheo ten chu de, chon lay gia tri Ma CD, hien thi thi Tenchude
            ViewBag.IDMIEN = new SelectList(data.MIENs.ToList().OrderBy(n => n.TENMIEN), "IDMIEN", "TENMIEN");
            return View(ks);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaKS(KHACHSAN ks, HttpPostedFileBase fileUpload)
        {
            //Dua du lieu vao dropdownload
            ViewBag.IDMIEN = new SelectList(data.MIENs.ToList().OrderBy(n => n.TENMIEN), "IDMIEN", "TENMIEN");
            //Kiem tra duong dan file
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            //Them vao CSDL
            else
            {
                if (ModelState.IsValid)
                {
                    //Luu ten fie, luu y bo sung thu vien using System.IO;
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //Luu duong dan cua file
                    var path = Path.Combine(Server.MapPath("~/PIC/PicHome/KsPic"), fileName);
                    //Kiem tra hình anh ton tai chua?
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        //Luu hinh anh vao duong dan
                        fileUpload.SaveAs(path);
                    }
                    ks.ANH = fileName;
                    //Luu vao CSDL   
                    UpdateModel(ks);
                    data.SubmitChanges();

                }
                return RedirectToAction("KhachSan");
            }
        }

        // Thue Xe =================================================================================
        public ActionResult ThueXe(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            //return View(db.SACHes.ToList());
            return View(data.PHUONGTIENs.ToList().OrderBy(n => n.IDPT).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult ThemmoiXe()
        {
            //Dua du lieu vao dropdownList
            //Lay ds tu tabke chu de, sắp xep tang dan trheo ten chu de, chon lay gia tri Ma CD, hien thi thi Tenchude
            //ViewBag.IDMIEN = new SelectList(data.MIENs.ToList().OrderBy(n => n.TENMIEN), "IDMIEN", "TENMIEN");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemmoiXe(PHUONGTIEN xe, HttpPostedFileBase fileUpload)
        {
            //Dua du lieu vao dropdownload
            //ViewBag.IDMIEN = new SelectList(data.MIENs.ToList().OrderBy(n => n.TENMIEN), "IDMIEN", "TENMIEN");

            //Kiem tra duong dan file
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh";
                return View();
            }
            //Them vao CSDL
            else
            {
                if (ModelState.IsValid)
                {
                    //Luu ten fie, luu y bo sung thu vien using System.IO;
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //Luu duong dan cua file
                    var path = Path.Combine(Server.MapPath("~/PIC/PicHome/xEPic"), fileName);
                    //Kiem tra hình anh ton tai chua?
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        //Luu hinh anh vao duong dan
                        fileUpload.SaveAs(path);
                    }
                    xe.ANH = fileName;
                    //Luu vao CSDL
                    data.PHUONGTIENs.InsertOnSubmit(xe);
                    data.SubmitChanges();
                }
                return RedirectToAction("ThueXe");
            }
        }
        public ActionResult Chitietxe(string id)
        {
            //Lay ra doi tuong tou theo ma
            PHUONGTIEN xe = data.PHUONGTIENs.SingleOrDefault(n => n.IDPT == id);
            ViewBag.IDPT = xe.IDPT;
            if (xe == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(xe);
        }

        //Xóa sản phẩm
        [HttpGet]
        public ActionResult XoaXe(string id)
        {
            //Lay ra doi tuong tou theo ma
            PHUONGTIEN xe = data.PHUONGTIENs.SingleOrDefault(n => n.IDPT == id);
            ViewBag.IDPT = xe.IDPT;
            if (xe == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(xe);
        }

        [HttpPost, ActionName("XoaXe")]
        public ActionResult XacNhanXoaXe(string id)
        {
            //Lay ra doi tuong sach can xoa theo ma
            PHUONGTIEN xe = data.PHUONGTIENs.SingleOrDefault(n => n.IDPT == id);
            ViewBag.IDPT = xe.IDPT;
            if (xe == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.PHUONGTIENs.DeleteOnSubmit(xe);
            data.SubmitChanges();
            return RedirectToAction("ThueXe");
        }

        [HttpGet]
        public ActionResult SuaXe(string id)
        {
            //Lay ra doi tuong sach theo ma
            PHUONGTIEN xe = data.PHUONGTIENs.SingleOrDefault(n => n.IDPT == id);
            ViewBag.IDPT = xe.IDPT;
            if (xe == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Dua du lieu vao dropdownList
            //Lay ds tu tabke chu de, sắp xep tang dan trheo ten chu de, chon lay gia tri Ma CD, hien thi thi Tenchude

            return View(xe);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaSuaXeKS(PHUONGTIEN xe, HttpPostedFileBase fileUpload)
        {
            //Dua du lieu vao dropdownload

            //Kiem tra duong dan file
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            //Them vao CSDL
            else
            {
                if (ModelState.IsValid)
                {
                    //Luu ten fie, luu y bo sung thu vien using System.IO;
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //Luu duong dan cua file
                    var path = Path.Combine(Server.MapPath("~/PIC/PicHome/XePic"), fileName);
                    //Kiem tra hình anh ton tai chua?
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        //Luu hinh anh vao duong dan
                        fileUpload.SaveAs(path);
                    }
                    xe.ANH = fileName;
                    //Luu vao CSDL   
                    UpdateModel(xe);
                    data.SubmitChanges();

                }
                return RedirectToAction("ThueXe");
            }
        }
        //=============================== VÉ ================================================================================================
        public ActionResult VeTour(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            //return View(db.SACHes.ToList());
            return View(data.VETOURs.ToList().OrderBy(n => n.IDVE).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Chitietve(int id)
        {
            //Lay ra doi tuong tou theo ma
            VETOUR vetour = data.VETOURs.SingleOrDefault(n => n.IDVE == id);
            ViewBag.IDVE = vetour.IDVE;
            if (vetour == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(vetour);
        }

        //Xóa sản phẩm
        [HttpGet]
        public ActionResult XoaVeTour(int id)
        {
            //Lay ra doi tuong tou theo ma
            VETOUR vetour = data.VETOURs.SingleOrDefault(n => n.IDVE == id);
            ViewBag.IDVE = vetour.IDVE;
            if (vetour == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(vetour);
        }

        [HttpPost, ActionName("XoaVeTour")]
        public ActionResult XacNhanXoaVeTour(int id)
        {
            //Lay ra doi tuong sach can xoa theo ma
            VETOUR vetour = data.VETOURs.SingleOrDefault(n => n.IDVE == id);
            ViewBag.IDVE = vetour.IDVE;
            if (vetour == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.VETOURs.DeleteOnSubmit(vetour);
            data.SubmitChanges();
            return RedirectToAction("VeTour");
        }
        //=============================== VÉ XE ================================================================================================
        public ActionResult VeXe(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            //return View(db.SACHes.ToList());
            return View(data.VEXEs.ToList().OrderBy(n => n.IDVEXE).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Chitietvexe(int id)
        {
            //Lay ra doi tuong tou theo ma
            VEXE vexe = data.VEXEs.SingleOrDefault(n => n.IDVEXE == id);
            ViewBag.IDVEXE = vexe.IDVEXE;
            if (vexe == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(vexe);
        }

        //Xóa sản phẩm
        [HttpGet]
        public ActionResult XoaVeXe(int id)
        {
            //Lay ra doi tuong tou theo ma
            VEXE vexe = data.VEXEs.SingleOrDefault(n => n.IDVEXE == id);
            ViewBag.IDVEXE = vexe.IDVEXE;
            if (vexe == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(vexe);
        }

        [HttpPost, ActionName("XoaVeXe")]
        public ActionResult XacNhanXoaVeXe(int id)
        {
            //Lay ra doi tuong sach can xoa theo ma
            VEXE vexe = data.VEXEs.SingleOrDefault(n => n.IDVEXE == id);
            ViewBag.IDVEXE = vexe.IDVEXE;
            if (vexe == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.VEXEs.DeleteOnSubmit(vexe);
            data.SubmitChanges();
            return RedirectToAction("VeXe");
        }
        //=============================== ĐƠN ĐẶT PHÒNG ================================================================================================
        public ActionResult DonDatPhong(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            //return View(db.SACHes.ToList());
            return View(data.VEPHONGs.ToList().OrderBy(n => n.IDDON).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Chitietdon(int id)
        {
            //Lay ra doi tuong tou theo ma
            VEPHONG don = data.VEPHONGs.SingleOrDefault(n => n.IDDON == id);
            ViewBag.IDDON = don.IDDON;
            if (don == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(don);
        }

        //Xóa sản phẩm
        [HttpGet]
        public ActionResult XoaDon(int id)
        {
            //Lay ra doi tuong tou theo ma
            VEPHONG don = data.VEPHONGs.SingleOrDefault(n => n.IDDON == id);
            ViewBag.IDDON = don.IDDON;
            if (don == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(don);
        }

        [HttpPost, ActionName("XoaDon")]
        public ActionResult XacNhanXoaDon(int id)
        {
            //Lay ra doi tuong sach can xoa theo ma
            VEPHONG don = data.VEPHONGs.SingleOrDefault(n => n.IDDON == id);
            ViewBag.IDDON = don.IDDON;
            if (don == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.VEPHONGs.DeleteOnSubmit(don);
            data.SubmitChanges();
            return RedirectToAction("DonDatPhong");
        }
    }

}