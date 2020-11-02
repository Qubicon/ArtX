using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArtX.Models
{
    public class Bookmark_Tag
    {
        public int BookmarkId { get; set; } //FK
        public int TagId { get; set; }  //FK
    }
}