using BookStore.DAL.Entities;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;
using BookStore.Models;

namespace BookStore.Services
{
    public class BookService : IBookService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public BookService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
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


        public async Task EditBook(Book book)
        {
            var result = 0;

            var bookExist = _repositoryWrapper.BookRepository.FindByCondition(i => i.BookID == book.BookID).Any();
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

            return books;
        }

        public List<AssignedGenreData> GetBookAssignedGenres(Book book)
        {
            var bookGenres = new HashSet<Genres>(book.Genres?.Select(c => c.Genre));
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

        public async Task UpdateBookGenres(string[] selectedGenres, int bookId)
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

        public async Task DeleteBook(int bookId)
        {
            var book = _repositoryWrapper.BookRepository.FindByCondition(i => i.BookID == bookId).FirstOrDefault();

            if (book == null)
            {
                return;
            }

            _repositoryWrapper.BookRepository.Delete(book);

            await _repositoryWrapper.SaveAsync();
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

        #endregion
    }
}
