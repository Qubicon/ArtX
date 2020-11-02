using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtX.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        public string TagContent { get; set; }
    }
}