using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOANWEBDULICH.Models;

namespace DOANWEBDULICH.Controllers
{
    public class DetailsController : Controller
    {
        dbQLDuLichDataContext data = new dbQLDuLichDataContext();
        // GET: Details

        public ActionResult DetailsDL(string id)
        {
            var tour = from t in data.TOURDULICHes
                       where t.IDTOUR == id
                       select t;
            return View(tour);
        }
        public ActionResult DetailsKS(string id)
        {
            var khachsan = from t in data.KHACHSANs
                       where t.IDKS == id
                       select t;
            return View(khachsan);
        }
        public ActionResult DetailsXe(string id)
        {
            var Xe = from t in data.PHUONGTIENs
                           where t.IDPT == id
                           select t;
            return View(Xe);
        }
    }
}