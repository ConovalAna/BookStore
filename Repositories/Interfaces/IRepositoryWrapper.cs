namespace BookStore.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        IBookRepository BookRepository { get; }
        IBookGenresRepository BookGenresRepository { get; }

        void Save();
        Task SaveAsync();
    }
}
