using BookStore.DAL.Entities;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;
using BookStore.Models;

namespace BookStore.Services
{
    public class BookService : IBookService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IUserService _userService;

        public BookService(IRepositoryWrapper repositoryWrapper, IUserService userService)
        {
            _repositoryWrapper = repositoryWrapper;
            _userService = userService;
        }

        public async Task<int> AddBookAsync(Book book)
        {
            var result = 0;
            if (book != null)
            {
                _repositoryWrapper.BookRepository.Create(book);
                await _repositoryWrapper.SaveAsync();
                result = book.BookID;
            }
            return result;
        }


        public async Task EditBookAsync(Book book)
        {
            var result = 0;

            var bookExist = _repositoryWrapper.BookRepository.FindByCondition(i => i.BookID == book.BookID).Any();
            var bookDetailsExist = _repositoryWrapper.BookDetailsRepository.FindByCondition(i => i.BookID == book.BookID).Any();
            if (bookDetailsExist)
            {
                _repositoryWrapper.BookDetailsRepository.Update(book.Details);
            }
            else
            {
                _repositoryWrapper.BookDetailsRepository.Create(book.Details);

            }
            if (bookExist)
            {
                _repositoryWrapper.BookRepository.Update(book);
            }

            await _repositoryWrapper.SaveAsync();
        }

        public Book GetBookModelForEdit(int id)
        {
            var book = _repositoryWrapper.BookRepository.FindByCondition(i => i.BookID == id).FirstOrDefault();

            return book;
        }

        public Book GetBookWithReviews(int id)
        {
            var book = _repositoryWrapper.BookRepository.FindByCondition(i => i.BookID == id).FirstOrDefault();
            if (book != null)
            {
                var reviews = _repositoryWrapper.ReviewRepository.FindByCondition(item => item.BookID == id).ToList();
                book.Reviews = reviews;
            }
            return book;
        }

        public List<Book> GetAllBooks()
        {
            var books = _repositoryWrapper.BookRepository.FindAll().ToList();
            return books;
        }

        public List<Book> GetFilteredBooks(string prefSearch, string prefGenre)
        {
            var books = new List<Book>();

            if (string.IsNullOrWhiteSpace(prefSearch))
            {
                books = GetAllBooks();
            }
            else
            {
                books = _repositoryWrapper.BookRepository.FindByCondition(i => i.Title.Contains(prefSearch)
                                                                                           || i.ShortDescription.Contains(prefSearch)
                                                                                           || i.Details.LongDescription.Contains(prefSearch)
                                                                                           || i.BookAuthors.Any(a => a.Author.Name.Contains(prefSearch))
                                                                                         ).ToList();
            }

            if (!string.IsNullOrWhiteSpace(prefGenre))
            {
                var parsed = Enum.TryParse(prefGenre, out Genres genre);
                if (parsed)
                {
                    var bookGenres = _repositoryWrapper.BookGenresRepository.FindByCondition(i => i.Genre == genre).ToList();

                    books = books.Where(book => bookGenres.Any(b => b.BookID == book.BookID)).ToList();
                }
            }

            foreach (var book in books)
            {
                if (_userService.UserHasBook(book.BookID))
                {
                    book.UserReadBook = true;
                }

                if (_userService.UserHasBook(book.BookID))
                {
                    book.UserReadBook = true;
                }
            }

            return books;
        }

        public List<AssignedGenreData> GetBookAssignedGenres(Book book)
        {
            var bookGenres = new HashSet<Genres>(book.Genres?.Select(c => c.Genre) ?? new HashSet<Genres>());
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

            return viewModel;
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _repositoryWrapper.AuthorRepository.FindAll().ToList();
        }
        public async Task UpdateBookAsync(string[] selectedGenres, Book book)
        {
            var selectedAuthors = book.SelectedAuthorsIDArray;

            await EditBookAsync(book);

            if (selectedGenres != null)
            {
                await UpdateBookGenresAsync(selectedGenres, book.BookID);
            }


            if (selectedAuthors != null)
            {
                await UpdateBookAuthors(selectedAuthors, book.BookID);
            }


            await _repositoryWrapper.SaveAsync();
        }

        public async Task UpdateBookGenresAsync(string[] selectedGenres, int bookId)
        {
            if (selectedGenres == null)
            {
                return;
            }

            var bookGenres = _repositoryWrapper.BookGenresRepository.FindByCondition(i => i.BookID == bookId).ToList();
            var genres = bookGenres.Select(item => item.Genre.ToString()).ToList();
            var toAdd = selectedGenres.Where(i => !genres.Contains(i)).ToList();
            var toRemove = genres.Where(i => !selectedGenres.Contains(i)).ToList();

            AddBookGenres(toAdd, bookId);
            DeleteBookGenres(toRemove, bookGenres);

            await _repositoryWrapper.SaveAsync();
        }
        public async Task UpdateBookAuthors(int[] selectedAuthors, int bookId)
        {
            if (selectedAuthors == null)
            {
                return;
            }

            var bookAuthors = _repositoryWrapper.BookAuthorRepository.FindByCondition(i => i.BookID == bookId).ToList();

            AddBookAuthors(selectedAuthors, bookId, bookAuthors);
            DeleteBookAuthors(selectedAuthors, bookAuthors);

            await _repositoryWrapper.SaveAsync();
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var book = _repositoryWrapper.BookRepository.FindByCondition(i => i.BookID == bookId).FirstOrDefault();

            if (book == null)
            {
                return;
            }

            _repositoryWrapper.BookRepository.Delete(book);

            await _repositoryWrapper.SaveAsync();
        }


        public bool BookExists(int id)
        {
            return _repositoryWrapper.BookRepository.FindByCondition(e => e.BookID == id).Any();
        }

        #region Private 

        private void AddBookGenres(List<string> toAdd, int bookId)
        {
            foreach (var add in toAdd)
            {
                var parsed = Enum.TryParse(add, out Genres genre);
                if (parsed)
                {
                    _repositoryWrapper.BookGenresRepository.Create(new BookGenre
                    {
                        BookID = bookId,
                        Genre = genre
                    });
                }
            }
        }

        private void DeleteBookGenres(List<string> toRemove, List<BookGenre> bookGenres)
        {
            foreach (var remove in toRemove)
            {
                var parsed = Enum.TryParse(remove, out Genres genre);
                if (parsed)
                {
                    var bookGenreToRemove = bookGenres.FirstOrDefault(i => i.Genre == genre);
                    _repositoryWrapper.BookGenresRepository.Delete(bookGenreToRemove);
                }
            }
        }

        private void AddBookAuthors(int[] selectedAuthors, int bookId, List<BookAuthor> bookAuthors)
        {
            var toAdd = selectedAuthors.Where(i => !bookAuthors.Any(item => item.AuthorID == i)).ToList();

            foreach (var add in toAdd)
            {
                _repositoryWrapper.BookAuthorRepository.Create(new BookAuthor
                {
                    BookID = bookId,
                    AuthorID = add,
                });
            }
        }

        private void DeleteBookAuthors(int[] selectedAuthors, List<BookAuthor> bookAuthors)
        {
            var toRemove = bookAuthors.Where(i => !selectedAuthors.Contains(i.AuthorID)).ToList();

            foreach (var remove in toRemove)
            {
                _repositoryWrapper.BookAuthorRepository.Delete(remove);
            }
        }

        #endregion
    }
}
