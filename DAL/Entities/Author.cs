using System.ComponentModel.DataAnnotations;

namespace BookStore.DAL.Entities
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string ImageURL { get; set; }

        public virtual ICollection<BookAuthor> BookAuthors { get; set; }

    }
}
