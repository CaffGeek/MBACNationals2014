using MBACNationals.Contingent.Commands;
using MBACNationals.Participant.Commands;
using System;
using System.Web.Mvc;
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
        public ActionResult Reservation(string province)
        {
            if (string.IsNullOrWhiteSpace(province))
                return View("_ProvinceSelector");

            ViewBag.Province = province;
            return View();
        }

        [RestrictAccessByRouteId] //Province
        public ActionResult Edit(string province)
        {
            if (string.IsNullOrWhiteSpace(province))
                return View("_ProvinceSelector");

            ViewBag.Province = province;
            return View();
        }

        [RestrictAccessByRouteId] //Province
        public ActionResult Arrivals(string province)
        {
            if (string.IsNullOrWhiteSpace(province))
                return View("_ProvinceSelector");

            ViewBag.Province = province;
            return View();
        }

        [RestrictAccessByRouteId] //Province
        public ActionResult Practice(string province)
        {
            if (string.IsNullOrWhiteSpace(province))
                return View("_ProvinceSelector");

            ViewBag.Province = province;
            return View();
        }

        [RestrictAccessByRouteId] //Province
        public ActionResult Profiles(string province)
        {
            if (string.IsNullOrWhiteSpace(province))
                return View("_ProvinceSelector");

            ViewBag.Province = province;
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

        [HttpGet]
        public JsonResult TravelPlans(string province)
        {
            var travelPlans = Domain.ContingentTravelPlanQueries.GetTravelPlans(province);

            return Json(travelPlans, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Rooms(string province)
        {
            var rooms = Domain.ContingentTravelPlanQueries.GetRooms(province);

            return Json(rooms, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult CreateTeam(CreateTeam command)
        {
            command.TeamId = command.TeamId == Guid.Empty
                ? Guid.NewGuid()
                : command.TeamId;

            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult RemoveTeam(RemoveTeam command)
        {
            if (command.TeamId == null || command.TeamId.Equals(Guid.Empty))
                return Json(command);

            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult AssignParticipantToContingent(AddParticipantToContingent command)
        {
            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult AssignParticipantToTeam(AddParticipantToTeam command)
        {
            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult AssignCoachToTeam(AddCoachToTeam command)
        {
            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult ChangeRoomType(ChangeRoomType command)
        {
            var contingent = Domain.ContingentViewQueries.GetContingent(command.Province);

            if (contingent == null)
                return Json(command);

            command.Id = contingent.Id;
            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult SaveTravelPlans(SaveTravelPlans command)
        {
            if (command.Id == null || command.Id.Equals(Guid.Empty))
                return Json(command);

            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }
    }
}

