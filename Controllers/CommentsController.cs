using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppContext = ArtX.Models.AppContext;


namespace ArtX.Controllers
{
    public class CommentsController : Controller
    {
        private AppContext db = new ArtX.Models.AppContext();

        // GET: Comments
        public ActionResult Index()
        {
            return View();
        }
    }
}