using ArtX.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ArtX.Controllers
{
    public class BookmarksController : Controller
    {
        private ArtX.Models.ApplicationDbContext db = new ArtX.Models.ApplicationDbContext();

        // GET: Bookmarks
        [Authorize(Roles = "User,Admin,Editor")]
        public ActionResult Index()
        {
            var bookmarks = db.Bookmarks.Include("Album").Include("User");
            ViewBag.Bookmarks = bookmarks;
            return View();
        }

        // GET: Show
        [Authorize (Roles = "User,Admin,Editor")]
        public ActionResult Show(int id)
        {
            Bookmark bookmark = db.Bookmarks.Find(id);
            ViewBag.Bookmark = bookmark;

            if (bookmark.Album is not null)
            {
                ViewBag.Album = bookmark.Album;
            }

            return View();

        }

        // GET: New
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult New()
        {
            Bookmark bookmark = new Bookmark();
            bookmark.UserId = User.Identity.GetUserId();

            var albums = from alb in db.Albums
                         select alb;
            ViewBag.Albums = albums;

            return View(bookmark);
        }

        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        public ActionResult New(Bookmark bookmark)
        {
            bookmark.UserId = User.Identity.GetUserId();

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

        //GET: Edit
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Edit(int id)
        {
            Bookmark bookmark = db.Bookmarks.Find(id);
            ViewBag.Bookmark = bookmark;
            return View();
        }

        [Authorize(Roles = "Admin,Editor")]
        [HttpPut]
        public ActionResult Edit(int id, Bookmark requestBookmark)
        {
            try
            {
                Bookmark bookmark = db.Bookmarks.Find(id);

                if (TryUpdateModel(bookmark))
                {
                    bookmark.Title = requestBookmark.Title;
                    bookmark.Description = requestBookmark.Description;
                    //rating ul si data nu ar trebui sa poata fi schimbate
                    //sau de fapt logic cred ca ar fi sa se reseteze la 0 rating- ul? din moment ce se modifica content ul
                    //sau oricum e aiurea, poate facem sa permitem doar modificarea descrierii si atat. Cum vrei si tu
                    //vedem
                    //iar albumul nu poate fi schimbat de aici, ca un bookmark poate fi in mai multe albume. O sa facem din AlbumController sa putem elimina/adauga un bookmark. Va fi optiunea utilizatorului
                    //ssau poate facem si de aici.. sa apara lista albumelor in care a fost adaugat si sa putem debifa unele albume, daca ar fi sa i dam adminului permisiunea asta dar nush mai vedem, ne mai uitam pe ce zice in cerinte.

                    //ramane de facut pt Content cu imagini.
                    bookmark.Content = requestBookmark.Content;

                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin,Editor")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Bookmark bookmark = db.Bookmarks.Find(id);
            db.Bookmarks.Remove(bookmark);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
