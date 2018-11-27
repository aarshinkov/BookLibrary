using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Data.Entity
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 1)]
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Writer")]
        public int? WriterId { get; set; }

        public Writer Writer { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int? GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        [StringLength(500, MinimumLength = 1)]
        public string Description { get; set; }

    }
}