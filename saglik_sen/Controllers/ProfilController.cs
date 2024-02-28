using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using saglik_sen.Models;

namespace saglik_sen.Controllers
{
    public class ProfilController : Controller
    {
        // GET: Profil
        SAGLIKSENEntities1 db =new SAGLIKSENEntities1();
        [Authorize]
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult guncelle(KULLANICILAR id)
        {
            db.Entry(id).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Redirect("/Login/Logout");
        }

    }
}