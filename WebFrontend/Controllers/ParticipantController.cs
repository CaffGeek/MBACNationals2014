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
                    Teams = Domain.TeamQueries.GetTeams(),
                });
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
        public ActionResult Create(CreateParticipant command)
        {
            command.Id = Guid.NewGuid();

            Domain.Dispatcher.SendCommand(command);

            return Redirect("Index");
        }

        [HttpPost]
        public ActionResult Rename(string id, string value)
        {
            var command = new RenameParticipant
            {
                Id = Guid.Parse(id),
                Name = value
            };

            Domain.Dispatcher.SendCommand(command);

            return Content(value);
        }
    }
}
