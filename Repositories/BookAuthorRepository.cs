using BookStore.DAL;
using BookStore.DAL.Entities;
using BookStore.Repositories.Interfaces;

namespace BookStore.Repositories
{
    public class BookAuthorRepository : RepositoryBase<BookAuthor>, IBookAuthorRepository
    {
        public BookAuthorRepository(ContextDB contextDB) : base(contextDB)
        {
        }
    }
}
