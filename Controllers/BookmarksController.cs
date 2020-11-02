using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtX.Controllers
{
    public class BookmarksController : Controller
    {

        // GET: Bookmarks
        public ActionResult Index()
        {
            return View();
        }
    }
}