using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtX.Controllers
{
    public class HomeController : Controller
    {
        private ArtX.Models.ApplicationDbContext db = new ArtX.Models.ApplicationDbContext();

        private int _perPage = 1;

        public static int last1 = 1;
        public static int last2 = 1;

        public ActionResult Index()
        {
            var bookmarks1 = db.Bookmarks.Include("Album").Include("User").OrderByDescending(o => o.Date); //default
            var bookmarks2 = db.Bookmarks.Include("Album").Include("User").OrderByDescending(o => o.Rating); //default

            ViewBag.bookmarks1 = bookmarks1;
            ViewBag.bookmarks2 = bookmarks2;

            string userId = "-1";
            string b = "nu";


            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                userId = User.Identity.GetUserId();

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated && User.IsInRole("Admin"))
                b = "da";

            ViewBag.userId = userId;
            ViewBag.b = b;

            var page = Convert.ToInt32(Request.Params.Get("page"));
            var currentPage = page; //aici ma obliga sa le initializez cu cv dar le modific in if uri
            var currentPage2 = page;

            if (page < 20000)
            {
                currentPage = page%10000;
                currentPage2 = last2;
                           last1 = page%10000;
            }
            else
            {
                currentPage = last1;
                currentPage2 = page % 20000;
                last2 = page % 20000;
            }
            // PAGINATION 1

            var totalItems = bookmarks1.Count();

            var offset = 0;

            if (!currentPage.Equals(0))
            {
                offset = (currentPage - 1) * this._perPage;
            }

            var paginatedBookmarks = bookmarks1.Skip(offset).Take(this._perPage);

            // ViewBag.perPage = this._perPage;

            ViewBag.total = totalItems;
            ViewBag.lastPage = Math.Ceiling((float)totalItems / (float)this._perPage);
            ViewBag.Bookmarks1 = paginatedBookmarks;


            // PAGINATION 2

            var totalItems2 = bookmarks2.Count();

            var offset2 = 0;

            if (!currentPage2.Equals(0))
            {
                offset2 = (currentPage2 - 1) * this._perPage;
            }

            var paginatedBookmarks2 = bookmarks2.Skip(offset2).Take(this._perPage);

            // ViewBag.perPage = this._perPage;

            ViewBag.total2 = totalItems2;
            ViewBag.lastPage2 = Math.Ceiling((float)totalItems2 / (float)this._perPage);
            ViewBag.Bookmarks2 = paginatedBookmarks2;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}