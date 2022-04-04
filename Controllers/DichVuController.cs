using DOANWEBDULICH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOANWEBDULICH.Controllers
{
    public class DichVuController : Controller
    {
        dbQLDuLichDataContext data = new dbQLDuLichDataContext();
        // GET: DichVu
        public ActionResult ThueXe()
        {
            

            var xedl = (from s in data.PHUONGTIENs select s).ToList();

            return View(xedl);

        }

        public ActionResult KhachSan()
        {
            var ks = (from k in data.KHACHSANs select k).ToList();

            return View(ks);
        }
    }
}