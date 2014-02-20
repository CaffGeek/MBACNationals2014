using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using WebFrontend.Attributes;

namespace WebFrontend.Controllers
{
    [Authorize]//TODO: Setup roles
    public class ContingentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [RestrictAccessByRouteId] //Province
        public ActionResult Edit(string province)
        {
            return View(province);
        }
    }
}

