using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtX.Models;

namespace ArtX.Controllers
{
    public class AlbumsController : Controller
    {

        private ArtX.Models.AppContext db = new ArtX.Models.AppContext();

        // GET: Albums
        public ActionResult Index()
        {
            var albums = db.Albums;
            ViewBag.Albums = albums;
            return View();
        }

        public ActionResult Show(int id)
        {
            Album album = db.Albums.Find(id);
            ViewBag.Album = album;

            return View();

        }
    }
}