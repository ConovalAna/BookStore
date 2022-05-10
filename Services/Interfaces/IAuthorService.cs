using BookStore.DAL.Entities;

namespace BookStore.Services.Interfaces
{
    public interface IAuthorService
    {
        public Task<int> AddAuthorAsync(Author author);
        public Task EditAuthor(Author Author);
        public Author GetAuthor(int id);
        public List<Author> GetAllAuthors();
        public List<Author> GetFilteredAuthors(string prefSearch);
        public Task DeleteAuthor(int AuthorId);
        public List<Author> GetAuthors();
        public bool AuthorExists(int id);
    }
}
