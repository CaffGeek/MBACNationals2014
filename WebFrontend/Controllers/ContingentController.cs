﻿using MBACNationals.Contingent.Commands;
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
            ViewBag.Province = province;
            return View();
        }

        [RestrictAccessByRouteId] //Province
        public ActionResult Edit(string province)
        {
            ViewBag.Province = province;
            return View();
        }

        [RestrictAccessByRouteId] //Province
        public ActionResult Arrivals(string province)
        {
            ViewBag.Province = province;
            return View();
        }

        [RestrictAccessByRouteId] //Province
        public ActionResult Practice(string province)
        {
            ViewBag.Province = province;
            return View();
        }

        [RestrictAccessByRouteId] //Province
        public ActionResult Profiles(string province)
        {
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
        public JsonResult RemoveTeam(RemoveTeam command)
        {
            if (command.TeamId == null || command.TeamId == Guid.Empty)
                return Json(command);

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

