﻿using ArtX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtX.Controllers
{
    public class CommentsController : Controller
    {
        private ArtX.Models.AppContext db = new ArtX.Models.AppContext();
        // GET: Comments
        public ActionResult Index()
        {
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Comment comm = db.Comments.Find(id);
            db.Comments.Remove(comm);
            db.SaveChanges();
            return Redirect("/Bookmarks/Show/" + comm.BookmarkId);
        }

        [HttpPost]
        public ActionResult New(Comment comm)
        {
            comm.Date = DateTime.Now;
            try
            {
                db.Comments.Add(comm);
                db.SaveChanges();
                return Redirect("/Bookmarks/Show/" + comm.BookmarkId);
            }

            catch (Exception e)
            {
                return Redirect("/Bookmarks/Show/" + comm.BookmarkId);
            }

        }

        public ActionResult Edit(int id)
        {
            Comment comm = db.Comments.Find(id);
            ViewBag.Comment = comm;
            return View();
        }

        [HttpPut]
        public ActionResult Edit(int id, Comment requestComment)
        {
            try
            {
                Comment comm = db.Comments.Find(id);
                if (TryUpdateModel(comm))
                {
                    comm.Content = requestComment.Content;
                    db.SaveChanges();
                }
                return Redirect("/Bookmarks/Show/" + comm.BookmarkId);
            }
            catch (Exception e)
            {
                return View();
            }

        }
    }
}