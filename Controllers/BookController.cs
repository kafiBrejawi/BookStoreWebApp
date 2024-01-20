using BookWebStore.Models;
using BookWebStore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace BookWebStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IRepository<Book> bookRepo;
        
        private readonly IRepository<Author> authRepo;
        private readonly IWebHostEnvironment hosting;

        public BookController(IRepository<Book> bookRepo, IRepository<Author> authRepo, IWebHostEnvironment hosting )
        {
            this.bookRepo = bookRepo;
            this.authRepo = authRepo;
            this.hosting = hosting;
        }
        public ActionResult Index()
        {
           var books = bookRepo.GetAll();
            return View(books);
        }

        
        public ActionResult Details(int id)
            
        {
            var book = bookRepo.Find(id);
            return View(book);
        }


        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                Authors = FillSelectList()
        };
            return View(model);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    string FileName = UploadFile(model.File) ?? String.Empty;
                    var author = authRepo.Find(model.AuthorId);
                    Book book = new Book
                    {
                        Title = model.Title,
                        LongDescription = model.LongDescription,
                        ShortDescription = model.ShortDescription,
                        ImageUrl = FileName,
                        Author = author
                        
                    

                    };
                    
                    bookRepo.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            var vmodel = new BookAuthorViewModel { Authors = FillSelectList() };
            ModelState.AddModelError("", "correct your insert");
            return View(vmodel);
           
        }

    
        public ActionResult Edit(int id)
        {
            var book = bookRepo.Find( id);
            var authorId = book.Author == null ? 0 : book.Author.Id;
            var modelview = new BookAuthorViewModel
            {
                BookId = book?.Id ?? 0,
                Title = book?.Title ?? "No Title",
                LongDescription = book?.LongDescription ?? "No Long Description",
                ShortDescription = book?.ShortDescription ?? "No Short Description",
                AuthorId = authorId,
                Authors = authRepo.GetAll().ToList(),
                ImageUrl = book.ImageUrl,
            };


            return View(modelview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
          public ActionResult Edit( int id ,BookAuthorViewModel model)
        {
            try
            {
                string FileName = UploadFile(model.File, model.ImageUrl) ?? string.Empty;

                if (model.AuthorId == -1)
                {
                    ViewBag.Message = "please insert authors";
                    var vmodel = new BookAuthorViewModel
                    {
                        Authors = FillSelectList()
                    };
                    return View(vmodel);
                }
                var auhtor = authRepo.Find(model.AuthorId);
                Book book = new Book    
                {
                    Id = model.BookId,
                    Title = model.Title,
                    LongDescription = model.LongDescription,
                    ShortDescription = model.ShortDescription,
                    Author= auhtor,
                    ImageUrl=FileName,
                    

                };
                bookRepo.Edit(book);
             
                return RedirectToAction(nameof(Create));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                model.Authors = FillSelectList();
                return View(model);
            }
        }


        public ActionResult Delete(int id)
        {
            var book= bookRepo.Find(id);

            return View(book);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete1(int id)
        {
            try

            {

                bookRepo.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
      
        List<Author> FillSelectList()
        {
            var authors = authRepo.GetAll().ToList();
            authors.Insert(0, new Author { Id = -1, FullName = "---Please Select a Author" });
            return authors;
        }
        public string UploadFile(IFormFile file,string ImageUrl)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");

                string newPath = Path.Combine(uploads, file.FileName);

                string oldpath = Path.Combine(uploads, ImageUrl);

                if (newPath != oldpath)
                {
                    System.IO.File.Delete(oldpath);

                    //save new file
                    file.CopyTo(new FileStream(newPath, FileMode.Create));
                }

                return file.FileName;
            }
            return ImageUrl;

        }
        public string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");

                string FullPath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(FullPath, FileMode.Create));
                return file.FileName;
            }
            return null;

        }
    
    
    public ActionResult Search(string term)
        {
            var result=bookRepo.Search(term);
            return View("Index",result);
        }
    }
   
}
