using KutuphaneYonetim.Data;
using KutuphaneYonetim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneYonetim.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string search, string sort)
        {
            int maxListCount = 10;

            var books = _context.Books.AsQueryable();

            // Arama
            if (!string.IsNullOrEmpty(search))
            {
                books = books.Where(b =>
                    b.Title.Contains(search) ||
                    b.Author.Contains(search));
            }

            // Sýralama
            books = sort switch
            {
                "title_desc" => books.OrderByDescending(b => b.Title),
                "year" => books.OrderBy(b => b.PublishYear),
                _ => books.OrderBy(b => b.Title)
            };

            ViewBag.TotalBooks = await _context.Books.CountAsync();
            ViewBag.Sort = sort;
            ViewBag.Search = search;

            return View(await books.Take(maxListCount).ToListAsync());
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public IActionResult Edit(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
                return RedirectToAction(nameof(Index));

            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
                return RedirectToAction(nameof(Index));

            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return RedirectToAction(nameof(Index));

            return View(book);
        }


        // POST: Books/Delete/5
        // POST: Books/Delete/5
        // POST: Books/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection form)
        {
            var book = await _context.Books.FindAsync(id);

            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }



    }
}
