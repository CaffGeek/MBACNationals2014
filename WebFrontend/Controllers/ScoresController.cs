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

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult Schedule(string division)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");

            var schedule = Domain.ScheduleQueries.GetSchedule(division);
            return Json(schedule, JsonRequestBehavior.AllowGet);
        }
    }
}
