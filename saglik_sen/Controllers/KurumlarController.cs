using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using saglik_sen.Models;

namespace saglik_sen.Controllers
{
    public class KurumlarController : Controller
    {
        // GET: Kurumlar
        SAGLIKSENEntities1 db = new SAGLIKSENEntities1();

        [Authorize]
        public ActionResult Index()
        {
            return View(db.KURUMLAR.ToList());
        }
        [Authorize]
        public ActionResult kurumekle()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult kurumekle(KURUMLAR p)
        {
            if(ModelState.IsValid)
            {
                db.KURUMLAR.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
            
        }
        [Authorize]
        public ActionResult guncelle(int id)
        {
            var model = db.KURUMLAR.Find(id);
            return View("guncelle",model);
        }
        [Authorize]
        [HttpPost]
        public ActionResult guncelle(KURUMLAR p)
        {
            if(ModelState.IsValid)
            {
                db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(); 

        }
        [Authorize]
        public ActionResult sil(int id)
        {
            var kurum = db.KURUMLAR.Find(id);
            db.KURUMLAR.Remove(kurum);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}