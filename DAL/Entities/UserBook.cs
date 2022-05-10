using System.ComponentModel.DataAnnotations;

namespace BookStore.DAL.Entities
{
    public class UserBook
    {
        [Key]
        public int UserBookID { get; set; }
        public int UserProfileID { get; set; }
        public int BookID { get; set; }
        public DateTime DownloadDate { get; set; }

        public virtual Book Book { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
