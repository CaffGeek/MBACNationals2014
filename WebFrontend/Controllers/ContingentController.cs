using System.Web.Mvc;
namespace WebFrontend.Controllers
{
    [Authorize]//TODO: Setup roles
    public class ContingentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(string province)
        {
            //TODO: Ensure access to province
            return View(province);
        }
    }
}
