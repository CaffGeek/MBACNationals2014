using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using WebFrontend.Attributes;
using MBACNationals.Participant.Commands;
using System;
using System.Linq;
using MBACNationals.Contingent.Commands;

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
        public ActionResult Reservation()
        {
            return View();
        }

        [RestrictAccessByRouteId] //Province
        public ActionResult Edit()
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

        [HttpPost]
        public JsonResult CreateTeam(CreateTeam command)
        {
            command.TeamId = command.TeamId == Guid.Empty
                ? Guid.NewGuid()
                : command.TeamId;

            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }
        
        [HttpPost]
        public JsonResult AssignParticipantToTeam(AddParticipantToTeam command)
        {
            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }

        [HttpPost]
        public JsonResult AssignCoachToTeam(AddCoachToTeam command)
        {
            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }
    }
}

