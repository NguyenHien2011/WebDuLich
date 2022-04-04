using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOANWEBDULICH.Models
{
    public class iHotel
    {
        dbQLDuLichDataContext data = new dbQLDuLichDataContext();
        public string iIDKS { set; get; }
        public string iTENKS { set; get; }

        public iHotel(string IDKS)
        {
            iIDKS = IDKS;
            KHACHSAN ks = data.KHACHSANs.Single(n => n.IDKS == iIDKS);
            iTENKS = ks.TENKS;
        }
    }
}