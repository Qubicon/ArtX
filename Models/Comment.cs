﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtX.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public int BookmarkId { get; set; } // FK
        public virtual Bookmark Bookmark { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}