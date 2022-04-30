using System.ComponentModel.DataAnnotations;

namespace BookStore.DAL.Entities
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }
        public int BookID { get; set; }
        public int UserID { get; set; }
        public int UserProfileID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Details { get; set; }
        public short Rating { get; set; }

        public virtual Book? Book { get; set; }
        public virtual UserProfile? UserProfile { get; set; }
    }
}
