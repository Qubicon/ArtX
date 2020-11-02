using System;
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
        public string CommentContent { get; set; }

        public int UserId { get; set; }     //FK
        public int BookmarkId { get; set; } //FK
    }
}