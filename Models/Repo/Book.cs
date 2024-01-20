using System.ComponentModel.DataAnnotations;

namespace BookWebStore.Models
{

    public class Book
    {
         public int Id { get; set; }
         public string  Title { get; set; }
       
          public string ShortDescription { get; set; }
         public string LongDescription { get; set; }
        public string ImageUrl { get; set; }
        public Author Author { get; set; }
    }
}
