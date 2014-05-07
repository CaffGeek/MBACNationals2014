using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebFrontend.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        public void Rebuild()
        {
            Domain.RebuildReadModels();
        }

    }
}
