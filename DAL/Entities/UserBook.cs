using System.ComponentModel.DataAnnotations;

namespace BookStore.DAL.Entities
{
    public class UserBook
    {
        [Key]
        public int UserBookID { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime DownloadDate { get; set; }

        public virtual Book Book { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
