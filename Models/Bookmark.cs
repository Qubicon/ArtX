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

        public int Rating { get; set; } //nr-ul de persoane care l au salvat
        public DateTime Date { get; set; }
        [RegularExpression(@"((\#[a-z0-9]+\s))*", ErrorMessage = "Dupa tag este necesar un spatiu si nu pot fi introduse tag-uri goale (corect:#catel #acasa21 #pisica , gresit:#catel#pisica, gresit: # ")]
        public string Tags { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Albumul este obligatoriu")]
        public int AlbumId { get; set; } // FK

        public virtual Album Album { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public IEnumerable<SelectListItem> Alb { get; set; }
    }
}