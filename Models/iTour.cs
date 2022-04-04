using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOANWEBDULICH.Models
{
    public class iTour
    {
        dbQLDuLichDataContext data = new dbQLDuLichDataContext();
        public string iIDTOUR { set; get; }
        public string iTENTOUR { set; get; }

        public iTour(string IDTOUR)
        {
            iIDTOUR = IDTOUR;
            TOURDULICH tour = data.TOURDULICHes.Single(n => n.IDTOUR == iIDTOUR);
            iTENTOUR = tour.TENTOUR;
        }

    }
}