using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
<<<<<<< Updated upstream
//using System.Data.Entity;
=======
using System.Data.Entity;
using System.Drawing;
using System.Dynamic;
>>>>>>> Stashed changes
using System.Linq;
using System.Web;

namespace ArtX.Models
{
    public class Bookmark
    {
        [Key]
        public int BookmarkId { get; set; }
        [Required]
<<<<<<< Updated upstream
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

=======
        public string Title { get; set; }
        public string Description { get; set; }
        // [Required] Il vom schimba mai tarziu (trebuie neaparat sa vedem cum facem upload la imagini)
        public string Content { get; set; } // Momentan afisez ca string
        public int Rating { get; set; } // (Asta cumva ar trebui sa fie initializat cu 0 din db - momentan il afisam ca number) Vreau sa-l fac cu stelute, dar va fi mai complicat si nu stiu daca avem timp pentru asta
        public DateTime Date { get; set; }


        public int AlbumId { get; set; } // FK



        public virtual Album Album { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }



    }
>>>>>>> Stashed changes
}