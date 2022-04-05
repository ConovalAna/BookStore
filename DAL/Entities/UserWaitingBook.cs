using System.ComponentModel.DataAnnotations;

namespace BookStore.DAL.Entities
{
    public class UserWaitingBook
    {
        [Key]
        public int UserWaitingBookID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }

        public virtual Book Book { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
