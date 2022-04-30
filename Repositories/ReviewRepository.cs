using BookStore.DAL;
using BookStore.DAL.Entities;
using BookStore.Repositories.Interfaces;

namespace BookStore.Repositories
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(ContextDB contextDB) : base(contextDB)
        {
        }
    }
}
