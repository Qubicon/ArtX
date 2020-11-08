using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ArtX.Models
{
    public class Bookmark
    {
        [Key]
        public int BookmarkId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        //  [Required] il facem required cand o sa stim cum sa luam poze :))
        public Image Content { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }

        public int AlbumId { get; set; }

    }

}