using BookStore.DAL.Entities;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;

namespace BookStore.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public UserService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task AssignBookAsync(int bookId)
        {
            var userId = 1;// get current user id
            await AssignBookAsync(bookId, userId);
        }

        public async Task AssignBookAsync(int bookId, int userId)
        {
            var book = _repositoryWrapper.BookRepository.FindByCondition(item => item.BookID == bookId).FirstOrDefault();

            if (book == null)
            {
                return;
            }
            if(UserHasBook(bookId,  userId))
            {
                return;
            }
            _repositoryWrapper.UserBookRepository.Create(new UserBook { UserProfileID = userId, BookID = bookId, DownloadDate=DateTime.Now });

            await _repositoryWrapper.SaveAsync();
        }

        public bool UserHasBook(int bookId)
        {
            var userId = 1;// get current user id
            var hasBook = UserHasBook(bookId, userId);
            return hasBook;
        }

        public bool UserHasBook(int bookId, int userId)
        {
            var hasBook = _repositoryWrapper.UserBookRepository.FindByCondition(item => item.BookID == bookId && item.UserProfileID == userId).Any();
            return hasBook;
        }

        public bool UserAddedReview(int bookId)
        {
            var userId = 1;// get current user id
            var hasReview = UserAddedReview(bookId, userId);
            return hasReview;
        }

        public bool UserAddedReview(int bookId, int userId)
        {
            var hasReview = _repositoryWrapper.ReviewRepository.FindByCondition(item => item.BookID == bookId && item.BookID == userId).Any();
            return hasReview;
        }
    }
}
