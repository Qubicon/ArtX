using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ArtX.Models
{
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }
        [Required]
        public string AlbumTitle { get; set; }
        
        public int UserId { get; set; }     //FK
    }
}