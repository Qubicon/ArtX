using ArtX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ArtX.Models.Bookmark;

namespace ArtX.Controllers
{
    public class BookmarksController : Controller
    {
        private Models.AppContext db = new Models.AppContext();
        // GET: Bookmarks
        public ActionResult Index()
        {
            var bookmarks = db.Bookmarks.Include("Album");
            ViewBag.Bookmarks = bookmarks;
            return View();
        }

        // GET: Show
        public ActionResult Show(int id)
        {
            Bookmark bookmark = db.Bookmarks.Find(id);
            ViewBag.Bookmark = bookmark;
            ViewBag.Album = bookmark.Album;

            return View();

        }

        // GET: New
        public ActionResult New()
        {
            var albums = from alb in db.Albums
                         select alb;
            ViewBag.Albums = albums;
            return View();
        }

        [HttpPost]
        public ActionResult New(Bookmark bookmark)
        {
            try
            {
                db.Bookmarks.Add(bookmark);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}
