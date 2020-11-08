using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppContext = ArtX.Models.AppContext;


namespace ArtX.Controllers
{
    public class BookmarksController : Controller
    {
        private AppContext db = new ArtX.Models.AppContext();

        // GET: Bookmarks
        public ActionResult Index()
        {

            var bookmarks = from bookmark in db.Bookmarks
                           orderby bookmark.Title
                           select bookmark;
            
            ViewBag.Bookmarks = bookmarks;
            return View();
        }


        public ActionResult Show(int id)
        {
            Models.Bookmark bookmark = db.Bookmarks.Find(id);
            ViewBag.Bookmark = bookmark;
            return View();
        }


        public ActionResult New()
        {
            return View();
        }


        [HttpPost]
        public ActionResult New(Models.Bookmark bookmark)
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