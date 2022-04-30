using BookStore.DAL;
using BookStore.Repositories.Interfaces;

namespace BookStore.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ContextDB _contextDB;
        private IBookRepository? _bookRepository;
        private IBookGenresRepository? _bookGenresRepository;

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
        public IBookGenresRepository BookGenresRepository
        {
            get
            {
                if (_bookGenresRepository == null)
                {
                    _bookGenresRepository = new BookGenresRepository(_contextDB);
                }

                return _bookGenresRepository;
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
