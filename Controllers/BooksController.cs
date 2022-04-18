#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.DAL;
using BookStore.DAL.Entities;
using BookStore.Services.Interfaces;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly ContextDB _context;
        private readonly IBookService _bookService;

        public BooksController(ContextDB context, IBookService bookService)
        {
            _context = context;
            _bookService = bookService;
        }

        // GET: Books
        public async Task<IActionResult> Index(string prefSearch,string prefOrderbyGenre)
        {
            ViewBag.PrefSearch = prefSearch;
            ViewBag.PrefOrderbyGenre = prefOrderbyGenre;
            return View(await _context.Books.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Reviews)
                .Include(b => b.Details)
                .Include(b => b.Genres)
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookID,Title,ImageURL,DownloadLink,Edition,CreatedDate,CreatedBy")] Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookService.AddBookAsync(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var book = await _context.Books
                .Include(b => b.Details)
                .Include(b => b.Genres)
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(m => m.BookID == id);
            await _bookService.AddBookAsync(book);

            PopulateAssignedCourseData(book);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        private void PopulateAssignedCourseData(Book book)
        {
            var bookGenres = new HashSet<Genres>(book.Genres.Select(c => c.Genre));
            var viewModel = new List<AssignedGenreData>();
            var genres = Enum.GetValues(typeof(Genres)).OfType<Genres>().ToList();

            foreach (var genre in genres)
            {
                viewModel.Add(new AssignedGenreData
                {
                    Genre = genre,
                    Assigned = bookGenres.Contains(genre)
                });
            }
            ViewBag.ViewGenres = viewModel;
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookID,Title,ImageURL,DownloadLink,ShortDescription,Details,Details.BookID, Details.Date,Details.Description,Details.SBN,Details.Edition")] Book book, string[] selectedGenres)
        {
            if (id != book.BookID)
            {
                return NotFound();
            }

            var bookGenres = await _context.BookGenres.Where(bg => bg.BookID == id).ToListAsync();
            


            if (ModelState.IsValid)
            {
                try
                {
                    await UpdateInstructorCourses(selectedGenres, bookGenres, id);
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        private async Task UpdateInstructorCourses(string[] selectedGenres, List<BookGenre> bookGenresList, int bookId)
        {
            if (selectedGenres == null)
            {
               
                return;
            }
            var bookGenres = bookGenresList.Select(i => i.Genre.ToString()).ToList();
            var toAdd = selectedGenres.Where(i => !bookGenres.Contains(i)).ToList();
            var toRemove = bookGenres.Where(i => !selectedGenres.Contains(i)).ToList();

            foreach (var add in toAdd)
            {
                var parsed = Enum.TryParse(add, out Genres genre);
                if (parsed)
                {
                    _context.BookGenres.Add(new BookGenre
                    {
                        BookID = bookId,
                        Genre = genre
                    });
                }
            }

            foreach (var remove in toRemove)
            {
                var parsed = Enum.TryParse(remove, out Genres genre);
                if (parsed)
                {
                    var bookGenre = bookGenresList.FirstOrDefault(g => g.Genre == genre);
                    _context.BookGenres.Remove(bookGenre);
                }
            }
            await _context.SaveChangesAsync();

        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookID == id);
        }
    }
}
