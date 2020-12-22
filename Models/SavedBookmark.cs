using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ArtX.Models;

namespace ArtX.Models
{
    public class SavedBookmark
    {
        [Key]
        public int SavedbookmarkId { get; set; }
        public string UserId { get; set; }
        public int BookmarkId { get; set; }
    }
}