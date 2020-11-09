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

        // Ce are legatura cu userii vom implementa mai tarziu

        // public int UserId { get; set; } // FK


        public virtual ICollection<Bookmark> Bookmarks {get; set;}

    }
}