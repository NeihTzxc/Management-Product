using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using qlbh.Models;

namespace qlbh.Controllers
{
    public class HomeController : Controller
    {
        qlbanhangEntities db = new qlbanhangEntities();

        public ActionResult Index(string maloaisp = "null")
        {
            if (maloaisp == "null")
            {
                var sanPhams = db.SanPhams.Include(s => s.LoaiSP);
                return View(sanPhams.ToList());
            }
            else
            {
                var sanPhams = db.SanPhams.Include(s => s.LoaiSP).Where(x => x.MaLoaiSP.ToString() == maloaisp);
                return View(sanPhams.ToList());
            }
            //var sanPhams = db.SanPhams.Include(s => s.LoaiSP).Where(x => x.MaLoaiSP.ToString() == "BE1D321B-BEDC-44E4-87F9-D11C544FD351");
            //return View(sanPhams.ToList());

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}