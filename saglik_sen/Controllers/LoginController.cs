using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using saglik_sen.Models;

namespace saglik_sen.Controllers
{
    public class LoginController : Controller
    {
        SAGLIKSENEntities1 db = new SAGLIKSENEntities1();
        // GET: Login

        public ActionResult Index()
        {
            return View();
        }
        
        
        [HttpPost]
        public ActionResult Index2(KULLANICILAR p)
        {
            var dogrula = db.KULLANICILAR.FirstOrDefault(x => x.KULLANICI == p.KULLANICI && x.SIFRE == p.SIFRE);
            if (dogrula != null)
            {
                FormsAuthentication.SetAuthCookie(dogrula.KULLANICI, false);
                Session["Ad"] = dogrula.KULLANICI.ToString();
                Session["KID"] = dogrula.KULLANICI_ID;
                return Redirect("/Uyeler/Index");

            }
            else
            {
                
                ViewBag.hata = "Kullanıcı Adınız Veya Şifreniz Hatalı!";
                return View("Index");
            }
            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }


    }
}