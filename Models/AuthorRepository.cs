using System.Collections.Generic;
using System.Linq;

namespace BookWebStore.Models
{
    public class AuthorDbRepository : IRepository<Author>
    {
        List<Author> authors;
        public AuthorDbRepository()
        {
            authors = new List<Author>()
            {
                new Author
                {
                    Id = 1,
                    FullName = "abd alkafi ",

                },
                new Author
                {
                    Id = 2,
                    FullName = "alaa hallabo",

                }
            };
        }

        public void Add(Author item)
        {

            authors.Add(item);
        }

        public void Delete(int id)
        {
            var author = authors.SingleOrDefault(b => b.Id == id);
            authors.Remove(author);
        }



        public List<Author> GetAll()
        {
            return authors;
        }

        public Author Find(int id)
        {
            var author = authors.SingleOrDefault(b => b.Id == id);
            return author;
        }

        IList<Author> IRepository<Author>.GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Edit(Author item)
        {
            throw new System.NotImplementedException();
        }

        public List<Author> Search(string term)
        {
            return authors.Where(a=>a.FullName.Contains(term)).ToList();
        }
    }
}

