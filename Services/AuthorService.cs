using BookStore.DAL.Entities;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;

namespace BookStore.Services
{
    public class AuthorService : IAuthorService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public AuthorService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }


        public async Task<int> AddAuthorAsync(Author author)
        {
            var result = 0;

            if (author != null)
            {
                _repositoryWrapper.AuthorRepository.Create(author);
                await _repositoryWrapper.SaveAsync();
                result = author.AuthorID;
            }
            return result;
        }

        public async Task DeleteAuthor(int authorId)
        {
            var author = _repositoryWrapper.AuthorRepository.FindByCondition(i => i.AuthorID == authorId).FirstOrDefault();

            if (author == null)
            {
                return;
            }

            _repositoryWrapper.AuthorRepository.Delete(author);

            await _repositoryWrapper.SaveAsync();
        }

        public async Task EditAuthor(Author author)
        {
            _repositoryWrapper.AuthorRepository.Update(author);

            await _repositoryWrapper.SaveAsync();
        }

        public List<Author> GetAllAuthors()
        {
            throw new NotImplementedException();
        }

        public Author GetAuthor(int id)
        {
            var author = _repositoryWrapper.AuthorRepository.FindByCondition(aut => aut.AuthorID == id).FirstOrDefault();
            return author;
        }

        public List<Author> GetAuthors()
        {
            var authors = _repositoryWrapper.AuthorRepository.FindAll().ToList();
            return authors;
        }

        public List<Author> GetFilteredAuthors(string prefSearch)
        {
            throw new NotImplementedException();
        }
        public bool AuthorExists(int id)
        {
            return _repositoryWrapper.AuthorRepository.FindByCondition(e => e.AuthorID == id).Any();
        }
    }
}
