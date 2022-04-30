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
            var book = _bookService.GetBookModelForEdit(id.GetValueOrDefault());

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

            var book = _bookService.GetBookModelForEdit(id.GetValueOrDefault());
            var viewModel = _bookService.GetBookAssignedGenres(book);

            ViewBag.ViewGenres = viewModel;

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
        public async Task<IActionResult> Edit(int id, [Bind("BookID,Title,ImageURL,DownloadLink,ShortDescription,Details,Details.BookID, Details.Date,Details.Description,Details.SBN,Details.Edition")] Book book, string[] selectedGenres)
        {
            if (id != book.BookID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bookService.EditBook(book);
                    await _bookService.UpdateBookGenres(selectedGenres, book.BookID);
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

             await _bookService.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookID == id);
        }
    }
}
