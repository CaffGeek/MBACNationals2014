using MBACNationals.Participant;
using System;
using System.Web.Mvc;

namespace WebFrontend.Controllers
{
    public class ParticipantController : Controller
    {
        public ActionResult Index()
        {
            return View(Domain.ParticipantQueries.GetParticipants());
        }

        public ActionResult View(Guid id)
        {
            return View(Domain.ParticipantQueries.GetParticipant(id));
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
