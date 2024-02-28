using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using saglik_sen.Models;


namespace saglik_sen.Controllers
{
    public class UyelerController : Controller
    {
        SAGLIKSENEntities1 db =new SAGLIKSENEntities1();
        
        // GET: Uyeler
        [Authorize]
        public ActionResult Index()
        {
            
            var listele = db.UYELER.ToList();
            var gonderilecekListe = new List<uyelerVM>();
            foreach (var item in listele)
            {
                uyelerVM kayit = new uyelerVM();
                kayit.ID = item.ID;
                kayit.TC = item.TC;
                kayit.AD = item.AD;
                kayit.SOYAD = item.SOYAD;
                kayit.DOGUM_TARIHI = item.DOGUM_TARIHI.ToString("dd/MM/yyyy");
                kayit.UNVAN = item.UNVAN;
                kayit.TEL = item.TEL;
                kayit.KURUMADI = item.KURUMLAR.KURUM;
                kayit.UYELIK_DURUMU = item.UYELIK_DURUMU==1?"Aktif":"Pasif";
                gonderilecekListe.Add(kayit);

            }
            ViewBag.Uyeler = gonderilecekListe;
            return View();
            
           
        }
        [Authorize]
        public ActionResult uyeekle()
        {
            //return View();
            ViewBag.kurumlar = db.KURUMLAR.ToList();
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult uyeekle(UYELER p)
        {
            if (p.TEL != null || p.TEL.Length >= 10 || p.TEL.Length <= 11)
            {

                if (!int.TryParse(p.TEL, out int cikis))
                {
                    ModelState.AddModelError("TEL", "Telefon Numarası Sayılardan Oluşmalıdır.");
                }
            }
            else
            {
                ModelState.AddModelError("TEL", "Lütfen geçerli bir telefon numarası giriniz.");
            }
            if (p.TC != null || p.TC.Length == 11)
            {

                if (!int.TryParse(p.TC, out int cikis))
                {
                    ModelState.AddModelError("TC", "Tc kimlik Numarası Sayılardan Oluşmalıdır.");
                }
            }
            else
            {
                ModelState.AddModelError("TC", "Lütfen geçerli bir tc kimlik numarası giriniz.");
            }
            if (ModelState.IsValid)
            {
                db.UYELER.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.kurumlar = db.KURUMLAR.ToList();
                return View(p);
            }
         
        }
        [Authorize]
       public ActionResult guncelle(int id)
        {
           ViewBag.kurumlar1 = db.KURUMLAR.ToList();
            var model = db.UYELER.FirstOrDefault(x=>x.ID==id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));

            }
            if (model.UYELIK_DURUMU == 1)
            {
                model.uyelik_durumu2 = true;
            }
            else
            {
                model.uyelik_durumu2 = false;
            }
            
            return View("guncelle", model);

        } 

       
        [Authorize]
        [HttpPost]
        public ActionResult guncelle(UYELER n)
        {
            if (n.uyelik_durumu2)
            {
                n.UYELIK_DURUMU = 1;
            }
            else
            {
                n.UYELIK_DURUMU = 0;
            }
            if (n.TEL!=null||n.TEL.Length>=10||n.TEL.Length<=11)
            {
               
                if (!int.TryParse(n.TEL,out int cikis))
                {
                    ModelState.AddModelError("TEL", "Telefon Numarası Sayılardan Oluşmalıdır.");
                }
            }
            else
            {
                ModelState.AddModelError("TEL", "Lütfen geçerli bir telefon numarası giriniz.");
            }
            if (n.TC != null || n.TEL.Length <= 11)
            {

                if (!int.TryParse(n.TEL, out int cikis))
                {
                    ModelState.AddModelError("TC", "Tc kimlik Numarası Sayılardan Oluşmalıdır.");
                }
            }
            else
            {
                ModelState.AddModelError("TC", "Lütfen geçerli bir tc kimlik numarası giriniz.");
            }
            if (ModelState.IsValid)
            {
               
                db.Entry(n).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else 
            {
                
                return guncelle(n.ID); 
            }
           

        } 
        [Authorize]
        public ActionResult Sil(int id)
        {
            var uye = db.UYELER.Find(id);
            db.UYELER.Remove(uye);
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }
        public int dogumsayidondur()
        {
            var dogumGunuOlanlar = db.UYELER.ToList().Where(p => p.DOGUM_TARIHI.ToString("dd/MM") == DateTime.Now.ToString("dd/MM") && p.UYELIK_DURUMU == 1);
            return dogumGunuOlanlar.Count();

        }
        public int bugungonderilen()
        {
            var dogumGunuOlanlar = db.LOG.ToList().Where(p => p.GONDERIM_TARIHI.ToString("dd/MM") == DateTime.Now.ToString("dd/MM") );
            return dogumGunuOlanlar.Count();

        }
        public int buhaftagonderilen()
        {
            var dogumGunuOlanlar = db.LOG.ToList().Where(x=>(new DateTime(x.GONDERIM_TARIHI.Year,x.GONDERIM_TARIHI.Month,x.GONDERIM_TARIHI.Day)) >= (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)).AddDays(-7));
            return dogumGunuOlanlar.Count();
        }
    }
}