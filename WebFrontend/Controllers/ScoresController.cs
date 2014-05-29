using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebFrontend.Controllers
{
    public class ScoresController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Entry");
        }

        public ActionResult Entry()
        {
            return View();
        }
    }
}
