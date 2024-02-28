using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using saglik_sen.Models;
using saglik_sen.SmsVM;

namespace saglik_sen.Controllers
{
    public class SmsController : Controller
    {
        // GET: Sms
        SAGLIKSENEntities1 db = new SAGLIKSENEntities1();
        public ActionResult Index()
        {

            var dogumGunuOlanlar = db.UYELER.ToList().Where(p => p.DOGUM_TARIHI.ToString("dd/MM") == DateTime.Now.ToString("dd/MM") && p.UYELIK_DURUMU==1 && p.TEL != null);
            return View(dogumGunuOlanlar.ToList());
        }

        public  ActionResult KurumSMSGONDER()
        {
            ViewBag.kurumlar = db.KURUMLAR.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult KurumSMSGONDER(KurumSmsVM smsVM)
        {
            if (ModelState.IsValid)
            {
                smsVM.UYELERs = db.UYELER.Where(x => x.KURUM_ID == smsVM.KURUM_ID).ToList();
                kurumsmsyolla(smsVM);            
                
            }
            ViewBag.kurumlar = db.KURUMLAR.ToList();

            return View(smsVM);
        }
        public void kurumsmsyolla(KurumSmsVM asdf)
        {
            string ss = "";

            foreach (var item in asdf.UYELERs)
            {
                ss += "<?xml version='1.0' encoding='UTF-8'?>";
                ss += "<MainmsgBody>";
                ss += "<UserName>KADI</UserName>";
                ss += "<PassWord>SIFRE</PassWord>";
                ss += "<CompanyCode>4344</CompanyCode>";
                ss += "<Type>12</Type>"; //Türkçe 12 / Normal 5
                ss += "<Developer></Developer>";
                ss += "<Originator>S.SEN IST-1</Originator >";
                ss += "<Version>xVer.2016</Version>";
                string gonderileceksms = "Sayın " + item.AD + " " + item.SOYAD + ", " + asdf.mesaj;
                ss += "<Mesgbody><![CDATA[" + gonderileceksms + "]]></Mesgbody>";
                ss += "<Numbers>" + item.TEL + "</Numbers>";
                ss += "<SDate></SDate>";
                ss += "<EDate></EDate>";
                ss += "</MainmsgBody>";
                XMLPOST("https://gateway.3gbilisim.com/SendSmsMany.aspx", ss);
                LOG logekle = new LOG();
                logekle.ADI = item.AD;
                logekle.TC = item.TC;
                logekle.GONDERIM_TARIHI = DateTime.Now;
                logekle.GONDERILEN_SMS = gonderileceksms;

                db.LOG.Add(logekle);
                db.SaveChanges();
            }


        }
        public string XMLPOST(string PostAddress, string xmlData)
        {
            try
            {
                WebClient wUpload = new WebClient();
                HttpWebRequest request = WebRequest.Create(PostAddress) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                Byte[] bPostArray = Encoding.UTF8.GetBytes(xmlData);
                Byte[] bResponse = wUpload.UploadData(PostAddress, "POST", bPostArray);
                Char[] sReturnChars = Encoding.UTF8.GetChars(bResponse);
                string sWebPage = new string(sReturnChars);
                return sWebPage;
            }
            catch
            {
                return "-1";
            }
        }





    


}
}