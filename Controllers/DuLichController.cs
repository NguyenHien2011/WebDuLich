using DOANWEBDULICH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DOANWEBDULICH.Controllers
{
    public class DuLichController : Controller
    {
        dbQLDuLichDataContext data = new dbQLDuLichDataContext();
        // GET: DuLich

        private List<MIEN> mbac(int count)
        {
            return data.MIENs.OrderByDescending(a => a.IDMIEN).Take(count).ToList();
        }

        public ActionResult DuLichMB ()
        {
            var MB = (from s in data.TOURDULICHes
                            where s.MIEN.IDMIEN == "MIB"
                            select s).ToList();

            return View(MB);
        }

        public ActionResult DuLichMT()
        {
            var MT = (from s in data.TOURDULICHes
                      where s.MIEN.IDMIEN == "MIT"
                      select s).ToList();
            return View(MT);
        }

        public ActionResult DuLichMN()
        {
            var MN = (from s in data.TOURDULICHes
                      where s.MIEN.IDMIEN == "MIN"
                      select s).ToList();
            return View(MN);
        }




    }
}