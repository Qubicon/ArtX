using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtX.Models
{
    public class User_Bookmark
    {
        public int UserId { get; set; }     //FK
        public int BookmarkId { get; set; } //FK
    }
}