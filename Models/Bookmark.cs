using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.Data.Entity;
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
        public string BookmarkTitle { get; set; }
        [Required]
        public Image BookmarkContent { get; set; }
        public int BookmarkRating { get; set; }
        public DateTime BookmarkDate { get; set; }  //p asta chiar il punem ? // DA

        public int AlbumId { get; set; } //are acelasi nume ca si cheia din Album.cs
                                         //astfel va vedea calculatorul ca este FK (cheie externa)
                                         //d aia le am dat denumirile astea lungi, ca sa diferentieze AlbumId de BookmarkId de exemplu

       // public virtual Album Album { get; set; }
      //  public virtual ICollection<Comment> Comments { get; set; }
    }






    // >> Pentru mai incolo.

    /*public class BookmarkDBContext : DbContext
    {
        public BookmarkDBContext() : base("DBConnectionString") { }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }*/

}