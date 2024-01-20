
using System.Collections.Generic;
using System.Linq;

namespace BookWebStore.Models
{
    public class BookRepository : IRepository<Book>
    {
        List<Book> books;

        public BookRepository()
        {

            books = new List<Book>()
            {
                new Book
                {
                    Id = 1,
                    Title = "HOMS",
                    ShortDescription="short",
                    LongDescription="long",
                    Author=new Author{Id=1},

                    ImageUrl="image.png",
                    
                        


                },
                new Book
                {
                    Id = 2,
                    Title = "HAMA",
                     ShortDescription="short",
                    LongDescription="long",
                       Author=new Author{Id = 2}
                }
            };
        }

        public void  Add(Book item)
        {
            item.Id = books.Max(b => b.Id) + 1;
            books.Add(item);
        }

        public void  Delete(int id)
        {
            var book = books.SingleOrDefault(b => b.Id == id);
            books.Remove(book);
        }

  


       public IList<Book> GetAll()
        {
            return books; 
        }

        public  Book Find(int id)
        {
            var book = books.SingleOrDefault(b => b.Id == id);
            return book; 
        }

        public void Edit(int id , Book item)
        {
            throw new System.NotImplementedException();
        }

        

        public void Edit(Book item)
        {
            throw new System.NotImplementedException();
        }

        public List<Book> Search(string term)
        {
            return books.Where(a => a.Title.Contains(term)).ToList();
        }
    }
}
    

