using BookStore.DAL;
using BookStore.DAL.Entities;
using BookStore.Repositories.Interfaces;

namespace BookStore.Repositories
{
    public class UserBookRepository : RepositoryBase<UserBook>, IUserBookRepository
    {
        public UserBookRepository(ContextDB contextDB) : base(contextDB)
        {
        }
    }
}
