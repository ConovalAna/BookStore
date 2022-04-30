using BookStore.DAL.Entities;
using BookStore.Models;

namespace BookStore.Services.Interfaces
{
    public interface IBookService
    {
        public Task<int> AddBookAsync(Book book);
        public Task EditBook(Book book);
        public Book GetBookModelForEdit(int id);
        public List<Book> GetAllBooks();
        public List<Book> GetFilteredBooks(string prefSearch, string prefOrderbyGenre);
        public List<AssignedGenreData> GetBookAssignedGenres(Book book);
        public Task UpdateBookGenres(string[] selectedGenres, int bookId);
        public Task DeleteBook(int bookId);
    }
}
