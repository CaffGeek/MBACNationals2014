using MBACNationals.Team.Commands;
using System;
using System.Web.Mvc;

namespace WebFrontend.Controllers
{
    public class TeamController : Controller
    {
        public ActionResult View(Guid id)
        {
            return View(
                new WebFrontend.Models.Team.View
                {
                    Team = Domain.TeamQueries.GetTeam(id),
                });
        }

        [HttpPost]
        public JsonResult Create(CreateTeam command)
        {
            command.Id = Guid.NewGuid();

            Domain.Dispatcher.SendCommand(command);

            return Json(command);
        }
    }
}
