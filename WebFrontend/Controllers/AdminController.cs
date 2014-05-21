using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebFrontend.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        [Authorize(Users = "Chad")]
        public void Rebuild()
        {
            Domain.RebuildReadModels();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Reports()
        {
            return View();
        }
    }
}
