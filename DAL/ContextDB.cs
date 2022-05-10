using BookStore.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DAL
{
    public class ContextDB : IdentityDbContext<IdentityUser>
    {
        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
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
