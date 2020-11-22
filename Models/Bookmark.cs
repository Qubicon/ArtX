using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtX.Models
{
    public class Bookmark
    {
        [Key]
        public int BookmarkId { get; set; }

        [Required(ErrorMessage = "Titlul este obligatoriu")]
        [StringLength(100, ErrorMessage = "Titlul nu poate avea mai mult de 20 caractere")]
        public string Title { get; set; }
        public string Description { get; set; }

        // [Required] Il vom schimba mai tarziu (trebuie neaparat sa vedem cum facem upload la imagini)
        [Required(ErrorMessage = "Continutul bookmark-ului este obligatoriu")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; } // Momentan afisez ca string

        public int Rating { get; set; } // (Asta cumva ar trebui sa fie initializat cu 0 din db - momentan il afisam ca number) Vreau sa-l fac cu stelute, dar va fi mai complicat si nu stiu daca avem timp pentru asta
        public DateTime Date { get; set; }



        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }


        [Required(ErrorMessage = "Albumul este obligatoriu")]
        public int AlbumId { get; set; } // FK

        public virtual Album Album { get; set; }    //De tinut minte: pe asta va trebui sa il facem tot o lista. Un bookmark poate fi regasit in mai multe albume
       // public virtual ICollection<Album> Albums { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public IEnumerable<SelectListItem> Alb { get; set; }

    }
}