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

        [Authorize(Roles = "User,Admin"), AllowAnonymous]
        // GET: Albums
        public ActionResult Index()
        {
            var albums = db.Albums;
            ViewBag.Albums = albums;
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }
            return View();
        }

        [Authorize(Roles = "User,Admin"), AllowAnonymous]
        public ActionResult Show(int id)
        {
            Album album = db.Albums.Find(id);
            ViewBag.Album = album;

            return View();

        }

        [Authorize(Roles = "Admin, User")]
        public ActionResult New()
        {
            Album album = new Album();
            return View(album);
        }

        
        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public ActionResult New(Album album)
        {
            try
            {
                db.Albums.Add(album);
                db.SaveChanges();
                TempData["message"] = "Albumul a fost adaugat!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }


        [HttpDelete]
        [Authorize(Roles = "Admin, User")]
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