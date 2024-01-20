using BookWebStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AuthorController : Controller
    {
        private  readonly IRepository<Author> _authorRepository;
		
        public AuthorController(IRepository<Author> repository)
        {
            this._authorRepository = repository;
        }
      
        public IActionResult Index()
        {
            var authors = _authorRepository.GetAll();
            return View(authors);
        }
       
        public ActionResult Details(int id)
        {
            var author = _authorRepository.Find(id);
            return View(author);
            
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
           
                try
                {

                _authorRepository.Add(author);

                return RedirectToAction(nameof(Index));
				

                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred: " + ex.Message);
                }
            

            return View();
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {
            var author=_authorRepository.Find(id);  

            return View(author);
        }
        [HttpGet]
        public ActionResult Privacy()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Author author)
        {
            try
            {
                _authorRepository.Edit(author);

                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred: " + ex.Message);
                
            }
           return View();
           
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var author = _authorRepository.Find(id);
            return View(author);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author author)
        {
            try
            {
                _authorRepository.Delete(id);
                

               

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
