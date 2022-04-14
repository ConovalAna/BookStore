using BookStore.DAL;
using BookStore.Repositories.Interfaces;

namespace BookStore.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ContextDB _contextDB;
        private IBookRepository? _bookRepository;   

        public IBookRepository BookRepository
        {
            get
            {
                if (_bookRepository == null)
                {
                    _bookRepository = new BookRepository(_contextDB);
                }

                return _bookRepository;
            }
        }

        public RepositoryWrapper(ContextDB contextDB)
        {
            _contextDB = contextDB;
        }

        public void Save()
        {
            _contextDB.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _contextDB.SaveChangesAsync();
        } 
    }
}
