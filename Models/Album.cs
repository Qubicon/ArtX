using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtX.Models
{
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }
        [Required]
        public string AlbumTitle { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Bookmark> Bookmarks { get; set; }

    }
}