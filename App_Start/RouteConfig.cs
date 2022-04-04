using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DOANWEBDULICH
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Tour du lich ==============================================================================
            //routes.MapRoute(
            //    name: "MienBac",
            //    url: "MienBac",
            //    defaults: new { controller = "DuLich", action = "DuLichMB", id = UrlParameter.Optional }
            //);
            // routes.MapRoute(
            //     name: "MienTrung",
            //     url: "MienTrung",
            //     defaults: new { controller = "DuLich", action = "DuLichMT", id = UrlParameter.Optional }
            // );
            // routes.MapRoute(
            //     name: "MienNam",
            //     url: "MienNam",
            //     defaults: new { controller = "DuLich", action = "DuLichMN", id = UrlParameter.Optional }
            // );
            // // Chi tiet==============================================================================

            //routes.MapRoute(
            //    name: "ChiTietDL",
            //    url: "ChiTiet",
            //    defaults: new { controller = "Details", action = "DetailsDL", id = UrlParameter.Optional }
            //);

            // routes.MapRoute(
            //     name: "ChiTietXe",
            //     url: "ChiTietXe",
            //     defaults: new { controller = "Details", action = "DetailsXe", id = UrlParameter.Optional }
            // );

            // routes.MapRoute(
            //     name: "ChiTietKS",
            //     url: "ChiTietKS",
            //     defaults: new { controller = "Details", action = "DetailsKS", id = UrlParameter.Optional }
            // );

            // //Đặt Xe - Khach San========================================================
            // routes.MapRoute(
            //     name: "DatXe",
            //     url: "DatXe",
            //     defaults: new { controller = "DichVu", action = "ThueXe", id = UrlParameter.Optional }
            // );

            // routes.MapRoute(
            //    name: "KhachSan",
            //    url: "KhachSan",
            //    defaults: new { controller = "DichVu", action = "KhachSan", id = UrlParameter.Optional }
            //);

            //==============================================================================

            routes.MapRoute(
                name: "TrangChu",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "TrangChu", id = UrlParameter.Optional }
            );
        }
    }
}
