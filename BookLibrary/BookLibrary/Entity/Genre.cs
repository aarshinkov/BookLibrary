using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookLibrary.Entity
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Genre")]
        public string GenreName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}