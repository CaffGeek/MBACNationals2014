using System.Web.Mvc;
namespace WebFrontend.Controllers
{
    public class ContingentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(string province)
        {
            return View(province);
        }
    }
}
