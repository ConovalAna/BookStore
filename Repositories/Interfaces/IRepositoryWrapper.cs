namespace BookStore.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        IBookRepository BookRepository { get; }
        IBookGenresRepository BookGenresRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        IBookDetailsRepository BookDetailsRepository { get; }
        IBookAuthorRepository BookAuthorRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IUserBookRepository UserBookRepository { get; }
        IUserProfileRepository UserProfileRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
