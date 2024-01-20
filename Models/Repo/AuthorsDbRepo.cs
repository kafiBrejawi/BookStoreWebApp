using System.Collections.Generic;
using System.Linq;

namespace BookWebStore.Models
{
    public class AuthorDbRepos : IRepository<Author>
    {

        BookstoreDbContext db;
        public AuthorDbRepos(BookstoreDbContext _db )
        {
            db = _db;
            
        }

        public void Add(Author item)
        {

            db.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var author = db.Authors.SingleOrDefault(b => b.Id == id);
            db.Authors.Remove(author);
            db.SaveChanges();
        }

        public void Edit(  Author newitem)
        {

            db.Update(newitem);
            db.SaveChanges();

        }

        public IList<Author> GetAll()
        {
            return db.Authors.ToList();
        }

        public Author Find(int id)
        {
            var author = db.Authors.SingleOrDefault(b => b.Id == id);
          
            return author;
           
        }

        public List<Author> Search(string term)
        {
           
            return db.Authors.Where(a=>a.FullName.Contains(term)).ToList();
        }
    }
}

