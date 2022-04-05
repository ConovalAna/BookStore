using BookStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DAL
{
    public class ContextDB : DbContext
    {
        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {
        }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookAuthor> BookAuthors { get; set; }
        public virtual DbSet<BookDetails> BookDetails { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<UserBook> UserBooks { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<UserWaitingBook> UserWaitingBooks { get; set; }
        public virtual DbSet<BookGenre> BookGenres { get; set; }
    }
}
