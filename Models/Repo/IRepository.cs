    using System.Collections.Generic;

namespace BookWebStore.Models
{
    public interface IRepository<T>
    {
      public  IList<T> GetAll();
      public  T Find(int id);
      public  void Add(T item);
      public void Edit(T item);
      public  void Delete(int id);
      public List<T> Search(string term);
    }
}
