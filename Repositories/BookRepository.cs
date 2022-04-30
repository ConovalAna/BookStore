using BookStore.DAL;
using BookStore.DAL.Entities;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStore.Repositories
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(ContextDB contextDB) : base(contextDB)
        {
        }

        public override IQueryable<Book> FindByCondition(Expression<Func<Book, bool>> expression)
        {
            var books = ContextDB.Set<Book>()
                .Include(b => b.Details)
                .Include(b => b.Genres)
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Where(expression).AsNoTracking();
            return books;
        }
    }
}