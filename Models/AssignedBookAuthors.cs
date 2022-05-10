using BookStore.DAL.Entities;

namespace BookStore.Models
{
    public class AssignedBookAuthors
    {
        public IEnumerable<Author> AuthorsCollection { get; set; }
    }
}
