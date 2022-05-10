using BookStore.DAL;
using BookStore.Repositories.Interfaces;

namespace BookStore.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ContextDB _contextDB;
        private IBookRepository? _bookRepository;
        private IBookGenresRepository? _bookGenresRepository;
        private IAuthorRepository? _authorRepository;
        private IBookDetailsRepository? _bookDetailsRepository;
        private IReviewRepository? _reviewRepository;
        private IBookAuthorRepository? _bookAuthorRepository;
        private IUserBookRepository? _userBookRepository;
        private IUserProfileRepository? _userProfileRepository;

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

        public IAuthorRepository AuthorRepository
        {
            get
            {
                if (_authorRepository == null)
                {
                    _authorRepository = new AuthorRepository(_contextDB);
                }

                return _authorRepository;
            }
        }

        public IBookDetailsRepository BookDetailsRepository
        {
            get
            {
                if (_bookDetailsRepository == null)
                {
                    _bookDetailsRepository = new BookDetailsRepository(_contextDB);
                }

                return _bookDetailsRepository;
            }
        }

        public IReviewRepository ReviewRepository
        {
            get
            {
                if (_reviewRepository == null)
                {
                    _reviewRepository = new ReviewRepository(_contextDB);
                }

                return _reviewRepository;
            }
        }

        public IBookAuthorRepository BookAuthorRepository
        {
            get
            {
                if (_bookAuthorRepository == null)
                {
                    _bookAuthorRepository = new BookAuthorRepository(_contextDB);
                }

                return _bookAuthorRepository;
            }
        }

        public IUserBookRepository UserBookRepository
        {
            get
            {
                if (_userBookRepository == null)
                {
                    _userBookRepository = new UserBookRepository(_contextDB);
                }

                return _userBookRepository;
            }
        }

        public IUserProfileRepository UserProfileRepository
        {
            get
            {
                if (_userProfileRepository == null)
                {
                    _userProfileRepository = new UserProfileRepository(_contextDB);
                }

                return _userProfileRepository;
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
