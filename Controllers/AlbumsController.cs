using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppContext = ArtX.Models.AppContext;


namespace ArtX.Controllers
{

    public class AlbumsController : Controller
    {
        private AppContext db = new ArtX.Models.AppContext();

        // GET: Albums
        public ActionResult Index()
        {
            return View();
        }
    }
}