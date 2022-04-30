using BookStore.DAL;
using BookStore.DAL.Entities;
using BookStore.Repositories.Interfaces;

namespace BookStore.Repositories
{
    public class BookGenresRepository : RepositoryBase<BookGenre>, IBookGenresRepository
    {
        public BookGenresRepository(ContextDB contextDB) : base(contextDB)
        {
        }
    }
}
