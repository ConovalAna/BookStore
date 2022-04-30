using BookStore.DAL;
using BookStore.DAL.Entities;
using BookStore.Repositories.Interfaces;

namespace BookStore.Repositories
{
    public class UserWaintingBookRepository : RepositoryBase<UserWaitingBook>, IUserWaintingBookRepository
    {
        public UserWaintingBookRepository(ContextDB contextDB) : base(contextDB)
        {
        }
    }
}
