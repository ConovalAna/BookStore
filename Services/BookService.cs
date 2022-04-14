using BookStore.DAL.Entities;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;

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
            if( book != null)
            {
                _repositoryWrapper.BookRepository.Create(book);
                await _repositoryWrapper.SaveAsync();
                result = book.BookID;
            }
            return result;
        }
    }
}
