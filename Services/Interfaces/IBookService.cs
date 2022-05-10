using BookStore.DAL.Entities;
using BookStore.Models;

namespace BookStore.Services.Interfaces
{
    public interface IBookService
    {
        public Task<int> AddBookAsync(Book book);
        public Task EditBookAsync(Book book);
        public Book GetBookModelForEdit(int id);
        public Book GetBookWithReviews(int id);
        public List<Book> GetAllBooks();
        public List<Book> GetFilteredBooks(string prefSearch, string prefOrderbyGenre);
        public List<AssignedGenreData> GetBookAssignedGenres(Book book);
        public Task UpdateBookGenresAsync(string[] selectedGenres, int bookId);
        public Task UpdateBookAsync(string[] selectedGenres, Book book);
        public Task DeleteBookAsync(int bookId);
        public IEnumerable<Author> GetAuthors();
        public bool BookExists(int id);

    }
}
