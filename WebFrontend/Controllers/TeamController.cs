using MBACNationals.Team.Commands;
using System;
using System.Web.Mvc;

namespace WebFrontend.Controllers
{
    public class TeamController : Controller
    {
        [HttpPost]
        public ActionResult Create(CreateTeam command)
        {
            command.Id = Guid.NewGuid();

            Domain.Dispatcher.SendCommand(command);

            return RedirectToAction("Index", "Participant");
        }
    }
}
