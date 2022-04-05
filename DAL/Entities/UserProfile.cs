using System.ComponentModel.DataAnnotations;

namespace BookStore.DAL.Entities
{
    public class UserProfile
    {
        [Key]
        public int UserProfileID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ImageURL { get; set; }
        public virtual UserRoles Role { get; set; }
    }
}
