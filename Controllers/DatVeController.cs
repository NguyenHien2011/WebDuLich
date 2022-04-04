using DOANWEBDULICH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DOANWEBDULICH.Controllers
{
    public class DatVeController : Controller
    {
        dbQLDuLichDataContext data = new dbQLDuLichDataContext();
        // GET: DatVe

        [HttpGet]
        public List<iTour> Laydulieu() // GIo hang contrler
        {
            List<iTour> lstDulieu = Session["iTour"] as List<iTour>;
            if (lstDulieu == null)
            {
                lstDulieu = new List<iTour>();
                Session["iTour"] = lstDulieu;
            }
            return lstDulieu;
        }

        public ActionResult ThemiTour(string iIDTOUR, string strURL)
        {
            List<iTour> lstDulieu = Laydulieu();
            iTour tour = lstDulieu.Find(n => n.iIDTOUR == iIDTOUR);
            if (tour == null)
            {
                tour = new iTour(iIDTOUR);
                lstDulieu.Add(tour);
                return Redirect(strURL);
            }

            else
            {
                return Redirect(strURL);
            }
        }

        [HttpGet]
        public ActionResult TourVe()
        {
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Home");
            }
            //if (Session["iTour"] == null)
            //{
            //    return RedirectToAction("TrangChu", "Home");
            //}

            List<iTour> lstDulieu = Laydulieu();

            return View(lstDulieu);
        }
        public ActionResult TourVe(FormCollection collection)
        {
            List<iTour> tour = Laydulieu();
            foreach ( var i in tour)
            {
                KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];


                VETOUR vv = new VETOUR();
                vv.IDKH = kh.IDKH;
                vv.IDTOUR = i.iIDTOUR;
                data.VETOURs.InsertOnSubmit(vv);
            }    
            
            data.SubmitChanges();
            Session["iTour"] = null;
            return RedirectToAction("XacNhan", "DatVe");
        }

        //==================================================================================
        [HttpGet]
        public List<iBus> Layxe() 
        {
            List<iBus> lstDulieuxe = Session["iBus"] as List<iBus>;
            if (lstDulieuxe == null)
            {
                lstDulieuxe = new List<iBus>();
                Session["iBus"] = lstDulieuxe;
            }
            return lstDulieuxe;
        }

        public ActionResult ThemiBus(string iIDPT, string strURL)
        {
            List<iBus> lstDulieuxe = Layxe();
            iBus xe = lstDulieuxe.Find(n => n.iIDPT == iIDPT);
            if (xe == null)
            {
                xe = new iBus(iIDPT);
                lstDulieuxe.Add(xe);
                return Redirect(strURL);
            }

            else
            {
                return Redirect(strURL);
            }
        }
        [HttpGet]
        public ActionResult MuaVeXe()
        {
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Home");
            }
            //if (Session["iTour"] == null)
            //{
            //    return RedirectToAction("TrangChu", "Home");
            //}

            List<iBus> lstDulieuxe = Layxe();

            return View(lstDulieuxe);
        }

        public ActionResult MuaVeXe(FormCollection collection)
        {
            List<iBus> bus = Layxe();
            foreach (var i in bus)
            {
                KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];


                VEXE vx = new VEXE();
                vx.IDKH = kh.IDKH;
                vx.IDPT = i.iIDPT;
                var NGAYXP = String.Format("{0:MM/dd/yyyy}", collection["NGAYXP"]);
                vx.NGAYXP = DateTime.Parse(NGAYXP);
                data.VEXEs.InsertOnSubmit(vx);
            }

            data.SubmitChanges();
            Session["iBus"] = null;
            return RedirectToAction("XacNhan", "DatVe");
        }
        //===================================================================

        [HttpGet]
        public List<iHotel> LayKs()
        {
            List<iHotel> lstDulieuks = Session["iHotel"] as List<iHotel>;
            if (lstDulieuks == null)
            {
                lstDulieuks = new List<iHotel>();
                Session["iHotel"] = lstDulieuks;
            }
            return lstDulieuks;
        }

        public ActionResult ThemiHotel(string iIDKS, string strURL)
        {
            List<iHotel> lstDulieuks = LayKs();
            iHotel ks = lstDulieuks.Find(n => n.iIDKS == iIDKS);
            if (ks == null)
            {
                ks = new iHotel(iIDKS);
                lstDulieuks.Add(ks);
                return Redirect(strURL);
            }

            else
            {
                return Redirect(strURL);
            }
        }

        [HttpGet]
        public ActionResult VeKS(string id)
        {
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Home");
            }
            //if (Session["iTour"] == null)
            //{
            //    return RedirectToAction("TrangChu", "Home");
            //}

            List<iHotel> lstDulieuks = LayKs();

            return View(lstDulieuks);
        }

        public ActionResult VeKS(FormCollection collection)
        {
            List<iHotel> ks = LayKs();
            foreach (var i in ks)
            {
                KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];


                VEPHONG vc = new VEPHONG();
                vc.IDKH = kh.IDKH;
                vc.IDKS = i.iIDKS;
                data.VEPHONGs.InsertOnSubmit(vc);
            }

            data.SubmitChanges();
            Session["iHotel"] = null;
            return RedirectToAction("XacNhan", "DatVe");
        }
        public ActionResult XacNhan()
        {
            return View();
        }
    }
}