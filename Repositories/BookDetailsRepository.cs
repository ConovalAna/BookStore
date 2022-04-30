using BookStore.DAL;
using BookStore.DAL.Entities;
using BookStore.Repositories.Interfaces;

namespace BookStore.Repositories
{
    public class BookDetailsRepository : RepositoryBase<BookDetails>, IBookDetailsRepository
    {
        public BookDetailsRepository(ContextDB contextDB) : base(contextDB)
        {
        }
    }
}
