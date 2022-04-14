using System.ComponentModel.DataAnnotations;

namespace BookStore.DAL.Entities
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string DownloadLink { get; set; }
        public string Edition { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }


        public virtual BookDetails? Details { get; set; }
        public virtual ICollection<BookGenre>? Genres { get; set; }
        public virtual ICollection<BookAuthor>? BookAuthors { get; set; }
    }
}
