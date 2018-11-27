using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Data.Entity
{
    public class Writer
    {
        [Key]
        public int WriterId { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(200, MinimumLength = 1)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        public virtual ICollection<Book> Books { get; set; }

    }
}