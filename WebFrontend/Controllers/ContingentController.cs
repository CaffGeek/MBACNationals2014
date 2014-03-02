using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using WebFrontend.Attributes;
using MBACNationals.Participant.Commands;
using System;

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

        [HttpPost]
        public JsonResult AssignParticipantToTeam(AddParticipantToTeam command)
        {
            Domain.Dispatcher.SendCommand(command);

            //return RedirectToAction("Index", "Contingent"); 
            return Json(command);
        }
    }
}

