using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.DAL.Entities
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string DownloadLink { get; set; }
        public string ShortDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        [NotMapped]
        public int[]? SelectedAuthorsIDArray { get; set; }

        [NotMapped]
        public bool UserReadBook { get; set; }
      
        [NotMapped]
        public bool UserAddedReview { get; set; }

        public virtual BookDetails? Details { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<BookGenre>? Genres { get; set; }
        public virtual ICollection<BookAuthor>? BookAuthors { get; set; }
    }
}
