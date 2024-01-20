
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BookWebStore.Models
{
    public class BookDbRepo : IRepository<Book>
    {
        BookstoreDbContext db;

        public BookDbRepo(BookstoreDbContext _db)
        {

            db = _db;
            
        }

        public void Add(Book item)
        {
            
           db.Books.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = db.Books.SingleOrDefault(b => b.Id == id);
            db.Books.Remove(book);
            db.SaveChanges();
        }

        public void Edit(Book Newitem)
        {
            db.Update(Newitem);
            db.SaveChanges();

        }


        public IList<Book> GetAll()
        {
            return db.Books.Include(a => a.Author).ToList();
        }

        public Book Find(int id)
        {
            var book = db.Books.Include(a => a.Author).SingleOrDefault(b => b.Id == id);
            return book;
        }
        public List <Book> Search(string term)
        {
            var result = db.Books.Include(a => a.Author).Where(b => b.Title.Contains(term) 
            || b.ShortDescription.Contains(term) 
            || b.LongDescription.Contains(term) 
            || b.Author.FullName.Contains(term)).ToList();
            
            return result;
        }

    }
}


