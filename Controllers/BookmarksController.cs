using ArtX.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace ArtX.Controllers
{
    public class BookmarksController : Controller
    {
        private ArtX.Models.ApplicationDbContext db = new ArtX.Models.ApplicationDbContext();

        private int _perPage = 3;
        // GET: Bookmarks
        [Authorize(Roles = "User,Admin"), AllowAnonymous]

        public ActionResult Index(string Criteriu = "def", string Ordine = "def")
        {
           /* foreach (var sb in db.SavedBookmarks)
                db.SavedBookmarks.Remove(sb);
            foreach (var sb in db.Bookmarks)
                db.Bookmarks.Remove(sb);*/

            db.SaveChanges();
            var bookmarks = db.Bookmarks.Include("Album").Include("User").OrderByDescending(o => o.Date); //default

            ViewBag.Bookmarks = bookmarks;

            string userId = "-1";
            string b = "nu";
 

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                userId = User.Identity.GetUserId();

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated && User.IsInRole("Admin"))
                b = "da";

            ViewBag.userId = userId;
            ViewBag.b = b;

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }

            // SEARCH

            var search = ""; 
            
            if (Request.Params.Get("search") != null)
            {
                search = Request.Params.Get("search").Trim();
                // trim whitespace from search string

                List<int> listaIds = db.Bookmarks.Where(
                    at => at.Title.Contains(search) || at.Description.Contains(search) || at.Tags.Contains(search)).Select(a => a.BookmarkId).ToList();
                // Search dupa titlu, descriere si tags

                bookmarks = db.Bookmarks.Where(bookmark => listaIds.Contains(bookmark.BookmarkId)).Include("Album").Include("User").OrderBy(a => a.Date); //default

                if (Criteriu == "Rating" && Ordine == "Desc")
                    bookmarks = bookmarks.OrderBy(o => -o.Rating);

                if (Criteriu == "Rating" && Ordine == "Cresc")
                    bookmarks = bookmarks.OrderBy(o => o.Rating);

                if (Criteriu == "Data" && Ordine == "Desc" || Criteriu == "def" && Ordine =="def")
                    bookmarks = bookmarks.OrderByDescending(o => o.Date);

                if (Criteriu == "Data" && Ordine == "Cresc")
                    bookmarks = bookmarks.OrderBy(o => o.Date);

                ViewBag.Bookmarks = bookmarks;
            }

            // PAGINATION

            var totalItems = bookmarks.Count();

            var currentPage = Convert.ToInt32(Request.Params.Get("page"));

            var offset = 0;

            if (!currentPage.Equals(0))
            {
                offset = (currentPage - 1) * this._perPage;
            }

            var paginatedBookmarks = bookmarks.Skip(offset).Take(this._perPage);

            // ViewBag.perPage = this._perPage;

            ViewBag.total = totalItems;
            ViewBag.lastPage = Math.Ceiling((float)totalItems / (float)this._perPage);
            ViewBag.Bookmarks = paginatedBookmarks;
            ViewBag.SearchString = search;

            return View();
        }

        // GET: Show
        [Authorize (Roles = "User,Admin"), AllowAnonymous]
        public ActionResult Show(int id)
        {
            Bookmark bookmark = db.Bookmarks.Find(id);
            ViewBag.Bookmark = bookmark;

            string userName = User.Identity.GetUserName();
            ViewBag.userName = userName;

            string userId = "-1";

            string b = "nu";

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                userId = User.Identity.GetUserId();

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated && User.IsInRole("Admin"))
                b = "da";

            ViewBag.userId = userId;
            ViewBag.b = b;

            if (bookmark.Album is not null)
                ViewBag.Album = bookmark.Album;

            return View(bookmark);

        }

        // GET: New
        [Authorize(Roles = "Admin,User")]
        public ActionResult New()
        {
            Bookmark bookmark = new Bookmark();
            bookmark.Alb = GetAllAlbums();
            bookmark.UserId = User.Identity.GetUserId();

            var albums = from alb in db.Albums
                         select alb;
            ViewBag.Albums = albums;

            return View(bookmark);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public ActionResult New(Bookmark bookmark)
        {
            bookmark.UserId = User.Identity.GetUserId();
            bookmark.Date = DateTime.Now;
            bookmark.Rating = 0;

            try
            {
                if (ModelState.IsValid)
                {
                    db.Bookmarks.Add(bookmark);
                    db.SaveChanges();
                    TempData["message"] = "Bookmark-ul a fost adaugat!";
                    return RedirectToAction("Index");
                }
                else
                {
                    bookmark.Alb = GetAllAlbums();
                    return View(bookmark);
                }
            }
            catch (Exception e)
            {
                bookmark.Alb = GetAllAlbums();
                return View(bookmark);
            }
        }

        //GET: Edit
        [Authorize(Roles = "Admin,User")]
        public ActionResult Edit(int id)
        {
            Bookmark bookmark = db.Bookmarks.Find(id);
            /*string g = System.Security.IPrincipal.IIdentity.User.GetUserId();*/
            string userId = User.Identity.GetUserId();
            ViewBag.userId = userId;
            /*if (userId == bookmark.UserId)
            {
                b = true;
            }
            else
            {
                b = false;
                *//*TempData["message"] = "Acest bookmark nu a fost postat de tine. Nu ai autorizatia de a il modifica!";
                return RedirectToAction("Index");*//*
            }*/
            ViewBag.Bookmark = bookmark;
            bookmark.Alb = GetAllAlbums();
            return View(bookmark);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPut]
        public ActionResult Edit(int id, Bookmark requestBookmark)
        {
            requestBookmark.Alb = GetAllAlbums();

            try
            {
                if(ModelState.IsValid)
                {
                    Bookmark bookmark = db.Bookmarks.Find(id);

                    if (TryUpdateModel(bookmark))
                    {
                        bookmark.Title = requestBookmark.Title;
                        bookmark.Description = requestBookmark.Description;
                        // Daca se schimba content-ul, atunci rating-ul trebuie resetat la 0.
                        // Sau permitem doar modificarea descrierii ca sa evitam toata chestia asta.
                        // iar albumul nu poate fi schimbat de aici, ca un bookmark poate fi in mai multe albume. O sa facem din AlbumController sa putem elimina/adauga un bookmark. Va fi optiunea utilizatorului - Iulia
                        //ssau poate facem si de aici.. sa apara lista albumelor in care a fost adaugat si sa putem debifa unele albume, daca ar fi sa i dam adminului permisiunea asta dar nush mai vedem, ne mai uitam pe ce zice in cerinte.

                        //ramane de facut pt Content cu imagini.
                        bookmark.Content = requestBookmark.Content;
                        bookmark.Date = DateTime.Now;
                        // Daca vreau sa actualizez data la realizarea modificarii bookmark-ului.

                        db.SaveChanges();
                        TempData["message"] = "Bookmark-ul a fost modificat!";
                    }
                    return RedirectToAction("Index");
                }
                else
                { 
                    return View(requestBookmark);
                }
            }
            catch (Exception e)
            {
                return View(requestBookmark);
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Bookmark bookmark = db.Bookmarks.Find(id);
            db.Bookmarks.Remove(bookmark);
            
            foreach (var i in db.SavedBookmarks)
            {
                if (i.BookmarkId == id)
                    db.SavedBookmarks.Remove(i);
            }

            db.SaveChanges();
            
            TempData["message"] = "Bookmark-ul a fost sters!";
            
            return RedirectToAction("Index");
        }


        [NonAction]
        public IEnumerable<SelectListItem> GetAllAlbums()
        {
            {
                // generam o lista goala
                var selectList = new List<SelectListItem>();
                // extragem toate albumele din baza de date
                var albums = from alb in db.Albums
                             select alb;
                // iteram prin albume
                foreach (var album in albums)
                {
                    // adaugam in lista elementele necesare pentru dropdown
                    selectList.Add(new SelectListItem
                    {
                        Value = album.AlbumId.ToString(),
                        Text = album.AlbumTitle.ToString()
                    });
                }
                // returnam lista de albume
                return selectList;
            }
        }
    }
}
