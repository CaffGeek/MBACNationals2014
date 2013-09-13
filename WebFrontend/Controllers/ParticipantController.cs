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
        
        [HttpPost]
        public ActionResult Create(CreateParticipant command)
        {
            command.Id = Guid.NewGuid();

            Domain.Dispatcher.SendCommand(command);

            return Redirect("Index");
        }
    }
}
