using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtX.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace ArtX.Controllers
{
    public class SavedBookmarksController : Controller
    {
        private ArtX.Models.ApplicationDbContext db = new ArtX.Models.ApplicationDbContext();
        
        // GET: SavedBookmarks
  
        public ActionResult Save(int id)
        {
            /* Bookmark bookmark = db.Bookmarks.Find(bookmarkId);*/
            /* var appUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);*/

            string currentUserId = User.Identity.GetUserId();
            SavedBookmark savedBookmark = new SavedBookmark();
            savedBookmark.BookmarkId = id;
            savedBookmark.UserId = currentUserId;
      /*      db.SavedBookmarks.Add(savedBookmark);*/
            /*db.SaveChanges();*/
            try
            {
                var b = db.SavedBookmarks.FirstOrDefault(x => x.BookmarkId == savedBookmark.BookmarkId && x.UserId == savedBookmark.UserId);

                if ( b == null)
                {
                    db.SavedBookmarks.Add(savedBookmark);

                    UpdateModel(db);
                    db.SaveChanges();
                    TempData["message"] = "Bookmark-ul a fost adaugat!";
                    return Redirect("/Bookmarks/Index");
                }
                else
                {
                    TempData["message"] = "Bookmark-ul este deja in MyBookmarks!";
                    return Redirect("/Bookmarks/Index");
                }

            }
            catch (Exception e)
            {
                return Redirect("/Bookmarks/Index");
            }
        }
/*
        [HttpPost]
        public ActionResult Save(SavedBookmark savedBookmark)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.SavedBookmarks.Add(savedBookmark);
                    db.SaveChanges();
                    TempData["message"] = "Bookmark-ul a fost adaugat!";
                    return RedirectToAction("Index");
                }
                else
                    return View(savedBookmark);
            }
            catch (Exception e)
            {
                return View(savedBookmark);
            }
        }*/

        public ActionResult Index()
        {
            string currentUserId = "-1";

            string b = "nu";

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                currentUserId = User.Identity.GetUserId();

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated && User.IsInRole("Admin"))
                b = "da";

            ViewBag.userId = currentUserId;
            ViewBag.b = b;

            var bookmarks = new List<Bookmark>();

           var allBookmarks =  db.Bookmarks.Include("Album").Include("User");

            foreach (SavedBookmark sbm in db.SavedBookmarks)//toate bookmarkurile salvate
            {
                Bookmark bookmark = new Bookmark();
                if (sbm.UserId == currentUserId)    //daca a fost salvat de userul curent
                {
                    bookmark = allBookmarks.FirstOrDefault(x => x.BookmarkId == sbm.BookmarkId);    //extrag bookmark ul
                    bookmarks.Add(bookmark);
                }
            }

            ViewBag.myBookmarks = bookmarks;

            return View();
        }
    }
}