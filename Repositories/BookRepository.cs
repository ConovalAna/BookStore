using BookStore.DAL;
using BookStore.DAL.Entities;
using BookStore.Repositories.Interfaces;
using System.Linq.Expressions;

namespace BookStore.Repositories
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(ContextDB contextDB) : base(contextDB)
        {
        }
    }
}