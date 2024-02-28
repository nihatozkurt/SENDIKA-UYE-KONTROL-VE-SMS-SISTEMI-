using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace saglik_sen.Controllers
{
    public class HakkindaController : Controller
    {
        // GET: Hakkinda
        [Authorize]
        public ActionResult Index()
        {
            
            return View();
        }
    }
}