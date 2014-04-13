using MBACNationals.Participant;
using MBACNationals.Participant.Commands;
using System;
using System.Web.Mvc;

namespace WebFrontend.Controllers
{
    public class ParticipantController : Controller
    {
        public ActionResult Index()
        {
            return View(
                new WebFrontend.Models.Participant.Index
                {
                    Participants = Domain.ParticipantQueries.GetParticipants(),
                });
        }

        [HttpGet]
        public JsonResult Index(Guid? id)
        {
            if (!id.HasValue)
                return Json(null, JsonRequestBehavior.AllowGet);

            var participant = Domain.ParticipantQueries.GetParticipant(id.Value);
            return Json(participant, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Contingent(string province)
        {
            var participant = Domain.ReservationQueries.GetParticipants(province);
            return Json(participant, JsonRequestBehavior.AllowGet);
        }

        public ActionResult View(Guid id)
        {
            return View(
                new WebFrontend.Models.Participant.View
                {
                    Participant = Domain.ParticipantQueries.GetParticipant(id),
                });
        }
        
        [HttpPost]
        public JsonResult Create(CreateParticipant command)
        {
            command.Id = Guid.NewGuid();

            Domain.Dispatcher.SendCommand(command);

            return Json(command);
        }

        [HttpPost]
        public JsonResult Update(UpdateParticipant command)
        {
            Domain.Dispatcher.SendCommand(command);

            return Json(command);
        }

        [HttpPost]
        public JsonResult AssignToRoom(AssignParticipantToRoom command)
        {
            Domain.Dispatcher.SendCommand(command);

            return Json(command);
        }

        [HttpPost]
        public JsonResult Rename(string id, string value)
        {
            var command = new RenameParticipant
            {
                Id = Guid.Parse(id),
                Name = value
            };

            Domain.Dispatcher.SendCommand(command);

            return Json(command);
        }
    }
}
