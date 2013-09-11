using MBACNationals.Participant;
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
        public void Create(CreateParticipant command)
        {
            //TODO: This doesn't work because I'm passed two strings, not a createparticipant command...
            //      What's right? changing UI to pass command, or create command here???
            System.Diagnostics.Debugger.Break();
        }
    }
}
