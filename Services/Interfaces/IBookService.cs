using BookStore.DAL.Entities;

namespace BookStore.Services.Interfaces
{
    public interface IBookService
    {
        public Task<int> AddBookAsync( Book book);
    }
}
