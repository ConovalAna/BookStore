using BookStore.DAL;
using BookStore.DAL.Entities;
using BookStore.Repositories.Interfaces;

namespace BookStore.Repositories
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(ContextDB contextDB) : base(contextDB)
        {
        }
    }
}
