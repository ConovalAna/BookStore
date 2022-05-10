namespace BookStore.Services.Interfaces
{
    public interface IUserService
    {
        public Task AssignBookAsync(int bookId);
        public Task AssignBookAsync(int bookId, int userId);
        public bool UserHasBook(int bookId);
        public bool UserHasBook(int bookId, int userId);
        public bool UserAddedReview(int bookId);
        public bool UserAddedReview(int bookId, int userId);

    }
}
