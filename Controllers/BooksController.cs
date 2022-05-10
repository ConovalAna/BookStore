#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.DAL;
using BookStore.DAL.Entities;
using BookStore.Services.Interfaces;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IUserService _userService;

        public BooksController( IBookService bookService, IUserService userService)
        {
            _bookService = bookService;
            _userService = userService;
        }

        // GET: Books
        public async Task<IActionResult> Index(string prefSearch, string prefOrderbyGenre)
        {
            ViewBag.PrefSearch = prefSearch;
            ViewBag.PrefOrderbyGenre = prefOrderbyGenre;
            var books = _bookService.GetFilteredBooks(prefSearch, prefOrderbyGenre);
            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.GetBookWithReviews(id.GetValueOrDefault());

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

        public async Task<IActionResult> GetBook(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            await _userService.AssignBookAsync(id.GetValueOrDefault());
            return RedirectToAction(nameof(Index));
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookID,Title,ImageURL,DownloadLink, ShortDescription")] Book book)
        {
            book.CreatedDate = DateTime.Now;
            book.CreatedBy = 1;

            if (ModelState.IsValid)
            {
                await _bookService.AddBookAsync(book);
                return RedirectToAction(nameof(Edit), new { id = book.BookID });
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

            var book = _bookService.GetBookModelForEdit(id.GetValueOrDefault());
            var viewModel = _bookService.GetBookAssignedGenres(book);
            var authors = _bookService.GetAuthors();

            book.SelectedAuthorsIDArray = book?.BookAuthors?.Select(ba => ba.AuthorID)?.ToArray() ?? Array.Empty<int>();
            ViewBag.ViewGenres = viewModel;
            ViewBag.AuthorsCollection = authors;

            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookID,Title,ImageURL,DownloadLink,ShortDescription,Details,Details.BookID, Details.Date,Details.Description,Details.SBN,Details.Edition, SelectedAuthorsIDArray")] Book book, string[] selectedGenres)
        {
            if (id != book.BookID)
            {
                return NotFound();
            }
            if (book.Details?.BookID == 0) book.Details.BookID = book.BookID;

            if (ModelState.IsValid)
            {
                try
                {
                    await _bookService.UpdateBookAsync(selectedGenres, book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_bookService.BookExists(book.BookID))
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
            var viewModel = _bookService.GetBookAssignedGenres(book);
            var authors = _bookService.GetAuthors();
            ViewBag.ViewGenres = viewModel;
            ViewBag.AuthorsCollection = authors;

            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = _bookService.GetBookModelForEdit(id.GetValueOrDefault());

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

            await _bookService.DeleteBookAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
