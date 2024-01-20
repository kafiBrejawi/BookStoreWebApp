using BookWebStore.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookWebStore.ViewModels
{
    public class BookAuthorViewModel
    {


        public int BookId { get; set; }

        [Required]
        [MaxLength(10)]
        [MinLength(5)]
        public string Title { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string LongDescription { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string ShortDescription { get; set; }
        public int AuthorId { get; set; }
        public List<Author> Authors { get; set; }

        public Microsoft.AspNetCore.Http.IFormFile File { get; set; }
        public string  ImageUrl { get; set; }
    }
}

