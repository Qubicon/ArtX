using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtX.Models;
using System.Linq;

namespace ArtX.Controllers
{
    public class AlbumsController : Controller
    {

        private ArtX.Models.ApplicationDbContext db = new ArtX.Models.ApplicationDbContext();

        [Authorize(Roles = "User,Admin,Editor")]
        // GET: Albums
        public ActionResult Index()
        {
            var albums = db.Albums;
            ViewBag.Albums = albums;
            return View();
        }

        [Authorize(Roles = "User,Admin,Editor")]
        public ActionResult Show(int id)
        {
            Album album = db.Albums.Find(id);
            ViewBag.Album = album;

            return View();

        }

        [Authorize(Roles = "Admin,Editor")]
        public ActionResult New()
        {
        /*    var bookmarks = from b in db.Bookmarks    //daca vom face sa si selecteze bookmarksuri inca de la creearea albumului, dar eu zic ca nu e nevoie
                         select b;
            ViewBag.Bookmarks = bookmarks;*/
            return View();
        }

        
        [HttpPost]
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult New(Album album)
        {
            try
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }


        [HttpDelete]
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Delete(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            TempData["message"] = "Albumul a fost sters";
            return RedirectToAction("Index");
        }

    }
}