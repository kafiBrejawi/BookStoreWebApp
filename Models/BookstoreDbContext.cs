
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;
namespace BookWebStore.Models
{
    public class BookstoreDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BookstoreDbContext( DbContextOptions<BookstoreDbContext> options) : base(options)
        {
            
        }
        public Microsoft.EntityFrameworkCore.DbSet<Author> Authors { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Book> Books { get; set; }
    }
}
