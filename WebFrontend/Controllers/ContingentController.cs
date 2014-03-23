using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using WebFrontend.Attributes;
using MBACNationals.Participant.Commands;
using System;
using MBACNationals.Team.Commands;

namespace WebFrontend.Controllers
{
    [Authorize]//TODO: Setup roles
    public class ContingentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Index(string province)
        {
            var contingent = Domain.ContingentViewQueries.GetContingent(province);

            if (contingent != null)
                return Json(contingent, JsonRequestBehavior.AllowGet);
            
            var command = new MBACNationals.Contingent.Commands.CreateContingent();
            command.Id = Guid.NewGuid();
            command.Province = province;
            Domain.Dispatcher.SendCommand(command);
            contingent = Domain.ContingentViewQueries.GetContingent(province);

            return Json(contingent, JsonRequestBehavior.AllowGet);
        }

        [RestrictAccessByRouteId] //Province
        public ActionResult Edit(string province)
        {
            return View(province);
        }

        [HttpPost]
        public JsonResult AssignTeamToContingent(AddTeamToContingent command)
        {
            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }

        [HttpPost]
        public JsonResult AssignParticipantToTeam(AddParticipantToTeam command)
        {
            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }
    }
}

