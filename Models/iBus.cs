using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOANWEBDULICH.Models
{
    public class iBus
    {
        dbQLDuLichDataContext data = new dbQLDuLichDataContext();
        public string iIDPT { set; get; }
        public string iTENPT { set; get; }

        public iBus(string IDPT)
        {
            iIDPT = IDPT;
            PHUONGTIEN bus = data.PHUONGTIENs.Single(n => n.IDPT == iIDPT);
            iTENPT = bus.TENPT;
        }
    }
}