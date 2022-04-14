namespace BookStore.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        IBookRepository BookRepository { get; }

        void Save();
        Task SaveAsync();
    }
}
